using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using ClassTeacherXu.Helpers;
using View = Autodesk.Revit.DB.View;
namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0206SetNewLocationOfColumnByChangeLocationPoint : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;
            View acview = uidoc.ActiveView;
            UIView acuiview = uidoc.ActiveUiview();
            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();
                //点选指定执行的元素, 本次按只能选择柱考虑
                Reference pickedEleReference = sel.PickObject(ObjectType.Element);
                //通过引用取到选中的元素
                Element element = doc.GetElement(pickedEleReference);
                FamilyInstance column = element as FamilyInstance;
                if (null != column)
                {
                    LocationPoint columnPoint = column.Location as LocationPoint;
                    XYZ newLocation = new XYZ(100, 100, 0);
                    columnPoint.Point = newLocation;
                }
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