using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;

using View = Autodesk.Revit.DB.View;

namespace ExerciseProject
{
    /// <summary>
    /// 使用API来编辑族时, 使用doc.Family.Creat.NewReferencePlan();创建参考平面
    /// </summary>
    /// 
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    [Journaling(JournalingMode.UsingCommandData)]
    class _0504aFamilyCreat : IExternalCommand
    {
        /// <summary>
        ///在族编辑模式下， 创建参考平面
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>

        //创建垂直参考平面( family API),通过平面上两点与向量
        void AddReferncePlane_VerticalOffset(Document doc, View view)
        {
            XYZ pt1 = new XYZ(-0.5, -2.0, 0);
            XYZ pt2 = new XYZ(-0.5, 2.0, 0);
            XYZ vec = XYZ.BasisZ;
            ReferencePlane refPlane = doc.FamilyCreate.NewReferencePlane(pt1, pt2, vec, view);
            refPlane.Name = "OffsetV";
        }

        //添加水平参考平面( family API), 通过平面上两点与向量
        void AddReferencePlane_HorizontalOffset(Document doc, View view)
        {
            XYZ pt1 = new XYZ(2.0, -0.5, 0.0);
            XYZ pt2 = new XYZ(-2.0, -0.5, 0);
            XYZ vec = XYZ.BasisZ;
            ReferencePlane refPlane = doc.FamilyCreate.NewReferencePlane(pt1, pt2, vec, view);
            refPlane.Name = "OffsetH";
        }

 

        //通过类型与名称找Element
        Element findElement(Document _rvtDoc, Type targetType, string targetName)
        {
            //get the elments of the given type
            FilteredElementCollector collector = new FilteredElementCollector(_rvtDoc);
            collector.WherePasses(new ElementClassFilter(targetType));
            //parse the collection for the given name
            //using LINQ query here
            var targetElems = from element in collector
                where element.Name.Equals(targetName)
                select element;
            List<Element> elems = targetElems.ToList<Element>();

            if (elems.Count >0)
            {
                return elems[0];
            }
            //cannot find it
            return null;
        }

        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication app = commandData.Application;
            Document doc = app.ActiveUIDocument.Document;
            Selection sel = app.ActiveUIDocument.Selection;


            

            Transaction ts = new Transaction(doc, "******");
            try
            {
                ts.Start();

                //垂直参考平面
                AddReferncePlane_VerticalOffset(doc, doc.ActiveView);
             //   AddReferencePlane_VerticalOffset2(doc);

                //水平参考平面
                AddReferencePlane_HorizontalOffset(doc, doc.ActiveView);

                ts.Commit();
            }
            catch (Exception)
            {
                if (ts.GetStatus() == TransactionStatus.Started)
                {
                    ts.RollBack();
                }
            }

            return Result.Succeeded;
        }
    }
}