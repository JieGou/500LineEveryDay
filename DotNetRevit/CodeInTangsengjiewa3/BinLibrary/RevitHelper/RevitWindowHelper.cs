﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.DB.Electrical;
using Autodesk.Windows;

namespace CodeInTangsengjiewa3.BinLibrary.RevitHelper
{
    public static class RevitWindowHelper
    {
        /// <summary>
        /// 这是做什么用的
        /// </summary>
        /// <returns></returns>
        public static IntPtr GetRevitHandle()
        {
            return Autodesk.Windows.ComponentManager.ApplicationWindow;
        }

        public static Form GetRevitWindow()
        {
            var handle = ComponentManager.ApplicationWindow;
            var window = System.Windows.Forms.Form.FromChildHandle(handle) as Form;
            return window;
        }

        public static IWin32Window GetRevitWindow_win32()
        {
            var handle = ComponentManager.ApplicationWindow;
            var window = System.Windows.Forms.Form.FromChildHandle(handle) as IWin32Window;
            return window;
        }
    }
}