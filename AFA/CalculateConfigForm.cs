using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Diagnostics;
using System.IO;

namespace AFA
{
    public partial class CalculateConfigForm : Form
    {
        public List<TreeNode> selGKNode = new List<TreeNode>();

        public CalculateConfigForm()
        {
            InitializeComponent();
        }

        private void CalculateConfigForm_Load(object sender, EventArgs e)
        {
            this.gkChkListBox.DisplayMember = "text";
            this.gkChkListBox.CheckOnClick = true;
            //this.gkChkListBox.ValueMember = "text";

            foreach(TreeNode node in MainForm.GKNode.Nodes)
            {
                bool bl = false;
                foreach (string key in MainForm.rotor_Process.Keys)
                {
                    //Process pro = MainForm.rotor_Process[key];
                    //string rotoPath = Common.prjName + Path.DirectorySeparatorChar + node.Text + Path.DirectorySeparatorChar;
                    if (key == node.Text && !MainForm.rotor_Process[key].HasExited)//(pro.StartInfo.WorkingDirectory == rotoPath&&!pro.HasExited)
                    {
                        bl = true;
                    }
                }
                this.gkChkListBox.Items.Add(node, bl);
            }
        }

        private void confirmBtn_Click(object sender, EventArgs e)
        {
            //foreach (TreeNode node in this.gkChkListBox.Items)
            //{
            //    this.selGKNode.Add(node);
            //}
            //this.selGKNode = this.gkChkListBox.SelectedItems;
            //if (this.selGKNode.Count==0)
            //{
            //    MessageBox.Show("请选择运行求解器的工况");
            //    return;
            //}
            this.Close();
        }

        private void gkChkListBox_ItemCheck(object sender, ItemCheckEventArgs e)
        {
            
            TreeNode node = (TreeNode)this.gkChkListBox.Items[e.Index];

            foreach (string key in MainForm.rotor_Process.Keys)
            {
                //Process pro = MainForm.rotor_Process[key];
                //string rotoPath = Common.prjName + Path.DirectorySeparatorChar + node.Text + Path.DirectorySeparatorChar;
                if (node.Text == node.Text && !MainForm.rotor_Process[key].HasExited)
                {
                    //MessageBox.Show("工况运行中不能修改.");
                    e.NewValue = CheckState.Checked;
                    ////this.gkChkListBox.SetItemCheckState(e.Index, CheckState.Checked);
                    return;
                }
            }

            if (e.CurrentValue == CheckState.Checked)
            {
                foreach (string key in MainForm.rotor_Process.Keys)
                {
                    //string rotoPath = Common.prjName + Path.DirectorySeparatorChar + node.Text + Path.DirectorySeparatorChar;
                    if (key == node.Text && !MainForm.rotor_Process[key].HasExited)
                    {
                        MessageBox.Show("工况运行中不能修改.");
                        e.NewValue = CheckState.Checked;
                        //this.gkChkListBox.SetItemCheckState(e.Index, CheckState.Checked);
                        return;
                    }
                }
            }

            if (e.NewValue == CheckState.Checked)
            {
                this.selGKNode.Add(node);
            }
            else
            {
                this.selGKNode.Remove(node);
            }
        }

        private void cancleBtn_Click(object sender, EventArgs e)
        {
            this.selGKNode =new List<TreeNode>();
            this.Close();
        }
    }
}
