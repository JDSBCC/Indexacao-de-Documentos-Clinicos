using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using Glintths.Authorization.Providers.BusinessEntities;
using Glintths.Authorization.Providers.Proxies;
using Glintths.Er.Common.BusinessEntities;
using Microsoft.Practices.EnterpriseLibrary.Data;

namespace Glintths.Er.Workspaces.BusinessLogic
{
    public class WorkspacesLogic
    {
        //public static Workspace CreateWorkspace(string companyDB, string name, string description, WorkspaceMember ownerUser)
        //{
        //    Database dal = CPCHS.Common.Database.Database.GetDatabase("ErWorkspaces", companyDB);
        //    using (DbConnection connection = dal.CreateConnection())
        //    {
        //        connection.Open();
        //        using (DbTransaction transaction = connection.BeginTransaction())
        //        {
        //            try
        //            {
        //                Workspace ws = new Workspace()
        //                {
        //                    WorkspaceName = name,
        //                    WorkspaceDesc = description
        //                };
        //                Workspace newWs = WorkspaceManagementBER.Instance.CreateWorkspace(companyDB, ws, transaction);

        //                CreateMemberPermissions(companyDB, ownerUser, newWs.WorkspaceId);

        //                //newWs.WorkspaceMembers = new WorkspaceMemberList();
        //                //newWs.WorkspaceMembers.Add(ownerUser);

        //                transaction.Commit();
        //                return newWs;
        //            }
        //            catch (Exception ex)
        //            {
        //                transaction.Rollback();
        //                throw ex;
        //            }
        //            finally
        //            {
        //                connection.Close();
        //            }
        //        }
        //    }
        //}

