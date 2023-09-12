<%@ Page Title="Admit Card" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AdmitCardGenerator.aspx.cs" Inherits="DigitalSchool.Forms.AdmitCard" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <link href="../Styles/reports/CommonBorder.css" rel="stylesheet" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
<div class="Tab-Dev">
    	<div class="TabTop">
    		<div class="c-left">1</div>
            	<div class="TabTop-C">Admit Card Generate</div>
            <div class="c-right">2</div>
        </div>
        <div class="TabLeft ComTabLeft">
        	<table width="100%" border="0">
                <tr>
                <td>Exame</td>
                <td>
                    <asp:DropDownList ID="dlExamType" runat="server" class="TabSelect"></asp:DropDownList></td>
              </tr>
              <tr>
                <td>Class</td>
                <td>
                    <asp:DropDownList ID="dlClass" runat="server" class="TabSelect"></asp:DropDownList></td>
              </tr>
              <tr>
                <td>Section</td>
                <td>
                 <asp:DropDownList ID="dlSection" runat="server" class="TabSelect"></asp:DropDownList></td>
              </tr>
              <tr>
                <td style="height:30px;"></td>
                <td></td>
              </tr>
              <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnACGenerate"  class="TabButton" runat="server" Text="Porcess" OnClick="btnACGenerate_Click" /></td>
              </tr>
            </table>

        </div>

        <div class="TabCenter"></div>

        <div class="TabRight">
            <table width="100%" border="0">
                <tr>
                <td>Exame</td>
                <td>
                    <asp:DropDownList ID="dlExamForAdmintcardByRoll" runat="server" class="TabSelect"></asp:DropDownList></td>
              </tr>
              <tr>
                <td>Class</td>
                <td>
                    <asp:DropDownList ID="dlClassForAdmintcardByRoll" runat="server" class="TabSelect"></asp:DropDownList></td>
              </tr>
              <tr>
                <td>Section</td>
                <td>
                 <asp:DropDownList ID="dlSectionForAdmintcardByRoll" runat="server" class="TabSelect"></asp:DropDownList></td>
              </tr>
              <tr>
                <td>Roll</td>
                <td>
                    <asp:TextBox ID="txtAdmitCardRoll" runat="server" class="TabSelect"></asp:TextBox></td>
              </tr>       
              <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnAdmitCardProcessByRoll" OnClientClick="checking();"  class="TabButton" ClientIDMode="Static" runat="server" Text="Porcess" OnClick="btnAdmitCardProcessByRoll_Click" /></td>
              </tr>
            </table>
        </div>
       
    </div> 

    <div class="Tab-Dev">
    	<div class="TabTop">
    		<div class="c-left">1</div>
            	<div class="TabTop-C">Id Card Generate</div>
            <div class="c-right">2</div>
        </div>
        <div class="TabLeft">
        	<table width="100%" border="0">
                <tr>
                <td>Class</td>
                <td>
                    <asp:DropDownList ID="dlClassForIdCard" runat="server" class="TabSelect"></asp:DropDownList></td>
              </tr>
             
              <tr>
                <td>Section</td>
                <td>
                 <asp:DropDownList ID="dlSectionForIdCard" runat="server" class="TabSelect"></asp:DropDownList></td>
              </tr>
              <tr>
                <td style="height:30px;"></td>
                <td></td>
              </tr> 
              <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="btnIdCardGenerate"  class="TabButton" runat="server" Text="Porcess" OnClick="btnIdCardGenerate_Click"/></td>
              </tr>
            </table>

        </div>
       
        <div class="TabCenter" style="height:165px;"></div>

         <div class="TabRight">
            <table width="100%" border="0">
                
              <tr>
                <td>Class</td>
                <td>
                    <asp:DropDownList ID="dlClassForIdCardByROll" runat="server" class="TabSelect"></asp:DropDownList></td>
              </tr>
              <tr>
                <td>Section</td>
                <td>
                 <asp:DropDownList ID="dlSectionForIdCardByRoll" runat="server" class="TabSelect"></asp:DropDownList></td>
              </tr>
              <tr>
                <td>Roll</td>
                <td>
                    <asp:TextBox ID="txtIdCardRoll" runat="server" class="TabSelect"></asp:TextBox></td>
              </tr> 
              <tr>
                <td>&nbsp;</td>
                <td>
                    <asp:Button ID="Button2"  class="TabButton" runat="server" Text="Porcess" OnClick="Button2_Click" /></td>
              </tr>
            </table>
        </div>

    </div> 
    

</asp:Content>
