<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="SubjectWiseNumberSheet.aspx.cs" Inherits="DS.UI.Reports.Examination.SubjectWiseNumberSheet" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .controlLength {
            min-width: 140px;
            margin: 5px;
        }
        .tgInput td:first-child, 
        .tgInput td:nth-child(3), 
        .tgInput td:nth-child(5), 
        .tgInput td:nth-child(4) {
            padding: 0px;
            width: 20px;    
        }     
        .tgPanel {
            width: 100%;
        }        
        .littleMargin {
            margin-right: 5px;
        }
        #btnPrintPreview{
            margin: 3px;
        } 
          @media (min-width: 320px) and (max-width: 480px) {
            .littleMargin {
            min-width:180px;
            margin-left:40px;
            }
        }       
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">    
     <div class="row">
        <div class="col-md-12">
            <!--breadcrumbs start -->
            <ul class="breadcrumb">               
                <li>
                    <a runat="server" href="~/Dashboard.aspx">
                        <i class="fa fa-dashboard"></i>
                        Dashboard
                    </a>
                </li> 
                <li><a runat="server" href="~/UI/Reports/ReportHome.aspx">Reports Module</a></li>
                <li><a runat="server" href="~/UI/Reports/Examination/ExaminationHome.aspx">Examination</a></li>
                <li class="active">Exam Attendance Sheet</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>  
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="">
        <div class="tgPanel">
            <div class="tgPanelHead">Exam Attendance Sheet</div>
             <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                 <Triggers>
                     <asp:AsyncPostBackTrigger ControlID="ddlBatch" />
                     <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
                 </Triggers>
        <ContentTemplate>
            <div class="row tbl-controlPanel"> 
                <div class="col-sm-10 col-sm-offset-1">
                    <div class="form-inline">
                         <div class="form-group">
                             <label for="exampleInputName2">Shift</label>
                                <asp:DropDownList ID="dlShift" runat="server" CssClass="input controlLength form-control"
                            ClientIDMode="Static" AutoPostBack="false">
                        </asp:DropDownList>
                         </div>
                        <div class="form-group">
                             <label for="exampleInputName2">Bacth</label>
                                <asp:DropDownList ID="ddlBatch" runat="server" CssClass="input controlLength form-control"
                           OnSelectedIndexChanged="ddlBatch_SelectedIndexChanged"  ClientIDMode="Static" AutoPostBack="true">
                        </asp:DropDownList>
                         </div>                        
                        <div class="form-group">
                             <label for="exampleInputName2">Group</label>
                                <asp:DropDownList ID="ddlGroup" runat="server" ClientIDMode="Static"
                           OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"  AutoPostBack="True" CssClass="input controlLength form-control">
                        </asp:DropDownList>
                         </div>
                        <div class="form-group">
                             <label for="exampleInputName2">Exam</label>
                                <asp:DropDownList ID="ddlExamList" runat="server" ClientIDMode="Static"   AutoPostBack="True" CssClass="input controlLength form-control">
                        </asp:DropDownList>
                         </div>
                        <div class="form-group">
                             <label for="exampleInputName2">Subject</label>
                                <asp:DropDownList ID="ddlSubjectList" runat="server" ClientIDMode="Static" CssClass="input controlLength form-control">
                        </asp:DropDownList>
                         </div>
                        <div class="form-group">
                             <label for="exampleInputName2">Section</label>
                                <asp:DropDownList ID="dlSection" runat="server" CssClass="input controlLength form-control"
                            ClientIDMode="Static" AutoPostBack="false">
                        </asp:DropDownList>
                         </div>
                       <%--  <div class="form-group">
                             <label for="exampleInputName2">Roll No</label>
                               <asp:TextBox runat="server" ID="txtRollNo" ClientIDMode="Static"  CssClass="input controlLength form-control"></asp:TextBox>
                         </div>--%>
                    </div>
                </div>
            </div> 
           
            <div class="row tbl-controlPanel"> 
            <div class="col-sm-6 col-sm-offset-3">
                <div class="form-inline">
                     <div class="form-group">
                         
                            <asp:Button ID="btnPreview" CssClass="btn btn-primary littleMargin" Text="Preview"
                          OnClientClick="return validateDropDown();"   ClientIDMode="Static" runat="server" OnClick="btnPreview_Click" />
                     </div>
                   
                   
                </div>
            </div>
        </div> 
           
            <br />
            </ContentTemplate>
                 </asp:UpdatePanel>
        </div>       
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateDropDown() {
            if (validateCombo('dlShift', 0, 'Select Shift Name') == false) return false;
            if (validateCombo('ddlBatch', 0, 'Select Batch Name') == false) return false;           
            return true;
        }
    </script>
</asp:Content>

