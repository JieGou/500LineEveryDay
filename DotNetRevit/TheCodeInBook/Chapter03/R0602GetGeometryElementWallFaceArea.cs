using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0602GetGeometryElementWallFaceArea : IExternalCommand
    {
        /// <summary>
        ///演示获取revit文件中墙的各个面的面积
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        ///
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


                FilteredElementCollector collector = new FilteredElementCollector(doc);
                var walls = collector.OfClass(typeof(Wall));

                string info = null;
                foreach (var item in walls)
                {
                    Options options = new Options();
                    GeometryElement geometry = item.get_Geometry(options);
                    int i = 0;
                    foreach (GeometryObject obj in geometry)
                    {
                        Solid solid = obj as Solid;
                        if (solid != null)
                        {
                            FaceArray faceArray = solid.Faces;
                            foreach (Face face in faceArray)
                            {
                                info += item.Id+":";
                                info += "Face" + i + "的面积: " + (face.Area/10.7639).ToString("0.00") + "\n";
                                //face.Area输出的是平方英尺, /10.7639得到平面米
                                i++;
                                //\r回车
                                //\n换行
                                //\r\n连用，表示跳到下一行，并且返回到下一行的起始位置
                                //\t 一个占位符，表式空格
                                //ToString("0.00")能指定输出的小数的格式.
                            }
                        }
                    }
                }

                MessageBox.Show(info, "信息");


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