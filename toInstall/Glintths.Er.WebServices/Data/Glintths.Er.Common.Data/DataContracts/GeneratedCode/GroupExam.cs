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
	/// Data Contract Class - GroupExam
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "GroupExam")]
	public partial class GroupExam 
	{
		private long id;
		private string execService;
		private Glintths.Er.Common.DataContracts.CrudOperation crudOperation;
		private int quantity;
		private string description;
		private string execServiceDescription;
		private string codExt;
		private string codRubr;
		
		[WcfSerialization::DataMember(Name = "Id", IsRequired = false, Order = 0)]
		public long Id
		{
		  get { return id; }
		  set { id = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "ExecService", IsRequired = false, Order = 1)]
		public string ExecService
		{
		  get { return execService; }
		  set { execService = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "CrudOperation", IsRequired = false, Order = 2)]
		public Glintths.Er.Common.DataContracts.CrudOperation CrudOperation
		{
		  get { return crudOperation; }
		  set { crudOperation = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Quantity", IsRequired = true, Order = 3)]
		public int Quantity
		{
		  get { return quantity; }
		  set { quantity = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Description", IsRequired = false, Order = 4)]
		public string Description
		{
		  get { return description; }
		  set { description = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "ExecServiceDescription", IsRequired = false, Order = 5)]
		public string ExecServiceDescription
		{
		  get { return execServiceDescription; }
		  set { execServiceDescription = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "CodExt", IsRequired = false, Order = 6)]
		public string CodExt
		{
		  get { return codExt; }
		  set { codExt = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "CodRubr", IsRequired = false, Order = 7)]
		public string CodRubr
		{
		  get { return codRubr; }
		  set { codRubr = value; }
		}				
	}
}

