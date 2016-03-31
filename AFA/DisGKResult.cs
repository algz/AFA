using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Diagnostics;
using ZedGraph;

namespace AFA
{
    public partial class DisGKResult : UserControl
    {
        //private Thread thread = null;
        public delegate void CallBackDelegate(string msg);
        private string currenGKPath = "";
        private sGK currentGK ;
        private Process process=null;
        TabPage page = null;
        MainForm mainForm = null;

        //long baseLine = 0;//当前续算基数值.
        long maxLine = 0;//当前迭代步数.  实际当前迭代步数=当前迭代步数+当前续算基数值

        GraphPane myPane = null;
        LineItem myCurve = null;
        //Dictionary<int, List<double>> pointYCollection = new Dictionary<int, List<double>>();
        Dictionary<int, PointPairList> pointYCollection = new Dictionary<int, PointPairList>();

        public int cmbType=0;
        public double[][] a = null;

        public DisGKResult()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pointYCollection"></param>
        /// <param name="isRunRoto">是否运行求解器</param>
        public DisGKResult(TabPage page, Dictionary<int, PointPairList> pointYCollection, bool isRunRoto = true)
        {
            
            InitializeComponent();

            this.page = page;
            this.currenGKPath = Common.prjName + Path.DirectorySeparatorChar + this.page.Text + Path.DirectorySeparatorChar;
            this.myPane = this.zedGraphControl1.GraphPane;

            //设置图标标题和x、y轴标题
            this.myPane.Title.Text = "计算监控";
            this.myPane.XAxis.Title.Text = "迭代步数";


            //更改标题的字体
            //FontSpec myFont = new FontSpec("宋体", 20, Color.Red, false, false, false);
            
            //this.myPane.Title.FontSpec = myFont;
            //this.myPane.XAxis.Title.FontSpec = myFont;
            //this.myPane.YAxis.Title.FontSpec = myFont;

            //设置网格线可见
            myPane.XAxis.MajorGrid.IsVisible = true;
            myPane.YAxis.MajorGrid.IsVisible = true;

            //设置网格线颜色
            myPane.XAxis.MajorGrid.Color = Color.Chocolate;
            myPane.YAxis.MajorGrid.Color = Color.Chocolate;

            //设置网格线形式
            myPane.XAxis.MajorGrid.DashOff = 1;
            myPane.YAxis.MajorGrid.DashOff = 1;
            myPane.XAxis.MajorGrid.DashOn = 4;
            myPane.YAxis.MajorGrid.DashOn = 4;

            //初始化图形
            if (pointYCollection == null||pointYCollection.Count==0)
            {
                this.myCurve = this.myPane.AddCurve("数据", new PointPairList(), Color.Red, SymbolType.None);
            }
            else
            {
                this.pointYCollection = pointYCollection;
                this.myCurve = this.myPane.AddCurve("数据", pointYCollection[0], Color.Red, SymbolType.None);
            }

           

            //监控显示(默认值)
            this.cmbDisType.SelectedIndex = 0;
            this.cmbType = 0;


            this.labGK.Text = this.page.Text;

            DisGKResult.CheckForIllegalCrossThreadCalls = false;
            this.mainForm = (MainForm)this.ParentForm;

            //当前工况
            this.currentGK = (sGK)MainForm.GKNode.Nodes[this.page.Text].Tag;

            //this.baseLine = Convert.ToInt64(this.currentGK.maxIterateNum);

            if (pointYCollection == null && isRunRoto)
            {
                Thread thread = new Thread(loadData);
                thread.IsBackground = true;
                thread.Start();
            }
            else if(pointYCollection.Count!=0)
            {
                try
                {
                    SyswareDataObject sdo = new SyswareDataObject();
                    if (SyswareDataObjectUtil.loadFresultData(sdo, Common.GKPath))
                    {
                        loadDataGridView(sdo.children[0].children);
                    }
                    else
                    {
                        MessageBox.Show("文件加载失败");
                    }
                    
                }
                catch (Exception e)
                {
                    MessageBox.Show("文件格式错误.详细:"+e.Message);
                }

            }
        }

