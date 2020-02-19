// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Windows.Documents;
// using GalaSoft.MvvmLight;
// using Autodesk.Revit;
// using Autodesk.Revit.DB;
// using Autodesk.Revit.UI;
// using CurvedBeamWpf.Model;
// using GalaSoft.MvvmLight.Command;
// using RevitDevelopmentFoudation.CodeInSDK.AutoCreateFloors.Helpers;
//
//
// namespace CurvedBeamWpf.ViewModel
// {
//     /// <summary>
//     /// This class contains properties that the main View can data bind to.
//     /// <para>
//     /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
//     /// </para>
//     /// <para>
//     /// You can also use Blend to data bind with the tool's support.
//     /// </para>
//     /// <para>
//     /// See http://www.galasoft.ch/mvvm
//     /// </para>
//     /// </summary>
//     ///
//     public class CurvedBeamViewModel : ViewModelBase
//     {
//         //实例化Model
//         public DataOfRevitModel DataOfRevitModel { set; get; }
//
//         //定义属性
//
//         // private Document doc = null;
//
//         private List<Level> _LevelTypes;
//
//         public List<Level> LevelTypes
//         {
//             get => _LevelTypes;
//             set
//             {
//                 _LevelTypes = value;
//                 RaisePropertyChanged("LevelTypes");
//             }
//         }
//
//         private List<string> _LevelTypesName;
//
//         public List<string> LevelTypesName
//         {
//             get => _LevelTypesName;
//             set
//             {
//                 _LevelTypesName = value;
//                 RaisePropertyChanged("LevelTypesName");
//             }
//         }
//
//         public Level LevelType = null;
//
//         private string _currentSelectOflevel;
//
//         public string CurrentSelectOfLevel
//         {
//             get { return _currentSelectOflevel; }
//             set
//             {
//                 _currentSelectOflevel = value;
//                 LevelType = _LevelTypes.Where(x => x.Name == _currentSelectOflevel) as Level;
//                 RaisePropertyChanged("CurrentSelectOfLevel");
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
//                 RaisePropertyChanged("BeamTypes");
//             }
//         }
//
//         private List<string> _BeamTypesName;
//
//         public List<string> BeamTypesName
//         {
//             get => _BeamTypesName;
//             set
//             {
//                 _BeamTypesName = value;
//                 RaisePropertyChanged("FloorTypesName");
//             }
//         }
//
//
//         public FamilySymbol BeamType = null;
//
//         private string _currentSelectOfBeam;
//
//         public string CurrentSelectOfBeam
//         {
//             get { return _currentSelectOfBeam; }
//             set
//             {
//                 _currentSelectOfBeam = value;
//                 BeamType = _BeamTypes.Where(x => x.Name == _currentSelectOfBeam) as FamilySymbol;
//                 RaisePropertyChanged("CurrentSelectOfBeam");
//             }
//         }
//
//         public string info = null;
//
//
//         //定义命令
//         public RelayCommand StartCreateBeamArcCommand
//         {
//             get;
//             private set;
//         }
//
//         void ExcuteStartCreateBeamArcCommand()
//         {
//             // info = LevelType.Name + "\n";
//             // info += LevelType.Elevation + "\n";
//             // info += BeamType.Name + "\n";
//             TaskDialog.Show("tips", "info");
//
//             // Arc beamArc = DataOfRevitModel.CreateArc(LevelType.Elevation);
//             // DataOfRevitModel.CreateCurvedBeam(BeamType as FamilySymbol,
//             //                                   beamArc, LevelType);
//
//             // TaskDialog.Show("tips", "测试");
//         }
//
//
//         // 定义命令
//          public RelayCommand StartCreateBeamEllispeCommand
//          {
//              get;
//              private set;
//          }
//         
//          void ExcuteCreateBeamEllispeCommand()
//          {
//              //*****************8
//          }
//
//         // 定义命令
//          public RelayCommand StartCreateBeamSplineCommand
//          {
//              get;
//              private set;
//          }
//         
//          void ExcuteCreateBeamSplineCommand()
//          {
//              //*****************8
//          }
//
//
//         
//
//         /// <summary>
//         /// Initializes a new instance of the MainViewModel class.
//         /// </summary>
//         public CurvedBeamViewModel(ExternalCommandData commandData)
//         {
//             // if (IsInDesignMode)
//             // {
//             //     FloorTypesName = new List<string>() {"11", "22", "33"};
//             // }
//             // else
//             // {
//             //     DataModel = new DataModel(commandData);
//             //     FloorTypes = DataModel.FloorTypes;
//             //     FloorTypesName = DataModel.FloorTypes.ConvertAll(x => x.Name);
//             // }
//
//             DataOfRevitModel = new DataOfRevitModel(commandData);
//
//             BeamTypes = DataOfRevitModel.BeamTypes;
//             BeamTypesName = DataOfRevitModel.BeamTypes.ConvertAll(x => x.Name);
//             CurrentSelectOfBeam = BeamTypesName.First();
//
//             LevelTypes = DataOfRevitModel.LevelTypes;
//             LevelTypesName = DataOfRevitModel.LevelTypes.ConvertAll(x => x.Name);
//             CurrentSelectOfLevel = LevelTypesName.First();
//
//             
//
//             StartCreateBeamArcCommand = new RelayCommand(ExcuteStartCreateBeamArcCommand);
//             StartCreateBeamEllispeCommand = new RelayCommand(ExcuteCreateBeamEllispeCommand);
//             StartCreateBeamSplineCommand = new RelayCommand(ExcuteCreateBeamSplineCommand);
//         }
//
//
//         // bool CanExcuteStartCreateCommand()
//     }
// }