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

namespace Glintths.Er.Interop.DataContracts
{
	/// <summary>
	/// Data Contract Class - JobInfo
	/// </summary>
	[WcfSerialization::DataContract(Namespace = "urn:Glintths.Er.Interop", Name = "JobInfo")]
	public partial class JobInfo 
	{
		private long jobID;
		private string jobURL;
		private string jobStatus;
		private string jobClient;
		private long jobPercentage;
		private string jobContext;
		private string jobClientFileDeleted;
		private System.DateTime jobCompletedTimestamp;
		private System.DateTime jobRegisteredTimestamp;
		private string jobDataField;
		
		[WcfSerialization::DataMember(Name = "JobID", IsRequired = false, Order = 6)]
		public long JobID
		{
		  get { return jobID; }
		  set { jobID = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "JobURL", IsRequired = false, Order = 4)]
		public string JobURL
		{
		  get { return jobURL; }
		  set { jobURL = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "JobStatus", IsRequired = false, Order = 0)]
		public string JobStatus
		{
		  get { return jobStatus; }
		  set { jobStatus = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "JobClient", IsRequired = false, Order = 5)]
		public string JobClient
		{
		  get { return jobClient; }
		  set { jobClient = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "JobPercentage", IsRequired = false, Order = 3)]
		public long JobPercentage
		{
		  get { return jobPercentage; }
		  set { jobPercentage = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "JobContext", IsRequired = false, Order = 7)]
		public string JobContext
		{
		  get { return jobContext; }
		  set { jobContext = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "JobClientFileDeleted", IsRequired = false, Order = 8)]
		public string JobClientFileDeleted
		{
		  get { return jobClientFileDeleted; }
		  set { jobClientFileDeleted = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "JobCompletedTimestamp", IsRequired = false, Order = 1)]
		public System.DateTime JobCompletedTimestamp
		{
		  get { return jobCompletedTimestamp; }
		  set { jobCompletedTimestamp = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "JobRegisteredTimestamp", IsRequired = false, Order = 2)]
		public System.DateTime JobRegisteredTimestamp
		{
		  get { return jobRegisteredTimestamp; }
		  set { jobRegisteredTimestamp = value; }
		}				
		
		[WcfSerialization::DataMember(Name = "jobData", IsRequired = false, Order = 9)]
		public string jobData
		{
		  get { return jobDataField; }
		  set { jobDataField = value; }
		}				
	}
}

