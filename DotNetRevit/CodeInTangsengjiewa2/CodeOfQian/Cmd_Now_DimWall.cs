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
    /// <summary>
    /// what can i do with revit api now?
    /// dim wall
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_Now_DimWall : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Wall wall = doc.GetElement(uidoc.Selection.PickObject(ObjectType.Element)) as Wall;

            if (wall != null)
            {
                ReferenceArray refArry = new ReferenceArray();

                Line wallLine = (wall.Location as LocationCurve).Curve as Line;

                XYZ wallDir = ((wall.Location as LocationCurve).Curve as Line).Direction;

                // wallDir = new XYZ(wallDir.Y, -wallDir.X, 0);

                Options opt = new Options();
                opt.ComputeReferences = true;
                opt.DetailLevel = ViewDetailLevel.Fine;
                GeometryElement gelem = wall.get_Geometry(opt);

                foreach (GeometryObject gobj in gelem)
                {
                    if (gobj is Solid)
                    {
                        Solid solid = gobj as Solid;
                        foreach (Face face in solid.Faces)
                        {
                            if (face is PlanarFace)
                            {
                                XYZ faceDir = face.ComputeNormal(new UV());
                                if (faceDir.IsAlmostEqualTo(wallDir) || faceDir.IsAlmostEqualTo(-wallDir))
                                {
                                    refArry.Append(face.Reference);
                                }
                            }
                        }
                    }
                }
                Transaction trans = new Transaction(doc, "trans");
                trans.Start();
                doc.Create.NewDimension(doc.ActiveView, wallLine, refArry);
                trans.Commit();
            }

            return Result.Succeeded;
        }
    }
}