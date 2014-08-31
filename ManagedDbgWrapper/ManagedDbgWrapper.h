// ManagedDbgWrapper.h
#pragma once

#include "../BreakPointInjector/BreakPointInjector.h"

using namespace System;
using namespace System::Runtime::InteropServices;

namespace ManagedDbgWrapper 
{
	public enum class DbgExceptionCode
	{
		// TODO: add all other exception codes
		BreakPoint = EXCEPTION_BREAKPOINT,
		AccessViolation = EXCEPTION_ACCESS_VIOLATION
	};

	[StructLayout(LayoutKind::Sequential)]
	public value struct LOAD_DLL_DEBUG_INFO
	{
		IntPtr hFile;
		IntPtr lpBaseOfDll;
		DWORD dwDebugInfoFileOffset;
		DWORD nDebugInfoSize;
		IntPtr lpImageName;
		WORD fUnicode;
	};

	[StructLayout(LayoutKind::Sequential)]
	public value struct CREATE_PROCESS_DEBUG_INFO
	{
		IntPtr hFile;
		IntPtr hProcess;
		IntPtr hThread;
		IntPtr lpBaseOfImage;
		DWORD dwDebugInfoFileOffset;
		DWORD nDebugInfoSize;
		IntPtr lpThreadLocalBase;
		IntPtr lpStartAddress;
		IntPtr lpImageName;
		WORD fUnicode;
	};

	[StructLayout(LayoutKind::Sequential)]
	public value struct PROCESS_INFORMATION 
	{
		IntPtr hProcess;
		IntPtr hThread;
		DWORD dwProcessId;
		DWORD dwThreadId;
	};

	[StructLayout(LayoutKind::Sequential)]
	public value struct EXCEPTION_RECORD
	{
		DbgExceptionCode ExceptionCode;
		DWORD ExceptionFlags;
		IntPtr ExceptionRecord;
		IntPtr ExceptionAddress;
		DWORD NumberParameters;
		// if we use ByValArray marshalling, we get TypeLoadException error at runtime!
		//[MarshalAs(UnmanagedType::ByValArray, SizeConst=EXCEPTION_MAXIMUM_PARAMETERS)] static array<ULONG_PTR>^ ExceptionInformation;
		IntPtr ExceptionInformation0;
		IntPtr ExceptionInformation1;
		IntPtr ExceptionInformation2;
		IntPtr ExceptionInformation3;
		IntPtr ExceptionInformation4;
		IntPtr ExceptionInformation5;
		IntPtr ExceptionInformation6;
		IntPtr ExceptionInformation7;
		IntPtr ExceptionInformation8;
		IntPtr ExceptionInformation9;
		IntPtr ExceptionInformation10;
		IntPtr ExceptionInformation11;
		IntPtr ExceptionInformation12;
		IntPtr ExceptionInformation13;
		IntPtr ExceptionInformation14;
	};

	[StructLayout(LayoutKind::Sequential)]
	public value struct EXCEPTION_DEBUG_INFO
	{
		EXCEPTION_RECORD ExceptionRecord;
		DWORD dwFirstChance;
	};

	public enum class DbgEventCode 
	{
		// TODO: add other event codes
		LoadDLL = LOAD_DLL_DEBUG_EVENT, 
		ProcessExited = EXIT_PROCESS_DEBUG_EVENT,
		Exception = EXCEPTION_DEBUG_EVENT,
		ProcessCreated = CREATE_PROCESS_DEBUG_EVENT
	};

	[StructLayout(LayoutKind::Explicit)]
	public value struct DEBUG_EVENT
	{
		[FieldOffset(0)] DbgEventCode dwDebugEventCode;
		[FieldOffset(4)] DWORD dwProcessId;
		[FieldOffset(8)] DWORD dwThreadId;
		[FieldOffset(12)] EXCEPTION_DEBUG_INFO Exception;
		[FieldOffset(12)] LOAD_DLL_DEBUG_INFO LoadDll;
		[FieldOffset(12)] CREATE_PROCESS_DEBUG_INFO CreateProcessInfo;
		// TODO
		/*[FieldOffset(12)] CREATE_THREAD_DEBUG_INFO CreateThread;
		[FieldOffset(12)] EXIT_THREAD_DEBUG_INFO ExitThread;
		[FieldOffset(12)] EXIT_PROCESS_DEBUG_INFO ExitProcess;
		[FieldOffset(12)] UNLOAD_DLL_DEBUG_INFO UnloadDll;
		[FieldOffset(12)] OUTPUT_DEBUG_STRING_INFO DebugString;
		[FieldOffset(12)] RIP_INFO RipInfo;*/
	};

