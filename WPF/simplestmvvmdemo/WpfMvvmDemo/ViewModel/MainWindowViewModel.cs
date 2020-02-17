using System;
using System.Collections.Generic;
using System.Windows.Documents;
using WpfApplication1.Commands;

namespace WpfApplication1.ViewModel
{
    public class MainWindowViewModel : ViewModelBase
    {
        private string _input;

        public string Input
        {
            get
            {
                return _input;
            }
            set
            {
                _input = value;
                RaisePropertyChanged("Input");
            }
        }

        private string _selectItem;
        public string SelectItem
        {
            get
            {
                return _selectItem;
            }
            set
            {
                _selectItem = value;
                RaisePropertyChanged("SelectItem");
            }
        }


        private string _display;

        public string Display
        {
            get
            {
                return _display;
            }
            set
            {
                _display = value;
                RaisePropertyChanged("Display");
            }
        }


        private List<string> _lisStrings;

        public List<string> LisStrings
        {
            get
            {
                return _lisStrings;
            }
            set
            {
                _lisStrings = value;
                RaisePropertyChanged("LisStrings");
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

            LisStrings = new List<string>() {"张三", "李四"};
            ;
        }
    }
}