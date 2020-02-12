using System.Collections.Generic;
using System.Windows.Documents;
using GalaSoft.MvvmLight;
using ShowAllFloors.Model;
using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
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
            // FloorTypesName = new List<string>() {"11", "22", "33"};
        }
    }
}