using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CodeInTangsengjiewa2.样板.UIs;

namespace CodeInTangsengjiewa2.样板
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class Cmd_CopyViewCropRegion : IExternalCommand
    {
        /// <summary>
        /// 复制视图裁剪
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                var uiapp = commandData.Application;
                var uidoc = uiapp.ActiveUIDocument;
                var doc = uidoc.Document;
                var acview = doc.ActiveView;

                var collector = new FilteredElementCollector(doc);
                var planviews = collector.OfClass(typeof(ViewPlan)).Where(m => !(m as ViewPlan).IsTemplate).Cast<View>()
                    .OrderBy(m => m.Name);

                ViewSelector selector = new ViewSelector();
                selector.sourceView.ItemsSource = planviews;
                selector.sourceView.DisplayMemberPath = "Title";
                selector.sourceView.SelectedIndex = 0;

                selector.targetViewList.ItemsSource = planviews;
                selector.targetViewList.DisplayMemberPath = "Title";

                selector.ShowDialog();

                var sourceview = selector.sourceView.SelectionBoxItem as View;
                var targetviews = selector.targetViewList.SelectedItems.Cast<ViewPlan>();

                Transaction ts = new Transaction(doc, "复制裁剪");
                ts.Start();

                var boundingbox = sourceview.CropBox;

                foreach (var targetview in targetviews)
                {
                    targetview.CropBox = boundingbox;
                    var para_crop = targetview.get_Parameter(BuiltInParameter.VIEWER_CROP_REGION);
                    var para_crop_visible = targetview.get_Parameter(BuiltInParameter.VIEWER_CROP_REGION_VISIBLE);
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