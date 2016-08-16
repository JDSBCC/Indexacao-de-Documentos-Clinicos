using Cpchs.ER2Indexer.WCF.BusinessEntities.Generated;
using Microsoft.Practices.EnterpriseLibrary.ExceptionHandling;
using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Runtime.Remoting.Messaging;

namespace Cpchs.ER2Indexer.WCF.BusinessEntities
{
	[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public sealed class ER2IndexerDocumentBER : ER2IndexerDocumentBER_GEN
    {
		#region Singleton
		private static ER2IndexerDocumentBER instance = new ER2IndexerDocumentBER();
		
		public static ER2IndexerDocumentBER Instance
		{
			get { return instance; }
		}
		#endregion

		private ER2IndexerDocumentBER()
		{
		}

        #region Methods and PackageNames

        protected override string InsertEmptyDocInfoDBMethodName
        {
            get { return "InsertEmptyDocInfo"; }
        }

        protected override string InsertEmptyDocInfoDBPackageName
        {
            get { return "PCK_ER2Indexer_V3"; }
        }

        protected override string UpdateDocInfoDBMethodName
        {
            get { return "UpdateDocInfo"; }
        }

        protected override string UpdateDocInfoDBPackageName
        {
            get { return "PCK_ER2Indexer_V3"; }
        }

        protected override string FinalizeDocInfoDBMethodName
        {
            get { return "FinalizeDocInfo"; }
        }

        protected override string FinalizeDocInfoDBPackageName
        {
            get { return "PCK_ER2Indexer_V3"; }
        }

        protected override string InsertEmptyFileStreamInfoDBMethodName
        {
            get { return "InsertEmptyFileStreamInfo"; }
        }

        protected override string InsertEmptyFileStreamInfoDBPackageName
        {
            get { return "PCK_ER2Indexer_V3"; }
        }

        protected override string UpdateFileStreamInfoDBMethodName
        {
            get { return "UpdateFileStreamInfo"; }
        }

        protected override string UpdateFileStreamInfoDBPackageName
        {
            get { return "PCK_ER2Indexer_V3"; }
        }

        protected override string UpdateFileStreamInfoThumbDBMethodName
        {
            get { return "UpdateFileStreamInfoThumb"; }
        }

        protected override string UpdateFileStreamInfoThumbDBPackageName
        {
            get { return "PCK_ER2Indexer_V3"; }
        }

        #endregion

        #region InsertEmptyDocInfo

        private object[] InsertEmptyDocInfoDBParameters()
        {
            ArrayList finalParameters = new ArrayList();

            object[] standardParameters = new object[]{ CallContext.GetData("UserName")
				 ,DBNull.Value
            		};

            finalParameters.AddRange(standardParameters);

            return finalParameters.ToArray();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        protected override void InsertEmptyDocInfoDB(string companyDB, DbTransaction transaction, DocumentInfo obj)
        {
            try
            {
                string dbMethod = InsertEmptyDocInfoDBMethod(companyDB);

                DbCommand dbCommand = dal.GetStoredProcCommand(
                            dbMethod,
                    InsertEmptyDocInfoDBParameters()
                );

                InsertEmptyDocInfoDBPreQuery(dbCommand, obj);

                if (transaction != null)
                {
                    dal.ExecuteNonQuery(dbCommand, transaction);
                }
                else
                {
                    dal.ExecuteNonQuery(dbCommand);
                }

                InsertEmptyDocInfoDBPosQuery(dbCommand, obj);

                obj.DocumentInfoXmlId = (dal is Microsoft.Practices.EnterpriseLibrary.Data.Oracle.OracleDatabase || dal is EntLibContrib.Data.OdpNet.OracleDatabase) ?
                        Convert.ToInt64(dbCommand.Parameters["p_sequence"].Value, CultureInfo.CurrentCulture) :
                        Convert.ToInt64(dbCommand.Parameters["@p_sequence"].Value, CultureInfo.CurrentCulture);
            }
            catch (Exception ex)
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

        #endregion

        #region UpdateDocInfo

        protected override object[] UpdateDocInfoDBParametersExtra(DocumentInfo obj)
        {
            return new object[] { obj.DocumentInfoPartialStream };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        protected override void UpdateDocInfoDB(string companyDB, DbTransaction transaction, DocumentInfo obj)
        {
            try
            {
                string dbMethod = UpdateDocInfoDBMethod(companyDB);
                int rowsAffected = 0;
                DbCommand dbCommand = dal.GetStoredProcCommand(
                        dbMethod,
                UpdateDocInfoDBParameters(obj)
                );

                UpdateDocInfoDBPreQuery(dbCommand, obj);

                rowsAffected = (transaction != null) ?
                    dal.ExecuteNonQuery(dbCommand, transaction) :
                    dal.ExecuteNonQuery(dbCommand);

                UpdateDocInfoDBPosQuery(dbCommand, obj);

            }
            catch (Exception ex)
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

        private object[] UpdateDocInfoDBParameters(DocumentInfo obj)
        {
            ArrayList finalParameters = new ArrayList();
            object[] standardParameters = new object[]
			{
                obj.DocumentInfoXmlId
				 ,CallContext.GetData("UserName")
            };

            finalParameters.AddRange(standardParameters);
            finalParameters.AddRange(UpdateDocInfoDBParametersExtra(obj));
            return finalParameters.ToArray();
        }

        #endregion

        #region InsertEmptyFileStreamInfo

        private object[] InsertEmptyFileStreamInfoDBParameters(FileStreamInfo obj)
        {
            ArrayList finalParameters = new ArrayList();

            object[] standardParameters = new object[]{
                obj.FileStreamInfoXmlId,
                obj.FileStreamInfoFileId,
                obj.FileStreamInfoFileName,
                DBNull.Value
            		};

            finalParameters.AddRange(standardParameters);

            return finalParameters.ToArray();
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        protected override void InsertEmptyFileStreamInfoDB(string companyDB, DbTransaction transaction, FileStreamInfo obj)
        {
            try
            {
                string dbMethod = InsertEmptyFileStreamInfoDBMethod(companyDB);

                DbCommand dbCommand = dal.GetStoredProcCommand(
                            dbMethod,
                    InsertEmptyFileStreamInfoDBParameters(obj)
                );

                InsertEmptyFileStreamInfoDBPreQuery(dbCommand, obj);

                if (transaction != null)
                {
                    dal.ExecuteNonQuery(dbCommand, transaction);
                }
                else
                {
                    dal.ExecuteNonQuery(dbCommand);
                }

                InsertEmptyFileStreamInfoDBPosQuery(dbCommand, obj);

                obj.FileStreamInfoBinId = (dal is Microsoft.Practices.EnterpriseLibrary.Data.Oracle.OracleDatabase || dal is EntLibContrib.Data.OdpNet.OracleDatabase) ?
                        Convert.ToInt64(dbCommand.Parameters["p_sequence"].Value, CultureInfo.CurrentCulture) :
                        Convert.ToInt64(dbCommand.Parameters["@p_sequence"].Value, CultureInfo.CurrentCulture);
            }
            catch (Exception ex)
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

        #endregion

        #region UpdateFileStreamInfo

        protected override object[] UpdateFileStreamInfoDBParametersExtra(FileStreamInfo obj)
        {
            return new object[] { obj.FileInfoPartialStream };
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        protected override void UpdateFileStreamInfoDB(string companyDB, DbTransaction transaction, FileStreamInfo obj)
        {
            try
            {
                string dbMethod = UpdateFileStreamInfoDBMethod(companyDB);
                int rowsAffected = 0;
                DbCommand dbCommand = dal.GetStoredProcCommand(
                        dbMethod,
                UpdateFileStreamInfoDBParameters(obj)
                );

                UpdateFileStreamInfoDBPreQuery(dbCommand, obj);

                rowsAffected = (transaction != null) ?
                    dal.ExecuteNonQuery(dbCommand, transaction) :
                    dal.ExecuteNonQuery(dbCommand);

                UpdateFileStreamInfoDBPosQuery(dbCommand, obj);

            }
            catch (Exception ex)
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

        private object[] UpdateFileStreamInfoDBParameters(FileStreamInfo obj)
        {
            ArrayList finalParameters = new ArrayList();
            object[] standardParameters = new object[]
			{
                obj.FileStreamInfoXmlId,
                obj.FileStreamInfoBinId
            };

            finalParameters.AddRange(standardParameters);
            finalParameters.AddRange(UpdateFileStreamInfoDBParametersExtra(obj));
            return finalParameters.ToArray();
        }

        #endregion

        #region UpdateFileStreamInfoThumb

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        protected override void UpdateFileStreamInfoThumbDB(string companyDB, DbTransaction transaction, FileStreamInfo obj)
        {
            try
            {
                string dbMethod = UpdateFileStreamInfoThumbDBMethod(companyDB);
                int rowsAffected = 0;
                DbCommand dbCommand = dal.GetStoredProcCommand(
                        dbMethod,
                UpdateFileStreamInfoThumbDBParameters(obj)
                );

                UpdateFileStreamInfoThumbDBPreQuery(dbCommand, obj);

                rowsAffected = (transaction != null) ?
                    dal.ExecuteNonQuery(dbCommand, transaction) :
                    dal.ExecuteNonQuery(dbCommand);

                UpdateFileStreamInfoThumbDBPosQuery(dbCommand, obj);

            }
            catch (Exception ex)
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

        private object[] UpdateFileStreamInfoThumbDBParameters(FileStreamInfo obj)
        {
            ArrayList finalParameters = new ArrayList();
            object[] standardParameters = new object[]
			{
                obj.FileStreamInfoXmlId,
                obj.FileStreamInfoBinId
            };

            finalParameters.AddRange(standardParameters);
            finalParameters.AddRange(UpdateFileStreamInfoThumbDBParametersExtra(obj));
            return finalParameters.ToArray();
        }

        protected override object[] UpdateFileStreamInfoThumbDBParametersExtra(FileStreamInfo obj)
        {
            return new object[] { obj.FileInfoPartialStream };
        }

        #endregion

        #region GetDocumentInfo

        public DocumentInfo GetDocumentInfo(
            string companyDb,
            decimal documentInfoXmlId,
            string external_ref, 
            DbTransaction transaction = null)
        {
            IDataReader reader = GetDocumentInfoDB(
                companyDb,
                documentInfoXmlId,
                external_ref, 
                transaction);
            DocumentInfo obj = null;
            if (reader.Read())
            {
                try
                {
                    obj = new DocumentInfo(
                        reader,
                        companyDb);
                }
                catch (Exception ex)
                {
                    bool rethrow = ExceptionPolicy.HandleException(ex, "Business Entities Exception Policy");
                    if (rethrow) throw;
                }
            }
            reader.Close();
            return obj;
        }

        private IDataReader GetDocumentInfoDB(
            string companyDb,
            decimal documentInfoXmlId,
            string external_ref, 
            DbTransaction transaction = null)
        {
            IDataReader ret = null;
            try
            {
                string dbMethod = GetDocumentInfoDBMethod(companyDb);
                DbCommand dbCommand = dal.GetStoredProcCommand(
                    dbMethod,
                    new object[] { documentInfoXmlId, external_ref,  DBNull.Value}
                );
                ret = (null == transaction) ? 
                    dal.ExecuteReader(dbCommand) :
                    dal.ExecuteReader(dbCommand, transaction);
            }
            catch (Exception ex)
            {
                bool rethrow = ExceptionPolicy.HandleException(ex, "Database Exception Policy");
                if (rethrow) throw;
            }
            return ret;
        }

        private string GetDocumentInfoDBMethod(
            string companyDb)
        {
            string proc = "GetDocumentInfo";
            string package = "PCK_ER2INDEXER_V3";
            dal = CPCHS.Common.Database.Database.GetDatabase("ER2Indexer", companyDb);
            proc = GetDBMethod(
                dal,
                proc,
                package);
            return proc;
        }

        #endregion GetDocumentInfo
	}
}