using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace TeacherTangClass
{
    public static class TransactionExtension
    {
        public static void IgnoreFailure(this Transaction trans)
        {
            var options = trans.GetFailureHandlingOptions();
            options.SetFailuresPreprocessor(new failure_ignore());
        }

        //public static void Invoke(this Document doc, Action<Transaction> action, string transactionName = "aaa")
        //{
        //    Transaction ts = new Transaction(doc, transactionName);
        //    LogHelper.LogException(delegate
        //    {
        //        ts.Start();
        //        action(ts);
        //        ts.Commit();
        //    }, @"c:\transactionException.txt");
        //}
    }

    public class failure_ignore : IFailuresPreprocessor
    {
        public FailureProcessingResult PreprocessFailures(FailuresAccessor failuresAccessor)
        {
            failuresAccessor.DeleteAllWarnings();
            //failuresAccessor.DeleteElements(failuresAccessor.el);
            return FailureProcessingResult.Continue;
        }
    }
}