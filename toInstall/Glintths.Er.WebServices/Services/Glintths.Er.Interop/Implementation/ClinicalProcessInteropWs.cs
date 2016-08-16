using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Glintths.Er.Interop.MessageContracts;
using Glintths.Er.Interop.BusinessLogic.ClinicalProcess;

namespace Glintths.Er.Interop.ServiceImplementation
{
    public partial class ClinicalProcessInteropWs
    {
        public ClinicalProcessInteropWs()
        {
        }

        public override PerformActionsResponse PerformActions(PerformActionsRequest request)
        {
            string message, stackTrace;
            PerformActionsResponse resp = new PerformActionsResponse();
            resp.Success = ClinicalProcessInteropLogic.PerformAction(request.CompanyDb, request.ActionsXml, out message, out stackTrace);
            resp.Message = message;
            resp.StackTrace = stackTrace;
            return resp;
        }
    }
}