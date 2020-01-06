using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using TeacherTangClass;
using View = Autodesk.Revit.DB.View;

namespace MyClass
{
    /// <summary>
    /// 从element获得族类型(FamilySymbol)名称
    /// </summary>
    public static partial class MyTestClass
    {
        /// <summary>
        /// 获得元素的类名
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetCategoryFromElement(this Document doc, Element element)
        {
            if (element is FamilyInstance)
            {
                ElementId typeId = element.GetTypeId();
                FamilySymbol symbol = doc.GetElement(typeId) as FamilySymbol;
                //String familyName = symbol.Family.Name;
                string category = symbol.Category.Name;
                return category;
            }
            else
            {
                ElementType type = doc.GetElement(element.GetTypeId()) as ElementType;
                String category = type.Category.Name;
                return category;
            }
        }

        /// <summary>
        /// 获得元素的族名
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetFamilyNameFromElement(this Document doc, Element element)
        {
            if (element is FamilyInstance)
            {
                ElementId typeId = element.GetTypeId();
                FamilySymbol symbol = doc.GetElement(typeId) as FamilySymbol;
                String familyName = symbol.Family.Name;
                return familyName;
            }
            else
            {
                ElementType type = doc.GetElement(element.GetTypeId()) as ElementType;
                String familyName = type.FamilyName;
                return familyName;
            }
        }

        /// <summary>
        /// 获得元素的类型名
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetFamilySymbolFromElement(this Document doc, Element element)
        {
            ElementType type = doc.GetElement(element.GetTypeId()) as ElementType;
            string symbolName = type.Name;
            return symbolName;
        }
    }
}