using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using GalaSoft.MvvmLight;


namespace ShowAllFloors.Model
{
    public class DataModel : ObservableObject
    {
        private UIDocument uidoc = null;
        private Document doc = null;
        private List<FloorType> floorTypes;
      

        public List<FloorType> FloorTypes
        {
            get => floorTypes;
            set
            {
                floorTypes = value;
                RaisePropertyChanged(() => FloorTypes);
            }
        }

        public DataModel(ExternalCommandData externalCommandData)
        {
            uidoc = externalCommandData.Application.ActiveUIDocument;
            doc = uidoc.Document;
            FloorTypes = new FilteredElementCollector(doc).OfCategory(BuiltInCategory.OST_Floors)
                .OfClass(typeof(FloorType))
                .Cast<FloorType>()
                .ToList();
        }
    }
}