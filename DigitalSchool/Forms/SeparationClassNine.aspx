<%@ Page Title="Separation Class Nine" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SeparationClassNine.aspx.cs" Inherits="DS.Forms.PromotionClassNine" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .tgPanel{
            width:500px;
        }
        .listTab{
            background-color: white;
            border: 1px solid #ddd9d9;
            height: 500px;
            margin: 0 auto;            
        } 
        .depertment_button:last-child {
            margin-top: 70px;
            text-align: center;
        } 
        input[type="checkbox"]
        {
            margin: 2px 5px;            
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:UpdatePanel ID="uplMessage" runat="server" UpdateMode="Conditional">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="lblDepartmentId" ClientIDMode="Static" runat="server" />
    <asp:UpdatePanel ID="UpdatePanel6" runat="server">
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="btnScience" />
            <asp:AsyncPostBackTrigger ControlID="btnCommerce" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnAtrs" EventName="Click" />
            <asp:AsyncPostBackTrigger ControlID="btnSubmit" EventName="Click" />
        </Triggers>
        <ContentTemplate>
            <div class="container">
                <div class="row">
                    <div class="tgPanel">
                        <asp:Panel ClientIDMode="Static" runat="server">
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>
                                        <asp:DropDownList ID="ddlBatchName" runat="server" CssClass="input">
                                            <asp:ListItem Selected="True">---Select Batch---</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td>
                                        <asp:DropDownList ID="dlShift" CssClass="input" runat="server" ClientIDMode="Static" AutoPostBack="false">
                                            <asp:ListItem>---Select Shift---</asp:ListItem>
                                            <asp:ListItem>All</asp:ListItem>
                                            <asp:ListItem>Morning</asp:ListItem>
                                            <asp:ListItem>Day</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>

                                    <td>
                                        <asp:Button ID="btnSearch" CssClass="btn btn-primary" Text="Search" ClientIDMode="Static" runat="server"
                                            OnClick="btnSearch_Click" />
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                        <br />
                    </div>
                    <br />
                </div>
                <div class="row">
                    <div class="col-md-3">
                        <div class="listTab">
                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                <ContentTemplate>
                                    <div class="tgPanelHead">All Student List</div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div style="overflow: auto">
                                <asp:CheckBoxList ID="chkbl" runat="server" CssClass="listStyle" ClientIDMode="Static"></asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="listTab">
                            <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                <ContentTemplate>
                                    <div class="">
                                        <div class="tgPanelHead">Add Group</div>
                                    </div>
                                    <div id="divGroup" runat="server">
                                        <div class="depertment_button">
                                            <asp:Button ID="btnScience" runat="server" Text="Science"
                                                CssClass="promotion_button" OnClientClick="return validateInputs();" OnClick="btnScience_Click" />
                                        </div>
                                        <div class="depertment_button">
                                            <asp:Button ID="btnCommerce" runat="server" Text="Commerce"
                                                CssClass="promotion_button" OnClientClick="return validateInputs();" OnClick="btnCommerce_Click" />
                                        </div>
                                        <div class="depertment_button">
                                            <asp:Button ID="btnAtrs" runat="server" Text="Arts"
                                                CssClass="promotion_button" OnClientClick="return validateInputs();" OnClick="btnAtrs_Click" />
                                        </div>
                                        <div class="depertment_button">
                                            <asp:Button ID="btnBack" runat="server" Text="Back"
                                                CssClass="promotion_button" OnClientClick="return validateInputs();" OnClick="btnBack_Click" />
                                        </div>
                                        <div class="depertment_button">
                                            <asp:Button ID="btnSubmit" runat="server" Text="Submit"
                                                CssClass="btn btn-primary promotion_button" OnClientClick="return validateInputs();" OnClick="btnSubmit_Click" />
                                        </div>
                                    </div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="listTab">
                            <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                <ContentTemplate>
                                    <div class="tgPanelHead">Science<asp:Label ID="lblScienceStudent" runat="server"></asp:Label></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div style="overflow: auto">
                                <asp:CheckBoxList ID="ChkblScience" CssClass="listStyle" runat="server" ClientIDMode="Static"></asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="listTab">
                            <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                                <ContentTemplate>
                                    <div class="tgPanelHead" style="color: white">Commerce<asp:Label ID="lblCommerceStudent" runat="server"></asp:Label></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div style="overflow: auto">
                                <asp:CheckBoxList ID="ChkblCommerce" CssClass="listStyle" runat="server" ClientIDMode="Static"></asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-2">
                        <div class="listTab">
                            <asp:UpdatePanel ID="UpdatePanel5" runat="server">
                                <ContentTemplate>
                                    <div class="tgPanelHead">Arts<asp:Label ID="lblArtsStudent" runat="server"></asp:Label></div>
                                </ContentTemplate>
                            </asp:UpdatePanel>
                            <div style="overflow: auto">
                                <asp:CheckBoxList ID="ChkblArts" CssClass="listStyle" runat="server" ClientIDMode="Static"></asp:CheckBoxList>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            $("#ChkblScience").click(function () {
                //alert('Te');
                //var val1 = this.value;
                //alert(val1);
                //var val2 = $('#ddlSession').val();
                //var val3 = val1 + "" + val2;
                //$('#lblbatch').html(val3);
            });
        });
        function validateInputs() {
            if (validateText('txtDepartment', 1, 50, 'Enter Department Name') == false) return false;
            return true;
        }
        function editDepartment(Did) {
            $('#lblDepartmentId').val(Did);

            var strDepartment = $('#r_' + Did + ' td:first-child').html();
            $('#txtDepartment').val(strDepartment);
            var strS = $('#r_' + Did + ' td:nth-child(2)').html();
            if (strS == 'True') {
                $("#chkStatus").removeProp('checked');
                $("#chkStatus").click();
            }
            else {
                $("#chkStatus").removeProp('checked');
            }
            $("#btnSave").val('Update');
        }
    </script>
</asp:Content>
