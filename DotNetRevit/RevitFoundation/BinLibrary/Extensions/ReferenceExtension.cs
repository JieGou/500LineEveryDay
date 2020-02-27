using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace CodeInTangsengjiewa2.BinLibrary.Extensions
{
    public static class ReferenceExtension
    {
        /// <summary>
        /// 返回值是元素 element
        /// </summary>
        /// <param name="thisref"></param>
        /// <param name="doc"></param>
        /// <returns></returns>
        public static Element GetElement(this Reference thisref, Document doc)
        {
            return doc.GetElement(thisref);
        }
    }
}