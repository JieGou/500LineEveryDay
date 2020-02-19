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
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0302LinearArrayAndRadialArray : IExternalCommand
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
                List<ElementId> elementsToArray = new List<ElementId>();
                string info = "阵列的元素如下:";
               //提示用户多选
                var referenceCollection = uidoc.Selection.PickObjects
                    (ObjectType.Element, "请选择元素");
                foreach (var reference in referenceCollection)
                {
                    var elem = doc.GetElement(reference);
                    elementsToArray.Add(elem.Id);
                    info += "\n\t" + "elem.Id: " + elem.Id + "; elem.GetType" + elem.GetType().ToString();
                }
                TaskDialog.Show("提示", info);
                XYZ translation = new XYZ(0,20,0);
                //创建线性阵列
                // LinearArray.Create
                //     (doc, acview, elementsToArray, 3, translation, ArrayAnchorMember.Second);
                //创造圆弧形阵列
                Line line = Line.CreateBound(new XYZ(0, 0, 0), new XYZ(0, 0, 1));
                RadialArray.Create
                (doc, acview, elementsToArray, 3, line, Math.PI / (180 / 30),ArrayAnchorMember.Second);
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