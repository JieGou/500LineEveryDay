using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;

namespace CodeInTangsengjiewa.BinLibrary.Extensions
{
   public  static class DocumentExtension
    {/// <summary>
     /// 
     /// </summary>
     /// <param name="uidoc"></param>
     /// UIView: revit当中的视图
     /// <returns></returns>
        public static UIView ActiveUiView(this UIDocument uidoc)
        {
            var acview = uidoc.ActiveView;
            var uiviews = uidoc.GetOpenUIViews(); //获取打开的视图
            var acuiview = uiviews.Where(m => acview.Id == m.ViewId).FirstOrDefault();
            return acuiview;

        }
    }
}
