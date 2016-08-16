using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using System.Runtime.Remoting;
using System.Web;

namespace Cpchs.Eresults.Common.WCF.Providers
{
    public sealed class ProviderManager
    {
        #region Singleton

        private static ProviderManager instance = new ProviderManager();
        public static ProviderManager Instance
        {
            get { return instance; }
        }

        #endregion

        private ProviderManager() { }

        Providers providers = null;
        public void SetProviderConfiguration(string xml_conf)
        {
            System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(Providers));
            providers = (Providers)s.Deserialize(new StringReader(xml_conf));
        }

        public object GetProvider(string providerID, HttpServerUtility server)
        {
            //System.IO.FileStream fStream = new System.IO.FileStream(providersFile, System.IO.FileMode.Open, System.IO.FileAccess.Read);
            //System.Xml.Serialization.XmlSerializer s = new System.Xml.Serialization.XmlSerializer(typeof(Providers));
            //Providers providers = (Providers)s.Deserialize(fStream);
            //fStream.Close();

            foreach (ProvidersProvider provider in providers.Provider)
            {
                if (provider.id.Equals(providerID))
                {
                    if (server != null)
                        return GetTypeThroughReflection(server.MapPath("") + "\\bin\\" + provider.assemblyPath, provider.@namespace, provider.@class);
                    else
                        return GetTypeThroughReflection(provider.assemblyPath, provider.@namespace, provider.@class);
                }
            }
            throw new Exception("The provider " + providerID + " is not configured on the configuration");
        }

        public object GetProvider(string providerID)
        {
            return GetProvider(providerID, null);
        }

        private object GetTypeThroughReflection(string sAssemblyName, string assemblyNamespace, string typeName)
        {
            try
            {
                if (File.Exists(sAssemblyName))
                {
                    Assembly assem = Assembly.LoadFrom(sAssemblyName);
                }
                else
                {
                    if (File.Exists(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\" + sAssemblyName))
                    {
                        Assembly assem = Assembly.LoadFrom(AppDomain.CurrentDomain.BaseDirectory + "\\bin\\" + sAssemblyName);
                    }
                }
            }
            catch { }

            try
            {
                ObjectHandle obj = Activator.CreateInstance(assemblyNamespace, assemblyNamespace + "." + typeName);
                object absOp = obj.Unwrap() as object;
                return absOp;
            }
            catch (Exception)
            {
                return null;
            }
        }

    }
}
