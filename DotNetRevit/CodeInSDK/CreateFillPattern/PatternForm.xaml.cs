using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Autodesk.Revit;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CreateFillPattern.Annotations;
using ProgressChangedEventArgs = Autodesk.Revit.DB.Events.ProgressChangedEventArgs;
using RvtView = Autodesk.Revit.DB.View;

namespace CreateFillPattern
{
    /// <summary>
    /// UserControl1.xaml 的交互逻辑
    /// </summary>
    public partial class PatternForm : Window
    {
        UIDocument docUI;

        Document doc;


        public PatternForm(ExternalCommandData commandData)
        {
            docUI = commandData.Application.ActiveUIDocument;
            doc = commandData.Application.ActiveUIDocument.Document;
            InitializeComponent();
            IniTreeView();
        }

        public List<T> GetAllElements<T>()
        {
            ElementClassFilter elementFilter = new ElementClassFilter(typeof(T));
            FilteredElementCollector collecotr = new FilteredElementCollector(doc);
            collecotr = collecotr.WherePasses(elementFilter);
            return collecotr.Cast<T>().ToList();
        }

        private void buttonCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void IniTreeView()
        {
            //List<T> nodeList = null;

            this.TreeViewLinePattern

            List<LinePatternElement> lstLinePatterns = GetAllElements<LinePatternElement>();

            for (int i = 0; i < lstLinePatterns.Count; i++)
            {
            }
        } //待完成

        /// <summary>
        /// create a fillPattern element
        /// </summary>
        /// <param name="patternName"></param>
        /// <returns></returns>
        private FillPatternElement CreateFacePattern(string patternName)
        {
            FillPattern fillPattern = new FillPattern(patternName, FillPatternTarget.Model,
                                                      FillPatternHostOrientation.ToView, 0.5, 0.5, 0.5);

            Transaction trans = new Transaction(doc);
            trans.Start("Create a fillPattern element");
            FillPatternElement fillPatternElement = FillPatternElement.Create(doc, fillPattern);
            trans.Commit();
            return fillPatternElement;
        }


        private FillPatternElement CreateComplexFacePattern(string patternName)
        {
            FillPatternElement fillPatternElement = null;

            FillPattern fillPattern = new FillPattern("API-created", FillPatternTarget.Model,
                                                      FillPatternHostOrientation.ToHost);

            List<FillGrid> grids = new List<FillGrid>();

            grids.Add(CreateGrid(new UV(0, 0.1), 0.5, 0, 0.55, 1.0, 0.1));
            grids.Add(CreateGrid(new UV(0, 0.5), 0.5, 0, 0.55, 1.0, 0.1));

            // Vertical lines.  
            grids.Add(CreateGrid(new UV(0, 0.1), 0.55, Math.PI / 2, 0.5, 0.4, 0.6));
            grids.Add(CreateGrid(new UV(1.0, 0.1), 0.55, Math.PI / 2, 0.5, 0.4, 0.6));

            fillPattern.SetFillGrids(grids);

            // Create the fill pattern element. Now document is modified; transaction is needed
            Transaction t = new Transaction(doc, "Create fill pattern");
            t.Start();
            fillPatternElement = FillPatternElement.Create(doc, fillPattern);

            t.Commit();
            return fillPatternElement;
        }

        private FillGrid CreateGrid(UV origin, double offset, double angle,
                                    double shift, params double[] segments)
        {
            FillGrid fillGrid = new FillGrid();
            // The arguments: origin, offset (vertical distance between lines), 
            // angle, shift (delta between location of start point per line)
            // The last two arguments are the segments: e.g. 1.0 units on, 
            // 0.1 units off (units are Revit units (ft))
            fillGrid.Origin = origin;
            fillGrid.Offset = offset;
            fillGrid.Angle = angle;
            fillGrid.Shift = shift;
            List<double> segmentsList = new List<double>();
            foreach (double d in segments)
            {
                segmentsList.Add(d);
            }
            fillGrid.SetSegments(segmentsList);

            return fillGrid;
        }


