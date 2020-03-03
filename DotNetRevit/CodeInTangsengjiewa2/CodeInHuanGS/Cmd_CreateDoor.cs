using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa2.BinLibrary.Extensions;
using CodeInTangsengjiewa2.BinLibrary.Helpers;


namespace CodeInTangsengjiewa2.CodeInHuanGS
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_CreateDoor : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;

            Transaction ts = new Transaction(doc, "创建梁");
            ts.Start();

            Wall wall =
                sel.PickObject(ObjectType.Element, doc.GetSelectionFilter(m => m is Wall)).GetElement(doc) as Wall;
            CreateDoorsInWall(doc, wall);

            ts.Commit();
            uidoc.RefreshActiveView();
            return Result.Succeeded;
        }

        void CreateDoorsInWall(Document doc, Wall wall)
        {
            string fileName = @"C:\ProgramData\Autodesk\RVT 2020\Libraries\China\消防\建筑\防火门\单扇防火门.rfa";

            Family family = null;
            if (!doc.LoadFamily(fileName, out family))
            {
                throw new Exception("Unable to load " + fileName);
            }

            //get active view's level for door creation
            Level level = doc.ActiveView.GenLevel;

            IEnumerator<Element> symbolEnumrator =
                family.GetFamilySymbolIds().Select(m => m.GetElement(doc)).GetEnumerator();

            double x = 0, y = 0, z = 0;
            while (symbolEnumrator.MoveNext())
            {
                FamilySymbol symbol = symbolEnumrator.Current as FamilySymbol;
                symbol.Activate();
                XYZ location = new XYZ(x, y, z);
                FamilyInstance instance =
                    doc.Create.NewFamilyInstance(location, symbol, wall, level, StructuralType.NonStructural);

                x += 2000d.MmToFeet();
                y += 1000d.MmToFeet(); //由于指定了主体wall, 这个距离会不起作用
                z += 500d.MmToFeet();
            }
        }
    }
}