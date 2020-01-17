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
using Myclass;
using TeacherTangClass;
using View = Autodesk.Revit.DB.View;
using MyClass;
using Form = Autodesk.Revit.DB.Form;

namespace TeacherTangClass.Extensions
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class ForcedDisplay : IExternalCommand
    {
        /// <summary>
        /// 强制显示
        /// </summary>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            var acView = doc.ActiveView;
            Transaction ts = new Transaction(doc, "******");

            try
            {
                ts.Start();

                FilteredElementCollector collection = new FilteredElementCollector(doc);
                collection.WhereElementIsNotElementType();
                var list = new List<ElementId>();

                foreach (Element i in collection)
                {
                    list.Add(i.Id);
                }

                MessageBox.Show((list.Count.ToString()));
                acView.UnhideElements(list);

                ts.Commit();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.ToString());

                if (ts.GetStatus() == TransactionStatus.Started)
                {
                    ts.RollBack();
                }
            }

            return Result.Succeeded;
        }
    }
}