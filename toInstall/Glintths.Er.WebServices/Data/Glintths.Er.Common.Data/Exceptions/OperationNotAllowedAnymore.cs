using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cpchs.Eresults.Common.WCF.Exceptions
{
    [Serializable]
    public class OperationNotAllowedAnymore: Exception
    {
        public string ErrorMessage
        {
            get
            {
                return "Já não é possível alterar a informação.";
            }
        }

        public int ErrorCode
        {
            get
            {
                return 20500;
            }
        }
    }
}
