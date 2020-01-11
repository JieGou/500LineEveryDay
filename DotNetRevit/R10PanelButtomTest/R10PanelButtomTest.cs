using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.UI.Selection;
using System.Windows;
using View = Autodesk.Revit.DB.View;

namespace R10PanelButtomTestNameSpace
{
    // public interface IExternalApplication

    class R10PanelButtomTest : IExternalApplication
    {
        public Autodesk.Revit.UI.Result OnStartup(UIControlledApplication application)
        {
            //添加一个新的ribbon面板
            RibbonPanel ribbonPanel = application.CreateRibbonPanel("NewRibbonPanel");

            //在新的面板上添加一个按钮
            PushButton pushButton = ribbonPanel.AddItem(
                    new PushButtonData("HelloRevit",
                        "helloRevit",
                        @"D:\Revit 2019.1 SDK\Add-In Manager\AddInManager.dll",
                        "AddInManager.CAddInManager"))
                as PushButton;
            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}