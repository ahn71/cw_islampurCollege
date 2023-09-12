<%@ Page Title="" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="pdf.aspx.cs" Inherits="DS.pdf" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ForLeftSideMenuList" runat="server">
     <asp:Panel ID="pnlPerson" runat="server">
     <div id="Div1" runat="server">This is div Content</div>
    <asp:Label ID="Label1" runat="server">Name:Md Jamal Hossain</asp:Label>
    <table border="1" style="font-family: Arial; font-size: 10pt; width: 200px">
        <tr>
            <td colspan="2" style="background-color: #18B5F0; font-size:16px; height: 18px; color: White; border: 1px solid white">
                <b>Personal Details</b>
            </td>
        </tr>
        <tr>
            <td><b>Name:</b></td>
            <td><asp:Label ID="lblName" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><b>Age:</b></td>
            <td><asp:Label ID="lblAge" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><b>City:</b></td>
            <td><asp:Label ID="lblCity" runat="server"></asp:Label></td>
        </tr>
        <tr>
            <td><b>Country:</b></td>
            <td><asp:Label ID="lblCountry" runat="server"></asp:Label></td>
        </tr>
    </table>
</asp:Panel>
    <asp:UpdatePanel runat="server" ID="upRoutine" UpdateMode="Conditional">
    <Triggers>
        <asp:AsyncPostBackTrigger ControlID="btnExport" />
       
    </Triggers>
    <ContentTemplate>
<asp:Button ID="btnExport" runat="server" Text="Export" OnClick="btnExport_Click" />
        </ContentTemplate>
        </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>
