using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Google.Protobuf;
using System.Web.Script.Serialization;

namespace RevitDevelopmentFoudation.CodeInSDK.CreateTrianglesTopography2
{
    class TrianglesData
    {
        //the points represent an enclosed area in the xy plane
        public IList<XYZ> Points { set; get; }

        //Triangle faces composing a polygon mesh
        public IList<IList<int>> Facets { set; get; }

        public static TrianglesData Load()
        {
            // string assemblyFileFoder = Path.GetDirectoryName(typeof(TrianglesData).Assembly.Location);
            // string emmfilePath = Path.Combine(assemblyFileFoder, "TrianglesData.json");
            // string emmfileContent = File.ReadAllText(emmfilePath);

            string Path =
                @"D:\githubRep2\Gitee500LinesEveryday\DotNetRevit\CodeInSDK\CreateTrianglesTopography2\TrianglesData.json";
            string emmfileContent = File.ReadAllText(Path);
            return JSONParse(emmfileContent);
        }

        private static TrianglesData JSONParse(string jsonString)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RegisterConverters(new JavaScriptConverter[] {new XYZConverter()});

            return serializer.Deserialize(jsonString, typeof(TrianglesData)) as TrianglesData;
        }


        public class XYZConverter : JavaScriptConverter
        {
            public override object Deserialize(
                IDictionary<string, object> dictionary, Type type,
                JavaScriptSerializer serializer)
            {
                return new XYZ(Convert.ToDouble(dictionary["X"]),
                               Convert.ToDouble(dictionary["Y"]),
                               Convert.ToDouble(dictionary["Z"]));
            }

            public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
            {
                Dictionary<string, object> dic = new Dictionary<string, object>();
                var node = obj as XYZ;
                if (node == null)
                    return null;
                dic.Add("X", node.X);
                dic.Add("Y", node.Y);
                dic.Add("Z", node.Z);

                return dic;
            }

            public override IEnumerable<Type> SupportedTypes
            {
                get
                {
                    return new Type[] {typeof(XYZ)};
                }
            }
        }
    }
}