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

namespace RevitDevelopmentFoudation.CodeInSDK.CurvedBeam
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public bool IsClickClosed
        {
            get;
            set;
        }
           

        public MainWindow()
        {
            InitializeComponent();
            
            CurvedBeam curvedBeam =new CurvedBeam();
          
            ListBoxBeamType.ItemsSource = curvedBeam.BeamMaps;
            ListBoxLevel.ItemsSource = curvedBeam.LevelMaps;

        }


        

        private void BtnArc_OnClick(object sender, RoutedEventArgs e)
        {
            TaskDialog.Show("按钮点击事件的演示", "按钮出发了");
        }
    }

   
}