        private LinePatternElement CreateLinePatternElement(string patternName)
        {
            //Create list of segments which define the line pattern
            List<LinePatternSegment> lstSegments = new List<LinePatternSegment>();
            lstSegments.Add(new LinePatternSegment(LinePatternSegmentType.Dot, 0.0));
            lstSegments.Add(new LinePatternSegment(LinePatternSegmentType.Space, 0.02));
            lstSegments.Add(new LinePatternSegment(LinePatternSegmentType.Dash, 0.03));
            lstSegments.Add(new LinePatternSegment(LinePatternSegmentType.Space, 0.02));

            LinePattern linePattern = new LinePattern(patternName);
            linePattern.SetSegments(lstSegments);

            Transaction trans = new Transaction(doc);
            trans.Start("Create a linepattern element");
            LinePatternElement linePatternElement = LinePatternElement.Create(doc, linePattern);
            trans.Commit();
            return linePatternElement;
        }


        private void buttonCreateFillPattern_Click(object sender, EventArgs e)
        {
            Wall targetWall = GetSelectedWall();
            if (targetWall == null)
            {
                TaskDialog.Show("Create Fill Pattern",
                                "Before applying FillPattern to a wall's surfaces, you must firstly select a wall.");
                this.Close();
                return;
            }

            FillPatternElement mySurfacePattern = CreateFacePattern("MySurfacePattern");
            Material targetMaterial = doc.GetElement(targetWall.GetMaterialIds(false).First<ElementId>()) as Material;
            Transaction trans = new Transaction(doc);
            trans.Start("Apply fillpattern to surface");
            targetMaterial.SurfaceForegroundPatternId = mySurfacePattern.Id;
            trans.Commit();
            this.Close();
        }

        private void buttonCreateLinePattern_Click(object sender, EventArgs e)
        {
            List<ElementId> lstGridTypeIds = new List<ElementId>();
            GetSelectedGridTypeIds(lstGridTypeIds);
            if (lstGridTypeIds.Count == 0)
            {
                TaskDialog.Show("Apply To Grids",
                                "Before applying LinePattern to Grids, you must firstly select at least one grid.");
                this.Close();
                return;
            }

            LinePatternElement myLinePatternElement = CreateLinePatternElement("MyLinePattern");
            foreach (ElementId typeId in lstGridTypeIds)
            {
                Element gridType = doc.GetElement(typeId);
                //set the parameter value of End Segment Pattern
                SetParameter("End Segment Pattern", myLinePatternElement.Id, gridType);
            }
            this.Close();
        }


        private void SetParameter(string paramName, ElementId eid, Element elem)
        {
            foreach (Parameter param in elem.Parameters)
            {
                if (param.Definition.Name == paramName)
                {
                    Transaction trans = new Transaction(doc);
                    trans.Start("Set parameter value");
                    param.Set(eid);
                    trans.Commit();
                    break;
                }
            }
        }


        private void buttonApplyToSurface_Click(object sender, EventArgs e)
        {
            Wall targetWall = GetSelectedWall();
            if (targetWall == null)
            {
                TaskDialog.Show("Apply To Surface",
                                "Before applying FillPattern to a wall's surfaces, you must firstly select a wall.");
                this.Close();
                return;
            }

            if (treeViewFillPattern.SelectedNode == null || treeViewFillPattern.SelectedNode.Parent == null)
            {
                TaskDialog.Show("Apply To Surface",
                                "Before applying FillPattern to a wall's surfaces, you must firstly select one FillPattern.");
                return;
            }

            List<FillPatternElement> lstPatterns = GetAllElements<FillPatternElement>();
            int patternIndex = int.Parse(treeViewFillPattern.SelectedNode.Name);
            Material targetMaterial = doc.GetElement(targetWall.GetMaterialIds(false).First<ElementId>()) as Material;
            Transaction trans = new Transaction(doc);
            trans.Start("Apply fillpattern to surface");
            targetMaterial.SurfaceForegroundPatternId = lstPatterns[patternIndex].Id;
            trans.Commit();

            this.Close();
        }


