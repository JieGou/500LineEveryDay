using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;

namespace RevitDevelopmentFoudation.CodeInSDK.CurvedBeam
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CurvedBeam : IExternalCommand
    {
        private UIApplication m_revit = null;
        public ArrayList m_beamMaps = new ArrayList();
        public ArrayList m_levels = new ArrayList();

        public ArrayList BeamMaps
        {
            get
            {
                return m_beamMaps;
            }
        }

        public ArrayList LevelMaps
        {
            get { return m_levels; }
        }


        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            m_revit = commandData.Application;
            Transaction tran = new Transaction(m_revit.ActiveUIDocument.Document, "curve beam");
            tran.Start();

            //if initialize failed return Result.Failed
            bool initializeOK = Initialize();

            foreach (object beamMap in BeamMaps)
            {
                
            }

            // MainWindow wpf = new MainWindow(); //实例化主窗口类
            // wpf.ShowDialog(); //展示界面

            //1  获取当前文档
            Document doc = commandData.Application.ActiveUIDocument.Document;

            MainWindow mainWindow = new MainWindow();

            // //非模态窗体: 窗口弹出的同时,墙已经创建.
            // mainWindow.Show();
            //模态窗体:
            mainWindow.ShowDialog();

            // //如果关闭直接退出,不会报错
            // if (!mainWindow.IsClickClosed)
            // {
            //     return Result.Cancelled;
            // }


            return Result.Succeeded;
        }

        private bool Initialize()
        {
            try
            {
                ElementClassFilter levelFilter = new ElementClassFilter(typeof(Level));

                ElementClassFilter famFilter = new ElementClassFilter(typeof(Family));
                LogicalOrFilter orFilter = new LogicalOrFilter(levelFilter, famFilter);

                FilteredElementCollector collector = new FilteredElementCollector(m_revit.ActiveUIDocument.Document);
                FilteredElementIterator i = collector.WherePasses(orFilter).GetElementIterator();
                i.Reset();
                bool moreElement = i.MoveNext();

                while (moreElement)
                {
                    object o = i.Current;
                    //add level to list
                    Level level = o as Level;

                    if (null != level)
                    {
                        m_levels.Add(new LevelMap(level));
                        goto nextLoop;
                    }

                    //get
                    Family f = o as Family;

                    if (null == f)
                    {
                        goto nextLoop;
                    }

                    foreach (ElementId elementId in f.GetFamilySymbolIds())
                    {
                        object symbol = m_revit.ActiveUIDocument.Document.GetElement(elementId);
                        FamilySymbol familyType = symbol as FamilySymbol;

                        if (null == familyType)
                        {
                            goto nextLoop;
                        }

                        if (null == familyType.Category)
                        {
                            goto nextLoop;
                        }

                        //add symbols of beams and braces to lists
                        string categoryName = familyType.Category.Name;

                        if (categoryName == "Structral Framing")
                        {
                            m_beamMaps.Add(new SymbolMap(familyType));
                        }
                    }

                    nextLoop:
                    moreElement = i.MoveNext();
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.ToString());
            }

            return true;
        }

        public Arc CreateArc(double z)
        {
            Autodesk.Revit.DB.XYZ center = new Autodesk.Revit.DB.XYZ(0, 0, z);
            double radius = 20.0;
            double startAngle = 0.0;
            double endAngle = 5.0;
            Autodesk.Revit.DB.XYZ xAxis = new Autodesk.Revit.DB.XYZ(1, 0, 0);
            Autodesk.Revit.DB.XYZ yAxis = new Autodesk.Revit.DB.XYZ(0, 1, 0);
            return Arc.Create(center, radius, startAngle, endAngle, xAxis, yAxis);
        }

        public Curve CreateEllipse(double z)
        {
            Autodesk.Revit.DB.XYZ center = new Autodesk.Revit.DB.XYZ(0, 0, z);
            double radX = 30;
            double radY = 50;
            Autodesk.Revit.DB.XYZ xVec = new Autodesk.Revit.DB.XYZ(1, 0, 0);
            Autodesk.Revit.DB.XYZ yVec = new Autodesk.Revit.DB.XYZ(0, 1, 0);
            double param0 = 0.0;
            double param1 = 3.1415;
            Curve ellpise = Ellipse.CreateCurve(center, radX, radY, xVec, yVec, param0, param1);
            m_revit.ActiveUIDocument.Document.Regenerate();
            return ellpise;
        }


        public Curve CreateNurbSpline(double z)
        {
            // create control points with same z value
            List<XYZ> ctrPoints = new List<XYZ>();
            Autodesk.Revit.DB.XYZ xyz1 = new Autodesk.Revit.DB.XYZ(-41.887503610431267, -9.0290629129782189, z);
            Autodesk.Revit.DB.XYZ xyz2 = new Autodesk.Revit.DB.XYZ(-9.27600019217055, 0.32213521486563046, z);
            Autodesk.Revit.DB.XYZ xyz3 = new Autodesk.Revit.DB.XYZ(9.27600019217055, 0.32213521486563046, z);
            Autodesk.Revit.DB.XYZ xyz4 = new Autodesk.Revit.DB.XYZ(41.887503610431267, 9.0290629129782189, z);

            ctrPoints.Add(xyz1);
            ctrPoints.Add(xyz2);
            ctrPoints.Add(xyz3);
            ctrPoints.Add(xyz4);

            IList<double> weights = new List<double>();
            double w1 = 1, w2 = 1, w3 = 1, w4 = 1;
            weights.Add(w1);
            weights.Add(w2);
            weights.Add(w3);
            weights.Add(w4);

            IList<double> knots = new List<double>();
            double k0 = 0, k1 = 0, k2 = 0, k3 = 0, k4 = 34.425128, k5 = 34.425128, k6 = 34.425128, k7 = 34.425128;

            knots.Add(k0);
            knots.Add(k1);
            knots.Add(k2);
            knots.Add(k3);
            knots.Add(k4);
            knots.Add(k5);
            knots.Add(k6);
            knots.Add(k7);

            Curve detailNurbSpline = NurbSpline.CreateCurve(3, knots, ctrPoints, weights);
            m_revit.ActiveUIDocument.Document.Regenerate();

            return detailNurbSpline;
        }


        public bool CreateCurvedBeam(FamilySymbol fsBeam, Curve curve, Level level)
        {
            FamilyInstance beam;

            try
            {
                if (!fsBeam.IsActive) fsBeam.Activate();
                beam = m_revit.ActiveUIDocument.Document.Create.NewFamilyInstance(curve, fsBeam, level,
                                                                                  StructuralType.Beam);

                if (null == beam)
                {
                    return false;
                }

                LocationCurve beamCurve = beam.Location as LocationCurve;

                if (null == beamCurve)
                {
                    return false;
                }
            }
            catch (Exception e)
            {
                TaskDialog.Show("revit", e.ToString());
                return false;
            }

            m_revit.ActiveUIDocument.Document.Regenerate();
            return true;
        }
    }


    public class SymbolMap
    {
        private string m_symbolName = "";
        private FamilySymbol m_symbol = null;

        private SymbolMap()
        {
        }

        public SymbolMap(FamilySymbol symbol)
        {
            m_symbol = symbol;
            string familyName = "";

            if (null != symbol.Family)
            {
                familyName = symbol.Family.Name;
            }

            m_symbolName = familyName + " : " + symbol.Name;
        }

        public string SymbolName
        {
            get
            {
                return m_symbolName;
            }
        }

        public FamilySymbol ElementType
        {
            get { return m_symbol; }
        }
    }


    public class LevelMap
    {
        string m_levelName = "";
        Level m_level = null;

        private LevelMap()
        {
            // no operation
        }

        public LevelMap(Level level)
        {
            m_level = level;
            m_levelName = level.Name;
        }

        public string LevelName
        {
            get
            {
                return m_levelName;
            }
        }

        /// <summary>
        /// Level property
        /// </summary>
        public Level Level
        {
            get
            {
                return m_level;
            }
        }
    }
}