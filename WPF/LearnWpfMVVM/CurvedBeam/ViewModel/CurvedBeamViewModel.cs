using System;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using LearnWpfMVVM;
using System.Collections.Generic;
using System.Linq;


namespace CurvedBeamWpf.ViewModel
{
    public class CurvedBeamViewModel : NotificationObject
    {
        private Document doc = null;
        private Action _closeAction = null;

        #region 属性

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

        private List<string> _levelTypesName;

        public List<string> LevelTypesName
        {
            get => _levelTypesName;
            set
            {
                _levelTypesName = value;
                RaisePropertyChanged("LevelTypesName");
            }
        }

        //
        public Level LevelType = null;
        private string _currentSelectOflevel;

        public string CurrentSelectOfLevel
        {
            get { return _currentSelectOflevel; }
            set
            {
                _currentSelectOflevel = value;
                LevelType = _levelTypes.Where(x => x.Name == _currentSelectOflevel) as Level;
                RaisePropertyChanged("CurrentSelectOfLevel");
            }
        }

        private List<FamilySymbol> _BeamTypes;

        public List<FamilySymbol> BeamTypes
        {
            get => _BeamTypes;
            set
            {
                _BeamTypes = value;
                RaisePropertyChanged("BeamTypes");
            }
        }

        private List<string> _beamTypesName;

        public List<string> BeamTypesName
        {
            get => _beamTypesName;
            set
            {
                _beamTypesName = value;
                RaisePropertyChanged("FloorTypesName");
            }
        }

        // 生成梁用的BeamType
        public FamilySymbol BeamType = null;

        private string _currentSelectOfBeam;

        public string CurrentSelectOfBeam
        {
            get { return _currentSelectOfBeam; }
            set
            {
                _currentSelectOfBeam = value;
                BeamType = _BeamTypes.Where(x => x.Name == _currentSelectOfBeam) as FamilySymbol;
                RaisePropertyChanged("CurrentSelectOfBeam");
            }
        }

        #endregion


        public CurvedBeamViewModel(ExternalCommandData commandData, Action closeAction)
        {
            doc = commandData.Application.ActiveUIDocument.Document;
            _closeAction = closeAction;

            InitializeData(); //初始化加载数据

            StartCreateArcCommand = new DelegateCommand();
            StartCreateArcCommand.ExecuteAction = new Action<object>(CreateBeamArc);

            StartCreateBeamEllispeCommand = new DelegateCommand();
            StartCreateBeamEllispeCommand.ExecuteAction = new Action<object>(CreateBeamEllispe);
        }


        public DelegateCommand StartCreateArcCommand { get; set; }
        public DelegateCommand StartCreateBeamEllispeCommand { get; set; }

        public string info = null;

        void InitializeData()
        {
            //Beam:
            BeamTypes = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_StructuralFraming)
                .OfClass(typeof(FamilySymbol))
                .Cast<FamilySymbol>()
                .ToList();
            BeamTypesName = BeamTypes.ConvertAll(x => x.Name);
            CurrentSelectOfBeam = BeamTypesName.First();

            // Level:
            LevelTypes = new FilteredElementCollector(doc)
                .OfCategory(BuiltInCategory.OST_Levels)
                .OfClass(typeof(Level))
                .Cast<Level>()
                .ToList();
            LevelTypesName = LevelTypes.ConvertAll(x => x.Name);
            CurrentSelectOfLevel = LevelTypesName.First();


        }


        void CreateBeamArc(object parameter)
        {
            var curve = Line.CreateBound(new XYZ(0, 0, 0), new XYZ(100, 0, 0));
            var level = doc.ActiveView.GenLevel.Id;
            Transaction ts = new Transaction(doc, "mvvm里执行命令的测试");
            ts.Start();

            Wall.Create(doc, curve, level, false);
            TaskDialog.Show("tips", "墙创建好了");

            ts.Commit();
        }


        void CreateBeamEllispe(object parameter)
        {
            string info = BeamType.Name;
            TaskDialog.Show("tips", info);
        }


        void ExcuteCreateBeamSplineCommand()
        {
            //*****************8
        }
    }
}