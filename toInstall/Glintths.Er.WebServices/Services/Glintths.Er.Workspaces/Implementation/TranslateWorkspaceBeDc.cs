
using System;
using System.Collections.Generic;
namespace Glintths.Er.Workspaces.ServiceImplementation
{
    public static class TranslateWorkspaceBeDc
    {
        #region Workspace

        public static Glintths.Er.Common.BusinessEntities.Workspace TranslateWorkspaceWorkspace(
            Glintths.Er.Common.DataContracts.Workspace from)
        {
            Glintths.Er.Common.BusinessEntities.Workspace to = new Glintths.Er.Common.BusinessEntities.Workspace();
            to.WorkspaceId = from.Id;
            to.WorkspaceParentId = from.ParentWorkspaceId;
            to.WorkspaceActive = from.Active;
            to.WorkspaceName = from.Name;
            to.WorkspaceDesc = from.Description;
            to.WorkspaceIsLeaf = from.IsLeaf;
            to.WorkspaceLevel = from.Level;
            to.WorkspaceMembers = TranslateWorkspaceMembersWorkspaceMembers(from.Members);
            to.WorkspaceChilds = TranslateWorkspacesWorkspaceList(from.ChildWorkspaces);

            return to;
        }

        public static Glintths.Er.Common.DataContracts.Workspace TranslateWorkspaceWorkspace(
            Glintths.Er.Common.BusinessEntities.Workspace from)
        {
            Glintths.Er.Common.DataContracts.Workspace to = new Glintths.Er.Common.DataContracts.Workspace();
            to.Id = from.WorkspaceId;
            to.ParentWorkspaceId = from.WorkspaceParentId;
            to.Active = from.WorkspaceActive;
            to.Name = from.WorkspaceName;
            to.Description = from.WorkspaceDesc;
            to.IsLeaf = from.WorkspaceIsLeaf;
            to.Level = from.WorkspaceLevel;
            to.Members = TranslateWorkspaceMembersWorkspaceMembers(from.WorkspaceMembers);
            to.ChildWorkspaces = TranslateWorkspacesWorkspaceList(from.WorkspaceChilds);

            return to;
        }
        
        #endregion

        #region WorkspaceMembers

        public static Glintths.Er.Common.DataContracts.WorkspaceMembers TranslateWorkspaceMembersWorkspaceMembers(
            Glintths.Er.Common.BusinessEntities.WorkspaceMemberList from)
        {
            Glintths.Er.Common.DataContracts.WorkspaceMembers to = new Common.DataContracts.WorkspaceMembers();
            to.Items = new Common.DataContracts.WorkspaceMemberList();

            if (from != null && from.Items != null)
            {
                foreach (var item in from.Items)
                {
                    to.Items.Add(TranslateWorkspaceMemberWorkspaceMember(item));
                }
            }
            return to;
        }

        public static Glintths.Er.Common.BusinessEntities.WorkspaceMemberList TranslateWorkspaceMembersWorkspaceMembers(
            Glintths.Er.Common.DataContracts.WorkspaceMembers from)
        {
            Glintths.Er.Common.BusinessEntities.WorkspaceMemberList to = new Common.BusinessEntities.WorkspaceMemberList();

            if (from != null && from.Items != null)
            {
                foreach (var item in from.Items)
                {
                    to.Add(TranslateWorkspaceMemberWorkspaceMember(item));
                }
            }

            return to;
        }
        
        #endregion

        #region WorkspaceMember

        public static Glintths.Er.Common.BusinessEntities.WorkspaceMember TranslateWorkspaceMemberWorkspaceMember(
            Glintths.Er.Common.DataContracts.WorkspaceMember from)
        {
            if (from == null)
                return null;
            Glintths.Er.Common.BusinessEntities.WorkspaceMember to = new Common.BusinessEntities.WorkspaceMember();

            to.WorkspaceMemberType = from.Type;
            to.WorkspaceMemberId = from.Id;
            to.WorkspaceMemberPerms = TranslateWorkspacePermissions(from.Permissions);

            return to;
        }

