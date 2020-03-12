using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CodeInTangsengjiewa3.样板.UIs;

namespace CodeInTangsengjiewa3.样板
{
    /// <summary>
    /// 复制视图裁剪
    /// </summary>
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_CopyViewCropRegion : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                var uiapp = commandData.Application;
                var uidoc = uiapp.ActiveUIDocument;
                var doc = uidoc.Document;
                var acview = doc.ActiveView;

                var collector = new FilteredElementCollector(doc);
                var planViews = collector.OfClass(typeof(ViewPlan)).Where(m => !(m as ViewPlan).IsTemplate).Cast<View>()
                    .OrderBy(m => m.Name);

                ViewSelector selector = new ViewSelector();
                selector.sourceView.ItemsSource = planViews;
                selector.sourceView.DisplayMemberPath = "Title";
                selector.sourceView.SelectedIndex = 0;

                selector.targetViewList.ItemsSource = planViews;
                selector.targetViewList.DisplayMemberPath = "Title";

                selector.ShowDialog();

                var sourceView = selector.sourceView.SelectionBoxItem as View;
                var targetViews = selector.targetViewList.SelectedItems.Cast<ViewPlan>();

                Transaction ts = new Transaction(doc, "复制裁剪");
                ts.Start();
                var boundingBox = sourceView.CropBox;
                foreach (var targetView in targetViews)
                {
                    targetView.CropBox = boundingBox;
                    var para_crop = targetView.get_Parameter(BuiltInParameter.VIEWER_CROP_REGION);
                    var para_crop_visible = targetView.get_Parameter(BuiltInParameter.VIEWER_CROP_REGION_VISIBLE);
                    para_crop_visible.Set(1);
                    para_crop.Set(1);
                }
                ts.Commit();
                selector.Close();
            }
            catch (Exception e)
            {
                message = e.ToString();
                return Result.Cancelled;
            }
            return Result.Succeeded;
        }
    }
}