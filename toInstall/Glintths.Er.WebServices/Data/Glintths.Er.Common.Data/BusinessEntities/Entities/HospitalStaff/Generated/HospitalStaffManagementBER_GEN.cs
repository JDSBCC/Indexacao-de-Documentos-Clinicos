
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using Glintths.Er.Common.BusinessEntities;
using CPCHS.Common.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Glintths.Er.Common.BusinessEntities.Generated
{
    /// <summary>
    /// Date Created: quinta-feira, 16 de Setembro de 2010
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public abstract class HospitalStaffManagementBER_GEN : CommonBER
    {
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2235:MarkAllNonSerializableFields"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		protected Microsoft.Practices.EnterpriseLibrary.Data.Database dal;	
		
		#region Variables
		#endregion
	
		protected HospitalStaffManagementBER_GEN()
		{
			/*try
			{
				dal=DatabaseFactory.CreateDatabase("ErEntities");
			}
			catch 
			{	
				dal = DatabaseFactory.CreateDatabase();
			}*/
	
		}
	
    #region TABLES OPERATIONS
	
		#region Cache HOSPITALSTAFF
		#endregion
	
	
		#region Select HOSPITALSTAFF Operations
		//
		//SELECT OPERATIONS HOSPITALSTAFF
		//
		//
		//
		//
		public virtual HospitalStaffList GetAllHospitalStaff(string companyDB)
		{
			IDataReader reader = GetAllHospitalStaffDB(companyDB);
			HospitalStaffList list = new HospitalStaffList();
			while(reader.Read())
			{
				try
    			{
					list.Add(new HospitalStaff(reader, companyDB));
				}
				catch(Exception ex)
				{
					// Quick Start is configured so that the Propagate Policy will
           			// log the exception and then recommend a rethrow.
            		bool rethrow = ExceptionPolicy.HandleException(ex, "Business Entities Exception Policy");
            		if (rethrow)
            		{
            			throw;
            			} 
				}
			} 
			reader.Close();
			return list;
		}
		
		//
		//
		//
		public virtual HospitalStaff GetHospitalStaffByUsername(string companyDB, string username )
		{
			IDataReader reader = GetHospitalStaffByUsernameDB(companyDB, username );
			HospitalStaff obj = null;
			if(reader.Read())
			{
				try
      			{
					obj = new HospitalStaff(reader, companyDB);
				}
				catch(Exception ex)
				{
					// Quick Start is configured so that the Propagate Policy will
            		// log the exception and then recommend a rethrow.
            		bool rethrow = ExceptionPolicy.HandleException(ex, "Business Entities Exception Policy");
            		if (rethrow)
            		{
	           		 	throw;
  	          		} 
				}
			} 
			reader.Close();

			return obj;
		}
		
		//
		//
		//
		public virtual HospitalStaffList GetHospitalStaffByType(string companyDB, string hospStaffType)
		{
			IDataReader reader = GetHospitalStaffByTypeDB(companyDB, hospStaffType);
			HospitalStaffList list = new HospitalStaffList();
			while(reader.Read())
			{
				try
    			{
					list.Add(new HospitalStaff(reader, companyDB));
				}
				catch(Exception ex)
				{
					// Quick Start is configured so that the Propagate Policy will
           			// log the exception and then recommend a rethrow.
            		bool rethrow = ExceptionPolicy.HandleException(ex, "Business Entities Exception Policy");
            		if (rethrow)
            		{
            			throw;
            			} 
				}
			} 
			reader.Close();
			return list;
		}
		
		
		//
		// DB
		//
		protected virtual string GetAllHospitalStaffDBMethod(string companyDB)
		{
			string proc = GetAllHospitalStaffDBMethodName;
            string package = GetAllHospitalStaffDBPackageName;
			
			dal = CPCHS.Common.Database.Database.GetDatabase("ErEntities", companyDB);
            
            proc = GetDBMethod(dal, proc, package);
			
			return proc;
		}
		
		protected virtual string GetAllHospitalStaffDBMethodName
		{
            get { return "GetAllHospitalStaff"; }
        }
		
        protected virtual string GetAllHospitalStaffDBPackageName
		{
            get { return "PCK_ENTITIES_HOSPITALSTAFF_GEN"; }
        }

		
		protected virtual IDataReader GetAllHospitalStaffDB(string companyDB)
		{
			IDataReader ret = null;
			try
      		{
				string dbMethod = GetAllHospitalStaffDBMethod(companyDB);
                DbCommand dbCommand;
                
                dbCommand = GetStoredProcCommand(dal, dbMethod, DBNull.Value);
                ret = dal.ExecuteReader(dbCommand);
                
                
			}
			catch(Exception ex)
			{
			  	// Quick Start is configured so that the Propagate Policy will
        		// log the exception and then recommend a rethrow.
        		bool rethrow = ExceptionPolicy.HandleException(ex, "Database Exception Policy");
        		if (rethrow)
        		{
        			throw;
        		} 
			}
			return ret;
		}
		
		//
		// DB
		//
		protected virtual string GetHospitalStaffByUsernameDBMethod(string companyDB)
		{
			string proc = GetHospitalStaffByUsernameDBMethodName;
            string package = GetHospitalStaffByUsernameDBPackageName;
			
			dal = CPCHS.Common.Database.Database.GetDatabase("ErEntities", companyDB);
            
            proc = GetDBMethod(dal, proc, package);
			
			return proc;
		}
		
		protected virtual string GetHospitalStaffByUsernameDBMethodName
		{
            get { return "GetHospitalStaffByUsername"; }
        }
		
        protected virtual string GetHospitalStaffByUsernameDBPackageName
		{
            get { return "PCK_ENTITIES_HOSPITALSTAFF_GEN"; }
        }

		
		protected virtual IDataReader GetHospitalStaffByUsernameDB(string companyDB, string username )
		{
			IDataReader ret = null;
			try
      		{
				string dbMethod = GetHospitalStaffByUsernameDBMethod(companyDB);
                DbCommand dbCommand;
                
                dbCommand = GetStoredProcCommand(dal, dbMethod, username , DBNull.Value);
                ret = dal.ExecuteReader(dbCommand);
                
                
			}
			catch(Exception ex)
			{
			  	// Quick Start is configured so that the Propagate Policy will
        		// log the exception and then recommend a rethrow.
        		bool rethrow = ExceptionPolicy.HandleException(ex, "Database Exception Policy");
        		if (rethrow)
        		{
        			throw;
        		} 
			}
			return ret;
		}
		
		//
		// DB
		//
		protected virtual string GetHospitalStaffByTypeDBMethod(string companyDB)
		{
			string proc = GetHospitalStaffByTypeDBMethodName;
            string package = GetHospitalStaffByTypeDBPackageName;
			
			dal = CPCHS.Common.Database.Database.GetDatabase("ErEntities", companyDB);
            
            proc = GetDBMethod(dal, proc, package);
			
			return proc;
		}
		
		protected virtual string GetHospitalStaffByTypeDBMethodName
		{
            get { return "GetHospitalStaffByType"; }
        }
		
        protected virtual string GetHospitalStaffByTypeDBPackageName
		{
            get { return "PCK_ENTITIES_HOSPITALSTAFF_GEN"; }
        }

		
		protected virtual IDataReader GetHospitalStaffByTypeDB(string companyDB, string hospStaffType)
		{
			IDataReader ret = null;
			try
      		{
				string dbMethod = GetHospitalStaffByTypeDBMethod(companyDB);
                DbCommand dbCommand;
                
                dbCommand = GetStoredProcCommand(dal, dbMethod, hospStaffType, DBNull.Value);
                ret = dal.ExecuteReader(dbCommand);
                
                
			}
			catch(Exception ex)
			{
			  	// Quick Start is configured so that the Propagate Policy will
        		// log the exception and then recommend a rethrow.
        		bool rethrow = ExceptionPolicy.HandleException(ex, "Database Exception Policy");
        		if (rethrow)
        		{
        			throw;
        		} 
			}
			return ret;
		}
		

		#endregion
		
		#region Custom HOSPITALSTAFF Operations
        //
		//CUSTOM OPERATIONS HOSPITALSTAFF
		//       
		//
		//
		//
		public virtual void SetHospitalStaffUser(string companyDB, string mechanNum , string hospStaffType , string username ,params DbTransaction[] transaction)
		{
			try
      		{
      			if (transaction.Length==0)
					SetHospitalStaffUserDB(companyDB, mechanNum , hospStaffType , username  ,null);
				else	
					SetHospitalStaffUserDB(companyDB, mechanNum , hospStaffType , username ,transaction[0]);
			}
			catch(Exception ex)
			{
				// Quick Start is configured so that the Propagate Policy will
          		// log the exception and then recommend a rethrow.
          		bool rethrow = ExceptionPolicy.HandleException(ex, "Business Entities Exception Policy");
          		if (rethrow)
          		{
          			throw;
          		} 
			}
		}
             
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		protected virtual void SetHospitalStaffUserDB(string companyDB, string mechanNum , string hospStaffType , string username ,DbTransaction transaction)
		{
			try
      		{
				string dbMethod = SetHospitalStaffUserDBMethod(companyDB);
				DbCommand dbCommand = GetStoredProcCommand(
						dal,
                		dbMethod,
				        mechanNum , hospStaffType , username 
				);
				
				SetHospitalStaffUserDBPreQuery(dbCommand,mechanNum , hospStaffType , username );

				if (transaction != null) 
                {
					dal.ExecuteNonQuery(dbCommand, transaction);
                }else{
					dal.ExecuteNonQuery(dbCommand);
                }
                
                
                
				SetHospitalStaffUserDBPosQuery(dbCommand,mechanNum , hospStaffType , username );

			}
			catch(Exception ex)
			{
			  	// Quick Start is configured so that the Propagate Policy will
        		// log the exception and then recommend a rethrow.
        		bool rethrow = ExceptionPolicy.HandleException(ex, "Database Exception Policy");
        		if (rethrow)
        		{
        			throw;
        		} 
			}
		}      
        
        #region Process the name of DBMethod
        
		//
		// DB
		//
		protected virtual string SetHospitalStaffUserDBMethod(string companyDB)
		{
			string proc = SetHospitalStaffUserDBMethodName;
            string package = SetHospitalStaffUserDBPackageName;
            
			dal = CPCHS.Common.Database.Database.GetDatabase("ErEntities", companyDB);
			
			proc = GetDBMethod(dal, proc, package);				
			
			return proc;
		}
        
		protected virtual string SetHospitalStaffUserDBMethodName
		{
            get { return "SetHospitalStaffUser"; }
        }
		
        protected virtual string SetHospitalStaffUserDBPackageName
		{
            get { return "PCK_ENTITIES_HOSPITALSTAFF_GEN"; }
        }
		
        
        #endregion
        
        #region Pre e Pos processamento
        
		protected virtual void SetHospitalStaffUserDBPosQuery(DbCommand dbCommand,string mechanNum , string hospStaffType , string username  )
		{
			
        }
        
    	protected virtual void SetHospitalStaffUserDBPreQuery(DbCommand dbCommand, string mechanNum , string hospStaffType , string username  )
    	{        
		
    	}
		
		#endregion		

		#endregion

		#region Cache ENTITY
		#endregion
	
	
    
    #endregion
    
    #region OBJECTS OPERATIONS
    
    #endregion
	
    }
}

