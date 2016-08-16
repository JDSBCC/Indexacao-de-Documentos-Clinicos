using Glintths.Er.Common.BusinessEntities;
using Glintths.Er.Workspaces.BusinessLogic;
using Glintths.Er.Workspaces.MessageContracts;


namespace Glintths.Er.Workspaces.ServiceImplementation
{
    public partial class WorkspacesManagementWs
    {
        public override CreateWorkspaceResponse CreateWorkspace(CreateWorkspaceRequest request)
        {
            CreateWorkspaceResponse resp = new CreateWorkspaceResponse();
            Workspace ws = WorkspacesLogic.CreateWorkspace(
                request.CompanyDB,
                request.Name,
                request.Description,
                TranslateWorkspaceBeDc.TranslateWorkspaceMemberWorkspaceMember(request.OwnerUser),
                request.Active,
                request.ParentId,
                TranslateWorkspaceBeDc.TranslateWorkspaceMembersWorkspaceMembers(request.Members),
                request.InheritMembers);
            resp.Workspace = TranslateWorkspaceBeDc.TranslateWorkspaceWorkspace(ws);            
            return resp;
        }

        public override ActivateWorkspaceResponse ActivateWorkspace(ActivateWorkspaceRequest request)
        {
            ActivateWorkspaceResponse resp = new ActivateWorkspaceResponse();
            resp.Donne = WorkspacesLogic.ActivateWorkspace(request.CompanyDB, request.WorkspaceId);
            return resp;
        }

        public override DeactivateWorkspaceResponse DeactivateWorkspace(DeactivateWorkspaceRequest request)
        {
            DeactivateWorkspaceResponse resp = new DeactivateWorkspaceResponse();
            resp.Donne = WorkspacesLogic.DeactivateWorkspace(request.CompanyDB, request.WorkspaceId);
            return resp;
        }
        
        public override AddMembersToWorkspaceResponse AddMembersToWorkspace(AddMembersToWorkspaceRequest request)
        {
            AddMembersToWorkspaceResponse resp = new AddMembersToWorkspaceResponse();

            resp.Donne = WorkspacesLogic.AddMembersToWorkspace(
                request.CompanyDB, 
                request.WorkspaceId,
                TranslateWorkspaceBeDc.TranslateWorkspaceMembersWorkspaceMembers(request.MembersToAdd));
            
            return resp;
        }

        public override RemoveMembersFromWorkspaceResponse RemoveMembersFromWorkspace(RemoveMembersFromWorkspaceRequest request)
        {
            RemoveMembersFromWorkspaceResponse resp = new RemoveMembersFromWorkspaceResponse();

            resp.Donne = WorkspacesLogic.RemoveMembersFromWorkspace(
                request.CompanyDB,
                request.WorkspaceId,
                TranslateWorkspaceBeDc.TranslateWorkspaceMembersWorkspaceMembers(request.MembersToRemove));
            

            return resp;
        }

        public override GetWorkspaceResponse GetWorkspace(GetWorkspaceRequest request)
        {
            GetWorkspaceResponse resp = new GetWorkspaceResponse();
            Workspace ws = WorkspacesLogic.GetWorkspace(request.CompanyDB, request.WorkspaceId);
            resp.Workspace = TranslateWorkspaceBeDc.TranslateWorkspaceWorkspace(ws);
            return resp;
        }

        public override AddChildsToWorkspaceResponse AddChildsToWorkspace(AddChildsToWorkspaceRequest request)
        {
            AddChildsToWorkspaceResponse resp = new AddChildsToWorkspaceResponse();

            var childWorkspaces = WorkspacesLogic.AddChildsToWorkspace(
                request.CompanyDB, 
                request.WorkspaceId,
                TranslateWorkspaceBeDc.TranslateWorkspacesWorkspaceList(request.ChildWorkspaces));

            resp.ChildWorkspaces = TranslateWorkspaceBeDc.TranslateWorkspacesWorkspaceList(childWorkspaces);

            return resp;
        }

        public override GetWorkspaceTreeResponse GetWorkspaceTree(GetWorkspaceTreeRequest request)
        {
            GetWorkspaceTreeResponse resp = new GetWorkspaceTreeResponse();

            Workspace rootWorkspace = WorkspacesLogic.GetWorkspaceTree(request.CompanyDB, request.WorkspaceId, request.User);

            resp.RootWorkspace = TranslateWorkspaceBeDc.TranslateWorkspaceWorkspace(rootWorkspace);
            return resp;
        }

        public override RemoveWorkspaceDocumentResponse RemoveWorkspaceDocument(RemoveWorkspaceDocumentResquest request)
        {
            RemoveWorkspaceDocumentResponse resp = new RemoveWorkspaceDocumentResponse();

            resp.Donne = WorkspacesLogic.RemoveWorkspaceDocument(
                request.CompanyDB,
                request.WorkspaceId,
                request.DocumentId);

            return resp;
        }
    }
}
