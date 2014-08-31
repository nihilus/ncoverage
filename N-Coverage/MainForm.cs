using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ManagedDbgWrapper;
using System.Threading;
using System.Runtime.InteropServices;
using NCoverage.CodeCoverage;
using System.Diagnostics;
using System.Collections;
using System.Collections.ObjectModel;

namespace NCoverage
{
	public partial class frmMain : Form
	{
		private delegate void LogDelegate(string s, Color c);
		private LogDelegate logDelegate_;
		private NCodeCoverage codeCoverage_;

		[DllImport("Kernel32.dll", EntryPoint = "OpenProcess", SetLastError = true)]
		private static extern IntPtr OpenProcess(int dwDesiredAccess, bool bInheritHandle, int dwProcessId);
		[DllImport("Kernel32.dll", EntryPoint = "CloseHandle", SetLastError = true)]
		private static extern void CloseHandle(IntPtr hObject);

		public frmMain()
		{
			InitializeComponent();
			codeCoverage_ = new NCodeCoverage();
			codeCoverage_.CodeCoverageFinished += new CoverageFinishedDelegate(codeCoverage_CodeCoverageFinished);
			//codeCoverage_.CodeCoverageDbgEvent += new DbgExceptionEventHandler(codeCoverage_CodeCoverageDbgEvent);
			codeCoverage_.ModuleLoaded += new ModuleLoadedDelegate(codeCoverage_ModuleLoaded);
			codeCoverage_.DebuggerAttached += new DebuggerAttachedDelegate(codeCoverage_DebuggerAttched);
			codeCoverage_.NonRelevantModuleLoaded += new ModuleLoadedDelegate(codeCoverage_NonRelevantModuleLoaded);
		}

		private void frmMain_Load(object sender, EventArgs e)
		{
			log("N-Coverage v1.0 ALPHA loaded");
			logDelegate_ = new LogDelegate(doLog);
			lsvProcesses.ListViewItemSorter = new ProcessSorter();
			refreshProcessList();
		}

		#region Code coverage events and logging

		private void codeCoverage_CodeCoverageDbgEvent(DbgCallbackEventArgs e)
		{
			switch (e.DebugEvent.dwDebugEventCode)
			{
				case DbgEventCode.LoadDLL:
					try
					{
						log("Loaded DLL from: "
							+ codeCoverage_.getRuntimePath(e.DebugEvent.LoadDll.hFile, e.DebugEvent.LoadDll.lpBaseOfDll)
							+ " at 0x" + e.DebugEvent.LoadDll.lpBaseOfDll.ToString("X"));
					}
					catch (Exception)
					{
						log("Warning: unable to resolve path of loaded DLL at 0x" 
							+ e.DebugEvent.LoadDll.lpBaseOfDll.ToString("X"));
					}
					break;

				case DbgEventCode.ProcessExited:
					log("Debuggee exited");
					break;

				case DbgEventCode.Exception:
					switch (e.DebugEvent.Exception.ExceptionRecord.ExceptionCode)
					{
						case DbgExceptionCode.AccessViolation:
							log("access violation debug event!");
							break;

						case DbgExceptionCode.BreakPoint:
							log("breakpoint debug event!");
							break;

						default:
							log("other exception!");
							break;
					}
					break;

				case DbgEventCode.ProcessCreated:
					log("Created proces at 0x" + e.DebugEvent.CreateProcessInfo.lpBaseOfImage.ToString("X"));
					break;

				default:
					log("other debug event!");
					break;
			}
		}

		private delegate void ListViewUpdateDelegate(Recording recording);
		
		/// <summary>
		/// udpate coverage listview with data from recording
		/// </summary>
		/// <param name="recording"></param>
		private void updateListView(Recording recording)
		{
			lsvCoverage.Items.Clear();
			foreach (PEModule module in recording.Modules)
			{
				ListViewItem item = new ListViewItem(module.Path);
				item.SubItems.Add(module.Hits.Count.ToString());
				int percent = (int)((float)module.Hits.Count / (float)module.FunctionCount * 100.0f);
				item.SubItems.Add(percent.ToString() + "%");
				lsvCoverage.Items.Add(item);
			}
		}

		private void lsvRecordings_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (lsvRecordings.SelectedIndices.Count > 0)
			{
				lsvCoverage.Items.Clear();
				int index = lsvRecordings.SelectedIndices[0];
				foreach (PEModule module in codeCoverage_.Recordings[index].Modules)
				{
					ListViewItem item = new ListViewItem(module.Path);
					item.SubItems.Add(module.Hits.Count.ToString());
					int percent = (int)((float)module.Hits.Count / (float)module.FunctionCount * 100.0f);
					item.SubItems.Add(percent.ToString() + "%");
					lsvCoverage.Items.Add(item);
				}
			}
		}		

