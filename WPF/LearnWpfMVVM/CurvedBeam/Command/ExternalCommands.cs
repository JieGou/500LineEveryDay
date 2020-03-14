using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using LearnWpfMVVM.CurvedBeam.View;
using LearnWpfMVVM.ExportFamilys;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Interop;

namespace LearnWpfMVVM.CurvedBeam.Command
{
    [Transaction(TransactionMode.Manual)]
    internal class ExternalCommands : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            CurvedBeamMainWindow mainWindow = new CurvedBeamMainWindow(commandData);

            //主父窗口为Revit
            IWin32Window revitWindow = new JtWindowHandle(Autodesk.Windows.ComponentManager.ApplicationWindow);
            WindowInteropHelper helper = new WindowInteropHelper(mainWindow)
            {
                Owner = revitWindow.Handle
            };

            mainWindow.ShowDialog();
            return Result.Succeeded;
        }
    }
}