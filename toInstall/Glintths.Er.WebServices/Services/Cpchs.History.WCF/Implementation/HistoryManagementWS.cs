using System.Collections.Generic;
using System.Text;

using Cpchs.History.WCF.MessageContracts;
using Cpchs.History.WCF.BusinessLogic;
using Cpchs.History.WCF.DataContracts;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace Cpchs.History.WCF.ServiceImplementation
{
    public partial class HistoryManagementWS
    {
        public override GetTreeLevelsResponse GetTreeLevels(GetTreeLevelsRequest request)
        {
            string result = HistoryLogic.GetTreeLevels(request.CompanyDb, request.Scope, request.SearchId, request.Attributes);

            MemoryStream memoryStream = new MemoryStream(new UTF8Encoding().GetBytes(result));
            new XmlTextWriter(memoryStream, Encoding.UTF8);
            XmlSerializer xs = new XmlSerializer(typeof(List<Node>));
            List<Node> nodes = (List<Node>)xs.Deserialize(memoryStream);

            Nodes nodesFinal = new Nodes();
            nodesFinal.AddRange(nodes);

            GetTreeLevelsResponse response = new GetTreeLevelsResponse {TreeLevels = new NodesList {Nodes = nodesFinal}};
            return response;
        }

        public override GetNodeCellsResponse GetNodeCells(GetNodeCellsRequest request)
        {
            string result = HistoryLogic.GetNodeCells(request.CompanyDb, request.Scope, request.Mode, request.SearchId, request.DateBegin, request.DateEnd, request.Attributes);

            MemoryStream memoryStream = new MemoryStream(new UTF8Encoding().GetBytes(result));
            new XmlTextWriter(memoryStream, Encoding.UTF8);
            XmlSerializer xs = new XmlSerializer(typeof(List<NodeDistr>));
            List<NodeDistr> nodes = (List<NodeDistr>)xs.Deserialize(memoryStream);

            NodeDistrs nodesFinal = new NodeDistrs();
            nodesFinal.AddRange(nodes);

            GetNodeCellsResponse response = new GetNodeCellsResponse
                                                {NodeCells = new NodeDistrsList {NodeDistrs = nodesFinal}};
            return response;
        }

    }
}