using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace RevitDevelopmentFoudation.CodeInSDK
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class CapitalizedAllTextNotes : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            #region sufferFormatStatus
            var formatStatusEnum = Enum.GetValues(typeof(FormatStatus));
            string message2 = null;
            foreach (object ele in formatStatusEnum)
            {
                message2 += ele.ToString()+"\n";
            }
            TaskDialog.Show("tips", message2);
            #endregion

            try
            {
                Document document = commandData.Application.ActiveUIDocument.Document;

                //Iterate through the document and find all the TextNote elements
                FilteredElementCollector collector = new FilteredElementCollector(document);

                collector.OfClass(typeof(TextNote)); //typeof 是关键字,不是方法. typeof:一个操作符，返回传入参数的类型

                if (collector.GetElementCount() == 0)
                {
                    message = "The document does not contain TextNote elements";
                    return Result.Failed;
                }

                //record all TextNotes that are not yet formatted to be AllCaps
                ElementSet textNotesToUpdate = new ElementSet();

                foreach (TextNote element in collector)
                {
                    //Extract the formattedText from TextNote
                    FormattedText formattedText = element.GetFormattedText();

                    if (formattedText.GetAllCapsStatus() != FormatStatus.All)
                    {
                        textNotesToUpdate.Insert(element);
                    }
                }

                //check whether we found any textnotes that need to be formatted
                if (textNotesToUpdate.IsEmpty)
                {
                    message = "No textnote elements needed updating";
                    return Result.Failed;
                }

                //apply the 'allcaps' formatting to the textNotes that still need it
                using (Transaction ts = new Transaction(document, "capitalize all textNote"))
                {
                    ts.Start();

                    foreach (TextNote ele in textNotesToUpdate)
                    {
                        FormattedText formattedText = ele.GetFormattedText();
                        formattedText.SetAllCapsStatus(true);
                        ele.SetFormattedText(formattedText);
                    }

                    ts.Commit();
                }

                return Result.Succeeded;
            }

            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }

          
        }
    }
}