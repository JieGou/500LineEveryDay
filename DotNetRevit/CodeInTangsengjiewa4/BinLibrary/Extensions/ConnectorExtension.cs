using Autodesk.Revit.DB;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeInTangsengjiewa.BinLibrary.Extensions;

namespace CodeInTangsengjiewa4.BinLibrary.Extensions
{
    /// <summary>
    /// 做什么用的?
    /// </summary>
    public static class ConnectorExtension
    {
        public static Connector GetConnectedCon(this Connector connector)
        {
            var result = default(Connector);
            var connectors = connector.AllRefs;
            var connectorDir = connector.CoordinateSystem.BasisZ;
            var connectorOrigin = connector.Origin;

            foreach (Connector con in connectors)
            {
                if (con.ConnectorType == ConnectorType.End || con.ConnectorType == ConnectorType.Curve)
                {
                    var conOrigin = con.Origin;
                    var conDir = con.CoordinateSystem.BasisZ;
                    if (connectorOrigin.IsAlmostEqualTo(conOrigin) && connectorDir.IsOppositeDirection(conDir))
                    {
                        result = con;
                    }
                }
            }
            return result;
        }
    }
}