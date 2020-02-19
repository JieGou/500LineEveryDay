// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Autodesk.Revit;
// using Autodesk.Revit.DB;
// using Autodesk.Revit.DB.Structure;
// using Autodesk.Revit.UI;
// using GalaSoft.MvvmLight;
//
//
// namespace CurvedBeamWpf.Model
// {
//     public class DataOfRevitModel: ObservableObject
//     {
//         private UIDocument uidoc = null;
//         private Document doc = null;
//
//
//         private List<Level> _LevelTypes;
//
//         public List<Level> LevelTypes
//         {
//             get => _LevelTypes;
//             set
//             {
//                 _LevelTypes = value;
//                 RaisePropertyChanged(() => LevelTypes);
//             }
//         }
//
//
//         private List<FamilySymbol> _BeamTypes;
//
//         public List<FamilySymbol> BeamTypes
//         {
//             get => _BeamTypes;
//             set
//             {
//                 _BeamTypes = value;
//                 RaisePropertyChanged(() => BeamTypes);
//             }
//         }
//
//
//         public DataOfRevitModel(ExternalCommandData externalCommandData)
//         {
//             uidoc = externalCommandData.Application.ActiveUIDocument;
//             doc = uidoc.Document;
//
//             BeamTypes = new FilteredElementCollector(doc)
//                 .OfCategory(BuiltInCategory.OST_StructuralFraming)
//                 .OfClass(typeof(FamilySymbol))
//                 .Cast<FamilySymbol>()
//                 .ToList();
//
//             LevelTypes = new FilteredElementCollector(doc)
//                 .OfCategory(BuiltInCategory.OST_Levels)
//                 .OfClass(typeof(Level))
//                 .Cast<Level>()
//                 .ToList();
//         }
//
//         public Arc CreateArc(double z)
//         {
//             Autodesk.Revit.DB.XYZ center = new Autodesk.Revit.DB.XYZ(0, 0, z);
//             double radius = 20.0;
//             double startAngle = 0.0;
//             double endAngle = 5.0;
//             Autodesk.Revit.DB.XYZ xAxis = new Autodesk.Revit.DB.XYZ(1, 0, 0);
//             Autodesk.Revit.DB.XYZ yAxis = new Autodesk.Revit.DB.XYZ(0, 1, 0);
//             return Arc.Create(center, radius, startAngle, endAngle, xAxis, yAxis);
//         }
//
//         public bool CreateCurvedBeam(FamilySymbol fsBeam, Curve curve, Level level)
//         {
//             FamilyInstance beam;
//
//             try
//             {
//                 if (!fsBeam.IsActive)
//                     fsBeam.Activate();
//                 beam = uidoc.Document.Create.NewFamilyInstance(curve, fsBeam, level, StructuralType.Beam);
//
//                 if (null == beam)
//                 {
//                     return false;
//                 }
//
//                 // get beam location curve
//                 LocationCurve beamCurve = beam.Location as LocationCurve;
//
//                 if (null == beamCurve)
//                 {
//                     return false;
//                 }
//             }
//             catch (Exception ex)
//             {
//                 TaskDialog.Show("Revit", ex.ToString());
//                 return false;
//             }
//
//             // regenerate document
//             doc.Regenerate();
//             return true;
//         }
//     }
// }