using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using TeacherTangClass;
using View = Autodesk.Revit.DB.View;
namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0304CreatReferencePlaneInFamilyDocument : IExternalCommand
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
                if (!doc.IsFamilyDocument)
                {
                    TaskDialog.Show("提示","不是族文档");
                }

                //创建的参照平面在里面上
                XYZ bubbleEnd = new XYZ(0, 100, 100);
                XYZ freeEnd = new XYZ(100, 100, 100);
                XYZ cutVector = XYZ.BasisY;
                View view = doc.ActiveView;
                ReferencePlane referencePlane = doc.FamilyCreate.NewReferencePlane
                    (bubbleEnd, freeEnd, cutVector, view);
                TaskDialog.Show("提示", "创建成功");
                referencePlane.Name = "MyReferencePlane";

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