using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PracticeBook.Day0119
{
    /*
     * 练习4 :  了解Revit api里面 FilterElementCollection的成员。
         */

    class T04
    {
        static void Main(string[] args)
        {
        }
    }

    /*    public class FilterElementCollection : IEnumerable<Element>, IDisposable
           {
         
        public FilteredElementCollector(Document doc);
        创建一个新的...,将从一个文档中搜寻和过滤.
   
        public FilteredElementCollector(Document doc,ElementId viewId);
        创建一个新的...,将从一个视图中所有的课件元素搜寻和过滤

        public FilteredElementCollector(Document doc,ICollection<ElementId> elementIds);
        创建一个新的...将从一个指定的元素集合中搜寻和过滤

        public FilteredElementCollector ContainedInDesignOption(ElementId desingOptionId)

        public Element FirstElement();
        传递第一个元素 进入filter
        
        public ElementId FirstElementId();
        返回第一个元素Id 进入filter

        public int GetElementCount();
        获得当前过滤器里的元素数量

        public FilteredElementIterator GetElementIdIterator();

        public FilteredElementCollector IntersectWith(FilteredElementCollector other);

        public FilteredElementCollector OfCategory(BuiltInCategory category);

        public FilteredElementCollector OfCategoryId(BuiltInCategory categoryId);

        public FilteredElementCollector OfClass(Type type);

        public FilteredElementCollector OwnedByView(ElementId viewId);

        public ICollection<ElementId> ToElementIds();
        返回通过过滤器的元素id集合

        public IList<Element> ToElements();
        返回通过过滤器的元素集合

        public FilteredElementCollector UnionWith(FilteredElementCollector other)
        将两个过滤器的元素统一起来

        public FilteredElementCollector WhereElementsCurveDriven();
        对收集器应用一个元素快速过滤

        public FilteredElementCollector WhereElementIsElementType();
        将ElementIsElementTypeFilter应用于收集器。

        public FilteredElementCollector WhereElementIsNotElementType();
        将一个反向的ElementIsElementTypeFilter应用于收集器。

        public FilteredElementCollector WhereElementIsViewIndependent();

        public FilteredElementCollector WherePasses(ElementFilter filter);
        将元素过滤器应用于收集器

        
           
   
        }    *
   */
}