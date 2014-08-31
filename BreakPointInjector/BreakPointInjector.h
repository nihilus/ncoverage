#pragma once
#define WIN32_LEAN_AND_MEAN
#include <windows.h>
#include <iostream>
#include <map>

/*
function pointers to callback functions
-BPPrologCallback function is called *before* the class is doing any modifications in memory/thread context
-BPCallback is called *after* changes were made (i.e. EIP reset, opcode rewritten if wanted), but (of course)
before the process is resumed again
*/
typedef void (__stdcall* PFnBPPrologCallback)(LPVOID bpAddress, PROCESS_INFORMATION processInfo, bool& restoreOpcode);
typedef void (__stdcall* PFnBPEpilogCallback)(LPVOID bpAddress, PROCESS_INFORMATION processInfo);
typedef DWORD (__stdcall* PFnDbgEventCallback)(DEBUG_EVENT dbgEvent);
typedef void (__stdcall* PFnDbgAttachedCallback)();

class BreakPointInjector
{
public:
	BreakPointInjector();
	~BreakPointInjector();
	bool debugProcess(LPCTSTR exeFile, LPCTSTR params);
	bool debugActiveProcess(DWORD pID);
	bool run();
	bool detach(bool killProcess);
	bool setBreakPoint(LPVOID address, bool oneShot);
	void removeBreakPoint(LPVOID address, bool restoreOpcode);
	void setBPPrologCallback(PFnBPPrologCallback funcPointer);
	void setBPCallback(PFnDbgEventCallback funcPointer);
	void setBPEpilogCallback(PFnBPEpilogCallback funcPointer);
	void setDbgEventCallback(PFnDbgEventCallback funcPointer);
	void setDbgAttachedCallback(PFnDbgAttachedCallback funcPointer);
	PROCESS_INFORMATION getProcInfo() { return pInfo; }

protected:
	bool patchMem(LPVOID address, LPVOID buffer, DWORD size, LPVOID replacedMem);

private:
	DWORD handleException(const DEBUG_EVENT& dbgEvent);
	static const char INT3 = 0xCC;
	struct ThreadInfo
	{
		HANDLE hThread;
		LPVOID lastBP;
	};
	struct BreakPointInfo
	{
		char opcode;
		bool oneShot;
	};

	typedef std::map<LPVOID, BreakPointInfo> BPMapType;
	typedef std::map<DWORD, ThreadInfo> ThreadMapType;
	
	PFnBPPrologCallback bpPrologCallback;
	PFnDbgEventCallback bpCallback;
	PFnBPEpilogCallback bpEpilogCallback;
	PFnDbgEventCallback dbgEventCallback_;
	PFnDbgAttachedCallback dbgAttachedCallback_;

	bool attached_;
	bool debugging_;
	bool killOnExit_;
	bool resumeProcess_;
	//stores bp-address/overwritten opcode pairs
	BPMapType* bpMap;
	ThreadMapType* threadInfoMap;
	PROCESS_INFORMATION pInfo;
	bool entryBP;
	bool singleStepOk_;
};