		/// <summary>
		/// recording session finished
		/// </summary>
		/// <param name="e"></param>
		private void codeCoverage_CodeCoverageFinished(CoverageFinishedEventArgs e)
		{
			log("Finishing code coverage for recording '" + e.FinishedRecording.Name + "'...", false);
			btnAttachStart.ImageIndex = btnSpawnStart.ImageIndex = 0;
			// since we have been called from a background thread we cant modify controls directly
			lsvRecordings.Invoke(new ListViewUpdateDelegate(updateListView), e.FinishedRecording);
			log("done.\n");
		}

		private void codeCoverage_ModuleLoaded(ModuleLoadedEventArgs e)
		{
			log("Loaded (code coverage) module from " + e.ModuleName + " at 0x" + e.ImageBase.ToString("X"));
		}

		void codeCoverage_DebuggerAttched(DebuggerAttachedEventArgs e)
		{
			log("Debugger attached to process " + e.ExeName + " (" + e.ProcessID + ")");
		}

		void codeCoverage_NonRelevantModuleLoaded(ModuleLoadedEventArgs e)
		{
			log("Loaded (ordinary) module from " + e.ModuleName + " at " + e.ImageBase.ToString("X"));
		}

		private void doLog(string s, Color c)
		{
			rtbLog.Text += s;
			rtbLog.SelectionStart = rtbLog.Text.Length;
			rtbLog.ScrollToCaret();
		}

		/// <summary>
		/// write log string to rich text box; can be called from any thread
		/// </summary>
		/// <param name="s"></param>
		private void log(string s, Color c)
		{
			if (rtbLog.InvokeRequired) rtbLog.Invoke(logDelegate_, s, c);
			else doLog(s, c);
		}

		private void log(string s, bool crlf)
		{
			if (crlf) log(s + "\n", Color.Black);
			else log(s, Color.Black);
		}

		private void log(string s)
		{
			log(s, true);
		}

		#endregion // Code coverage events and logging

		private void btnAddModule_Click(object sender, EventArgs e)
		{
			DialogResult result = openFileDialog.ShowDialog();
			try
			{
				if (result == DialogResult.OK)
				{
					// add all modules to modules list
					PEModule exeModule = null;
					for (int i=0; i<openFileDialog.FileNames.Length; ++i)
					{
						PEModule m = new PEModule(openFileDialog.FileNames[i]);
						codeCoverage_.Modules.Add(m);
						ListViewItem item = new ListViewItem(m.Path);
						item.SubItems.Add(m.FunctionCount.ToString());
						item.SubItems.Add(m.BasicBlockCount.ToString());
						item.SubItems.Add(m.ImageBase.ToString("X"));
						lsvModules.Items.Add(item);
						log("Added module " + m.Path + " with " + m.FunctionCount + " functions to modules list");
						if (exeModule == null && m.Path.EndsWith(".exe")) exeModule = m;
					}
					if (exeModule != null) txbCmdLine.Text = exeModule.Path;
				}
			}
			catch (Exception exception)
			{
				MessageBox.Show(exception.Message);
			}
		}

		private void btnRemoveModule_Click(object sender, EventArgs e)
		{
			if (lsvModules.SelectedIndices.Count > 0)
			{
				int index = lsvModules.SelectedIndices[0];
				PEModule m = codeCoverage_.Modules[index];
				log("Removed module " + m.Path + " from modules list");
				codeCoverage_.Modules.RemoveAt(index);
				lsvModules.Items.RemoveAt(index);
			}
		}

		/// <summary>
		/// add new recording to treeview / code coverage
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAddRecording_Click(object sender, EventArgs e)
		{
			using(AddRecordingForm addRecForm = new AddRecordingForm())
			{
				addRecForm.ShowDialog();
				Recording recording = addRecForm.NewRecording;
				if (recording != null)
				{
					// if we already got a recording with the given name we have to remove it first
					for (int i = 0; i < codeCoverage_.Recordings.Count; ++i)
					{
						if (codeCoverage_.Recordings[i].Name == recording.Name)
						{
							codeCoverage_.Recordings.RemoveAt(i);
							break;
						}
					}
					ListViewItem item = new ListViewItem(recording.Name);
					item.ImageIndex = (int)recording.RecordingType;
					lsvRecordings.Items.Add(item);
					codeCoverage_.Recordings.Add(recording);
				}
			}
		}

