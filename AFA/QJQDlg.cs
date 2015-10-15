using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AFA
{
    public partial class QJQDlg : Form
    {
        public QJQDlg()
        {
            InitializeComponent();
        }

        private void QJQDlg_Load(object sender, EventArgs e)
        {
            this.tbCFL.Text = Common.CFL;
            this.tbITMAX.Text = Common.ITMAX;
            this.tbSAVE_STEP.Text = Common.SAVE_STEP;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Common.TestDoubleData(this.tbCFL.Text))
            {
                MessageBox.Show("CFL数中请输入整数.");
                return;
            }
            if (!Common.TestDoubleData(this.tbITMAX.Text))
            {
                MessageBox.Show("最大迭代步数中请输入整数.");
                return;
            }
            if (!Common.TestDoubleData(this.tbSAVE_STEP.Text))
            {
                MessageBox.Show("保存间隔步数中请输入整数.");
                return;
            }
            Common.CFL = this.tbCFL.Text;
            Common.ITMAX = this.tbITMAX.Text;
            Common.SAVE_STEP = this.tbSAVE_STEP.Text;

            this.DialogResult = DialogResult.OK;
            //Common.SaveToCFDIN(MainForm.GKNode.Nodes);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