        public static Workspace CreateWorkspace(
            string companyDB,
            string name,
            string description,
            WorkspaceMember ownerUser,
            bool? active,
            long? parentId,
            WorkspaceMemberList members,
            bool? inheritMembers)
        {
            Database dal = CPCHS.Common.Database.Database.GetDatabase("ErWorkspaces", companyDB);
            using (DbConnection connection = dal.CreateConnection())
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        if (ownerUser == null)
                            throw new Exception("The owner user cannot be null");

                        Workspace ws = new Workspace()
                        {
                            WorkspaceName = name,
                            WorkspaceDesc = description,
                            WorkspaceActive = active.HasValue ? active.Value : false,
                            WorkspaceParentId = parentId
                        };
                        Workspace newWs = WorkspaceManagementBER.Instance.CreateWorkspace(companyDB, ws, transaction);

                        WorkspaceMemberList list;
                        if (members != null)
                        {
                            members.Add(ownerUser);
                            list = members;
                        }
                        else
                        {
                            list = new WorkspaceMemberList();
                            list.Add(ownerUser);
                        }

                        CreateMemberPermissions(companyDB, list, newWs.WorkspaceId);

                        newWs.WorkspaceMembers = list;

                        transaction.Commit();
                        return newWs;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        public static bool ActivateWorkspace(string companyDB, long workspaceId)
        {
            Workspace wsBe = new Workspace()
            {
                WorkspaceId = workspaceId,
                WorkspaceActive = true
            };
            WorkspaceManagementBER.Instance.UpdateWorkspaceActiveState(companyDB, wsBe);
            return true;
        }

        public static bool DeactivateWorkspace(string companyDB, long workspaceId)
        {
            Workspace wsBe = new Workspace()
            {
                WorkspaceId = workspaceId,
                WorkspaceActive = false
            };
            WorkspaceManagementBER.Instance.UpdateWorkspaceActiveState(companyDB, wsBe);
            return true;
        }

        public static bool AddMembersToWorkspace(string companyDB, long workspaceId, WorkspaceMemberList members)
        {
            CreateMemberPermissions(companyDB, members, workspaceId);
            return true;

            //foreach (var item in members.Items)
            //{
            //item.WorkspaceId = workspaceId;
            //WorkspaceManagementBER.Instance.AddMemberToWorkspace(companyDB, item, transaction);
            //}
            ////transaction.Commit();
            //return true;
            //        }
            //        catch (Exception ex)
            //        {
            //            transaction.Rollback();
            //            throw ex;
            //        }
            //        finally
            //        {
            //            connection.Close();
            //        }
            //    }
            //}
        }

        public static bool RemoveMembersFromWorkspace(string companyDB, long workspaceId, WorkspaceMemberList members)
        {
            throw new NotImplementedException();
            //Database dal = CPCHS.Common.Database.Database.GetDatabase("ErWorkspaces", companyDB);
            //using (DbConnection connection = dal.CreateConnection())
            //{
            //    connection.Open();
            //    using (DbTransaction transaction = connection.BeginTransaction())
            //    {
            //        try
            //        {
            //            foreach (var item in members.Items)
            //            {
            //                item.WorkspaceId = workspaceId;
            //                WorkspaceManagementBER.Instance.RemoveWorkspaceMember(companyDB, item, transaction);
            //            }
            //            transaction.Commit();
            //            return true;
            //        }
            //        catch (Exception ex)
            //        {
            //            transaction.Rollback();
            //            throw ex;
            //        }
            //        finally
            //        {
            //            connection.Close();
            //        }
            //    }
            //}
        }

        public static Workspace GetWorkspace(string companyDB, long workspaceId)
        {
            Workspace ws = WorkspaceManagementBER.Instance.GetWorkspace(companyDB, workspaceId);
            //ws.WorkspaceMembers = WorkspaceManagementBER.Instance.GetWorkspaceMembers(companyDB, workspaceId);
            //ws.WorkspaceDocuments = WorkspaceManagementBER.Instance.GetWorkspaceDocuments(companyDB, workspaceId);
            return ws;
        }

        public static WorkspaceList AddChildsToWorkspace(string companyDb, long parentWorkspaceId, WorkspaceList workspaceList)
        {
            Database dal = CPCHS.Common.Database.Database.GetDatabase("ErWorkspaces", companyDb);
            using (DbConnection connection = dal.CreateConnection())
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        foreach (Workspace item in workspaceList.Items)
                        {
                            item.WorkspaceParentId = parentWorkspaceId;
                            WorkspaceManagementBER.Instance.UpdateWorkspaceParent(companyDb, item, transaction);
                        }

                        transaction.Commit();
                        return workspaceList;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

        //private static void CreateMemberPermissions(string companyDB, WorkspaceMember ownerUser, long workspaceId)
        //{
        //    WorkspaceMemberList list = new WorkspaceMemberList();
        //    list.Add(ownerUser);
        //    CreateMemberPermissions(companyDB, list, workspaceId);
        //}

        private static void CreateMemberPermissions(string companyDB, WorkspaceMemberList ownerUsers, long workspaceId)
        {
            AutMemberList memberList = new AutMemberList();
            foreach (WorkspaceMember user in ownerUsers.Items)
            {
                AutMemberDetail detail = new AutMemberDetail()
                {
                    MemberType = user.WorkspaceMemberType,
                    MemberValue =user.WorkspaceMemberId
                };
                AutMemberDetailList detailList = new AutMemberDetailList()
                {
                    Items = new System.ComponentModel.BindingList<AutMemberDetail>()
                };
                detailList.Items.Add(detail);

                AutMember member = new AutMember()
                {
                    MemberScope = "ERESULTS",
                    MemberDetails = detailList
                };

                member.MemberPermissions = new AutPermissionTypeList();
                foreach (var item in user.WorkspaceMemberPerms)
                {
                    AutPermissionType permType = new AutPermissionType()
                    {
                        PermTypeType = item,
                        PermTypeValue = null
                    };
                    member.MemberPermissions.Add(permType);
                }
                memberList.Add(member);
            }

            AutContext context = new AutContext()
            {
                ContextType = "WORKSPACE_ID",
                ContextValue = workspaceId.ToString()
            };

            AuthorizationManagementProxy autProxy = new AuthorizationManagementProxy(companyDB, "Admin");
            bool allOk = autProxy.AddMultiplePermissionsForUsers(companyDB, memberList, context);

            if (allOk == false)
                throw new Exception("Permissions registering failed");
        }

        public static Workspace GetWorkspaceTree(string companyDB, long workspaceId, string user)
        {
            WorkspaceList wsList = WorkspaceManagementBER.Instance.GetWorkspaceTree(companyDB, workspaceId, user);

            Workspace root = wsList.Items.Where(x => x.WorkspaceLevel == 1).FirstOrDefault();

            if (root == null)
            {
                throw new Exception("Error on GetWorkspaceTree: no root was found.");
            }
            else
                wsList.Remove(root);

            BuildWorkspacesTree(wsList, ref root);

            return root;
        }

        private static void BuildWorkspacesTree(WorkspaceList wsList, ref Workspace root)
        {
            long id = root.WorkspaceId;
            List<Workspace> childWsList = wsList.Items.Where(x => x.WorkspaceParentId == id).ToList();
            root.WorkspaceChilds = new WorkspaceList();

            foreach (var item in childWsList)
            {
                wsList.Remove(item);
                root.WorkspaceChilds.Items.Add(item);
            }

            for(int i = 0 ; i < childWsList.Count ; i++)
            {
                var item = childWsList[i];
                BuildWorkspacesTree(wsList, ref item);
            }
        }

        public static bool RemoveWorkspaceDocument(string companyDB, long workspaceId, long documentId)
        {

            Database dal = CPCHS.Common.Database.Database.GetDatabase("ErWorkspaces", companyDB);
            using (DbConnection connection = dal.CreateConnection())
            {
                connection.Open();
                using (DbTransaction transaction = connection.BeginTransaction())
                {
                    try
                    {
                        //Workspace ws = WorkspaceManagementBER.Instance.GetWorkspace(companyDB, workspaceId);
                        //ws.WorkspaceMembers = WorkspaceManagementBER.Instance.GetWorkspaceMembers(companyDB, workspaceId);
                        //ws.workspaceDocuments = WorkspaceManagementBER.Instance.GetWorkspaceDocuments(companyDB, workspaceId);
                        WorkspaceDocumentList workspaceDocuments = WorkspaceManagementBER.Instance.GetWorkspaceDocuments(companyDB, workspaceId);

                        WorkspaceDocument workspaceDocument = null;

                        for (int i = 0; i < workspaceDocuments.Count; i++)
                        {
                            if (workspaceDocuments[i].DocumentId == documentId) {
                                workspaceDocument = workspaceDocuments[i];
                                break;
                            }
                        }

                        WorkspaceManagementBER.Instance.RemoveWorkspaceDocument(companyDB, workspaceDocument, transaction);
                        
                        transaction.Commit();
                        
                        return true;
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        throw ex;
                    }
                    finally
                    {
                        connection.Close();
                    }
                }
            }
        }

    }
}
