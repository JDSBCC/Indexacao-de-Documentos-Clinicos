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

namespace Glintths.Er.Common.DataContracts
{
	/// <summary>
	/// Data Contract Class - Mcdt
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "Mcdt")]
	public partial class Mcdt 
	{
		private System.Nullable<long> mcdtId;
		private string code;
		private string description;
		private string altDescription;
		private bool active;
		private bool requestable;
		private Glintths.Er.Common.DataContracts.Rubric rubric;
		private ServiceList execServices;
		private Glintths.Er.Common.DataContracts.CrudOperation crudOperation;
		private ProductList products;
		private string canDomicile;
		private Glintths.Er.Common.DataContracts.GenericItems sides;
		private string codeArs;
		private string convArea;
		private System.Nullable<System.DateTime> lastRequiredDate;
		private MessageList messageList;
		
		[WcfSerialization::DataMember(Name = "McdtId", IsRequired = false, Order = 0)]
		public System.Nullable<long> McdtId
		{
		  get { return mcdtId; }
		  set { mcdtId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Code", IsRequired = true, Order = 1)]
		public string Code
		{
		  get { return code; }
		  set { code = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Description", IsRequired = true, Order = 2)]
		public string Description
		{
		  get { return description; }
		  set { description = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "AltDescription", IsRequired = false, Order = 3)]
		public string AltDescription
		{
		  get { return altDescription; }
		  set { altDescription = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Active", IsRequired = true, Order = 4)]
		public bool Active
		{
		  get { return active; }
		  set { active = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Requestable", IsRequired = true, Order = 5)]
		public bool Requestable
		{
		  get { return requestable; }
		  set { requestable = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Rubric", IsRequired = true, Order = 6)]
		public Glintths.Er.Common.DataContracts.Rubric Rubric
		{
		  get { return rubric; }
		  set { rubric = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "ExecServices", IsRequired = false, Order = 7)]
		public ServiceList ExecServices
		{
		  get { return execServices; }
		  set { execServices = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "CrudOperation", IsRequired = false, Order = 8)]
		public Glintths.Er.Common.DataContracts.CrudOperation CrudOperation
		{
		  get { return crudOperation; }
		  set { crudOperation = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Products", IsRequired = false, Order = 9)]
		public ProductList Products
		{
		  get { return products; }
		  set { products = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "CanDomicile", IsRequired = false, Order = 10)]
		public string CanDomicile
		{
		  get { return canDomicile; }
		  set { canDomicile = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Sides", IsRequired = false, Order = 11)]
		public Glintths.Er.Common.DataContracts.GenericItems Sides
		{
		  get { return sides; }
		  set { sides = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "CodeArs", IsRequired = false, Order = 12)]
		public string CodeArs
		{
		  get { return codeArs; }
		  set { codeArs = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "ConvArea", IsRequired = false, Order = 13)]
		public string ConvArea
		{
		  get { return convArea; }
		  set { convArea = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "LastRequiredDate", IsRequired = false, Order = 14)]
		public System.Nullable<System.DateTime> LastRequiredDate
		{
		  get { return lastRequiredDate; }
		  set { lastRequiredDate = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "MessageList", IsRequired = false, Order = 15)]
		public MessageList MessageList
		{
		  get { return messageList; }
		  set { messageList = value; }
		}				
	}
}

