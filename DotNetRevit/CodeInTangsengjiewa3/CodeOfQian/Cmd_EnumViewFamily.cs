using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace CodeInTangsengjiewa3.CodeOfQian
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_EnumViewFamily : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var view = doc.ActiveView;

            //枚举转为数字
            string info = "";

            info += "■枚举转为int:\n";
            info += (int) ViewFamily.Elevation;
            //数字转为枚举
            info += "■数字转为枚举:\n";
            info += ((ViewFamily) 114).ToString();

            //枚举转为字符串
            info += "■枚举转为字符串:\n";
            info += ViewFamily.Elevation.ToString();
            info += ViewFamily.Elevation;

            //将字符串转为枚举
            info += "■字符串转为枚举:\n";
            info += (ViewFamily) Enum.Parse(typeof(ViewFamily), "Elevation");

            info += @"■Enum.Format:" + "\n";
            info += Enum.Format(typeof(ViewFamily), 114, "g");
            //format参数可以输入: G g X x F f G d

            info += Enum.GetUnderlyingType(typeof(ViewFamily));

            info += Enum.GetValues(typeof(ViewFamily));

            foreach (var item in Enum.GetNames(typeof(ViewFamily)))
            {
                info += item + "\n";
            }
            foreach (var item in Enum.GetValues(typeof(ViewFamily)))
            {
                info += (int)item + "\n";
            }

            TaskDialog.Show("tips", info);
            return Result.Succeeded;
        }
    }
}