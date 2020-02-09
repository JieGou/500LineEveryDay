using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;


namespace CreateFillPattern
{
    public class Command : IExternalCommand
    {


        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;
            PatternForm wpfPatternForm = new PatternForm(commandData);

            wpfPatternForm.ShowDialog();            
            return Result.Succeeded;
                       
        }
    }
}
