using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using NCoverage.CodeCoverage;

namespace NCoverage
{
	public partial class AddRecordingForm : Form
	{
		private Recording newRecording_;

		public AddRecordingForm()
		{
			InitializeComponent();
		}

		private void btnOk_Click(object sender, EventArgs e)
		{
			if (txbName.Text.Length > 0)
			{
				RecordingTypes recType;
				if (rdbFilter.Checked) recType = RecordingTypes.Filter;
				else if (rdbRecording.Checked) recType = RecordingTypes.Active;
				else recType = RecordingTypes.Inactive;
				newRecording_ = new Recording(txbName.Text, recType);
				this.Close();
			}
		}

		public Recording NewRecording
		{
			get { return newRecording_; }
		}
	}
}