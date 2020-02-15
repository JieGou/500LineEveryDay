using System.Collections.Generic;
using System.Linq;
using System.Windows.Documents;
using GalaSoft.MvvmLight;
using ShowAllFloors.Model;
using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using GalaSoft.MvvmLight.Command;
using ShowAllFloors.view;
using ShowAllFloors.Command;

namespace ShowAllFloors.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    ///

   
    public class ShowFloorViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>


        public DataModel DataModel { set; get; }

        private List<FloorType> floorTypes;
        public List<FloorType> FloorTypes
        {
            get { return floorTypes; }
            set
            {
                floorTypes = value;
                RaisePropertyChanged("FloorTypes");
            }
        }

        private List<string> floorTypesName;
        public List<string> FloorTypesName
        {
            get { return floorTypesName; }
            set
            {
                floorTypesName = value;
                RaisePropertyChanged("FloorTypesName");
            }
        }

        private FloorType floorType = null;

        public string currentSelect;
        public string CurrentSelect
        {
            get { return currentSelect; }
            set
            {
                currentSelect = value;
                floorType = floorTypes.Where(x => x.Name ==currentSelect) as FloorType;
                RaisePropertyChanged("CurrentSelect");
            }
        }


       
       
        public RelayCommand StartCreateCommand
        {
            get;
            private set;
        }

        void ExcuteStartCreateCommand()
        {
            TaskDialog.Show("tips", "这是一个绑定命令的演示");
        }

        // bool CanExcuteStartCreateCommand()

        public ShowFloorViewModel(ExternalCommandData commandData)
        {
            // if (IsInDesignMode)
            // {
            //     FloorTypesName = new List<string>() {"11", "22", "33"};
            // }
            // else
            // {
            //     DataModel = new DataModel(commandData);
            //     FloorTypes = DataModel.FloorTypes;
            //     FloorTypesName = DataModel.FloorTypes.ConvertAll(x => x.Name);
            // }

            DataModel = new DataModel(commandData);
            FloorTypes = DataModel.FloorTypes;
            FloorTypesName = DataModel.FloorTypes.ConvertAll(x => x.Name); 
            currentSelect = FloorTypesName.First();

            //初始化命令
            StartCreateCommand =new RelayCommand(ExcuteStartCreateCommand);


            // FloorTypesName = new List<string>() {"11", "22", "33"};
        }
    }
}