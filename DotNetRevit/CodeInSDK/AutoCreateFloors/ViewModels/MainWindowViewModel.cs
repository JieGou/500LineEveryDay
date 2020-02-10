using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitDevelopmentFoudation.CodeInSDK.AutoCreateFloors.Helpers;

namespace RevitDevelopmentFoudation.CodeInSDK.AutoCreateFloors.ViewModels
{
    class MainWindowViewModel : NotificationObject
    {
        private Document doc = null;
        private CurveArrArray curveArrArray = null;
        private FloorType floorType = null;
        private List<FloorType> floorTypes = null;

        public  DelegateCommand StartCreateCommand { get; set; }
        private Level currentLevel = null;

        public MainWindowViewModel(ExternalCommandData commandData)
        {
            DateOfRevit data= new da
        }

    }
}