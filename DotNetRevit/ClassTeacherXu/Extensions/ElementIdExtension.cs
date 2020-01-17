using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace TeacherTangClass.Extensions
{/// <summary>
/// 给ElementId增加扩展方法， 能根据elementId调到element
/// </summary>
    public static class ElementIdExtension
    {
        public static Element GetElement(this ElementId id, Document doc)
        {
            return doc.GetElement(id);
        }
    }
}
