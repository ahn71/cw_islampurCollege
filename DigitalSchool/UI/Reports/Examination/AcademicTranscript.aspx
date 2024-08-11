<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AcademicTranscript.aspx.cs" Inherits="DS.UI.Reports.Examination.AcademicTranscript" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .controlLength {
            min-width: 130px;
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
                <li class="active">Academic Transcript.</li>               
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
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="dlBatch" />
                 <asp:AsyncPostBackTrigger ControlID="dlSection" />
                <asp:AsyncPostBackTrigger ControlID="ddlGroup" />
                <asp:AsyncPostBackTrigger ControlID="ddlRoll" />
            </Triggers>
            <ContentTemplate>
                <div class="tgPanel">
                    <div class="tgPanelHead">Academic Transcript</div>
                          <div class="row tbl-controlPanel"> 
                            <div class="col-sm-8 col-sm-offset-2">
                                <div class="form-inline">
                                     <div class="form-group">
                                         <label for="exampleInputName2">Shift</label>
                                            <asp:DropDownList ID="dlShift" runat="server" CssClass="input controlLength form-control"
                                                ClientIDMode="Static" AutoPostBack="false">
                                            </asp:DropDownList>
                                     </div>
                                    <div class="form-group">
                                         <label for="exampleInputName2">Bacth</label>
                                            <asp:DropDownList ID="dlBatch" runat="server" AutoPostBack="true" CssClass="input controlLength form-control"
                                                OnSelectedIndexChanged="dlBatch_SelectedIndexChanged">
                                            </asp:DropDownList>
                                     </div>
                                    <div class="form-group">
                                         <label for="exampleInputName2">Exam Id</label>
                                            <asp:DropDownList ID="dlExamId" runat="server" CssClass="input controlLength form-control"
                                                ClientIDMode="Static" AutoPostBack="false">
                                            </asp:DropDownList>
                                     </div>
                                    <div class="form-group">
                                         <label for="exampleInputName2">Group</label>
                                            <asp:DropDownList ID="ddlGroup" runat="server" ClientIDMode="Static"
                                   OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"  AutoPostBack="True" CssClass="input controlLength form-control">
                                </asp:DropDownList>
                                     </div>
                                    
                                </div>
                            </div>
                        </div> 
                        <div class="row tbl-controlPanel"> 
                        <div class="col-sm-8 col-sm-offset-2">
                            <div class="form-inline">
                                 <div class="form-group">
                                     <label for="exampleInputName2">Section</label>
                                        <asp:DropDownList ID="dlSection" AutoPostBack="true" CssClass="input controlLength form-control"
                                        OnSelectedIndexChanged="dlSection_SelectedIndexChanged" runat="server">
                                    </asp:DropDownList>
                                 </div>
                                <div class="form-group">
                                     <label for="exampleInputName2">Roll</label>
                                        <asp:DropDownList ID="ddlRoll" runat="server" ClientIDMode="Static"
                                    CssClass="input controlLength form-control"
                                    AutoPostBack="false">
                                </asp:DropDownList>
                                 </div>
                                <div class="form-group">
                                     <label for="exampleInputName2"></label>
                                     <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" CssClass="btn btn-success littleMargin"
                                    OnClientClick="return validateDropDown();" runat="server" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnPrintPreview" runat="server" Text="Print Preview"
                                    OnClientClick="return validateDropDown();" CssClass="btn btn-success" OnClick="btnPrintPreview_Click" />
                                 </div>
                                <div class="form-group">
                                     <label for="exampleInputName2"></label>
                                        <asp:Label ID="lblPaymentDateStatus" runat="server" BorderColor="White" ForeColor="#0000CC"></asp:Label>
                                 </div>
                                
                            </div>
                        </div>
                    </div> 
                    <%--<table class="tbl-controlPanel">
                        <tr>
                            <td>Shift</td>
                            <td>
                                <asp:DropDownList ID="dlShift" runat="server" CssClass="input controlLength"
                                    ClientIDMode="Static" AutoPostBack="false">
                                </asp:DropDownList>
                            </td>
                            <td>Batch</td>
                            <td>
                                <asp:DropDownList ID="dlBatch" runat="server" AutoPostBack="true" CssClass="input controlLength"
                                    OnSelectedIndexChanged="dlBatch_SelectedIndexChanged">
                                </asp:DropDownList></td>
                            <td>Exam Id</td>
                            <td>
                                <asp:DropDownList ID="dlExamId" runat="server" CssClass="input controlLength"
                                    ClientIDMode="Static" AutoPostBack="false">
                                </asp:DropDownList>
                            </td>
                            <td>Group</td>
                            <td>
                                <asp:DropDownList ID="ddlGroup" runat="server" ClientIDMode="Static"
                                   OnSelectedIndexChanged="ddlGroup_SelectedIndexChanged"  AutoPostBack="True" CssClass="input controlLength">
                                </asp:DropDownList>
                            </td>

                            <td></td>
                        </tr>
                        <tr>
                            <td>Section</td>
                            <td>
                                <asp:DropDownList ID="dlSection" AutoPostBack="true" CssClass="input controlLength"
                                    OnSelectedIndexChanged="dlSection_SelectedIndexChanged" runat="server">
                                </asp:DropDownList>
                            </td>
                            <td>Roll</td>
                            <td>
                                <asp:DropDownList ID="ddlRoll" runat="server" ClientIDMode="Static"
                                    CssClass="input controlLength"
                                    AutoPostBack="false">
                                </asp:DropDownList>
                            </td>
                            <td colspan="3">
                                <asp:Button ID="btnSearch" Text="Search" ClientIDMode="Static" CssClass="btn btn-success littleMargin"
                                    OnClientClick="return validateDropDown();" runat="server" OnClick="btnSearch_Click" />
                                <asp:Button ID="btnPrintPreview" runat="server" Text="Print Preview"
                                    OnClientClick="return validateDropDown();" CssClass="btn btn-success" OnClick="btnPrintPreview_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td></td>
                            <td colspan="3">
                                <asp:Label ID="lblPaymentDateStatus" runat="server" BorderColor="White" ForeColor="#0000CC"></asp:Label>
                            </td>
                        </tr>
                    </table>--%>
                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
    <div class="">
        <div class="tgPanel">
            <div class="tgPanelHead">Searching Result</div>
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                </Triggers>
                <ContentTemplate>
                    <div style="width: 595px; margin: 5px auto; border: 1px solid gray">
                        <div>
                            <div style="width: 583px; margin: 0 auto;">
                                <div id="divAcademicTranscript" class="datatables_wrapper" runat="server"
                                    style="width: 100%; height: auto; overflow: auto; overflow-x: hidden;">
                                </div>
                            </div>
                        </div>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        //$(document).ready(function () { $("#ddlRoll").select2(); });
        function validateDropDown() {            
            if (validateCombo('dlShift', 0, 'Select Shift Name') == false) return false;
            if (validateCombo('dlBatch', 0, 'Select Batch Name') == false) return false;
            if (validateCombo('dlExamId', 0, 'Select Exam Id') == false) return false;
           // if (validateCombo('dlSection', 0, 'Select Section Name') == false) return false;
            //if (validateCombo('ddlRoll', 0, 'Select Roll Number') == false) return false;          
            return true;
        }
        $(document).ready(function () {
            $("#ddlRoll").select2();
        });
        function goToNewTab(url) {
            window.open(url);
            load();
        }
        function load() {
            $("#ddlRoll").select2();
        }
    </script>
</asp:Content>
