namespace AFA
{
    partial class CalculateLoadingForm
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
            this.dataText = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dataText
            // 
            this.dataText.AutoSize = true;
            this.dataText.Location = new System.Drawing.Point(12, 9);
            this.dataText.Name = "dataText";
            this.dataText.Size = new System.Drawing.Size(23, 12);
            this.dataText.TabIndex = 0;
            this.dataText.Text = "...";
            // 
            // CalculateLoadingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(532, 116);
            this.Controls.Add(this.dataText);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CalculateLoadingForm";
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "转换网格";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.CalculateLoadingForm_FormClosed);
            this.Load += new System.EventHandler(this.LoadingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label dataText;
    }
}