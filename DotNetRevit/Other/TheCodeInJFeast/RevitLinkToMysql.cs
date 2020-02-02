using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using MySql.Data.MySqlClient;

namespace TheCodeInJFeast
{
    /* ————————————————
    版权声明：本文为CSDN博主「黑夜の骑士」的原创文章，遵循 CC 4.0 BY-SA 版权协议，转载请附上原文出处链接及本声明。
    原文链接：https://blog.csdn.net/birdfly2015/article/details/87923301

        这是一个从mysql数据库读取数据的测试案例.
    */


    [Transaction(TransactionMode.Manual)]
    class RevitLinkToMysql : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            Document doc = commandData.Application.ActiveUIDocument.Document;

            //输入自己的Mysql账号（一般默认为root）、密码以及数据库名称        
            String connetStr = "server=127.0.0.1;port=3306;user=root;password=123456;database=mystudytest;";
            MySqlConnection conn = new MySqlConnection(connetStr);

            conn.Open(); //打开通道，建立连接

            string sqlCmd = "select * from student";
            MySqlCommand cmd = new MySqlCommand(sqlCmd, conn);

            MySqlDataReader reader = cmd.ExecuteReader(); //执行ExecuteReader()返回一个MySqlDataReader对象

            string strShow = null;

            while (reader.Read()) //遍历每一行
            {
                //输出数据库结果，列既可以通过列号，也可以通过列名来得到
                //针对不通类型参数，采用相对应的reader.get方法
                strShow += reader.GetString("student_id") + ":" + reader.GetInt32("age").ToString() + "\n";
            }

            TaskDialog.Show("连接Mysql数据库", strShow);
            return Result.Succeeded;
        }
    }
}