
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
	/// Service Contract Class - GetHospitalStaffByUsernameResponse
	/// </summary>
	[WCF::MessageContract(WrapperName = "GetHospitalStaffByUsernameResponse", WrapperNamespace = "http://Glintths.Er.Services.Model/2010/Glintths")] 
	public partial class GetHospitalStaffByUsernameResponse
	{
		private Glintths.Er.Common.DataContracts.HospitalStaff hospitalStaff;
	 		
		[WCF::MessageBodyMember(Namespace = "http://Glintths.Er.Services.Model/2010/Glintths", Name = "HospitalStaff")]
		public Glintths.Er.Common.DataContracts.HospitalStaff HospitalStaff
		{
			get { return hospitalStaff; }
			set { hospitalStaff = value; }
		}
	}
}

