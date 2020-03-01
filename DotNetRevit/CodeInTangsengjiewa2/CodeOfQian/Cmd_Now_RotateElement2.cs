using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Xml;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Visual;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa2.BinLibrary.Extensions;
using CodeInTangsengjiewa2.BinLibrary.Helpers;
using CodeInTangsengjiewa2.通用.UIs;
using Point = Autodesk.Revit.DB.Point;


namespace CodeInTangsengjiewa2.CodeOfQian
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_Now_RotateElement2 : IExternalCommand
    {
        /// <summary>
        /// what can i do with revit api now?
        /// CopyElement: 旋转一个柱子
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            var app = uiapp.Application;// Autodesk.Revit.ApplicationServices.Application

            doc.Invoke(m =>
                       {
                           //假设是一片墙
                           Element ele = sel.PickObject(ObjectType.Element, "请选择一个元素").GetElement(doc);


                           LocationRotate(app, ele);

                           
                       }
                     , "旋转元素1");

            return Result.Succeeded;
        }

        bool LocationRotate(Autodesk.Revit.ApplicationServices.Application app, Autodesk.Revit.DB.Element element)
        {
            bool rotated = false;

            LocationCurve curve = element.Location as LocationCurve;
            if (null != curve)
            {
                Curve line = curve.Curve;
                XYZ aa = line.GetEndPoint(0);
                XYZ cc = new XYZ(aa.X, aa.Y, aa.Z + 10);
                Line axis = Line.CreateBound(aa, cc);
                rotated = curve.Rotate(axis, Math.PI / 2);
            }

            return rotated;
        }
    }
}