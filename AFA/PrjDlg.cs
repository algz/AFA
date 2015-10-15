using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace AFA
{
    public partial class PrjDlg : Form
    {
        public string m_strPrj = string.Empty;
        public PrjDlg()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            string strPrjName = Common.startupFolder + this.tbPrjName.Text;
            if (this.tbPrjName.Text.IndexOfAny(System.IO.Path.GetInvalidFileNameChars()) >= 0)
            {
                MessageBox.Show(this, "文件夹非法字符");
                return;
            }
            //string regexstr = @"^[A-Za-z]+$";
            //System.Text.RegularExpressions.Regex regex = new System.Text.RegularExpressions.Regex(regexstr, System.Text.RegularExpressions.RegexOptions.IgnoreCase);
            //System.Text.RegularExpressions.Match m = regex.Match(strPrjName);
            //if (!m.Success)
            //{
            //    MessageBox.Show(this, "请输入英文字符");
            //    return;
            //}
            if (Directory.Exists(strPrjName))
            {
                MessageBox.Show("项目名" + strPrjName + "已存在.");
                return;
            }
            else
            {
                try
                {
                    Directory.CreateDirectory(strPrjName);
                    m_strPrj = strPrjName;
                    Common.prjName = m_strPrj;
                    this.DialogResult = DialogResult.OK;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                    return;
                }
                
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
