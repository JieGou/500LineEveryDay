using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Itenso.Configuration;
using LearnWpfMVVM;
using LearnWpfMVVM.CurvedBeam.View;
using System;
using System.Collections.Generic;
using System.IO;
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

        private Level _currentSelectOflevel;

        public Level CurrentSelectOfLevel
        {
            get => _currentSelectOflevel;
            set
            {
                _currentSelectOflevel = value;
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

        private FamilySymbol _currentSelectOfBeam;

        public FamilySymbol CurrentSelectOfBeam
        {
            get => _currentSelectOfBeam;
            set
            {
                _currentSelectOfBeam = value;
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

            if (!File.Exists(ApplicationSettings.UserConfigurationFilePath))
            {
                if (LevelTypes.Count > 0)
                {
                    CurrentSelectOfLevel = LevelTypes.First();
                }

                if (BeamTypes.Count > 0)
                {
                    CurrentSelectOfBeam = BeamTypes.First();
                }
            }
        }

        public RelayCommand<CurvedBeamMainWindow> WindowLoaded { get; set; }

        public RelayCommand<CurvedBeamMainWindow> StartCreateArcCommand { get; set; }

        public RelayCommand<CurvedBeamMainWindow> StartCreateBeamEllispeCommand { get; set; }

        private void InitializeData()
        {
            //梁类型
            BeamTypes = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_StructuralFraming)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .ToList();

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

            if (BeamTypes.Count > 0)
            {
                CurrentSelectOfBeam = BeamTypes.First();
            }
        }

        private void CreateBeamArc(CurvedBeamMainWindow win)
        {
            _win.Close();

            var curve = Line.CreateBound(new XYZ(0, 0, 0), new XYZ(100, 0, 0));

            Transaction ts = new Transaction(doc, "mvvm里执行命令的测试");
            ts.Start();

            if (_currentSelectOfBeam != null && !_currentSelectOfBeam.IsActive)
            {
                _currentSelectOfBeam.Activate();
            }

            doc.Create.NewFamilyInstance(curve, _currentSelectOfBeam, _currentSelectOflevel, StructuralType.Beam);
            TaskDialog.Show("tips", "梁创建好了");

            ts.Commit();
        }

        private void CreateBeamEllispe(CurvedBeamMainWindow win)
        {
            _win.Close();
            string info = _currentSelectOfBeam + "\n";
            info += _currentSelectOfBeam.Name + "\n";

            info += _currentSelectOflevel + "\n";
            info += _currentSelectOflevel.Name + "\n";

            TaskDialog.Show("tips", info);
        }

        private void ExcuteCreateBeamSplineCommand()
        {
            //*****************8
        }
    }
}