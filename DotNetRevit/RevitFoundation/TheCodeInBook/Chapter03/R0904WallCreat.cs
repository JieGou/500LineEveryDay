using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using View = Autodesk.Revit.DB.View;

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class R0904WallCreat : IExternalCommand
    {
        /// <summary>
        /// 代码片段4-7
        /// 创建墙(wall) 的方法1
        /// 代码片段4-8
        /// 创建墙(wall) 的方法2  Wall.Creat(Document doc, IList<Curve> profile,bool structure) 给点墙的曲线,从默认墙类型创建墙, 则可以创建一个梯形的墙
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

                #region  代码片段4-7
                ElementId levelId = new ElementId(341705);
                Wall wall = Wall.Create(doc, Line.CreateBound(new XYZ(0, 0, 4000/304.8), new XYZ(4000 / 304.8, 4000 / 304.8,4000/304.8)),
                    levelId, false);
                //levelId 是墙的下口标高,会覆盖Line的z坐标.
                
                TaskDialog.Show("Tips:", "创建成功");
                #endregion
                

                #region  代码片段4-8
                IList<Curve> curves = new List<Curve>();
                curves.Add(Line.CreateBound(new XYZ(100, 20, 0), new XYZ(100, -20, 0)));
                curves.Add(Line.CreateBound(new XYZ(100, -20, 0), new XYZ(100, -10, 10)));
                curves.Add(Line.CreateBound(new XYZ(100, -10, 10), new XYZ(100, 10, 10)));
                curves.Add(Line.CreateBound(new XYZ(100, 10, 10), new XYZ(100, 20, 0)));
                //曲线形状是墙的正面,即创建的是一面墙,而不是一些列墙.
                //形状必须闭合,否则抛异常.
                //能否创建斜墙呢?

                Wall wall2 = Wall.Create(doc, curves, false);
                TaskDialog.Show("Tips", "梯形墙创建成功");
                
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