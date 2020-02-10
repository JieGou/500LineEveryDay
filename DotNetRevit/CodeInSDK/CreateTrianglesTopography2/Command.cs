using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.DB.Architecture;


namespace RevitDevelopmentFoudation.CodeInSDK.CreateTrianglesTopography2
{
    [Transaction(TransactionMode.Manual)]
    [Regeneration(RegenerationOption.Manual)]
    public class Command : IExternalCommand
    {
        /// <summary>
        /// 画的是地形图
        /// </summary>
        /// <param name="commandData"></param>
        /// <param name="message"></param>
        /// <param name="elements"></param>
        /// <returns></returns>
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                Document document = commandData.Application.ActiveUIDocument.Document;
                TrianglesData trianglesData = TrianglesData.Load();

                using (Transaction tran = new Transaction(document, "create triangles"))
                {
                    tran.Start();

                    IList<PolymeshFacet> triangleFacets = new List<PolymeshFacet>();

                    foreach (IList<int> facet in trianglesData.Facets)
                    {
                        triangleFacets.Add(new PolymeshFacet(facet[0], facet[1], facet[2]));
                    }

                    TopographySurface topoSurface =
                        TopographySurface.Create(document, trianglesData.Points, triangleFacets);

                    Parameter name = topoSurface.get_Parameter(BuiltInParameter.ROOM_NAME);

                    if (name != null)
                    {
                        name.Set("CreateTrianglesTopography");
                    }

                    tran.Commit();
                }

                return Result.Succeeded;
            }
            catch (Exception e)
            {
                message = e.Message;
                return Result.Failed;
            }
        }
    }
}