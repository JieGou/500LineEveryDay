using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

using View = Autodesk.Revit.DB.View;

using Form = Autodesk.Revit.DB.Form;

namespace RevitDevelopmentFoundation.Chapter04
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class R0416CreateColumn : IExternalCommand
    {
        /// <summary>
        /// 代码片段4-16 创建柱子
        /// </summary>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acView = uidoc.ActiveView;

            Transaction ts = new Transaction(doc, "******");

            try
            {
                ts.Start();

                FamilySymbol familySymbol = doc.GetElement(new ElementId(346524)) as FamilySymbol;
                Level level = doc.GetElement(new ElementId(311)) as Level;
                FamilyInstance familyInstance =
                    doc.Create.NewFamilyInstance(new XYZ(0, 0, 0), familySymbol,level, StructuralType.NonStructural);
                TaskDialog.Show("tips", "柱子创建成功");
                //level是底标高



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