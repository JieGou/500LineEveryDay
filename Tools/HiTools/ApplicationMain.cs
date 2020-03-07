using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace HiTools
{
    [Transaction(TransactionMode.Manual)]
    class ApplicationMain : IExternalApplication
    {
        public Result OnStartup(UIControlledApplication application)
        {
            //1 创建RibbonTab
            application.CreateRibbonTab("HiTools");
            //2 在tab中创建uiPanel
            RibbonPanel rp = application.CreateRibbonPanel("HiTools", "UIPannel");
            //3 指定程序集的名称及所使用的类名

            string assemblyPath = @"D:\Git\githubRep2\Gitee500LinesEveryday\Tools\HiTools\bin\Debug\HiTools.dll";
            string classNameHelloRevit = "HiTools.Cmd_HelloRevit";

            //4 创建按钮 pushButton
            PushButtonData pbd =
                new PushButtonData("HiToolsInternalName", "HelloRevit", assemblyPath, classNameHelloRevit);

            PushButton pushButton = rp.AddItem(pbd) as PushButton;

            //4-2 给按钮加个图片: 支持png, 大小要求32x32 16x16
            // string imgPath = @"D:\Desktop\hello32x32.png";
            // pushButton.LargeImage =new BitmapImage(new Uri(imgPath));
            pushButton.LargeImage =
                new BitmapImage(new Uri("pack://application:,,,/HiTools;component/Source/HelloRevit32x32.png",
                                        UriKind.Absolute));

            //4-3 给按钮设置一个默认的提示信息
            pushButton.ToolTip = "Hello Revit";

            //4-4 再加一个button HelloWorld;
            string classNameHelloWorld = "HiTools.Cmd_HelloWorld";
            PushButtonData pbd2 =
                new PushButtonData("HiToolsInternalName2", "HelloWorld", assemblyPath, classNameHelloWorld);
            PushButton pushButton2 = rp.AddItem(pbd2) as PushButton;

            // string imgPath2 = @"D:\Desktop\world 32x32.png";
            // pushButton2.LargeImage =new BitmapImage(new Uri(imgPath2));

            pushButton2.LargeImage =
                new BitmapImage(new Uri("pack://application:,,,/HiTools;component/Source/HelloWorld32x32.png",
                                        UriKind.Absolute));
            pushButton2.ToolTip = "Hello World";

            // //注意要点:
            // //1 当前程序集路径
            // string InfoAssemblyPath = Assembly.GetExecutingAssembly().Location;
            // //1.2 当前程序的路径
            // string InfoRevitLocation = Process.GetCurrentProcess().MainModule.FileName;

            //2 在tab中再创建uiPanel StackPanel
            RibbonPanel rp2 = application.CreateRibbonPanel("HiTools", "StackedItem");

            string classNameHelloWorld2 = "HiTools.Cmd_HelloWorld";
            RibbonItemData item1 =
                new PushButtonData("HiToolsInternalName2", "HelloWorld1", assemblyPath, classNameHelloWorld);
            RibbonItemData item2 =
                new PushButtonData("HiToolsInternalName4", "HelloWorld2", assemblyPath, classNameHelloWorld);

            IList<RibbonItem> listRibbon1 = rp2.AddStackedItems(item1, item2);
            PushButton pushButton3 = listRibbon1[0] as PushButton;
            PushButton pushButton4 = listRibbon1[1] as PushButton;
            //如何让按钮不显示名称

            //加图片和提示
            pushButton3.Image =
                new BitmapImage(new Uri("pack://application:,,,/HiTools;component/Source/Image16x16.png",
                                        UriKind.Absolute));
            pushButton3.ToolTip = "Hello World";
            // pushButton3.ItemText.Remove(0);

            pushButton4.Image =
                new BitmapImage(new Uri("pack://application:,,,/HiTools;component/Source/Image16x16.png",
                                        UriKind.Absolute));
            pushButton4.ToolTip = "Hello World";

            return Result.Succeeded;
        }

        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}