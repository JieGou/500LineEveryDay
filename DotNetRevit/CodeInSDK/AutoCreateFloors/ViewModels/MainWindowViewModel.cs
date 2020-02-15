// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using Autodesk.Revit.DB;
// using Autodesk.Revit.UI;
// using RevitDevelopmentFoudation.CodeInSDK.AutoCreateFloors.Helpers;
// using RevitDevelopmentFoudation.CodeInSDK.AutoCreateFloors.Models;
//
// namespace RevitDevelopmentFoudation.CodeInSDK.AutoCreateFloors.ViewModels
// {
//     class MainWindowViewModel : NotificationObject
//     {
//         private Document doc = null;
//         private CurveArrArray curveArrArray = null;
//         private FloorType floorType = null;
//         private List<FloorType> floorTypes = null;
//
//         public DelegateCommand StartCreateCommand { get; set; }
//         private Level currentLevel = null;
//
//         public MainWindowViewModel(ExternalCommandData commandData)
//         {
//             DataOfRevit data = new DataOfRevit(commandData);
//             floorTypes = data.FloorTypes;
//             floorTypesName = data.FloorTypes.ConvertAll(x => x.Name); //
//             currentSelect = floorTypesName.First();
//             curveArrArray = data.curveArrArray;
//
//             prompt = $"预计创建楼板为: {currentSelect}";
//             prompt2 = $"预计创建{curveArrArray.Size} 个楼板";
//             doc = commandData.Application.ActiveUIDocument.Document;
//
//             StartCreateCommand = new DelegateCommand();
//             StartCreateCommand.ExecuteAction = new Action<object>(StartCreate);
//         }
//
//         private string currentSelect;
//
//         public string CurrentSelect
//         {
//             get { return currentSelect; }
//             set
//             {
//                 currentSelect = value;
//                 Prompt = $"预计创建楼板为:{currentSelect}";
//                 floorType = floorTypes.Where(x => x.Name == currentSelect) as FloorType;
//                 RaisePropertyChanged("CurrentSelect");
//             }
//         }
//
//         private string prompt2;
//
//         public string Promot2
//         {
//             get { return prompt2; }
//             set
//             {
//                 prompt2 = value;
//                 RaisePropertyChanged("Prompt2");
//             }
//         }
//
//         private string prompt;
//
//         public string Prompt
//         {
//             get { return prompt; }
//             set
//             {
//                 prompt = value;
//                 RaisePropertyChanged("Prompt");
//             }
//         }
//
//         private List<string> floorTypesName;
//
//         public List<string> FloorTypesName
//         {
//             get { return floorTypesName; }
//             set
//             {
//                 floorTypesName = value;
//                 RaisePropertyChanged("FloorTypesName");
//             }
//         }
//
//         private void StartCreate(object parameter)
//         {
//             Transaction trans = new Transaction(doc, "创建楼板");
//             trans.Start();
//
//             foreach (CurveArray curveArray in curveArrArray)
//             {
//                 doc.Create.NewFloor(curveArray, floorType, currentLevel, true);
//             }
//
//             trans.Commit();
//         }
//     }
// }