<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="AddTitle.aspx.cs" Inherits="DS.UI.Administration.Finance.Accounts.AddTitle" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     <style>
        .tgPanel {
            width: 100%;
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
         .tahoma {
            text-align:center;
        }        
        #MainContent_CalendarExtender1_daysTable td,
        #MainContent_CalendarExtender1_daysTable td:first-child,
        #MainContent_CalendarExtender1_daysTable td:nth-child(3),
        #MainContent_CalendarExtender2_daysTable td,
        #MainContent_CalendarExtender2_daysTable td:first-child,
        #MainContent_CalendarExtender2_daysTable td:nth-child(3),
        #MainContent_CalendarExtender3_daysTable td,
        #MainContent_CalendarExtender3_daysTable td:first-child,
        #MainContent_CalendarExtender3_daysTable td:nth-child(3){
            width: auto;
            margin: 0;
            padding: 0;
        }
         .charCount {
            padding: 0px;
            list-style: outside none none;
            margin: 0px;
        }
        .charCount li{
            display: inline;
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
                <li><a runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Finance/FinanceHome.aspx">Finance Module</a></li>
                <li><a runat="server" href="~/UI/Administration/Finance/Accounts/AccountsHome.aspx">Accounts Management</a></li>                          
                <li class="active">Add Title</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-6">
                <h4 class="text-right">Title List</h4>
            </div>
            <div class="col-md-6"></div>
        </div>
        <div class="row">            
            <div class="col-md-6">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="btnSave" />
                    </Triggers>
                    <ContentTemplate>
                        <div id="divTemplateList" class="datatables_wrapper" runat="server"
                            style="width: 100%; height: auto; max-height: 300px; overflow: auto; overflow-x: hidden;">
                        </div>
                         <asp:HiddenField ID="lblTitleID" Value="0" ClientIDMode="Static" runat="server" />
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Add Title</div>
                    <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                 <tr>
                                    <td style="width: 15%">Type
                                    </td>
                                    <td style="width: 78%";>
                                        <asp:RadioButtonList ID="rblTitleType" ClientIDMode="Static" RepeatDirection="Horizontal"  runat="server">
                                            <asp:ListItem  Selected="True"  Value="False">Income</asp:ListItem>
                                             <asp:ListItem    Value="True">Expense</asp:ListItem>
                                        </asp:RadioButtonList>
                                    </td>
                                </tr> 
                                <tr>
                                    <td style="width: 15%">Title
                                    </td>
                                    <td style="width: 78%";>
                                        <asp:TextBox ID="txtTitle" runat="server" Width="100%" ClientIDMode="Static" CssClass="input form-control"></asp:TextBox>
                                    </td>
                                </tr>                               
                                <tr>
                                    <td></td>
                                    <td>
                                        <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" ClientIDMode="Static" Text="Save"
                                            OnClientClick="return btnAddTitle_validation();" OnClick="btnSave_Click"/>
                                       <asp:Button ID="btnReset" OnClientClick="clearIt();" CssClass="btn btn-default" runat="server" ClientIDMode="Static" Text="Reset"
                                           />
                                    </td>
                                </tr>
                            </table>                            
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>       
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">       
        function btnAddTitle_validation() {
            if (validateText('txtTitle', 1, 100, 'Enter a Title') == false) return false;           
            return true;
        }
        function SavedSuccess() {
            clearIt();
            showMessage('Saved successfully', 'success');
        }
        function updateSuccess() {
            clearIt();
            showMessage('Updated successfully', 'success');
        }
        function editTemplate(titleID) {
            $('#lblTitleID').val(titleID);
            var title = $('#title' + titleID).html();
            var type = $('#type' + titleID).html();
            if (type == "Income")
            {
                $('#<%=rblTitleType.ClientID %>').find("input[value='False']").prop("checked", true);               
               
            }
            else
            {
                $('#<%=rblTitleType.ClientID %>').find("input[value='True']").prop("checked", true);
            }           
            $('#txtTitle').val(title);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('input[type=text]').val('');
            $("#btnSave").val('Save');
            setFocus('txtTitle');           
        }
    </script>
</asp:Content>
