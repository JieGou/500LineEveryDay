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
using ExerciseProject.TeacherTangClass.Extensions;

namespace MyClass
{
    public static partial class MyTestClass
    {
        /// <summary>
        /// 从元素获得元素的category
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetCategoryFromElement(this Document doc, Element element)
        {
            var result = element.Category;
            return result.Name;
        }

        /// <summary>
        /// 从元素获得元素的族familyName
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetFamilyNameFromElement(this Document doc, Element element)
        {
            //return  (element.GetTypeId().GetElement(doc) as ElementType)?.FamilyName;

            var typeid = element.GetTypeId();
            var elementType = typeid.GetElement(doc) as ElementType;
            string familyName = elementType.FamilyName;
            return familyName;
        }

        /// <summary>
        /// 从元素获得元素的familySymbol
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        public static string GetFamilySymbolFromElement(this Document doc, Element element)
        {
            var typeid = element.GetTypeId();
            ElementType elementType = typeid.GetElement(doc) as ElementType;
            string  familySymbolName =elementType.Name;
            return familySymbolName;
        }
    }
}