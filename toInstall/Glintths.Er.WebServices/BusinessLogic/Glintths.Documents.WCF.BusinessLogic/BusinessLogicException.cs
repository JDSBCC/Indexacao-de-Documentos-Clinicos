using System;

namespace Cpchs.Documents.WCF.BusinessLogic
{
    public sealed class BusinessLogicException : Exception
    {
        internal BusinessLogicException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
