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
    [DataContract(Name = "AnaRes_GEN", Namespace = "http://glintt.com/types")]
    [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1705:LongAcronymsShouldBePascalCased"), System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1707:IdentifiersShouldNotContainUnderscores"), Serializable]
    public abstract class AnaRes_GEN : AbstractEntity
    {	
		#region Variables
		
		private long id = 0; ///
		private long parId = 0; ///
		private long elemId = 0; ///
		private long version = 0; ///
		private long examOrder = 0;

        ///
        private string grouper;
		
		private Codif exam = new Codif();
		private Codif prod = new Codif();
		private AnaResList childs = new AnaResList();
		private AlphanumResList alphanums = new AlphanumResList();
		private MicroResList micros = new MicroResList();
        private AttachResList attachs = new AttachResList();
		private NoteList reqNotes = new NoteList();
		private NoteList resNotes = new NoteList();
	
		#endregion
		
        #region Constructors
		
		/// <summary>
        /// Initialize an new empty AnaRes object.
        /// </summary>
        public AnaRes_GEN() 
			: base(ObjectState.Added, null) 
        {
        }
	
		/// <summary>
        /// Initialize an new empty AnaRes object.
        /// </summary>
        public AnaRes_GEN(long id) 
			: base(ObjectState.Added , null) 
        {
			this.id = id;
        }
		/// <summary>
        /// Initialize an new empty AnaRes object.
        /// </summary>
        public AnaRes_GEN(IDataReader reader, string companyDB) 
			: base(ObjectState.Unchanged, reader, companyDB) 
        {
			LoadFromReader(reader);
        }
		
        /// <summary>
        /// Initialize a new  AnaRes object with the given parameters.
        /// </summary>
        public  AnaRes_GEN(long parId, long elemId, long version, long examOrder, string grouper) 
			: base(ObjectState.Added, null) 
        {	 
			this.parId = parId;
			this.elemId = elemId;
			this.version = version;
			this.examOrder = examOrder;
            this.grouper = grouper;
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
        public long ParId
        {
            get { return this.parId; }
            set { 
				if(this.parId != value) {
					this.parId = value;
					DataStateChanged(ObjectState.Modified, "ParId");
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long ElemId
        {
            get { return this.elemId; }
            set { 
				if(this.elemId != value) {
					this.elemId = value;
					DataStateChanged(ObjectState.Modified, "ElemId");
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long Version
        {
            get { return this.version; }
            set { 
				if(this.version != value) {
					this.version = value;
					DataStateChanged(ObjectState.Modified, "Version");
				}
			}
		}
		
		
		[DataMember]
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Naming", "CA1704:IdentifiersShouldBeSpelledCorrectly")]
        public long ExamOrder
        {
            get { return this.examOrder; }
            set { 
				if(this.examOrder != value) {
					this.examOrder = value;
					DataStateChanged(ObjectState.Modified, "ExamOrder");
				}
			}
		}


        [DataMember]
        public string Grouper
        {
            get { return this.grouper; }
            set
            {
                if (this.grouper != value)
                {
                    DataStateChanged(ObjectState.Modified, "Grouper");
                    this.grouper = value;
                }
            }
        }
		
		
		[DataMember]
		public Codif Exam
		{
			get { return this.exam; }
			set { 
				if(this.exam != value) {
					DataStateChanged(ObjectState.Modified, "Exam");
            				this.exam = value;
				}
			}
		}
		
		
		[DataMember]
		public Codif Prod
		{
			get { return this.prod; }
			set { 
				if(this.prod != value) {
					DataStateChanged(ObjectState.Modified, "Prod");
            				this.prod = value;
				}
			}
		}
		
		
		[DataMember]
		public AnaResList Childs
		{
			get { return this.childs; }
			set { 
				if(this.childs != value) {
					DataStateChanged(ObjectState.Modified, "Childs");
            				this.childs = value;
				}
			}
		}
		
		
		[DataMember]
		public AlphanumResList Alphanums
		{
			get { return this.alphanums; }
			set { 
				if(this.alphanums != value) {
					DataStateChanged(ObjectState.Modified, "Alphanums");
            				this.alphanums = value;
				}
			}
		}
		
		
		[DataMember]
		public MicroResList Micros
		{
			get { return this.micros; }
			set { 
				if(this.micros != value) {
					DataStateChanged(ObjectState.Modified, "Micros");
            				this.micros = value;
				}
			}
		}

        [DataMember]
        public AttachResList Attachs
        {
            get { return this.attachs; }
            set
            {
                if (this.attachs != value)
                {
                    DataStateChanged(ObjectState.Modified, "Attachs");
                    this.attachs = value;
                }
            }
        }
		
		
		[DataMember]
		public NoteList ReqNotes
		{
			get { return this.reqNotes; }
			set { 
				if(this.reqNotes != value) {
					DataStateChanged(ObjectState.Modified, "ReqNotes");
            				this.reqNotes = value;
				}
			}
		}
		
		
		[DataMember]
		public NoteList ResNotes
		{
			get { return this.resNotes; }
			set { 
				if(this.resNotes != value) {
					DataStateChanged(ObjectState.Modified, "ResNotes");
            				this.resNotes = value;
				}
			}
		}
		
		
		#endregion
	
		#region Methods
		
		protected void LoadAnaRes_GEN(IDataReader reader, string companyDb)
        {
			base.CompanyDB = companyDb;
            base.ObjectState = CPCHS.Common.BusinessEntities.ObjectState.Unchanged;

			LoadFromReader(reader);
        }
	
		public virtual void AddChilds(AnaRes obj, bool loading)
		{
			if(!loading)
				obj.ObjectState = ObjectState.Added;
			this.childs.Add(obj);
			//if(!loading)
			//	base.Change();
		}

		public virtual void RemoveChilds(AnaRes obj)
		{
			this.childs.RemoveItem(obj);
			/*obj = this.childs[this.childs.IndexOf(obj)];
			this.childs.Remove(obj);
			if (obj.ObjectState == ObjectState.Unchanged || obj.ObjectState == ObjectState.Modified)
				this.childsToRemove.Add(obj);
			base.Change();*/
		}
		public virtual void AddAlphanums(AlphanumRes obj, bool loading)
		{
			if(!loading)
				obj.ObjectState = ObjectState.Added;
			this.alphanums.Add(obj);
			//if(!loading)
			//	base.Change();
		}

		public virtual void RemoveAlphanums(AlphanumRes obj)
		{
			this.alphanums.RemoveItem(obj);
			/*obj = this.alphanums[this.alphanums.IndexOf(obj)];
			this.alphanums.Remove(obj);
			if (obj.ObjectState == ObjectState.Unchanged || obj.ObjectState == ObjectState.Modified)
				this.alphanumsToRemove.Add(obj);
			base.Change();*/
		}
		public virtual void AddMicros(MicroRes obj, bool loading)
		{
			if(!loading)
				obj.ObjectState = ObjectState.Added;
			this.micros.Add(obj);
			//if(!loading)
			//	base.Change();
		}

		public virtual void RemoveMicros(MicroRes obj)
		{
			this.micros.RemoveItem(obj);
			/*obj = this.micros[this.micros.IndexOf(obj)];
			this.micros.Remove(obj);
			if (obj.ObjectState == ObjectState.Unchanged || obj.ObjectState == ObjectState.Modified)
				this.microsToRemove.Add(obj);
			base.Change();*/
		}

        public virtual void AddAttachs(AttachRes obj, bool loading)
        {
            if (!loading)
                obj.ObjectState = ObjectState.Added;
            this.attachs.Add(obj);
            //if(!loading)
            //	base.Change();
        }

        public virtual void RemoveAttachs(AttachRes obj)
        {
            this.attachs.RemoveItem(obj);
            /*obj = this.attachs[this.attachs.IndexOf(obj)];
            this.attachs.Remove(obj);
            if (obj.ObjectState == ObjectState.Unchanged || obj.ObjectState == ObjectState.Modified)
                this.attachs.Add(obj);
            base.Change();*/
        }

		public virtual void AddReqNotes(Note obj, bool loading)
		{
			if(!loading)
				obj.ObjectState = ObjectState.Added;
			this.reqNotes.Add(obj);
			//if(!loading)
			//	base.Change();
		}

		public virtual void RemoveReqNotes(Note obj)
		{
			this.reqNotes.RemoveItem(obj);
			/*obj = this.reqNotes[this.reqNotes.IndexOf(obj)];
			this.reqNotes.Remove(obj);
			if (obj.ObjectState == ObjectState.Unchanged || obj.ObjectState == ObjectState.Modified)
				this.reqNotesToRemove.Add(obj);
			base.Change();*/
		}
		public virtual void AddResNotes(Note obj, bool loading)
		{
			if(!loading)
				obj.ObjectState = ObjectState.Added;
			this.resNotes.Add(obj);
			//if(!loading)
			//	base.Change();
		}

		public virtual void RemoveResNotes(Note obj)
		{
			this.resNotes.RemoveItem(obj);
			/*obj = this.resNotes[this.resNotes.IndexOf(obj)];
			this.resNotes.Remove(obj);
			if (obj.ObjectState == ObjectState.Unchanged || obj.ObjectState == ObjectState.Modified)
				this.resNotesToRemove.Add(obj);
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
						case "PARID":
							if (!reader.IsDBNull(i)) this.parId = reader.GetInt64(i);
							break;
						case "ELEMID":
							if (!reader.IsDBNull(i)) this.elemId = reader.GetInt64(i);
							break;
						case "VERSION":
							if (!reader.IsDBNull(i)) this.version = reader.GetInt64(i);
							break;
						case "EXAMORDER":
							if (!reader.IsDBNull(i)) this.examOrder = reader.GetInt64(i);
							break;
                        case "EXAMGROUPER":
                            if (!reader.IsDBNull(i)) this.grouper = reader.GetString(i);
                            break;
					}
				}
            }
		}
		
		public override bool Equals(object obj)
		{
			AnaRes anares = obj as AnaRes;
			if (anares == null)
				return false;
			return anares.Id == Id;; 
		}
		
		public override int GetHashCode()
		{
			return base.GetHashCode ();
		}
		
		#endregion
    }
}


