// using System;
// using System.Collections.Generic;
// using System.Data;
// using System.Linq;
// using System.Reflection.Emit;
// using System.Text;
// using System.Threading.Tasks;
// using System.Windows.Forms;
//
// namespace FConsoleMainF1121.CSharpTutorialUtilityEdition.Chapter1
//
// {
//     class F1162
//     {
//         /// <summary>
//         /// CSharpTutorialUtilityEdition(C#教程实用版)
//          //// 事件1
//         /// 事件的例子:下面的案例代码抄自
//         /// https://www.cnblogs.com/vickylinj/p/10922139.html
//         /// </summary>
//         /// <param name="args"></param>
//         private static void MyApp_SpaceKeyPressed()
//         {
//             //当按下事件,当时的时间显示出来.
//             Console.WriteLine("{0}按下空格键", DateTime.Now.ToLongTimeString());
//         }
//
//         static void Main(string[] args)
//         {
//             APP2 MyApp = new APP2();
//             //在+=后面按tab键会自动生成一个事件处理的方法函数
//             MyApp.SpaceKeyPressed += MyApp_SpaceKeyPressed;
//             //开始运行
//             MyApp.StartRun();
//         }
//     }
//     //定义一个委托类型
//     public delegate void SpaceKeyPressedEventHandler();
//
//     public class APP2
//     {
//         //声明一个事件, 类型是委托类型
//         public event SpaceKeyPressedEventHandler SpaceKeyPressed;
//
//         //受保护的方法,写成虚方法,是为了方便子类重写,实现多态
//         protected virtual void OnspaceKeyPressd()
//         {
//             if (this.SpaceKeyPressed != null)
//             {
//                 SpaceKeyPressed();
//             }
//         }
//
//         public void StartRun()
//         {
//             while (true)
//             {
//                 //获取按键值
//                 ConsoleKeyInfo keyInfo = Console.ReadKey();
//
//                 //如果为空值
//                 if (keyInfo.Key == ConsoleKey.Spacebar)
//                 {
//                     //触发事件
//                     OnspaceKeyPressd();
//                 }
//
//                 //esc 退出
//                 if (keyInfo.Key == ConsoleKey.Escape)
//                 {
//                     break;
//                 }
//             }
//         }
//
//         private static void MyApp_SpaceKeyPressed()
//         {
//             //当按下事件,当时的时间显示出来.
//             Console.WriteLine("{0}按下空格键", DateTime.Now.ToLongTimeString());
//         }
//     }
// }

