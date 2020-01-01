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
using System.Windows;
using teacherTangClass;
using View = Autodesk.Revit.DB.View;

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _06GetAttributesByBilutInParameter : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            View acView = uidoc.ActiveView;
            UIView acuiview = uidoc.ActiveUiview();


            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();


                string info = "length = ";

                Reference pickedEleReference = sel.PickObject(ObjectType.Element);
                //通过引用取到选中的元素
                Element elem = doc.GetElement(pickedEleReference);

                Wall wall = elem as Wall;

                Parameter parameterLength = wall.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH);
                if (parameterLength != null && parameterLength.StorageType == StorageType.Double)
                {
                    double length = parameterLength.AsDouble();
                    info += "\n\t" + length.ToString();
                }
                
                TaskDialog.Show("提示", info);



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