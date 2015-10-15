//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using Microsoft.VisualBasic;

//namespace AFA
//{
//    /// <summary>
//    /// SYSWARE的COM库封装类
//    /// </summary>
//    public class ComLibrary
//    {
//        #region 成员

//        /// <summary>
//        /// SYSWARE的COM对象名称
//        /// </summary>
//        private const String COM_PROGID = "Sysware.SyswareSDK.SyswareCom.SyswareComServer";

//        /// <summary>
//        /// SYSWARE的COM对象实例
//        /// </summary>
//        private static object _syswareObj = null;

//        #endregion

//        #region 构造函数
//        /// <summary>
//        /// SYSWARE的COM库封装类
//        /// </summary>
//        static ComLibrary()
//        {
//            try
//            {
//                _syswareObj = Interaction.GetObject("", "Sysware.SyswareSDK.SyswareCom.SyswareComServer");
//            }
//            catch (System.Exception ex)
//            {
//                //ConsoleLog.WriteErrorMessage("创建SYSWARE COM对象失败！" + ex.Message);
//            }
//        }
//        #endregion

//        #region 输出信息到Sysware 日志窗口
//        /// <summary>
//        /// 将错误信息显示到sysware的日志窗口中，并且根据stop显示是否停止
//        /// </summary>
//        /// <param name="msg">要显示的信息</param>
//        /// <param name="stop">是否停止</param>
//        public static void PostErrorMessage(String msg, Boolean stop)
//        {
//            try
//            {
//                if (_syswareObj != null)
//                {
//                    Interaction.CallByName(_syswareObj, "PostErrorMessage", CallType.Method,
//                        new object[] { msg, stop });
//                }
//            }
//            catch (System.Exception ex)
//            {
//                ////ConsoleLog.WriteErrorMessage(ex.Message);
//            }
//        }

//        /// <summary>
//        /// 将警告信息显示到sysware的日志窗口中
//        /// </summary>
//        /// <param name="msg">要显示的信息</param>
//        public static void PostWarningMessage(String msg)
//        {
//            try
//            {
//                if (_syswareObj != null)
//                {
//                    Interaction.CallByName(_syswareObj, "PostWarningMessage", CallType.Method,
//                        new object[] { msg });
//                }
//            }
//            catch (System.Exception ex)
//            {
//                ////ConsoleLog.WriteErrorMessage(ex.Message);
//            }
//        }

//        /// <summary>
//        /// 将信息显示到sysware的日志窗口中
//        /// </summary>
//        /// <param name="msg">要显示的信息</param>
//        public static void PostMessage(String msg)
//        {
//            try
//            {
//                if (_syswareObj != null)
//                {
//                    Interaction.CallByName(_syswareObj, "PostMessage", CallType.Method,
//                        new object[] { msg });
//                }
//            }
//            catch (System.Exception ex)
//            {
//                //ConsoleLog.WriteErrorMessage(ex.Message);
//            }
//        }
//        #endregion

//        #region 判断TDE-IDE是否已经启动
//        /// <summary>
//        /// 判断TDE-IDE是否已经启动
//        /// </summary>
//        /// <returns></returns>
//        public static Boolean IsRuntimeServerStarted()
//        {
//            try
//            {
//                if (_syswareObj != null)
//                {
//                    return (Boolean)Interaction.CallByName(_syswareObj, "IsRuntimeServerStarted", CallType.Method);
//                }
//            }
//            catch { }
//            return false;
//        }
//        #endregion

//        #region 获取参数的值
//        /// <summary>
//        /// 获取参数的值
//        /// </summary>
//        /// <param name="context">上下文（默认空字符串）</param>
//        /// <param name="name">参数名</param>
//        /// <returns></returns>
//        public static Object GetParameter(String context, String name)
//        {
//            try
//            {
//                if (_syswareObj != null)
//                {
//                    return Interaction.CallByName(_syswareObj, "GetParameter", CallType.Method,
//                        new object[] { context, name, String.Empty });
//                }
//            }
//            catch (System.Exception ex)
//            {
//                //ConsoleLog.WriteErrorMessage(ex.Message);
//            }
//            return null;
//        }
//        #endregion

