//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using WcfSerialization = global::System.Runtime.Serialization;

namespace Cpchs.Entities.WCF.DataContracts
{
	/// <summary>
	/// Data Contract Class - PaginationRequest
	/// </summary>
	[WcfSerialization::DataContract(Namespace = "urn:Cpchs.Entities", Name = "PaginationRequest")]
	[WcfSerialization::KnownType(typeof(OrderTypeEnum))]
	public partial class PaginationRequest 
	{
		private System.Nullable<long> pageNumber;
		private System.Nullable<long> itemsPerPage;
		private string orderField;
		private OrderTypeEnum orderType;
        private System.Nullable<long> resultsCount;
		
		[WcfSerialization::DataMember(Name = "PageNumber", IsRequired = true, Order = 0)]
		public System.Nullable<long> PageNumber
		{
		  get { return pageNumber; }
		  set { pageNumber = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "ItemsPerPage", IsRequired = true, Order = 1)]
		public System.Nullable<long> ItemsPerPage
		{
		  get { return itemsPerPage; }
		  set { itemsPerPage = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "OrderField", IsRequired = true, Order = 2)]
		public string OrderField
		{
		  get { return orderField; }
		  set { orderField = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "OrderType", IsRequired = false, Order = 3)]
		public OrderTypeEnum OrderType
		{
		  get { return orderType; }
		  set { orderType = value; }
		}

        [WcfSerialization::DataMember(Name = "ResultsCount", IsRequired = false, Order = 4)]
        public long? ResultsCount
        {
            get { return resultsCount; }
            set { resultsCount = value; }
        }
	}
}
