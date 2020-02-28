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
    class R0801SetLevelBaseType : IExternalCommand
    {
        /// <summary>
        /// 代码片段4-1
        /// 修改标高元素的基面
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



            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();

                Reference pickedEleReference = sel.PickObject(ObjectType.Element);

                Element elment = doc.GetElement(pickedEleReference);

                Level level = elment as Level;

                LevelType levelType =doc.GetElement(level.GetTypeId()) as LevelType;
                Parameter relativeBaseType = levelType.get_Parameter(BuiltInParameter.LEVEL_RELATIVE_BASE_TYPE);
                //相当于revit里编辑level的类型.

                //项目机电是0, 测量点是1;
                relativeBaseType.Set(1);
                TaskDialog.Show("tips", "succeed");

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