//        #region 设置参数的值
//        /// <summary>
//        /// 设置参数的值
//        /// </summary>
//        /// <param name="context">上下文（默认空字符串）</param>
//        /// <param name="name">参数名</param>
//        /// <param name="value">参数值</param>
//        public static void SetParameter(String context, String name, Object value)
//        {
//            try
//            {
//                if (_syswareObj != null)
//                {
//                    Object obj = Interaction.CallByName(_syswareObj, "SetParameter", CallType.Method,
//                        new object[] { context, name, value, String.Empty });
//                }
//            }
//            catch (System.Exception ex)
//            {
//                //ConsoleLog.WriteErrorMessage(ex.Message);
//            }
//        }
//        #endregion

//        #region 获取数组参数的值
//        /// <summary>
//        /// 获取数组参数的值
//        /// </summary>
//        /// <param name="context">上下文（默认空字符串）</param>
//        /// <param name="name">参数名</param>
//        /// <returns></returns>
//        public static Object GetArrayParameter(String context, String name)
//        {
//            try
//            {
//                if (_syswareObj != null)
//                {
//                    return Interaction.CallByName(_syswareObj, "GetArrayParameter", CallType.Method,
//                        new object[] { context, name, String.Empty });
//                }
//            }
//            catch (System.Exception ex)
//            {
//                //ConsoleLog.WriteErrorMessage(ex.Message);
//            }
//            return null;
//        }
//        #endregion

//        #region 设置数组参数的值
//        /// <summary>
//        /// 设置数组参数的值
//        /// </summary>
//        /// <param name="context">上下文（默认空字符串）</param>
//        /// <param name="name">参数名</param>
//        /// <param name="array">数组对象</param>
//        public static void SetArrayParameter(String context, String name, Object array)
//        {
//            try
//            {
//                if (_syswareObj != null)
//                {
//                    Interaction.CallByName(_syswareObj, "SetArrayParameter", CallType.Method,
//                        new object[] { context, name, array, String.Empty });
//                }
//            }
//            catch (System.Exception ex)
//            {
//                //ConsoleLog.WriteErrorMessage(ex.Message);
//            }
//        }
//        #endregion

//        #region 创建整数数组参数
//        /// <summary>
//        /// 创建整数数组参数
//        /// </summary>
//        /// <param name="name">参数名</param>
//        /// <param name="array">数组对象</param>
//        /// <param name="input">是否作为输入参数</param>
//        /// <param name="output">是否作为输出参数</param>
//        /// <param name="gui">是否作为GUI参数</param>
//        /// <returns></returns>
//        /// <returns></returns>
//        public static Boolean CreateIntArrayParameter(String name, Object array, Boolean input, Boolean output, Boolean gui)
//        {
//            try
//            {
//                if (_syswareObj != null)
//                {
//                    return (Boolean)Interaction.CallByName(_syswareObj, "CreateIntArrayParameter", CallType.Method,
//                        new object[] { name, array, input, output, gui });
//                }
//            }
//            catch (System.Exception ex)
//            {
//                //ConsoleLog.WriteErrorMessage(ex.Message);
//            }
//            return false;
//        }
//        #endregion

//        #region 创建实数数组参数
//        /// <summary>
//        /// 创建实数数组参数
//        /// </summary>
//        /// <param name="name">参数名</param>
//        /// <param name="array">数组对象</param>
//        /// <param name="input">是否作为输入参数</param>
//        /// <param name="output">是否作为输出参数</param>
//        /// <param name="gui">是否作为GUI参数</param>
//        /// <returns></returns>
//        /// <returns></returns>
//        public static Boolean CreateDoubleArrayParameter(String name, Object array, Boolean input, Boolean output, Boolean gui)
//        {
//            try
//            {
//                if (_syswareObj != null)
//                {
//                    return (Boolean)Interaction.CallByName(_syswareObj, "CreateDoubleArrayParameter", CallType.Method,
//                        new object[] { name, array, input, output, gui });
//                }
//            }
//            catch (System.Exception ex)
//            {
//                //ConsoleLog.WriteErrorMessage(ex.Message);
//            }
//            return false;
//        }
//        #endregion

