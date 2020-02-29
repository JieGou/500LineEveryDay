using System;
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
    public class Cmd_Now_DimTwoPoint : IExternalCommand
    {
        /// <summary>
        /// what can i do with revit api now?
        /// dim wall2
        /// From :https://blog.csdn.net/NancyLee18/article/details/82969370
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            View view = doc.ActiveView;
            ViewType vt = view.ViewType;

            if (vt == ViewType.FloorPlan || vt == ViewType.Elevation)
            {
                Reference eRef =
                    uidoc.Selection.PickObject(ObjectType.Element, "Please pick a curve based element like wall.");
                Element element = doc.GetElement(eRef);
                if (eRef != null && element != null)
                {
                    XYZ dirVec = new XYZ();
                    XYZ viewNormal = view.ViewDirection;

                    LocationCurve locCurve = element.Location as LocationCurve;
                    if (locCurve == null || locCurve.Curve == null)
                    {
                        TaskDialog.Show("Prompt", "Selected element isn’t curve based!");
                        //  return Result.Cancelled;
                    }

                    XYZ dirCur = locCurve.Curve.GetEndPoint(0).Subtract(locCurve.Curve.GetEndPoint(1)).Normalize();
                    double d = dirCur.DotProduct(viewNormal);
                    if (d > -0.000000001 && d < 0.000000001)
                    {
                        dirVec = dirCur.CrossProduct(viewNormal);
                        XYZ p1 = locCurve.Curve.GetEndPoint(0);
                        XYZ p2 = locCurve.Curve.GetEndPoint(1);
                        XYZ dirLine = XYZ.Zero.Add(p1);
                        XYZ newVec = XYZ.Zero.Add(dirVec);
                        newVec = newVec.Normalize().Multiply(3);
                        dirLine = dirLine.Subtract(p2);
                        p1 = p1.Add(newVec);
                        p2 = p2.Add(newVec);
                        Line newLine = Line.CreateBound(p1, p2);
                        ReferenceArray arrRefs = new ReferenceArray();
                        Options options = new Options();
                        options.ComputeReferences = true;
                        options.DetailLevel = ViewDetailLevel.Fine;
                        GeometryElement gelement = element.get_Geometry(options);
                        foreach (var geoObject in gelement)
                        {
                            Solid solid = geoObject as Solid;
                            if (solid == null)
                                continue;

                            FaceArrayIterator fIt = solid.Faces.ForwardIterator();
                            while (fIt.MoveNext())
                            {
                                PlanarFace p = fIt.Current as PlanarFace;
                                if (p == null)
                                    continue;

                                p2 = p.FaceNormal.CrossProduct(dirLine);
                                if (p2.IsZeroLength())
                                {
                                    arrRefs.Append(p.Reference);
                                }
                                if (2 == arrRefs.Size)
                                {
                                    break;
                                }
                            }
                            if (2 == arrRefs.Size)
                            {
                                break;
                            }
                        }
                        if (arrRefs.Size != 2)
                        {
                            TaskDialog.Show("Prompt", "Couldn’t find enough reference for creating dimension");
                            //return Result.Cancelled;
                        }

                        Transaction trans = new Transaction(doc, "create dimension");
                        trans.Start();
                        doc.Create.NewDimension(doc.ActiveView, newLine, arrRefs);
                        trans.Commit();
                    }
                    else
                    {
                        TaskDialog.Show("Prompt", "Selected element isn’t curve based!");
                        // return Result.Cancelled;
                    }
                }
            }
            else
            {
                TaskDialog.Show("Prompt", "Only support Plan View or Elevation View");
            }

            return Result.Succeeded;
        }
    }
}