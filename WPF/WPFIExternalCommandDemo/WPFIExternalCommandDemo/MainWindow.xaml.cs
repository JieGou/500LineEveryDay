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

namespace WPFIExternalCommandDemo
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        //1 注册外部事件
        private CreatWall creatWallCommand = null;
        private ExternalEvent creatwallEvent = null;

        private CreatWall2 creatWallCommand2 = null;
        private ExternalEvent creatwallEvent2 = null;

        public MainWindow()
        {
            InitializeComponent();
            //2 初始化
            creatWallCommand = new CreatWall();
            creatwallEvent = ExternalEvent.Create(creatWallCommand);
            // 初始化第二个命令
            creatWallCommand2 = new CreatWall2();
            creatwallEvent2 = ExternalEvent.Create(creatWallCommand2);
        }

        private void Botton_Click(object sender, RoutedEventArgs e)
        {
            //3 属性传值
            creatWallCommand.wallHeight = Convert.ToDouble(this.TextBox.Text) / 0.3048;
            //4 执行命令
            creatwallEvent.Raise();
        }

        private void Botton2_Click(object sender, RoutedEventArgs e)
        {
            creatWallCommand2.wallHeight = Convert.ToDouble(this.TextBox2.Text) / 0.3048;
            creatwallEvent2.Raise();
        }
    }
}