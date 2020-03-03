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
    /// <summary>
    /// what can i do with revit api now?
    /// 创建3d视图
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    public class Cmd_Now_Create3DView : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            UIDocument uidoc = commandData.Application.ActiveUIDocument;
            Document doc = uidoc.Document;
            Selection sel = uidoc.Selection;


            Transaction ts =new Transaction(doc,"创建3d视图");
            ts.Start();

            //Find  a 3D view type
            IEnumerable<ViewFamilyType> viewFamilyTypes =
                from elem in new FilteredElementCollector(doc).OfClass(typeof(ViewFamilyType))
                let type = elem as ViewFamilyType
                where type.ViewFamily == ViewFamily.ThreeDimensional
                select type;
            //create new Perspective View3D
            View3D view3D = View3D.CreatePerspective(doc, viewFamilyTypes.First().Id);
            if (null != view3D)
            {
                // by default, the 3d view uses a default orientation.
                XYZ eye = new XYZ(0, -100, 10);
                XYZ up = new XYZ(0, 0, 1);
                XYZ forward = new XYZ(0, 1, 0);
                view3D.SetOrientation(new ViewOrientation3D(eye, up, forward));
                //turn off the far clip plane with standard parameter API
                Parameter farClip = view3D.get_Parameter(BuiltInParameter.VIEWER_BOUND_ACTIVE_FAR);
                farClip.Set(1);
            }
            ts.Commit();


            return Result.Succeeded;
        }
    }
}