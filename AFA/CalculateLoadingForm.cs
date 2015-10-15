using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using AFA.LoadMask;
using System.Threading;
using System.Diagnostics;
using System.IO;

namespace AFA
{
    public partial class CalculateLoadingForm : Form
    {

        private OpaqueCommand cmd = new OpaqueCommand();
        public delegate void CallBackDelegate(string msg);
        //Thread thread;
        MainForm mainForm;
        private Process proc=null;
        private List<TreeNode> selGKNodes;

        public CalculateLoadingForm()
        {
            InitializeComponent();
        }

        public CalculateLoadingForm(List<TreeNode> GKNodes)
        {
            InitializeComponent();
            this.selGKNodes = GKNodes;
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            //this.BackColor = Color.White;
            //this.TransparencyKey = Color.White;
            CalculateLoadingForm.CheckForIllegalCrossThreadCalls = false;

            mainForm = (MainForm)this.Owner;
            Thread thread = new Thread(loadData);
            thread.IsBackground = true;
            thread.Start();
            
            //this.thread = new Thread( new ParameterizedThreadStart(loadData));
            //thread.IsBackground = true;
            ////thread.Start();// 无参的回调
            //thread.Start(new CallBackDelegate(callBack)); //有参的回调
            cmd.ShowOpaqueLayer(this, 123, true);
        }


        private void loadData(object obj)
        {
            #region  工况数据保存为CFD文件.
            if (!Common.SaveToCFDIN(this.selGKNodes))
            {
                return;
            }
            #endregion

            #region 进行预处理,将网格文件.msh复制并改名到项目下.
            preProcess();
            #endregion
        }



        #region 公共计算模块

        /// <summary>
        /// 进行预处理,将网格文件.msh复制并改名到项目下,调用Cpp2转换网格为FUSELAGE.DAT文件.
        /// </summary>
        private void preProcess()
        {
            //FileInfo fInfo;
            //string strFuselage = Common.prjName + "GRID.dat";
            string grid = Common.prjName + "\\GRID.dat"; //网格文件
            string fu_plot = Common.prjName + "\\SURFACE.DAT"; //物面网格文件
            File.Copy(Common.appFolder + "pre_mesh32.exe", Common.prjName + "\\pre_mesh32.exe", true);//更新预处理文件
            if (!File.Exists(grid)||!File.Exists(fu_plot))
            {//FUSELAGE.dat存在，说明已经转换过了。
                if (string.Compare(Common.prjName + "\\rotorwing_gambit.msh", Common.MESHLOCATION, true) != 0)
                {
                    File.Copy(Common.MESHLOCATION, Common.prjName + "\\rotorwing_gambit.msh", true);
                }
                
                this.proc=Common.RunFile(Common.prjName + "\\pre_mesh32.exe", Common.prjName, PreProcess_OutputHandler);
            }
            else
            {
                if (MessageBox.Show(this,"是否重新转换网格文件", "转换网络", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    this.proc = Common.RunFile(Common.prjName + "\\pre_mesh32.exe", Common.prjName, PreProcess_OutputHandler);
                }
                else
                {
                    this.cmd.HideOpaqueLayer();
                    this.Close();
                }
            }
        }


        /// <summary>
        /// 预处理,多线程异步通信委托事件
        /// </summary>
        /// <param name="sendingProcess"></param>
        /// <param name="outLine"></param>
        private  void PreProcess_OutputHandler(object sendingProcess,DataReceivedEventArgs outLine)
        {
            // Collect the sort command output.
            string data=outLine.Data;
            if (!String.IsNullOrEmpty(outLine.Data))
            {
                this.dataText.Text = outLine.Data;

                if (data.Contains("物面个数："))
                {
                    
                    this.dataText.Text = "准备工况文件......";
                    for (int i = 0; i < MainForm.GKNode.Nodes.Count; i++)
                    {
                        TreeNode node = MainForm.GKNode.Nodes[i];
                        string strGKDir = Common.prjName + "\\" + node.Text + "\\";
                        if (!File.Exists(Common.prjName + Path.DirectorySeparatorChar + "GRID.DAT"))
                        {
                            MessageBox.Show("网格转换失败,没有生成grid.dat文件!");
                            break;
                        }
                        else
                        {
                            this.dataText.Text = node.Text + "工况复制完成。";
                            sGK gk = (sGK)node.Tag;
                            gk.PlanesNum=data.Replace("物面个数：", "").Trim();
                            node.Tag = gk;
                        }

                    }
                    Thread.Sleep(1500);
                    cmd.HideOpaqueLayer();
                    this.Close();
                }

                else if (data == " 完成网格转换。")
                {
                    //this.dataText.Text = "准备工况文件......";
                    //for (int i = 0; i < MainForm.GKNode.Nodes.Count; i++)
                    //{
                    //    string strGKDir = Common.prjName + "\\" + MainForm.GKNode.Nodes[i].Text + "\\";
                    //    if (!File.Exists(Common.prjName + Path.DirectorySeparatorChar + "GRID.DAT"))
                    //    {
                    //        MessageBox.Show("网格转换失败,没有生成grid.dat文件!");
                    //        break;
                    //    }
                    //    else
                    //    {
                    //        this.dataText.Text = MainForm.GKNode.Nodes[i].Text + "工况复制完成。";
                    //    }

                    //}
                    //Thread.Sleep(1500);
                    //cmd.HideOpaqueLayer();
                    //this.Close();
                }
            }
        }


        #endregion

        private void CalculateLoadingForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.proc != null&&!proc.HasExited)
            {
                proc.Kill();
            }
        }
    }
}
