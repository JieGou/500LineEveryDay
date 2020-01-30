using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace WPFIExternalCommandDemo
{
  public class CreatWall : IExternalEventHandler
    {
        public  double wallHeight { get; set; }
        public void Execute(UIApplication app)
        {
            //1  获取当前文档
            Document doc = app.ActiveUIDocument.Document;

            double height = wallHeight;

            //2 获取 CW 102-50-100的墙的族类型
            FilteredElementCollector collector = new FilteredElementCollector(doc);
            Element ele = collector.OfCategory(BuiltInCategory.OST_Walls).OfClass(typeof(WallType))
                .FirstOrDefault(x => x.Name == "常规 - 200mm");
            WallType wallType = ele as WallType;

            //3 获取标高
            Level level =
                new FilteredElementCollector(doc).OfClass(typeof(Level)).FirstOrDefault(x => x.Name == "标高 1") as Level;
            //4 创建线
            XYZ start = new XYZ(0, 0, 0);
            XYZ end = new XYZ(10, 10, 0);
            Line geomLine = Line.CreateBound(start, end);

            XYZ ceshiPoint = new XYZ(0, 0, 0);

            //无连接高度
            // double height = 15 / 0.3048;
            // double height = Convert.ToDouble(MainWindow.TextBox.Text);
            double offset = 0;

            //5 创建事务
            Transaction ts = new Transaction(doc, "******");
            ts.Start();
            Wall wall = Wall.Create(doc, geomLine, wallType.Id, level.Id, height, offset, false, false);

            ts.Commit();
        }

        public string GetName()
        {
            return "CreatWall";
        }
    }
}