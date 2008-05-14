<%@ Page Language="C#" MasterPageFile="~/Assets/Layout/Pages/Main.master" AutoEventWireup="true"
    CodeFile="NewUser.aspx.cs" Inherits="NewUser" Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cphMain" runat="Server">
    <div class="post">
        <div class="title">
            <h2>
                Create a ZenDemo Account</h2>
        </div>
        <asp:CreateUserWizard ID="CreateUserWizard1" runat="server" ContinueDestinationPageUrl="~/Default.aspx" FinishDestinationPageUrl="~/Default.aspx">
            <WizardSteps>
                <asp:CreateUserWizardStep runat="server">
                </asp:CreateUserWizardStep>
                <asp:CompleteWizardStep runat="server">
                </asp:CompleteWizardStep>
            </WizardSteps>
        </asp:CreateUserWizard>
    </div>
</asp:Content>
