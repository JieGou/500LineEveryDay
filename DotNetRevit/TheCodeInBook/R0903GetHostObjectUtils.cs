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
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Myclass;
using TeacherTangClass;
using View = Autodesk.Revit.DB.View;
using MyClass;
using Form = Autodesk.Revit.DB.Form;

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class R0903GetHostObjectUtils : IExternalCommand
    {
        /// <summary>
        /// 代码片段4-6
        /// 获取楼板的上表面
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
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

                Floor floor = doc.GetElement(new ElementId(352449)) as Floor;
                //获取一个楼板面的引用
                IList<Reference> references = HostObjectUtils.GetTopFaces(floor);

                if (references.Count ==1)
                {
                    var reference = references[0];

                    //从引用获取面的几何对象, 这里是一个PlanarFace
                    GeometryObject topFaceGeo = floor.GetGeometryObjectFromReference(reference);

                    //转成我们想要的对象
                    PlanarFace topFace =topFaceGeo as PlanarFace;
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