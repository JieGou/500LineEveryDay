﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Visual;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa2.BinLibrary.Extensions;
using CodeInTangsengjiewa2.BinLibrary.Helpers;
using CodeInTangsengjiewa2.通用.UIs;


namespace CodeInTangsengjiewa2.CodeOfQian
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_Now_MoveElementByLocation : IExternalCommand
    {
        /// <summary>
        /// what can i do with revit api now?
        /// move wall by location
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;

            Selection sel = uidoc.Selection;

            ElementId eleId = sel.PickObject(ObjectType.Element, doc.GetSelectionFilter(m => m is Wall)).ElementId;

            // doc.Invoke(m => { ElementTransformUtils.MoveElement(doc, eleId, new XYZ(1000d.MmToFeet(), 1000d.MmToFeet(), 0)); },
            //            "移动一片墙");

            doc.Invoke(m =>
            {
                LocationCurve wallLine = eleId.GetElement(doc).Location as LocationCurve;

                XYZ transVec = new XYZ(1000d.MmToFeet(), 1000d.MmToFeet(), 0);

                wallLine.Move(transVec);//墙上的窗户会跟着一起移动
            }, "移动墙通过location");

            return Result.Succeeded;
        }
    }
}