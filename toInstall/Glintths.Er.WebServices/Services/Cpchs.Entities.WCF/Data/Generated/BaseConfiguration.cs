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
	/// Data Contract Class - BaseConfiguration
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "BaseConfiguration")]
	public partial class BaseConfiguration 
	{
		private string companyDb;
		private string username;
		private string machine;
		
		[WcfSerialization::DataMember(Name = "CompanyDb", IsRequired = true, Order = 0)]
		public string CompanyDb
		{
		  get { return companyDb; }
		  set { companyDb = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Username", IsRequired = false, Order = 1)]
		public string Username
		{
		  get { return username; }
		  set { username = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Machine", IsRequired = true, Order = 2)]
		public string Machine
		{
		  get { return machine; }
		  set { machine = value; }
		}				
	}
}

