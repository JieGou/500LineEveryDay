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

using View = Autodesk.Revit.DB.View;
namespace ExerciseProject
{
    /// <summary>
    /// 需要在族文档中才可以运行成功
    /// </summary>
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
   

            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();
                if (!doc.IsFamilyDocument)
                {
                    TaskDialog.Show("提示", "不是族文档");
                }
                //创建的参照平面在里面上: 使用两点,切向量,视图,创建新的参考平面
                // //创建的参考平面是在6000高度上的
                // XYZ bubbleEnd = new XYZ(0, 6000 / 304.8, 6000 / 304.8);
                // XYZ freeEnd = new XYZ(6000 / 304.8, 6000 / 304.8, 6000 / 304.8);
                // XYZ cutVector = XYZ.BasisY;
                // //XYZ.BasisY = (0,1,0), 
                // View view = doc.ActiveView;
                // ReferencePlane referencePlane3 = doc.FamilyCreate.NewReferencePlane
                //     (bubbleEnd, freeEnd, cutVector, view);
                //创建的参考平面能在参照标高显示的
                XYZ bubbleEnd = new XYZ(-6000 / 304.8, 6000 / 304.8, 0);
                XYZ freeEnd = new XYZ(6000 / 304.8, 6000 / 304.8, 0);
                XYZ cutVector = XYZ.BasisZ;
                //XYZ.BasisY = (0,1,0), 
                View view = doc.ActiveView;
                ReferencePlane referencePlane6 = doc.FamilyCreate.NewReferencePlane
                    (bubbleEnd, freeEnd, cutVector, view);
                string info = null;
                //起点是freeEnd
                info += "\n\t" + "Plane(平面):" + referencePlane6.GetPlane().Origin;
                //方向是 freeEnd到 bubbleEnd
                info += "\n\t" + "Direction(方向):" + referencePlane6.Direction;
                //法向量由direction 和cutVector根据右手定则得到
                info += "\n\t" + "Normal(法向量):" + referencePlane6.Normal;
                TaskDialog.Show("提示", "创建成功" + "\n\t" + info);
                referencePlane6.Name = "MyReferencePlane6";
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