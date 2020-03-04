using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using CodeInTangsengjiewa2.BinLibrary.Extensions;
using CodeInTangsengjiewa2.BinLibrary.Helpers;


namespace CodeInTangsengjiewa2.CodeInHuanGS
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_CreateBeam : IExternalCommand
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

            //get the active view's level for beam creation
            Level level = doc.ActiveView.GenLevel;
            //load a family symbol from file
            FamilySymbol gotSymbol = null;
            string fileName = @"C:\ProgramData\Autodesk\RVT 2020\Libraries\China\结构\框架\钢\堞形梁.rfa";
            string fileNameFromFilePath = System.IO.Path.GetFileNameWithoutExtension(fileName);
            string name = "CB460X28.3";
            FamilyInstance instance = null;

            if (doc.LoadFamilySymbol(fileName, name, out gotSymbol))
            {
                gotSymbol.Activate();
                // look for a model line in the list of selected elements
                ICollection<Element> eles = sel.GetElementIds().Select(m => m.GetElement(doc)).ToList();
                ModelLine modelLine = null;
                foreach (Element ele in eles)
                {
                    if (ele is ModelLine)
                    {
                        modelLine = ele as ModelLine;
                        break;
                    }
                }

                if (null != modelLine)
                {
                    //create new beam
                    instance = doc.Create.NewFamilyInstance(modelLine.GeometryCurve, gotSymbol, level,
                                                            StructuralType.Beam);
                }
                else
                {
                    throw new Exception("Please select a model line before invoking this command");
                }
            }

            else
            {
                throw new Exception("could not load" + fileName);
            }
            ts.Commit();
            return Result.Succeeded;
        }
    }
}