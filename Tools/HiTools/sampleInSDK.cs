// using System;
// using System.Collections;
// using System.Collections.Generic;
// using System.Diagnostics;
// using System.Linq;
// using System.Reflection;
// using System.Text;
// using System.Threading.Tasks;
// using System.Windows.Media.Imaging;
// using System.Windows.Navigation;
// using Autodesk.Revit.Attributes;
// using Autodesk.Revit.DB;
// using Autodesk.Revit.UI;
//
// namespace HiTools
// {
//     [Transaction(TransactionMode.Manual)]
//     class ApplicationMain2 : IExternalApplication
//     {
//         public Result OnStartup(UIControlledApplication application)
//         {
//             // add new ribbon panel
//             RibbonPanel ribbonPanel = application.CreateRibbonPanel("NewRibbonPanel");
//
//             //Create a push button in the ribbon panel �NewRibbonPanel"
//             string assembly = @"D:\Sample\HelloWorld\bin\Debug\HelloWorld.dll";
//             PushButton pushButton = ribbonPanel.AddItem(new PushButtonData("Hello Button",
//                                                                            "Hello Button", assembly,
//                                                                            "Hello.HelloButton")) as PushButton;
//
//             // create bitmap image for button
//             Uri uriImage = new Uri(@"D:\Sample\HelloWorld\bin\Debug\39-Globe_32x32.png");
//             BitmapImage largeImage = new BitmapImage(uriImage);
//
//             // assign bitmap to button
//             pushButton.LargeImage = largeImage;
//
//             // assign a small bitmap to button which is used if command
//             // is moved to Quick Access Toolbar
//             Uri uriSmallImage = new Uri(@"D:\Sample\HelloWorld\bin\Debug\39-Globe_16x16.png");
//             BitmapImage smallImage = new BitmapImage(uriSmallImage);
//
//             // assign small image to button
//             pushButton.Image = smallImage;
//
//             // add a vertical separator bar to the panel
//             ribbonPanel.AddSeparator();
//
//             // define 3 new buttons to be added as stacked buttons
//             PushButtonData buttonRed = new PushButtonData("Hello Red", "Hello Red",
//                                                           assembly, "Hello.HelloRed");
//             buttonRed.Image = new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\Red.bmp"));
//             PushButtonData buttonBlue = new PushButtonData("Hello Blue", "Hello Blue",
//                                                            assembly, "Hello.HelloBlue");
//             buttonBlue.Image = new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\Blue.bmp"));
//             PushButtonData buttonGreen = new PushButtonData("Hello Green", "Hello Green",
//                                                             assembly, "Hello.HelloGreen");
//             buttonGreen.Image =
//                 new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\Green.bmp"));
//
//             // add 3 stacked buttons to the panel
//             ribbonPanel.AddStackedItems(buttonRed, buttonBlue, buttonGreen);
//
//             // add a pull-down button to the panel 
//             PulldownButton pulldownButton = ribbonPanel.AddItem(new PulldownButtonData("Hello",
//                                                                                        "Hello 123")) as PulldownButton;
//             pulldownButton.LargeImage =
//                 new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\Hello.bmp"));
//
//             // add some menu items to the pull-down button and assign bitmaps to them
//             PushButton buttonOne = pulldownButton.AddPushButton(new PushButtonData("Hello One",
//                                                                                    "Hello 123", assembly,
//                                                                                    "Hello.HelloOne"));
//             buttonOne.LargeImage =
//                 new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\One.bmp"));
//             PushButton buttonTwo = pulldownButton.AddPushButton(new PushButtonData("Hello Two",
//                                                                                    "Hello 123", assembly,
//                                                                                    "Hello.HelloTwo"));
//             buttonTwo.LargeImage =
//                 new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\Two.bmp"));
//             PushButton buttonThree = pulldownButton.AddPushButton(new PushButtonData("Hello Three",
//                                                                                      "Hello 123", assembly,
//                                                                                      "Hello.HelloThree"));
//             buttonThree.LargeImage =
//                 new BitmapImage(new Uri(@"D:\Sample\HelloWorld\bin\Debug\Three.bmp"));
//
//             return Result.Succeeded;
//         }
//
//         public Result OnShutdown(UIControlledApplication application)
//         {
//             return Result.Succeeded;
//         }
//     }
// }