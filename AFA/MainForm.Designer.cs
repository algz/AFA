namespace AFA
{
    partial class MainForm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsFile = new System.Windows.Forms.ToolStripMenuItem();
            this.tsNew = new System.Windows.Forms.ToolStripMenuItem();
            this.tsOpen = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSave = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSaveAs = new System.Windows.Forms.ToolStripMenuItem();
            this.tsExit = new System.Windows.Forms.ToolStripMenuItem();
            this.tsCalculate = new System.Windows.Forms.ToolStripMenuItem();
            this.configure = new System.Windows.Forms.ToolStripMenuItem();
            this.tsHelp = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsAddGK = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsAddGK = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsEditGK = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsEditGK = new System.Windows.Forms.ToolStripMenuItem();
            this.tsDelGK = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsWG = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsWG = new System.Windows.Forms.ToolStripMenuItem();
            this.cmsQJ = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsQJ = new System.Windows.Forms.ToolStripMenuItem();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.treeView1 = new System.Windows.Forms.TreeView();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.showGKBtn = new System.Windows.Forms.CheckBox();
            this.meshDisControl1 = new MeshDisLib.MeshDisControl();
            this.menuStrip1.SuspendLayout();
            this.cmsAddGK.SuspendLayout();
            this.cmsEditGK.SuspendLayout();
            this.cmsWG.SuspendLayout();
            this.cmsQJ.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsFile,
            this.tsCalculate,
            this.configure,
            this.tsHelp});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(831, 25);
            this.menuStrip1.TabIndex = 0;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // tsFile
            // 
            this.tsFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsNew,
            this.tsOpen,
            this.tsSave,
            this.tsSaveAs,
            this.tsExit});
            this.tsFile.Image = global::AFA.Properties.Resources.documents;
            this.tsFile.Name = "tsFile";
            this.tsFile.Size = new System.Drawing.Size(60, 21);
            this.tsFile.Text = "文件";
            // 
            // tsNew
            // 
            this.tsNew.Image = global::AFA.Properties.Resources.folder_new;
            this.tsNew.Name = "tsNew";
            this.tsNew.Size = new System.Drawing.Size(152, 22);
            this.tsNew.Text = "新建";
            this.tsNew.Click += new System.EventHandler(this.tsNew_Click);
            // 
            // tsOpen
            // 
            this.tsOpen.Image = global::AFA.Properties.Resources.open_file;
            this.tsOpen.Name = "tsOpen";
            this.tsOpen.Size = new System.Drawing.Size(152, 22);
            this.tsOpen.Text = "打开";
            this.tsOpen.Click += new System.EventHandler(this.tsOpen_Click);
            // 
            // tsSave
            // 
            this.tsSave.Image = global::AFA.Properties.Resources.save;
            this.tsSave.Name = "tsSave";
            this.tsSave.Size = new System.Drawing.Size(152, 22);
            this.tsSave.Text = "保存";
            this.tsSave.Click += new System.EventHandler(this.tsSave_Click);
            // 
            // tsSaveAs
            // 
            this.tsSaveAs.Enabled = false;
            this.tsSaveAs.Name = "tsSaveAs";
            this.tsSaveAs.Size = new System.Drawing.Size(152, 22);
            this.tsSaveAs.Text = "另存为";
            this.tsSaveAs.Visible = false;
            this.tsSaveAs.Click += new System.EventHandler(this.tsSaveAs_Click);
            // 
            // tsExit
            // 
            this.tsExit.Image = global::AFA.Properties.Resources.logout;
            this.tsExit.Name = "tsExit";
            this.tsExit.Size = new System.Drawing.Size(152, 22);
            this.tsExit.Text = "退出";
            this.tsExit.Click += new System.EventHandler(this.tsExit_Click);
            // 
            // tsCalculate
            // 
            this.tsCalculate.Image = global::AFA.Properties.Resources.calculator__1_;
            this.tsCalculate.Name = "tsCalculate";
            this.tsCalculate.Size = new System.Drawing.Size(60, 21);
            this.tsCalculate.Text = "计算";
            this.tsCalculate.Click += new System.EventHandler(this.tsCalculate_Click);
            // 
            // configure
            // 
            this.configure.Image = global::AFA.Properties.Resources.icon_tools;
            this.configure.Name = "configure";
            this.configure.Size = new System.Drawing.Size(60, 21);
            this.configure.Text = "配置";
            this.configure.Click += new System.EventHandler(this.configure_Click);
            // 
            // tsHelp
            // 
            this.tsHelp.Image = global::AFA.Properties.Resources.help;
            this.tsHelp.Name = "tsHelp";
            this.tsHelp.Size = new System.Drawing.Size(60, 21);
            this.tsHelp.Text = "帮助";
            // 
            // cmsAddGK
            // 
            this.cmsAddGK.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsAddGK});
            this.cmsAddGK.Name = "cmsAddGK";
            this.cmsAddGK.Size = new System.Drawing.Size(125, 26);
            // 
            // tsAddGK
            // 
            this.tsAddGK.Name = "tsAddGK";
            this.tsAddGK.Size = new System.Drawing.Size(124, 22);
            this.tsAddGK.Text = "添加工况";
            this.tsAddGK.Click += new System.EventHandler(this.tsAddGK_Click);
            // 
            // cmsEditGK
            // 
            this.cmsEditGK.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsEditGK,
            this.tsDelGK});
            this.cmsEditGK.Name = "cmsEditGK";
            this.cmsEditGK.Size = new System.Drawing.Size(101, 48);
            // 
            // tsEditGK
            // 
            this.tsEditGK.Name = "tsEditGK";
            this.tsEditGK.Size = new System.Drawing.Size(100, 22);
            this.tsEditGK.Text = "编辑";
            this.tsEditGK.Click += new System.EventHandler(this.tsEditGK_Click);
            // 
            // tsDelGK
            // 
            this.tsDelGK.Name = "tsDelGK";
            this.tsDelGK.Size = new System.Drawing.Size(100, 22);
            this.tsDelGK.Text = "删除";
            this.tsDelGK.Click += new System.EventHandler(this.tsDelGK_Click);
            // 
            // cmsWG
            // 
            this.cmsWG.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsWG});
            this.cmsWG.Name = "cmsWG";
            this.cmsWG.Size = new System.Drawing.Size(125, 26);
            // 
            // tsWG
            // 
            this.tsWG.Name = "tsWG";
            this.tsWG.Size = new System.Drawing.Size(124, 22);
            this.tsWG.Text = "导入网格";
            this.tsWG.Click += new System.EventHandler(this.tsWG_Click);
            // 
            // cmsQJ
            // 
            this.cmsQJ.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsQJ});
            this.cmsQJ.Name = "cmsQJ";
            this.cmsQJ.Size = new System.Drawing.Size(101, 26);
            // 
            // tsQJ
            // 
            this.tsQJ.Name = "tsQJ";
            this.tsQJ.Size = new System.Drawing.Size(100, 22);
            this.tsQJ.Text = "设置";
            this.tsQJ.Click += new System.EventHandler(this.tsQJ_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 25);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.groupBox2);
            this.splitContainer1.Size = new System.Drawing.Size(831, 445);
            this.splitContainer1.SplitterDistance = 144;
            this.splitContainer1.TabIndex = 4;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.treeView1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(144, 445);
            this.groupBox1.TabIndex = 4;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "计算数据";
            // 
            // treeView1
            // 
            this.treeView1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.treeView1.Location = new System.Drawing.Point(3, 17);
            this.treeView1.Name = "treeView1";
            this.treeView1.Size = new System.Drawing.Size(138, 425);
            this.treeView1.TabIndex = 0;
            this.treeView1.NodeMouseClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseClick);
            this.treeView1.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeView1_NodeMouseDoubleClick);
            this.treeView1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.treeView1_MouseDown);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox2.Location = new System.Drawing.Point(0, 0);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(683, 445);
            this.groupBox2.TabIndex = 4;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "工作区";
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 17);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(677, 425);
            this.tabControl1.TabIndex = 0;
            this.tabControl1.ControlRemoved += new System.Windows.Forms.ControlEventHandler(this.tabControl1_ControlRemoved);
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.showGKBtn);
            this.tabPage1.Controls.Add(this.meshDisControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(669, 399);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "网格显示";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // showGKBtn
            // 
            this.showGKBtn.AutoSize = true;
            this.showGKBtn.BackColor = System.Drawing.Color.Transparent;
            this.showGKBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.showGKBtn.Checked = true;
            this.showGKBtn.CheckState = System.Windows.Forms.CheckState.Checked;
            this.showGKBtn.Enabled = false;
            this.showGKBtn.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.showGKBtn.FlatAppearance.CheckedBackColor = System.Drawing.Color.Transparent;
            this.showGKBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.Transparent;
            this.showGKBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Transparent;
            this.showGKBtn.ForeColor = System.Drawing.Color.Black;
            this.showGKBtn.Location = new System.Drawing.Point(6, 6);
            this.showGKBtn.Name = "showGKBtn";
            this.showGKBtn.Size = new System.Drawing.Size(120, 16);
            this.showGKBtn.TabIndex = 1;
            this.showGKBtn.Text = "是否显示远场边界";
            this.showGKBtn.UseVisualStyleBackColor = false;
            this.showGKBtn.CheckedChanged += new System.EventHandler(this.showGKBtn_CheckedChanged);
            // 
            // meshDisControl1
            // 
            this.meshDisControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.meshDisControl1.Location = new System.Drawing.Point(3, 3);
            this.meshDisControl1.Name = "meshDisControl1";
            this.meshDisControl1.Size = new System.Drawing.Size(663, 393);
            this.meshDisControl1.TabIndex = 0;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(831, 470);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "MainForm";
            this.Text = "全机气动分析";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.cmsAddGK.ResumeLayout(false);
            this.cmsEditGK.ResumeLayout(false);
            this.cmsWG.ResumeLayout(false);
            this.cmsQJ.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsFile;
        private System.Windows.Forms.ToolStripMenuItem tsCalculate;
        private System.Windows.Forms.ToolStripMenuItem tsHelp;
        private System.Windows.Forms.ToolStripMenuItem tsNew;
        private System.Windows.Forms.ToolStripMenuItem tsOpen;
        private System.Windows.Forms.ToolStripMenuItem tsSave;
        private System.Windows.Forms.ToolStripMenuItem tsSaveAs;
        private System.Windows.Forms.ToolStripMenuItem tsExit;
        private System.Windows.Forms.ContextMenuStrip cmsAddGK;
        private System.Windows.Forms.ToolStripMenuItem tsAddGK;
        private System.Windows.Forms.ContextMenuStrip cmsEditGK;
        private System.Windows.Forms.ToolStripMenuItem tsEditGK;
        private System.Windows.Forms.ToolStripMenuItem tsDelGK;
        private System.Windows.Forms.ContextMenuStrip cmsWG;
        private System.Windows.Forms.ToolStripMenuItem tsWG;
        private System.Windows.Forms.ContextMenuStrip cmsQJ;
        private System.Windows.Forms.ToolStripMenuItem tsQJ;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.TreeView treeView1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private MeshDisLib.MeshDisControl meshDisControl1;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ToolStripMenuItem configure;
        private System.Windows.Forms.CheckBox showGKBtn;
    }
}

