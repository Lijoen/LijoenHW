<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="EditContact.aspx.cs" Inherits="TestMaster.WebForm4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .auto-style1 {
            width: 886px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderHeader" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolderLeft" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ContentPlaceHolderMain" runat="server">

    <form id="form1" runat="server">

        <table>
            <tr>

                <td>
                       <br />




                    </td>
                <td>

                                         <asp:Label ID="lSearchFirstName" runat="server" Text="Search First Name"></asp:Label>
        <br />
        <asp:TextBox ID="txSearchFirstName" runat="server"  OnTextChanged="txSearchFirstName_TextChanged"></asp:TextBox>
        <br />
        <asp:Button ID="bSearch" runat="server" OnClick="bSearch_Click" Text="Search" />
        <br />
        <br />

        <br />

                </td>

                </tr>

            <tr>

                <td>



                </td>


                <td class="auto-style1">

     
    <asp:GridView ID="contactGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="ID" DataSourceID="SqlDataSource3" AllowSorting="True" CellPadding="20" Width="900px" AllowPaging="True" Height="150px" OnSelectedIndexChanged="contactGrid_SelectedIndexChanged1" BackColor="White" BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" GridLines="Horizontal" CellSpacing="10" HorizontalAlign="Right">
        <Columns>
            <asp:CommandField ButtonType="Button" ShowSelectButton="True" EditText="" SelectText="SELECT" >
            <ControlStyle BackColor="#006600" Font-Bold="True" ForeColor="White" />
            <ItemStyle HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:CommandField>
            <asp:CommandField ButtonType="Button" ShowEditButton="True" EditText="EDIT" UpdateText="Save" >
            <ControlStyle BackColor="#CC9900" ForeColor="White" />
            <ItemStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:CommandField>
            <asp:BoundField DataField="Firstname" HeaderText="First Name" SortExpression="Firstname" >
            <ControlStyle Height="30px" />
            </asp:BoundField>
            <asp:BoundField DataField="Lastname" HeaderText="Last Name" SortExpression="Lastname">
            </asp:BoundField>
            <asp:BoundField DataField="SSN" HeaderText="Social Security" SortExpression="SSN">
            </asp:BoundField>
            <asp:BoundField DataField="EmailAdress" HeaderText="Email Adress" SortExpression="EmailAdress" />
            <asp:CommandField ButtonType="Button" ShowDeleteButton="True" >
            <ControlStyle BackColor="Maroon" ForeColor="White" />
            <ItemStyle Width="80px" HorizontalAlign="Center" VerticalAlign="Middle" />
            </asp:CommandField>
        </Columns>
        <FooterStyle BackColor="White" ForeColor="#333333" />
        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
        <RowStyle BackColor="White" ForeColor="#333333" />
        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="Black" />
        <SortedAscendingCellStyle BackColor="#F7F7F7" />
        <SortedAscendingHeaderStyle BackColor="#487575" />
        <SortedDescendingCellStyle BackColor="#E5E5E5" />
        <SortedDescendingHeaderStyle BackColor="#275353" />
        </asp:GridView>

        <asp:SqlDataSource ID="SqlDataSource2" runat="server"></asp:SqlDataSource>
        <asp:SqlDataSource ID="SqlDataSourceFIlterName" runat="server" ConnectionString="<%$ ConnectionStrings:ContactsConnectionString %>" SelectCommand="spGetContactsLikeFirstName2" SelectCommandType="StoredProcedure">
            <SelectParameters>
                <asp:ControlParameter ControlID="txSearchFirstName" Name="firstName" PropertyName="Text" Type="String" />
            </SelectParameters>
        </asp:SqlDataSource>

                    </td>

                </tr>
            <tr>

        <td>
                

        </td>
               
                <td class="auto-style1">
                    <br />
        <asp:Label ID="lAddAdress" runat="server" Font-Bold="True" Font-Underline="False" Text="Add Adress" Visible="False"></asp:Label>

        <br />
        <asp:Label ID="lStreet" runat="server" Text="Street" Visible="False"></asp:Label>
        <br />
        <asp:TextBox ID="txStreet" runat="server" Visible="False" Width="190px"></asp:TextBox>
        <br />
        <asp:Label ID="lCity" runat="server" Text="City" Visible="False"></asp:Label>
        <br />
        <asp:TextBox ID="txCity" runat="server" Visible="False" Width="120px"></asp:TextBox>
        <br />
        <asp:Button ID="bAddAdress" runat="server" OnClick="bAddAdress_Click" Text="Add" Width="47px" BackColor="#99FF99" ForeColor="Black" Visible="False" />
                    <br />
                    <br />
                    <asp:Label ID="lAdresses" runat="server" Font-Bold="True" Font-Size="20px" Text="Current Adresses" Visible="False"></asp:Label>
        <br />

            </td>

                <td></td>

                </tr>

            <tr>
               
                    <td></td>
                <td>

        <asp:GridView ID="adressGrid" runat="server" AutoGenerateColumns="False" DataKeyNames="ID1" DataSourceID="SqlDataSource1" CellPadding="2" Width="900px" ForeColor="Black" GridLines="None" BackColor="LightGoldenrodYellow" BorderColor="Tan" BorderWidth="1px" HorizontalAlign="Center" OnSelectedIndexChanged="adressGrid_SelectedIndexChanged">
            <AlternatingRowStyle BackColor="PaleGoldenrod" />
            <Columns>
                <asp:BoundField DataField="Street" HeaderText="Street" SortExpression="Street" />
                <asp:BoundField DataField="City" HeaderText="City" SortExpression="City" />
                <asp:CommandField ButtonType="Button" ShowEditButton="True">
                <ControlStyle BackColor="#996600" ForeColor="White" />
                <ItemStyle Width="80px" />
                </asp:CommandField>
                <asp:CommandField ButtonType="Button" ShowDeleteButton="True">
                <ControlStyle BackColor="Maroon" ForeColor="White" />
                <ItemStyle Width="80px" />
                </asp:CommandField>
            </Columns>
            <FooterStyle BackColor="Tan" />
            <HeaderStyle BackColor="Tan" Font-Bold="True" />
            <PagerStyle ForeColor="DarkSlateBlue" HorizontalAlign="Center" BackColor="PaleGoldenrod" />
            <SelectedRowStyle BackColor="DarkSlateBlue" ForeColor="GhostWhite" />
            <SortedAscendingCellStyle BackColor="#FAFAE7" />
            <SortedAscendingHeaderStyle BackColor="#DAC09E" />
            <SortedDescendingCellStyle BackColor="#E1DB9C" />
            <SortedDescendingHeaderStyle BackColor="#C2A47B" />
        </asp:GridView>


        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:adressConnectionString %>" InsertCommand="spAddAdress" InsertCommandType="StoredProcedure" SelectCommand="spGetAdressesFromID" SelectCommandType="StoredProcedure" UpdateCommand="spUpdateAdressGridView" UpdateCommandType="StoredProcedure" DeleteCommand="spDeleteAdressGridView" DeleteCommandType="StoredProcedure">
            <DeleteParameters>
                <asp:Parameter Name="ID" Type="Int32" />
                <asp:Parameter Name="ID1" Type="Int32" />
                <asp:Parameter Name="AID" Type="Int32" />
                <asp:Parameter Name="CID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="Street" Type="String" />
                <asp:Parameter Name="City" Type="String" />
                <asp:Parameter Name="Postal" Type="String" />
                <asp:Parameter Direction="InputOutput" Name="NewIDA" Type="Int32" />
            </InsertParameters>
            <SelectParameters>
                <asp:ControlParameter ControlID="contactGrid" Name="CID" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
            
           
            <UpdateParameters>
                <asp:Parameter Name="ID" Type="Int32" />
                <asp:Parameter Name="ID1" Type="Int32" />
                <asp:Parameter Name="AID" Type="Int32" />
                <asp:Parameter Name="CID" Type="Int32" />
                <asp:Parameter Name="Street" Type="String" />
       <%--         <asp:Parameter Name="City" Type="String" />--%>
                <asp:Parameter Name="city" Type="String" />
                <asp:Parameter Name="postal" Type="String" />
            </UpdateParameters>
        </asp:SqlDataSource>

        <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:contactConnectionStringTable %>" DeleteCommand="spDeleteContact" DeleteCommandType="StoredProcedure" InsertCommand="spAddContact" InsertCommandType="StoredProcedure" SelectCommand="spGetAllContacts" SelectCommandType="StoredProcedure" UpdateCommand="spUpdateContact_test" UpdateCommandType="StoredProcedure">
            <DeleteParameters>
                <asp:Parameter Name="ID" Type="Int32" />
            </DeleteParameters>
            <InsertParameters>
                <asp:Parameter Name="firstName" Type="String" />
                <asp:Parameter Name="lastName" Type="String" />
                <asp:Parameter Name="SSN" Type="String" />
                <asp:Parameter Name="emailAdress" Type="String" />
                <asp:Parameter Direction="InputOutput" Name="newID" Type="Int32" />
            </InsertParameters>

            <UpdateParameters>
                <asp:Parameter Name="firstName" Type="String" />
                <asp:Parameter Name="lastName" Type="String" />
                <asp:Parameter Name="SSN" Type="String" />
                <asp:Parameter Name="emailAdress" Type="String" />
                <asp:Parameter Name="ID" Type="Int32" />
            </UpdateParameters>
        </asp:SqlDataSource>

                        </td>
            </tr>
        </table>
    </form>

</asp:Content>
