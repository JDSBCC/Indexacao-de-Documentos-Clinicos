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
	/// Data Contract Class - EpisodeType
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "EpisodeType")]
	public partial class EpisodeType 
	{
		private string code;
		private string description;
		
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
	}
}

