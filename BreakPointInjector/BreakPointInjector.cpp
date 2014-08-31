/********************************************************************
created:	2004/07/19
modified:	02:11:2007
filename: 	E:\Sourcecode\vpnLoader\BreakPointInjector\BreakPointInjector.cpp
file path:	E:\Sourcecode\vpnLoader\BreakPointInjector
file base:	BreakPointInjector
file ext:	cpp
author:		Jan Newger
version:    v0.22
purpose:    This class allows you to inject Breakpoints into arbitrary
processes. 
history:	v0.1: first release. kind of working ;)
v0.2: fixed some bugs; breakpoints can be removed in the 
application defined callback. Introduced a third callback
function "BPEpilogCallback" which gets called, as soon as
the debuggee is resumed
v0.21: added support for multithreading
v0.22: added support for individual handling of access violations
or foreign breakpoints in derived classes
*********************************************************************/

#include "BreakPointInjector.h"
#include <mmsystem.h>
#include <TCHAR.h>

using namespace std;

BreakPointInjector::BreakPointInjector() :
	attached_(false),
	bpPrologCallback(NULL),
	bpCallback(NULL),
	bpEpilogCallback(NULL),
	dbgEventCallback_(NULL),
	entryBP(true),
	singleStepOk_(false),
	killOnExit_(false),
	debugging_(true),
	resumeProcess_(false)
{
	bpMap = new BPMapType;
	threadInfoMap = new ThreadMapType;
}

BreakPointInjector::~BreakPointInjector()
{
	delete bpMap;
	ThreadMapType::iterator i;
	for(i = threadInfoMap->begin(); i != threadInfoMap->end(); ++i)
		CloseHandle(i->second.hThread);
	delete threadInfoMap;
}

bool BreakPointInjector::debugProcess(LPCTSTR exeFile, LPCTSTR params)
{
	STARTUPINFO startupInfo;
	PROCESS_INFORMATION processInformation;
	memset(&startupInfo, 0, sizeof(startupInfo));
	startupInfo.cb = sizeof(startupInfo);

	// use generic string functions so we can be compiled with ANSI and UNICODE
	LPTSTR p = NULL;
	if (params != NULL)
	{
		size_t len = _tcslen(params);
		if (len > 0)
		{
			p = new wchar_t(len*sizeof(TCHAR));
			_tcscpy(p, params);
		}
	}
	 
	bool retVal = (CreateProcess(exeFile,
								 p, 
								 NULL, 
								 NULL, 
								 false, 
								 DEBUG_ONLY_THIS_PROCESS | CREATE_SUSPENDED, 
								 NULL, 
			   					 NULL, 
								 &startupInfo, 
								 &processInformation) != 0);
	attached_ = retVal;
	pInfo = processInformation;
	resumeProcess_ = true;
	delete p;

	return retVal;
}

bool BreakPointInjector::debugActiveProcess(DWORD pID)
{
	attached_ = (DebugActiveProcess(pID) != 0);
	resumeProcess_ = false;
	return attached_;
}

/* Patches a memory location pointed to by address and returns overwritten bytes
in buffer pointed to by replacedMem (if it's non-NULL) */
bool BreakPointInjector::patchMem(LPVOID address, LPVOID buffer, DWORD size, LPVOID replacedMem)
{
	DWORD oldProtect;

	//change requested memory page attributes to read/write
	if (!VirtualProtectEx(pInfo.hProcess, address, size, PAGE_READWRITE, &oldProtect)) return false;

	//first save memory block
	if (replacedMem != NULL)
	{
		DWORD read;
		ReadProcessMemory(pInfo.hProcess, address, replacedMem, size, &read);
		if (read != size) return false;
	}

	//perform actual patch...
	if (!WriteProcessMemory(pInfo.hProcess, address, buffer, size, NULL)) return false;

	//...and change back (uncritical if we fail here)
	VirtualProtectEx(pInfo.hProcess, address, size, oldProtect, &oldProtect);
	FlushInstructionCache(pInfo.hProcess, address, size);

	return true;
}

/* Sets a breakpoint in the debuggee at the specified address
and stores address and overwritten code in a STL-map, so we can
later associate a breakpoint (i.e. address) with the belonging
overwritten opcode */
bool BreakPointInjector::setBreakPoint(LPVOID address, bool oneShot)
{
	DWORD origOpcode;

	BPMapType::const_iterator i = bpMap->find(address);
	if (i != bpMap->end()) return true;

	//write int3 opcode to memory and save overwritten code
	if (patchMem(address, (LPVOID)&INT3, 1, (LPVOID)&origOpcode))
	{
		BreakPointInfo bpInfo;
		bpInfo.opcode = origOpcode;
		bpInfo.oneShot = oneShot;
		(*bpMap)[address] = bpInfo;
		return true;
	}
	else return false;
}

