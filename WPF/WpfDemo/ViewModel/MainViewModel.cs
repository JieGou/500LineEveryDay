using System.Collections.ObjectModel;
using System.Security.RightsManagement;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using WpfDemo.Db;
using WpfDemo.Model;

namespace WpfDemo.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel()
        {
            localDb = new LocalDb();
            Query();
            QuerryCommand = new RelayCommand(Query);


        }


        public RelayCommand QuerryCommand { set; get; }

        private readonly LocalDb localDb;

        private ObservableCollection<Student> gridModelList;
        
        public ObservableCollection<Student> GridModelList
        {
            get { return gridModelList; }
            set
            {
                gridModelList = value;
                RaisePropertyChanged();
            }
        }

        public void Query()
        {
            var models = localDb.GetStudents();
            GridModelList = new ObservableCollection<Student>();

            if (models != null)
            {
                models.ForEach(arg => { GridModelList.Add(arg); });
            }
        }
    }
}