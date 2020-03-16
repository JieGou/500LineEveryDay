using Autodesk.Revit.DB;
using CodeInTangsengjiewa3.BinLibrary.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeInTangsengjiewa3.BinLibrary.Helpers
{
    /// <summary>
    /// 事务帮助类
    /// </summary>
    public static class TransactionHelper
    {
        /// <summary>
        /// 启动事务
        /// </summary>
        /// <param name="doc">Revit Document对象</param>
        /// <param name="action">委托</param>
        /// <param name="name">事务名称 默认为"Invoke"</param>
        /// <param name="ignorefailure">事务名称 默认为"Invoke"</param>
        public static void Invoke(this Document doc, Action<Transaction> action, string name = "Invoke", bool ignorefailure = true)
        {
            LogHelper.LogException(delegate
            {
                using (Transaction transaction = new Transaction(doc, name))
                {
                    transaction.Start();
                    if (ignorefailure)
                    {
                        transaction.IgnoreFailure();
                    }

                    action(transaction);
                    bool flag = transaction.GetStatus() == TransactionStatus.Started;
                    if (flag)
                    {
                        transaction.Commit();
                    }
                }
            }, "c:\\revitExceptionlog.txt");
        }

        /// <summary>
        /// 启动子事务
        /// </summary>
        /// <param name="doc">Revit Document对象</param>
        /// <param name="action">委托</param>
        public static void SubtranInvoke(this Document doc, Action<SubTransaction> action)
        {
            using (SubTransaction subTransaction = new SubTransaction(doc))
            {
                subTransaction.Start();
                action(subTransaction);
                bool flag = subTransaction.GetStatus() == TransactionStatus.Started;
                if (flag)
                {
                    subTransaction.Commit();
                }
            }
        }
    }
}