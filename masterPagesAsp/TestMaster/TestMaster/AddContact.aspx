<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AddContact.aspx.cs" Inherits="TestMaster.WebForm3" %>

<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

    <form id="form1" runat="server">
        <asp:Label ID="lFirstName" runat="server" Text="First Name"></asp:Label>
        <br />

        <asp:TextBox ID="txFirstName" runat="server"></asp:TextBox>

        <br />
        <asp:Label ID="lLastName" runat="server" Text="Last Name"></asp:Label>
        <br />
        <asp:TextBox ID="txLastName" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lSocialSecurity" runat="server" Text="Social Security"></asp:Label>
        <br />
        <asp:TextBox ID="txSocialSecurity" runat="server" OnTextChanged="Page_Load"></asp:TextBox>
        <br />
        <asp:Label ID="lEmailAdress" runat="server" Text="Email Adress"></asp:Label>
        <br />
        <asp:TextBox ID="txEmailAdress" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lSSNExists" runat="server" BackColor="#CC0000" ForeColor="White" Visible="False"></asp:Label>
        <br />
        <asp:Button ID="bAddUser"  BackColor="Green" ForeColor="White" runat="server" Text="Add This User" OnClick="bAddUser_Click" />

    </form>

</asp:Content>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">

 </asp:Content>

<asp:Content ID="Content5" runat="server" contentplaceholderid="head">
    <style type="text/css">
        #form1 {
            width: 655px;
        }
    </style>
</asp:Content>


