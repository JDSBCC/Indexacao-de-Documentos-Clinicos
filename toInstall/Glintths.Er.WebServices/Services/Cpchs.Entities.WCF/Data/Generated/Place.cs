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
	/// Data Contract Class - Place
	/// </summary>
	[WcfSerialization::DataContract(Namespace = "urn:Cpchs.Entities", Name = "Place")]
	public partial class Place 
	{
		private long id;
		private string code;
		private string acronym;
		private string description;
		private ApplicationCollection applications;
		
		[WcfSerialization::DataMember(Name = "Id", IsRequired = false, Order = 0)]
		public long Id
		{
		  get { return id; }
		  set { id = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Code", IsRequired = false, Order = 1)]
		public string Code
		{
		  get { return code; }
		  set { code = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Acronym", IsRequired = false, Order = 2)]
		public string Acronym
		{
		  get { return acronym; }
		  set { acronym = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Description", IsRequired = false, Order = 3)]
		public string Description
		{
		  get { return description; }
		  set { description = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Applications", IsRequired = false, Order = 4)]
		public ApplicationCollection Applications
		{
		  get { return applications; }
		  set { applications = value; }
		}				
	}
}

