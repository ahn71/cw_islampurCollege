<%@ Page Title="Shift Confuguration" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="ShiftConfiguration.aspx.cs" Inherits="DS.Forms.ShiftConfiguration" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContent" runat="server">
    <style>
        .tgPanel {
            width: 500px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">   
    <asp:UpdatePanel ID="uplMessage" runat="server">
        <ContentTemplate>
            <p class="message" id="lblMessage" clientidmode="Static" runat="server"></p>
        </ContentTemplate>
    </asp:UpdatePanel>
    <asp:HiddenField ID="hfShiftConfigId" ClientIDMode="Static" runat="server" />
    <div class="container">
        <div class="row">
            <div class="col-md-6">
                <p class="text-right">Shift Confuguration List</p>
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
                        <div id="divShiftList" class="datatables_wrapper" runat="server" 
                            style="width: 100%; height: auto; max-height: 500px; overflow: auto; overflow-x: hidden;"></div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Add Shift Configuration</div>
                    <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                            <table class="tbl-controlPanel">
                                <tr>
                                    <td>Shift Name
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtShiftName" runat="server" Width="261px" ClientIDMode="Static" CssClass="input"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Start Time
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlStartHour" runat="server" AutoPostBack="false" ClientIDMode="Static" CssClass="dropDownListRoutine" Width="85px">
                                            <asp:ListItem>  </asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlStartMinute" runat="server" AutoPostBack="false" ClientIDMode="Static" CssClass="dropDownListRoutine" Width="85px">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>26</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                            <asp:ListItem>28</asp:ListItem>
                                            <asp:ListItem>29</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlStartAMPM" runat="server" AutoPostBack="false" ClientIDMode="Static" CssClass="dropDownListRoutine" Width="85px">
                                            <asp:ListItem>AM</asp:ListItem>
                                            <asp:ListItem>PM</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Close Time
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlCloseHour" runat="server" AutoPostBack="false" ClientIDMode="Static" CssClass="dropDownListRoutine" Width="85px">
                                            <asp:ListItem>  </asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlCloseMinute" runat="server" AutoPostBack="false" ClientIDMode="Static" CssClass="dropDownListRoutine" Width="85px">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem>16</asp:ListItem>
                                            <asp:ListItem>17</asp:ListItem>
                                            <asp:ListItem>18</asp:ListItem>
                                            <asp:ListItem>19</asp:ListItem>
                                            <asp:ListItem>20</asp:ListItem>
                                            <asp:ListItem>21</asp:ListItem>
                                            <asp:ListItem>22</asp:ListItem>
                                            <asp:ListItem>23</asp:ListItem>
                                            <asp:ListItem>24</asp:ListItem>
                                            <asp:ListItem>25</asp:ListItem>
                                            <asp:ListItem>26</asp:ListItem>
                                            <asp:ListItem>27</asp:ListItem>
                                            <asp:ListItem>28</asp:ListItem>
                                            <asp:ListItem>29</asp:ListItem>
                                            <asp:ListItem>30</asp:ListItem>
                                            <asp:ListItem>35</asp:ListItem>
                                            <asp:ListItem>40</asp:ListItem>
                                            <asp:ListItem>45</asp:ListItem>
                                            <asp:ListItem>50</asp:ListItem>
                                            <asp:ListItem>55</asp:ListItem>
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="ddlCloseAMPM" runat="server" AutoPostBack="false" ClientIDMode="Static" CssClass="dropDownListRoutine" Width="85px">
                                            <asp:ListItem>AM</asp:ListItem>
                                            <asp:ListItem>PM</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Late Time
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlLateMinute" runat="server" AutoPostBack="false" ClientIDMode="Static" CssClass="dropDownListRoutine" Width="85px">
                                            <asp:ListItem>00</asp:ListItem>
                                            <asp:ListItem>01</asp:ListItem>
                                            <asp:ListItem>02</asp:ListItem>
                                            <asp:ListItem>03</asp:ListItem>
                                            <asp:ListItem>04</asp:ListItem>
                                            <asp:ListItem>05</asp:ListItem>
                                            <asp:ListItem>06</asp:ListItem>
                                            <asp:ListItem>07</asp:ListItem>
                                            <asp:ListItem>08</asp:ListItem>
                                            <asp:ListItem>09</asp:ListItem>
                                            <asp:ListItem>10</asp:ListItem>
                                            <asp:ListItem>11</asp:ListItem>
                                            <asp:ListItem>12</asp:ListItem>
                                            <asp:ListItem>13</asp:ListItem>
                                            <asp:ListItem>14</asp:ListItem>
                                            <asp:ListItem>15</asp:ListItem>
                                            <asp:ListItem></asp:ListItem>
                                        </asp:DropDownList>
                                        (AS Minute)
                                    </td>
                                </tr>
                                <tr>
                                    <td>&nbsp; Weekend</td>
                                    <td>
                                        <asp:DropDownList runat="server" Width="261px" ID="ddlDay" ClientIDMode="Static" CssClass="dropDownListRoutine" AutoPostBack="false">
                                            <asp:ListItem>  </asp:ListItem>
                                            <asp:ListItem>Saturday</asp:ListItem>
                                            <asp:ListItem>Sunday</asp:ListItem>
                                            <asp:ListItem>Monday</asp:ListItem>
                                            <asp:ListItem>Tuesday</asp:ListItem>
                                            <asp:ListItem>Wednesday</asp:ListItem>
                                            <asp:ListItem>Thursday</asp:ListItem>
                                            <asp:ListItem>Friday</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                            <div class="buttonBox">
                                <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" ClientIDMode="Static" Text="Save" 
                                    OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                <input id="tnReset" type="reset" value="Reset" class="btn btn-default" onclick="clearIt();" />
                            </div>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </div>
            </div>
        </div>        
    </div>    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptContent" runat="server">
    <script type="text/javascript">
        function validateInputs() {
            if (validateText('txtShiftName', 1, 30, 'Enter a Valid Shift Name') == false) return false;
            if (validateText('ddlStartHour', 1, 30, 'Please Select Start Houre') == false) return false;
            if (validateText('ddlCloseHour', 1, 30, 'Please Select Close Houre') == false) return false;
            if (validateText('ddlDay', 1, 30, 'Please Select Weekend day') == false) return false;
            return true;
        }
        function editShift(ConfigId) {
            $('#hfShiftConfigId').val(ConfigId);
            var strShift = $('#r_' + ConfigId + ' td:first-child').html();
            $('#txtShiftName').val(strShift);
            var startTime = $('#r_' + ConfigId + ' td:nth-child(2)').html();
            var startTimes = startTime.split(':');
            $('#ddlStartHour').val(startTimes[0]);
            $('#ddlStartMinute').val(startTimes[1]);
            startTimes = startTime.split(' ');
            $('#ddlStartAMPM').val(startTimes[1]);
            var closeTime = $('#r_' + ConfigId + ' td:nth-child(3)').html();
            var closeTimes = closeTime.split(':');
            $('#ddlCloseHour').val(closeTimes[0]);
            $('#ddlCloseMinute').val(closeTimes[1]);
            closeTimes = closeTime.split(' ');
            $('#ddlCloseAMPM').val(closeTimes[1]);
            $('#ddlLateMinute').val($('#r_' + ConfigId + ' td:nth-child(4)').html());
            $('#ddlDay').val($('#r_' + ConfigId + ' td:nth-child(5)').html());

            $("#btnSave").val('Update');
        }
        function clearIt() {
            $('#hfShiftConfigId').val('');
            $('#txtShiftName').val('');
            setFocus('txtShiftName');
            $('#ddlStartHour').val('');
            $('#ddlCloseHour').val('');
            $('#ddlDay').val('');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            showMessage('Updated successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
