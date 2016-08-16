using System;
using System.Data;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Globalization;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using CPCHS.Common.BusinessEntities;
using System.Runtime.Serialization;
using Cpchs.Eresults.Common.WCF;

namespace Cpchs.Eresults.Common.WCF.BusinessEntities.Generated
{
    /// <summary>
    /// Date Created: ter?a-feira, 10 de Julho de 2012
    /// Created By: Generated by CodeSmith
	/// Template Created By: CPCHS psilva, 2005
    /// </summary>
    [DataContract(Name = "AttachRes_GEN", Namespace = "http://glintt.com/types")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public abstract class AttachRes_GEN : AbstractEntity
    {	
		#region Variables
		
		private long id = 0; ///
		private long anaResId = 0; ///
		private string name = string.Empty; ///
		private long resOrder = 0; ///
        private string descr = string.Empty; ///
		
		private NoteList notes = new NoteList();
	
		#endregion
		
        #region Constructors
		
		/// <summary>
        /// Initialize an new empty AttachRes object.
        /// </summary>
        public AttachRes_GEN() 
			: base(ObjectState.Added, null) 
        {
        }
	
		/// <summary>
        /// Initialize an new empty AttachRes object.
        /// </summary>
        public AttachRes_GEN(long id) 
			: base(ObjectState.Added , null) 
        {
			this.id = id;
        }
		/// <summary>
        /// Initialize an new empty AttachRes object.
        /// </summary>
        public AttachRes_GEN(IDataReader reader, string companyDB) 
			: base(ObjectState.Unchanged, reader, companyDB) 
        {
			LoadFromReader(reader);
        }
		
        /// <summary>
        /// Initialize a new  AttachRes object with the given parameters.
        /// </summary>
        public AttachRes_GEN(long anaResId, string name, long resOrder, string descr) 
			: base(ObjectState.Added, null) 
        {	 
			this.anaResId = anaResId;
			this.name = name;
			this.resOrder = resOrder;
            this.descr = descr;
        }
		#endregion
		
		#region Properties
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long Id
        {
            get { return this.id; }
            set { 
				if(this.id != value) {
					this.id = value;
					DataStateChanged(ObjectState.Modified, "Id");
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long AnaResId
        {
            get { return this.anaResId; }
            set { 
				if(this.anaResId != value) {
					this.anaResId = value;
					DataStateChanged(ObjectState.Modified, "AnaResId");
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string Name
        {
            get { return this.name; }
            set { 
				if(this.name != value) {
					this.name = value;
					DataStateChanged(ObjectState.Modified, "Name");
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long ResOrder
        {
            get { return this.resOrder; }
            set { 
				if(this.resOrder != value) {
					this.resOrder = value;
					DataStateChanged(ObjectState.Modified, "ResOrder");
				}
			}
		}

        [DataMember]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public string Descr
        {
            get { return this.descr; }
            set
            {
                if (this.descr != value)
                {
                    this.descr = value;
                    DataStateChanged(ObjectState.Modified, "Descr");
                }
            }
        }
		
		
		
		[DataMember]
		public NoteList Notes
		{
			get { return this.notes; }
			set { 
				if(this.notes != value) {
					DataStateChanged(ObjectState.Modified, "Notes");
            				this.notes = value;
				}
			}
		}
		
		
		#endregion
	
		#region Methods
		
		protected void LoadAttachRes_GEN(IDataReader reader, string companyDb)
        {
			base.CompanyDB = companyDb;
            base.ObjectState = CPCHS.Common.BusinessEntities.ObjectState.Unchanged;

			LoadFromReader(reader);
        }
	
		public virtual void AddNotes(Note obj, bool loading)
		{
			if(!loading)
				obj.ObjectState = ObjectState.Added;
			this.notes.Add(obj);
			//if(!loading)
			//	base.Change();
		}

		public virtual void RemoveNotes(Note obj)
		{
			this.notes.RemoveItem(obj);
			/*obj = this.notes[this.notes.IndexOf(obj)];
			this.notes.Remove(obj);
			if (obj.ObjectState == ObjectState.Unchanged || obj.ObjectState == ObjectState.Modified)
				this.notesToRemove.Add(obj);
			base.Change();*/
		}
		private void LoadFromReader(IDataReader reader)
		{
			if (reader != null && !reader.IsClosed)
            {	
				for(int i=0; i<reader.FieldCount; i++) {
					switch(reader.GetName(i).ToUpper(System.Globalization.CultureInfo.CurrentCulture)) {
						case "ID":
							if (!reader.IsDBNull(i)) this.id = reader.GetInt64(i);
							break;
						case "ANARESID":
							if (!reader.IsDBNull(i)) this.anaResId = reader.GetInt64(i);
							break;
						case "NAME":
							if (!reader.IsDBNull(i)) this.name = Convert.ToString(reader.GetValue(i));
							break;
						case "RESORDER":
							if (!reader.IsDBNull(i)) this.resOrder = reader.GetInt64(i);
							break;
                        case "DESCR":
                            if (!reader.IsDBNull(i)) this.descr = Convert.ToString(reader.GetValue(i));
                            break;
					}
				}
            }
		}
		
		public override bool Equals(object obj)
		{
			AttachRes attachres = obj as AttachRes;
			if (attachres == null)
				return false;
			return attachres.Id == Id;; 
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}
		
		#endregion
    }
}

