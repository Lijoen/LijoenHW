<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="LeftMeny.aspx.cs" Inherits="TestMaster.WebForm2" %>


<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
    <asp:TreeView ID="TreeView1" runat="server">
        <Nodes>
            <asp:TreeNode Text="Contact" Value="Contact">
                <asp:TreeNode NavigateUrl="~/AddContact.aspx" Text="Add" Value="Add"></asp:TreeNode>
                <asp:TreeNode Text="Edit" Value="Edit"></asp:TreeNode>
            </asp:TreeNode>
        </Nodes>
    </asp:TreeView>
</asp:Content>
