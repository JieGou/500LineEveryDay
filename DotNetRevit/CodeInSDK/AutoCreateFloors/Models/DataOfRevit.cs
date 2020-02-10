using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitDevelopmentFoudation.CodeInSDK.AutoCreateFloors.Models
{
    class DataOfRevit
    {
        private UIDocument uidoc = null;
        private Document doc = null;

        public CurveArrArray CurveArrArray = new CurveArrArray();
        public  List<FloorType> FloorTypes { get; set; }
        public static bool result;
        private List<FamilyInstance> structuralBeams =new List<FamilyInstance>();

        public LevelType currentLevel = null;

        public  DataOfRevit(ExternalCommandData externalCommand)
        {
            uidoc = externalCommand.Application.ActiveUIDocument;
            doc = uidoc.Document;
            FloorTypes =
        }

    }
}
