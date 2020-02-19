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

using View = Autodesk.Revit.DB.View;
using MyClass;
using Form = Autodesk.Revit.DB.Form;

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class R0905WallCreat2 : IExternalCommand
    {
        /// <summary>
        /// 代码片段4-9
        /// 创建墙(wall) 的方法3
        /// Wall.Creat(Document doc, IList<Curve> profile,ElementId wallTypeId,ElementId levelId,boos structural,XYZ normal)
        /// 最后一个参数normal指向墙的朝向. 如果墙的朝向不符合要求,可以使用 Wall.Flip()方法俩调整墙的朝向.
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

                #region 代码4-9

                ElementId levelId = new ElementId(694);
                ElementId wallType = new ElementId(398);
                IList<Curve> curves = new List<Curve>();
                //创建第一面墙
                XYZ[] vertexes = new XYZ[] {new XYZ(0, 0, 0), new XYZ(0, 200, 0), new XYZ(0, 0, 200) };

                for (int i = 0; i < vertexes.Length; i++)
                {
                    if (i != vertexes.Length - 1)
                    {
                        curves.Add(Line.CreateBound(vertexes[i], vertexes[i + 1]));
                    }
                    else
                    {
                        curves.Add(Line.CreateBound(vertexes[i], vertexes[0]));
                    }
                }

                Wall wall = null;
                wall = Wall.Create(doc, curves, wallType, levelId, false, new XYZ(-1, 0, 0));

                //创建第二面墙,朝向相反
                curves.Clear();
                vertexes = new XYZ[] {new XYZ(0, 0, 200), new XYZ(0, 200, 200), new XYZ(0, 200, 0)};
                
                for (int i = 0; i < vertexes.Length; i++)
                {
                    if (i != vertexes.Length-1)
                    {
                        curves.Add(Line.CreateBound(vertexes[i],vertexes[i+1]));
                    }
                    else
                    {
                        curves.Add(Line.CreateBound(vertexes[i],vertexes[0]));
                    }
                }
                wall = Wall.Create(doc, curves, wallType, levelId, false, new XYZ(1, 0, 0));

                TaskDialog.Show("tips", "成功");



                #endregion

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