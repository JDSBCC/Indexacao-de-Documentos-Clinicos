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
	/// Data Contract Class - DocumentLog
	/// </summary>	
	[WcfSerialization::DataContract(Namespace = "urn:Cpchs.Documents", Name = "DocumentLog")]
	public partial class DocumentLog 
	{
		private System.Nullable<long> instId;
		private string instDesc;
		private System.Nullable<long> placeId;
		private string placeDesc;
		private System.Nullable<long> appId;
		private string appDesc;
		private System.Nullable<long> docTypeId;
		private string docTypeDesc;
		private string doc;
		private System.Nullable<System.DateTime> docDate;
		private System.Nullable<System.DateTime> arrivDate;
		private System.Nullable<System.DateTime> procDate;
		private long statusId;
		private string statusDesc;
		private System.Nullable<long> patTypeId;
		private string patTypeDesc;
		private string patId;
		private System.Nullable<long> docId;
		private long xmlId;
		private DocumentEpisodeLogs documentEpisodeLogs;
		private DocumentIndexLogs documentIndexLogs;
		private long docLogId;
		private bool isProcessable;
		private bool isIgnorable;
		
		[WcfSerialization::DataMember(Name = "InstId", IsRequired = true, Order = 0)]
		public System.Nullable<long> InstId
		{
		  get { return instId; }
		  set { instId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "InstDesc", IsRequired = true, Order = 1)]
		public string InstDesc
		{
		  get { return instDesc; }
		  set { instDesc = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "PlaceId", IsRequired = true, Order = 2)]
		public System.Nullable<long> PlaceId
		{
		  get { return placeId; }
		  set { placeId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "PlaceDesc", IsRequired = true, Order = 3)]
		public string PlaceDesc
		{
		  get { return placeDesc; }
		  set { placeDesc = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "AppId", IsRequired = true, Order = 4)]
		public System.Nullable<long> AppId
		{
		  get { return appId; }
		  set { appId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "AppDesc", IsRequired = true, Order = 5)]
		public string AppDesc
		{
		  get { return appDesc; }
		  set { appDesc = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "DocTypeId", IsRequired = true, Order = 6)]
		public System.Nullable<long> DocTypeId
		{
		  get { return docTypeId; }
		  set { docTypeId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "DocTypeDesc", IsRequired = true, Order = 7)]
		public string DocTypeDesc
		{
		  get { return docTypeDesc; }
		  set { docTypeDesc = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "Doc", IsRequired = true, Order = 8)]
		public string Doc
		{
		  get { return doc; }
		  set { doc = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "DocDate", IsRequired = true, Order = 9)]
		public System.Nullable<System.DateTime> DocDate
		{
		  get { return docDate; }
		  set { docDate = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "ArrivDate", IsRequired = true, Order = 10)]
		public System.Nullable<System.DateTime> ArrivDate
		{
		  get { return arrivDate; }
		  set { arrivDate = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "ProcDate", IsRequired = false, Order = 11)]
		public System.Nullable<System.DateTime> ProcDate
		{
		  get { return procDate; }
		  set { procDate = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "StatusId", IsRequired = true, Order = 12)]
		public long StatusId
		{
		  get { return statusId; }
		  set { statusId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "StatusDesc", IsRequired = true, Order = 13)]
		public string StatusDesc
		{
		  get { return statusDesc; }
		  set { statusDesc = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "PatTypeId", IsRequired = false, Order = 14)]
		public System.Nullable<long> PatTypeId
		{
		  get { return patTypeId; }
		  set { patTypeId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "PatTypeDesc", IsRequired = false, Order = 15)]
		public string PatTypeDesc
		{
		  get { return patTypeDesc; }
		  set { patTypeDesc = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "PatId", IsRequired = false, Order = 16)]
		public string PatId
		{
		  get { return patId; }
		  set { patId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "DocId", IsRequired = false, Order = 17)]
		public System.Nullable<long> DocId
		{
		  get { return docId; }
		  set { docId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "XmlId", IsRequired = true, Order = 18)]
		public long XmlId
		{
		  get { return xmlId; }
		  set { xmlId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "DocumentEpisodeLogs", IsRequired = false, Order = 19)]
		public DocumentEpisodeLogs DocumentEpisodeLogs
		{
		  get { return documentEpisodeLogs; }
		  set { documentEpisodeLogs = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "DocumentIndexLogs", IsRequired = true, Order = 20)]
		public DocumentIndexLogs DocumentIndexLogs
		{
		  get { return documentIndexLogs; }
		  set { documentIndexLogs = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "DocLogId", IsRequired = true, Order = 21)]
		public long DocLogId
		{
		  get { return docLogId; }
		  set { docLogId = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "IsProcessable", IsRequired = true, Order = 22)]
		public bool IsProcessable
		{
		  get { return isProcessable; }
		  set { isProcessable = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "IsIgnorable", IsRequired = true, Order = 23)]
		public bool IsIgnorable
		{
		  get { return isIgnorable; }
		  set { isIgnorable = value; }
		}				
	}
}
