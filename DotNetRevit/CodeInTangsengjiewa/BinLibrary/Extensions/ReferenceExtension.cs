using Autodesk.Revit.DB;

namespace CodeInTangsengjiewa.BinLibrary.Extensions
{
    public static class ReferenceExtension
    {
        public static Element GetElement(this Reference thisref, Document doc)
        {
            return doc.GetElement(thisref);
        }
    }
}