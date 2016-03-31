using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Xml;
using System.Threading;
using AFA.LoadMask;
using System.Configuration;
using ZedGraph;
using MeshDisLib;

namespace AFA
{
    public partial class MainForm : Form
    {
        private string m_strPrjFile = string.Empty;
        public static Dictionary<string, Process> rotor_Process = new Dictionary<string,Process>();
        public string tecplot360WorkDir;

        public static AFATreeNode GKNode;
        private static AFATreeNode WGNode;

        public MainForm()
        {
            InitializeComponent();
            this.tsSave.Enabled = false;//新建或打开后才能保持
            this.tsSaveAs.Enabled = false;//
        }

        public static bool CheckNodeName(string strName)
        {
            //增加工况文件夹重名检查.
            if (Directory.Exists(Common.prjName + Path.DirectorySeparatorChar + strName))
            {
                MessageBox.Show("工况名称重复.");
                return false;
            }

            for (int i = 0; i < GKNode.Nodes.Count; i++)
            {
                if (string.Compare(strName, GKNode.Nodes[i].Text, true) == 0)
                {
                    MessageBox.Show("已存在名称为" + "\"" + strName + "\"的工况,请修改.");
                    return false;
                }
            }



            return true;
        }

        private void InitTree()
        {
            this.treeView1.Nodes.Clear();
            AFATreeNode root = null;
            AFATreeNode node = null;
            root = new AFATreeNode("计算数据", TreeNodeType.nodeRoot);
            this.treeView1.Nodes.Add(root);

            GKNode = new AFATreeNode("工况设置", TreeNodeType.nodeGK);
            root.Nodes.Add(GKNode);

            WGNode = new AFATreeNode("计算网格", TreeNodeType.nodeWG);
            root.Nodes.Add(WGNode);

            node = new AFATreeNode("求解设置", TreeNodeType.nodeQJ);
            root.Nodes.Add(node);

            this.treeView1.ExpandAll();

            int i = 0;
            foreach (TabPage page in this.tabControl1.TabPages)
            {
                if (i++ != 0)
                {
                    this.tabControl1.TabPages.Remove(page);
                }
            }

            this.tabPage1.Controls.Remove(this.meshDisControl1);
            this.showGKBtn.Enabled = false;
            this.meshDisControl1 = new MeshDisLib.MeshDisControl();
            this.meshDisControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.meshDisControl1.Location = new System.Drawing.Point(3, 3);
            this.meshDisControl1.Name = "meshDisControl1";
            this.meshDisControl1.Size = new System.Drawing.Size(663, 393);
            this.meshDisControl1.TabIndex = 0;
            this.tabPage1.Controls.Add(this.meshDisControl1);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(669, 399);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "网格显示";
            this.tabPage1.UseVisualStyleBackColor = true;
            //this.meshDisControl1.clear();//.MeshDis(null);
        }


        private void MainForm_Load(object sender, EventArgs e)
        {

            Configuration config = System.Configuration.ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            KeyValueConfigurationElement element = config.AppSettings.Settings["tecplot360_workDir"];
            if (element != null)
            {
                this.tecplot360WorkDir= config.AppSettings.Settings["tecplot360_workDir"].Value;
            }
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            Point clickPoint = new Point(e.X, e.Y);
            AFATreeNode tNode = (AFATreeNode)this.treeView1.GetNodeAt(clickPoint);
            if (tNode == null)
            {
                return;
            }
            else
            {
                this.treeView1.SelectedNode = tNode;
                switch (tNode.NodeType)
                {
                    case TreeNodeType.nodeGK:
                        tNode.ContextMenuStrip = this.cmsAddGK;
                        break;
                    case TreeNodeType.nodeGKCase:
                        tNode.ContextMenuStrip = this.cmsEditGK;
                        break;
                    case TreeNodeType.nodeWG:
                        tNode.ContextMenuStrip = this.cmsWG;
                        break;
                    case TreeNodeType.nodeQJ:
                        tNode.ContextMenuStrip = this.cmsQJ;
                        break;
                    default:
                        break;
                }

            }
        }

        private void tsAddGK_Click(object sender, EventArgs e)
        {
            AFATreeNode selNode = (AFATreeNode)this.treeView1.SelectedNode;
            if (selNode != null)
            {
                GKDlg dlg;
                if (selNode.Nodes.Count == 0)
                {
                    dlg = new GKDlg(false);
                }
                else
                {
                    dlg = new GKDlg(false, (AFATreeNode)selNode.Nodes[selNode.Nodes.Count - 1]);
                }
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    AFATreeNode node = null;
                    node = new AFATreeNode(dlg.m_Data.Name, TreeNodeType.nodeGKCase);
                    node.Name = dlg.m_Data.Name;
                    node.Tag = dlg.m_Data;
                    selNode.Nodes.Add(node);
                    //GKNode.Nodes
                }

            }
            selNode.Expand();
        }