		/// <summary>
		/// remove recording from treeview / code coverage
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnRemoveRecording_Click(object sender, EventArgs e)
		{
			if (lsvRecordings.SelectedItems.Count > 0)
			{
				int index = lsvRecordings.SelectedIndices[0];
				lsvRecordings.Items.RemoveAt(index);
				codeCoverage_.Recordings.RemoveAt(index);
			}
		}

		/// <summary>
		/// check if recordings types are ready for starting a new recording
		/// i.e. there has to be exactly one active recording
		/// </summary>
		/// <returns></returns>
		private bool checkRecordingsList()
		{
			int count = 0;
			foreach (Recording rec in codeCoverage_.Recordings) 
				if (rec.RecordingType == RecordingTypes.Active) ++count;

			if (count != 1) 
			{
				MessageBox.Show("Unable to start recording: Make sure there is exactly one active recording!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
				return false;
			}
			else return true;
		}

		/// <summary>
		/// start code coverage by attaching to the selected process
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnAttachStart_Click(object sender, EventArgs e)
		{
			if (codeCoverage_.IsRunning)
			{
				codeCoverage_.stop(false);
				btnAttachStart.ImageIndex = 0;
				log("Debugger detached from process");
			}
			else
			{
				ListViewItem processItem = lsvProcesses.SelectedItems[0];
				if (processItem != null)
				{
					if (checkRecordingsList())
					{
						Process p = (Process)processItem.Tag;
						log("Attaching debugger to process...");
						btnAttachStart.ImageIndex = 1;
						codeCoverage_.start(p);
					}
				}
				else log("Can't attach: select process!");
			}
		}

		/// <summary>
		/// start code coverage by spawning a new process
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSpawnStart_Click(object sender, EventArgs e)
		{
			if (codeCoverage_.IsRunning)
			{
				codeCoverage_.stop(false);
				btnSpawnStart.ImageIndex = 0;
				log("Debugger detached from process");
			}
			else
			{
				if (txbCmdLine.Text == "")
				{
					log("Invalid command line. Debugger has not been started!");
					return;
				}
				if (checkRecordingsList())
				{
					log("Starting debugger...");
					btnSpawnStart.ImageIndex = 1;
					codeCoverage_.start(txbCmdLine.Text);
				}
			}
		}

		private void btnSelectExe_Click(object sender, EventArgs e)
		{
			if (openExedialog.ShowDialog() == DialogResult.OK) txbCmdLine.Text = openExedialog.FileName;
		}
		
		private void lsvRecordings_MouseClick(object sender, MouseEventArgs e)
		{
			if (e.Button == MouseButtons.Right && lsvRecordings.SelectedIndices.Count > 0)
				cmsRecording.Show((sender as Control), e.X, e.Y);
		}

		/// <summary>
		/// update recording in treeview / code coverage
		/// </summary>
		/// <param name="newRecType"></param>
		private void updateRecording(RecordingTypes newRecType)
		{
			if (lsvRecordings.SelectedIndices.Count > 0)
			{
				ListViewItem selectedItem = lsvRecordings.SelectedItems[0];
				selectedItem.ImageIndex = (int)newRecType;
				codeCoverage_.Recordings[selectedItem.Index].RecordingType = newRecType;
			}
		}

		private void activeToolStripMenuItem_Click(object sender, EventArgs e)
		{
			updateRecording(RecordingTypes.Active);
		}

		private void inactiveToolStripMenuItem_Click(object sender, EventArgs e)
		{
			updateRecording(RecordingTypes.Inactive);
		}

		private void filterToolStripMenuItem_Click(object sender, EventArgs e)
		{
			updateRecording(RecordingTypes.Filter);
		}

		private void chkRestoreBPs_CheckedChanged(object sender, EventArgs e)
		{
			codeCoverage_.RestoreBreakPoints = chkRestoreBPs.Checked;
		}

		private void btnColor_Click(object sender, EventArgs e)
		{
			if (colorDialog.ShowDialog() == DialogResult.OK) picColor.BackColor = colorDialog.Color;
		}

		#region Process refresh / sorting stuff

		private class ProcessSorter : IComparer
		{
			private int col_;
			private bool descending_;

			public ProcessSorter()
			{
				col_ = 0;
				descending_ = false;
			}

			public ProcessSorter(int column, bool descending)
			{
				col_ = column;
				descending_ = descending;
			}

			public int Compare(object x, object y)
			{
				int factor = -1;
				if (!descending_) factor = 1;

				string xStr = ((ListViewItem)x).SubItems[col_].Text;
				string yStr = ((ListViewItem)y).SubItems[col_].Text;

				// column 0 is our PID
				if (col_ == 0)
				{
					int xVal = Int32.Parse(xStr);
					int yVal = Int32.Parse(yStr);
					return factor * xVal.CompareTo(yVal);
				}
				else return factor * String.Compare(xStr, yStr);
			}
		};

		private void btnRefreshList_Click(object sender, EventArgs e)
		{
			refreshProcessList();
		}

		//private Process[] oldProcesses_;
		private void refreshProcessList()
		{
			Process[] tmpProcesses = System.Diagnostics.Process.GetProcesses();
			List<Process> processes = new List<Process>();
			if (chkOnlyAccessible.Checked)
			{
				foreach (Process process in tmpProcesses)
				{
					IntPtr hProcess = OpenProcess(0x001f0fff, false, process.Id);
					if (hProcess != IntPtr.Zero)
					{
						processes.Add(process);
						CloseHandle(hProcess);
					}
				}
			}
			else processes.AddRange(tmpProcesses);

			//// check if we need to refresh the process list
			//bool needsRefrsh = true;
			//if (oldProcesses_ == null) oldProcesses_ = processes;
			//else if (processes.Length == oldProcesses_.Length)
			//{
			//    int i;
			//    for (i = 0; i < processes.Length; ++i)
			//        if (processes[i].ProcessName != oldProcesses_[i].ProcessName) break;
			//    if (i == processes.Length) needsRefrsh = false;
			//}
			//if (!needsRefrsh) return;

			// process list changed so we need to update; try to keep the previously selected item
			int selectedIndex = 0;
			if (lsvProcesses.SelectedIndices.Count > 0) selectedIndex = lsvProcesses.SelectedIndices[0];
			lsvProcesses.BeginUpdate();
			lsvProcesses.Items.Clear();
			foreach (Process proc in processes)
			{
				ListViewItem item = new ListViewItem(proc.Id.ToString());
				item.SubItems.Add(proc.ProcessName);
				item.Tag = proc;
				lsvProcesses.Items.Add(item);
			}
			// select old item
			if (selectedIndex < lsvProcesses.Items.Count)
			{
				lsvProcesses.Items[selectedIndex].Selected = true;
				lsvProcesses.Items[selectedIndex].EnsureVisible();
			}
			lsvProcesses.EndUpdate();
			//oldProcesses_ = processes;
		}

		private void lsvProcesses_ColumnClick(object sender, ColumnClickEventArgs e)
		{
			if (lsvProcesses.Sorting == SortOrder.Ascending) lsvProcesses.Sorting = SortOrder.Descending;
			else lsvProcesses.Sorting = SortOrder.Ascending;
			bool descending = (lsvProcesses.Sorting == SortOrder.Descending);
			lsvProcesses.ListViewItemSorter = new ProcessSorter(e.Column, descending);
		}

		#endregion // Process refresh / sorting stuff

		private void btnGenerate_Click(object sender, EventArgs e)
		{
			lsvFinalSet.Items.Clear();
			codeCoverage_.generateFinalSet();
			int totalCount = 0;
			foreach (CoverageModule module in codeCoverage_.FinalSet)
			{
				ListViewItem item = new ListViewItem(module.Name);
				item.SubItems.Add(module.Hits.Count.ToString());
				lsvFinalSet.Items.Add(item);
				totalCount += module.Hits.Count;
			}
			log("Successfully generated the final set totalling to " + totalCount + " hits");
		}

		private void rdbAppHandle_CheckedChanged(object sender, EventArgs e)
		{
			codeCoverage_.ContinueAfterException = rdbContinueExec.Checked;
		}

		/// <summary>
		/// export hits so it can be imported by the IDA plugin
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnExport_Click(object sender, EventArgs e)
		{
			try
			{
				if (lsvFinalSet.SelectedIndices.Count > 0)
				{
					int index = lsvFinalSet.SelectedIndices[0];
					DialogResult result = saveFileDialog.ShowDialog();
					if (result == DialogResult.OK)
					{
						NCoverageExportOptions options = new NCoverageExportOptions(picColor.BackColor);
						options.UseColor = chkColor.Checked;
						codeCoverage_.FinalSet[index].exportHitsToFile(saveFileDialog.FileName, options);
						log("Successfully exported " + codeCoverage_.FinalSet[index].Hits.Count + " hits to " + saveFileDialog.FileName);
					}
				}
				else log("Nothing exported. Select module!");
			}
			catch (System.Exception exception)
			{
				MessageBox.Show(exception.Message);	
			}
		}

		private void chkVerbose_CheckedChanged(object sender, EventArgs e)
		{
			codeCoverage_.Verbose = chkVerbose.Checked;
		}

		private void exitToolStripMenuItem_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
		{
			MessageBox.Show("N-coverage v1.0 ALPHA, (C) 2007-2008, Jan Newger", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
		}
	}
}