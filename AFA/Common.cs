using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace AFA
{
    public enum TreeNodeType
    {
        /// <summary>
        /// 计算数据
        /// </summary>
        nodeRoot = 0,
        /// <summary>
        /// 工况设置
        /// </summary>
        nodeGK = 1,
        /// <summary>
        /// 工况
        /// </summary>
        nodeGKCase = 2,
        /// <summary>
        /// 计算网格
        /// </summary>
        nodeWG = 3,
        /// <summary>
        /// 网格
        /// </summary>
        nodeWGCase = 4,
        /// <summary>
        /// 求解设置
        /// </summary>
        nodeQJ = 5,
    }

    public class AFATreeNode : TreeNode
    {
        public AFATreeNode(string strText, TreeNodeType NodeType)
            : base(strText)
        {
            m_NodeType = NodeType;
        }
        private TreeNodeType m_NodeType;
        public TreeNodeType NodeType
        {
            get
            {
                return m_NodeType;
            }
            set
            {
                m_NodeType = value;
            }
        }
    }

    public struct sGK
    {
        /// <summary>
        /// 工况名称
        /// </summary>
        public string Name;
        /// <summary>
         /// 1表征仅进行机身气动特性计算
        ///11表征考虑机身和旋翼耦合
        ///101表征考虑机身和进气道耦合
        ///111表征机身/旋翼/进气道耦合
        /// </summary>
        public string MODEL;
        /// <summary>
        /// 机身气动力矩积分作用点
        /// </summary>
        public string FUX;
        public string FUY;
        public string FUZ;
        /// <summary>
        /// 马赫数，对孤立机身分析时使用该数值
        /// </summary>
        public string MACH_INF;
        /// <summary>
        /// 旋翼个数
        /// </summary>
        public int NDISK;
        /// <summary>
        /// 发动机进气流量
        /// </summary>
        public string FLOWRATE;
        /// <summary>
        /// 主旋翼前进比
        /// </summary>
        public string ZMU;
        /// <summary>
        /// 攻角
        /// </summary>
        public string PITCH_V;
        /// <summary>
        /// 偏航角
        /// </summary>
        public string YAW_V;
        /// <summary>
        /// 雷诺数
        /// </summary>
        public string RE;
        /// <summary>
        /// 是否续算标识:0重算;1续算
        /// </summary>
        public string BLCONT;
        /// <summary>
        /// 续算数据
        /// </summary>
        public string DATA;
        /// <summary>
        /// 旋翼数据
        /// </summary>
        public sXY[] XYData;
        /// <summary>
        /// 物面个数
        /// </summary>
        public string PlanesNum;
        /// <summary>
        /// 最大迭代数(用于续算)
        /// </summary>
        public string maxIterateNum;
        /// <summary>
        /// 进气道数据
        /// </summary>
        public Inlet inlet;
    }

    /// <summary>
    /// 进气道
    /// </summary>
    public struct Inlet
    {
        /// <summary>
        /// 进气道出口静压(pa)
        /// </summary>
        public string PRESS_OUT1;
        /// <summary>
        /// 进气道出口总温(k)
        /// </summary>
        public string TEMP_OUT0;
        /// <summary>
        /// 喷管入口静压(pa)
        /// </summary>
        public string PRESS_IN1;
        /// <summary>
        /// 喷管入口总压(pa)
        /// </summary>
        public string PRESS_IN0;
        /// <summary>
        ///喷管入口总温(k)
        /// </summary>
        public string TEMP_IN0;

    }

    /// <summary>
    /// 旋翼
    /// </summary>
    public struct sXY
    {
        /// <summary>
        /// 旋翼半径
        /// </summary>
        public string RADIUS;
        /// <summary>
        /// 旋翼根切半径
        /// </summary>
        public string RADIUSC;
        /// <summary>
        /// 桨叶扭转角
        /// </summary>
        public string TWSIT;
        /// <summary>
        /// 桨叶弦长
        /// </summary>
        public string CHORD;
        /// <summary>
        /// 旋翼旋转方向
        ///0表征负向旋转（逆时针）
        ///1表征正向旋转（顺时针）
        /// </summary>
        public int INVERSE;
        /// <summary>
        /// 桨盘倾倒角度
        /// </summary>
        public string ALF_TPP;
        /// <summary>
        /// 桨盘厚度
        /// </summary>
        public string HDISK;
        /// <summary>
        /// 桨叶片数
        /// </summary>
        public string N_BLADE;
        /// <summary>
        /// 桨叶旋转速度
        /// </summary>
        public string OMIGA;
        /// <summary>
        /// 旋翼前进比
        /// </summary>
        public string MU;
        /// <summary>
        ///旋翼主载荷向量
        ///0表征X轴正向，1表征X轴负向
        ///2表征Y轴正向，3表征Y轴负向
        ///4表征Z轴正向，5表征Z轴负向 
        /// </summary>
        public int DISKAK;
        /// <summary>
        /// 旋翼桨盘中心
        /// </summary>
        public string DISKX;
        public string DISKY;
        public string DISKZ;
        /// <summary>
        /// 旋翼桨叶挥舞角
        /// </summary>
        public string FLAP_0;
        public string FLAP_C1;
        public string FLAP_S1;
        /// <summary>
        /// 旋翼桨叶操纵角
        /// </summary>
        public string PITCH_0;
        public string PITCH_C;
        public string PITCH_S;
        /// <summary>
        /// 旋翼桨叶翼型数据
        /// </summary>
        public string BLADE;
    }
    class Common
    {
        /// <summary>
        /// 应用程序目录
        /// </summary>
        public static string startupFolder = Application.StartupPath + "\\";
        /// <summary>
        /// 翼型目录
        /// </summary>
        public static string yxFolder = Application.StartupPath + "\\YX\\";
        /// <summary>
        /// CMD程序目录
        /// </summary>
        public static string appFolder = Application.StartupPath + "\\APP\\";
        /// <summary>
        /// 项目名称
        /// </summary>
        public static string prjName = Application.StartupPath;
        /// <summary>
        /// CFL数
        /// </summary>
        public static string CFL = "2";
        /// <summary>
        /// 最大迭代步数
        /// </summary>
        public static string ITMAX = "1000";
        /// <summary>
        /// 保存间隔步数
        /// </summary>
        public static string SAVE_STEP = "100";
        /// <summary>
        /// 网格文件路径
        /// </summary>
        public static string MESHLOCATION = string.Empty;

        public static bool TestDoubleData(string strData)
        {
            try
            {
                Convert.ToDouble(strData.Trim());
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region 计算共公模块

        public delegate void OutputHandler(object sendingProcess, DataReceivedEventArgs outLine);

        /// <summary>
        /// 运行程序
        /// </summary>
        /// <param name="appName">程序名</param>
        /// <param name="workDir">工作目录</param>
        /// <param name="handler">回调事件</param>
        /// <param name="exitProcessHandler">退出关闭事件</param>
        public static Process RunFile(string appName, string workDir, OutputHandler handler, EventHandler exitProcessHandler)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = appName;
            proc.StartInfo.Arguments = "";
            proc.StartInfo.WorkingDirectory = workDir;

            proc.StartInfo.UseShellExecute = false;
            //重定向标准输入     
            proc.StartInfo.RedirectStandardInput = true;
            //重定向标准输出
            proc.StartInfo.RedirectStandardOutput = true;
            //增加接收数据委托事件
            proc.OutputDataReceived += new DataReceivedEventHandler(handler);// (PreProcess_OutputHandler);
            //重定向错误输出  
            proc.StartInfo.RedirectStandardError = true;
            //设置不显示窗口
            proc.StartInfo.CreateNoWindow = true;

            //增加退出事件
            if (exitProcessHandler != null)
            {
                proc.EnableRaisingEvents = true;    //一定要有这个才能触发Exited 事件
                proc.Exited += new EventHandler(exitProcessHandler);
            }

            try
            {
                proc.Start();
                proc.BeginOutputReadLine();//开启异步读取输出流
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }

            return proc;
        }



        public static Process RunFile(string appName, string workDir)
        {
            Process proc = new Process();
            proc.StartInfo.FileName = appName;
            proc.StartInfo.Arguments = "";
            proc.StartInfo.WorkingDirectory = workDir;

            proc.StartInfo.UseShellExecute = false;
            //重定向标准输入     
            proc.StartInfo.RedirectStandardInput = true;
            //重定向标准输出
            proc.StartInfo.RedirectStandardOutput = true;
            //重定向错误输出  
            proc.StartInfo.RedirectStandardError = true;
            //设置不显示窗口
            proc.StartInfo.CreateNoWindow = true;

            try
            {
                proc.Start();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
            
            return proc;
            //if (bWaite)
            //{
            //StreamWriter myStreamWriter = proc.StandardInput;

            //while (!proc.HasExited)
            //{
            //String s = proc.StandardOutput.ReadLine();
            //Console.Out.WriteLine(s);
            //this.dataText.Text = s;
            //switch (s)
            //{
            //    case " 是否需要重新生成网格(Y/N)？":
            //        myStreamWriter.WriteLine("y");
            //        Console.Out.WriteLine("y");
            //        break;
            //    case " 机身网格类型：(1.结构网格2.非结构网格)":
            //        myStreamWriter.WriteLine("2");
            //        Console.Out.WriteLine("2");
            //        break;
            //}
            //}
            //proc.WaitForExit();
            //myStreamWriter.Close();
            //proc.Close();
            //String strRst = proc.StandardOutput.ReadToEnd();
            //if (proc.HasExited)
            //{
            //    // MessageBox.Show("结束");
            //} 
            //}
        }

        /// <summary>
        /// 创建或修改工况数据到项目目录下的CFDIN.DAT文件中,并复制求解器(rotorCFD)到项目目录中.
        /// </summary>
        /// <param name="GKNodes">工况结点集</param>
        /// <returns></returns>
        public static bool SaveToCFDIN(List<TreeNode> GKNodes)
        {
            if (Common.MESHLOCATION == string.Empty)
            {
                MessageBox.Show("请在\"计算网格\"节点上点击右键导入网格.");
                return false;
            }
            int iGK = GKNodes.Count; //工况结点集
            string strGKDir = string.Empty;
            string strCFDIN = string.Empty;
            sGK data;
            try
            {
                //创建或修改工况数据到项目目录下的CFDIN.DAT文件中.
                for (int i = 0; i < iGK; i++)
                {
                    strGKDir = Common.prjName + "\\" + GKNodes[i].Text + "\\";
                    if (!Directory.Exists(strGKDir))
                    {
                        Directory.CreateDirectory(strGKDir);
                    }

                    data = (sGK)GKNodes[i].Tag;
                    ;//准备写入工况数据
                    string sp = "   ";
                    using (StreamWriter sw = new StreamWriter(strGKDir + "CFDIN.DAT", false))
                    {
                        sw.WriteLine("INPUT_PARA");
                        sw.WriteLine(sp);
                        sw.WriteLine("MODEL");
                        sw.WriteLine(data.MODEL);
                        sw.WriteLine(sp);
                        sw.WriteLine("FUSELAGE");
                        sw.WriteLine("FUX FUY FUZ");
                        sw.WriteLine(data.FUX + sp + data.FUY + sp + data.FUZ);
                        sw.WriteLine("MACH_INF");
                        sw.WriteLine(data.MACH_INF);
                        sw.WriteLine(sp);
                        sw.WriteLine("FUSELAGE/ROTOR");

                        #region 旋翼
                        sw.WriteLine("NDISK MU");
                        if (data.NDISK == 0)
                        {
                            sw.WriteLine("1  0");
                            sw.WriteLine("DISKX DISKY DISKZ DISKAK RADIUS CUTR TWSIT CHORD INVERSE");
                            sw.WriteLine("0  0  0  0  0  0  0  0  0  0");
                            sw.WriteLine("FLAP_0 FLAP_C1 FLAP_S1 PITCH_0 PITCH_S PITCH_C");
                            sw.WriteLine("0       0        0        0       0        0 ");
                            sw.WriteLine("ALF_TPP HDISK N_BLADE OMIGA");
                            sw.WriteLine("0        0    0       0");
                            sw.WriteLine("BLADE");
                            sw.WriteLine("0");
                        }
                        else
                        {
                            sw.WriteLine(data.NDISK + sp + data.ZMU);
                            sw.WriteLine("DISKX DISKY DISKZ DISKAK RADIUS CUTR TWSIT CHORD INVERSE");
                            for (int j = 0; j < data.NDISK; j++)
                            {
                                sw.WriteLine(data.XYData[j].DISKX + sp + data.XYData[j].DISKY + sp
                                    + data.XYData[j].DISKZ + sp + data.XYData[j].DISKAK + sp
                                    + data.XYData[j].RADIUS + sp + data.XYData[j].RADIUSC + sp
                                    + data.XYData[j].TWSIT + sp + data.XYData[j].CHORD + sp
                                    + data.XYData[j].INVERSE);
                            }
                            sw.WriteLine("FLAP_0 FLAP_C1 FLAP_S1 PITCH_0 PITCH_S PITCH_C");
                            for (int j = 0; j < data.NDISK; j++)
                            {
                                sw.WriteLine(data.XYData[j].FLAP_0 + sp + data.XYData[j].FLAP_C1 + sp
                                    + data.XYData[j].FLAP_S1 + sp + data.XYData[j].PITCH_0 + sp
                                    + data.XYData[j].PITCH_S + sp + data.XYData[j].PITCH_C);
                            }
                            sw.WriteLine("ALF_TPP HDISK N_BLADE OMIGA");
                            for (int j = 0; j < data.NDISK; j++)
                            {
                                sw.WriteLine(data.XYData[j].ALF_TPP + sp + data.XYData[j].HDISK + sp
                                    + data.XYData[j].N_BLADE + sp + data.XYData[j].OMIGA);
                            }
                            sw.WriteLine("BLADE");
                            for (int j = 0; j < data.NDISK; j++)
                            {
                                sw.WriteLine("#" + data.XYData[j].BLADE.Replace('|', '#') + "#");

                                if (data.XYData[j].BLADE != "")
                                {
                                    string[] yx = data.XYData[j].BLADE.Split(new Char[] { '|' });
                                    for (int k = 0; k < yx.Length; k++)
                                    {
                                        string[] yxDGV = yx[k].Split(new Char[] { '#' });
                                        File.Copy(Common.yxFolder + yxDGV[2], strGKDir + yxDGV[2], true);
                                    }
                                }

                            }
                        }
                        
                        sw.WriteLine(sp);
                        #endregion

                        #region 进气道
                        sw.WriteLine("AIRINTAKE");
                        sw.WriteLine("PRESS_OUT1   TEMP_OUT0");
                        sw.WriteLine(data.inlet.PRESS_OUT1 + " " + data.inlet.TEMP_OUT0);
                        sw.WriteLine("PRESS_IN1  PRESS_IN0  TEMP_IN0");
                        sw.WriteLine(data.inlet.PRESS_IN1 + " " + data.inlet.PRESS_IN0 + " " + data.inlet.TEMP_IN0);
                        //sw.WriteLine("FLOWRATE");
                        //sw.WriteLine(data.FLOWRATE);
                        sw.WriteLine(sp);
                        #endregion

                        sw.WriteLine("COMPUTE_PARA");
                        sw.WriteLine("PITCH_V YAW_V RE");
                        sw.WriteLine(data.PITCH_V + sp + data.YAW_V + sp + data.RE);
                        sw.WriteLine("ITMAX SAVE_STEP CFL");
                        sw.WriteLine(Common.ITMAX + sp + Common.SAVE_STEP + sp + Common.CFL);
                        sw.WriteLine(sp);
                        sw.WriteLine("BLCONT");//是否续算:0不续算;1续算.求解器读入到此行不在向下读取. ("BLCONT DATA");
                        sw.WriteLine(data.BLCONT);
                        sw.WriteLine("MESHLOCATION");//网格文件.此参数求解器不读入.
                        sw.WriteLine(Common.prjName + "\\FUSELAGE.dat");
                        sw.WriteLine("PLANESNUM");//物面个数.此参数求解器不读入.
                        sw.WriteLine(data.PlanesNum);
                        sw.Flush();
                        sw.Close();
                    }

                    //拷贝rotorCFD.exe到项目目录下
                    File.Copy(Common.appFolder + "rotorCFD.exe", strGKDir + "rotorCFD.exe", true);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return false;
            }
            return true;
        }

        #endregion

        /// <summary>
        /// 判断OS位数
        /// </summary>
        /// <returns>32或64</returns>
        public static string Is32bitOr64bitOS()
        {

            return null;
            //string addressWidth = String.Empty;
            //ConnectionOptions mConnOption = new ConnectionOptions();
            //ManagementScope mMs = new ManagementScope("//localhost", mConnOption);
            //ObjectQuery mQuery = new ObjectQuery("select AddressWidth from Win32_Processor");
            //ManagementObjectSearcher mSearcher = new ManagementObjectSearcher(mMs, mQuery);
            //ManagementObjectCollection mObjectCollection = mSearcher.Get();
            //foreach (ManagementObject mObject in mObjectCollection)
            //{
            //    addressWidth = mObject["AddressWidth"].ToString();
            //}
            //return addressWidth;
        }
        
        public static int diskakToCmbdisk(int diskak)
        {
            switch (diskak)
            {
                case 1:
                    return 0;
                case -1:
                    return 1;
                case 2:
                    return 2;
                case -2:
                    return 3;
                case 3:
                    return 4;
                case -3:
                    return 5;
            }
            return 0;
        }

        public static int cmbdiskToDiskak(int cmbdisk)
        {
            switch (cmbdisk)
            {
                case 0:
                    return 1;
                case 1:
                    return -1;
                case 2:
                    return 2;
                case 3:
                    return -2;
                case 4:
                    return 3;
                case 5:
                    return -3;
            }
            return 1;
        }
    }
}
