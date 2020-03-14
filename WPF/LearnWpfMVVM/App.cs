using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Itenso.Configuration;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Media.Imaging;

namespace LearnWpfMVVM
{
    public class App : IExternalApplication
    {
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }

        public Result OnStartup(UIControlledApplication app)
        {
            //定义按钮
            RibbonPanel ribbonPanel = app.CreateRibbonPanel("创建梁");
            PushButtonData pushButtonData = new PushButtonData("CurvedBeamBtn",
                "创建梁",
                Assembly.GetExecutingAssembly().Location,
                "LearnWpfMVVM.CurvedBeam.Command.ExternalCommands");
            RibbonButton ribbonButton = ribbonPanel.AddItem(pushButtonData) as RibbonButton;

            var imageFile = PathTool.GetImagePath() + "CurvedBeam.png";
            var largeImageFile = PathTool.GetImagePath() + "CurvedBeam.png";
            var tooltipImageFile = PathTool.GetImagePath() + "CurvedBeam.png";

            if (ribbonButton != null)
            {
                ribbonButton.Image = new BitmapImage(new Uri(imageFile));
                ribbonButton.ToolTipImage = new BitmapImage(new Uri(tooltipImageFile));

                ribbonButton.LargeImage = new BitmapImage(new Uri(largeImageFile));
                ribbonButton.ToolTip = "mvvm模式创建梁";
                ribbonButton.LongDescription = "通过命令能快速的创建梁...\n";

                ContextualHelp help = new ContextualHelp(ContextualHelpType.Url, "http://www.glsbim.com/");
                ribbonButton.SetContextualHelp(help);
            }

            app.ViewActivated += new EventHandler<ViewActivatedEventArgs>(OnViewActivated);

            return Result.Succeeded;
        }

        // From https://thebuildingcoder.typepad.com/blog/2012/02/build-your-own-document-changed-event.html
        /// <summary>
        /// 文档切换事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnViewActivated(object sender, ViewActivatedEventArgs e)
        {
            View vPrevious = e.PreviousActiveView;
            View vCurrent = e.CurrentActiveView;

            var title1 = vPrevious.Document.Title;
            var title2 = vCurrent.Document.Title;

            //切换了文档 两文档的标题不同 需要注意对配制文件进行删除
            if (!title1.Equals(title2, StringComparison.OrdinalIgnoreCase))
            {
                var configurationFilePath = ApplicationSettings.UserConfigurationFilePath;
                if (File.Exists(configurationFilePath))
                {
                    File.Delete(configurationFilePath);
                }
            }
        }
    }
}