﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserPrivilege.aspx.cs" Inherits="DigitalSchool.Admin.UserPrivilege" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .fieldset {
           float: right;
    height: 83%;
    margin-right: 5px;
    padding-bottom: 23px;
    padding-left: 5px;
    padding-right: 5px;
    width: 57%;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     
   <div class="tgPanel" style="margin-top:20px;border:1px solid #9E4B9E!important">
    <div class="tgPanelHead" style="background-color:#9E4B9E;padding: 2px 10px;">Privilege</div>
    <div class="tgInput">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>  
        
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnSelectAll" EventName="Click" />
            </Triggers>
            <ContentTemplate>
             
        <asp:Panel ID="Panel1" runat="server" Height="60%" Width="100%" BorderStyle="Solid "  style=" border:0px solid">
            <fieldset style="padding: 5px; float:left; width: 35%; margin :5px; height:86%; padding: 5px">
                <legend>All User</legend>
                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>
                <br/>
                <br />
                <asp:ListBox ID="lbUserList" runat="server" Height="80%" OnSelectedIndexChanged="lbUserList_SelectedIndexChanged" AutoPostBack="True"></asp:ListBox>
            </fieldset >


            <fieldset class="fieldset" >
                <legend>Privilege For User</legend>
                <div class="CheckList">
               <asp:CheckBoxList ID="chkUserPrivilegeList" runat="server" Height="80%" Width="80%"  ></asp:CheckBoxList>
                    </div>
            </fieldset>

            <div class="buttonPrevious">
                <asp:Button ID="btnSelectAll" class="UserButton" runat="server" Text="Select All" OnClick="btnSelectAll_Click" />

            </div>
        </asp:Panel>

                 </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    </div>
</asp:Content>