        private Wall GetSelectedWall()
        {
            Wall wall = null;
            foreach (ElementId elemId in docUI.Selection.GetElementIds())
            {
                Element elem = doc.GetElement(elemId);
                wall = elem as Wall;
                if (wall != null)
                    return wall;
            }
            return wall;
        }


        private void buttonApplyToCutSurface_Click(object sender, EventArgs e)
        {
            Wall targetWall = GetSelectedWall();
            if (targetWall == null)
            {
                TaskDialog.Show("Apply To CutSurface",
                                "Before applying FillPattern to a wall's cutting surfaces, you must firstly select a wall.");
                this.Close();
                return;
            }

            if (treeViewFillPattern.SelectedNode == null || treeViewFillPattern.SelectedNode.Parent == null)
            {
                TaskDialog.Show("Apply To CutSurface",
                                "Before applying FillPattern to a wall's cutting surfaces, you must firstly select one FillPattern.");
                return;
            }

            List<FillPatternElement> lstPatterns = GetAllElements<FillPatternElement>();
            int patternIndex = int.Parse(treeViewFillPattern.SelectedNode.Name);
            Material targetMaterial = doc.GetElement(targetWall.GetMaterialIds(false).First<ElementId>()) as Material;

            Transaction trans = new Transaction(doc);
            trans.Start("Apply fillpattern to cutting surface");
            targetMaterial.CutForegroundPatternId = lstPatterns[patternIndex].Id;
            trans.Commit();

            this.Close();
        }


        private void buttonApplyToGrids_Click(object sender, EventArgs e)
        {
            List<ElementId> lstGridTypeIds = new List<ElementId>();
            GetSelectedGridTypeIds(lstGridTypeIds);
            if (lstGridTypeIds.Count == 0)
            {
                TaskDialog.Show("Apply To Grids",
                                "Before applying LinePattern to Grids, you must firstly select at least one grid.");
                this.Close();
                return;
            }

            if (treeViewLinePattern.SelectedNode == null || treeViewLinePattern.Parent == null)
            {
                TaskDialog.Show("Apply To Grids",
                                "Before applying LinePattern to Grids, you must firstly select a LinePattern.");
                return;
            }
            ElementId eid = new ElementId(int.Parse(treeViewLinePattern.SelectedNode.Name));
            foreach (ElementId typeId in lstGridTypeIds)
            {
                Element gridType = doc.GetElement(typeId);
                //set the parameter value of End Segment Pattern
                SetParameter("End Segment Pattern", eid, gridType);
            }
            this.Close();
        }


        private void GetSelectedGridTypeIds(List<ElementId> lstGridTypeIds)
        {
            foreach (ElementId elemId in docUI.Selection.GetElementIds())
            {
                Element elem = doc.GetElement(elemId);
                Autodesk.Revit.DB.Grid grid = elem as Autodesk.Revit.DB.Grid;
                if (grid != null)
                {
                    ElementId gridTypeId = grid.GetTypeId();
                    if (!lstGridTypeIds.Contains(gridTypeId))
                        lstGridTypeIds.Add(gridTypeId);
                }
            }
        }


        private void buttonCreateComplexFillPattern_Click(object sender, EventArgs e)
        {
            Wall targetWall = GetSelectedWall();
            if (targetWall == null)
            {
                TaskDialog.Show("Create Fill Pattern",
                                "Before applying FillPattern to a wall's surfaces, you must firstly select a wall.");
                this.Close();
                return;
            }

            FillPatternElement mySurfacePattern = CreateComplexFacePattern("MyComplexPattern");
            Material targetMaterial = doc.GetElement(targetWall.GetMaterialIds(false).First<ElementId>()) as Material;
            Transaction trans = new Transaction(doc);
            trans.Start("Apply complex fillpattern to surface");
            targetMaterial.SurfaceForegroundPatternId = mySurfacePattern.Id;
            trans.Commit();
            this.Close();
        }




    }
}