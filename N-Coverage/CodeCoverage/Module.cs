using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Collections.ObjectModel;
using System.Drawing;

namespace NCoverage.CodeCoverage
{
	/// <summary>
	/// every module needs at least a list of hits and a name
	/// </summary>
	public interface IModule
	{
		HitSet Hits
		{
			get;
		}

		string Name
		{
			get;
			set;
		}
	}

	/// <summary>
	/// module class which is based on an actual PE file
	/// </summary>
	public class PEModule : IModule
	{
		private string modulePath_;
		private string moduleName_;
		private uint imageBase_;
		
		HitSet hitSet_;
		// keep list of all breakpoints (VA) so we can silently implement rebasing...
		private List<uint> bpAddresses_;
		// ...and export the addresses as a read only collection
		private ReadOnlyCollection<uint> roBPAddresses_;

		public PEModule(string modulePath)
		{
			bpAddresses_ = new List<uint>();
			roBPAddresses_ = new ReadOnlyCollection<uint>(bpAddresses_);
			loadModuleFile(modulePath);
			hitSet_ = new HitSet(imageBase_);
		}
		
		/// <summary>
		/// copy constructor
		/// </summary>
		/// <param name="module"></param>
		public PEModule(PEModule module)
		{
			bpAddresses_ = new List<uint>(module.bpAddresses_);
			roBPAddresses_ = new ReadOnlyCollection<uint>(bpAddresses_);
			modulePath_ = module.modulePath_;
			imageBase_ = module.imageBase_;
			hitSet_ = new HitSet(imageBase_);
		}

		/// <summary>
		/// load module name, all RVAs and IBA from file
		/// </summary>
		/// <param name="fileName"></param>
		private void loadModuleFile(string fileName)
		{
			try
			{
				using (StreamReader sr = new StreamReader(fileName))
				{
					modulePath_ = sr.ReadLine();
					imageBase_ = Convert.ToUInt32(sr.ReadLine(), 16);
					string line;
					while ((line = sr.ReadLine()) != null)
					{
						uint addr = Convert.ToUInt32(line, 16);
						bpAddresses_.Add(addr + imageBase_);
						//hitSet_.addHit(addr + imageBase_);
					}
				}
			}
			catch (Exception e)
			{
				throw new ModuleException(e.Message);
			}		
		}

		/// <summary>
		/// get/set image base adress of PE image; possibly rebases the BP addresses
		/// </summary>
		public uint ImageBase
		{
			get { return imageBase_; }
			set 
			{ 
				// possibly rebase breakpoint addresses
				if (imageBase_ != value)
				{
					for (int i = 0; i < bpAddresses_.Count; ++i)
						bpAddresses_[i] = bpAddresses_[i] - imageBase_ + value;
					imageBase_ = value;
				}
			}
		}

		/// <summary>
		/// export break point addresses as VAs so the debugger class
		/// can set all breakpoints (possibly rebased)
		/// </summary>
		public ReadOnlyCollection<uint> BreakPointAddresses
		{
			get { return roBPAddresses_; }
		}

		/// <summary>
		/// full path to the PE file
		/// </summary>
		public string Path
		{
			get { return modulePath_; }
		}

		public int FunctionCount
		{
			get { return bpAddresses_.Count; }
		}

		// atm we can only deal with functions!
		public int BasicBlockCount
		{
			get { return 0; }
		}

		public HitSet Hits
		{
			get { return hitSet_; }
		}

		public string Name
		{
			get { return moduleName_; }
			set { moduleName_ = value; }
		}
	}

	/// <summary>
	/// export options class
	/// </summary>
	public class NCoverageExportOptions
	{
		private Color color_;
		private bool exportMarkers_;
		private bool appendHitCount_;
		private bool blendColors_;
		private bool usecolor_;

		public NCoverageExportOptions()
		{
			color_ = Color.LightGreen;
		}

		public NCoverageExportOptions(Color c)
		{
			color_ = c;
			usecolor_ = true;
		}

		public bool ExportMarkers
		{
			get { return exportMarkers_; }
			set { exportMarkers_ = value; }
		}

		public bool AppendHitCount
		{
			get { return appendHitCount_; }
			set { appendHitCount_ = value; }
		}

		public bool BlendColors
		{
			get { return blendColors_; }
			set { blendColors_ = value; }
		}

		public bool UseColor
		{
			get { return usecolor_; }
			set { usecolor_ = value; }
		}

		public Color HitColor
		{
			get { return color_; }
			set { color_ = value; }
		}
	}

	/// <summary>
	/// module class which is created by the code coverage process
	/// </summary>
	public class CoverageModule : IModule
	{
		private HitSet hits_;
		private string name_;

		public CoverageModule(string name)
		{
			hits_ = new HitSet();
			name_ = name;
		}

		public HitSet Hits
		{
			get { return hits_; }
		}

		public string Name
		{
			get { return name_; }
			set { name_ = value; }
		}

		/// <summary>
		/// export hits to file so it can be imported by the IDA plugin
		/// </summary>
		/// <param name="fileName"></param>
		public void exportHitsToFile(string fileName, NCoverageExportOptions options)
		{
			try
			{
				FileStream fs = new FileStream(fileName, FileMode.Create);
				BinaryWriter bw = new BinaryWriter(fs);
				// write header
				bw.Write(options.UseColor);
				bw.Write(options.ExportMarkers);
				bw.Write(options.BlendColors);
				bw.Write(options.AppendHitCount);
				bw.Write(options.HitColor.R);
				bw.Write(options.HitColor.G);
				bw.Write(options.HitColor.B);

				foreach (Hit hit in hits_)
				{
					bw.Write(hit.Address);
					bw.Write(hit.Count);
				}
				bw.Close();
			}
			catch (Exception e)
			{
				throw new ModuleException(e.Message);
			}
		}
	}

	public class ModuleException : Exception
	{
		public ModuleException()
		{
		}
		public ModuleException(string message)
			: base(message)
		{
		}
		public ModuleException(string message, Exception inner)
			: base(message, inner)
		{
		}
	}
}
