using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace PracticeBook.Day0120
{
    /*
     * 练习10: revit api中有静态方法的类
     *     
     */
    public class T012010
    {
        static void Main(string[] args)
        {
        }
    }

    /*
  //共同点: 创建实例的静态方法;
            获取类Id 版本号的静态方法;

    Category:

        public static Category GetCategory(Document document, BuiltInCategory categoryId);
        public static Category GetCategory(Document document, ElementId categoryId);
        public static bool IsBuiltInCategoryValid(BuiltInCategory builtInCategory);


    Reference:
         public static Reference ParseFromStableRepresentation(Document document, string representation);
              
    Document
        public static DocumentVersion GetDocumentVersion(Document doc);

    Pipe
        public static Pipe Create(Document document, ElementId pipeTypeId, ElementId levelId, Connector startConnector, Connector endConnector);
        public static Pipe Create(Document document, ElementId pipeTypeId, ElementId levelId, Connector startConnector, XYZ endPoint);
        public static Pipe Create(Document document, ElementId systemTypeId, ElementId pipeTypeId, ElementId levelId, XYZ startPoint, XYZ endPoint);
        public static Pipe CreatePlaceholder(Document document, ElementId systemTypeId, ElementId pipeTypeId, ElementId levelId, XYZ startPoint, XYZ endPoint);
        public static bool IsPipeTypeId(Document document, ElementId pipeTypeId);
        public static bool IsPipingConnector(Connector connector);
        public static bool IsPipingSystemTypeId(Document document, ElementId systemTypeId);

    Duct
    public static Duct Create(Document document, ElementId ductTypeId, ElementId levelId, Connector startConnector, Connector endConnector);
    public static Duct Create(Document document, ElementId ductTypeId, ElementId levelId, Connector startConnector, XYZ endPoint);
    public static Duct Create(Document document, ElementId systemTypeId, ElementId ductTypeId, ElementId levelId, XYZ startPoint, XYZ endPoint);
    public static Duct CreatePlaceholder(Document document, ElementId systemTypeId, ElementId ductTypeId, ElementId levelId, XYZ startPoint, XYZ endPoint);
    public static bool IsDuctTypeId(Document document, ElementId ductTypeId);
    public static bool IsHvacSystemTypeId(Document document, ElementId systemTypeId);

    CableTray
    public static CableTray Create(Document document, ElementId cabletrayType, XYZ startPoint, XYZ endPoint, ElementId levelId);
    public static bool IsValidCableTrayType(Document document, ElementId cabletrayType);
    
    Level
    public static Level Create(Document document, double elevation);

    LabelUtils
    public static string GetLabelFor(BuiltInCategory builtInCategory);
    public static string GetLabelFor(BuiltInParameter builtInParam);
    public static string GetLabelFor(BuiltInParameterGroup builtInParamGroup);
    public static string GetLabelFor(DisplayUnitType displayUnitType);
    public static string GetLabelFor(ParameterType paramType);
    public static string GetLabelFor(UnitSymbolType unitSymbolType);
    public static string GetLabelFor(UnitType unitType);
    public static string GetLabelFor(gbXMLBuildingType buildingType, Document document);
    public static string GetLabelFor(BuiltInParameter builtInParam, LanguageType language);
    public static string GetLabelFor(DuctLossMethodType ductLossMethodType, Document doc);
    public static string GetLabelFor(PipeFlowState pipeFlowState, Document doc);
    public static string GetLabelFor(PipeLossMethodType pipeLossMethodType, Document doc);
    private static string GetLabelForEnum(BuiltInParameter builtInParam, int LossMethodType, Document doc);


     */
}