        public static Glintths.Er.Common.DataContracts.WorkspaceMember TranslateWorkspaceMemberWorkspaceMember(
            Glintths.Er.Common.BusinessEntities.WorkspaceMember from)
        {
            Glintths.Er.Common.DataContracts.WorkspaceMember to = new Glintths.Er.Common.DataContracts.WorkspaceMember();

            to.Type = from.WorkspaceMemberType;
            to.Id = from.WorkspaceMemberId;
            to.Permissions = TranslateWorkspacePermissions(from.WorkspaceMemberPerms);

            return to;
        }
        
        #endregion

        #region WorkspacePermissions

        private static Glintths.Er.Common.DataContracts.WorkspacePermissions TranslateWorkspacePermissions(List<string> from)
        {
            Glintths.Er.Common.DataContracts.WorkspacePermissions to = new Glintths.Er.Common.DataContracts.WorkspacePermissions();
            to.Items = new Common.DataContracts.WorkspacePermissionList();

            if (from != null)
            {
                foreach (string item in from)
                {
                    to.Items.Add(TranslateWorkspacePermission(item));
                }
            }

            return to;
        }

        private static List<string> TranslateWorkspacePermissions(Glintths.Er.Common.DataContracts.WorkspacePermissions from)
        {
            List<string> to = new List<string>();

            if (from != null && from.Items != null)
            {
                foreach (Glintths.Er.Common.DataContracts.WorkspacePermission item in from.Items)
                {
                    to.Add(TranslateWorkspacePermission(item));
                }
            }

            return to;
        }
        
        #endregion

        #region WorkspacePermission

        private static string TranslateWorkspacePermission(Common.DataContracts.WorkspacePermission from)
        {
            switch (from)
            {
                case Glintths.Er.Common.DataContracts.WorkspacePermission.View:
                    return "VIEW";                    
                case Glintths.Er.Common.DataContracts.WorkspacePermission.Admin:
                    return "ADMIN";
                case Glintths.Er.Common.DataContracts.WorkspacePermission.Promote:
                    return "PROMOTE";
                case Glintths.Er.Common.DataContracts.WorkspacePermission.Delete:
                    return "DELETE";
                default:
                    throw new ArgumentOutOfRangeException("The WorkspacePermission value is not valid");
            }
        }

        private static Glintths.Er.Common.DataContracts.WorkspacePermission TranslateWorkspacePermission(string from)
        {
            switch (from)
            {
                case "VIEW":
                    return Glintths.Er.Common.DataContracts.WorkspacePermission.View;
                case "ADMIN":
                    return Glintths.Er.Common.DataContracts.WorkspacePermission.Admin;
                case "PROMOTE":
                    return Glintths.Er.Common.DataContracts.WorkspacePermission.Promote;
                case "DELETE":
                    return Glintths.Er.Common.DataContracts.WorkspacePermission.Delete;
                default:
                    throw new ArgumentOutOfRangeException("The WorkspacePermission value is not valid");
            }
        }
        
        #endregion

        #region WorkspaceList

        public static Glintths.Er.Common.BusinessEntities.WorkspaceList TranslateWorkspacesWorkspaceList(
            Glintths.Er.Common.DataContracts.Workspaces from)
        {
            Glintths.Er.Common.BusinessEntities.WorkspaceList to = new Glintths.Er.Common.BusinessEntities.WorkspaceList();

            if (from != null)
            {
                foreach (Glintths.Er.Common.DataContracts.Workspace item in from.Items)
                {
                    to.Add(TranslateWorkspaceWorkspace(item));
                }
            }

            return to;
        }

        public static Glintths.Er.Common.DataContracts.Workspaces TranslateWorkspacesWorkspaceList(
            Glintths.Er.Common.BusinessEntities.WorkspaceList from)
        {
            Glintths.Er.Common.DataContracts.Workspaces to = new Common.DataContracts.Workspaces();
            to.Items = new Common.DataContracts.WorkspaceList();

            if (from != null)
            {
                foreach (Glintths.Er.Common.BusinessEntities.Workspace item in from.Items)
                {
                    to.Items.Add(TranslateWorkspaceWorkspace(item));
                }
            }

            return to;
        }
        
        #endregion
    }
}
