using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.UI.Selection;
using System.Windows;
using View = Autodesk.Revit.DB.View;
using Autodesk.Windows;

namespace R10PanelButtomTestNameSpace
{
    // public interface IExternalApplication

    class R10PanelButtomTest : IExternalApplication
    {
        public Autodesk.Revit.UI.Result OnStartup(UIControlledApplication application)
        {
            //创建新的标签页
            application.CreateRibbonTab("我的工具"); //

            //添加一个新的ribbon面板
            Autodesk.Revit.UI.RibbonPanel ribbonPanel = application.CreateRibbonPanel("我的工具", "New1");

            //在新的面板上添加一个按钮
            PushButton pushButton = ribbonPanel.AddItem(
                    new PushButtonData("AddinManager",
                        "Manual Mode",
                        @"D:\Revit 2019.1 SDK\Add-In Manager\AddInManager.dll",
                        "AddInManager.CAddInManager"))
                as PushButton;

            //添加第二个个新的ribbon面板
            Autodesk.Revit.UI.RibbonPanel ribbonPanel2 = application.CreateRibbonPanel("我的工具", "New2");

            //在新的面板上添加一个按钮
            PushButton pushButton2 = ribbonPanel2.AddItem(
                    new PushButtonData("AddinManager",
                        "Faceless",
                        @"D:\Revit 2019.1 SDK\Add-In Manager\AddInManager.dll",
                        "AddInManager.CAddInManagerFaceless"))
                as PushButton;

            #region 标签设置到最前端

            Autodesk.Windows.RibbonControl ribbon = Autodesk.Windows.ComponentManager.Ribbon;

            Autodesk.Windows.RibbonTab rt = null;

            foreach (Autodesk.Windows.RibbonTab tab in ribbon.Tabs)

            {
                if (tab.Name == "我的工具")

                {
                    rt = tab;
                    break;
                }
            }

            ribbon.Tabs.Remove(rt);
            ribbon.Tabs.Insert(0, rt);

            #endregion

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}