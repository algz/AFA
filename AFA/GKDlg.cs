﻿using System;
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
    public partial class GKDlg : Form
    {

        private MainForm mainForm;
        private bool m_bEdit = false;
        public sGK _Data = new sGK();
        public sGK m_Data
        {
            get
            {
                return _Data; 
            }
            set
            {
                _Data = value;
            }
        }
        private AFATreeNode m_Node;
        private string m_oldName = string.Empty;

        /// <summary>
        /// 新建工况
        /// </summary>
        /// <param name="bEdit"></param>
        public GKDlg(bool bEdit)
        {
            m_bEdit = bEdit;
            InitializeComponent();

            this.cmbINVERSE.SelectedIndex = 0;
            this.cmbDISKAK.SelectedIndex = 0;
            this.btnEditXY.Enabled = false;
            this.btnDelXY.Enabled = false;

            this.chkBLCONT.Enabled = false;
        }

        /// <summary>
        /// 修改工况
        /// </summary>
        /// <param name="bEdit"></param>
        /// <param name="m_Node"></param>
        public GKDlg(bool bEdit, AFATreeNode m_Node)
        {
            m_bEdit = bEdit;
            InitializeComponent();

            
            this.cmbINVERSE.SelectedIndex = 0;
            this.cmbDISKAK.SelectedIndex = 0;
            this.btnEditXY.Enabled = false;
            this.btnDelXY.Enabled = false;

            this.m_Node = m_Node;


        }

        /// <summary>
        /// 检测旋翼数据,在添加旋翼及修改旋翼时调用
        /// </summary>
        /// <returns></returns>
        private bool checkXY()
        {
            if (!Common.TestDoubleData(this.tbRADIUS.Text))
            {
                MessageBox.Show("旋翼半径中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbRADIUSC.Text))
            {
                MessageBox.Show("旋翼根切中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbHDISK.Text))
            {
                MessageBox.Show("桨盘厚度中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbHDISK.Text))
            {
                MessageBox.Show("桨盘厚度中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbCHORD.Text))
            {
                MessageBox.Show("桨叶弦长中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbN_BLADE.Text))
            {
                MessageBox.Show("桨叶片数中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbOMIGA.Text))
            {
                MessageBox.Show("转速中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbMU.Text))
            {
                MessageBox.Show("前进比中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbTWSIT.Text))
            {
                MessageBox.Show("桨叶扭转角中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbALF_TPP.Text))
            {
                MessageBox.Show("倾倒角度中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbDISKX.Text))
            {
                MessageBox.Show("旋翼中心坐标X中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbDISKY.Text))
            {
                MessageBox.Show("旋翼中心坐标Y中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbDISKZ.Text))
            {
                MessageBox.Show("旋翼中心坐标Z中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbPITCH_0.Text))
            {
                MessageBox.Show("旋翼操纵TO中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbPITCH_0.Text))
            {
                MessageBox.Show("旋翼操纵TO中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbPITCH_C.Text))
            {
                MessageBox.Show("旋翼操纵T1C中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbPITCH_S.Text))
            {
                MessageBox.Show("旋翼操纵T1S中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbFLAP_0.Text))
            {
                MessageBox.Show("旋翼挥舞BO中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbFLAP_0.Text))
            {
                MessageBox.Show("旋翼挥舞BO中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbFLAP_C1.Text))
            {
                MessageBox.Show("旋翼挥舞B1C中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbFLAP_S1.Text))
            {
                MessageBox.Show("旋翼挥舞B1S中请输入实数.");
                return false;
            }

            //if (this.dgvYX.Rows.Count==0)
            //{
            //     MessageBox.Show("请添加翼型.");
            //    return false;
            //}
            return true;
        }

        private bool checkData()
        {
            if (this.tbName.Text.Trim().Length == 0)
            {
                MessageBox.Show("请输入工况名称.");
                return false;
            }


            if (m_bEdit)
            {
                if (string.Compare(m_oldName, this.tbName.Text.Trim()) != 0)
                {
                    if (!MainForm.CheckNodeName(this.tbName.Text.Trim()))
                    {
                        this.tbName.Focus();
                        this.tbName.SelectAll();
                        return false;
                    }
                }
            }
            else
            {
                if (!MainForm.CheckNodeName(this.tbName.Text.Trim()))
                {
                    this.tbName.Focus();
                    this.tbName.SelectAll();
                    return false;
                }
            }

            if (!Common.TestDoubleData(this.tbMACH_INF.Text))
            {
                MessageBox.Show("来流马赫数中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbRE.Text))
            {
                MessageBox.Show("雷洛数中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbFUX.Text))
            {
                MessageBox.Show("机身合力作用点X中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbFUY.Text))
            {
                MessageBox.Show("机身合力作用点Y中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbFUZ.Text))
            {
                MessageBox.Show("机身合力作用点Z中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbPITCH_V.Text))
            {
                MessageBox.Show("攻角中请输入实数.");
                return false;
            }
            if (!Common.TestDoubleData(this.tbYAW_V.Text))
            {
                MessageBox.Show("偏航角中请输入实数.");
                return false;
            }
            //if (!Common.TestDoubleData(this.tbFLOWRATE.Text))
            //{
            //    MessageBox.Show("发动机质量流量中请输入实数.");
            //    return false;
            //}
            if (!Common.TestDoubleData(this.tbZMU.Text))
            {
                MessageBox.Show("主旋翼前进比中请输入实数.");
                return false;
            }
            if (this.chkModelXY.Checked && this.XYlistView.Items.Count == 0)
            {
                MessageBox.Show("请添加旋翼.");
                return false;
            }
            else
            {
                foreach (ListViewItem item in this.XYlistView.Items)
                {
                    sXY xy=(sXY)item.Tag;
                    if (xy.BLADE == "")
                    {
                        MessageBox.Show("请添加'"+item.Text+"'的翼型数据.");
                        return false;
                    }
                    
                    
                }
            }
            return true;
        }

        public void SetData(ref AFATreeNode selNode)
        {
            if (selNode == null)
            {
                return;
            }
            m_Node = selNode;
            _Data = (sGK)selNode.Tag;
            m_oldName = _Data.Name;
            this.tbName.Text = _Data.Name;
            if (_Data.MODEL == "1")
            {
                this.chkModelXY.Checked = false;
                this.chkModelJQD.Checked = false;
            }
            if (_Data.MODEL == "11")
            {
                this.chkModelXY.Checked = true;
                this.chkModelJQD.Checked = false;
            }
            if (_Data.MODEL == "101")
            {
                this.chkModelXY.Checked = false;
                this.chkModelJQD.Checked = true;
            }
            if (_Data.MODEL == "111")
            {
                this.chkModelXY.Checked = true;
                this.chkModelJQD.Checked = true;
            }
            if (_Data.BLCONT == "1")
            {
                this.chkBLCONT.Checked = true;
            }
            else
            {
                this.chkBLCONT.Checked = false;
            }
            this.tbMACH_INF.Text = _Data.MACH_INF;
            this.tbRE.Text = _Data.RE;
            this.tbFUX.Text = _Data.FUX;
            this.tbFUY.Text = _Data.FUY;
            this.tbFUZ.Text = _Data.FUZ;
            this.tbPITCH_V.Text = _Data.PITCH_V;
            this.tbYAW_V.Text = _Data.YAW_V;
            //this.tbFLOWRATE.Text = _Data.FLOWRATE;
            this.tbZMU.Text = _Data.ZMU;
            string strXYName = string.Empty;
            for (int i = 0; i < _Data.NDISK; i++)
            {
                strXYName = "旋翼" + i.ToString();
                ListViewItem item=new ListViewItem();
                item.Text=strXYName;
                //node = new TreeNode(strXYName);

                sXY dataXY = new sXY();
                dataXY.ALF_TPP = _Data.XYData[i].ALF_TPP;
                dataXY.CHORD = _Data.XYData[i].CHORD;
                dataXY.DISKAK = _Data.XYData[i].DISKAK;
                dataXY.DISKX = _Data.XYData[i].DISKX;
                dataXY.DISKY = _Data.XYData[i].DISKY;
                dataXY.DISKZ = _Data.XYData[i].DISKZ;
                dataXY.FLAP_0 = _Data.XYData[i].FLAP_0;
                dataXY.FLAP_C1 = _Data.XYData[i].FLAP_C1;
                dataXY.FLAP_S1 = _Data.XYData[i].FLAP_S1;
                dataXY.HDISK = _Data.XYData[i].HDISK;
                dataXY.INVERSE = _Data.XYData[i].INVERSE;
                dataXY.MU = _Data.XYData[i].MU;
                dataXY.N_BLADE = _Data.XYData[i].N_BLADE;
                dataXY.OMIGA = _Data.XYData[i].OMIGA;
                dataXY.PITCH_0 = _Data.XYData[i].PITCH_0;
                dataXY.PITCH_C = _Data.XYData[i].PITCH_C;
                dataXY.PITCH_S = _Data.XYData[i].PITCH_S;
                dataXY.RADIUS = _Data.XYData[i].RADIUS;
                dataXY.RADIUSC = _Data.XYData[i].RADIUSC;
                dataXY.TWSIT = _Data.XYData[i].TWSIT;
                dataXY.BLADE = _Data.XYData[i].BLADE;

                item.Tag = dataXY;
                //this.treeViewXY.Nodes.Add(node);
                this.XYlistView.Items.Add(item);
                if (i == 0)
                {
                    //默认加载首结点
                    //this.treeViewXY.SelectedNode = node;
                    item.Selected=true;
                    this.loadYXGridFormNodeTag((sXY)item.Tag);
                }
            }

            #region 进气道
            this.MASSOUT_RATE.Text = _Data.inlet.MASSOUT_RATE;
            this.TEMP_OUT0.Text = _Data.inlet.TEMP_OUT0;

            #endregion

            bool flag = true;
            if (this.XYlistView.Items.Count == 0)
            {
                flag = false;
            }
            this.btnEditXY.Enabled = flag;
            this.btnDelXY.Enabled = flag;
            this.gbYSJ.Enabled = flag;
            this.groupBox1.Enabled = flag;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (!checkData())
            {
                return;
            }

            #region 存储数据
            

            _Data.Name = this.tbName.Text;
            if (!this.chkModelXY.Checked && !this.chkModelJQD.Checked)
            {
                _Data.MODEL = "1"; 
            }
            if (this.chkModelXY.Checked && !this.chkModelJQD.Checked)
            {
                _Data.MODEL = "11";
            }
            if (!this.chkModelXY.Checked && this.chkModelJQD.Checked)
            {
                _Data.MODEL = "101";
            }
            if (this.chkModelXY.Checked && this.chkModelJQD.Checked)
            {
                _Data.MODEL = "111";
            }
            if (this.chkBLCONT.Checked)
            {
                _Data.BLCONT = "1";
            }
            else
            {
                _Data.BLCONT = "0";
            }
            _Data.MACH_INF = this.tbMACH_INF.Text;
            _Data.RE = this.tbRE.Text;
            _Data.FUX = this.tbFUX.Text;
            _Data.FUY = this.tbFUY.Text;
            _Data.FUZ = this.tbFUZ.Text;
            _Data.PITCH_V = this.tbPITCH_V.Text;
            _Data.YAW_V = this.tbYAW_V.Text;
            //_Data.FLOWRATE = this.tbFLOWRATE.Text;
            _Data.ZMU = this.tbZMU.Text;

            btnEditXY_Click(null, null);

            _Data.NDISK = this.XYlistView.Items.Count;// this.treeViewXY.Nodes.Count;
            _Data.XYData = new sXY[_Data.NDISK];
            for (int i = 0; i < _Data.NDISK; i++)
            {
                _Data.XYData[i] = (sXY)this.XYlistView.Items[i].Tag;// this.treeViewXY.Nodes[i].Tag; ;
            }

            #region 进气道
            _Data.inlet.MASSOUT_RATE = this.MASSOUT_RATE.Text;
            _Data.inlet.TEMP_OUT0=this.TEMP_OUT0.Text;
            #endregion

            #endregion

            string originDir = (_Data.Name == null ? null : (Common.prjName + Path.DirectorySeparatorChar + _Data.Name));
            string destDir = Common.prjName + Path.DirectorySeparatorChar + this.tbName.Text;

            //检查工况名是否重复
            if (originDir != destDir && Directory.Exists(destDir))
            {
                MessageBox.Show("工况名重复");
                return;
            }

            if (originDir != destDir)
            {
                if (originDir != null && Directory.Exists(originDir))
                {
                    Directory.Move(originDir, destDir);
                    TabPage page = ((TabControl)this.mainForm.Controls.Find("tabControl1", true)[0]).TabPages[originDir.Split('\\').Last()];
                    if (page != null)
                    {
                        page.Controls.Find("labGK",true)[0].Text=page.Name = page.Text = this.tbName.Text;
                    }

                    m_Node.Tag = _Data;
                    m_Node.Name = m_Node.Text = this.tbName.Text; ;
                    this.mainForm.SaveFile(Common.prjName + Path.DirectorySeparatorChar + "str.prj");
                }
                else
                {
                    //工况文件夹不存在则创建文件夹
                    Directory.CreateDirectory(destDir);
                }
            }


            if (m_bEdit)
            {

                m_Node.Tag = _Data;
                m_Node.Name = m_Node.Text = _Data.Name;
            }

            Button btn = (Button)sender;
            if (btn.Name == "btnOK")
            {
                this.DialogResult = DialogResult.OK;
            }
            
            //Common.SaveToCFDIN(MainForm.GKNode.Nodes);

            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;

        }

        private void btnAddYX_Click(object sender, EventArgs e)
        {
            if (this.XYlistView.SelectedItems.Count == 0)
            {
                MessageBox.Show("请选择翼型.");
                return;
            }
            YXDlg dlg = new YXDlg();
            if (dlg.ShowDialog()==DialogResult.OK)
            {
                this.dgvYX.Rows.Add();
                int iRow = this.dgvYX.Rows.Count - 1;
                this.dgvYX.Rows[iRow].Cells[0].Value = dlg.m_startR;
                this.dgvYX.Rows[iRow].Cells[1].Value = dlg.m_endR;
                this.dgvYX.Rows[iRow].Cells[2].Value = dlg.m_YXpath;

                sXY xy=(sXY)this.XYlistView.SelectedItems[0].Tag;
                xy.BLADE="#"+dlg.m_startR+"#"+dlg.m_endR+"#"+dlg.m_YXpath+"#";
                this.XYlistView.SelectedItems[0].Tag = xy;
            }
        }

        private void btnAddXY_Click(object sender, EventArgs e)
        {
            if (!checkXY())
            {
                return;
            }

            ListViewItem item = new ListViewItem();
            //item.BackColor = Color.LightSteelBlue;
            string name = "0";
            if (this.XYlistView.Items.Count != 0)
            {
                name = this.XYlistView.Items[this.XYlistView.Items.Count - 1].Text;
                name = (Convert.ToInt32(name.Substring(name.Length - 1)) + 1).ToString();
            }
            item.Text = "旋翼" + name;// this.XYlistView.Items.Count;
            

            sXY dataXY = new sXY();
            dataXY.ALF_TPP = this.tbALF_TPP.Text;
            dataXY.CHORD = this.tbCHORD.Text;
            dataXY.DISKAK = Common.cmbdiskToDiskak(this.cmbDISKAK.SelectedIndex);
            
            
            dataXY.DISKX = this.tbDISKX.Text;
            dataXY.DISKY = this.tbDISKY.Text;
            dataXY.DISKZ = this.tbDISKZ.Text;
            dataXY.FLAP_0 = this.tbFLAP_0.Text;
            dataXY.FLAP_C1 = this.tbFLAP_C1.Text;
            dataXY.FLAP_S1 = this.tbFLAP_S1.Text;
            dataXY.HDISK = this.tbHDISK.Text;
            dataXY.INVERSE = this.cmbINVERSE.SelectedIndex;
            dataXY.MU = this.tbMU.Text;
            dataXY.N_BLADE = this.tbN_BLADE.Text;
            dataXY.OMIGA = this.tbOMIGA.Text;
            dataXY.PITCH_0 = this.tbPITCH_0.Text;
            dataXY.PITCH_C = this.tbPITCH_C.Text;
            dataXY.PITCH_S = this.tbPITCH_S.Text;
            dataXY.RADIUS = this.tbRADIUS.Text;
            dataXY.RADIUSC = this.tbRADIUSC.Text;
            dataXY.TWSIT = this.tbTWSIT.Text;
            dataXY.BLADE = string.Empty;
            for (int i = 0; i < this.dgvYX.Rows.Count; i++)
            {
                for (int j = 0; j < this.dgvYX.Columns.Count; j++)
                {
                    if (j == 0)
                    {
                        dataXY.BLADE = dataXY.BLADE + this.dgvYX.Rows[i].Cells[j].Value.ToString();
                    }
                    else
                    {
                        dataXY.BLADE = dataXY.BLADE + "#" + this.dgvYX.Rows[i].Cells[j].Value.ToString();
                    }
                }

                if (i < this.dgvYX.Rows.Count - 1)
                {
                    dataXY.BLADE = dataXY.BLADE + "|";
                }
            }

            item.Tag = dataXY;
            this.XYlistView.Items.Add(item);
            item.Selected = true;

            this.btnEditXY.Enabled = true;
            this.btnDelXY.Enabled = true;
            this.gbYSJ.Enabled = true;
            this.groupBox1.Enabled = true;
        }

        private void btnEditXY_Click(object sender, EventArgs e)
        {
            if (!this.chkModelXY.Checked)
            {
                //_Data.NDISK = 0;
                //_Data.XYData = null;
                return;
            }
            
             
             if (this.XYlistView.SelectedItems.Count!=0)
             {
                 
                 if (!checkXY())
                 {
                     return;
                 }

                 sXY dataXY = new sXY();
                 dataXY.ALF_TPP = this.tbALF_TPP.Text;
                 dataXY.CHORD = this.tbCHORD.Text;
                 //switch (this.cmbDISKAK.SelectedIndex)
                 //{
                 //    case 0:
                 //        dataXY.DISKAK=this.cmbDISKAK.se
                 //}
                 dataXY.DISKAK = Common.cmbdiskToDiskak(this.cmbDISKAK.SelectedIndex);

                 dataXY.DISKX = this.tbDISKX.Text;
                 dataXY.DISKY = this.tbDISKY.Text;
                 dataXY.DISKZ = this.tbDISKZ.Text;
                 dataXY.FLAP_0 = this.tbFLAP_0.Text;
                 dataXY.FLAP_C1 = this.tbFLAP_C1.Text;
                 dataXY.FLAP_S1 = this.tbFLAP_S1.Text;
                 dataXY.HDISK = this.tbHDISK.Text;
                 dataXY.INVERSE = this.cmbINVERSE.SelectedIndex;
                 dataXY.MU = this.tbMU.Text;
                 dataXY.N_BLADE = this.tbN_BLADE.Text;
                 dataXY.OMIGA = this.tbOMIGA.Text;
                 dataXY.PITCH_0 = this.tbPITCH_0.Text;
                 dataXY.PITCH_C = this.tbPITCH_C.Text;
                 dataXY.PITCH_S = this.tbPITCH_S.Text;
                 dataXY.RADIUS = this.tbRADIUS.Text;
                 dataXY.RADIUSC = this.tbRADIUSC.Text;
                 dataXY.TWSIT = this.tbTWSIT.Text;
                 dataXY.BLADE = string.Empty;
                 for (int i = 0; i < this.dgvYX.Rows.Count; i++)
                 {
                     for (int j = 0; j < this.dgvYX.Columns.Count; j++)
                     {
                         if (j == 0)
                         {
                             dataXY.BLADE = dataXY.BLADE+this.dgvYX.Rows[i].Cells[j].Value.ToString();
                         }
                         else
                         {
                             dataXY.BLADE = dataXY.BLADE + "#" + this.dgvYX.Rows[i].Cells[j].Value.ToString();
                         }
                     }
                     if (i < this.dgvYX.Rows.Count - 1)
                     {
                         dataXY.BLADE = dataXY.BLADE + "|";
                     }
                 }

                 //selNode.Tag = dataXY;
                 this.XYlistView.SelectedItems[0].Tag=dataXY;
             }

        }

        /// <summary>
        /// 保存旋翼控件到结构体对象
        /// </summary>
        /// <returns></returns>
        private sXY saveXYControlToObject()
        {
            sXY dataXY = new sXY();
            if (!this.chkModelXY.Checked)
            {
                //_Data.NDISK = 0;
                //_Data.XYData = null;
                return dataXY;
            }
            //TreeNode selNode = this.treeViewXY.SelectedNode;

            //if (selNode != null || this.XYlistView.SelectedItems.Count != 0)
            //{

                if (!checkXY())
                {
                    return dataXY;
                }

                
                dataXY.ALF_TPP = this.tbALF_TPP.Text;
                dataXY.CHORD = this.tbCHORD.Text;
                //switch (this.cmbDISKAK.SelectedIndex)
                //{
                //    case 0:
                //        dataXY.DISKAK=this.cmbDISKAK.se
                //}
                dataXY.DISKAK = Common.cmbdiskToDiskak(this.cmbDISKAK.SelectedIndex);

                dataXY.DISKX = this.tbDISKX.Text;
                dataXY.DISKY = this.tbDISKY.Text;
                dataXY.DISKZ = this.tbDISKZ.Text;
                dataXY.FLAP_0 = this.tbFLAP_0.Text;
                dataXY.FLAP_C1 = this.tbFLAP_C1.Text;
                dataXY.FLAP_S1 = this.tbFLAP_S1.Text;
                dataXY.HDISK = this.tbHDISK.Text;
                dataXY.INVERSE = this.cmbINVERSE.SelectedIndex;
                dataXY.MU = this.tbMU.Text;
                dataXY.N_BLADE = this.tbN_BLADE.Text;
                dataXY.OMIGA = this.tbOMIGA.Text;
                dataXY.PITCH_0 = this.tbPITCH_0.Text;
                dataXY.PITCH_C = this.tbPITCH_C.Text;
                dataXY.PITCH_S = this.tbPITCH_S.Text;
                dataXY.RADIUS = this.tbRADIUS.Text;
                dataXY.RADIUSC = this.tbRADIUSC.Text;
                dataXY.TWSIT = this.tbTWSIT.Text;
                dataXY.BLADE = string.Empty;
                for (int i = 0; i < this.dgvYX.Rows.Count; i++)
                {
                    for (int j = 0; j < this.dgvYX.Columns.Count; j++)
                    {
                        if (j == 0)
                        {
                            dataXY.BLADE = dataXY.BLADE + this.dgvYX.Rows[i].Cells[j].Value.ToString();
                        }
                        else
                        {
                            dataXY.BLADE = dataXY.BLADE + "#" + this.dgvYX.Rows[i].Cells[j].Value.ToString();
                        }
                    }
                    if (i < this.dgvYX.Rows.Count - 1)
                    {
                        dataXY.BLADE = dataXY.BLADE + "|";
                    }
                }

                return dataXY;
            //    selNode.Tag = dataXY;
            //    this.XYlistView.SelectedItems[0].Tag = dataXY;
            //}
        }

        private void btnDelXY_Click(object sender, EventArgs e)
        {
            if (this.XYlistView.SelectedItems.Count != 0 && MessageBox.Show("是否删除\"" + this.XYlistView.SelectedItems[0].Text + "\"?",
    "通告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.XYlistView.Items.Remove(this.XYlistView.SelectedItems[0]);
                bool flag = true;
                if (this.XYlistView.Items.Count == 0)
                {
                    flag = false;
                }
                this.btnEditXY.Enabled = flag;
                this.btnDelXY.Enabled = flag;
                this.gbYSJ.Enabled = flag;
                this.groupBox1.Enabled = flag;
            }

            //if (MessageBox.Show("是否删除\"" + this.treeViewXY.SelectedNode.Text + "\"?",
            //    "通告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            //{
                
            //}
        }

        private void treeViewXY_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //Point clickPoint = new Point(e.X, e.Y);
            //TreeNode selNode = e.Node;// this.treeViewXY.GetNodeAt(clickPoint);
            loadYXGridFormNodeTag((sXY)e.Node.Tag);
        }


        private void btnDelYX_Click(object sender, EventArgs e)
        {
            if (this.dgvYX.SelectedRows.Count == 0)
            {
                MessageBox.Show("请选择要删除的行.");
            }
            else
            {
                this.dgvYX.Rows.Remove(this.dgvYX.SelectedRows[0]);
            }
        }

        private void btnClearYX_Click(object sender, EventArgs e)
        {
            this.dgvYX.Rows.Clear();
        }


        #region 自定义方法
        /// <summary>
        /// 从指定的旋翼结点加载数据到翼型Grid中.
        /// </summary>
        /// <param name="selNode"></param>
        private void loadYXGridFormNodeTag(sXY dataXY)
        {
            if (!dataXY.Equals(null))
            {
                this.btnEditXY.Enabled = true;
                this.btnDelXY.Enabled = true;

                //sXY dataXY = (sXY)selNode.Tag;
                this.tbALF_TPP.Text = dataXY.ALF_TPP;
                this.tbCHORD.Text = dataXY.CHORD;

                this.cmbDISKAK.SelectedIndex = Common.diskakToCmbdisk(dataXY.DISKAK);
                
                
                this.tbDISKX.Text = dataXY.DISKX;
                this.tbDISKY.Text = dataXY.DISKY;
                this.tbDISKZ.Text = dataXY.DISKZ;
                this.tbFLAP_0.Text = dataXY.FLAP_0;
                this.tbFLAP_C1.Text = dataXY.FLAP_C1;
                this.tbFLAP_S1.Text = dataXY.FLAP_S1;
                this.tbHDISK.Text = dataXY.HDISK;
                this.cmbINVERSE.SelectedIndex = dataXY.INVERSE;
                this.tbMU.Text = dataXY.MU;
                this.tbN_BLADE.Text = dataXY.N_BLADE;
                this.tbOMIGA.Text = dataXY.OMIGA;
                this.tbPITCH_0.Text = dataXY.PITCH_0;
                this.tbPITCH_C.Text = dataXY.PITCH_C;
                this.tbPITCH_S.Text = dataXY.PITCH_S;
                this.tbRADIUS.Text = dataXY.RADIUS;
                this.tbRADIUSC.Text = dataXY.RADIUSC;
                this.tbTWSIT.Text = dataXY.TWSIT;

                this.dgvYX.Rows.Clear();
                if (dataXY.BLADE != "")
                {
                    
                    string[] yx = dataXY.BLADE.Split(new Char[] { '|' });

                    for (int i = 0; i < yx.Length; i++)
                    {
                        string[] yxDGV = yx[i].Split(new Char[] { '#' });
                        this.dgvYX.Rows.Add();
                        int iRow = this.dgvYX.Rows.Count - 1;
                        this.dgvYX.Rows[iRow].Cells[0].Value = yxDGV[0];
                        this.dgvYX.Rows[iRow].Cells[1].Value = yxDGV[1];
                        this.dgvYX.Rows[iRow].Cells[2].Value = yxDGV[2];
                    }
                }

            }
        }

        #endregion

        private void GKDlg_Load(object sender, EventArgs e)
        {
            this.mainForm=(MainForm)this.Owner;

            this.SetData( ref this.m_Node);
            if (!this.m_bEdit)
            {
                this.tbName.Text = "";
                this.chkBLCONT.Enabled = false;
                this.chkBLCONT.Checked = false;
            }
            else
            {
                string GKPath = Common.prjName + Path.DirectorySeparatorChar + this.tbName.Text + Path.DirectorySeparatorChar;
                this.chkBLCONT.Enabled = File.Exists(GKPath + "FRESULT.DAT");
            }

            chkModelXY_CheckedChanged(null, null);
        }

        private void chkModelXY_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkModelXY.Checked)
            {
                //this.groupBox2.Enabled = true;
                //this.Height = 656;
                this.groupBox2.Enabled = true;
            }
            else
            {
                //this.groupBox2.Enabled = false;
                //this.Height = 656 - this.groupBox2.Height;//283;
                this.groupBox2.Enabled = false;
            }
        }

        private void treeViewXY_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            e.DrawDefault = true; //我这里用默认颜色即可，只需要在TreeView失去焦点时选中节点仍然突显
            return;
        }

        private void chkModelJQD_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkModelJQD.Checked)
            {
                this.inletBox.Enabled = true;
            }
            else
            {
                this.inletBox.Enabled = false;
            }
        }

        private void XYlistView_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            e.Item.ForeColor = Color.Black;
            e.Item.BackColor = SystemColors.Window;
            if (e.IsSelected)
            {
                e.Item.BackColor = SystemColors.Highlight;
                e.Item.ForeColor = Color.White;
                loadYXGridFormNodeTag((sXY)e.Item.Tag);
            }
            else
            {
                e.Item.Tag = saveXYControlToObject();

            }
            if (this.XYlistView.SelectedItems.Count == 0)
            {
                this.gbYSJ.Enabled = groupBox1.Enabled = false;
            }
            else
            {
                gbYSJ.Enabled = groupBox1.Enabled= true;
            }
            
        }

        private void XYlistView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {

            e.DrawDefault = true; //我这里用默认颜色即可，只需要在TreeView失去焦点时选中节点仍然突显
            return;

            //if (e.State == ListViewItemStates.Selected && e.Item.Selected)
            //{
            //    //e.Item.BackColor = Color.Blue;
            //    //e.Item.ForeColor = Color.White;
            //    //演示为绿底白字
            //    //e.Graphics.FillRectangle(Brushes.DarkBlue, e.Item.Bounds);

            //    Font nodeFont = e.Item.Font;
            //    //if (nodeFont == null) nodeFont = ((TreeView)sender).Font;
            //    e.Graphics.DrawString(e.Item.Text, nodeFont, Brushes.White, Rectangle.Inflate(e.Bounds, 2, 0));
            //}
            //else
            //{
            //    e.DrawDefault = true;
            //}
        }

        private void dgvYX_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            DataGridViewRow row = this.dgvYX.Rows[e.RowIndex];
            YXDlg dlg = new YXDlg(row.Cells[0].Value.ToString(), row.Cells[1].Value.ToString(), Common.prjName+@"\XY\"+row.Cells[2].Value.ToString());
            if (dlg.ShowDialog() == DialogResult.OK)
            {
                row.Cells[0].Value = dlg.m_startR;
                row.Cells[1].Value = dlg.m_endR;
                row.Cells[2].Value = dlg.m_YXpath;

                sXY xy = (sXY)this.XYlistView.SelectedItems[0].Tag;
                xy.BLADE = "#" + dlg.m_startR + "#" + dlg.m_endR + "#" + dlg.m_YXpath + "#";
                this.XYlistView.SelectedItems[0].Tag = xy;
            }
        }

        private void XYlistView_Validated(object sender, EventArgs e)
        {
            //try
            //{
            //    if (XYlistView.FocusedItem != null)
            //    {
            //        XYlistView.FocusedItem.BackColor = SystemColors.Highlight;
            //        XYlistView.FocusedItem.ForeColor = Color.White;
            //        XYlistView.SelectedIndices.Add(XYlistView.FocusedItem.Index);
            //    }
            //}
            //catch (Exception eEx)
            //{
            //    MessageBox.Show(eEx.Message);
            //}
        }



        //private void XYlistView_Validated(object sender, EventArgs e)
        //{
        //    if (XYlistView.FocusedItem != null)
        //    {
        //        XYlistView.FocusedItem.BackColor = SystemColors.Highlight;
        //        XYlistView.FocusedItem.ForeColor = Color.White;
        //    }
        //}



    }
}
