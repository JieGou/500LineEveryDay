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
    /// Array element
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_Now_ArrayElement : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            doc.Invoke(m =>
            {
                View view = doc.ActiveView;
                ElementId eleId = sel.PickObject(ObjectType.Element).ElementId;
                //表明阵列的相对方向
                XYZ translation = new XYZ(1000d.MmToFeet(), 2000d.MmToFeet(), 0); 
             
                LinearArray.Create(doc, view, eleId, 3, translation, ArrayAnchorMember.Second);
                //count: 阵列后的总数量
                //ArrayAnchorMember.Last :相邻元素的间距为 将translation按count 均分
                //ArrayAnchorMember.Second : 相邻元素的间距为 将translation

            }, "阵列元素");

            return Result.Succeeded;
        }
    }
}