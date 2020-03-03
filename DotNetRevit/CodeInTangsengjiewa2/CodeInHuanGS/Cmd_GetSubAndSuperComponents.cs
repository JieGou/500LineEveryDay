using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa2.BinLibrary.Extensions;

namespace CodeInTangsengjiewa2.CodeInHuanGS
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_GetSubAndSuperComponents : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;

            FamilyInstance f = sel.PickObject(ObjectType.Element, doc.GetSelectionFilter(m => m is FamilyInstance))
                                   .GetElement(doc) as FamilyInstance;

            GetSubAndSuperComponents(f);

            return Result.Succeeded;
        }

        public void GetSubAndSuperComponents(FamilyInstance familyInstance)
        {
            ICollection<ElementId> subEleSet = familyInstance.GetSubComponentIds();
            if (subEleSet != null)
            {
                string subElementNmaes = "\n";
                foreach (ElementId eId in subEleSet)
                {
                    FamilyInstance f = familyInstance.Document.GetElement(eId) as FamilyInstance;
                    subElementNmaes += f.Name + "\n";
                }

                TaskDialog.Show("SubElement", "Subcomponent count =" + subEleSet.Count + "\n" + subElementNmaes);
            }

            FamilyInstance super = familyInstance.SuperComponent as FamilyInstance;
            if (super != null)
            {
                TaskDialog.Show("SuperComponent", "SuperComponent:" + super.Name);
            }
        }
    }
}