using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;

namespace CodeInTangsengjiewa3.建筑
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_CutFloorWithLine : IExternalCommand
    {
        private Floor floor = null;
        ICollection<ElementId> ids_add = new List<ElementId>();
        private Application App = null;

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            App = app;
            var uidoc = commandData.Application.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;
            var acview = uidoc.ActiveView;

            app.DocumentChanged += OnDocumentChanged;
            


        }

        private void OnIdling(object sender, IdlingEventArgs e)
        {
            var docInApp = App.Documents.Cast<Document>();


        }
    }
}