using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using CurvedBeamWpf.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;

namespace LearnWpfMVVM.CurvedBeam.View
{
    /// <summary>
    /// ExportFamilyMainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CurvedBeamMainWindow : Window
    {
        public CurvedBeamMainWindow(ExternalCommandData commandData)
        {
            InitializeComponent();
            this.DataContext = new CurvedBeamViewModel(commandData);
        }
    }
}