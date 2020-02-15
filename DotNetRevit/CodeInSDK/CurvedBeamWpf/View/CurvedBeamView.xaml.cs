// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text;
// using System.Threading.Tasks;
// using System.Windows;
// using System.Windows.Controls;
// using System.Windows.Data;
// using System.Windows.Documents;
// using System.Windows.Input;
// using System.Windows.Media;
// using System.Windows.Media.Imaging;
// using System.Windows.Navigation;
// using System.Windows.Shapes;
// using Autodesk.Revit.DB;
// using Autodesk.Revit.UI;
// using CurvedBeamWpf.Model;
// using CurvedBeamWpf.ViewModel;
//
//
// namespace CurvedBeamWpf.view
// {
//     /// <summary>
//     ///
//     /// </summary>
//     public partial class CurvedBeamView : Window
//     {
//         public CurvedBeamView(ExternalCommandData commandData)
//         {
//             InitializeComponent();
//             this.DataContext = new DataOfRevitModel(commandData);
//         }
//
//         private void CreateBeamArc_Click(object sender, RoutedEventArgs e)
//         {
//
//         }
//     }
// }