        private void loadDataGridView(List<SyswareDataObject> sdoList)
        {
            int i=this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[i].Cells[0].Value = "迭代步数:" + sdoList[0].value;
            this.dataGridView1.Rows[i].Cells[1].Value = "密度残值:" + sdoList[1].value;

            #region 全机气动载荷
            i = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[i].Cells[0].Value = "全机气动载荷:";
            i = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[i].Cells[0].Value = "全机X方向力";
            this.dataGridView1.Rows[i].Cells[1].Value = "全机Y方向力";
            this.dataGridView1.Rows[i].Cells[2].Value = "全机Z方向力" ;
            this.dataGridView1.Rows[i].Cells[3].Value = "全机X方向力矩";
            this.dataGridView1.Rows[i].Cells[4].Value = "全机Y方向力矩" ;
            this.dataGridView1.Rows[i].Cells[5].Value = "全机Z方向力矩" ;
            i = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[i].Cells[0].Value =   sdoList[2].children[0].value;
            this.dataGridView1.Rows[i].Cells[1].Value =   sdoList[2].children[1].value;
            this.dataGridView1.Rows[i].Cells[2].Value =   sdoList[2].children[2].value;
            this.dataGridView1.Rows[i].Cells[3].Value =  sdoList[2].children[3].value;
            this.dataGridView1.Rows[i].Cells[4].Value =  sdoList[2].children[4].value;
            this.dataGridView1.Rows[i].Cells[5].Value =  sdoList[2].children[5].value;
            #endregion

            #region 旋翼气动载荷
            i = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[i].Cells[0].Value = "旋翼气动载荷:";
            i = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[i].Cells[0].Value = "编号";
            this.dataGridView1.Rows[i].Cells[1].Value =  "旋翼拉力系数";
            this.dataGridView1.Rows[i].Cells[2].Value =  "旋翼扭矩系数";
            this.dataGridView1.Rows[i].Cells[3].Value =  "旋翼滚转力矩系数";
            this.dataGridView1.Rows[i].Cells[4].Value =  "旋翼俯仰力矩系数";
            for (int j = 0; j < sdoList[3].children.Count; j++)
            {
                SyswareDataObject tem = sdoList[3].children[j];

                i = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[i].Cells[0].Value = j+1;
                this.dataGridView1.Rows[i].Cells[1].Value =  tem.children[0].value;
                this.dataGridView1.Rows[i].Cells[2].Value =  tem.children[1].value;
                this.dataGridView1.Rows[i].Cells[3].Value =  tem.children[2].value;
                this.dataGridView1.Rows[i].Cells[4].Value =  tem.children[3].value;
                //this.dataGridView1.Rows[i].Cells[4].Value = (j + 1) + "号旋翼Y方向力矩:" + (tem.children.Count==5?tem.children[4].value:"");
                //this.dataGridView1.Rows[i].Cells[4].Value = (j + 1) + "号旋翼Z方向力矩:" + (tem.children.Count == 6 ? tem.children[5].value : "");
            }

            #endregion

            #region 分部件气动载荷
            i = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[i].Cells[0].Value = "分部件气动载荷:";
            i = this.dataGridView1.Rows.Add();
            this.dataGridView1.Rows[i].Cells[0].Value ="编号";
            this.dataGridView1.Rows[i].Cells[1].Value = "物面X方向力";
            this.dataGridView1.Rows[i].Cells[2].Value = "物面Y方向力";
            this.dataGridView1.Rows[i].Cells[3].Value = "物面Z方向力";
            this.dataGridView1.Rows[i].Cells[4].Value = "物面X方向力矩";
            this.dataGridView1.Rows[i].Cells[5].Value = "物面Y方向力矩";
            this.dataGridView1.Rows[i].Cells[6].Value = "物面Z方向力矩";
            for (int j = 0; j < sdoList[4].children.Count; j++)
            {
                SyswareDataObject tem = sdoList[4].children[j];

                i = this.dataGridView1.Rows.Add();
                this.dataGridView1.Rows[i].Cells[0].Value = j+1;
                this.dataGridView1.Rows[i].Cells[1].Value =  tem.children[0].value;
                this.dataGridView1.Rows[i].Cells[2].Value =  tem.children[1].value;
                this.dataGridView1.Rows[i].Cells[3].Value = tem.children[2].value;
                this.dataGridView1.Rows[i].Cells[4].Value =  tem.children[3].value;
                this.dataGridView1.Rows[i].Cells[5].Value =  tem.children[4].value;
                this.dataGridView1.Rows[i].Cells[6].Value =  tem.children[5].value;
            }
            #endregion
        }
        private void DisGKResult_Load(object sender, EventArgs e)
        {
        }

