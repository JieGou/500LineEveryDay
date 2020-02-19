using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;


namespace RevitDevelopmentFoudation.CodeInTangsengjiewa.建筑
{
    /// <summary>
    /// 未测试
    /// </summary>

    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]

    class Cmd_BeamAlignToRoofAndFloor:IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            var acview = doc.ActiveView;

            var IsAlignTopFace = false; //根据设置确定
            var IsAlignBottomFace = true; //根据设置确定

            var selectedIds = sel.GetElementIds();

            var selectionCollector = new FilteredElementCollector(doc, selectedIds); //选择集的集合

            var beamFilter = new ElementCategoryFilter(BuiltInCategory.OST_StructuralFraming);
            var roofFilter =new ElementCategoryFilter(BuiltInCategory.OST_Roofs);
            var floorFilter =new ElementCategoryFilter(BuiltInCategory.OST_Floors);
            var rampFilter =new ElementCategoryFilter(BuiltInCategory.OST_Ramps);
            var structuralFoundationFilter =new ElementCategoryFilter((BuiltInCategory.OST_StructuralFoundation));

            var roofCollector = new FilteredElementCollector(doc, selectedIds).WhereElementIsNotElementType().WherePasses(roofFilter);
            var floorCollector = new FilteredElementCollector(doc, selectedIds).WhereElementIsNotElementType().WherePasses(floorFilter);
            var rampCollector = new FilteredElementCollector(doc, selectedIds).WhereElementIsNotElementType().WherePasses(rampFilter);
            var strFoudationCollection = new FilteredElementCollector(doc, selectedIds).WhereElementIsNotElementType().WherePasses(structuralFoundationFilter);
            var beamCollector = new FilteredElementCollector(doc, selectedIds).WhereElementIsNotElementType().WherePasses(beamFilter);

            //1 梁随屋面,将与屋面在同一层的梁进行处理,使之紧贴屋面
            //-1 获取屋面顶面或者地面边界线

            var floorFaces = default(IList<Reference>);

            foreach (Floor floor in floorCollector)
            {
                if (IsAlignBottomFace)
                {
                    floorFaces = HostObjectUtils.GetBottomFaces(floor);

                }
                else if (IsAlignTopFace)
                {
                    floorFaces = HostObjectUtils.GetTopFaces(floor);
                }
                //删除空引用
                floorFaces = floorFaces.Where(m => floor.GetGeometryObjectFromReference(m) as Face != null).ToList();

                if(floorFaces.Count ==0 || floorFaces == null) continue;

                //用屋面边线切断所有投影相交的梁
                foreach (FamilyInstance beam in beamCollector)
                {
                    
                }

            }
                    
            return Result.Succeeded;

        }
    }
}