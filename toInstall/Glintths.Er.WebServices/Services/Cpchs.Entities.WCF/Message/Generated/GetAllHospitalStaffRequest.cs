
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using WCF = global::System.ServiceModel;

namespace Glintths.Er.Entities.MessageContracts
{
	/// <summary>
	/// Service Contract Class - GetAllHospitalStaffRequest
	/// </summary>
	[WCF::MessageContract(WrapperName = "GetAllHospitalStaffRequest", WrapperNamespace = "http://Glintths.Er.Services.Model/2010/Glintths")] 
	public partial class GetAllHospitalStaffRequest
	{
        private Cpchs.Entities.WCF.DataContracts.BaseConfiguration configuration;
        private Cpchs.Entities.WCF.DataContracts.PaginationRequest pagination;
	 		
		[WCF::MessageBodyMember(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "Configuration")]
        public Cpchs.Entities.WCF.DataContracts.BaseConfiguration Configuration
		{
			get { return configuration; }
			set { configuration = value; }
		}
			
		[WCF::MessageBodyMember(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "Pagination")]
        public Cpchs.Entities.WCF.DataContracts.PaginationRequest Pagination
		{
			get { return pagination; }
			set { pagination = value; }
		}
	}
}

