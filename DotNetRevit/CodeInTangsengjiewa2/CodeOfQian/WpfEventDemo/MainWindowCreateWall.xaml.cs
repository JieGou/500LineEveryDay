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
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using MessageBox = System.Windows.MessageBox;


namespace CodeInTangsengjiewa2.CodeOfQian.WpfEventDemo
{
    /// <summary>
    /// 交互逻辑
    /// </summary>
    public partial class MainWindowCreateWall : Window
    {
        //1 注册外部事件
        private Cmd_CreateWall CmdCreateWall = null;
        private ExternalEvent createWallEvent = null;


        public MainWindowCreateWall()
        {
            InitializeComponent();
            //2 初始化
            CmdCreateWall = new Cmd_CreateWall();
            createWallEvent = ExternalEvent.Create(CmdCreateWall);
        }

        private void CreateWallBtn1_Click(object sender, RoutedEventArgs e)
        {

            //3 触发事件,执行命令
            //4 属性传值
            CmdCreateWall.WallHeight = Convert.ToDouble(this.TxBoxWallHeight1.Text);
            createWallEvent.Raise();
        }
    }
}