<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ExamTimetable.aspx.cs" Inherits="DS.UI.Academic.Timetable.ExamTimetable" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel{
            width: 100%;
        }
        .tbl-controlPanel td{
            width: 50%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;           
        }
        .controlLength{
            width: 230px;
        }   
        optgroup{
            color: #1fb5ad;
        } 
        option{
            color: #424242;
        }         
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
       <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="">
        <div class="row">
            <div class="col-md-4">
                <h4 class="text-left">Teacher Work Allotment</h4>             
            </div>
            <div class="col-md-4">
            
                  <asp:DropDownList ID="ddlShift" AutoPostBack="true" runat="server" ClientIDMode="Static" CssClass="input controlLength" >
                                                            
                  </asp:DropDownList>

                

            </div>
     <div class="row">            
            <div class="col-md-12">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>  
                        <asp:AsyncPostBackTrigger ControlID="ddlShift" />                      
                    </Triggers>
                    <ContentTemplate>
                        <div id="SubList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto;  overflow: auto; overflow-x: hidden;">
                        </div>       
                                              
                    </ContentTemplate>

                     

                </asp:UpdatePanel>
               
            </div>
            <%--<div class="col-md-4">
                <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                    <ContentTemplate>
                        <div id="TeacherList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;">
                        </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>--%>
        </div>
    </div>
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>
