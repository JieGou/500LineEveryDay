using Autodesk.Revit.DB;
using CodeInTangsengjiewa2.BinLibrary.Extensions;
using System;
using System.Windows.Media.TextFormatting;

namespace CodeInTangsengjiewa2.BinLibrary.Helpers
{
    public static class TransactionHelper
    {
        public static void Invoke(this Document doc, Action<Transaction> action, string name = "Invoke")
        {
            LogHelper.LogException(delegate
            {
                using (Transaction transaction = new Transaction(doc, name))
                {
                    transaction.Start();
                    action(transaction);
                    bool flag = transaction.GetStatus() == (TransactionStatus) 1;

                    if (flag)
                    {
                        transaction.Commit();
                    }
                }
            }, @"C:\\revitExceptionLog.txt");
        }

        public static void Invoke(
            this Document doc, Action<Transaction> action, string name = "Invoke", bool ignorefailure = true)
        {
            LogHelper.LogException(delegate
            {
                using (Transaction transaction = new Transaction(doc, name))
                {
                    transaction.Start();

                    if (ignorefailure)
                    {
                        transaction.Ignorefailure();
                    }

                    action(transaction);

                    bool flag = transaction.GetStatus() == TransactionStatus.Started;

                    if (flag)
                    {
                        transaction.Commit();
                    }
                }
            }, @"C:\\revitExceptionLog.txt");
        }

        public static void SuntranInvoke(this Document doc, Action<SubTransaction> action)
        {
            using (SubTransaction subTransaction = new SubTransaction(doc))
            {
                subTransaction.Start();
                action(subTransaction);
                bool flag = subTransaction.GetStatus() == (TransactionStatus) 1;

                if (flag)
                {
                    subTransaction.Commit();
                }
            }
        }
    }
}