namespace AFA
{
    partial class DisGKResult
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.zedGraphControl1 = new ZedGraph.ZedGraphControl();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.publishBtn = new System.Windows.Forms.Button();
            this.extProcessBtn = new System.Windows.Forms.Button();
            this.stopCalculateBtn = new System.Windows.Forms.Button();
            this.panel2 = new System.Windows.Forms.Panel();
            this.iterateNum = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.xyllTxt = new System.Windows.Forms.Label();
            this.jszlTxt = new System.Windows.Forms.Label();
            this.jsslTxt = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.mdcyTxt = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbDisType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.labGK = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.zedGraphControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(786, 552);
            this.tableLayoutPanel1.TabIndex = 6;
            // 
            // zedGraphControl1
            // 
            this.zedGraphControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.zedGraphControl1.Location = new System.Drawing.Point(3, 129);
            this.zedGraphControl1.Name = "zedGraphControl1";
            this.zedGraphControl1.ScrollGrace = 0D;
            this.zedGraphControl1.ScrollMaxX = 0D;
            this.zedGraphControl1.ScrollMaxY = 0D;
            this.zedGraphControl1.ScrollMaxY2 = 0D;
            this.zedGraphControl1.ScrollMinX = 0D;
            this.zedGraphControl1.ScrollMinY = 0D;
            this.zedGraphControl1.ScrollMinY2 = 0D;
            this.zedGraphControl1.Size = new System.Drawing.Size(780, 420);
            this.zedGraphControl1.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 87.05128F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 12.94872F));
            this.tableLayoutPanel2.Controls.Add(this.panel1, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.panel2, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(780, 120);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.publishBtn);
            this.panel1.Controls.Add(this.extProcessBtn);
            this.panel1.Controls.Add(this.stopCalculateBtn);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(681, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(96, 114);
            this.panel1.TabIndex = 0;
            // 
            // publishBtn
            // 
            this.publishBtn.Location = new System.Drawing.Point(5, 74);
            this.publishBtn.Name = "publishBtn";
            this.publishBtn.Size = new System.Drawing.Size(75, 23);
            this.publishBtn.TabIndex = 11;
            this.publishBtn.Text = "发布结果";
            this.publishBtn.UseVisualStyleBackColor = true;
            this.publishBtn.Click += new System.EventHandler(this.publishBtn_Click);
            // 
            // extProcessBtn
            // 
            this.extProcessBtn.Location = new System.Drawing.Point(5, 40);
            this.extProcessBtn.Name = "extProcessBtn";
            this.extProcessBtn.Size = new System.Drawing.Size(75, 23);
            this.extProcessBtn.TabIndex = 10;
            this.extProcessBtn.Text = "后处理";
            this.extProcessBtn.UseVisualStyleBackColor = true;
            this.extProcessBtn.Click += new System.EventHandler(this.extProcessBtn_Click);
            // 
            // stopCalculateBtn
            // 
            this.stopCalculateBtn.Location = new System.Drawing.Point(5, 6);
            this.stopCalculateBtn.Name = "stopCalculateBtn";
            this.stopCalculateBtn.Size = new System.Drawing.Size(75, 23);
            this.stopCalculateBtn.TabIndex = 9;
            this.stopCalculateBtn.Text = "停止计算";
            this.stopCalculateBtn.UseVisualStyleBackColor = true;
            this.stopCalculateBtn.Click += new System.EventHandler(this.stopCalculateBtn_Click);
            // 
            // panel2
            // 
            this.panel2.Controls.Add(this.iterateNum);
            this.panel2.Controls.Add(this.label9);
            this.panel2.Controls.Add(this.label7);
            this.panel2.Controls.Add(this.label6);
            this.panel2.Controls.Add(this.xyllTxt);
            this.panel2.Controls.Add(this.jszlTxt);
            this.panel2.Controls.Add(this.jsslTxt);
            this.panel2.Controls.Add(this.label13);
            this.panel2.Controls.Add(this.label12);
            this.panel2.Controls.Add(this.label8);
            this.panel2.Controls.Add(this.mdcyTxt);
            this.panel2.Controls.Add(this.label5);
            this.panel2.Controls.Add(this.label4);
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.cmbDisType);
            this.panel2.Controls.Add(this.label3);
            this.panel2.Controls.Add(this.labGK);
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(3, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(672, 114);
            this.panel2.TabIndex = 1;
            // 
            // iterateNum
            // 
            this.iterateNum.AutoSize = true;
            this.iterateNum.Location = new System.Drawing.Point(98, 34);
            this.iterateNum.Name = "iterateNum";
            this.iterateNum.Size = new System.Drawing.Size(11, 12);
            this.iterateNum.TabIndex = 29;
            this.iterateNum.Text = "0";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(15, 34);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(83, 12);
            this.label9.TabIndex = 28;
            this.label9.Text = "当前迭代步数:";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(506, 36);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(11, 12);
            this.label7.TabIndex = 27;
            this.label7.Text = "0";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(503, 11);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(11, 12);
            this.label6.TabIndex = 26;
            this.label6.Text = "0";
            // 
            // xyllTxt
            // 
            this.xyllTxt.AutoSize = true;
            this.xyllTxt.Location = new System.Drawing.Point(284, 79);
            this.xyllTxt.Name = "xyllTxt";
            this.xyllTxt.Size = new System.Drawing.Size(11, 12);
            this.xyllTxt.TabIndex = 25;
            this.xyllTxt.Text = "0";
            // 
            // jszlTxt
            // 
            this.jszlTxt.AutoSize = true;
            this.jszlTxt.Location = new System.Drawing.Point(284, 54);
            this.jszlTxt.Name = "jszlTxt";
            this.jszlTxt.Size = new System.Drawing.Size(11, 12);
            this.jszlTxt.TabIndex = 21;
            this.jszlTxt.Text = "0";
            // 
            // jsslTxt
            // 
            this.jsslTxt.AutoSize = true;
            this.jsslTxt.Location = new System.Drawing.Point(282, 34);
            this.jsslTxt.Name = "jsslTxt";
            this.jsslTxt.Size = new System.Drawing.Size(11, 12);
            this.jsslTxt.TabIndex = 20;
            this.jsslTxt.Text = "0";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(384, 36);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(113, 12);
            this.label13.TabIndex = 18;
            this.label13.Text = "进气道总压恢复系数";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(384, 11);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(113, 12);
            this.label12.TabIndex = 17;
            this.label12.Text = "进气道压力畸变系数";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(223, 79);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(53, 12);
            this.label8.TabIndex = 13;
            this.label8.Text = "旋翼拉力";
            // 
            // mdcyTxt
            // 
            this.mdcyTxt.AutoSize = true;
            this.mdcyTxt.Location = new System.Drawing.Point(282, 11);
            this.mdcyTxt.Name = "mdcyTxt";
            this.mdcyTxt.Size = new System.Drawing.Size(11, 12);
            this.mdcyTxt.TabIndex = 11;
            this.mdcyTxt.Text = "0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(223, 55);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 9;
            this.label5.Text = "机身阻力";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(223, 34);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 8;
            this.label4.Text = "机身升力";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(223, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 7;
            this.label2.Text = "密度残值";
            // 
            // cmbDisType
            // 
            this.cmbDisType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDisType.FormattingEnabled = true;
            this.cmbDisType.Items.AddRange(new object[] {
            "密度残值",
            "机身升力",
            "机身阻力",
            "旋翼拉力",
            "进气道压力畸变系数",
            "进气道总压恢复系数"});
            this.cmbDisType.Location = new System.Drawing.Point(84, 52);
            this.cmbDisType.Name = "cmbDisType";
            this.cmbDisType.Size = new System.Drawing.Size(121, 20);
            this.cmbDisType.TabIndex = 6;
            this.cmbDisType.SelectedValueChanged += new System.EventHandler(this.cmbDisType_SelectedValueChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(19, 55);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(59, 12);
            this.label3.TabIndex = 3;
            this.label3.Text = "显示选择:";
            // 
            // labGK
            // 
            this.labGK.AutoSize = true;
            this.labGK.Location = new System.Drawing.Point(98, 11);
            this.labGK.Name = "labGK";
            this.labGK.Size = new System.Drawing.Size(29, 12);
            this.labGK.TabIndex = 4;
            this.labGK.Text = "工况";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "计算工况:";
            // 
            // DisGKResult
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DisGKResult";
            this.Size = new System.Drawing.Size(786, 552);
            this.Load += new System.EventHandler(this.DisGKResult_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private ZedGraph.ZedGraphControl zedGraphControl1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button publishBtn;
        private System.Windows.Forms.Button extProcessBtn;
        private System.Windows.Forms.Button stopCalculateBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label xyllTxt;
        private System.Windows.Forms.Label jszlTxt;
        private System.Windows.Forms.Label jsslTxt;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label mdcyTxt;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cmbDisType;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labGK;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label iterateNum;
        private System.Windows.Forms.Label label9;

    }
}
