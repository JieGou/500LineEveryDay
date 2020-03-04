using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Visual;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa2.BinLibrary.Extensions;
using CodeInTangsengjiewa2.BinLibrary.Helpers;

namespace CodeInTangsengjiewa2.CodeInHuanGS
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_GetFaces : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            Autodesk.Revit.ApplicationServices.Application app = uiapp.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            // Reference faceReference = sel.PickObject(ObjectType.Face, "选个面");
            // Face face = doc.GetElement(faceReference).GetGeometryObjectFromReference(faceReference) as Face;

            Reference reference = sel.PickObject(ObjectType.Element, "选个东西");
            var elementId = reference.ElementId;

            Options opt = new Options();
            opt.DetailLevel = ViewDetailLevel.Fine;

            GeometryElement geoElem = doc.GetElement(reference).get_Geometry(opt);
            List<Face> faces = geoElem.GetFaces();

            doc.Invoke(m =>
            {
                // Material yellowPaint = doc.GetElement(new ElementId(12859)) as Material;
                ElementId yellowPaintId = new ElementId(12859);

                doc.Paint(elementId, faces[0], yellowPaintId);
                // IList<Asset> objlibraryAsset = app.GetAssets(AssetType.Appearance);
            }, "paint");

            return Result.Succeeded;
        }
    }
}