        private void tsEditGK_Click(object sender, EventArgs e)
        {
            AFATreeNode selNode = (AFATreeNode)this.treeView1.SelectedNode;
            
            if (selNode != null)
            {
                GKDlg dlg = new GKDlg(true, selNode);
                //dlg.SetData(ref selNode);
                if (dlg.ShowDialog(this) == DialogResult.OK)
                {
                    refreshChart(selNode);
                }

            }
        }

        private void tsDelGK_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(@"是否删除工况(" + this.treeView1.SelectedNode.Text + ")?同时工况文件夹也将删除.",
                "通告", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                TreeNode node = this.treeView1.SelectedNode;
                if (MainForm.rotor_Process.ContainsKey(node.Text)&& !MainForm.rotor_Process[node.Text].HasExited)
                {
                    MessageBox.Show("求解器没有停止无法删除工况!");
                    return;
                }
                
                if (Directory.Exists(Common.prjName +Path.DirectorySeparatorChar+ node.Text))
                {
                    Directory.Delete(Common.prjName +Path.DirectorySeparatorChar+ node.Text,true);
                }
                
                foreach (TabPage page in tabControl1.TabPages)
                {
                    if (this.treeView1.SelectedNode.Text == page.Text)
                    {
                        tabControl1.TabPages.Remove(page);
                    }
                }

                node.Remove();
            }
        }

        private void tsNew_Click(object sender, EventArgs e)
        {
            PrjDlg dlg = new PrjDlg();
            if (dlg.ShowDialog() == DialogResult.OK)
            {

                InitTree();
                string str = dlg.m_strPrj;
                str = str.Substring(str.LastIndexOf("\\") + 1);
                this.Text = "全机气动布局---"+str;
                m_strPrjFile = dlg.m_strPrj + "\\str.prj";
                this.tsSave.Enabled = true;
                this.tsSaveAs.Enabled = true;

                Common.GKPath = "";
            }
        }

        private void tsQJ_Click(object sender, EventArgs e)
        {
            AFATreeNode selNode = (AFATreeNode)this.treeView1.SelectedNode;
            if (selNode != null)
            {
                QJQDlg dlg = new QJQDlg();
                dlg.ShowDialog();
            }
        }

        private void tsWG_Click(object sender, EventArgs e)
        {
            AFATreeNode selNode = (AFATreeNode)this.treeView1.SelectedNode;
            if (selNode != null && MessageBox.Show("导入网格会清除已计算的结果数据", "提示", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {

                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "导入网格";
                // dlg.InitialDirectory = Common.dataFolder;
                dlg.Filter = "网格文件|*.msh" +"|All Files|*.*";

                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    if (dlg.FileName == Common.prjName + "\\rotorwing_gambit.msh")
                    {
                        MessageBox.Show("源网格文件与目标网格文件相同,不能导入.");
                        return;
                    }
                //                    Directory.GetCurrentDirectory
                //File.Copy(Common.MESHLOCATION, Common.prjName + "\\rotorwing_gambit.msh", true);

                    selNode.Nodes.Clear();
                    string str = dlg.FileName;
                    string temp = str.Substring(str.LastIndexOf("\\") + 1);
                    AFATreeNode node = null;
                    node = new AFATreeNode(temp, TreeNodeType.nodeWGCase);
                    selNode.Nodes.Add(node);
                    selNode.Expand();
                    Common.MESHLOCATION = str;


                    //if (MessageBox.Show("是否复制到本地", "网格复制", MessageBoxButtons.OKCancel) == DialogResult.OK)
                    //{
                    this.tabPage1.Controls.Remove(this.meshDisControl1);
                    this.showGKBtn.Enabled = false;
                    this.meshDisControl1 = new MeshDisLib.MeshDisControl();
                    this.meshDisControl1.Dock = System.Windows.Forms.DockStyle.Fill;
                    this.meshDisControl1.Location = new System.Drawing.Point(3, 3);
                    this.meshDisControl1.Name = "meshDisControl1";
                    this.meshDisControl1.Size = new System.Drawing.Size(663, 393);
                    this.meshDisControl1.TabIndex = 0;
                    this.tabPage1.Controls.Add(this.meshDisControl1);

                        Thread thread = new Thread(new ParameterizedThreadStart(copyMSH));
                        thread.IsBackground = true;
                        //thread.Start();// 无参的回调
                        thread.Start(new CallBackDelegate(callBack)); //有参的回调


                        cmd.ShowOpaqueLayer(this, 123, true);
                    //}
                    //else
                    //{
                    //    this.meshDisControl1.MeshDis(dlg.FileName);
                    //}
                }

            }
        }

        private void copyMSH(object obj)
        {
            File.Copy(Common.MESHLOCATION, Common.prjName + "\\rotorwing_gambit.msh", true);
            CallBackDelegate cbd = (CallBackDelegate)obj;
            if (this.InvokeRequired)
            {
                //CallBackDelegate cbd = new CallBackDelegate(callBack);
                this.BeginInvoke(cbd, Common.MESHLOCATION); //异步调用回调
                //this.Invoke(cbd, msg); //同步调用回调
            }
            else
            {
                this.Close();
            }
        }

        private void callBack(string msg)
        {
            this.meshDisControl1.MeshDis(msg);

            clearResultData();

            this.showGKBtn.Enabled = true;
            this.cmd.HideOpaqueLayer();
        }

        private void clearResultData()
        {
            File.Delete(Common.prjName + @"\DISK.DAT");
            File.Delete(Common.prjName + @"\GRID.DAT");
            File.Delete(Common.prjName + @"\SURFACE.DAT");



            for (int i = 0; i < this.treeView1.Nodes[0].Nodes[0].Nodes.Count; i++)
            {
                sGK gk = (sGK)this.treeView1.Nodes[0].Nodes[0].Nodes[i].Tag;
                if(Directory.Exists(Common.prjName + "\\" + gk.Name))
                {
                    Directory.Delete(Common.prjName + "\\" + gk.Name, true);
                }

                foreach(TabPage page in this.tabControl1.TabPages)
                {
                    if (page.Text == gk.Name)
                    {
                        this.tabControl1.TabPages.Remove(page);
                        break;
                    }
                    
                }
            }
            
        }
        
        private void tsCalculate_Click(object sender, EventArgs e)
        {
            if (GKNode==null||GKNode.Nodes.Count == 0 || WGNode.Nodes.Count == 0)
            {
                return;
            }

            CalculateConfigForm configForm = new CalculateConfigForm();
            configForm.ShowDialog(this);
            if (configForm.selGKNode.Count == 0)
            {
                return;
            }

            tabControl1.SelectedIndex = 1;
            int iTabs = tabControl1.TabPages.Count;

            //转换网格文件,同时复制.msh文件到项目目录下.
            CalculateLoadingForm loadForm = new CalculateLoadingForm(configForm.selGKNode);
            loadForm.ShowDialog(this);

            #region 按工况数量运行N个求解器(rotorCFD),读取CFD文件,并开启N个监控tab.
            //int iGK = MainForm.GKNode.Nodes.Count;
            string strGKDir = string.Empty;
            for (int i = 0; i < configForm.selGKNode.Count; i++)
            {
                strGKDir = Common.prjName + Path.DirectorySeparatorChar + configForm.selGKNode[i].Text + Path.DirectorySeparatorChar;

                tabControl1.TabPages.RemoveByKey(configForm.selGKNode[i].Text);
                TabPage m_tabPage = new TabPage();
                m_tabPage.Name = configForm.selGKNode[i].Text;// "tPage" + i;
                m_tabPage.Tag = configForm.selGKNode[i].Text;//MainForm.GKNode.Nodes[i].Text;
                m_tabPage.Text = configForm.selGKNode[i].Text;//MainForm.GKNode.Nodes[i].Text;
                m_tabPage.UseVisualStyleBackColor = true;
                this.tabControl1.Controls.Add(m_tabPage);
                //mainForm.Controls.Find("tabControl1", true)[0].Controls.Add(m_tabPage);
                DisGKResult disGKResult1 = new DisGKResult(m_tabPage,null,true);
                m_tabPage.Controls.Add(disGKResult1);
                disGKResult1.Dock = DockStyle.Fill;
                disGKResult1.Name = "disGKResult" + i;

                if (i == configForm.selGKNode.Count - 1)
                {
                    this.tabControl1.SelectedTab = m_tabPage;
                }
                
            }
            #endregion
        }

        private void RotoProcess_OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            // Collect the sort command output.
            string data = outLine.Data;
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                //this.dataText.Text = outLine.Data;
                switch (data)
                {
                    case " 完成网格转换。":
                        Thread.Sleep(2000);
                        //cmd.HideOpaqueLayer();
                        this.Close();
                        break;
                }
            }
        }

        public void SaveFile(string strFile)
        {
            m_strPrjFile = strFile;
            AFATreeNode treeNode = null;

            XmlDocument doc = new XmlDocument();
            XmlNode xmlnode;
            xmlnode = doc.CreateElement("全机气动分析");
            doc.AppendChild(xmlnode);
            XmlElement elm = doc.DocumentElement;

            XmlElement subelm, partelm, velm,xyelm;

            subelm = doc.CreateElement("项目路径");
            subelm.InnerText = Common.prjName;
            elm.AppendChild(subelm);

            subelm = doc.CreateElement("工况");
            elm.AppendChild(subelm);
            partelm = doc.CreateElement("工况数量");
            partelm.InnerText = GKNode.Nodes.Count.ToString();
            subelm.AppendChild(partelm);
            if (GKNode.Nodes.Count > 0)
            {
                sGK data;
                for (int i = 0; i < GKNode.Nodes.Count; i++)
                {
                    treeNode = (AFATreeNode)GKNode.Nodes[i];
                    data = (sGK)treeNode.Tag;
                    partelm = doc.CreateElement("工况" + i.ToString());
                    subelm.AppendChild(partelm);
                    velm = doc.CreateElement("名称");
                    velm.InnerText = data.Name;
                    partelm.AppendChild(velm);
                    velm = doc.CreateElement("MODEL");
                    velm.InnerText = data.MODEL;
                    partelm.AppendChild(velm);
                    velm = doc.CreateElement("BLCONT");
                    velm.InnerText = data.BLCONT;
                    partelm.AppendChild(velm);
                    velm = doc.CreateElement("MACH_INF");
                    velm.InnerText = data.MACH_INF;
                    partelm.AppendChild(velm);
                    velm = doc.CreateElement("RE");
                    velm.InnerText = data.RE;
                    partelm.AppendChild(velm);
                    velm = doc.CreateElement("FUX");
                    velm.InnerText = data.FUX;
                    partelm.AppendChild(velm);
                    velm = doc.CreateElement("FUY");
                    velm.InnerText = data.FUY;
                    partelm.AppendChild(velm);
                    velm = doc.CreateElement("FUZ");
                    velm.InnerText = data.FUZ;
                    partelm.AppendChild(velm);
                    velm = doc.CreateElement("PITCH_V");
                    velm.InnerText = data.PITCH_V;
                    partelm.AppendChild(velm);
                    velm = doc.CreateElement("YAW_V");
                    velm.InnerText = data.YAW_V;
                    partelm.AppendChild(velm);
                    velm = doc.CreateElement("FLOWRATE");
                    velm.InnerText = data.FLOWRATE;
                    partelm.AppendChild(velm);
                    velm = doc.CreateElement("ZMU");
                    velm.InnerText = data.ZMU;
                    partelm.AppendChild(velm);
                    velm = doc.CreateElement("NDISK");
                    velm.InnerText = data.NDISK.ToString();
                    partelm.AppendChild(velm);
                    velm = doc.CreateElement("PLANESNUM");
                    velm.InnerText = data.PlanesNum;
                    partelm.AppendChild(velm);
                    for (int j = 0; j < data.NDISK; j++)
                    {
                        velm = doc.CreateElement("旋翼"+j.ToString());
                        partelm.AppendChild(velm);
                        xyelm = doc.CreateElement("DISKX");
                        xyelm.InnerText = data.XYData[j].DISKX;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("DISKY");
                        xyelm.InnerText = data.XYData[j].DISKY;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("DISKZ");
                        xyelm.InnerText = data.XYData[j].DISKZ;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("DISKAK");
                        xyelm.InnerText = data.XYData[j].DISKAK.ToString();
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("RADIUS");
                        xyelm.InnerText = data.XYData[j].RADIUS;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("RADIUSC");
                        xyelm.InnerText = data.XYData[j].RADIUSC;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("TWSIT");
                        xyelm.InnerText = data.XYData[j].TWSIT;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("CHORD");
                        xyelm.InnerText = data.XYData[j].CHORD;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("INVERSE");
                        xyelm.InnerText = data.XYData[j].INVERSE.ToString();
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("FLAP_0");
                        xyelm.InnerText = data.XYData[j].FLAP_0;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("FLAP_C1");
                        xyelm.InnerText = data.XYData[j].FLAP_C1;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("FLAP_S1");
                        xyelm.InnerText = data.XYData[j].FLAP_S1;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("PITCH_0");
                        xyelm.InnerText = data.XYData[j].PITCH_0;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("PITCH_S");
                        xyelm.InnerText = data.XYData[j].PITCH_S;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("PITCH_C");
                        xyelm.InnerText = data.XYData[j].PITCH_C;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("ALF_TPP");
                        xyelm.InnerText = data.XYData[j].ALF_TPP;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("HDISK");
                        xyelm.InnerText = data.XYData[j].HDISK;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("N_BLADE");
                        xyelm.InnerText = data.XYData[j].N_BLADE;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("OMIGA");
                        xyelm.InnerText = data.XYData[j].OMIGA;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("MU");
                        xyelm.InnerText = data.XYData[j].MU;
                        velm.AppendChild(xyelm);
                        xyelm = doc.CreateElement("BLADE");
                        xyelm.InnerText = data.XYData[j].BLADE;
                        velm.AppendChild(xyelm);
                    }

                    #region 进气道
                    velm = doc.CreateElement("MASSOUT_RATE");
                    velm.InnerText = data.inlet.MASSOUT_RATE;
                    partelm.AppendChild(velm);

                    velm = doc.CreateElement("TEMP_OUT0");
                    velm.InnerText = data.inlet.TEMP_OUT0;
                    partelm.AppendChild(velm);

                    #endregion
                }
            }
            subelm = doc.CreateElement("求解器设置");
            elm.AppendChild(subelm);
            partelm = doc.CreateElement("ITMAX");
            partelm.InnerText = Common.ITMAX;
            subelm.AppendChild(partelm);
            partelm = doc.CreateElement("SAVE_STEP");
            partelm.InnerText = Common.SAVE_STEP;
            subelm.AppendChild(partelm);
            partelm = doc.CreateElement("CFL");
            partelm.InnerText = Common.CFL;
            subelm.AppendChild(partelm);

            doc.Save(m_strPrjFile);
        }

        private void tsSave_Click(object sender, EventArgs e)
        {
            try
            {
                SaveFile(m_strPrjFile);
                MessageBox.Show("保存完成");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private string GetNodeText(XmlElement elm, string textL1, string textL2, string textL3, string textL4)
        {
            try
            {
                if (textL2.Trim().Length == 0)
                {
                    return elm.SelectSingleNode("./" + textL1).InnerText;
                }
                else
                {
                    if (textL3.Trim().Length == 0)
                    {
                        return elm.SelectSingleNode("./" + textL1).SelectSingleNode("./" + textL2).InnerText;
                    }
                    else
                    {
                        if (textL4.Trim().Length == 0)
                        {
                            return elm.SelectSingleNode("./" + textL1).SelectSingleNode("./" + textL2).SelectSingleNode("./" + textL3).InnerText;
                        }
                        else
                        {
                            return elm.SelectSingleNode("./" + textL1).SelectSingleNode("./" + textL2).SelectSingleNode("./" + textL3).SelectSingleNode("./" + textL4).InnerText;
                        }
                    }
                }
            }
            catch
            {
                return "0";
            }
        }

        private void OpenFile(string strFile)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(strFile);
            XmlElement root = doc.DocumentElement;
            //Common.prjName = GetNodeText(root, "项目路径", string.Empty, string.Empty, string.Empty);

            int Num = 0;
            Num = Convert.ToInt32(GetNodeText(root, "工况", "工况数量", string.Empty, string.Empty));
            if (Num > 0)
            {
                sGK data = new sGK();
                string NumName = string.Empty;
                for (int i = 0; i < Num; i++)
                {
                    NumName = "工况" + i.ToString();
                    data.Name = GetNodeText(root, "工况", NumName, "名称", string.Empty);
                    data.MODEL = GetNodeText(root, "工况", NumName, "MODEL", string.Empty);
                    data.BLCONT = GetNodeText(root, "工况", NumName, "BLCONT", string.Empty);
                    data.MACH_INF = GetNodeText(root, "工况", NumName, "MACH_INF", string.Empty);
                    data.RE = GetNodeText(root, "工况", NumName, "RE", string.Empty);
                    data.FUX = GetNodeText(root, "工况", NumName, "FUX", string.Empty);
                    data.FUY = GetNodeText(root, "工况", NumName, "FUY", string.Empty);
                    data.FUZ = GetNodeText(root, "工况", NumName, "FUZ", string.Empty);
                    data.PITCH_V = GetNodeText(root, "工况", NumName, "PITCH_V", string.Empty);
                    data.YAW_V = GetNodeText(root, "工况", NumName, "YAW_V", string.Empty);
                    data.FLOWRATE = GetNodeText(root, "工况", NumName, "FLOWRATE", string.Empty);
                    data.ZMU = GetNodeText(root, "工况", NumName, "ZMU", string.Empty);
                    data.NDISK = Convert.ToInt32(GetNodeText(root, "工况", NumName, "NDISK", string.Empty));
                    data.PlanesNum = GetNodeText(root, "工况", NumName, "PLANESNUM", string.Empty);

                    data.XYData = new sXY[data.NDISK];
                    string XYName = string.Empty;
                    for (int j = 0; j < data.NDISK; j++)
                    {
                        XYName = "旋翼" + j.ToString();
                        data.XYData[j].DISKX = GetNodeText(root, "工况", NumName, XYName, "DISKX");
                        data.XYData[j].DISKY = GetNodeText(root, "工况", NumName, XYName, "DISKY");
                        data.XYData[j].DISKZ = GetNodeText(root, "工况", NumName, XYName, "DISKZ");
                        data.XYData[j].DISKAK = Convert.ToInt32(GetNodeText(root, "工况", NumName, XYName, "DISKAK"));
                        data.XYData[j].RADIUS = GetNodeText(root, "工况", NumName, XYName, "RADIUS");
                        data.XYData[j].RADIUSC = GetNodeText(root, "工况", NumName, XYName, "RADIUSC");
                        data.XYData[j].TWSIT = GetNodeText(root, "工况", NumName, XYName, "TWSIT");
                        data.XYData[j].CHORD = GetNodeText(root, "工况", NumName, XYName, "CHORD");
                        data.XYData[j].INVERSE = Convert.ToInt32(GetNodeText(root, "工况", NumName, XYName, "INVERSE"));
                        data.XYData[j].FLAP_0 = GetNodeText(root, "工况", NumName, XYName, "FLAP_0");
                        data.XYData[j].FLAP_C1 = GetNodeText(root, "工况", NumName, XYName, "FLAP_C1");
                        data.XYData[j].FLAP_S1 = GetNodeText(root, "工况", NumName, XYName, "FLAP_S1");
                        data.XYData[j].PITCH_0 = GetNodeText(root, "工况", NumName, XYName, "PITCH_0");
                        data.XYData[j].PITCH_S = GetNodeText(root, "工况", NumName, XYName, "PITCH_S");
                        data.XYData[j].PITCH_C = GetNodeText(root, "工况", NumName, XYName, "PITCH_C");
                        data.XYData[j].ALF_TPP = GetNodeText(root, "工况", NumName, XYName, "ALF_TPP");
                        data.XYData[j].HDISK = GetNodeText(root, "工况", NumName, XYName, "HDISK");
                        data.XYData[j].N_BLADE = GetNodeText(root, "工况", NumName, XYName, "N_BLADE");
                        data.XYData[j].OMIGA = GetNodeText(root, "工况", NumName, XYName, "OMIGA");
                        data.XYData[j].MU = GetNodeText(root, "工况", NumName, XYName, "MU");
                        data.XYData[j].BLADE = GetNodeText(root, "工况", NumName, XYName, "BLADE");
                    }

                    #region 进气道
                    data.inlet.MASSOUT_RATE = GetNodeText(root, "工况", NumName, "MASSOUT_RATE", string.Empty);
                    data.inlet.TEMP_OUT0 = GetNodeText(root, "工况", NumName, "TEMP_OUT0", string.Empty);
                    #endregion

                    AFATreeNode node = null;
                    node = new AFATreeNode(data.Name, TreeNodeType.nodeGKCase);
                    node.Name = data.Name;
                    node.Tag = data;
                    MainForm.GKNode.Nodes.Add(node);
                }
            }
            Common.ITMAX = GetNodeText(root, "求解器设置", "ITMAX", string.Empty, string.Empty);
            Common.SAVE_STEP = GetNodeText(root, "求解器设置", "SAVE_STEP", string.Empty, string.Empty);
            Common.CFL = GetNodeText(root, "求解器设置", "CFL", string.Empty, string.Empty);

        }

        public delegate void CallBackDelegate(string msg);
        private OpaqueCommand cmd = new OpaqueCommand();

        public delegate void MethodParamInvoker(string val);
        private void openFileCallBack(object obj,LoadingForm form)
        {
            try
            {
                object[] objs=(object[])obj;
                
                string fileName = objs[1].ToString();
                //MethodInvoker mi = new MethodInvoker(form.setLoadingText);
                //this.BeginInvoke(mi);
                MethodParamInvoker mpi = new MethodParamInvoker(form.setLoadingText);
                this.BeginInvoke(mpi, "加载.Mesh文件.");

                MeshDisControl meshDisControl = (MeshDisControl)objs[0];
                //form.loadingText = "加载.Mesh文件.";
                meshDisControl.MeshDis(fileName.ToString());
            }
            catch (Exception e)
            {
                MessageBox.Show("openFileCallBack:"+e.Message);
            }
        }


        private void tsOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog oDlg = new OpenFileDialog();
            oDlg.Title = "打开文件";
            oDlg.InitialDirectory = Common.startupFolder;
            oDlg.Filter = "prj files(*.prj)|*.prj";
            if (oDlg.ShowDialog() == DialogResult.OK)
            {
                this.m_strPrjFile = oDlg.FileName;
                Common.prjName = oDlg.FileName.Replace("\\" + oDlg.SafeFileName, "");

                InitTree();
                string str = oDlg.FileName;
                str = str.Substring(str.LastIndexOf("\\") + 1);
                this.Text = "全机气动布局---" + str;

                this.tsSave.Enabled = true;
                this.tsSaveAs.Enabled = true;

                string fileName = oDlg.FileName;
                OpenFile(fileName);
                GKNode.Expand();

                WGNode.Nodes.Clear();
                string strMsh = fileName.Substring(0, fileName.LastIndexOf("\\") + 1) + "rotorwing_gambit.msh";
                if (File.Exists(strMsh))
                {
                    AFATreeNode node = null;
                    node = new AFATreeNode("rotorwing_gambit.msh", TreeNodeType.nodeWGCase);
                    WGNode.Nodes.Add(node);
                    WGNode.Expand();
                    Common.MESHLOCATION = strMsh;

                    //this.meshDisControl1.MeshDis(strMsh);

                    object[] objs = new object[2];
                    objs[0] = this.meshDisControl1;
                    objs[1] = strMsh;
                    LoadingForm f = new LoadingForm(objs, this.openFileCallBack);
                    f.ShowDialog(this);

                    this.showGKBtn.Enabled = true;
                    this.showGKBtn.Checked = true;
                }

                //打开计算结果页面
                for (int i = 0; i < GKNode.Nodes.Count; i++)
                {

                    TreeNode node = GKNode.Nodes[i];
                    Common.GKPath = Common.prjName + Path.DirectorySeparatorChar + node.Text + Path.DirectorySeparatorChar;
                    string GKPath = Common.GKPath;
                    if (!Directory.Exists(GKPath))
                    {
                        continue;
                    }
                    sGK data = (sGK)node.Tag;
                    tabControl1.TabPages.RemoveByKey(node.Text);
                    //foreach (TabPage page in tabControl1.TabPages)
                    //{
                    //    if (node.Text == page.Text)
                    //    {
                    //        tabControl1.TabPages.Remove(page);
                    //    }
                    //}
                    TabPage m_tabPage = new TabPage();
                    m_tabPage.Name = node.Text;// "tPage" + i;
                    m_tabPage.Tag = node.Text;//MainForm.GKNode.Nodes[i].Text;
                    m_tabPage.Text = node.Text;//MainForm.GKNode.Nodes[i].Text;
                    m_tabPage.UseVisualStyleBackColor = true;
                    this.tabControl1.Controls.Add(m_tabPage);
                    this.tabControl1.SelectedTab = m_tabPage; //this.tabControl1.TabPages[this.tabControl1.TabPages.Count-1];

                    //mainForm.Controls.Find("tabControl1", true)[0].Controls.Add(m_tabPage);

                    #region 加载上次运行图画
                    Dictionary<int, PointPairList> pointYCollection = new Dictionary<int, PointPairList>();
                    string[] arr = new string[5];
                    SyswareDataObject sdo = new SyswareDataObject();
                    try
                    {

                        if (SyswareDataObjectUtil.loadFresultData(sdo, GKPath, false, true))
                        {
                            sdo = sdo.children[0];
                            foreach (SyswareDataObject tem in sdo.children)
                            {
                                arr[0] = tem.children[0].value;
                                arr[1] = tem.children[1].value;
                                arr[2] = tem.children[2].children[2].value;
                                arr[3] = tem.children[2].children[0].value;
                                arr[4] = tem.children[3].children[0].children[2].value;

                                for (int j = 1; j < arr.Length; j++)
                                {
                                    double axisY = 0.0;
                                    try
                                    {
                                        axisY = double.Parse(arr[j], System.Globalization.NumberStyles.Any);
                                    }
                                    catch
                                    {
                                        axisY = 0.0;
                                    }

                                    if (!pointYCollection.ContainsKey(j - 1))
                                    {
                                        pointYCollection.Add(j - 1, new PointPairList());
                                    }
                                    pointYCollection[j - 1].Add(Convert.ToDouble(arr[0]), Convert.ToDouble(axisY));
                                }
                            }
                        }
                        else
                        {
                            MessageBox.Show("文件加载失败");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("文件格式错误.详细:" + ex.Message);
                        return;
                    }

                    

                    DisGKResult disGKResult1 = new DisGKResult(m_tabPage, pointYCollection, false);
                    disGKResult1.drawGraph(arr);
                    #endregion

                    m_tabPage.Controls.Add(disGKResult1);
                    disGKResult1.Dock = DockStyle.Fill;
                    disGKResult1.Name = "disGKResult" + i;
                }
            }
        }

        private void tsSaveAs_Click(object sender, EventArgs e)
        {
            SaveFileDialog sDlg = new SaveFileDialog();
            sDlg.Title = "请输入文件名称";
            sDlg.Filter = "prj files(*.prj)|*.prj";
            sDlg.InitialDirectory = Common.prjName;
            sDlg.OverwritePrompt = true;
            if (sDlg.ShowDialog() == DialogResult.OK)
            {
                string str = sDlg.FileName;
                str = str.Substring(str.LastIndexOf("\\") + 1);
                this.Text = "全机气动布局---" + str;
                SaveFile(sDlg.FileName);
            }
        }

        private void tsExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MainForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (string key in MainForm.rotor_Process.Keys)
            {
                Process pro = MainForm.rotor_Process[key];
                if (!pro.HasExited)
                {
                    pro.Kill();
                }
            }
            
            if (MainForm.GKNode!=null&&MainForm.GKNode.Nodes.Count>0&&MessageBox.Show("是否保存?", "保存", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                SaveFile(this.m_strPrjFile);
            }
            //Process[] processes;
            //processes = Process.GetProcesses();
            //foreach (Process p in processes)
            //{
            //    string name = p.ProcessName;
            //    if (name.Contains("rotor") || name.Contains("pre_mesh"))
            //    {
            //        p.Kill();
            //    }
            //}
        }

        private void configure_Click(object sender, EventArgs e)
        {
            new ConfigureForm().ShowDialog(this);
        }

        private void tabControl1_ControlRemoved(object sender, ControlEventArgs e)
        {
            TabPage page=(TabPage)e.Control;
            try
            {
                Process pro = MainForm.rotor_Process[page.Text];
                if (!pro.HasExited)
                {
                    pro.Kill();
                }
            }
            catch
            {
            }
            finally
            {
                MainForm.rotor_Process.Remove(page.Text);
            }

            
        }

        private void showGKBtn_CheckedChanged(object sender, EventArgs e)
        {
            this.meshDisControl1.showEdge(this.showGKBtn.Checked);
        }

        private void treeView1_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent != null && e.Node.Parent.Text == "工况设置" && e.Node.Nodes.Count == 0)
            {
                tsEditGK_Click(sender, null);
            }
            else if (e.Node.Text == "求解设置")
            {
                tsQJ_Click(null,null);
            }
            
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.Node.Parent != null && e.Node.Parent.Text == "工况设置" && e.Node.Nodes.Count == 0)
            {
                refreshChart((AFATreeNode)e.Node);
            }
            
        }

        /// <summary>
        /// 工况刷新图表
        /// </summary>
        /// <param name="selNode"></param>
        private void refreshChart(AFATreeNode selNode)
        {
            //AFATreeNode selNode = (AFATreeNode)this.treeView1.SelectedNode;
            if (selNode != null&&selNode.Tag!=null)
            {
                sGK gk = (sGK)selNode.Tag;
                if (gk.MODEL == "1" || gk.MODEL == "101")
                {
                    this.meshDisControl1.addRotor(null);
                }
                else
                {
                    if (gk.NDISK != 0)
                    {
                        double[,] rotor = new double[gk.NDISK, 7];
                        for (int i = 0; i < gk.NDISK; i++)
                        {
                            rotor[i, 0] = Convert.ToDouble(gk.XYData[i].DISKX);
                            rotor[i, 1] = Convert.ToDouble(gk.XYData[i].DISKY);
                            rotor[i, 2] = Convert.ToDouble(gk.XYData[i].DISKZ);
                            rotor[i, 3] = Convert.ToDouble(gk.XYData[i].RADIUS);
                            rotor[i, 4] = Convert.ToDouble(gk.XYData[i].HDISK);
                            rotor[i, 5] = Convert.ToDouble(gk.XYData[i].DISKAK);
                            rotor[i, 6] = Convert.ToDouble(gk.XYData[i].ALF_TPP);

                        }

                        this.meshDisControl1.addRotor(rotor);
                    }
                }
                
            }
            
        }
    }
}