	public enum class DbgContinueMethod 
	{
		Continue = DBG_CONTINUE, 
		ExceptionNotHandled = DBG_EXCEPTION_NOT_HANDLED
	};

	// event arguments class for all debug exception event callbacks
	public ref class DbgCallbackEventArgs : EventArgs
	{
	private:
		DEBUG_EVENT event_;
		DbgContinueMethod method_;

	public:
		DbgCallbackEventArgs(DEBUG_EVENT dbgEvent, DbgContinueMethod method) :
			event_(dbgEvent), method_(method)
		{
		}

		property DbgContinueMethod ContinueMethod
		{
			DbgContinueMethod get() { return method_; }
			void set(DbgContinueMethod value) { method_ = value; }
		}

		property DEBUG_EVENT DebugEvent
		{
			DEBUG_EVENT get() { return event_; }
		}
	};

	// the public managed event handlers
	public delegate void DbgExceptionEventHandler(DbgCallbackEventArgs^ ea);
	public delegate void DbgAttachedEventHandler();

	// internal trampoline delegate types
	[UnmanagedFunctionPointer(CallingConvention::StdCall)]
	private delegate unsigned int DbgEventTrampolineHandler(DEBUG_EVENT exceptInfo);
	[UnmanagedFunctionPointer(CallingConvention::StdCall)]
	private delegate void DbgAttachedTrampolineHandler();

	// our code coverage debug engine
	public ref class ManagedCCDebugEngine
	{
	private:
		BreakPointInjector* bpInjector_;
		DbgEventTrampolineHandler^ dbgTrampolineDelegate_;
		DbgEventTrampolineHandler^ bpTrampolineDelegate_;
		DbgAttachedTrampolineHandler^ entryBPTrampolineDelegate_;
		
		unsigned int dbgEventTrampoline(DEBUG_EVENT dbgEvent);
		unsigned int bpEventTrampoline(DEBUG_EVENT dbgEvent);
		void DbgAttachedEventTrampoline();

	public:
		ManagedCCDebugEngine()
		{
			bpInjector_ = new BreakPointInjector();
			
			bpTrampolineDelegate_ = gcnew DbgEventTrampolineHandler(this, &ManagedCCDebugEngine::bpEventTrampoline);
			dbgTrampolineDelegate_ = gcnew DbgEventTrampolineHandler(this, &ManagedCCDebugEngine::dbgEventTrampoline);
			entryBPTrampolineDelegate_ = gcnew DbgAttachedTrampolineHandler(this, &ManagedCCDebugEngine::DbgAttachedEventTrampoline);
			
			// marshall function pointers to trampoline delegate handlers
			IntPtr p = Marshal::GetFunctionPointerForDelegate(bpTrampolineDelegate_);
			bpInjector_->setBPCallback((PFnDbgEventCallback)p.ToPointer());
			p = Marshal::GetFunctionPointerForDelegate(dbgTrampolineDelegate_);
			bpInjector_->setDbgEventCallback((PFnDbgEventCallback)p.ToPointer());
			p = Marshal::GetFunctionPointerForDelegate(entryBPTrampolineDelegate_);
			bpInjector_->setDbgAttachedCallback((PFnDbgAttachedCallback)p.ToPointer());
		}

		~ManagedCCDebugEngine()
		{
			// close managed objects, then call finalizer to free native stuff
			this->!ManagedCCDebugEngine();
		}

		!ManagedCCDebugEngine()
		{
			delete bpInjector_;
		}

		bool detach(bool killProcess) { return bpInjector_->detach(killProcess); }

		property PROCESS_INFORMATION ProcessInfo
		{
			PROCESS_INFORMATION get() 
			{ 
				_PROCESS_INFORMATION tmp = bpInjector_->getProcInfo();
				PROCESS_INFORMATION pInfo;
				pInfo.dwProcessId = tmp.dwProcessId;
				pInfo.dwThreadId = tmp.dwThreadId;
				pInfo.hProcess = (IntPtr)tmp.hProcess;
				pInfo.hThread = (IntPtr)tmp.hThread;
				return pInfo;
			}
		}

		// our public events
		event DbgExceptionEventHandler^ OnBreakPoint;
		event DbgExceptionEventHandler^ OnDbgEvent;
		event DbgAttachedEventHandler^ OnDbgAttached;

		bool debug(String^ application, String^ parameters);
		bool debugActiveProcess(unsigned int processID);
		bool run();
		bool setBreakPoint(IntPtr address, bool oneShot);
	};
}
