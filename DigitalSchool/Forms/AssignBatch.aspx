<%@ Page Title="Batch Assign" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="AssignBatch.aspx.cs" Inherits="DS.Forms.AssignBatch" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">      
    <style>
        .tgPanel{
            border:none;        
        }
        .listStyle{
            padding: 10px 20px;
            font-size: 1.2em;
            cursor: pointer;            
        }
    </style>    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server"> 
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="container">
        <div class="row">
            <div class="col-md-12">
                <div class="col-md-5"></div>
                <div class="col-md-2">
                    <p class="text-center">Assign Batch</p>
                </div>
                <div class="col-md-5"></div>
            </div>
        </div>
        <div class="row">
            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                <ContentTemplate>
                    <div class="col-md-12">
                        <div class="col-md-5">
                            <div class="tgPanelHead">Not Assign Batch List</div>
                            <asp:ListBox ID="lstNotAssignedList" runat="server" Width="100%" Height="400px" CssClass="listStyle"
                                OnSelectedIndexChanged="lstNotAssignedList_SelectedIndexChanged"></asp:ListBox>
                        </div>
                        <div class="col-md-2">
                            <asp:Button ID="btnAssign" runat="server" CssClass="btn btn-primary" Text="Assign" OnClick="btnAssign_Click"
                                Style="margin-top: 100px; margin-left: 25%" />
                        </div>
                        <div class="col-md-5">
                            <div class="tgPanelHead">All Assigned Batch List</div>
                            <asp:ListBox ID="lstAssignedList" runat="server" Width="100%" Height="400px" CssClass="listStyle"></asp:ListBox>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">

</asp:Content>

