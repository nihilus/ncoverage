namespace NCoverage
{
	partial class frmMain
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
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMain));
			this.menuStrip = new System.Windows.Forms.MenuStrip();
			this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.openProjectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addRecordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripSeparator();
			this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.recordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.addRecordingToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
			this.removeRecordingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
			this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.rtbLog = new System.Windows.Forms.RichTextBox();
			this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
			this.label4 = new System.Windows.Forms.Label();
			this.toolStrip = new System.Windows.Forms.ToolStrip();
			this.toolStripButton1 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSplitButton1 = new System.Windows.Forms.ToolStripSplitButton();
			this.newRecordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.newModuleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripButton4 = new System.Windows.Forms.ToolStripButton();
			this.toolStripButton5 = new System.Windows.Forms.ToolStripButton();
			this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
			this.toolStripSplitButton2 = new System.Windows.Forms.ToolStripSplitButton();
			this.attachToProcessAndStartToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.spwanProcessAndStartRecordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator4 = new System.Windows.Forms.ToolStripSeparator();
			this.tsbRefreshList = new System.Windows.Forms.ToolStripButton();
			this.groupBox4 = new System.Windows.Forms.GroupBox();
			this.lsvRecordings = new System.Windows.Forms.ListView();
			this.columnHeader8 = new System.Windows.Forms.ColumnHeader();
			this.imageList = new System.Windows.Forms.ImageList(this.components);
			this.label2 = new System.Windows.Forms.Label();
			this.btnRemoveModule = new System.Windows.Forms.Button();
			this.btnAddModule = new System.Windows.Forms.Button();
			this.lsvModules = new System.Windows.Forms.ListView();
			this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader7 = new System.Windows.Forms.ColumnHeader();
			this.btnRemoveRecording = new System.Windows.Forms.Button();
			this.btnAddRecording = new System.Windows.Forms.Button();
			this.lsvCoverage = new System.Windows.Forms.ListView();
			this.columnHeader12 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader10 = new System.Windows.Forms.ColumnHeader();
			this.cmsRecording = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.addRecordingToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
			this.removeRecordingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.changeTypeToToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.activeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.inactiveToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.filterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.tabControl1 = new System.Windows.Forms.TabControl();
			this.tabPage1 = new System.Windows.Forms.TabPage();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.btnSelectExe = new System.Windows.Forms.Button();
			this.chkOnlyAccessible = new System.Windows.Forms.CheckBox();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.chkVerbose = new System.Windows.Forms.CheckBox();
			this.chkRestoreBPs = new System.Windows.Forms.CheckBox();
			this.rdbContinueExec = new System.Windows.Forms.RadioButton();
			this.rdbAppHandle = new System.Windows.Forms.RadioButton();
			this.btnRefreshList = new System.Windows.Forms.Button();
			this.btnAttachStart = new System.Windows.Forms.Button();
			this.imageListStartStop = new System.Windows.Forms.ImageList(this.components);
			this.btnSpawnStart = new System.Windows.Forms.Button();
			this.txbCmdLine = new System.Windows.Forms.TextBox();
			this.label1 = new System.Windows.Forms.Label();
			this.lsvProcesses = new System.Windows.Forms.ListView();
			this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
			this.tabPage2 = new System.Windows.Forms.TabPage();
			this.groupBox3 = new System.Windows.Forms.GroupBox();
			this.groupBox5 = new System.Windows.Forms.GroupBox();
			this.checkBox1 = new System.Windows.Forms.CheckBox();
			this.picColor = new System.Windows.Forms.PictureBox();
			this.checkBox4 = new System.Windows.Forms.CheckBox();
			this.checkBox2 = new System.Windows.Forms.CheckBox();
			this.btnColor = new System.Windows.Forms.Button();
			this.chkColor = new System.Windows.Forms.CheckBox();
			this.label3 = new System.Windows.Forms.Label();
			this.lsvFinalSet = new System.Windows.Forms.ListView();
			this.columnHeader9 = new System.Windows.Forms.ColumnHeader();
			this.columnHeader11 = new System.Windows.Forms.ColumnHeader();
			this.btnExport = new System.Windows.Forms.Button();
			this.btnGenerate = new System.Windows.Forms.Button();
			this.colorDialog = new System.Windows.Forms.ColorDialog();
			this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.openExedialog = new System.Windows.Forms.OpenFileDialog();
			this.menuStrip.SuspendLayout();
			this.toolStrip.SuspendLayout();
			this.groupBox4.SuspendLayout();
			this.cmsRecording.SuspendLayout();
			this.tabControl1.SuspendLayout();
			this.tabPage1.SuspendLayout();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.tabPage2.SuspendLayout();
			this.groupBox3.SuspendLayout();
			this.groupBox5.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picColor)).BeginInit();
			this.SuspendLayout();
			// 
			// menuStrip
			// 
			this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.recordingToolStripMenuItem,
            this.helpToolStripMenuItem});
			this.menuStrip.Location = new System.Drawing.Point(0, 0);
			this.menuStrip.Name = "menuStrip";
			this.menuStrip.Size = new System.Drawing.Size(830, 24);
			this.menuStrip.TabIndex = 1;
			this.menuStrip.Text = "menuStrip";
			// 
			// fileToolStripMenuItem
			// 
			this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newProjectToolStripMenuItem,
            this.openProjectToolStripMenuItem,
            this.addRecordingToolStripMenuItem,
            this.addModuleToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
			this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
			this.fileToolStripMenuItem.Size = new System.Drawing.Size(35, 20);
			this.fileToolStripMenuItem.Text = "File";
			// 
			// newProjectToolStripMenuItem
			// 
			this.newProjectToolStripMenuItem.Enabled = false;
			this.newProjectToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newProjectToolStripMenuItem.Image")));
			this.newProjectToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newProjectToolStripMenuItem.Name = "newProjectToolStripMenuItem";
			this.newProjectToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.newProjectToolStripMenuItem.Text = "New Project...";
			// 
			// openProjectToolStripMenuItem
			// 
			this.openProjectToolStripMenuItem.Enabled = false;
			this.openProjectToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("openProjectToolStripMenuItem.Image")));
			this.openProjectToolStripMenuItem.Name = "openProjectToolStripMenuItem";
			this.openProjectToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.openProjectToolStripMenuItem.Text = "Open Project";
			// 
			// addRecordingToolStripMenuItem
			// 
			this.addRecordingToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addRecordingToolStripMenuItem.Image")));
			this.addRecordingToolStripMenuItem.Name = "addRecordingToolStripMenuItem";
			this.addRecordingToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.addRecordingToolStripMenuItem.Text = "Add Recording...";
			this.addRecordingToolStripMenuItem.Click += new System.EventHandler(this.btnAddRecording_Click);
			// 
			// addModuleToolStripMenuItem
			// 
			this.addModuleToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("addModuleToolStripMenuItem.Image")));
			this.addModuleToolStripMenuItem.Name = "addModuleToolStripMenuItem";
			this.addModuleToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.addModuleToolStripMenuItem.Text = "Add Module...";
			// 
			// toolStripMenuItem1
			// 
			this.toolStripMenuItem1.Name = "toolStripMenuItem1";
			this.toolStripMenuItem1.Size = new System.Drawing.Size(164, 6);
			// 
			// exitToolStripMenuItem
			// 
			this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
			this.exitToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.exitToolStripMenuItem.Text = "Exit";
			this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
			// 
			// optionsToolStripMenuItem
			// 
			this.optionsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.settingsToolStripMenuItem});
			this.optionsToolStripMenuItem.Enabled = false;
			this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
			this.optionsToolStripMenuItem.Size = new System.Drawing.Size(56, 20);
			this.optionsToolStripMenuItem.Text = "Options";
			// 
			// settingsToolStripMenuItem
			// 
			this.settingsToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("settingsToolStripMenuItem.Image")));
			this.settingsToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
			this.settingsToolStripMenuItem.Size = new System.Drawing.Size(136, 22);
			this.settingsToolStripMenuItem.Text = "Settings...";
			// 
			// recordingToolStripMenuItem
			// 
			this.recordingToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRecordingToolStripMenuItem2,
            this.removeRecordingToolStripMenuItem1,
            this.toolStripSeparator3});
			this.recordingToolStripMenuItem.Name = "recordingToolStripMenuItem";
			this.recordingToolStripMenuItem.Size = new System.Drawing.Size(67, 20);
			this.recordingToolStripMenuItem.Text = "Recording";
			// 
			// addRecordingToolStripMenuItem2
			// 
			this.addRecordingToolStripMenuItem2.Name = "addRecordingToolStripMenuItem2";
			this.addRecordingToolStripMenuItem2.Size = new System.Drawing.Size(172, 22);
			this.addRecordingToolStripMenuItem2.Text = "Add recording...";
			// 
			// removeRecordingToolStripMenuItem1
			// 
			this.removeRecordingToolStripMenuItem1.Name = "removeRecordingToolStripMenuItem1";
			this.removeRecordingToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
			this.removeRecordingToolStripMenuItem1.Text = "Remove recording";
			// 
			// toolStripSeparator3
			// 
			this.toolStripSeparator3.Name = "toolStripSeparator3";
			this.toolStripSeparator3.Size = new System.Drawing.Size(169, 6);
			// 
			// helpToolStripMenuItem
			// 
			this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
			this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
			this.helpToolStripMenuItem.Size = new System.Drawing.Size(40, 20);
			this.helpToolStripMenuItem.Text = "Help";
			// 
			// aboutToolStripMenuItem
			// 
			this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
			this.aboutToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
			this.aboutToolStripMenuItem.Text = "About";
			this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
			// 
			// rtbLog
			// 
			this.rtbLog.BackColor = System.Drawing.SystemColors.Window;
			this.rtbLog.Dock = System.Windows.Forms.DockStyle.Bottom;
			this.rtbLog.Font = new System.Drawing.Font("Lucida Console", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.rtbLog.ForeColor = System.Drawing.SystemColors.WindowText;
			this.rtbLog.Location = new System.Drawing.Point(0, 532);
			this.rtbLog.Name = "rtbLog";
			this.rtbLog.ReadOnly = true;
			this.rtbLog.Size = new System.Drawing.Size(830, 183);
			this.rtbLog.TabIndex = 4;
			this.rtbLog.Text = "";
			// 
			// openFileDialog
			// 
			this.openFileDialog.Filter = "N-Coverage dumps (*.ndump)|*.ndump|All files (*.*)|*.*";
			this.openFileDialog.Multiselect = true;
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(12, 33);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(96, 13);
			this.label4.TabIndex = 13;
			this.label4.Text = "Available Modules:";
			// 
			// toolStrip
			// 
			this.toolStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripButton1,
            this.toolStripSplitButton1,
            this.toolStripButton4,
            this.toolStripButton5,
            this.toolStripSeparator1,
            this.toolStripSplitButton2,
            this.toolStripSeparator4,
            this.tsbRefreshList});
			this.toolStrip.Location = new System.Drawing.Point(0, 24);
			this.toolStrip.Name = "toolStrip";
			this.toolStrip.Size = new System.Drawing.Size(830, 25);
			this.toolStrip.TabIndex = 17;
			this.toolStrip.Text = "toolStrip1";
			// 
			// toolStripButton1
			// 
			this.toolStripButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton1.Enabled = false;
			this.toolStripButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton1.Image")));
			this.toolStripButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton1.Name = "toolStripButton1";
			this.toolStripButton1.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton1.Text = "toolStripButton1";
			this.toolStripButton1.ToolTipText = "Create New Project";
			// 
			// toolStripSplitButton1
			// 
			this.toolStripSplitButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripSplitButton1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newRecordingToolStripMenuItem,
            this.newModuleToolStripMenuItem});
			this.toolStripSplitButton1.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton1.Image")));
			this.toolStripSplitButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripSplitButton1.Name = "toolStripSplitButton1";
			this.toolStripSplitButton1.Size = new System.Drawing.Size(32, 22);
			this.toolStripSplitButton1.Text = "toolStripSplitButton1";
			this.toolStripSplitButton1.ToolTipText = "Add New Item";
			this.toolStripSplitButton1.ButtonClick += new System.EventHandler(this.btnAddRecording_Click);
			// 
			// newRecordingToolStripMenuItem
			// 
			this.newRecordingToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newRecordingToolStripMenuItem.Image")));
			this.newRecordingToolStripMenuItem.Name = "newRecordingToolStripMenuItem";
			this.newRecordingToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.newRecordingToolStripMenuItem.Text = "Add Recording...";
			this.newRecordingToolStripMenuItem.Click += new System.EventHandler(this.btnAddRecording_Click);
			// 
			// newModuleToolStripMenuItem
			// 
			this.newModuleToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("newModuleToolStripMenuItem.Image")));
			this.newModuleToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.newModuleToolStripMenuItem.Name = "newModuleToolStripMenuItem";
			this.newModuleToolStripMenuItem.Size = new System.Drawing.Size(167, 22);
			this.newModuleToolStripMenuItem.Text = "Add Module...";
			this.newModuleToolStripMenuItem.Click += new System.EventHandler(this.btnAddModule_Click);
			// 
			// toolStripButton4
			// 
			this.toolStripButton4.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton4.Enabled = false;
			this.toolStripButton4.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton4.Image")));
			this.toolStripButton4.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton4.Name = "toolStripButton4";
			this.toolStripButton4.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton4.Text = "toolStripButton4";
			this.toolStripButton4.ToolTipText = "Open Project";
			// 
			// toolStripButton5
			// 
			this.toolStripButton5.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripButton5.Enabled = false;
			this.toolStripButton5.Image = ((System.Drawing.Image)(resources.GetObject("toolStripButton5.Image")));
			this.toolStripButton5.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.toolStripButton5.Name = "toolStripButton5";
			this.toolStripButton5.Size = new System.Drawing.Size(23, 22);
			this.toolStripButton5.Text = "toolStripButton5";
			this.toolStripButton5.ToolTipText = "Save Project";
			// 
			// toolStripSeparator1
			// 
			this.toolStripSeparator1.Name = "toolStripSeparator1";
			this.toolStripSeparator1.Size = new System.Drawing.Size(6, 25);
			// 
			// toolStripSplitButton2
			// 
			this.toolStripSplitButton2.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.toolStripSplitButton2.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.attachToProcessAndStartToolStripMenuItem,
            this.spwanProcessAndStartRecordingToolStripMenuItem});
			this.toolStripSplitButton2.Image = ((System.Drawing.Image)(resources.GetObject("toolStripSplitButton2.Image")));
			this.toolStripSplitButton2.ImageTransparentColor = System.Drawing.Color.Transparent;
			this.toolStripSplitButton2.Name = "toolStripSplitButton2";
			this.toolStripSplitButton2.Size = new System.Drawing.Size(32, 22);
			// 
			// attachToProcessAndStartToolStripMenuItem
			// 
			this.attachToProcessAndStartToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("attachToProcessAndStartToolStripMenuItem.Image")));
			this.attachToProcessAndStartToolStripMenuItem.Name = "attachToProcessAndStartToolStripMenuItem";
			this.attachToProcessAndStartToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
			this.attachToProcessAndStartToolStripMenuItem.Text = "Attach to process and start recording";
			this.attachToProcessAndStartToolStripMenuItem.Click += new System.EventHandler(this.btnAttachStart_Click);
			// 
			// spwanProcessAndStartRecordingToolStripMenuItem
			// 
			this.spwanProcessAndStartRecordingToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("spwanProcessAndStartRecordingToolStripMenuItem.Image")));
			this.spwanProcessAndStartRecordingToolStripMenuItem.Name = "spwanProcessAndStartRecordingToolStripMenuItem";
			this.spwanProcessAndStartRecordingToolStripMenuItem.Size = new System.Drawing.Size(265, 22);
			this.spwanProcessAndStartRecordingToolStripMenuItem.Text = "Spawn process and start recording";
			this.spwanProcessAndStartRecordingToolStripMenuItem.Click += new System.EventHandler(this.btnSpawnStart_Click);
			// 
			// toolStripSeparator4
			// 
			this.toolStripSeparator4.Name = "toolStripSeparator4";
			this.toolStripSeparator4.Size = new System.Drawing.Size(6, 25);
			// 
			// tsbRefreshList
			// 
			this.tsbRefreshList.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
			this.tsbRefreshList.Image = ((System.Drawing.Image)(resources.GetObject("tsbRefreshList.Image")));
			this.tsbRefreshList.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.tsbRefreshList.Name = "tsbRefreshList";
			this.tsbRefreshList.Size = new System.Drawing.Size(23, 22);
			this.tsbRefreshList.Text = "toolStripButton2";
			this.tsbRefreshList.ToolTipText = "Refresh process list";
			this.tsbRefreshList.Click += new System.EventHandler(this.btnRefreshList_Click);
			// 
			// groupBox4
			// 
			this.groupBox4.Controls.Add(this.lsvRecordings);
			this.groupBox4.Controls.Add(this.label2);
			this.groupBox4.Controls.Add(this.btnRemoveModule);
			this.groupBox4.Controls.Add(this.btnAddModule);
			this.groupBox4.Controls.Add(this.lsvModules);
			this.groupBox4.Controls.Add(this.btnRemoveRecording);
			this.groupBox4.Controls.Add(this.btnAddRecording);
			this.groupBox4.Controls.Add(this.lsvCoverage);
			this.groupBox4.Location = new System.Drawing.Point(3, 52);
			this.groupBox4.Name = "groupBox4";
			this.groupBox4.Size = new System.Drawing.Size(496, 476);
			this.groupBox4.TabIndex = 45;
			this.groupBox4.TabStop = false;
			this.groupBox4.Text = "Recordings";
			// 
			// lsvRecordings
			// 
			this.lsvRecordings.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader8});
			this.lsvRecordings.FullRowSelect = true;
			this.lsvRecordings.Location = new System.Drawing.Point(6, 209);
			this.lsvRecordings.Name = "lsvRecordings";
			this.lsvRecordings.ShowItemToolTips = true;
			this.lsvRecordings.Size = new System.Drawing.Size(156, 230);
			this.lsvRecordings.SmallImageList = this.imageList;
			this.lsvRecordings.TabIndex = 49;
			this.lsvRecordings.UseCompatibleStateImageBehavior = false;
			this.lsvRecordings.View = System.Windows.Forms.View.Details;
			this.lsvRecordings.MouseClick += new System.Windows.Forms.MouseEventHandler(this.lsvRecordings_MouseClick);
			this.lsvRecordings.SelectedIndexChanged += new System.EventHandler(this.lsvRecordings_SelectedIndexChanged);
			// 
			// columnHeader8
			// 
			this.columnHeader8.Text = "Recording";
			this.columnHeader8.Width = 152;
			// 
			// imageList
			// 
			this.imageList.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageList.ImageStream")));
			this.imageList.TransparentColor = System.Drawing.Color.Magenta;
			this.imageList.Images.SetKeyName(0, "");
			this.imageList.Images.SetKeyName(1, "");
			this.imageList.Images.SetKeyName(2, "");
			this.imageList.Images.SetKeyName(3, "");
			this.imageList.Images.SetKeyName(4, "");
			this.imageList.Images.SetKeyName(5, "");
			this.imageList.Images.SetKeyName(6, "Save.bmp");
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(6, 22);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(133, 13);
			this.label2.TabIndex = 48;
			this.label2.Text = "Use modules for recording:";
			// 
			// btnRemoveModule
			// 
			this.btnRemoveModule.Location = new System.Drawing.Point(251, 180);
			this.btnRemoveModule.Name = "btnRemoveModule";
			this.btnRemoveModule.Size = new System.Drawing.Size(102, 23);
			this.btnRemoveModule.TabIndex = 47;
			this.btnRemoveModule.Text = "Remove module";
			this.btnRemoveModule.UseVisualStyleBackColor = true;
			this.btnRemoveModule.Click += new System.EventHandler(this.btnRemoveModule_Click);
			// 
			// btnAddModule
			// 
			this.btnAddModule.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnAddModule.Location = new System.Drawing.Point(143, 180);
			this.btnAddModule.Name = "btnAddModule";
			this.btnAddModule.Size = new System.Drawing.Size(102, 23);
			this.btnAddModule.TabIndex = 46;
			this.btnAddModule.Text = "Add module...";
			this.btnAddModule.UseVisualStyleBackColor = true;
			this.btnAddModule.Click += new System.EventHandler(this.btnAddModule_Click);
			// 
			// lsvModules
			// 
			this.lsvModules.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader7});
			this.lsvModules.Location = new System.Drawing.Point(6, 38);
			this.lsvModules.MultiSelect = false;
			this.lsvModules.Name = "lsvModules";
			this.lsvModules.ShowItemToolTips = true;
			this.lsvModules.Size = new System.Drawing.Size(483, 136);
			this.lsvModules.TabIndex = 45;
			this.lsvModules.UseCompatibleStateImageBehavior = false;
			this.lsvModules.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader3
			// 
			this.columnHeader3.Text = "Module";
			this.columnHeader3.Width = 285;
			// 
			// columnHeader4
			// 
			this.columnHeader4.Text = "# Functions";
			this.columnHeader4.Width = 68;
			// 
			// columnHeader5
			// 
			this.columnHeader5.Text = "# BB";
			this.columnHeader5.Width = 48;
			// 
			// columnHeader7
			// 
			this.columnHeader7.Text = "IBA";
			this.columnHeader7.Width = 62;
			// 
			// btnRemoveRecording
			// 
			this.btnRemoveRecording.Location = new System.Drawing.Point(251, 446);
			this.btnRemoveRecording.Name = "btnRemoveRecording";
			this.btnRemoveRecording.Size = new System.Drawing.Size(118, 23);
			this.btnRemoveRecording.TabIndex = 40;
			this.btnRemoveRecording.Text = "Remove recording";
			this.btnRemoveRecording.UseVisualStyleBackColor = true;
			this.btnRemoveRecording.Click += new System.EventHandler(this.btnRemoveRecording_Click);
			// 
			// btnAddRecording
			// 
			this.btnAddRecording.Location = new System.Drawing.Point(127, 446);
			this.btnAddRecording.Name = "btnAddRecording";
			this.btnAddRecording.Size = new System.Drawing.Size(118, 23);
			this.btnAddRecording.TabIndex = 39;
			this.btnAddRecording.Text = "Add recording...";
			this.btnAddRecording.UseVisualStyleBackColor = true;
			this.btnAddRecording.Click += new System.EventHandler(this.btnAddRecording_Click);
			// 
			// lsvCoverage
			// 
			this.lsvCoverage.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader12,
            this.columnHeader6,
            this.columnHeader10});
			this.lsvCoverage.Location = new System.Drawing.Point(168, 209);
			this.lsvCoverage.Name = "lsvCoverage";
			this.lsvCoverage.ShowItemToolTips = true;
			this.lsvCoverage.Size = new System.Drawing.Size(321, 231);
			this.lsvCoverage.TabIndex = 36;
			this.lsvCoverage.UseCompatibleStateImageBehavior = false;
			this.lsvCoverage.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader12
			// 
			this.columnHeader12.Text = "Module";
			this.columnHeader12.Width = 219;
			// 
			// columnHeader6
			// 
			this.columnHeader6.Text = "# Hits";
			this.columnHeader6.Width = 40;
			// 
			// columnHeader10
			// 
			this.columnHeader10.Text = "Coverage";
			this.columnHeader10.Width = 58;
			// 
			// cmsRecording
			// 
			this.cmsRecording.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.addRecordingToolStripMenuItem1,
            this.removeRecordingToolStripMenuItem,
            this.toolStripSeparator2,
            this.changeTypeToToolStripMenuItem});
			this.cmsRecording.Name = "cmsRecording";
			this.cmsRecording.Size = new System.Drawing.Size(173, 76);
			// 
			// addRecordingToolStripMenuItem1
			// 
			this.addRecordingToolStripMenuItem1.Name = "addRecordingToolStripMenuItem1";
			this.addRecordingToolStripMenuItem1.Size = new System.Drawing.Size(172, 22);
			this.addRecordingToolStripMenuItem1.Text = "Add recording";
			this.addRecordingToolStripMenuItem1.Click += new System.EventHandler(this.btnAddRecording_Click);
			// 
			// removeRecordingToolStripMenuItem
			// 
			this.removeRecordingToolStripMenuItem.Name = "removeRecordingToolStripMenuItem";
			this.removeRecordingToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.removeRecordingToolStripMenuItem.Text = "Remove recording";
			this.removeRecordingToolStripMenuItem.Click += new System.EventHandler(this.btnRemoveRecording_Click);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(169, 6);
			// 
			// changeTypeToToolStripMenuItem
			// 
			this.changeTypeToToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.activeToolStripMenuItem,
            this.inactiveToolStripMenuItem,
            this.filterToolStripMenuItem});
			this.changeTypeToToolStripMenuItem.Name = "changeTypeToToolStripMenuItem";
			this.changeTypeToToolStripMenuItem.Size = new System.Drawing.Size(172, 22);
			this.changeTypeToToolStripMenuItem.Text = "Change type to";
			// 
			// activeToolStripMenuItem
			// 
			this.activeToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("activeToolStripMenuItem.Image")));
			this.activeToolStripMenuItem.Name = "activeToolStripMenuItem";
			this.activeToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.activeToolStripMenuItem.Text = "Active";
			this.activeToolStripMenuItem.ToolTipText = "Use for recording";
			this.activeToolStripMenuItem.Click += new System.EventHandler(this.activeToolStripMenuItem_Click);
			// 
			// inactiveToolStripMenuItem
			// 
			this.inactiveToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("inactiveToolStripMenuItem.Image")));
			this.inactiveToolStripMenuItem.Name = "inactiveToolStripMenuItem";
			this.inactiveToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.inactiveToolStripMenuItem.Text = "Inactive";
			this.inactiveToolStripMenuItem.ToolTipText = "Disabled";
			this.inactiveToolStripMenuItem.Click += new System.EventHandler(this.inactiveToolStripMenuItem_Click);
			// 
			// filterToolStripMenuItem
			// 
			this.filterToolStripMenuItem.Image = ((System.Drawing.Image)(resources.GetObject("filterToolStripMenuItem.Image")));
			this.filterToolStripMenuItem.ImageTransparentColor = System.Drawing.Color.Magenta;
			this.filterToolStripMenuItem.Name = "filterToolStripMenuItem";
			this.filterToolStripMenuItem.Size = new System.Drawing.Size(124, 22);
			this.filterToolStripMenuItem.Text = "Filter";
			this.filterToolStripMenuItem.ToolTipText = "Use as filter";
			this.filterToolStripMenuItem.Click += new System.EventHandler(this.filterToolStripMenuItem_Click);
			// 
			// tabControl1
			// 
			this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
			this.tabControl1.Controls.Add(this.tabPage1);
			this.tabControl1.Controls.Add(this.tabPage2);
			this.tabControl1.HotTrack = true;
			this.tabControl1.ImageList = this.imageList;
			this.tabControl1.Location = new System.Drawing.Point(504, 57);
			this.tabControl1.Multiline = true;
			this.tabControl1.Name = "tabControl1";
			this.tabControl1.SelectedIndex = 0;
			this.tabControl1.Size = new System.Drawing.Size(326, 475);
			this.tabControl1.TabIndex = 47;
			// 
			// tabPage1
			// 
			this.tabPage1.Controls.Add(this.groupBox1);
			this.tabPage1.ImageIndex = 3;
			this.tabPage1.Location = new System.Drawing.Point(4, 26);
			this.tabPage1.Name = "tabPage1";
			this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage1.Size = new System.Drawing.Size(318, 445);
			this.tabPage1.TabIndex = 0;
			this.tabPage1.Text = "Code coverage engine";
			this.tabPage1.UseVisualStyleBackColor = true;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.btnSelectExe);
			this.groupBox1.Controls.Add(this.chkOnlyAccessible);
			this.groupBox1.Controls.Add(this.groupBox2);
			this.groupBox1.Controls.Add(this.btnRefreshList);
			this.groupBox1.Controls.Add(this.btnAttachStart);
			this.groupBox1.Controls.Add(this.btnSpawnStart);
			this.groupBox1.Controls.Add(this.txbCmdLine);
			this.groupBox1.Controls.Add(this.label1);
			this.groupBox1.Controls.Add(this.lsvProcesses);
			this.groupBox1.Location = new System.Drawing.Point(0, 0);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(318, 445);
			this.groupBox1.TabIndex = 47;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "Code coverage engine";
			// 
			// btnSelectExe
			// 
			this.btnSelectExe.Location = new System.Drawing.Point(286, 386);
			this.btnSelectExe.Name = "btnSelectExe";
			this.btnSelectExe.Size = new System.Drawing.Size(24, 23);
			this.btnSelectExe.TabIndex = 62;
			this.btnSelectExe.Text = "...";
			this.btnSelectExe.UseVisualStyleBackColor = true;
			this.btnSelectExe.Click += new System.EventHandler(this.btnSelectExe_Click);
			// 
			// chkOnlyAccessible
			// 
			this.chkOnlyAccessible.AutoSize = true;
			this.chkOnlyAccessible.Checked = true;
			this.chkOnlyAccessible.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkOnlyAccessible.Location = new System.Drawing.Point(133, 117);
			this.chkOnlyAccessible.Name = "chkOnlyAccessible";
			this.chkOnlyAccessible.Size = new System.Drawing.Size(179, 17);
			this.chkOnlyAccessible.TabIndex = 61;
			this.chkOnlyAccessible.Text = "Only show accessible processes";
			this.chkOnlyAccessible.UseVisualStyleBackColor = true;
			this.chkOnlyAccessible.CheckedChanged += new System.EventHandler(this.btnRefreshList_Click);
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.chkVerbose);
			this.groupBox2.Controls.Add(this.chkRestoreBPs);
			this.groupBox2.Controls.Add(this.rdbContinueExec);
			this.groupBox2.Controls.Add(this.rdbAppHandle);
			this.groupBox2.Location = new System.Drawing.Point(9, 19);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(303, 88);
			this.groupBox2.TabIndex = 60;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "Debugging options";
			// 
			// chkVerbose
			// 
			this.chkVerbose.AutoSize = true;
			this.chkVerbose.Location = new System.Drawing.Point(163, 65);
			this.chkVerbose.Name = "chkVerbose";
			this.chkVerbose.Size = new System.Drawing.Size(131, 17);
			this.chkVerbose.TabIndex = 62;
			this.chkVerbose.Text = "Verbose output (slow!)";
			this.chkVerbose.UseVisualStyleBackColor = true;
			this.chkVerbose.CheckedChanged += new System.EventHandler(this.chkVerbose_CheckedChanged);
			// 
			// chkRestoreBPs
			// 
			this.chkRestoreBPs.AutoSize = true;
			this.chkRestoreBPs.Location = new System.Drawing.Point(6, 65);
			this.chkRestoreBPs.Name = "chkRestoreBPs";
			this.chkRestoreBPs.Size = new System.Drawing.Size(121, 17);
			this.chkRestoreBPs.TabIndex = 61;
			this.chkRestoreBPs.Text = "Restore breakpoints";
			this.chkRestoreBPs.UseVisualStyleBackColor = true;
			this.chkRestoreBPs.CheckedChanged += new System.EventHandler(this.chkRestoreBPs_CheckedChanged);
			// 
			// rdbContinueExec
			// 
			this.rdbContinueExec.AutoSize = true;
			this.rdbContinueExec.Location = new System.Drawing.Point(6, 42);
			this.rdbContinueExec.Name = "rdbContinueExec";
			this.rdbContinueExec.Size = new System.Drawing.Size(265, 17);
			this.rdbContinueExec.TabIndex = 60;
			this.rdbContinueExec.Text = "Bypass exception handling and continue execution";
			this.rdbContinueExec.UseVisualStyleBackColor = true;
			this.rdbContinueExec.CheckedChanged += new System.EventHandler(this.rdbAppHandle_CheckedChanged);
			// 
			// rdbAppHandle
			// 
			this.rdbAppHandle.AutoSize = true;
			this.rdbAppHandle.Checked = true;
			this.rdbAppHandle.Location = new System.Drawing.Point(6, 19);
			this.rdbAppHandle.Name = "rdbAppHandle";
			this.rdbAppHandle.Size = new System.Drawing.Size(183, 17);
			this.rdbAppHandle.TabIndex = 59;
			this.rdbAppHandle.TabStop = true;
			this.rdbAppHandle.Text = "Let application handle exceptions";
			this.rdbAppHandle.UseVisualStyleBackColor = true;
			this.rdbAppHandle.CheckedChanged += new System.EventHandler(this.rdbAppHandle_CheckedChanged);
			// 
			// btnRefreshList
			// 
			this.btnRefreshList.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnRefreshList.ImageList = this.imageList;
			this.btnRefreshList.Location = new System.Drawing.Point(9, 113);
			this.btnRefreshList.Name = "btnRefreshList";
			this.btnRefreshList.Size = new System.Drawing.Size(99, 23);
			this.btnRefreshList.TabIndex = 59;
			this.btnRefreshList.Text = "Refresh list";
			this.btnRefreshList.UseVisualStyleBackColor = true;
			this.btnRefreshList.Click += new System.EventHandler(this.btnRefreshList_Click);
			// 
			// btnAttachStart
			// 
			this.btnAttachStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnAttachStart.ImageIndex = 0;
			this.btnAttachStart.ImageList = this.imageListStartStop;
			this.btnAttachStart.Location = new System.Drawing.Point(62, 350);
			this.btnAttachStart.Name = "btnAttachStart";
			this.btnAttachStart.Size = new System.Drawing.Size(195, 23);
			this.btnAttachStart.TabIndex = 54;
			this.btnAttachStart.Text = "Attach and start recording";
			this.btnAttachStart.UseVisualStyleBackColor = true;
			this.btnAttachStart.Click += new System.EventHandler(this.btnAttachStart_Click);
			// 
			// imageListStartStop
			// 
			this.imageListStartStop.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("imageListStartStop.ImageStream")));
			this.imageListStartStop.TransparentColor = System.Drawing.Color.Transparent;
			this.imageListStartStop.Images.SetKeyName(0, "servicerunning.ico");
			this.imageListStartStop.Images.SetKeyName(1, "servicestopped.ico");
			// 
			// btnSpawnStart
			// 
			this.btnSpawnStart.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnSpawnStart.ImageIndex = 0;
			this.btnSpawnStart.ImageList = this.imageListStartStop;
			this.btnSpawnStart.Location = new System.Drawing.Point(45, 414);
			this.btnSpawnStart.Name = "btnSpawnStart";
			this.btnSpawnStart.Size = new System.Drawing.Size(229, 23);
			this.btnSpawnStart.TabIndex = 53;
			this.btnSpawnStart.Text = "Spawn process and start recording";
			this.btnSpawnStart.UseVisualStyleBackColor = true;
			this.btnSpawnStart.Click += new System.EventHandler(this.btnSpawnStart_Click);
			// 
			// txbCmdLine
			// 
			this.txbCmdLine.Location = new System.Drawing.Point(6, 388);
			this.txbCmdLine.Name = "txbCmdLine";
			this.txbCmdLine.Size = new System.Drawing.Size(276, 20);
			this.txbCmdLine.TabIndex = 51;
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(6, 372);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(47, 13);
			this.label1.TabIndex = 50;
			this.label1.Text = "Cmdline:";
			// 
			// lsvProcesses
			// 
			this.lsvProcesses.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
			this.lsvProcesses.FullRowSelect = true;
			this.lsvProcesses.HideSelection = false;
			this.lsvProcesses.Location = new System.Drawing.Point(9, 142);
			this.lsvProcesses.MultiSelect = false;
			this.lsvProcesses.Name = "lsvProcesses";
			this.lsvProcesses.Size = new System.Drawing.Size(303, 202);
			this.lsvProcesses.TabIndex = 1;
			this.lsvProcesses.UseCompatibleStateImageBehavior = false;
			this.lsvProcesses.View = System.Windows.Forms.View.Details;
			this.lsvProcesses.ColumnClick += new System.Windows.Forms.ColumnClickEventHandler(this.lsvProcesses_ColumnClick);
			// 
			// columnHeader1
			// 
			this.columnHeader1.Text = "PID";
			this.columnHeader1.Width = 62;
			// 
			// columnHeader2
			// 
			this.columnHeader2.Text = "Process";
			this.columnHeader2.Width = 218;
			// 
			// tabPage2
			// 
			this.tabPage2.Controls.Add(this.groupBox3);
			this.tabPage2.ImageIndex = 4;
			this.tabPage2.Location = new System.Drawing.Point(4, 26);
			this.tabPage2.Name = "tabPage2";
			this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
			this.tabPage2.Size = new System.Drawing.Size(318, 445);
			this.tabPage2.TabIndex = 1;
			this.tabPage2.Text = "Export final set";
			this.tabPage2.UseVisualStyleBackColor = true;
			// 
			// groupBox3
			// 
			this.groupBox3.Controls.Add(this.groupBox5);
			this.groupBox3.Controls.Add(this.label3);
			this.groupBox3.Controls.Add(this.lsvFinalSet);
			this.groupBox3.Controls.Add(this.btnExport);
			this.groupBox3.Controls.Add(this.btnGenerate);
			this.groupBox3.Location = new System.Drawing.Point(0, 0);
			this.groupBox3.Name = "groupBox3";
			this.groupBox3.Size = new System.Drawing.Size(318, 445);
			this.groupBox3.TabIndex = 0;
			this.groupBox3.TabStop = false;
			this.groupBox3.Text = "Export final set";
			// 
			// groupBox5
			// 
			this.groupBox5.Controls.Add(this.checkBox1);
			this.groupBox5.Controls.Add(this.picColor);
			this.groupBox5.Controls.Add(this.checkBox4);
			this.groupBox5.Controls.Add(this.checkBox2);
			this.groupBox5.Controls.Add(this.btnColor);
			this.groupBox5.Controls.Add(this.chkColor);
			this.groupBox5.Location = new System.Drawing.Point(9, 277);
			this.groupBox5.Name = "groupBox5";
			this.groupBox5.Size = new System.Drawing.Size(301, 158);
			this.groupBox5.TabIndex = 7;
			this.groupBox5.TabStop = false;
			this.groupBox5.Text = "Export options";
			// 
			// checkBox1
			// 
			this.checkBox1.AutoSize = true;
			this.checkBox1.Enabled = false;
			this.checkBox1.Location = new System.Drawing.Point(6, 94);
			this.checkBox1.Name = "checkBox1";
			this.checkBox1.Size = new System.Drawing.Size(185, 17);
			this.checkBox1.TabIndex = 6;
			this.checkBox1.Text = "Export hits as markers (NOT YET)";
			this.checkBox1.UseVisualStyleBackColor = true;
			// 
			// picColor
			// 
			this.picColor.BackColor = System.Drawing.Color.LightGreen;
			this.picColor.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picColor.Location = new System.Drawing.Point(214, 19);
			this.picColor.Name = "picColor";
			this.picColor.Size = new System.Drawing.Size(26, 23);
			this.picColor.TabIndex = 5;
			this.picColor.TabStop = false;
			// 
			// checkBox4
			// 
			this.checkBox4.AutoSize = true;
			this.checkBox4.Enabled = false;
			this.checkBox4.Location = new System.Drawing.Point(6, 117);
			this.checkBox4.Name = "checkBox4";
			this.checkBox4.Size = new System.Drawing.Size(210, 17);
			this.checkBox4.TabIndex = 4;
			this.checkBox4.Text = "Append hit count to marker (NOT YET)";
			this.checkBox4.UseVisualStyleBackColor = true;
			// 
			// checkBox2
			// 
			this.checkBox2.AutoSize = true;
			this.checkBox2.Enabled = false;
			this.checkBox2.Location = new System.Drawing.Point(6, 71);
			this.checkBox2.Name = "checkBox2";
			this.checkBox2.Size = new System.Drawing.Size(200, 17);
			this.checkBox2.TabIndex = 3;
			this.checkBox2.Text = "Blend with existing colors (NOT YET)";
			this.checkBox2.UseVisualStyleBackColor = true;
			// 
			// btnColor
			// 
			this.btnColor.Location = new System.Drawing.Point(84, 19);
			this.btnColor.Name = "btnColor";
			this.btnColor.Size = new System.Drawing.Size(125, 23);
			this.btnColor.TabIndex = 1;
			this.btnColor.Text = "Select color...";
			this.btnColor.UseVisualStyleBackColor = true;
			this.btnColor.Click += new System.EventHandler(this.btnColor_Click);
			// 
			// chkColor
			// 
			this.chkColor.AutoSize = true;
			this.chkColor.Location = new System.Drawing.Point(6, 48);
			this.chkColor.Name = "chkColor";
			this.chkColor.Size = new System.Drawing.Size(136, 17);
			this.chkColor.TabIndex = 0;
			this.chkColor.Text = "Export color information";
			this.chkColor.UseVisualStyleBackColor = true;
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(6, 16);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(124, 13);
			this.label3.TabIndex = 6;
			this.label3.Text = "Final code coverage set:";
			// 
			// lsvFinalSet
			// 
			this.lsvFinalSet.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader9,
            this.columnHeader11});
			this.lsvFinalSet.Location = new System.Drawing.Point(9, 32);
			this.lsvFinalSet.Name = "lsvFinalSet";
			this.lsvFinalSet.Size = new System.Drawing.Size(301, 210);
			this.lsvFinalSet.TabIndex = 5;
			this.lsvFinalSet.UseCompatibleStateImageBehavior = false;
			this.lsvFinalSet.View = System.Windows.Forms.View.Details;
			// 
			// columnHeader9
			// 
			this.columnHeader9.Text = "Module";
			this.columnHeader9.Width = 230;
			// 
			// columnHeader11
			// 
			this.columnHeader11.Text = "# Hits";
			this.columnHeader11.Width = 55;
			// 
			// btnExport
			// 
			this.btnExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnExport.ImageIndex = 6;
			this.btnExport.ImageList = this.imageList;
			this.btnExport.Location = new System.Drawing.Point(162, 248);
			this.btnExport.Name = "btnExport";
			this.btnExport.Size = new System.Drawing.Size(117, 23);
			this.btnExport.TabIndex = 2;
			this.btnExport.Text = "Export hits...";
			this.btnExport.UseVisualStyleBackColor = true;
			this.btnExport.Click += new System.EventHandler(this.btnExport_Click);
			// 
			// btnGenerate
			// 
			this.btnGenerate.BackColor = System.Drawing.SystemColors.Control;
			this.btnGenerate.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.btnGenerate.ImageIndex = 4;
			this.btnGenerate.ImageList = this.imageList;
			this.btnGenerate.Location = new System.Drawing.Point(39, 248);
			this.btnGenerate.Name = "btnGenerate";
			this.btnGenerate.Size = new System.Drawing.Size(117, 23);
			this.btnGenerate.TabIndex = 4;
			this.btnGenerate.Text = "Generate set";
			this.btnGenerate.UseVisualStyleBackColor = false;
			this.btnGenerate.Click += new System.EventHandler(this.btnGenerate_Click);
			// 
			// colorDialog
			// 
			this.colorDialog.Color = System.Drawing.Color.Orange;
			// 
			// saveFileDialog
			// 
			this.saveFileDialog.Filter = "N-Coverage hits (*.nhits)|*.nhits|All files (*.*)|*.*";
			// 
			// openExedialog
			// 
			this.openExedialog.Filter = "Executables (*.exe)|*.exe|All files (*.*)|*.*";
			// 
			// frmMain
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(830, 715);
			this.Controls.Add(this.groupBox4);
			this.Controls.Add(this.toolStrip);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.rtbLog);
			this.Controls.Add(this.menuStrip);
			this.Controls.Add(this.tabControl1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.MainMenuStrip = this.menuStrip;
			this.MaximizeBox = false;
			this.Name = "frmMain";
			this.Text = "N-Coverage v1.0 ALPHA";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.menuStrip.ResumeLayout(false);
			this.menuStrip.PerformLayout();
			this.toolStrip.ResumeLayout(false);
			this.toolStrip.PerformLayout();
			this.groupBox4.ResumeLayout(false);
			this.groupBox4.PerformLayout();
			this.cmsRecording.ResumeLayout(false);
			this.tabControl1.ResumeLayout(false);
			this.tabPage1.ResumeLayout(false);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.tabPage2.ResumeLayout(false);
			this.groupBox3.ResumeLayout(false);
			this.groupBox3.PerformLayout();
			this.groupBox5.ResumeLayout(false);
			this.groupBox5.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.picColor)).EndInit();
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.MenuStrip menuStrip;
		private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newProjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem openProjectToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem optionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
		private System.Windows.Forms.RichTextBox rtbLog;
		private System.Windows.Forms.OpenFileDialog openFileDialog;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.ToolStrip toolStrip;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
		private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton toolStripButton4;
		private System.Windows.Forms.ToolStripButton toolStripButton5;
		private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton1;
		private System.Windows.Forms.ToolStripMenuItem addRecordingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newRecordingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem newModuleToolStripMenuItem;
		private System.Windows.Forms.ToolStripButton toolStripButton1;
		private System.Windows.Forms.ToolStripMenuItem addModuleToolStripMenuItem;
		private System.Windows.Forms.GroupBox groupBox4;
		private System.Windows.Forms.ListView lsvCoverage;
		private System.Windows.Forms.ColumnHeader columnHeader6;
		private System.Windows.Forms.ColumnHeader columnHeader10;
		private System.Windows.Forms.Button btnAddRecording;
		private System.Windows.Forms.Button btnRemoveRecording;
		private System.Windows.Forms.ImageList imageList;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button btnRemoveModule;
		private System.Windows.Forms.Button btnAddModule;
		private System.Windows.Forms.ListView lsvModules;
		private System.Windows.Forms.ColumnHeader columnHeader3;
		private System.Windows.Forms.ColumnHeader columnHeader4;
		private System.Windows.Forms.ColumnHeader columnHeader5;
		private System.Windows.Forms.ColumnHeader columnHeader7;
		private System.Windows.Forms.ContextMenuStrip cmsRecording;
		private System.Windows.Forms.ToolStripMenuItem removeRecordingToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.ToolStripMenuItem changeTypeToToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem inactiveToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem activeToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem filterToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem recordingToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem addRecordingToolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem addRecordingToolStripMenuItem2;
		private System.Windows.Forms.ToolStripMenuItem removeRecordingToolStripMenuItem1;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
		private System.Windows.Forms.ToolStripSplitButton toolStripSplitButton2;
		private System.Windows.Forms.ToolStripMenuItem attachToProcessAndStartToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem spwanProcessAndStartRecordingToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator4;
		private System.Windows.Forms.ToolStripButton tsbRefreshList;
		private System.Windows.Forms.TabControl tabControl1;
		private System.Windows.Forms.TabPage tabPage1;
		private System.Windows.Forms.TabPage tabPage2;
		private System.Windows.Forms.GroupBox groupBox1;
		private System.Windows.Forms.Button btnAttachStart;
		private System.Windows.Forms.Button btnSpawnStart;
		private System.Windows.Forms.TextBox txbCmdLine;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.ListView lsvProcesses;
		private System.Windows.Forms.ColumnHeader columnHeader1;
		private System.Windows.Forms.ColumnHeader columnHeader2;
		private System.Windows.Forms.Button btnRefreshList;
		private System.Windows.Forms.GroupBox groupBox2;
		private System.Windows.Forms.CheckBox chkVerbose;
		private System.Windows.Forms.CheckBox chkRestoreBPs;
		private System.Windows.Forms.RadioButton rdbContinueExec;
		private System.Windows.Forms.RadioButton rdbAppHandle;
		private System.Windows.Forms.ColorDialog colorDialog;
		private System.Windows.Forms.GroupBox groupBox3;
		private System.Windows.Forms.GroupBox groupBox5;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.ListView lsvFinalSet;
		private System.Windows.Forms.ColumnHeader columnHeader9;
		private System.Windows.Forms.ColumnHeader columnHeader11;
		private System.Windows.Forms.Button btnGenerate;
		private System.Windows.Forms.Button btnExport;
		private System.Windows.Forms.Button btnColor;
		private System.Windows.Forms.CheckBox chkColor;
		private System.Windows.Forms.CheckBox checkBox4;
		private System.Windows.Forms.CheckBox checkBox2;
		private System.Windows.Forms.ImageList imageListStartStop;
		private System.Windows.Forms.CheckBox chkOnlyAccessible;
		private System.Windows.Forms.PictureBox picColor;
		private System.Windows.Forms.ColumnHeader columnHeader12;
		private System.Windows.Forms.ListView lsvRecordings;
		private System.Windows.Forms.ColumnHeader columnHeader8;
		private System.Windows.Forms.SaveFileDialog saveFileDialog;
		private System.Windows.Forms.CheckBox checkBox1;
		private System.Windows.Forms.Button btnSelectExe;
		private System.Windows.Forms.OpenFileDialog openExedialog;
		private System.Windows.Forms.ToolStripSeparator toolStripMenuItem1;
		private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;


	}
}

