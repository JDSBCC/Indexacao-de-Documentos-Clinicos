
using System;
using System.Data;
using System.Data.Common;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Runtime.Remoting.Messaging;
using Cpchs.Eresults.Common.WCF.BusinessEntities;
using CPCHS.Common.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;
using Microsoft.Practices.EnterpriseLibrary.Caching;
using Microsoft.Practices.EnterpriseLibrary.Caching.Expirations;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;

namespace Cpchs.Eresults.Common.WCF.BusinessEntities.Generated
{
    /// <summary>
    /// Date Created: segunda-feira, 18 de Outubro de 2010
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public abstract class VideoManagementBER_GEN : CommonBER
    {
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2235:MarkAllNonSerializableFields"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1051:DoNotDeclareVisibleInstanceFields")]
		protected Microsoft.Practices.EnterpriseLibrary.Data.Database dal;	
		
		#region Variables
		#endregion
	
		protected VideoManagementBER_GEN()
		{
			/*try
			{
				dal=DatabaseFactory.CreateDatabase("VideoWCF");
			}
			catch 
			{	
				dal = DatabaseFactory.CreateDatabase();
			}*/
	
		}
	
    #region TABLES OPERATIONS
	
		#region Cache VIDEO
		#endregion
	
	
		#region Select VIDEO Operations
		//
		//SELECT OPERATIONS VIDEO
		//
		//
		//
		//
		public virtual VideoList GetVideosByDocumentId(string companyDB, long document_Id )
		{
			IDataReader reader = GetVideosByDocumentIdDB(companyDB, document_Id );
			VideoList list = new VideoList();
			while(reader.Read())
			{
				try
    			{
					list.Add(new Video(reader, companyDB));
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
		protected virtual string GetVideosByDocumentIdDBMethod(string companyDB)
		{
			string proc = GetVideosByDocumentIdDBMethodName;
            string package = GetVideosByDocumentIdDBPackageName;
			
			dal = CPCHS.Common.Database.Database.GetDatabase("VideoWCF", companyDB);
            
            proc = GetDBMethod(dal, proc, package);
			
			return proc;
		}
		
		protected virtual string GetVideosByDocumentIdDBMethodName
		{
            get { return "GetVideosByDocumentId"; }
        }
		
        protected virtual string GetVideosByDocumentIdDBPackageName
		{
            get { return "PCK_DOCUMENTS_VIDEO_GEN"; }
        }

		
		protected virtual IDataReader GetVideosByDocumentIdDB(string companyDB, long document_Id )
		{
			IDataReader ret = null;
			try
      		{
				string dbMethod = GetVideosByDocumentIdDBMethod(companyDB);
                DbCommand dbCommand;
                
                dbCommand = GetStoredProcCommand(dal, dbMethod, document_Id , DBNull.Value);
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
		
		#region Insert VIDEO Operations
		//
		//INSERT OPERATIONS VIDEO
		//
		//
		//
		//
		public virtual Video AddVideo(string companyDB, Video obj,params DbTransaction[] transaction)
		{		
			try
     		{
     			if (transaction.Length==0)
					AddVideoDB(companyDB, null, obj);
				else
					AddVideoDB(companyDB, transaction[0], obj);
				
				obj.ObjectState = ObjectState.Unchanged;
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
			
			return obj;
		}
		
		
		//
		// DB
		//
		protected virtual string AddVideoDBMethod(string companyDB)
		{
			string proc = AddVideoDBMethodName;
            string package = AddVideoDBPackageName;
            
			dal = CPCHS.Common.Database.Database.GetDatabase("VideoWCF", companyDB);
			
            proc = GetDBMethod(dal, proc, package);
			
			return proc;
		}

		protected virtual string AddVideoDBMethodName
		{
           get { return "AddVideo"; }
        }
        
        protected virtual string AddVideoDBPackageName
		{
            get { return "PCK_DOCUMENTS_VIDEO_GEN"; }
        }

		protected virtual object[] AddVideoDBParametersExtra(Video obj)
		{
            return new object[] {};
        }
		protected object[] AddVideoDBParameters(Video obj)
    	{
        	ArrayList finalParameters=new ArrayList();

			object[] standardParameters = new object[]{
					obj.VideoElemId
                  ,	obj.VideoVersionCode
                  ,	obj.VideoDuration
                  ,	obj.VideoDesc
				  , CallContext.GetData("UserName")
					};

        	finalParameters.AddRange(standardParameters);
        	finalParameters.AddRange(AddVideoDBParametersExtra(obj));

        	return finalParameters.ToArray();
    	}

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
		protected virtual void AddVideoDB(string companyDB, DbTransaction transaction, Video obj)
		{
			try
      		{
				string dbMethod = AddVideoDBMethod(companyDB);
				
				DbCommand dbCommand = dal.GetStoredProcCommand(
                			dbMethod,
					AddVideoDBParameters(obj)
				);

				AddVideoDBPreQuery(dbCommand, obj);
	
				if (transaction != null)
				{
					dal.ExecuteNonQuery(dbCommand, transaction);
				}
				else
				{
					dal.ExecuteNonQuery(dbCommand);
				}

				AddVideoDBPosQuery(dbCommand, obj);

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
		protected virtual void AddVideoDBPosQuery(DbCommand dbCommand, Video obj)
		{
			
        }
    	protected virtual void AddVideoDBPreQuery(DbCommand dbCommand, Video obj)
    	{        
		
    	}

		
		#endregion
		
		#region Cache VIDEOLINK
		#endregion
	
	
		#region Select VIDEOLINK Operations
		//
		//SELECT OPERATIONS VIDEOLINK
		//
		//
		//
		//
		public virtual VideoLinkList GetVideoLinksByDocumentId(string companyDB, long document_Id )
		{
			IDataReader reader = GetVideoLinksByDocumentIdDB(companyDB, document_Id );
			VideoLinkList list = new VideoLinkList();
			while(reader.Read())
			{
				try
    			{
					list.Add(new VideoLink(reader, companyDB));
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
		protected virtual string GetVideoLinksByDocumentIdDBMethod(string companyDB)
		{
			string proc = GetVideoLinksByDocumentIdDBMethodName;
            string package = GetVideoLinksByDocumentIdDBPackageName;
			
			dal = CPCHS.Common.Database.Database.GetDatabase("VideoWCF", companyDB);
            
            proc = GetDBMethod(dal, proc, package);
			
			return proc;
		}
		
		protected virtual string GetVideoLinksByDocumentIdDBMethodName
		{
            get { return "GetVideoLinksByDocumentId"; }
        }
		
        protected virtual string GetVideoLinksByDocumentIdDBPackageName
		{
            get { return "PCK_DOCUMENTS_VIDEO_GEN"; }
        }

		
		protected virtual IDataReader GetVideoLinksByDocumentIdDB(string companyDB, long document_Id )
		{
			IDataReader ret = null;
			try
      		{
				string dbMethod = GetVideoLinksByDocumentIdDBMethod(companyDB);
                DbCommand dbCommand;
                
                dbCommand = GetStoredProcCommand(dal, dbMethod, document_Id , DBNull.Value);
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
		
    
    #endregion
    
    #region OBJECTS OPERATIONS
    
    #endregion
	
    }
}

