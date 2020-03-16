using Autodesk.Revit.DB.Electrical;
using Autodesk.Windows;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CodeInTangsengjiewa3.BinLibrary.RevitHelper
{
    public static class RevitWindowHelper
    {
        /// <summary>
        /// 获取Revit句柄
        /// </summary>
        /// <returns></returns>
        public static IntPtr GetRevitHandle()
        {
            return Autodesk.Windows.ComponentManager.ApplicationWindow;
        }

        /// <summary>
        /// 获取Revit窗体
        /// </summary>
        /// <returns></returns>
        public static Form GetRevitWindow()
        {
            var handle = ComponentManager.ApplicationWindow;
            var window = System.Windows.Forms.Form.FromChildHandle(handle) as Form;
            return window;
        }

        /// <summary>
        /// 获取Revit窗体(win32)
        /// </summary>
        /// <returns></returns>
        public static IWin32Window GetRevitWindow_win32()
        {
            var handle = ComponentManager.ApplicationWindow;
            var window = System.Windows.Forms.Form.FromChildHandle(handle) as IWin32Window;
            return window;
        }
    }
}