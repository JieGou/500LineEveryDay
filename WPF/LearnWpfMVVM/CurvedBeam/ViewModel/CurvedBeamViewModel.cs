using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using LearnWpfMVVM;
using LearnWpfMVVM.CurvedBeam.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace CurvedBeamWpf.ViewModel
{
    public class CurvedBeamViewModel : ViewModelBase
    {
        private Document doc = null;
        private CurvedBeamMainWindow _win;

        #region 属性

        //level

        private List<Level> _levelTypes;

        public List<Level> LevelTypes
        {
            get => _levelTypes;
            set
            {
                _levelTypes = value;
                RaisePropertyChanged("LevelTypes");
            }
        }

        public Level LevelType = null;

        private Level _currentSelectOflevel;

        public Level CurrentSelectOfLevel
        {
            get => _currentSelectOflevel;
            set
            {
                _currentSelectOflevel = value;
                LevelType = _levelTypes.FirstOrDefault(x => x.Name == _currentSelectOflevel.Name) as Level;
                RaisePropertyChanged("CurrentSelectOfLevel");
            }
        }

        private List<FamilySymbol> _beamTypes;

        public List<FamilySymbol> BeamTypes
        {
            get => _beamTypes;
            set
            {
                _beamTypes = value;
                RaisePropertyChanged("BeamTypes");
            }
        }

        // 生成梁用的BeamType
        public FamilySymbol BeamType = null;

        private FamilySymbol _currentSelectOfBeam;

        public FamilySymbol CurrentSelectOfBeam
        {
            get => _currentSelectOfBeam;
            set
            {
                _currentSelectOfBeam = value;
                BeamType = BeamTypes.FirstOrDefault(x => x.Name == _currentSelectOfBeam.Name);
                RaisePropertyChanged("CurrentSelectOfBeam");
            }
        }

        #endregion 属性

        public CurvedBeamViewModel(ExternalCommandData commandData)
        {
            doc = commandData.Application.ActiveUIDocument.Document;

            //初始化加载数据
            InitializeData();

            WindowLoaded = new RelayCommand<CurvedBeamMainWindow>(OnWindowLoaded);
            StartCreateArcCommand = new RelayCommand<CurvedBeamMainWindow>(CreateBeamArc);
            StartCreateBeamEllispeCommand = new RelayCommand<CurvedBeamMainWindow>(CreateBeamEllispe);
        }

        private void OnWindowLoaded(CurvedBeamMainWindow win)
        {
            _win = win;
        }

        public RelayCommand<CurvedBeamMainWindow> WindowLoaded { get; set; }

        public RelayCommand<CurvedBeamMainWindow> StartCreateArcCommand { get; set; }

        public RelayCommand<CurvedBeamMainWindow> StartCreateBeamEllispeCommand { get; set; }

        public string info = null;

        private void InitializeData()
        {
            //梁类型
            BeamTypes = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_StructuralFraming)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .ToList();

            if (BeamTypes.Count > 0)
            {
                CurrentSelectOfBeam = BeamTypes.First();
            }

            //标高
            LevelTypes = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Levels)
                .OfClass(typeof(Level))
                .Cast<Level>()
                .ToList();
            if (LevelTypes.Count > 0)
            {
                CurrentSelectOfLevel = LevelTypes.First();
            }
        }

        private void CreateBeamArc(CurvedBeamMainWindow win)
        {
            _win.Close();

            var curve = Line.CreateBound(new XYZ(0, 0, 0), new XYZ(100, 0, 0));

            Transaction ts = new Transaction(doc, "mvvm里执行命令的测试");
            ts.Start();

            BeamType = BeamTypes.FirstOrDefault(x => x.Name == _currentSelectOfBeam.Name);

            if (!BeamType.IsActive)
            {
                BeamType.Activate();
            }

            LevelType = _levelTypes.FirstOrDefault(x => x.Name == _currentSelectOflevel.Name) as Level;
            // if (!LevelType.IsActive)
            // {
            //     symbol.Activate();
            // }

            doc.Create.NewFamilyInstance(curve, BeamType, LevelType, StructuralType.Beam);
            TaskDialog.Show("tips", "梁创建好了");

            ts.Commit();
        }

        private void CreateBeamEllispe(CurvedBeamMainWindow win)
        {
            _win.Close();
            string info = CurrentSelectOfBeam + "\n";
            info += BeamType.Name + "\n";

            // LevelType = _levelTypes.FirstOrDefault(x => x.Name == _currentSelectOflevel) as Level;

            info += CurrentSelectOfLevel + "\n";
            info += LevelType.Name + "\n";

            TaskDialog.Show("tips", info);
        }

        private void ExcuteCreateBeamSplineCommand()
        {
            //*****************8
        }
    }
}