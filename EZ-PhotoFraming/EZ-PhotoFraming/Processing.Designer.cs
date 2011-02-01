namespace PhotoFraming
{
    partial class Processing
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
            System.Windows.Forms.TreeNode treeNode1 = new System.Windows.Forms.TreeNode("Node1 Child Sample");
            System.Windows.Forms.TreeNode treeNode2 = new System.Windows.Forms.TreeNode("Node0 Sample", new System.Windows.Forms.TreeNode[] {
            treeNode1});
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Processing));
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.toolStripSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.statusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.notifyIcon1 = new System.Windows.Forms.NotifyIcon(this.components);
            this.dirTree = new System.Windows.Forms.TreeView();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.backgroundWorker = new System.ComponentModel.BackgroundWorker();
            this.ptBox = new System.Windows.Forms.PictureBox();
            this.openToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.startallBtn = new System.Windows.Forms.ToolStripButton();
            this.startselectedBtn = new System.Windows.Forms.ToolStripButton();
            this.removeDirBtn = new System.Windows.Forms.ToolStripButton();
            this.helpToolStripButton = new System.Windows.Forms.ToolStripButton();
            this.toolStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.ptBox)).BeginInit();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(50, 50);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openToolStripButton,
            this.toolStripSeparator,
            this.startallBtn,
            this.startselectedBtn,
            this.toolStripSeparator2,
            this.removeDirBtn,
            this.toolStripSeparator1,
            this.helpToolStripButton,
            this.toolStripSeparator3});
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(742, 47);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            // 
            // toolStripSeparator
            // 
            this.toolStripSeparator.Name = "toolStripSeparator";
            this.toolStripSeparator.Size = new System.Drawing.Size(6, 47);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(6, 47);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(6, 47);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(6, 47);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLbl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 444);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(742, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // statusLbl
            // 
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(109, 17);
            this.statusLbl.Text = "toolStripStatusLabel1";
            // 
            // notifyIcon1
            // 
            this.notifyIcon1.Text = "notifyIcon1";
            this.notifyIcon1.Visible = true;
            // 
            // dirTree
            // 
            this.dirTree.AllowDrop = true;
            this.dirTree.CheckBoxes = true;
            this.dirTree.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dirTree.Location = new System.Drawing.Point(0, 0);
            this.dirTree.Name = "dirTree";
            treeNode1.Name = "Node1";
            treeNode1.Text = "Node1 Child Sample";
            treeNode2.Name = "Node0";
            treeNode2.Text = "Node0 Sample";
            this.dirTree.Nodes.AddRange(new System.Windows.Forms.TreeNode[] {
            treeNode2});
            this.dirTree.Size = new System.Drawing.Size(250, 397);
            this.dirTree.TabIndex = 2;
            this.dirTree.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.dirTree_AfterSelect);
            this.dirTree.DragDrop += new System.Windows.Forms.DragEventHandler(this.dirTree_DragDrop);
            this.dirTree.DragEnter += new System.Windows.Forms.DragEventHandler(this.dirTree_DragEnter);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 47);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.dirTree);
            this.splitContainer1.Panel1MinSize = 250;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.ptBox);
            this.splitContainer1.Panel2MinSize = 250;
            this.splitContainer1.Size = new System.Drawing.Size(742, 397);
            this.splitContainer1.SplitterDistance = 250;
            this.splitContainer1.TabIndex = 4;
            // 
            // backgroundWorker
            // 
            this.backgroundWorker.WorkerReportsProgress = true;
            this.backgroundWorker.WorkerSupportsCancellation = true;
            this.backgroundWorker.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker_DoWork);
            this.backgroundWorker.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker_RunWorkerCompleted);
            // 
            // ptBox
            // 
            this.ptBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ptBox.ErrorImage = global::PhotoFraming.Properties.Resources.ErrorIco;
            this.ptBox.InitialImage = global::PhotoFraming.Properties.Resources.LoadingIco;
            this.ptBox.Location = new System.Drawing.Point(0, 0);
            this.ptBox.Name = "ptBox";
            this.ptBox.Size = new System.Drawing.Size(488, 397);
            this.ptBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ptBox.TabIndex = 0;
            this.ptBox.TabStop = false;
            this.ptBox.LoadCompleted += new System.ComponentModel.AsyncCompletedEventHandler(this.ptBox_LoadCompleted);
            this.ptBox.SizeChanged += new System.EventHandler(this.ptBox_SizeChanged);
            // 
            // openToolStripButton
            // 
            this.openToolStripButton.AutoSize = false;
            this.openToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.openToolStripButton.Image = ((System.Drawing.Image)(resources.GetObject("openToolStripButton.Image")));
            this.openToolStripButton.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.openToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.openToolStripButton.Name = "openToolStripButton";
            this.openToolStripButton.Size = new System.Drawing.Size(46, 44);
            this.openToolStripButton.Text = "&Open";
            // 
            // startallBtn
            // 
            this.startallBtn.AutoSize = false;
            this.startallBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.startallBtn.Image = global::PhotoFraming.Properties.Resources.StartAllIco;
            this.startallBtn.Name = "startallBtn";
            this.startallBtn.Size = new System.Drawing.Size(46, 44);
            this.startallBtn.ToolTipText = "Process All Images";
            this.startallBtn.Click += new System.EventHandler(this.startallBtn_Click);
            // 
            // startselectedBtn
            // 
            this.startselectedBtn.AutoSize = false;
            this.startselectedBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.startselectedBtn.Image = global::PhotoFraming.Properties.Resources.StartIco;
            this.startselectedBtn.Name = "startselectedBtn";
            this.startselectedBtn.Size = new System.Drawing.Size(46, 44);
            this.startselectedBtn.ToolTipText = "Process Selected Images";
            this.startselectedBtn.Click += new System.EventHandler(this.startselectedBtn_Click);
            // 
            // removeDirBtn
            // 
            this.removeDirBtn.AutoSize = false;
            this.removeDirBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.removeDirBtn.Image = global::PhotoFraming.Properties.Resources.RemoveIco;
            this.removeDirBtn.Name = "removeDirBtn";
            this.removeDirBtn.Size = new System.Drawing.Size(46, 44);
            this.removeDirBtn.ToolTipText = "Remove Selected Images";
            this.removeDirBtn.Click += new System.EventHandler(this.removeDirBtn_Click);
            // 
            // helpToolStripButton
            // 
            this.helpToolStripButton.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.helpToolStripButton.AutoSize = false;
            this.helpToolStripButton.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.helpToolStripButton.Image = global::PhotoFraming.Properties.Resources.HelpIco;
            this.helpToolStripButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.helpToolStripButton.Name = "helpToolStripButton";
            this.helpToolStripButton.Size = new System.Drawing.Size(46, 44);
            this.helpToolStripButton.Text = "He&lp";
            // 
            // Processing
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(742, 466);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.toolStrip1);
            this.MinimumSize = new System.Drawing.Size(750, 500);
            this.Name = "Processing";
            this.Text = "Processing";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.dirTree_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.dirTree_DragEnter);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.ptBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton openToolStripButton;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator;
        private System.Windows.Forms.ToolStripButton helpToolStripButton;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.NotifyIcon notifyIcon1;
        private System.Windows.Forms.TreeView dirTree;
        private System.Windows.Forms.ToolStripStatusLabel statusLbl;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ToolStripButton removeDirBtn;
        private System.Windows.Forms.PictureBox ptBox;
        private System.Windows.Forms.ToolStripButton startselectedBtn;
        private System.Windows.Forms.ToolStripButton startallBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.ComponentModel.BackgroundWorker backgroundWorker;
    }
}