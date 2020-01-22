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
using ClassTeacherXu.Helpers;
using View = Autodesk.Revit.DB.View;
using MyClass;

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0604GetGeometryElementColumnFaceArea : IExternalCommand
    {
        /// <summary>
        ///演示获取revit文件柱的各个面的面积
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
            UIView acuiview = uidoc.ActiveUiview();


            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();


                FilteredElementCollector collector = new FilteredElementCollector(doc);
                var columns = collector.OfClass(typeof(FamilyInstance));

                string info = null;
                foreach (var item in columns)
                {
                    //info += "\n\t"+"族:" +item.get_Parameter(BuiltInParameter.ELEM_FAMILY_PARAM).AsValueString();

                    info += "\n\t" + "category is:" + MyTestClass.GetCategoryFromElement(doc, item);
                    info += "\n\t" + "family is:" + MyTestClass.GetFamilyNameFromElement(doc, item);
                    info += "\n\t" + "familySymbol is:" + MyTestClass.GetFamilySymbolFromElement(doc, item);
                    // TaskDialog.Show("提示", info);

                    Options options = new Options();
                    GeometryElement geometry = item.get_Geometry(options);
                    int i = 0;
                    foreach (GeometryObject obj in geometry)
                    {
                        // GeometryInstance instance = obj as GeometryInstance;
                        // if (instance != null)
                        //     continue;
                        // GeometryElement geometryElement = instance.GetInstanceGeometry();
                        // if (geometryElement != null)
                        //     continue;
                        // foreach (GeometryObject elem in geometryElement)
                        // {
                        Solid solid = obj as Solid;
                        if (solid != null)
                            //if (solid != null || solid.Volume.ToString() != "0")
                            // ||表示逻辑 或
                        {
                            FaceArray faceArray = solid.Faces;
                            foreach (Face face in faceArray)
                            {
                                info += "\n\t" + "Face" + i + "的面积: " + (face.Area / 10.7639).ToString("0.00");
                                //face.Area输出的是平方英尺, /10.7639得到平面米
                                i++;
                            }
                        }

                        // }
                    }
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