        private void loadData(object obj)
        {
            //CallBackDelegate cbd = (CallBackDelegate)obj;
            //string msg = "";

            //TabPage page = (TabPage)this.Parent;
            string rotoPath = Common.prjName + Path.DirectorySeparatorChar + this.page.Text + Path.DirectorySeparatorChar;
            if (!Directory.Exists(rotoPath))
            {
                return;
            }

            TreeNode node=MainForm.GKNode.Nodes[this.page.Text];
            sGK gk=(sGK)MainForm.GKNode.Nodes[this.page.Text].Tag;
            if (gk.BLCONT == "1")
            {
                //续算
                if (File.Exists(this.currenGKPath + "listen_roto.dat"))
                {
                    File.Delete(this.currenGKPath + "listen_roto.dat");
                }
                if (File.Exists(this.currenGKPath + "listen_roto.temp"))
                    File.Move(this.currenGKPath + "listen_roto.temp", this.currenGKPath + "listen_roto.dat");//更改文件名,用于续算完成后合并成一个文件.
            }
            else
            {

            }

            this.calcStateLabel.Text = "计算中.";
            this.process = Common.RunFile(rotoPath + "rotorCFD.exe", rotoPath, RotoProcess_OutputHandler, exitProcess);
            if (this.process == null)
            {
                MessageBox.Show("求解器运行失败!");
            }
            else
            {
                MainForm.rotor_Process.Add(this.page.Text,this.process);
            }
        }

        /// <summary>
        /// 进程退出返回事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void exitProcess(object sender, EventArgs e)
        {
            this.calcStateLabel.Text = "计算已完成.";
            //MessageBox.Show("计算已完成.");
        }

