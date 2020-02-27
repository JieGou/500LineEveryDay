// using Autodesk.Revit.UI;
// using Autodesk.Windows;
// using System.Collections.Generic;
// using System.IO;
// using System.Reflection;
// using System.Windows.Media.Imaging;
// using YourCustomUtilityLibrary;
//
// namespace ReallyCoolAddin
// {
//     public class StackedButton
//     {
//         public IList<Autodesk.Revit.RibbonItem> Create(RibbonPanel ribbonPanel)
//         {
//             // Get Assembly
//             Assembly assembly = Assembly.GetExecutingAssembly();
//             string assemblyLocation = assembly.Location;
//
//             // Get DLL Location
//             string executableLocation = Path.GetDirectoryName(assemblyLocation);
//             string dllLocationTest = Path.Combine(executableLocation, "TestDLLName.dll");
//
//             // Set Image
//             BitmapSource pb1Image = UTILImage.GetEmbeddedImage(assembly, "Resources.16x16_Button1.ico");
//             BitmapSource pb2Image = UTILImage.GetEmbeddedImage(assembly, "Resources.16x16_Button2.ico");
//             BitmapSource pb1LargeImage = UTILImage.GetEmbeddedImage(assembly, "Resources.24x24_Button1.ico");
//             BitmapSource pb2LargeImage = UTILImage.GetEmbeddedImage(assembly, "Resources.24x24_Button2.ico");
//
//             // Set Button Name
//             string buttonName1 = "ButtonTest1";
//             string buttonName2 = "ButtonTest2";
//
//             // Create push buttons
//             PushButtonData buttondata1 = new PushButtonData(buttonName1, buttonTextTest, dllLocationTest, "Command1");
//             buttondata1.Image = pb1Image;
//             buttondata1.LargeImage = pb1LargeImage;
//
//             PushButtonData buttondata2 = new PushButtonData(buttonName2, buttonTextTest, dllLocationTest, "Command2");
//             buttondata2.Image = pb2Image;
//             buttondata2.LargeImage = pb2LargeImage;
//
//             // Create StackedItem
//             IList<Autodesk.Revit.RibbonItem> ribbonItem = ribbonPanel.AddStackedItems(buttondata1, buttondata2);
//
//             // Find Autodes.Windows.RibbonItems
//             UTILRibbonItem utilRibbon = new UTILRibbonItem();
//             var btnTest1 = utilRibbon.getButton("Tab", "Panel", buttonName1);
//             var btnTest2 = utilRibbon.getButton("Tab", "Panel", buttonName2);
//
//             // Set Size and Text Visibility
//             btnTest1.Size = RibbonItemSize.Large;
//             btnTest1.ShowText = false;
//             btnTest2.Size = RibbonItemSize.Large;
//             btnTest2.ShowText = false;
//
//             // Return StackedItem
//             return ribbonItem;
//         }
//     }
// }