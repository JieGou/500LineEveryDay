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
using View = Autodesk.Revit.DB.View;

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
        public static string GetCategoryFromElement(Document doc, Element element)
        {
            var result = element.Category.Name;
            return result;
     

        }


        /// <summary>
        /// 从元素获得元素的族familyName
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="element"></param>
        /// <returns></returns>
        // public static string GetFamilyNameFromElement(Document doc, Element element)
        // {
        //     //return  (element.GetTypeId().GetElement(doc) as ElementType)?.FamilyName;
        //
        //     ElementId elementTypeid = element.GetTypeId();
        //     //elementTypeid.GetElement(doc) 得到的element是type（FamilySymbol 或者 WallType）
        //     //（FamilySymbol 或者 WallType） 是ElementType的子类； 子类 as 父类
        //     //将通过（FamilySymbol 或者 WallType）的FamilyName属性得到族名
        //
        //     var elementType = elementTypeid.GetElement(doc) as ElementType;
        //     // var elementType = element as ElementType;
        //     string familyName = elementType.FamilyName;
        //     return familyName;
        // }

        public static string GetFamilyNameFromElement(Document doc, Element element)
        {
            
            ElementId elementTypeId = element.GetTypeId();
            ElementType elementType = doc.GetElement(elementTypeId) as ElementType;
            return elementType.FamilyName;
        }


        /// <summary>
        /// 从元素获得元素的familySymbol
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="element"></param>
        /// <returns></returns>

        public static string GetFamilySymbolFromElement(Document doc, Element element)
        {
            
            return element.Name;
        }
    }
}