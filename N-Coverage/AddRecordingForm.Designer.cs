namespace NCoverage
{
	partial class AddRecordingForm
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.label1 = new System.Windows.Forms.Label();
			this.txbName = new System.Windows.Forms.TextBox();
			this.rdbRecording = new System.Windows.Forms.RadioButton();
			this.btnCancel = new System.Windows.Forms.Button();
			this.btnOk = new System.Windows.Forms.Button();
			this.rdbFilter = new System.Windows.Forms.RadioButton();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.rdbInactive = new System.Windows.Forms.RadioButton();
			this.groupBox1.SuspendLayout();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(9, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(149, 13);
			this.label1.TabIndex = 0;
			this.label1.Text = "Enter name for new recording:";
			// 
			// txbName
			// 
			this.txbName.Location = new System.Drawing.Point(12, 25);
			this.txbName.Name = "txbName";
			this.txbName.Size = new System.Drawing.Size(267, 20);
			this.txbName.TabIndex = 1;
			// 
			// rdbRecording
			// 
			this.rdbRecording.AutoSize = true;
			this.rdbRecording.Checked = true;
			this.rdbRecording.Location = new System.Drawing.Point(6, 19);
			this.rdbRecording.Name = "rdbRecording";
			this.rdbRecording.Size = new System.Drawing.Size(143, 17);
			this.rdbRecording.TabIndex = 2;
			this.rdbRecording.TabStop = true;
			this.rdbRecording.Text = "Active (use for recording)";
			this.rdbRecording.UseVisualStyleBackColor = true;
			// 
			// btnCancel
			// 
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Location = new System.Drawing.Point(204, 121);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.TabIndex = 3;
			this.btnCancel.Text = "Cancel";
			this.btnCancel.UseVisualStyleBackColor = true;
			// 
			// btnOk
			// 
			this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.btnOk.Location = new System.Drawing.Point(123, 121);
			this.btnOk.Name = "btnOk";
			this.btnOk.Size = new System.Drawing.Size(75, 23);
			this.btnOk.TabIndex = 4;
			this.btnOk.Text = "Ok";
			this.btnOk.UseVisualStyleBackColor = true;
			this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
			// 
			// rdbFilter
			// 
			this.rdbFilter.AutoSize = true;
			this.rdbFilter.Location = new System.Drawing.Point(157, 18);
			this.rdbFilter.Name = "rdbFilter";
			this.rdbFilter.Size = new System.Drawing.Size(80, 17);
			this.rdbFilter.TabIndex = 6;
			this.rdbFilter.Text = "Use as filter";
			this.rdbFilter.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.rdbInactive);
			this.groupBox1.Controls.Add(this.rdbRecording);
			this.groupBox1.Controls.Add(this.rdbFilter);
			this.groupBox1.Location = new System.Drawing.Point(12, 51);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(267, 64);
			this.groupBox1.TabIndex = 7;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Recording types";
			// 
			// rdbInactive
			// 
			this.rdbInactive.AutoSize = true;
			this.rdbInactive.Location = new System.Drawing.Point(6, 41);
			this.rdbInactive.Name = "rdbInactive";
			this.rdbInactive.Size = new System.Drawing.Size(221, 17);
			this.rdbInactive.TabIndex = 7;
			this.rdbInactive.TabStop = true;
			this.rdbInactive.Text = "Inactive (don\'t use for recording / filtering)";
			this.rdbInactive.UseVisualStyleBackColor = true;
			// 
			// AddRecordingForm
			// 
			this.AcceptButton = this.btnOk;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(284, 149);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.btnOk);
			this.Controls.Add(this.btnCancel);
			this.Controls.Add(this.txbName);
			this.Controls.Add(this.label1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.Name = "AddRecordingForm";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "Create new recording";
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.TextBox txbName;
		private System.Windows.Forms.RadioButton rdbRecording;
		private System.Windows.Forms.Button btnCancel;
		private System.Windows.Forms.Button btnOk;
		private System.Windows.Forms.RadioButton rdbFilter;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.RadioButton rdbInactive;
	}
}