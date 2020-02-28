using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI.Events;
using Autodesk.Revit.UI.Selection;
using System.Windows;

using View = Autodesk.Revit.DB.View;
namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0105GetPropertyOfElement : IExternalCommand
    {
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
                //点选指定执行的元素
                Reference pickedEleReference = sel.PickObject(ObjectType.Element);
                //通过引用取到选中的元素
                Element elem = doc.GetElement(pickedEleReference);
                ParameterSet parameters = elem.Parameters;
                ElementType elementType = doc.GetElement(elem.GetTypeId()) as ElementType;
                Parameter elementParameter1 = elem.get_Parameter(BuiltInParameter.ELEM_CATEGORY_PARAM_MT);
                Parameter elementParameter2 = elem.get_Parameter(BuiltInParameter.ELEM_CATEGORY_PARAM);
                string info = "属性如下:";
                //获得族名称
                info += "\n\t" + "elementType.FamilyName(族名称):" + elementType.FamilyName;
                info += "\n\t" + "elementType.Nam(族类型):" + elementType.Name;
                info += "\n\t" + " ELEM_CATEGORY_PARAM_MT" + elementParameter1.AsValueString();
                info += "\n\t" + " ELEM_CATEGORY_PARAM" + elementParameter2.AsValueString();
                foreach (Parameter para in parameters)
                {
                    if (para.Definition.Name == "长度" && para.StorageType == StorageType.Double)
                    {
                        string length = para.AsValueString();
                        info += "\n\t" + "(para.Definition.Name= 长度)" + length;
                    }
                }
                TaskDialog.Show("提示", info, TaskDialogCommonButtons.Close);
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