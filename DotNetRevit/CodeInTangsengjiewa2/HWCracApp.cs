using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using CodeInTangsengjiewa2.BinLibrary.Extensions;

#region Copyright
/************************************************************************************ 
 * Copyright (c) 2017Microsoft All Rights Reserved. 
 * CLR版本： 4.0.30319.18444 
 *机器名称：MDELL-PC-XZB 
 *公司名称：Microsoft 
 *命名空间：$rootnamespace$ 
 *文件名：  Class1 
 *版本号：  V1.0.0.0 
 *唯一标识：cab6412c-0eb8-4290-ba35-6f4ebad7bbc4 
 *当前的用户域：MDELL-PC-XZB 
 *创建人：  xuzhaobin 
 *电子邮箱：384785044@qq.com 
 *创建时间：2017/1/13 14:34:10 
 *描述： 
 * 
 *===================================================================== 
 *修改标记 
 *修改时间：2017/1/13 14:34:10 
 *修改人： 徐召彬 
 *版本号： V1.0.0.0 
 *描述： 
 * 
/************************************************************************************/
#endregion

namespace CodeInTangsengjiewa2
{
    class HWCracApp : IExternalApplication

    {
        public Result OnStartup(UIControlledApplication application)
        {
            string bintab = "唐僧工具箱";
            application.CreateRibbonTab(bintab);

            var asmpath = Assembly.GetExecutingAssembly().Location;

            var image = Properties.Resources.hideshow.ToBitmapSource();

            // Type extendwireT = typeof(Cmd_ExtendWire);

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}