//        #region 创建字符串数组参数
//        /// <summary>
//        /// 创建字符串数组参数
//        /// </summary>
//        /// <param name="name">参数名</param>
//        /// <param name="array">数组对象</param>
//        /// <param name="input">是否作为输入参数</param>
//        /// <param name="output">是否作为输出参数</param>
//        /// <param name="gui">是否作为GUI参数</param>
//        /// <returns></returns>
//        /// <returns></returns>
//        public static Boolean CreateStringArrayParameter(String name, Object array, Boolean input, Boolean output, Boolean gui)
//        {
//            try
//            {
//                if (_syswareObj != null)
//                {
//                    return (Boolean)Interaction.CallByName(_syswareObj, "CreateStringArrayParameter", CallType.Method,
//                        new object[] { name, array, input, output, gui });
//                }
//            }
//            catch (System.Exception ex)
//            {
//                //ConsoleLog.WriteErrorMessage(ex.Message);
//            }
//            return false;
//        }
//        #endregion

//        #region 设置指定参数的分组
//        /// <summary>
//        /// 设置指定参数的分组
//        /// </summary>
//        /// <param name="paraName">参数名称</param>
//        /// <param name="groupName">分组名称</param>
//        public static void SetParameterGroup(String paraName, String groupName)
//        {
//            try
//            {
//                if (_syswareObj != null)
//                {
//                    Interaction.CallByName(_syswareObj, "SetParameterGroup", CallType.Method,
//                        new object[] { paraName, groupName });
//                }
//            }
//            catch (System.Exception ex)
//            {
//                // ConsoleLog.WriteErrorMessage(ex.Message);
//            }
//        }
//        #endregion


//        public static Boolean CreateDoubleParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
//        {

//            try
//            {
//                if (_syswareObj != null)
//                {
//                    return (Boolean)Interaction.CallByName(_syswareObj, "CreateDoubleParameter", CallType.Method,
//                        new object[] { name, value, input, output, gui });
//                }
//            }
//            catch (System.Exception ex)
//            {
//                // ConsoleLog.WriteErrorMessage(ex.Message);
//            }
//            return false;
//        }

//        public static Boolean CreateStringParameter(String name, Object value, Boolean input, Boolean output, Boolean gui)
//        {

//            try
//            {
//                if (_syswareObj != null)
//                {
//                    return (Boolean)Interaction.CallByName(_syswareObj, "CreateStringParameter", CallType.Method,
//                        new object[] { name, value, input, output, gui });
//                }
//            }
//            catch (System.Exception ex)
//            {
//                // ConsoleLog.WriteErrorMessage(ex.Message);
//            }
//            return false;
//        }



//        public static Boolean ParseUnit(String unitName)
//        {

//            try
//            {
//                if (_syswareObj != null)
//                {
//                    return (Boolean)Interaction.CallByName(_syswareObj, "ParseUnit", CallType.Method,
//                        new object[] { unitName });
//                }
//            }
//            catch (System.Exception ex)
//            {
//                // ConsoleLog.WriteErrorMessage(ex.Message);
//            }
//            return false;
//        }

//        public static Boolean SetParameterUnit(String context, String name, Object value)
//        {

//            try
//            {
//                if (_syswareObj != null)
//                {
//                    return (Boolean)Interaction.CallByName(_syswareObj, "SetParameterUnit", CallType.Method,
//                        new Object[] { context, name, value });
//                }
//            }
//            catch (System.Exception ex)
//            {
//                // ConsoleLog.WriteErrorMessage(ex.Message);
//            }
//            return false;
//        }


//        //'添加（或者合并）结构化参数
//        public static Boolean AddDataObject(string path)
//        {
//            try
//            {
//                if (_syswareObj != null && path != null)
//                {
//                    return (Boolean)Interaction.CallByName(_syswareObj, "AddDataObject", CallType.Method,
//                        new Object[] { "", path });
//                }
//            }
//            catch (System.Exception ex)
//            {
//                //ConsoleLog.WriteErrorMessage(ex.Message);
//            }
//            return false;
//        }

//        ////'修改结构化参数属性值 节点层级间用点
//        //public static Boolean UpdateDataObjectProperty(string path)
//        //{
//        //    try
//        //    {
//        //        if (_syswareObj != null && path != null)
//        //        {
//        //            return (Boolean)Interaction.CallByName(_syswareObj, "AddDataObject", CallType.Method,
//        //                new Object[] { "", path });
//        //        }
//        //    }
//        //    catch (System.Exception ex)
//        //    {
//        //        ConsoleLog.WriteErrorMessage(ex.Message);
//        //    }
//        //    return false;
//        //}



//    }
//}
