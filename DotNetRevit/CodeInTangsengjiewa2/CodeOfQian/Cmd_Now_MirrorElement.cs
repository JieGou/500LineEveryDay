using System;
using System.Collections;
using System.Collections.Generic;
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


namespace CodeInTangsengjiewa2.CodeOfQian
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_Now_MirrorElement : IExternalCommand
    {
        /// <summary>
        /// what can i do with revit api now?
        ///mirror Element:  镜像图元
        ///***************取面的操作没看明白.
        /// </summary>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;

            doc.Invoke(m =>
                       {
                           Wall wall = sel.PickObject(ObjectType.Element, doc.GetSelectionFilter(x => x is Wall))
                                           .GetElement(doc) as Wall;
                           MirrorWall(doc, wall);
                       }
                     , "复制元素1");

            return Result.Succeeded;
        }

        public void MirrorWall(Document doc, Wall wall)
        {
            Reference reference = HostObjectUtils.GetSideFaces(wall, ShellLayerType.Exterior).First();

            //get one of the wall's major side faces
            Face face = wall.GetGeometryObjectFromReference(reference) as Face;

            UV bboxMin = face.GetBoundingBox().Min;
            //create a plane based on the side face with an offset of 10 in the X & Y directions
            Plane plane =
                Plane.CreateByNormalAndOrigin(face.ComputeNormal(bboxMin),
                                              face.Evaluate(bboxMin).Add(new XYZ(10, 10, 0)));
            ElementTransformUtils.MirrorElement(doc, wall.Id, plane);
        }
    }
}