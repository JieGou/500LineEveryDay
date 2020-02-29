using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Visual;
using Autodesk.Revit.UI;
using CodeInTangsengjiewa2.BinLibrary.Extensions;
using CodeInTangsengjiewa2.BinLibrary.Helpers;
using CodeInTangsengjiewa2.通用.UIs;


namespace CodeInTangsengjiewa2.CodeOfQian
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_Now_CreateLevel : IExternalCommand
    {
        /// <summary>
        /// what can i do with revit api now?
        /// Create Level
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var view = uidoc.ActiveGraphicalView;

            // doc.Invoke(m => { Level.Create(doc, 8000d.MmToFeet()); }, "create level");
            // return Result.Succeeded;

            //根据标高值查找标高的名称
            doc.Invoke(m =>
            {
                Level level = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Levels)
                    .OfClass(typeof(Level)).Cast<Level>()
                    .FirstOrDefault(x => Math.Abs(x.Elevation - 8000d.MmToFeet()) < 1e-6);

                level.get_Parameter(BuiltInParameter.LEVEL_ELEV).Set(10000d.MmToFeet());
                level.get_Parameter(BuiltInParameter.DATUM_TEXT).Set("修改标高名称");

            }, "change level elevation value ");



            return Result.Succeeded;
        }
    }
}