/*All exception events come here. We only handle breakpoints and hand the rest over to
the SEH which *must* handle it. If noone does, the program crashes (see below)
*/
DWORD BreakPointInjector::handleException(const DEBUG_EVENT& dbgEvent)
{
	DWORD exceptCode = dbgEvent.u.Exception.ExceptionRecord.ExceptionCode;
	LPVOID exceptAddr = dbgEvent.u.Exception.ExceptionRecord.ExceptionAddress;

	//default is to disable exception handling and continue execution
	DWORD continueStatus = DBG_CONTINUE;

	if (exceptCode == EXCEPTION_BREAKPOINT)
	{
		/* when we attach to the debugge resp. create it with debug-flag set, windows suspends the
		process and sets an entry breakpoint. We ignore it ;) */
		if (entryBP) 
		{
			entryBP = false;		
			if (dbgAttachedCallback_ != NULL) dbgAttachedCallback_();
			return DBG_CONTINUE;
		}
		
		BPMapType::const_iterator i = bpMap->find(exceptAddr);
		
		//is this one of *our* breakpoints ?
		if (i != bpMap->end())
		{
			BreakPointInfo bpInfo = (*bpMap)[exceptAddr];
			/* The "bpPrologCallback" gives us the opportunity to make modifications to the
			process memory, i.e. change code, modify thread context, etc. before this class performs
			any modifications. If "restoreOpcode" is set to false, the callback function itself is 
			responsible	for writing appropriate opcode(s) to memory. 
			"bpPrologCallback" must *not* modify the EIP register! This has to be done in "bpCallback"! */
			bool restoreOpcode = true;
			if (bpPrologCallback != NULL) bpPrologCallback(exceptAddr, pInfo, restoreOpcode);

			//get current thread context
			HANDLE currentThread = (*threadInfoMap)[dbgEvent.dwThreadId].hThread;

			CONTEXT threadContext;
			threadContext.ContextFlags = CONTEXT_CONTROL;
			GetThreadContext(currentThread, &threadContext);

			//the instruction pointer points to opcode followed by our breakpoint, so decrement by one
			threadContext.Eip = threadContext.Eip - 1;
			threadContext.ContextFlags = CONTEXT_CONTROL;
			SetThreadContext(currentThread, &threadContext);
			
			//should we restore original opcode or was this handled by "bpPrologCallback" ?
			if (restoreOpcode) patchMem(exceptAddr, &bpInfo.opcode, 1, NULL);
			if (bpCallback)	bpCallback(dbgEvent);

			// don't set trap flag if we only have a one-shot breakpoint
			if (!bpInfo.oneShot)
			{
				/* We are *not* in single stepping mode, so turn on single stepping by setting the appropriate 
				processor flag. This will cause the processor to only execute one single instruction
				followed by an interrupt */
				threadContext.ContextFlags = CONTEXT_FULL;
				GetThreadContext(currentThread, &threadContext);
				threadContext.EFlags |= 0x100;
				threadContext.ContextFlags = CONTEXT_FULL;
				SetThreadContext(currentThread, &threadContext);
				
				/* Save address of current breakpoint so we can rewrite it after the single stepping
				exception has been thrown */
				(*threadInfoMap)[dbgEvent.dwThreadId].lastBP = exceptAddr;
				singleStepOk_ = true;
			}
		} 
		else 
		{
			// this is not our breakpoint so call default debug event handler
			if (dbgEventCallback_ != NULL) continueStatus = dbgEventCallback_(dbgEvent);
			else continueStatus = DBG_EXCEPTION_NOT_HANDLED;
		}
	}
	else if (exceptCode == EXCEPTION_SINGLE_STEP && singleStepOk_)
	{
		/* We are in single step mode and "stepped" one instruction over one of our interrupts,
		which has already been overwritten. So we have to reset it and turn off single stepping.
		The address of this interrupt was previously saved in "lastBP" */
		HANDLE currentThread = (*threadInfoMap)[dbgEvent.dwThreadId].hThread;
		CONTEXT threadContext;
		threadContext.ContextFlags = CONTEXT_FULL;
		GetThreadContext(currentThread, &threadContext);
		//clear trap flag
		threadContext.EFlags &= ~0x100;
		threadContext.ContextFlags = CONTEXT_FULL;
		
		SetThreadContext(currentThread, &threadContext);
		patchMem((*threadInfoMap)[dbgEvent.dwThreadId].lastBP, (LPVOID)&INT3, 1, NULL);
		singleStepOk_ = false;
		continueStatus = DBG_CONTINUE;
	}
	else 
	{
		/* This is neither a breakpoint nor a single step interrupt but some other kind of exception. 
		Since we are not responsible for them, we let the SEH do this. This means, that either the 
		program itself has an exception	handler which catches the exception, or that it doesn't! 
		If this is not a first chance exception the programm will crash! Anyway, not our fault ;) */
		if (dbgEventCallback_ != NULL) continueStatus = dbgEventCallback_(dbgEvent);
		else continueStatus = DBG_EXCEPTION_NOT_HANDLED;
	}
	
	return continueStatus;
}

