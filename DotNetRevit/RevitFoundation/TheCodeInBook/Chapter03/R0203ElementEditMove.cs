using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa2.BinLibrary.Helpers;
using View = Autodesk.Revit.DB.View;
namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0203ElementEditMove : IExternalCommand
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
                //获取revit文档的创建句柄
                Autodesk.Revit.Creation.Document creater = doc.Create;
                //创建一根柱子: 使用给点的位置(坐标原点),柱子的类型和标高(高度为0)
                XYZ origin = new XYZ(0, 0, 0);
                //获得level
                Level level = null;
                if (acview.ViewType == ViewType.FloorPlan)
                {
                    level = acview.GenLevel;
                }
                if (level == null)
                {
                    MessageBox.Show("未找到楼层");
                    return Result.Cancelled;
                }
                //获取symbol
                var symbol = doc.TCollector<FamilySymbol>()
                    .Where(m => m.Category.Id.IntegerValue == (int) BuiltInCategory.OST_StructuralColumns).First();
                if (symbol == null)
                {
                    MessageBox.Show("未找到柱子的族符号");
                    return Result.Cancelled;
                }
                //创建柱子
                FamilyInstance colum = creater.NewFamilyInstance(origin, symbol, level,
                    Autodesk.Revit.DB.Structure.StructuralType.Column);
                // 把柱子移动到新的位置
                XYZ newPlace = new XYZ(100, 100, 100);
                ElementTransformUtils.MoveElement(doc, colum.Id, newPlace);
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