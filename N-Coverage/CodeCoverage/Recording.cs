using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;

namespace NCoverage.CodeCoverage
{
	public enum RecordingTypes { Active=0, Inactive=1, Filter=2 };

	/// <summary>
	/// represents a single recording session
	/// </summary>
	public class Recording
	{
		//private ModulesCollection modules_;
		private string recordingName_;
		private RecordingTypes recordingType_;
		// keep track of all breakpoints so we can associate each hit with the respective module
		private Dictionary<uint, PEModule> addressMap_;
		private List<PEModule> modules_;
		private ReadOnlyCollection<PEModule> roModules_;

		public Recording()
		{
			recordingType_ = RecordingTypes.Active;
			addressMap_ = new Dictionary<uint, PEModule>();
			modules_ = new List<PEModule>();
			roModules_ = new ReadOnlyCollection<PEModule>(modules_);
		}

		public Recording(string name, RecordingTypes recordingType) 
			: this()
		{
			recordingType_ = recordingType;
			recordingName_ = name;
		}

		/// <summary>
		/// add hit to module whose address space contains the given address
		/// </summary>
		/// <param name="address">VA of the breakpoint hit</param>
		/// <returns></returns>
		public bool addHit(uint address)
		{
			// find correct module
			PEModule module = addressMap_[address];
			if (module != null)
			{
				module.Hits.addHit(address);
				return true;
			}
			else return false;
		}

		/// <summary>
		/// reset list of modules / known addresses
		/// </summary>
		public void reset()
		{
			modules_.Clear();
			addressMap_.Clear();
		}

		/// <summary>
		/// add new module as well as all breakpoint addresses of the given module to internal list
		/// </summary>
		/// <param name="module"></param>
		public void addModule(PEModule module)
		{
			// we need to add a copy of the given module so we are able to count hits for this recording only
			PEModule moduleCopy = new PEModule(module);
			// add all breakpoint addresses (VA)
			foreach (uint bp in module.BreakPointAddresses) addressMap_.Add(bp, moduleCopy);
			modules_.Add(moduleCopy);
		}

		/// <summary>
		/// return read only list of modules used in this recording
		/// </summary>
		public ReadOnlyCollection<PEModule> Modules
		{
			get { return roModules_; }
		}

		public string Name
		{
			get { return recordingName_; }
			set { recordingName_ = value; }
		}

		public RecordingTypes RecordingType
		{
			get { return recordingType_; }
			set { recordingType_ = value; }
		}
	}
}
