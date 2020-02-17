using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MVVMTest.Control;

namespace MVVMTest.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _input;

        public string Input
        {
            get { return _input; }
            set
            {
                _input = value;
                RaisePropertyChanged("Input");
            }
        }

        private string _display;

        public string Display
        {
            get => _display;
            set
            {
                _display = value;
                RaisePropertyChanged("Display");
            }
        }

        public DelegateCommand SetTextCommand { get; set; }

        private void SetText(object obj)
        {
            Display = Input;
        }

        public MainWindowViewModel()
        {
            SetTextCommand = new DelegateCommand(new Action<object>(SetText));
        }
    }
}