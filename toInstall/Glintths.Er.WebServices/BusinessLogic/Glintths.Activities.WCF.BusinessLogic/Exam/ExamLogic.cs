using Cpchs.Eresults.Common.WCF.BusinessEntities;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace Cpchs.Activities.WCF.BusinessLogic
{
    public class ExamLogic
    {
        public static AnaResList GetAnaResByDocId(string companyDb, long docId, string username, string sessionId)
        {
            AnaResList results = AnaResMgrBER.Instance.GetAnaResByDocId(companyDb, docId, username, sessionId);
            AlphanumResList alphanums = AnaResMgrBER.Instance.GetAlphanumResByDocId(companyDb, docId);
            MicroResList micros = AnaResMgrBER.Instance.GetMicroResByDocId(companyDb, docId);
            AntibResList antibs = AnaResMgrBER.Instance.GetAntibResByDocId(companyDb, docId);
            AttachResList attachs = AnaResMgrBER.Instance.GetAttachResByDocId(companyDb, docId);
            NoteList notes = AnaResMgrBER.Instance.GetNotesByDocId(companyDb, docId);
            AnaResList res = BuildHierarchy(results, alphanums, micros, antibs, attachs, notes);
            return MergeAnaRes(res);
        }

        public static AnaResList GetAnaResByMultiCrit(string companyDb, string entIds, long? epiTypeId, string epi, string doc, string extId, DateTime? epiBeginDate, DateTime? epiEndDate, long? docTypeId, long? appId, long? placeId, long? instId, DateTime? docBeginDate, DateTime? docEndDate, DateTime? valBeginDate, DateTime? valEndDate, string globalFilters, string docsSession, string servsSession, string username, string sessionId)
        {
            string dsf = (docsSession == null || docsSession == "N") ? "S" : "N";
            string ssf = (servsSession == null || servsSession == "N") ? "S" : "N";
            AnaResList results = AnaResMgrBER.Instance.GetAnaResByMultiCrit(companyDb, entIds, epiTypeId, epi, doc, extId, epiBeginDate, epiEndDate, docTypeId, appId, placeId, instId, docBeginDate, docEndDate, valBeginDate, valEndDate, globalFilters, dsf, ssf, username, sessionId);
            AlphanumResList alphanums = AnaResMgrBER.Instance.GetAlphanumResByMultiCrit(companyDb, entIds, epiTypeId, epi, doc, extId, epiBeginDate, epiEndDate, docTypeId, appId, placeId, instId, docBeginDate, docEndDate, valBeginDate, valEndDate, globalFilters, dsf, ssf, username);
            MicroResList micros = AnaResMgrBER.Instance.GetMicroResByMultiCrit(companyDb, entIds, epiTypeId, epi, doc, extId, epiBeginDate, epiEndDate, docTypeId, appId, placeId, instId, docBeginDate, docEndDate, valBeginDate, valEndDate, globalFilters, dsf, ssf, username);
            AntibResList antibs = AnaResMgrBER.Instance.GetAntibResByMultiCrit(companyDb, entIds, epiTypeId, epi, doc, extId, epiBeginDate, epiEndDate, docTypeId, appId, placeId, instId, docBeginDate, docEndDate, valBeginDate, valEndDate, globalFilters, dsf, ssf, username);
            AttachResList attachs = AnaResMgrBER.Instance.GetAttachResByMultiCrit(companyDb, entIds, epiTypeId, epi, doc, extId, epiBeginDate, epiEndDate, docTypeId, appId, placeId, instId, docBeginDate, docEndDate, valBeginDate, valEndDate, globalFilters, dsf, ssf, username);
            NoteList notes = AnaResMgrBER.Instance.GetNotesByMultiCrit(companyDb, entIds, epiTypeId, epi, doc, extId, epiBeginDate, epiEndDate, docTypeId, appId, placeId, instId, docBeginDate, docEndDate, valBeginDate, valEndDate, globalFilters, dsf, ssf, username);
            AnaResList res = BuildHierarchy(results, alphanums, micros, antibs, attachs, notes);
            return MergeAnaRes(res);
        }

        private static AnaResList BuildHierarchy(AnaResList results, AlphanumResList alphanums, MicroResList micros, AntibResList antibs, AttachResList attachs, NoteList notes)
        {
            AnaResList roots = new AnaResList();
            AnaResList childs = new AnaResList();
            foreach (AnaRes res in results.Items.Where(res => res.ParId == 0))
            {
                roots.Items.Add(res);
            }
            foreach (AnaRes res in results.Items.Where(res => res.ParId != 0))
            {
                childs.Items.Add(res);
            }
            AnaResList tree = new AnaResList();
            foreach (AnaRes root in roots.Items)
            {
                if (!string.IsNullOrEmpty(root.Grouper))
                {
                    AnaRes grouper = tree.Items.FirstOrDefault(anaRes => anaRes.Exam.Code == root.Grouper);
                    if (grouper == null)
                    {
                        grouper = BuilGrouperFromChild(root);
                    }
                    grouper.Childs.Items.Add(root);
                    tree.Items.Add(grouper);
                }
                else
                {
                    tree.Items.Add(root);
                }
            }
            InsertChilds(tree, childs, notes);
            InsertAlphanums(tree, alphanums, notes);
            InsertMicros(tree, micros, antibs, notes);
            InsertAttachs(tree, attachs, notes);
            return tree;
        }

        private static void InsertChilds(AnaResList parents, AnaResList childs, NoteList notes)
        {
            foreach (AnaRes parent in parents.Items)
            {
                foreach (AnaRes child in childs.Items.Where(child => child.ParId == parent.Id))
                {
                    if (!string.IsNullOrEmpty(child.Grouper))
                    {
                        AnaRes grouper = parent.Childs.Items.FirstOrDefault(anaRes => anaRes.Exam.Code == child.Grouper);
                        AnaRes tousegrouper = grouper;
                        if (grouper == null)
                        {
                            tousegrouper = BuilGrouperFromChild(child);
                        }

                        tousegrouper.Childs.Items.Add(child);
                        if (grouper == null)
                            parent.Childs.Items.Add(tousegrouper);
                    }
                    else
                    {
                        parent.Childs.Items.Add(child);
                        foreach (
                            Note note in
                                notes.Items.Where(
                                    note =>
                                    note.DomName.ToUpper() == "DOCUMENTO" &&
                                    parent.DocId.ToString(CultureInfo.InvariantCulture) == note.DomId))
                        {
                            parent.ReqNotes.Items.Add(note);
                        }
                        foreach (
                            Note note in
                                notes.Items.Where(
                                    note =>
                                    note.DomName.ToUpper() == "ELEMENTO" &&
                                    parent.ElemId.ToString(CultureInfo.InvariantCulture) == note.DomId &&
                                    note.SubdomName.ToUpper() == "VERSAO" &&
                                    parent.Version.ToString(CultureInfo.InvariantCulture) == note.SubdomId))
                        {
                            parent.ResNotes.Items.Add(note);
                        }
                    }
                }
                InsertChilds(parent.Childs, childs, notes);
            }
        }

        private static AnaRes BuilGrouperFromChild(AnaRes child)
        {
            AnaRes grouper = new AnaRes
                             {
                                 AppId = child.AppId,
                                 CompanyDB = child.CompanyDB,
                                 DocId = child.DocId,
                                 DocTypeId = child.DocTypeId,
                                 ElemId = child.ElemId,
                                 EmiDate = child.EmiDate,
                                 EpiBeginDate = child.EpiBeginDate,
                                 EpiEndDate = child.EpiEndDate,
                                 EpiId = child.EpiId,
                                 EpiType = child.EpiType,
                                 EspExec = child.EspExec,
                                 EspReq = child.EspReq,
                                 Exam = new Codif(child.Grouper, child.Grouper),
                                 ExamOrder = child.ExamOrder,
                                 ExecDate = child.ExecDate,
                                 ExtId = child.ExtId,
                                 ParId = child.ParId,
                                 Prod = child.Prod,
                                 ReqId = child.ReqId,
                                 SerExec = child.SerExec,
                                 SerReq = child.SerReq,
                                 ValDate = child.ValDate,
                                 Version = child.Version
                             };
            return grouper;
        }

        private static void InsertAlphanums(AnaResList results, AlphanumResList alphanums, NoteList notes)
        {
            foreach (AnaRes res in results.Items)
            {
                foreach (AlphanumRes alphanum in alphanums.Items.Where(alphanum => alphanum.AnaResId == res.Id))
                {
                    alphanum.ValDate = res.ValDate;
                    alphanum.ResDate = res.ValDate;
                    alphanum.Product = res.Prod.Descr;
                    alphanum.ReqDate = res.ExecDate;
                    alphanum.ReqId = res.ReqId;
                    alphanum.DocDate = res.ExecDate;
                    alphanum.EpiBeginDate = res.EpiBeginDate;
                    alphanum.EpiEndDate = res.EpiEndDate;
                    alphanum.EpiId = res.EpiId;
                    alphanum.EpiType = res.EpiType;
                    alphanum.EspExec = res.EspExec;
                    alphanum.EspReq = res.EspReq;
                    alphanum.ExtId = res.ExtId;
                    alphanum.SerExec = res.SerExec;
                    alphanum.SerReq = res.SerReq;
                    alphanum.DocId = res.DocId;
                    alphanum.ReqNotes = res.ReqNotes;
                    alphanum.AppId = res.AppId;
                    alphanum.DocTypeId = res.DocTypeId;
                    alphanum.ElemId = res.ElemId;
                    alphanum.VerCod = res.Version;
                    res.Alphanums.Items.Add(alphanum);
                    foreach (Note note in notes.Items.Where(note => note.DomName.ToUpper() == "RESULTADO" && alphanum.AnaResId.ToString(CultureInfo.InvariantCulture) == note.DomId))
                    {
                        alphanum.Notes.Items.Add(note);
                    }
                }
                InsertAlphanums(res.Childs, alphanums, notes);
            }
        }

        private static void InsertMicros(AnaResList results, MicroResList micros, AntibResList antibs, NoteList notes)
        {
            foreach (AnaRes res in results.Items)
            {
                foreach (MicroRes micro in micros.Items.Where(micro => micro.AnaResId == res.Id))
                {
                    micro.ValDate = res.ValDate;
                    micro.ResDate = res.ValDate;
                    micro.Product = res.Prod.Descr;
                    micro.ReqDate = res.ExecDate;
                    micro.ReqId = res.ReqId;
                    micro.DocDate = res.ExecDate;
                    micro.EpiBeginDate = res.EpiBeginDate;
                    micro.EpiEndDate = res.EpiEndDate;
                    micro.EpiId = res.EpiId;
                    micro.EpiType = res.EpiType;
                    micro.EspExec = res.EspExec;
                    micro.EspReq = res.EspReq;
                    micro.ExtId = res.ExtId;
                    micro.SerExec = res.SerExec;
                    micro.SerReq = res.SerReq;
                    micro.DocId = res.DocId;
                    micro.ReqNotes = res.ReqNotes;
                    micro.AppId = res.AppId;
                    micro.DocTypeId = res.DocTypeId;
                    micro.ElemId = res.ElemId;
                    micro.VerCod = res.Version;
                    res.Micros.Items.Add(micro);
                    foreach (AntibRes antib in antibs.Items.Where(antib => antib.MicroResId == micro.Id))
                    {
                        micro.Antibs.Items.Add(antib);
                    }
                    foreach (Note note in notes.Items.Where(note => note.DomName.ToUpper() == "RESULTADO" && micro.AnaResId.ToString(CultureInfo.InvariantCulture) == note.DomId))
                    {
                        micro.Notes.Items.Add(note);
                    }
                }
                InsertMicros(res.Childs, micros, antibs, notes);
            }
        }

        private static void InsertAttachs(AnaResList results, AttachResList attachs, NoteList notes)
        {
            foreach (AnaRes res in results.Items)
            {
                foreach (AttachRes attach in attachs.Items.Where(attach => attach.AnaResId == res.Id))
                {
                    attach.ValDate = res.ValDate;
                    attach.ResDate = res.ValDate;
                    attach.Product = res.Prod.Descr;
                    attach.ReqDate = res.ExecDate;
                    attach.ReqId = res.ReqId;
                    attach.DocDate = res.ExecDate;
                    attach.EpiBeginDate = res.EpiBeginDate;
                    attach.EpiEndDate = res.EpiEndDate;
                    attach.EpiId = res.EpiId;
                    attach.EpiType = res.EpiType;
                    attach.EspExec = res.EspExec;
                    attach.EspReq = res.EspReq;
                    attach.ExtId = res.ExtId;
                    attach.SerExec = res.SerExec;
                    attach.SerReq = res.SerReq;
                    attach.DocId = res.DocId;
                    attach.ReqNotes = res.ReqNotes;
                    attach.AppId = res.AppId;
                    attach.DocTypeId = res.DocTypeId;
                    attach.ElemId = res.ElemId;
                    attach.VerCod = res.Version;
                    res.Attachs.Items.Add(attach);
                    foreach (Note note in notes.Items.Where(note => note.DomName.ToUpper() == "RESULTADO" && attach.AnaResId.ToString(CultureInfo.InvariantCulture) == note.DomId))
                    {
                        attach.Notes.Items.Add(note);
                    }
                }
                InsertAttachs(res.Childs, attachs, notes);
            }
        }

        private static AnaResList MergeAnaRes(AnaResList results)
        {
            AnaResList temp = new AnaResList();
            foreach (AnaRes res in results.Items.OrderBy(f => f.ExamOrder).Where(res => !FindAnaRes(temp, res)))
            {
                temp.Items.Add(res);
            }
            foreach (AnaRes res in temp.Items)
            {
                res.Childs = MergeAnaRes(res.Childs);
            }
            if (temp.Items != null && temp.Items.Count != 0)
                return new AnaResList() { Items = new System.ComponentModel.BindingList<AnaRes>(temp.Items.OrderBy(f => f.ExamOrder).ToList()) };
            else
                return temp;
        }

        private static bool FindAnaRes(AnaResList results, AnaRes anaRes)
        {
            for (int i = 0; i < results.Items.Count; i++)
            {
                if (results.Items[i].Exam.Code != anaRes.Exam.Code)
                {
                    continue;
                }
                XmlSerializer sr = new XmlSerializer(typeof(AnaRes));
                MemoryStream st = new MemoryStream();
                sr.Serialize(st, anaRes);
                st.Position = 0;
                AnaRes clonedAnaRes = sr.Deserialize(st) as AnaRes;
                st.Close();
                st.Dispose();
                if (clonedAnaRes != null)
                {
                    foreach (AnaRes child in clonedAnaRes.Childs.Items)
                    {
                        results.Items[i].Childs.Items.Add(child);
                    }
                    foreach (Note note in clonedAnaRes.ResNotes.Items)
                    {
                        results.Items[i].ResNotes.Items.Add(note);
                    }
                    foreach (AlphanumRes alphanum in clonedAnaRes.Alphanums.Items)
                    {
                        results.Items[i].Alphanums.Items.Add(alphanum);
                    }
                    foreach (MicroRes micro in clonedAnaRes.Micros.Items)
                    {
                        results.Items[i].Micros.Items.Add(micro);
                    }
                    foreach (AttachRes attach in clonedAnaRes.Attachs.Items)
                    {
                        results.Items[i].Attachs.Items.Add(attach);
                    }
                }
                return true;
            }
            return false;
        }
    }
}