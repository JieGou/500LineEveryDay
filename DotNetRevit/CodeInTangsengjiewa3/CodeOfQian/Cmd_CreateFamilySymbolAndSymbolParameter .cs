using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace CodeInTangsengjiewa3.CodeOfQian
{
    /// <summary>
    ///Create Family Symbol And Symbol Parameter.
    /// code from:https://www.jianshu.com/p/80833193d73b
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_CreateFamilySymbolAndSymbolParameter : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;

            FamilyTypesParameters(doc);

            return Result.Succeeded;
        }

        public void FamilyTypesParameters(Document doc)
        {
            if (!doc.IsFamilyDocument)
            {
                return;
            }

            using (Transaction t = new Transaction(doc, "family test"))
            {
                t.Start();
                FamilyManager mgr = doc.FamilyManager;
                FamilyParameter param = mgr.AddParameter
                    ("New Parameter", BuiltInParameterGroup.PG_DATA,
                     ParameterType.Text, false); //没错，这里就是我们需要告诉set方法的参数

                for (int i = 0; i < 5; i++)
                {
                    FamilyType newType = mgr.NewType(i.ToString());
                    mgr.CurrentType = newType;
                    mgr.Set(param, "this value" + i); //注意这里的第一个参数就是上面记录的；
                }
                t.Commit();
            }
        }
    }
}