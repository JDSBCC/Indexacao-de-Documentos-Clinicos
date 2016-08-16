// Copyright 2004, Microsoft Corporation
// Sample Code - Use restricted to terms of use defined in the accompanying license agreement (EULA.doc)

//--------------------------------------------------------------
// Autogenerated by XSDObjectGen version 1.4.4.1
// Schema file: ERIndexerDocument.xsd
// Creation Date: 13-01-2009 12:16:20
//--------------------------------------------------------------

using System;
using System.Xml.Serialization;
using System.Collections;
using System.Xml.Schema;
using System.ComponentModel;

namespace Cpchs.ER2Indexer.WCF.BusinessEntities
{
    ///// <summary>
    //    /// Serializes current Dados object into an XML document
    //    /// </summary>
    //    // <returns>string XML value</returns>
    //    public virtual string Serialize()
    //    {
    //        System.IO.StreamReader streamReader = null;
    //        System.IO.MemoryStream memoryStream = null;
    //        try
    //        {
    //            memoryStream = new System.IO.MemoryStream();
    //            Serializer.Serialize(memoryStream, this);
    //            memoryStream.Seek(0, System.IO.SeekOrigin.Begin);
    //            streamReader = new System.IO.StreamReader(memoryStream);
    //            return streamReader.ReadToEnd();
    //        }
    //        finally
    //        {
    //            if ((streamReader != null))
    //            {
    //                streamReader.Dispose();
    //            }
    //            if ((memoryStream != null))
    //            {
    //                memoryStream.Dispose();
    //            }
    //        }
    //    }

    //    /// <summary>
    //    /// Deserializes workflow markup into an Dados object
    //    /// </summary>
    //    // <param name="xml">string workflow markup to deserialize</param>
    //    // <param name="obj">Output Dados object</param>
    //    // <param name="exception">output Exception value if deserialize failed</param>
    //    // <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
    //    public static bool Deserialize(string xml, out Dados obj, out System.Exception exception)
    //    {
    //        exception = null;
    //        obj = default(Dados);
    //        try
    //        {
    //            obj = Deserialize(xml);
    //            return true;
    //        }
    //        catch (System.Exception ex)
    //        {
    //            exception = ex;
    //            return false;
    //        }
    //    }

    //    public static bool Deserialize(string xml, out Dados obj)
    //    {
    //        System.Exception exception = null;
    //        return Deserialize(xml, out obj, out exception);
    //    }

    //    public static Dados Deserialize(string xml)
    //    {
    //        System.IO.StringReader stringReader = null;
    //        try
    //        {
    //            stringReader = new System.IO.StringReader(xml);
    //            return ((Dados)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
    //        }
    //        finally
    //        {
    //            if ((stringReader != null))
    //            {
    //                stringReader.Dispose();
    //            }
    //        }
    //    }
}
