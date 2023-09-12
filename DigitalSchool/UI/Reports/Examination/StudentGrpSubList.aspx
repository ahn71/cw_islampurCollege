<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="StudentGrpSubList.aspx.cs" Inherits="DS.UI.Reports.Examination.StudentGrpSubList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .controlLength {
            min-width: 160px;
            margin: 5px;
        }

        .tgPanel {
            width: 100%;
        }

        .littleMargin {
            margin-right: 5px;
        }

        /*.tbl-controlPanel {
            width: 728px;
        }*/
          @media (min-width: 320px) and (max-width: 480px) {
            .litleMargin {
            
           margin-left:5px;
            }
        }
    </style>   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
     <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">               
                <li>
                    <a id="A1" runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li> 
                <li><a id="A2" runat="server" href="~/UI/Reports/ReportHome.aspx">Reports Module</a></li>
                <li><a id="A3" runat="server" href="~/UI/Reports/Examination/ExaminationHome.aspx">Examination</a></li>
                <li class="active">Student Group Subject List</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>  
    <div class="">
        <div class="tgPanel">
            <div class="tgPanelHead">Student Group Subject List</div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
                    <asp:AsyncPostBackTrigger ControlID="ddlgroup" />
                    <asp:AsyncPostBackTrigger ControlID="ddlSection" />
                    <asp:AsyncPostBackTrigger ControlID="btnPreview" />
                </Triggers>
                <ContentTemplate>

                    <div class="row tbl-controlPanel ">
                        
                        <div class="col-md-10 col-md-offset-1">
                            <div class="form-inline">
                                <div class="form-group">
                                    <label for="exampleInputName2">Shift</label>
                                    <asp:DropDownList ID="ddlShiftList"  runat="server" class="input controlLength form-control">
                                    </asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputEmail2">Batch</label>
                                    <asp:DropDownList ID="ddlBatch"  runat="server" class="input controlLength form-control" 
                                         OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged" AutoPostBack="True"></asp:DropDownList>
                                </div>
                                <div class="form-group" runat="server" id="divGroup" visible="false">
                                    <label for="exampleInputName2">Group</label>
                                    <asp:DropDownList ID="ddlgroup"  runat="server" class="input controlLength form-control" 
                                        OnSelectedIndexChanged="ddlgroup_SelectedIndexChanged" ClientIDMode="Static" Enabled="true"  AutoPostBack="True"></asp:DropDownList>
                                </div>
                                <div class="form-group">
                                    <label for="exampleInputName2">Section</label>
                                    <asp:DropDownList ID="ddlSection"  OnSelectedIndexChanged="ddlSection_SelectedIndexChanged"
                                         AutoPostBack="true" runat="server" class="input controlLength form-control"></asp:DropDownList>
                                </div>
                                 <div class="form-group">
                                    <label for="exampleInputName2">Roll</label>
                                    <asp:DropDownList ID="ddlRoll" runat="server" class="input controlLength form-control"></asp:DropDownList>
                                </div>                                 
                                <asp:Button ID="btnPreview" Text="Preview & Print " ClientIDMode="Static" runat="server"
                                 OnClientClick="return validateDropDown();"  OnClick="btnPreview_Click"  CssClass="btn btn-success litleMargin"/>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
</asp:Content>

