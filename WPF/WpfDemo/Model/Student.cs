using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace WpfDemo.Model
{
    public class Student : ViewModelBase
    {
        private int id;

        public int Id
        {
            get => id;
            set
            {
                id = value;
                RaisePropertyChanged();
            }
        }

        private string name;


        public string Name
        {
            get => name;
            set
            {
                name = value;
                RaisePropertyChanged();
            }
        }
    }
}