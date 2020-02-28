using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CodeInTangsengjiewa2.BinLibrary.Helpers;
using CodeInTangsengjiewa2.通用.UIs;


namespace CodeInTangsengjiewa2.CodeOfQian
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class EnumViewFamiliy : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var view = uidoc.ActiveGraphicalView;

            //枚举转为数字

            string info = "";

            info += "▲枚举转为int:\n";
            info += (int) ViewFamily.Elevation;
            info += "\n";

            info += "▲枚举转为字符串:\n";
            info += ViewFamily.Elevation.ToString();
            info += "\n";
            info += ViewFamily.Elevation;
            info += "\n";

            info += "▲将整数转为枚举:\n";
            info += ((ViewFamily) 114).ToString();
            info += "\n";

            info += "▲将字符串转为枚举:\n";
            info += (ViewFamily) Enum.Parse(typeof(ViewFamily), "Elevation");
            info += "\n";

            info += @"▲Enum.Format:" + "\n";
            info += Enum.Format(typeof(ViewFamily), 114, "g");
            //format可以输入: G g X x F f G d
            info += "\n";

            info += @"▲Enum.GetName:" + "\n";
            info += Enum.GetName(typeof(ViewFamily), 114);
            //value  是enum的value,  不能是key
            info += "\n";

            info += @"▲Enum.GetUnderlyingType:" + "\n";
            info += Enum.GetUnderlyingType(typeof(ViewFamily));
            info += "\n";

            info += @"▲Enum.GetValues:" + "\n";
            var arrayItems = Enum.GetValues(typeof(ViewFamily));
            foreach (object arrayItem in arrayItems)
            {
                info += arrayItem.ToString() + "\n";
            }
            info += "\n";

            foreach (object arrayItem2 in arrayItems)
            {
                info += (int)arrayItem2 + "\n";
            }
            info += "\n";

            



            TaskDialog.Show("tips", info);
            return Result.Succeeded;
        }
    }
}