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

using View = Autodesk.Revit.DB.View;
using Form = Autodesk.Revit.DB.Form;

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class R0805GetCompoundStructure : IExternalCommand
    {
        /// <summary>
        /// 代码片段4-5
        /// 获取墙每层厚度和材质
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
                string info = null;

                Wall wall = doc.GetElement(new ElementId(348910)) as Wall;
                CompoundStructure compoundStructure = wall.WallType.GetCompoundStructure();

                if (compoundStructure != null)
                {
                    if (compoundStructure.LayerCount > 0)

                    {
                        foreach (CompoundStructureLayer compoundStructureLayer in compoundStructure.GetLayers())
                        {
                            //获得材质和厚度
                            ElementId materialId = compoundStructureLayer.MaterialId;
                            double layerWidth = compoundStructureLayer.Width * 304.8;
                            info += "\n:materialId :" + materialId;
                            info += "\nlayerWidth :" + layerWidth;
                        }
                    }

                    TaskDialog.Show("Tips", info);
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