<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ProfileStafforFaculty.aspx.cs" Inherits="DS.UI.Reports.StafforFaculty.ProfileStafforFaculty" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .litleMargin {
            margin-left: 5px;
        }
        .tgPanel {
            width: 100%;
        }
        input[type="radio"] {
            margin: 10px;
        }
        .btn {
            margin: 3px;
        }
        .controlLength{
            width: 250px;
        }
        .tbl-controlPanel {
            width:450px;
        }
        .tbl-controlPanel td:first-child{
            padding-right: 5px;
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
                <li><a runat="server" href="~/UI/Reports/StafforFaculty/StaffFacultyHome.aspx">Employees Info</a></li>
                <li class="active">Profile</li>               
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
      
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <div class="tgPanel">
        <asp:UpdatePanel ID="UpdatePanel2" runat="server">
            <Triggers>                
                <asp:AsyncPostBackTrigger ControlID="btnSearch" />
                <asp:AsyncPostBackTrigger ControlID="rblEmpType" />
            </Triggers>
            <ContentTemplate>
                <table class="tbl-controlPanel">
                    <tr>   
                        <td>                  
                            <asp:RadioButtonList ID="rblEmpType" runat="server" CssClass="radiobuttonlist"  RepeatDirection="Horizontal" 
                                AutoPostBack="true" OnSelectedIndexChanged="rblEmpType_SelectedIndexChanged" >
                              
                            </asp:RadioButtonList></td>
                          <td>CardNo</td>
                        <td>
                            <asp:DropDownList ID="ddlCardNo" Width="194px" runat="server" CssClass="input controlLength" ClientIDMode="Static">
                                
                            </asp:DropDownList>
                        </td> 
                         
                          
                     </tr>
                                     
                   
                </table>
                 <table class="tbl-controlPanel">
                    <tr>
                     <td>
                            <asp:RadioButtonList ID="rblImageStatus" runat="server" CssClass="radiobuttonlist"  RepeatDirection="Horizontal" >
                                <asp:ListItem  Selected="True" Value="1">With Image</asp:ListItem>
                                <asp:ListItem  Value="0">Without Image</asp:ListItem>
                            </asp:RadioButtonList></td>                      
                        <td>
                             <asp:Button ID="btnSearch" Text="Preview & Print" ClientIDMode="Static" runat="server" CssClass="btn btn-primary"
                              OnClientClick="return validation();"  OnClick="btnSearch_Click"  />
                        </td>
                          <td></td>
                        <td></td>
                     </tr>
                                     
                   
                </table>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>       
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ddlCardNo").select2();
        });
        function validation() {
            if (validateCombo('ddlCardNo', "0", 'Select Card No') == false) return false;            
            return true;
        }
        function goToNewTab(url) {
            window.open(url);
            load();
        }
        function load() {           
            $("#ddlCardNo").select2();
        }
    </script>
</asp:Content>
