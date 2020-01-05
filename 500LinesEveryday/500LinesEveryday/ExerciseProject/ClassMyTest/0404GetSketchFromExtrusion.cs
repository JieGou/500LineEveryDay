using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using TeacherTangClass;
using View = Autodesk.Revit.DB.View;

namespace ClassMyTest
{
    public static class CreationExtension
    {
        ///失败: 看不用, 不知道怎么用.
        // public static void GetSketchFromExtrusion( this IExternalCommand commandData)
        //
        // {
        //     Document doc = commandData.ActiveUIDocument.Document;
        //     Extrusion extrusion = doc.GetElement(new ElementId(3388)) as Extrusion;
        //     SketchPlane sketchPlane = extrusion.Sketch.SketchPlane;
        //     CurveArrArray sketchProfile = extrusion.Sketch.Profile;
        // }
    }
}