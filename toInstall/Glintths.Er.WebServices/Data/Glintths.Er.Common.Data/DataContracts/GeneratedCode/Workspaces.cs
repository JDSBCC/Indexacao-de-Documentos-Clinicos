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
	/// Data Contract Class - Workspaces
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "http://Glintths.Er.Services.Model/2011/Glintths", Name = "Workspaces")]
	public partial class Workspaces 
	{
		private WorkspaceList items;
		
		[WcfSerialization::DataMember(Name = "Items", IsRequired = false, Order = 1)]
		public WorkspaceList Items
		{
		  get { return items; }
		  set { items = value; }
		}				
	}
}

