using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Visual;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa2.BinLibrary.Extensions;
using CodeInTangsengjiewa2.BinLibrary.Helpers;
using CodeInTangsengjiewa2.通用.UIs;


namespace CodeInTangsengjiewa2.CodeOfQian
{
    /// <summary>
    /// what can i do with revit api now?
    /// CopyElement: 将元素复制到指定的位置
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_Now_CopyElement : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            doc.Invoke(m =>
                       {
                           Element ele = sel.PickObject(ObjectType.Element, "请选择一个元素").GetElement(doc);
                           ICollection<Element> eles = new List<Element>();
                           eles.Add(ele);
                           XYZ newTrans = new XYZ(1000d.MmToFeet(), 2000d.MmToFeet(), 0);
                           var ele2 = ElementTransformUtils.CopyElement(doc, ele.Id, newTrans);
                           string info = "";
                           int i = 0;
                           info += "元素总个数:" + eles.Count;
                           foreach (var elementId in ele2)
                           {
                               i++;
                               info += i + ":\nNew:" + elementId + "\nOld:" + ele.Id;
                           }
                           TaskDialog.Show("tips", info);
                       }
                     , "复制元素1");

            return Result.Succeeded;
        }
    }
}