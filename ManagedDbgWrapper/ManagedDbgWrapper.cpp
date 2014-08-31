#include "ManagedDbgWrapper.h"
#include <vcclr.h>

namespace ManagedDbgWrapper
{
	// we need a trampoline so we can call the whole event chain and wrap
	// (additional) parameters in our event argument class

	// our breakpoints
	unsigned int ManagedCCDebugEngine::bpEventTrampoline(DEBUG_EVENT dbgEvent)
	{
		DbgCallbackEventArgs^ ea = gcnew DbgCallbackEventArgs(dbgEvent, DbgContinueMethod::ExceptionNotHandled);
		OnBreakPoint(ea);
		return (unsigned int)ea->ContinueMethod;
	}

	// all other debug events
	unsigned int ManagedCCDebugEngine::dbgEventTrampoline(DEBUG_EVENT dbgEvent)
	{
		DbgCallbackEventArgs^ ea = gcnew DbgCallbackEventArgs(dbgEvent, DbgContinueMethod::ExceptionNotHandled);
		OnDbgEvent(ea);
		return (unsigned int)ea->ContinueMethod;
	}

	void ManagedCCDebugEngine::DbgAttachedEventTrampoline()
	{
		OnDbgAttached();
	}

	bool ManagedCCDebugEngine::debug(String^ application, String^ parameters)
	{
		// there is an implicit conversion from interior_ptr to pin_ptr!
		pin_ptr<const wchar_t> app = PtrToStringChars(application);
		pin_ptr<const wchar_t> params = PtrToStringChars(parameters);
		return bpInjector_->debugProcess(app, params);
	}

	bool ManagedCCDebugEngine::debugActiveProcess(unsigned int processID)
	{
		return bpInjector_->debugActiveProcess(processID);
	}

	bool ManagedCCDebugEngine::run()
	{
		return bpInjector_->run();
	}

	bool ManagedCCDebugEngine::setBreakPoint(IntPtr address, bool oneShot)
	{
		return bpInjector_->setBreakPoint((LPVOID)address, oneShot);
	}
}
