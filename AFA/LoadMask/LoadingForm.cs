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

namespace AFA.LoadMask
{
    public partial class LoadingForm : Form
    {

        private OpaqueCommand cmd = new OpaqueCommand();

        private object param;

        public LoadingForm()
        {
            InitializeComponent();
        }

        public delegate void CallLoadBackDelegate(object param, LoadingForm loadingForm);
        private CallLoadBackDelegate LoadCallBack;
        public LoadingForm(object param,CallLoadBackDelegate callBack)
        {
            InitializeComponent();
            this.LoadCallBack = callBack;
            this.param = param;
        }

        private void LoadingForm_Load(object sender, EventArgs e)
        {
            this.cmd.ShowOpaqueLayer(this, 123, true);

            //如果子线程修改主线程控件内容,必须使用(取消线程安全保护模式!),否则报错:线程间操作无效: 从不是创建控件“loadDataText”的线程访问它。
            //LoadingForm.CheckForIllegalCrossThreadCalls = false;

            //MethodInvoker mi=new MethodInvoker(
            
            //Thread thread = new Thread(loadData);//无参线程函数
            //thread.Start();
            Thread thread = new Thread(new ParameterizedThreadStart(this.loadDataFun));//有参线程函数
            thread.IsBackground = true;
            thread.Start(this.param);//传线程函数参数.
            
            //this.thread = new Thread( new ParameterizedThreadStart(loadData));
            //thread.IsBackground = true;
            ////thread.Start();// 无参的回调
            //thread.Start(new CallBackDelegate(callBack)); //有参的回调
            
        }

        /// <summary>
        /// 线程调用函数
        /// </summary>
        /// <param name="callParam"></param>
        private void loadDataFun(object callParam)
        {
            this.LoadCallBack(callParam, this);
            try
            {
                MethodInvoker mi = new MethodInvoker(this.Close);
                this.BeginInvoke(mi);
                //this.Close();
            }
            catch(Exception e)
            {
                MessageBox.Show("loadDataFun:"+e.Message);
            }

        }

        /// <summary>
        /// 进度指示文本
        /// </summary>
        public void setLoadingText(string val)
        {
                this.loadDataText.Text = val;
        }
    }
}