        /// <summary>
        /// 异步处理
        /// </summary>
        /// <param name="sendingProcess"></param>
        /// <param name="outLine"></param>
        private void RotoProcess_OutputHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            string data = outLine.Data;
            if (data!=null&&data.Trim()!="")
            {
                if (data.Contains("计算结果输出"))
                {
                    MessageBox.Show("工况("+this.page.Text+")监控完成");
                    return;
                }
                try
                {
                    data = outLine.Data.Trim();
                    //保存原始数据添加到文件
                    using (FileStream fs = File.Open(this.currenGKPath + "listen_roto.temp", FileMode.Append))
                    {
                        using (StreamWriter sw = new StreamWriter(fs))
                        {
                            sw.WriteLine(data);
                        }
                    }

                    string[] arr = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                    drawGraph(arr);

                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message);
                    stopCalculateBtn_Click(null, null);
                    
                }
            }
        }

        public void drawGraph(string[] arr)
        {
            try
            {
                if (arr == null || arr.Length == 0)
                {
                    return;
                }
                //string[] arr = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
                this.maxLine = Convert.ToInt64(arr[0]);
                this.mdcyTxt.Text = arr.Length >= 2 ? arr[1] : "0";
                this.jsslTxt.Text = arr.Length >= 3 ? arr[2] : "0";
                this.jszlTxt.Text = arr.Length >= 4 ? arr[3] : "0";
                this.xyllTxt.Text = arr.Length >= 5 ? arr[4] : "0";
                for (int i = 1; i < arr.Length; i++)
                {
                    double axisY = double.Parse(arr[i], System.Globalization.NumberStyles.Any);
                    if (!this.pointYCollection.ContainsKey(i - 1))
                    {
                        pointYCollection.Add(i - 1, new PointPairList());
                    }
                    this.pointYCollection[i - 1].Add(Convert.ToDouble(arr[0]), Convert.ToDouble(axisY));
                }


                //画到zedGraphControl1控件中，此句必加
                zedGraphControl1.AxisChange();
                //重绘控件
                Refresh();

                this.iterateNum.Text = this.maxLine.ToString();
                this.currentGK.maxIterateNum = this.maxLine.ToString();
                MainForm.GKNode.Nodes[this.page.Text].Tag = this.currentGK;
            }
            catch
            {
            }
            
        }


        /// <summary>
        /// 停止计算
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void stopCalculateBtn_Click(object sender, EventArgs e)
        {
            if (this.process != null && !this.process.HasExited)
            {
                this.process.Kill();
                MessageBox.Show("工况("+this.page.Text+")停止计算");
            }
            SyswareDataObject sdo = new SyswareDataObject();
            if (SyswareDataObjectUtil.loadFresultData(sdo, this.currenGKPath))
            {
                loadDataGridView(sdo.children[0].children);
            }
            
        }

        /// <summary>
        /// 后处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void extProcessBtn_Click(object sender, EventArgs e)
        {
            string workDir=((MainForm)this.ParentForm).tecplot360WorkDir;
            if (workDir == null)
            {
                MessageBox.Show("tecplot360 工作目录没设置");
                return;
            }
            string filePath = this.currenGKPath + @"FU_PLOT.dat";
            if (!File.Exists(filePath))
            {
                MessageBox.Show("网格文件没有生成");
                return;
            }
            workDir = new FileInfo(workDir).DirectoryName;
            workDir = new FileInfo(workDir).DirectoryName;
            //workDir = workDir.Remove(workDir.LastIndexOf("\\bin"));


            try
            {
                Process proc = new Process();
                if (!File.Exists(this.currenGKPath + "FU_PLOT.plt"))
                {
                    proc.StartInfo.FileName = workDir + Path.DirectorySeparatorChar + @"\bin\preplot.exe";// @"D:\Program Files\Tecplot\Tec360 2010\bin\tec360.exe";
                    proc.StartInfo.Arguments = filePath;
                    proc.StartInfo.WorkingDirectory = workDir;//workDir.Remove(workDir.LastIndexOf("\\bin"));
                    proc.StartInfo.UseShellExecute = false;
                    //重定向标准输入     
                    proc.StartInfo.RedirectStandardInput = true;
                    //重定向标准输出
                    proc.StartInfo.RedirectStandardOutput = true;
                    //重定向错误输出  
                    proc.StartInfo.RedirectStandardError = true;
                    //设置不显示窗口
                    proc.StartInfo.CreateNoWindow = true;
                    proc.Start();
                    proc.WaitForExit();
                }
                proc = new Process();
                proc.StartInfo.FileName = workDir + Path.DirectorySeparatorChar + @"\bin\tec360.exe";// @"D:\Program Files\Tecplot\Tec360 2010\bin\tec360.exe";
                proc.StartInfo.Arguments = this.currenGKPath + "FU_PLOT.plt";
                proc.StartInfo.WorkingDirectory = workDir;//workDir.Remove(workDir.LastIndexOf("\\bin"));
                proc.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>
        /// 发布结果
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void publishBtn_Click(object sender, EventArgs e)
        {
            sGK result=new sGK();
            foreach (TreeNode n in MainForm.GKNode.Nodes)
            {
                if (this.page.Text == n.Text)
                {
                    result = (sGK)n.Tag;
                    break;
                }
            }
            //sGK result = (sGK)selNode.Tag;
            string msg = SyswareDataObjectUtil.DataObjectToXml<sGK>(result, "AFA", SyswareDataObjectUtil.saveDataObjectToXml, this.currenGKPath,this.maxLine);
            msg = msg == "" ? "发布完成" : msg;
            MessageBox.Show(msg);
        }

        /// <summary>
        /// 图表显示类型下拉框选择
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbDisType_SelectedValueChanged(object sender, EventArgs e)
        {
            
            this.cmbType = this.cmbDisType.SelectedIndex;
            switch (cmbType)
            {
                case 0:
                    this.myPane.YAxis.Type = AxisType.Log;
                    this.myPane.YAxis.Title.Text = "密度残值";
                    break;
                case 1:
                    this.myPane.YAxis.Type = AxisType.Linear;
                    this.myPane.YAxis.Title.Text = "机身升力";
                    break;
                case 2:
                    this.myPane.YAxis.Type = AxisType.Linear;
                    this.myPane.YAxis.Title.Text = "机身阻力";
                    break;
                case 3:
                    this.myPane.YAxis.Type = AxisType.Linear;
                    this.myPane.YAxis.Title.Text = "旋翼拉力";
                    break;
            }

            //if(this.myCurve!=null)
            //{
            //    //this.myCurve.Clear();
            //}
            if (!this.pointYCollection.ContainsKey(cmbType))
            {
                this.pointYCollection.Add(cmbType, new PointPairList());
            }

            this.myCurve.Points=this.pointYCollection[cmbType];

            //画到zedGraphControl1控件中，此句必加
            zedGraphControl1.AxisChange();

            //重绘控件
            Refresh();
            
        }
    }
}
