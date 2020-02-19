// /*
//
//   In App.xaml:
//   <Application.Resources>
//       <vm:ViewModelLocator xmlns:vm="clr-namespace:ShowAllFloors"
//                            x:Key="Locator" />
//   </Application.Resources>
//   
//   In the View:
//   DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
//
//   You can also use Blend to do all this with the tool's support.
//   See http://www.galasoft.ch/mvvm
// */
//
// using Autodesk.Revit.UI;
// using CommonServiceLocator;
// using GalaSoft.MvvmLight.Ioc;
//
//
// namespace CurvedBeamWpf.ViewModel
// {
//     /// <summary>
//     /// This class contains static references to all the view models in the
//     /// application and provides an entry point for the bindings.
//     /// </summary>
//     public class ViewModelLocator
//     {
//         /// <summary>
//         /// Initializes a new instance of the ViewModelLocator class.
//         /// </summary>
//         public ViewModelLocator()
//         {
//             ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
//
//             // if (ViewModelBase.IsInDesignModeStatic)
//             // {
//             //     // Create design time view services and models
//             //     SimpleIoc.Default.Register<ShowFloorViewModel, DesignDataService>();
//             // }
//             // else
//             // {
//             //     // Create run time view services and models
//             //     SimpleIoc.Default.Register<ShowFloorViewModel, DataService>();
//             // }
//             
//             SimpleIoc.Default.Register<CurvedBeamViewModel>();
//         }
//
//         // public string commandData;
//
//         public CurvedBeamViewModel CurvedBeam
//         {
//             get
//             {
//                 return ServiceLocator.Current.GetInstance<CurvedBeamViewModel>();
//             }
//         }
//
//         public static void Cleanup()
//         {
//             // TODO Clear the ViewModels
//         }
//     }
// }