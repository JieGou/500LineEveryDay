using System;
using System.Collections.Generic;
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
using Autodesk.Revit.UI;
using ShowAllFloors.ViewModel;

namespace ShowAllFloors.view
{
    /// <summary>
    ///
    /// </summary>
    public partial class ShowFloorView : Window
    {
      
        public ShowFloorView(ExternalCommandData commandData)
        {
           
            InitializeComponent();
            this.DataContext =new ShowFloorViewModel(commandData);
        }
    }
}