bool BreakPointInjector::run()
{
	if (!attached_) return false;

	DEBUG_EVENT dbgEvent;

	if (resumeProcess_) ResumeThread(pInfo.hThread);

	while (debugging_)
	{
		// clear event code so we dont get duplicate events
		dbgEvent.dwDebugEventCode = 0;
		WaitForDebugEvent(&dbgEvent, 500);

		DWORD continueStatus = DBG_CONTINUE;
		bool wasBP = false;

		switch (dbgEvent.dwDebugEventCode)
		{
			case CREATE_PROCESS_DEBUG_EVENT:
				pInfo.hProcess = dbgEvent.u.CreateProcessInfo.hProcess;
				pInfo.dwProcessId = dbgEvent.dwProcessId;
				pInfo.hThread = dbgEvent.u.CreateProcessInfo.hThread;
				pInfo.dwThreadId = dbgEvent.dwThreadId;
				(*threadInfoMap)[dbgEvent.dwThreadId].hThread = dbgEvent.u.CreateProcessInfo.hThread;
				(*threadInfoMap)[dbgEvent.dwThreadId].lastBP = NULL;

				if (dbgEventCallback_ != NULL) dbgEventCallback_(dbgEvent);
				CloseHandle(dbgEvent.u.CreateProcessInfo.hFile); 
				break;

			case CREATE_THREAD_DEBUG_EVENT:
				(*threadInfoMap)[dbgEvent.dwThreadId].hThread = dbgEvent.u.CreateThread.hThread;
				(*threadInfoMap)[dbgEvent.dwThreadId].lastBP = NULL;
				break;

			case EXIT_THREAD_DEBUG_EVENT:
				threadInfoMap->erase(dbgEvent.dwThreadId);
				break;

			case EXCEPTION_DEBUG_EVENT:
				continueStatus = handleException(dbgEvent);
				wasBP = dbgEvent.u.Exception.ExceptionRecord.ExceptionCode == EXCEPTION_BREAKPOINT ? true : false;
				break;

			case LOAD_DLL_DEBUG_EVENT:
				if (dbgEventCallback_ != NULL) dbgEventCallback_(dbgEvent);
				CloseHandle(dbgEvent.u.LoadDll.hFile);
				break;

			case OUTPUT_DEBUG_STRING_EVENT:
				break;
		}

		ContinueDebugEvent(dbgEvent.dwProcessId, dbgEvent.dwThreadId, continueStatus);

		if (dbgEvent.dwDebugEventCode == EXIT_PROCESS_DEBUG_EVENT)
		{
			if (dbgEventCallback_ != NULL) dbgEventCallback_(dbgEvent);
			break;
		}
		
		if (wasBP && (bpEpilogCallback != NULL)) 
		{
			BPMapType::const_iterator i = bpMap->find(dbgEvent.u.Exception.ExceptionRecord.ExceptionAddress);
			//is this one of *our* breakpoints ?
			if (i != bpMap->end()) bpEpilogCallback(dbgEvent.u.Exception.ExceptionRecord.ExceptionAddress, pInfo);
		}
	}

	// we should detach
	if (!debugging_)
	{
		DebugSetProcessKillOnExit(killOnExit_);
		if(DebugActiveProcessStop(pInfo.dwProcessId))
		{
			// we need a new process handle since ContinueDebugEvent alreaedy closed our hProcess handle
			pInfo.hProcess = OpenProcess(PROCESS_ALL_ACCESS, FALSE, pInfo.dwProcessId);
			// clear all breakpoints from process
			while (bpMap->size() > 0) removeBreakPoint(bpMap->begin()->first, true);
			CloseHandle(pInfo.hProcess);
		}
	}
	return true;
}

bool BreakPointInjector::detach(bool killProcess)
{
	killOnExit_ = killProcess;
	debugging_ = false;
	return true;
}

void BreakPointInjector::removeBreakPoint(LPVOID address, bool restoreOpcode)
{
	BPMapType::iterator i = bpMap->find(address);
	if (i != bpMap->end())
	{
		BreakPointInfo bpInfo = i->second;
		if (restoreOpcode) patchMem(address, &bpInfo.opcode, 1, NULL);
		bpMap->erase(i);
	}
}

void BreakPointInjector::setBPCallback(PFnDbgEventCallback funcPointer)
{
	bpCallback = funcPointer;
}

void BreakPointInjector::setBPPrologCallback(PFnBPPrologCallback funcPointer)
{
	bpPrologCallback = funcPointer;
}

void BreakPointInjector::setBPEpilogCallback(PFnBPEpilogCallback funcPointer)
{
	bpEpilogCallback = funcPointer;
}

void BreakPointInjector::setDbgEventCallback(PFnDbgEventCallback funcPointer)
{
	dbgEventCallback_ = funcPointer;
}

void BreakPointInjector::setDbgAttachedCallback(PFnDbgAttachedCallback funcPointer)
{
	dbgAttachedCallback_ = funcPointer;
}