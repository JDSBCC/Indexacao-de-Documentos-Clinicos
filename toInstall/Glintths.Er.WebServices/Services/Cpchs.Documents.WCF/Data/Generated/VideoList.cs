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

namespace Cpchs.Documents.WCF.DataContracts
{
	/// <summary>
	/// Data Contract Class - VideoList
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "urn:Cpchs.Documents", Name = "VideoList")]
	public partial class VideoList 
	{
		private Videos videos;
		
		[WcfSerialization::DataMember(Name = "Videos", IsRequired = false, Order = 0)]
		public Videos Videos
		{
		  get { return videos; }
		  set { videos = value; }
		}				
	}
}

