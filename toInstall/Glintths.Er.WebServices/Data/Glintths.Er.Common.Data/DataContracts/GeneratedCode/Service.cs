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
	/// Data Contract Class - Service
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "Service")]
	public partial class Service 
	{
		private string code;
		private string description;
		private bool isMicro;
		private bool infoClinMand;
		private Glintths.Er.Common.DataContracts.CrudOperation crudOperation;
		
		[WcfSerialization::DataMember(Name = "Code", IsRequired = true, Order = 0)]
		public string Code
		{
		  get { return code; }
		  set { code = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Description", IsRequired = true, Order = 1)]
		public string Description
		{
		  get { return description; }
		  set { description = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "IsMicro", IsRequired = false, Order = 2)]
		public bool IsMicro
		{
		  get { return isMicro; }
		  set { isMicro = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "InfoClinMand", IsRequired = false, Order = 3)]
		public bool InfoClinMand
		{
		  get { return infoClinMand; }
		  set { infoClinMand = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "CrudOperation", IsRequired = false, Order = 4)]
		public Glintths.Er.Common.DataContracts.CrudOperation CrudOperation
		{
		  get { return crudOperation; }
		  set { crudOperation = value; }
		}				
	}
}
