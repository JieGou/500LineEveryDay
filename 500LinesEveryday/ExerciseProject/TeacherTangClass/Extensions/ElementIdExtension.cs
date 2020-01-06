using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;

namespace ExerciseProject.TeacherTangClass.Extensions
{
    public static class ElementIdExtension
    {
        public static Element GetElement(this ElementId id, Document doc)
        {
            return doc.GetElement(id);
        }
    }
}
