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
using MyClass;

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0605ShowSelectedElementName : IExternalCommand
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
 


            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();

                Reference reference = sel.PickObject(ObjectType.Element);
                Element elem = doc.GetElement(reference);

                string info = null;


                info += "Category: " + MyTestClass.GetCategoryFromElement(doc, elem);
                info += "\n\t" + "Family: " + MyTestClass.GetFamilyNameFromElement(doc, elem);
                info += "\n\t" + "FamilySymbol: " + MyTestClass.GetFamilySymbolFromElement(doc, elem);

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