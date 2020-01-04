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
    class _0401SketchPlaneAndCreatModelCurve : IExternalCommand
    {
        /// <summary>
        /// 代码演示从文档中找到名字为"Ref.Level"的标高, 以此创建一个新的草图平面.
        /// 然后用几何点(0,0,0)到点(10,10,0)的直线创建一条模型曲线
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
            View acview = uidoc.ActiveView;
            UIView acuiview = uidoc.ActiveUiview();
            //在族文档中找到名字为Ref.Level的标高
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            collector = collector.OfCategory(BuiltInCategory.OST_Levels);
            // var levelElements = from element in collector
            //     where element.Name == "Ref.Level"
            //     select element;
            // List<Autodesk.Revit.DB.Element> levels =
            //     levelElements.ToList<Autodesk.Revit.DB.Element>();


            #region

            //上面的代码试试用下面的表达式能否运行
            List<Autodesk.Revit.DB.Element> levels
                = collector.Where(m => m.Name == "Ref.Level").ToList();

            #endregion

            //C#使用 ToList()将枚举转化为List集合,例如
            //string[] strArr = new string[3] { "1", "2", "3" };
            // List<string> strList = strArr.ToList();


            if (levels.Count <= 0)
            {
                TaskDialog.Show("提示", "找不到标高");
            }
            else
            {
                TaskDialog.Show("提示", "找到标高个数:" + levels.Count);
            }


            Level refLevel = levels[0] as Level;

            ElementId levelId = refLevel.LevelId;
            string info = null;
            info += "\n\t" + "refLevel.Id:" + refLevel.Id;
            info += "\n\t" + "refLevel.Id:" + refLevel.LevelId;
            TaskDialog.Show("提示", info);
            //创建一条几何直线,一个基于标高的草图平面,然后在这个草图平面上创建一条模型线


            Line line = Line.CreateBound(XYZ.Zero, new XYZ(60000 / 304.8, 60000 / 304.8, 40000 / 304.8));
            SketchPlane sketchPlane = SketchPlane.Create(doc, levelId);
            ModelCurve modelLine = doc.Create.NewModelCurve(line, sketchPlane);
            TaskDialog.Show("提示", "成功");


            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();


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