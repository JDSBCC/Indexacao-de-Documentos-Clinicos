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
	/// Data Contract Class - HGroup
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "HGroup")]
	public partial class HGroup 
	{
		private long id;
		private string code;
		private string acronym;
		private string description;
		private string scope;
		private long parentId;
		private HGroupExams exams;
		private HGroups hGroups;
		
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
		
		[WcfSerialization::DataMember(Name = "Scope", IsRequired = false, Order = 4)]
		public string Scope
		{
		  get { return scope; }
		  set { scope = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "ParentId", IsRequired = false, Order = 5)]
		public long ParentId
		{
		  get { return parentId; }
		  set { parentId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Exams", IsRequired = false, Order = 6)]
		public HGroupExams Exams
		{
		  get { return exams; }
		  set { exams = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "HGroups", IsRequired = false, Order = 7)]
		public HGroups HGroups
		{
		  get { return hGroups; }
		  set { hGroups = value; }
		}				
	}
}
