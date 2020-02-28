using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CodeFrame.PracticeBookInFoundation.Day0216
{
    /*
     *  DataTable: 创建DataTable对象
     *  
     */


    public class T0228DataTable
    {
        static void Main(string[] args)
        {
            DataTable dt = CreateDataTable();
            var users = TraverseDataTable(dt);

            foreach (UserInfo user in users)
            {
                Console.WriteLine($"姓名: {user.Name};\n年龄: {user.Age}\n分数: {user.Score}\n");
            }

            Console.ReadKey();
        }


        //创建DataTable对象
        public static DataTable CreateDataTable()
        {
            //创建DataTable
            DataTable dt = new DataTable("NewDt");

            //创建自增长的ID列
            DataColumn dc = dt.Columns.Add("ID", Type.GetType("System.Int32"));
            dc.AutoIncrement = true;
            dc.AutoIncrementSeed = 1;
            dc.AutoIncrementStep = 1;
            dc.AllowDBNull = false;

            //创建其他列:
            dt.Columns.Add("Name", Type.GetType("System.String"));
            dt.Columns.Add("Age", Type.GetType("System.Int32"));
            dt.Columns.Add("Score", Type.GetType("System.Int32"));
            dt.Columns.Add("CreateTime", Type.GetType("System.DateTime"));

            //创建数据
            DataRow dr = dt.NewRow();
            dr["Name"] = "张三";
            dr["Age"] = 18;
            dr["Score"] = 85.5;
            dr["CreateTime"] = DateTime.Now;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Name"] = "李四";
            dr["Age"] = 28;
            dr["Score"] = 85.5;
            dr["CreateTime"] = DateTime.Now;
            dt.Rows.Add(dr);

            dr = dt.NewRow();
            dr["Name"] = "王五";
            dr["Age"] = 38;
            dr["Score"] = 85.5;
            dr["CreateTime"] = DateTime.Now;
            dt.Rows.Add(dr);

            return dt;
        }

        //遍历DataTable对象
        /// <summary>
        /// 遍历DataTable对象,转换成List对象
        /// </summary>
        public static List<UserInfo> TraverseDataTable(DataTable dt)
        {
            List<UserInfo> userList = new List<UserInfo>();

            //判断DataTable是否为空
            if (dt == null || dt.Rows.Count == 0)
            {
                return null;
            }

            //遍历DataTable对象,转换成List
            foreach (DataRow row in dt.Rows)
            {
                UserInfo user = new UserInfo();

                if (dt.Columns.Contains("ID") && !Convert.IsDBNull(row["ID"]))
                    user.ID = Convert.ToInt32(row["ID"]);

                if (dt.Columns.Contains("Name") && !Convert.IsDBNull(row["Name"]))
                    user.Name = Convert.ToString(row["Name"]);

                if (dt.Columns.Contains("Age") && !Convert.IsDBNull(row["Age"]))
                    user.Age = Convert.ToInt32(row["Age"]);

                if (dt.Columns.Contains("Score") && !Convert.IsDBNull(row["Score"]))
                    user.Score = Convert.ToDouble(row["Score"]);

                if (dt.Columns.Contains("CreateTime") && !Convert.IsDBNull(row["CreateTime"]))
                    user.CreateTime = Convert.ToDateTime(row["CreateTime"]);

                userList.Add(user);
            }

            return userList;
        }

        /// <summary>
        /// 用户信息类
        /// </summary>
        public class UserInfo
        {
            /// <summary>
            /// 编号
            /// </summary>
            public int ID { get; set; }

            /// <summary>
            /// 名称
            /// </summary>
            public string Name { get; set; }

            /// <summary>
            /// 年龄
            /// </summary>
            public int Age { get; set; }

            /// <summary>
            /// 成绩
            /// </summary>
            public double Score { get; set; }

            /// <summary>
            /// 创建时间
            /// </summary>
            public DateTime CreateTime { get; set; }
        }
    }
}