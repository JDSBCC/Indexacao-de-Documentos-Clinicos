using System;

namespace Cpchs.Documents.WCF.BusinessLogic
{
    public sealed class BusinessRulesException : Exception
    {
        internal BusinessRulesException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
