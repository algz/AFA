using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace AFA
{
    public partial class YXDlg : Form
    {
        public string m_startR = string.Empty;
        public string m_endR = string.Empty;
        public string m_YXpath = string.Empty;
        public YXDlg()
        {
            InitializeComponent();
        }

        public YXDlg(string m_startR,string m_endR,string m_YXpath)
        {
            InitializeComponent();

            this.tbStartR.Text = m_startR;
            this.tbEndR.Text = m_endR;
            this.tbYXPath.Text = m_YXpath;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!Common.TestDoubleData(this.tbStartR.Text))
            {
                MessageBox.Show("起始半径中请输入实数.");
                return;
            }
            if (!Common.TestDoubleData(this.tbEndR.Text))
            {
                MessageBox.Show("终止半径中请输入实数.");
                return;
            }
            if (this.tbYXPath.Text.Trim().Length==0)
            {
                MessageBox.Show("请导入翼型数据文件.");
                return;
            }

            m_startR = this.tbStartR.Text;
            m_endR = this.tbEndR.Text;
            //m_YXpath = this.tbYXPath.Text;

            m_YXpath = Path.GetFileName(this.tbYXPath.Text);// m_YXpath.Substring(m_YXpath.LastIndexOf("\\") + 1);
            this.DialogResult = DialogResult.OK;
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "导入翼型数据文件";
            dlg.InitialDirectory = Common.yxFolder;
            dlg.Filter = "翼型数据文件(*.dat)|*.dat";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.tbYXPath.Text = dlg.FileName;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
