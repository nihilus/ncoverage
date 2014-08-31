using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Collections.ObjectModel;
using System.Runtime.InteropServices;
using Microsoft.Win32.SafeHandles;
using ManagedDbgWrapper;
using System.Diagnostics;
using System.IO;
using System.Windows.Forms;

namespace NCoverage.CodeCoverage
{
	#region Event delegates / helper classes

	public delegate void CoverageFinishedDelegate(CoverageFinishedEventArgs e);
	public delegate void ModuleLoadedDelegate(ModuleLoadedEventArgs e);
	public delegate void DebuggerAttachedDelegate(DebuggerAttachedEventArgs e);

	public class DebuggerAttachedEventArgs : EventArgs
	{
		private uint processID_;
		private string exeName_;
		public DebuggerAttachedEventArgs(uint processID, string exeName)
		{
			processID_ = processID;
			exeName_ = exeName;
		}

		public string ExeName
		{
			get { return exeName_; }
		}

		public uint ProcessID
		{
			get { return processID_; }
		}
	}

	public class CoverageFinishedEventArgs : EventArgs
	{
		private Recording recording_;
		public CoverageFinishedEventArgs(Recording recording)
		{
			recording_ = recording;
		}

		public Recording FinishedRecording
		{
			get { return recording_; }
		}
	}

	public class ModuleLoadedEventArgs : EventArgs
	{
		private string moduleName_;
		private uint imageBase_;
		public ModuleLoadedEventArgs(string moduleName, uint imageBase)
		{
			moduleName_ = moduleName;
			imageBase_ = imageBase;
		}

		public string ModuleName
		{
			get { return moduleName_; }
		}

		public uint ImageBase
		{
			get { return imageBase_; }
		}
	}

	#endregion // Event delegates / helper classes

	/// <summary>
	/// this class maages the whole process of code coverage, i.e. handles the debugger class
	/// and manages all the breakpoint hits and associates them with the given modules
	/// </summary>
	public class NCodeCoverage
	{
		private ManagedCCDebugEngine debugEngine_;
		private List<Recording> recordings_;
		private Recording activeRecording_;
		private List<PEModule> modules_;
		private List<CoverageModule> finalSet_;
		private ReadOnlyCollection<CoverageModule> roFinalSet_;
		// lookup could be faster if we would compare hashes of the modules path
		private SortedDictionary<string, PEModule> moduleNamesMap_;
		private Thread dbgthread_;
		private bool restoreBreakPoints_;
		private Process dbgProcess_;
		private string commandLine_;
		private string exeName_;
		private bool continueAfterException_;
		private bool verbose_;
		private bool isRunning_;

		#region DLL imports

		[DllImport("Kernel32.dll", EntryPoint = "CreateFileMapping", CharSet = CharSet.Unicode, SetLastError = true)]
		private static extern SafeFileHandle CreateFileMapping(SafeFileHandle hFile, uint lpAttributes, uint flProtect, uint dwMaximumSizeHigh, uint dwMaximumSizeLow, string lpName);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern IntPtr MapViewOfFile(SafeFileHandle hFileMappingObject, uint dwDesiredAccess, uint dwFileOffsetHigh, uint dwFileOffsetLow, IntPtr dwNumberOfBytesToMap);

		[DllImport("psapi.dll", SetLastError = true)]
		private static extern uint GetMappedFileName(IntPtr hProcess, IntPtr lpv, StringBuilder lpFilename, uint nSize);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern bool UnmapViewOfFile(IntPtr lpBaseAddress);

		[DllImport("kernel32.dll", SetLastError = true)]
		private static extern uint QueryDosDevice(string lpDeviceName, StringBuilder lpTargetPath, int ucchMax);

		private const uint PAGE_READONLY = 0x2;
		private const int MAX_PATH = 260;
		private const uint FILE_MAP_READ = 0x0004;

		#endregion

		#region Hide debugger class

		private class AntiAntiDebugger
		{
			#region DLL imports

			[DllImport("kernel32.dll")]
			private static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, int size, out IntPtr lpNumberOfBytesRead);

