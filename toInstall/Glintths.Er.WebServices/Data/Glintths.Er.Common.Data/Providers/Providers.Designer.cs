using System.Collections.Generic;
using System.ComponentModel;

namespace Cpchs.Eresults.Common.WCF.Providers
{
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/Providers")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace = "http://tempuri.org/Providers", IsNullable = false)]
    public partial class Providers : System.ComponentModel.INotifyPropertyChanged
    {
        [EditorBrowsable(EditorBrowsableState.Never)]
        private List<ProvidersProvider> providerField;

        public Providers()
        {
            if ((this.providerField == null))
            {
                this.providerField = new List<ProvidersProvider>();
            }
        }

        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Provider")]
        public List<ProvidersProvider> Provider
        {
            get
            {
                return this.providerField;
            }
            set
            {
                if ((this.providerField != null))
                {
                    if ((providerField.Equals(value) != true))
                    {
                        this.providerField = value;
                        this.OnPropertyChanged("Provider");
                    }
                }
                else
                {
                    this.providerField = value;
                    this.OnPropertyChanged("Provider");
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
    }

    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Xml", "2.0.50727.3053")]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true, Namespace = "http://tempuri.org/Providers")]
    public partial class ProvidersProvider : System.ComponentModel.INotifyPropertyChanged
    {

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string idField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string assemblyPathField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string namespaceField;

        [EditorBrowsable(EditorBrowsableState.Never)]
        private string classField;

        /// <remarks/>
        public string id
        {
            get
            {
                return this.idField;
            }
            set
            {
                if ((this.idField != null))
                {
                    if ((idField.Equals(value) != true))
                    {
                        this.idField = value;
                        this.OnPropertyChanged("id");
                    }
                }
                else
                {
                    this.idField = value;
                    this.OnPropertyChanged("id");
                }
            }
        }

        /// <remarks/>
        public string assemblyPath
        {
            get
            {
                return this.assemblyPathField;
            }
            set
            {
                if ((this.assemblyPathField != null))
                {
                    if ((assemblyPathField.Equals(value) != true))
                    {
                        this.assemblyPathField = value;
                        this.OnPropertyChanged("assemblyPath");
                    }
                }
                else
                {
                    this.assemblyPathField = value;
                    this.OnPropertyChanged("assemblyPath");
                }
            }
        }

        /// <remarks/>
        public string @namespace
        {
            get
            {
                return this.namespaceField;
            }
            set
            {
                if ((this.namespaceField != null))
                {
                    if ((namespaceField.Equals(value) != true))
                    {
                        this.namespaceField = value;
                        this.OnPropertyChanged("namespace");
                    }
                }
                else
                {
                    this.namespaceField = value;
                    this.OnPropertyChanged("namespace");
                }
            }
        }

        /// <remarks/>
        public string @class
        {
            get
            {
                return this.classField;
            }
            set
            {
                if ((this.classField != null))
                {
                    if ((classField.Equals(value) != true))
                    {
                        this.classField = value;
                        this.OnPropertyChanged("class");
                    }
                }
                else
                {
                    this.classField = value;
                    this.OnPropertyChanged("class");
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string info)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(info));
            }
        }
    }
}
