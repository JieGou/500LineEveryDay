using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Architecture;
using Autodesk.Revit.UI;
using CodeInTangsengjiewa2.BinLibrary.Extensions;

namespace CodeInTangsengjiewa2.CodeOfQian.WpfEventDemo
{
    class Cmd_CreateWall : IExternalEventHandler
    {
        //传值
        public double WallHeight { get; set; }

        public void Execute(UIApplication uiapp)
        {
            //如果是标题栏按钮关闭的,则退出程序, 以避免后续程序执行报错.
            Document doc = uiapp.ActiveUIDocument.Document;

            double height = WallHeight.MmToFeet();
            // double height = 4000d.MmToFeet();
        
            double offset = 1000d.MmToFeet();

            Curve curve = Line.CreateBound(new XYZ(10, 10, 0), new XYZ(0, 0, 0));

            ElementId wallTypeId = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Walls)
                .OfClass(typeof(WallType)).Cast<WallType>().OrderBy(x => x.Name).FirstOrDefault()?.Id;

            // TaskDialog.Show("tips", walltypeId.GetElement(doc).Name);

            ElementId levelId = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Levels)
                .OfClass(typeof(Level)).Cast<Level>().OrderBy(x => x.Elevation).FirstOrDefault()?.Id;

            // TaskDialog.Show("tips", levelId.GetElement(doc).Name);

            Transaction ts = new Transaction(doc, "create wall");
            ts.Start();
            Wall.Create(doc, curve, wallTypeId, levelId, height, offset, false, false);
            ts.Commit();
        }

        public string GetName()
        {
            return "Cmd_CreateWall";
        }
    }
}