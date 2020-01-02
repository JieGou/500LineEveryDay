using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using Autodesk.Revit.DB.Events;
using TeacherTangClass;
using View = Autodesk.Revit.DB.View;
using System.Diagnostics;

namespace ExerciseProject
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _09CreatFamilyinstanceFromFamily : IExternalCommand

    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            View acView = uidoc.ActiveView;
            UIView acuiview = uidoc.ActiveUiview();


            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();


                ///在文档中,找名字为"0762x2032"的门类型, 如果没有找到,则加载一个名称为"M_单嵌板4.rfa"的族文件,这样就得到一个族,然后从族中获取名为0762x2032的门类型.
                /// 然后在文档中找到一个直线形的墙,计算墙的重点位置,在此处插入类型为 0762x2032的门.
                /// 

                string doorTypeName = "750 x 2000mm";
                FamilySymbol doorType = null;

                //在文档中找到名字为 "0762 x 2032"的门类型
                ElementFilter doorCategoryFilter = new ElementCategoryFilter(BuiltInCategory.OST_Doors);
                ElementFilter familySymbolFilter = new ElementClassFilter(typeof(FamilySymbol));
                LogicalAndFilter andFilter = new LogicalAndFilter(doorCategoryFilter, familySymbolFilter);

                FilteredElementCollector doorSymbols = new FilteredElementCollector(doc);
                doorSymbols = doorSymbols.WherePasses(andFilter);
                bool symbolFound = false;
                foreach (FamilySymbol element in doorSymbols)
                {
                    if (element.Name == doorTypeName)
                    {
                        symbolFound = true;
                        doorType = element;
                        break;
                    }
                }

                //如果没有找到, 就加载一个族文件
                if (!symbolFound)
                {
                    string file = @"C:\ProgramData\Autodesk\RVT 2019\Libraries\China\建筑\门\普通门\平开门\单扇";
                    Family family;
                    bool loadSuccess = doc.LoadFamily(file, out family);
                    if (loadSuccess)
                    {
                        foreach (var doorTypeId in family.GetValidTypes())
                        {
                            doorType = doc.GetElement(doorTypeId) as FamilySymbol;
                            if (doorType != null)
                            {
                                if (doorType.Name == doorTypeName)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    else
                    {
                        TaskDialog.Show("load family failed", "could not load family file");
                    }
                }

                //使用组类型创建门
                if (doorType != null)
                {
                    //首先找到线性的墙
                    ElementFilter wallFilter = new ElementClassFilter(typeof(Wall));
                    FilteredElementCollector filteredElements = new FilteredElementCollector(doc);
                    filteredElements = filteredElements.WherePasses(wallFilter);

                    Wall wall = null;
                    Line line = null;
                    foreach (Wall element in filteredElements)
                    {
                        LocationCurve locationCurve = element.Location as LocationCurve;
                        if (locationCurve != null)
                        {
                            line = locationCurve.Curve as Line;
                            if (line != null)
                            {
                                wall = element;
                                break;
                            }
                        }
                    }

                    //在墙的中心创建一个门
                    if (wall != null)
                    {
                        XYZ midPoint = (line.GetEndPoint(0) + line.GetEndPoint(1)) / 2;
                        Level wallLevel = doc.GetElement(wall.LevelId) as Level;
                        //创建门: 传入标高参数
                        FamilyInstance door = doc.Create.NewFamilyInstance(midPoint, doorType, wall,
                            wallLevel, Autodesk.Revit.DB.Structure.StructuralType.NonStructural);
                        TaskDialog.Show("成功", door.Id.ToString());
                        Trace.WriteLine("door created" + door.Id.ToString());
                    }

                    else
                    {
                        TaskDialog.Show("失败", "元素不存在,没找到合适的墙");
                    }
                }
                else
                {
                    TaskDialog.Show("失败提示", "没有找到族类型" + doorTypeName);
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