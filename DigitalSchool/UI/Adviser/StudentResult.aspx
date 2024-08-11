<%@ Page Title="" Language="C#" MasterPageFile="~/Adviser.Master" AutoEventWireup="true" CodeBehind="StudentResult.aspx.cs" Inherits="DS.UI.Adviser.StudentResult" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
      <style>
        .tgPanel {
            width: 100%;
        }

        .input {
            width: 200px;
        }

        .tbl-controlPanel {
            width: 400px;
        }

            .tbl-controlPanel td:first-child,
            .tbl-controlPanel td:nth-child(2n+1) {
                text-align: right;
                padding-right: 7px;
            }

        .table tr th {
            background-color: #23282C;
            color: white;
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
                    <a id="A1" runat="server" href="~/UI/Adviser/AdviserHome.aspx">
                        <i class="fa fa-dashboard"></i>
                        DashBoard
                    </a>
                </li>                
                <li class="active">Examination</li>                               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="row"> 
        <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ddlExamType" />
            </Triggers>
        <ContentTemplate>
        <div class="col-md-6">
        <div class="tgPanel" style="width:100%;height:115px">        
                          
            <table class="tbl-controlPanel" style="text-align:center">
                <tr>
                    <td>
                        Exam Type
                    </td>
                    <td>:</td>
                    <td>
                        <asp:DropDownList ID="ddlExamType" runat="server" Width="300px" ClientIDMode="Static"
                         AutoPostBack="true" OnSelectedIndexChanged="ddlExamType_SelectedIndexChanged" CssClass="input controlLength"></asp:DropDownList>
                    </td>
                </tr>
            </table>                        
        </div>       
        </div> 
        <div class="col-md-6">
            <div class="tgPanel" style="width:100%">    
            <div id="divResultSummary" class="datatables_wrapper" runat="server" style="width: 100%; height: auto"></div>
                </div>
        </div>  
            </ContentTemplate>
            </asp:UpdatePanel>        
    </div>
   <br />
    <div class="row">
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <ContentTemplate>               
                <div class="col-md-3">
                    <a id="A2" runat="server" onclick="return validateDropDown();" onserverclick="A2_ServerClick">
                        <div class="panel">
                            <div class="panel-body">
                                <h5 class="text-center">Tabulation Sheet</h5>
                            </div>
                        </div>
                    </a>
                </div>
                <div class="col-md-3">
                    <a id="A3" runat="server" onclick="return validateDropDown();" onserverclick="A3_ServerClick">
                        <div class="panel">
                            <div class="panel-body">
                                <h5 class="text-center">Result Summary</h5>
                            </div>
                        </div>
                    </a>
                </div>                            
                 <div class="col-md-3">
                    <a id="A6" runat="server" onclick="return validateDropDown();" onserverclick="A6_ServerClick">
                        <div class="panel">
                            <div class="panel-body">
                                <h5 class="text-center">Fail Subject</h5>
                            </div>
                        </div>
                    </a>
                </div>
                 <div id="divacademictranscript" runat="server" visible="false" class="col-md-3">
                    <a id="A4" runat="server" onclick="return validateDropDown();" onserverclick="A4_ServerClick">
                        <div class="panel">
                            <div class="panel-body">
                                <h5 class="text-center">Academic Transcript</h5>
                            </div>
                        </div>
                    </a>
                </div>    
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
      <script type="text/javascript">
          function validateDropDown() {
              if (validateCombo('ddlExamType', 0, 'Select Exam Type') == false) return false;
              return true;
          }
    </script>
</asp:Content>
