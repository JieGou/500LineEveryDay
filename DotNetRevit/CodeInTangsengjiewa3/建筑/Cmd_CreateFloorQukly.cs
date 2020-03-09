using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using CodeInTangsengjiewa3.BinLibrary.Extensions;
using CodeInTangsengjiewa3.BinLibrary.Helpers;
using CodeInTangsengjiewa3.通用.UIs;

namespace CodeInTangsengjiewa3.建筑
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_CreateFloorQukly:IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var uiapp = commandData.Application;
            var app = uiapp.Application;
            var uidoc = uiapp.ActiveUIDocument;
            var doc = uidoc.Document;
            var sel = uidoc.Selection;

            var acview = doc.ActiveView;
            var floorTypes = doc.TCollector<FloorType>().ToList();
            var selectorUI = FloorSelector.Instance;
            selectorUI.FloorBox.ItemsSource = floorTypes;
            selectorUI.FloorBox.DisplayMemberPath = "Name";
            selectorUI.FloorBox.SelectedIndex = 0;
            selectorUI.ShowDialog();

            var targetFloorType = selectorUI.FloorBox.SelectedItem as FloorType;
            var beamrefs = default(IList<Reference>);
            try
            {
                beamrefs = sel.PickObjects(ObjectType.Element,
                                          doc.GetSelectionFilter(m => m.Category.Id.IntegerValue ==
                                                                      (int) BuiltInCategory.OST_StructuralFraming), "选择生产板的梁");
            }
            catch (Exception e)
            {
                message = e.ToString();
                MessageBox.Show("用户取消了命令");
                return Result.Cancelled;
            }
            var beams = beamrefs.Select(m => m.GetElement(doc));

            Transaction temtran = new Transaction(doc, "temtran");
            temtran.Start();
            foreach (Element beam in beams)
            {
                var joinedelements = JoinGeometryUtils.GetJoinedElements(doc, beam);
                if (joinedelements.Count >0)
                {
                    foreach (var id in joinedelements)
                    {
                        var temele = id.GetElement(doc);
                        var isjoined = JoinGeometryUtils.AreElementsJoined(doc, beam, temele);
                    }
                }
            }
            temtran.RollBack();
            var solids = new List<GeometryElement>();
            foreach (var element in beams)
            {
                solids.AddRange(element.GetSolids());
            }

        }
    }
}