			[DllImport("kernel32.dll")]
			private static extern bool WriteProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] lpBuffer, int size, out IntPtr lpNumberOfBytesWritten);

			[DllImport("kernel32.dll", SetLastError = true)]
			static extern bool GetThreadContext(IntPtr hThread, ref CONTEXT lpContext);

			[DllImport("Kernel32.dll", SetLastError = true)]
			private static extern bool GetThreadSelectorEntry(IntPtr hThread, uint dwSelector, out LDT_ENTRY lpSelectorEntry);

			public enum CONTEXT_FLAGS : uint
			{
				CONTEXT_i386 = 0x10000,
				CONTEXT_i486 = 0x10000,   //  same as i386
				CONTEXT_CONTROL = CONTEXT_i386 | 0x01, // SS:SP, CS:IP, FLAGS, BP
				CONTEXT_INTEGER = CONTEXT_i386 | 0x02, // AX, BX, CX, DX, SI, DI
				CONTEXT_SEGMENTS = CONTEXT_i386 | 0x04, // DS, ES, FS, GS
				CONTEXT_FLOATING_POINT = CONTEXT_i386 | 0x08, // 387 state
				CONTEXT_DEBUG_REGISTERS = CONTEXT_i386 | 0x10, // DB 0-3,6,7
				CONTEXT_EXTENDED_REGISTERS = CONTEXT_i386 | 0x20, // cpu specific extensions
				CONTEXT_FULL = CONTEXT_CONTROL | CONTEXT_INTEGER | CONTEXT_SEGMENTS,
				CONTEXT_ALL = CONTEXT_CONTROL | CONTEXT_INTEGER | CONTEXT_SEGMENTS | CONTEXT_FLOATING_POINT | CONTEXT_DEBUG_REGISTERS | CONTEXT_EXTENDED_REGISTERS
			}

			[StructLayout(LayoutKind.Sequential)]
			public struct FLOATING_SAVE_AREA
			{
				public uint ControlWord;
				public uint StatusWord;
				public uint TagWord;
				public uint ErrorOffset;
				public uint ErrorSelector;
				public uint DataOffset;
				public uint DataSelector;
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 80)]
				public byte[] RegisterArea;
				public uint Cr0NpxState;
			}

			[StructLayout(LayoutKind.Sequential)]
			public struct CONTEXT
			{
				public uint ContextFlags; //set this to an appropriate value 
				// Retrieved by CONTEXT_DEBUG_REGISTERS
				public uint Dr0;
				public uint Dr1;
				public uint Dr2;
				public uint Dr3;
				public uint Dr6;
				public uint Dr7;
				// Retrieved by CONTEXT_FLOATING_POINT
				public FLOATING_SAVE_AREA FloatSave;
				// Retrieved by CONTEXT_SEGMENTS
				public uint SegGs;
				public uint SegFs;
				public uint SegEs;
				public uint SegDs;
				// Retrieved by CONTEXT_INTEGER
				public uint Edi;
				public uint Esi;
				public uint Ebx;
				public uint Edx;
				public uint Ecx;
				public uint Eax;
				// Retrieved by CONTEXT_CONTROL
				public uint Ebp;
				public uint Eip;
				public uint SegCs;
				public uint EFlags;
				public uint Esp;
				public uint SegSs;
				// Retrieved by CONTEXT_EXTENDED_REGISTERS
				[MarshalAs(UnmanagedType.ByValArray, SizeConst = 512)]
				public byte[] ExtendedRegisters;
			}

			public struct _Bytes
			{
				public byte BaseMid;
				public byte Flags1;
				public byte Flags2;
				public byte BaseHi;    
			}

			[StructLayout(LayoutKind.Explicit)]
			public struct LDT_ENTRY
			{
				[FieldOffset(0)] public ushort LimitLow;
				[FieldOffset(2)] public ushort BaseLow;
				[FieldOffset(4)] public _Bytes Bytes;
			}

			#endregion

			private IntPtr hProcess_;
			private IntPtr hThread_;

			public AntiAntiDebugger(IntPtr hProcess, IntPtr hThread)
			{
				hProcess_ = hProcess;
				hThread_ = hThread;
			}

			private byte[] readProcessMemory(IntPtr address, int size, out int bytesWritten)
			{
				byte[] tmp = new byte[size];
				IntPtr written;
				ReadProcessMemory(hProcess_, address, tmp, size, out written);
				bytesWritten = written.ToInt32();
				return tmp;
			}

			private void writeProcessMemory(IntPtr address, byte[] buffer, out int bytesWritten)
			{
				IntPtr written;
				WriteProcessMemory(hProcess_, address, buffer, buffer.Length, out written);
				bytesWritten = written.ToInt32();
				//WriteProcessMemory(hProcess_, address, buffer, (UIntPtr)buffer.Length, out written);
				//bytesWritten = written.ToInt32();
			}

			uint arrToAddr(byte[] addr)
			{
				uint retAddr = (uint)((uint)(addr[3]) << 24 | (uint)(addr[2]) << 16 | (uint)(addr[1]) << 8 | (uint)(addr[0]));
				return retAddr;
			}

			public bool hideDebugger()
			{
				CONTEXT context = new CONTEXT();
				context.ContextFlags = (uint)CONTEXT_FLAGS.CONTEXT_ALL;
				if (!GetThreadContext(hThread_, ref context)) return false;

				// translate segment selector address to linear virtual address
				LDT_ENTRY ldtEntry;
				if (!GetThreadSelectorEntry(hThread_, context.SegFs, out ldtEntry)) return false;
				uint fsVirtAddress = (uint)((uint)(ldtEntry.Bytes.BaseHi) << 24 | (uint)(ldtEntry.Bytes.BaseMid) << 16 | (uint)(ldtEntry.BaseLow));

				// TEB pointer is at [fs:0x18]
				uint tebPtr = fsVirtAddress + 0x18;
				int read;
				// read TEB virtual address
				byte[] tmpAddr = readProcessMemory((IntPtr)tebPtr, 4, out read);
				if (read != 4) return false;

				// PEB address is at offset 0x30
				uint pebAddr = arrToAddr(tmpAddr) + 0x30;//(uint)(tmpAddr[3] << 24 | tmpAddr[2] << 16 | tmpAddr[1] << 8 | tmpAddr[0]) + 0x30;
				tmpAddr = readProcessMemory((IntPtr)pebAddr, 4, out read);
				if (read != 4) return false;

				uint patchAddr = arrToAddr(tmpAddr) + 2;//(uint)(tmpAddr[3] << 24 | tmpAddr[2] << 16 | tmpAddr[1] << 8 | tmpAddr[0]) + 2;
				byte[] patch = readProcessMemory((IntPtr)patchAddr, 4, out read);
				patch[0] = 0;
				writeProcessMemory((IntPtr)patchAddr, patch, out read);

				return read == 4 ? true : false;
			}
		}

		#endregion // Hide debugger class

		public NCodeCoverage()
		{
			recordings_ = new List<Recording>();
			modules_ = new List<PEModule>();
			moduleNamesMap_ = new SortedDictionary<string, PEModule>();
			finalSet_ = new List<CoverageModule>();
			roFinalSet_ = new ReadOnlyCollection<CoverageModule>(finalSet_);
		}

		/// <summary>
		/// one of our breakpoints was hit
		/// </summary>
		/// <param name="ea"></param>
		private void debugEngine_OnBreakPoint(DbgCallbackEventArgs ea)
		{
			activeRecording_.addHit((uint)ea.DebugEvent.Exception.ExceptionRecord.ExceptionAddress);
			Console.WriteLine("BP hit at 0x" + ea.DebugEvent.Exception.ExceptionRecord.ExceptionAddress.ToString("X") + " in thread: 0x" + ea.DebugEvent.dwThreadId.ToString("X"));
		}

		[DllImport("kernel32.dll")]
		private static extern Int32 ReadProcessMemory(IntPtr hProcess, IntPtr lpBaseAddress, [In, Out] byte[] buffer, int size, out IntPtr lpNumberOfBytesRead);

		/// <summary>
		/// all debug events end up here
		/// </summary>
		/// <param name="ea"></param>
		private void debugEngine_OnDbgEvent(DbgCallbackEventArgs ea)
		{
			switch (ea.DebugEvent.dwDebugEventCode)
			{
				case DbgEventCode.Exception:
					Console.WriteLine("exception at 0x" + ea.DebugEvent.Exception.ExceptionRecord.ExceptionAddress.ToString("X") + " in thread: 0x" + ea.DebugEvent.dwThreadId.ToString("X") + " code: 0x" + ea.DebugEvent.Exception.ExceptionRecord.ExceptionCode.ToString("X"));
					if (continueAfterException_) ea.ContinueMethod = DbgContinueMethod.Continue;
					else ea.ContinueMethod = DbgContinueMethod.ExceptionNotHandled;
					//uint addr = (uint)ea.DebugEvent.Exception.ExceptionRecord.ExceptionAddress.ToInt32();
					//addr -= 4;
					//IntPtr written;
					//byte[] buf = new byte[12];
					//ReadProcessMemory(debugEngine_.ProcessInfo.hProcess, (IntPtr)addr, buf, 12, out written);
					//Console.WriteLine("memory dump:");
					//for (int i = 0; i < 12; ++i)
					//{
					//    Console.WriteLine("[0x" + addr.ToString("X") + "] " + buf[i].ToString("X"));
					//    ++addr;
					//}
					break;

				case DbgEventCode.ProcessExited:
					CodeCoverageFinished(new CoverageFinishedEventArgs(activeRecording_));
					break;

				case DbgEventCode.LoadDLL:
					if (!addRuntimeModule(ea.DebugEvent.LoadDll.hFile, ea.DebugEvent.LoadDll.lpBaseOfDll) && verbose_)
					{
						string modulePath = getRuntimePath(ea.DebugEvent.LoadDll.hFile, ea.DebugEvent.LoadDll.lpBaseOfDll);
						NonRelevantModuleLoaded(new ModuleLoadedEventArgs(modulePath, (uint)ea.DebugEvent.LoadDll.lpBaseOfDll));
					}
					break;

				case DbgEventCode.ProcessCreated:
					// search for matching module of the main process
					addRuntimeModule(ea.DebugEvent.CreateProcessInfo.hFile, ea.DebugEvent.CreateProcessInfo.lpBaseOfImage);
					//// process executable is always the first module
					//exeName_ = Path.GetFileName(activeRecording_.Modules[0].Path);
					exeName_ = getRuntimePath(ea.DebugEvent.CreateProcessInfo.hFile, ea.DebugEvent.CreateProcessInfo.lpBaseOfImage);
					break;

				default:
					break;
			}
		}

		/// <summary>
		/// add module to internal list of modules used for recording and set break points if
		/// modules is in our global list of known modules
		/// module path is resolved via file handle and IBA of the module
		/// </summary>
		/// <param name="hFile"></param>
		/// <param name="imageBase"></param>
		private bool addRuntimeModule(IntPtr hFile, IntPtr imageBase)
		{
			string modulePath = "";
			try 
			{
				modulePath = getRuntimePath(hFile, imageBase);
			}
			catch (Exception)
			{
			}
			PEModule module;
			if (moduleNamesMap_.TryGetValue(modulePath, out module))
			{
				// we found a matching module so set breakpoints (and possibly rebase)
				module.ImageBase = (uint)imageBase;
				// add loaded module to active recording
				activeRecording_.addModule(module);
				// raise event
				ModuleLoaded(new ModuleLoadedEventArgs(modulePath, (uint)imageBase.ToInt32()));
				return true;
			}
			return false;
		}

		/// <summary>
		/// use file mapping "trick" to get the module name from the module file handle
		/// and IBA to which the module has been mapped
		/// </summary>
		/// <param name="hFile"></param>
		/// <param name="imageBase"></param>
		/// <returns></returns>
		public string getRuntimePath(IntPtr hFile, IntPtr imageBase)
		{
			// handle will be closed by debugging engine!
			SafeFileHandle dllHandle = new SafeFileHandle(hFile, false);
			using (SafeFileHandle hMappedFile = CreateFileMapping(dllHandle, 0, PAGE_READONLY, 0, 0, null))
			{
				IntPtr hView = MapViewOfFile(hMappedFile, FILE_MAP_READ, 0, 0, IntPtr.Zero);
				if (hView == IntPtr.Zero) Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
				StringBuilder sbFileName = new StringBuilder(MAX_PATH + 1);
				// now try to get the mapped filename using the debuggees process handle
				if (GetMappedFileName(debugEngine_.ProcessInfo.hProcess, imageBase, sbFileName, MAX_PATH) == 0)
					Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
				// the path to the loaded module in junction form
				string volumePath = sbFileName.ToString();
				string dosPath = volumePath;

				foreach (string drivePath in Environment.GetLogicalDrives())
				{
					string drive = drivePath.Substring(0, 2);
					StringBuilder lpTargetPath = new StringBuilder(MAX_PATH);

					// translate drive name to corresponding junction name
					if (QueryDosDevice(drive, lpTargetPath, MAX_PATH) == 0)
						Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
					string targetPath = lpTargetPath.ToString();
					if (dosPath.StartsWith(targetPath))
					{
						dosPath = dosPath.Replace(targetPath, drive);
						break;
					}
				}

				UnmapViewOfFile(hView);
				return dosPath;
			}
		}

		/// <summary>
		/// start new code covergage session by attaching to an existing process
		/// </summary>
		private void attach()
		{
			isRunning_ = true;
			using (debugEngine_ = new ManagedCCDebugEngine())
			{
				debugEngine_.OnBreakPoint += new DbgExceptionEventHandler(debugEngine_OnBreakPoint);
				debugEngine_.OnDbgEvent += new DbgExceptionEventHandler(debugEngine_OnDbgEvent);
				debugEngine_.OnDbgAttached += new DbgAttachedEventHandler(debugEngine_OnDbgAttached);
				bool retVal = debugEngine_.debugActiveProcess((uint)dbgProcess_.Id);
				retVal = debugEngine_.run();	
			}
			isRunning_ = false;
		}

		/// <summary>
		/// start new code coverage session by spawning a new process
		/// </summary>
		private void spawn()
		{
			isRunning_ = true;
			using (debugEngine_ = new ManagedCCDebugEngine())
			{
				debugEngine_.OnBreakPoint += new DbgExceptionEventHandler(debugEngine_OnBreakPoint);
				debugEngine_.OnDbgEvent += new DbgExceptionEventHandler(debugEngine_OnDbgEvent);
				debugEngine_.OnDbgAttached += new DbgAttachedEventHandler(debugEngine_OnDbgAttached);
				debugEngine_.debug(commandLine_, "");
				debugEngine_.run();
			}
			isRunning_ = false;
		}

		void debugEngine_OnDbgAttached()
		{
			// we have to set our breakpoints here, otherwise we might have a breakpoint hit before we
			// completely attached to the process!
			foreach (PEModule module in activeRecording_.Modules)
			{
				foreach (uint bp in module.BreakPointAddresses)
					debugEngine_.setBreakPoint((IntPtr)bp, !restoreBreakPoints_);
			}			

			// hide debugger
			AntiAntiDebugger dbgStealth = new AntiAntiDebugger(debugEngine_.ProcessInfo.hProcess, debugEngine_.ProcessInfo.hThread);
			dbgStealth.hideDebugger();
			DebuggerAttached(new DebuggerAttachedEventArgs(debugEngine_.ProcessInfo.dwProcessId, exeName_));
		}

		/// <summary>
		/// start new code coverage session with given ThreadStart
		/// </summary>
		/// <param name="ts"></param>
		private void startCodeCoverage(ThreadStart ts)
		{
			if (dbgthread_ != null)
			{
				dbgthread_.Abort();
				dbgthread_.Join();
			}
			// search for active recording (GUI has to ensure there is only one, otherwise the first is taken)
			activeRecording_ = null;
			foreach (Recording rec in recordings_)
				if (rec.RecordingType == RecordingTypes.Active)
				{
					activeRecording_ = rec;
					break;
				}
			// make sure we start from a clean recording
			activeRecording_.reset();

			// we need to know which module belongs to which path so we can look it up while debugging
			moduleNamesMap_.Clear();
			foreach (PEModule module in modules_) moduleNamesMap_.Add(module.Path, module);
			
			dbgthread_ = new Thread(ts);
			dbgthread_.Start();
		}

		/// <summary>
		/// start code coverage by attaching to a running process
		/// </summary>
		/// <param name="process"></param>
		public void start(Process process)
		{
			dbgProcess_ = process;
			startCodeCoverage(new ThreadStart(attach));
		}

		/// <summary>
		/// start new code coverage by spawning a new process
		/// </summary>
		/// <param name="commandLine"></param>
		public void start(string commandLine)
		{
			commandLine_ = commandLine;
			startCodeCoverage(new ThreadStart(spawn));
		}

		/// <summary>
		/// stop code coverage
		/// </summary>
		/// <param name="killOnExit"></param>
		public bool stop(bool killOnExit)
		{
			return debugEngine_.detach(killOnExit);
		}

		/// <summary>
		/// iterate over all recordings and merge/diff hit sets by module depending on the recording types
		/// </summary>
		public void generateFinalSet()
		{
			finalSet_.Clear();
			// every module path maps to one coverage module
			Dictionary<string, CoverageModule> tmpSet = new Dictionary<string, CoverageModule>();

			// merge all hits first
			foreach (Recording recording in recordings_)
			{
				foreach (PEModule peModule in recording.Modules)
				{
					// merge all modules into tmpSet
					if (recording.RecordingType == RecordingTypes.Active)
					{
						string name = Path.GetFileName(peModule.Path);
						if (!tmpSet.ContainsKey(peModule.Path)) tmpSet.Add(peModule.Path, new CoverageModule(name));
						tmpSet[peModule.Path].Hits.merge(peModule.Hits);
					}
				}
			}

			// then perform filtering
			foreach (Recording recording in recordings_)
			{
				foreach (PEModule peModule in recording.Modules)
				{
					// only filter if module exists (i.e. already has some hits)
					if (recording.RecordingType == RecordingTypes.Filter && tmpSet.ContainsKey(peModule.Path))
						tmpSet[peModule.Path].Hits.filter(peModule.Hits);
				}
			}

			finalSet_.AddRange(tmpSet.Values);
		}

		public ReadOnlyCollection<CoverageModule> FinalSet
		{
			get { return roFinalSet_; }
		}

		/// <summary>
		/// we only maintain one global modules list, so modules are added to our code coverage class
		/// </summary>
		public List<PEModule> Modules
		{
			get { return modules_; }
		}

		/// <summary>
		/// get list of available recordings in this session
		/// </summary>
		public List<Recording> Recordings
		{
			get { return recordings_; }
		}

		/// <summary>
		/// controls whether breakpoints are automatically restored after beeing hit
		/// </summary>
		public bool RestoreBreakPoints
		{
			get { return restoreBreakPoints_; }
			set { restoreBreakPoints_ = value; }
		}

		public bool ContinueAfterException
		{
			get { return continueAfterException_; }
			set { continueAfterException_ = value; }
		}

		public bool Verbose
		{
			get { return verbose_; }
			set { verbose_ = value; }
		}

		public bool IsRunning
		{
			get { return isRunning_; }
		}

		#region Events

		/// <summary>
		/// export debug exception events from debugger class
		/// </summary>
		//public event DbgExceptionEventHandler CodeCoverageDbgEvent
		//{
		//    // forward event subscription directly to debugging engine
		//    add	{ debugEngine_.OnDbgEvent += value;	}
		//    remove { debugEngine_.OnDbgEvent -= value; }
		//}

		// standard info events
		public event CoverageFinishedDelegate CodeCoverageFinished;
		public event ModuleLoadedDelegate ModuleLoaded;
		public event DebuggerAttachedDelegate DebuggerAttached;
		public event DebuggerAttachedDelegate DebuggerDetached;
		public event ModuleLoadedDelegate NonRelevantModuleLoaded;

		// verbose info events

		#endregion // Events
	}
}
