using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace AFA
{
    public partial class ConfigureForm : Form
    {
        public ConfigureForm()
        {
            InitializeComponent();
        }

        private void cancelBtn_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void confirmBtn_Click(object sender, EventArgs e)
        {
            string txt = this.filePathTxt.Text;
            if (txt == null || txt == "")
            {
                MessageBox.Show("输入tecplot360 工作目录");
                return;
            }
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationElement element = config.AppSettings.Settings["tecplot360_workDir"];
            if (element == null)
            {
                //appSetting = new ConnectionStringSettings("WDMConnectionString", connectString);
                config.AppSettings.Settings.Add("tecplot360_workDir", txt);
                //config.ConnectionStrings.ConnectionStrings.Add(appSetting);
            }
            else
            {
                config.AppSettings.Settings["tecplot360_workDir"].Value = txt;
            }
            //appSetting.ConnectionString = connectString;
            config.Save(ConfigurationSaveMode.Modified, true);// 重新载入配置文件的配置节
            ConfigurationManager.RefreshSection("appSettings");

            ((MainForm)this.Owner).tecplot360WorkDir = txt;
            this.Close();
        }

        private void browsFileBtn_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Title = "选择Tecplot360目录";
            dlg.InitialDirectory = Common.yxFolder;
            dlg.Filter = "Tecplot360文件(*.exe)|*.exe";
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                this.filePathTxt.Text = dlg.FileName;
            }
        }

        private void ConfigureForm_Load(object sender, EventArgs e)
        {
            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationElement element = config.AppSettings.Settings["tecplot360_workDir"];
            if (element != null)
            {
                this.filePathTxt.Text=config.AppSettings.Settings["tecplot360_workDir"].Value;
            }
        }
    }
}
