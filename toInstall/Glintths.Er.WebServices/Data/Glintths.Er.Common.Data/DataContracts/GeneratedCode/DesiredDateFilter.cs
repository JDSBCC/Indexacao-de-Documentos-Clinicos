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
	/// Data Contract Class - DesiredDateFilter
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "DesiredDateFilter")]
	public partial class DesiredDateFilter 
	{
		private string episodeType;
		private bool postExecution;
		
		[WcfSerialization::DataMember(Name = "EpisodeType", IsRequired = false, Order = 0)]
		public string EpisodeType
		{
		  get { return episodeType; }
		  set { episodeType = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "PostExecution", IsRequired = false, Order = 1)]
		public bool PostExecution
		{
		  get { return postExecution; }
		  set { postExecution = value; }
		}				
	}
}
