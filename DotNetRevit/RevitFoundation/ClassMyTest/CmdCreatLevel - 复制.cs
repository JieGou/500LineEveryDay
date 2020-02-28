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
using  System.Windows;
using CodeInTangsengjiewa2.BinLibrary.Helpers;

namespace RevitFoundation.ClassMyTest
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class CmdCreatLevel : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acView = uidoc.ActiveView;
           //获取标高的type
           var types = doc.TCollector<LevelType>().Where(m => m.Name == "下标头");
           var targettype = types.First();
            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();
               var level = Level.Create(doc, 3000 / 304.8);
               level.ChangeTypeId(targettype.Id);
               TaskDialog.Show(targettype.Name,targettype.FamilyName+targettype.Category);
                ts.Commit();
            }
            catch (Exception)
            {
                if (ts.GetStatus() == TransactionStatus.Started)
                {
                    ts.RollBack();
                }
            }
            return Result.Succeeded;

          
        }
    }
}