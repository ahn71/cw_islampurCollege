<%@ Page Title="" Language="C#" MasterPageFile="~/main.Master" AutoEventWireup="true" CodeBehind="ShiftConfig.aspx.cs" Inherits="DS.UI.Academic.Timetable.SetTimings.ShiftConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .tgPanel {
            width: 100%;
        }
        .input{
            color:#000;
            
        }
        .tbl-controlPanel td:first-child{
            text-align: right;
            padding-right: 5px;
        }
          #ConfigId_length {
             display: none;
            padding: 15px;
        }
         #ConfigId_filter {
            display: none;
            padding: 15px;
        }
          #ConfigId_info {
             display: none;
            padding: 15px;
        }
         #ConfigId_paginate {
            display: none;
            padding: 15px;
        }
        .no-footer {
           border-bottom: 1px solid #ecedee !important;
        }
        @media (min-width: 320px) and (max-width: 480px) {
             .input{
            color:#000;
            margin-top:10px;
            
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
    <asp:HiddenField ID="hfShiftConfigId" ClientIDMode="Static" runat="server" />
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
                <li><a id="A1" runat="server" href="~/UI/Administration/AdministrationHome.aspx">Administration Module</a></li>
                <li><a id="A2" runat="server" href="~/UI/Administration/Settings/SettingsHome.aspx">System Settings Module</a></li>
                <li><a id="A3" runat="server" href="~/UI/Administration/Settings/GeneralSettings/GeneralSettingsHome.aspx">General Settings</a></li>
                <li class="active">Shift Configuration</li>
            </ul>
            <!--breadcrumbs end -->
        </div>
    </div>
    <div class="">
        <div class="row">
            <div class="col-md-6">
                <h4 class="text-right" style="float:left">Shift Configuration List</h4>
                <div class="dataTables_filter_New" style="float: right; margin-right: 0px;">
                    <label>
                        Search:
                         <input type="text" class="Search_New" placeholder="type here" />
                    </label>
                </div>                
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
                        <div class="tgPanel">
                        <div id="divShiftList" class="datatables_wrapper" runat="server" 
                            style="width: 100%; height: auto; max-height: 350px; overflow: auto; overflow-x: hidden;"></div>
                            </div>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>
            <div class="col-md-6">
                <div class="tgPanel">
                    <div class="tgPanelHead">Add Shift Configuration</div>
                    <asp:UpdatePanel ID="up3" runat="server" UpdateMode="Conditional">
                        <ContentTemplate>
                             <div class="row tbl-controlPanel">
                                <div class="col-sm-10 col-sm-offset-1">
                                               <div class="row tbl-controlPanel">
                                            <label class="col-sm-3"></label>
                                            <div class="col-sm-9">
                                             <asp:RadioButtonList ID="rblShiftType" ClientIDMode="Static" RepeatDirection="Horizontal"  runat="server">
                                            <asp:ListItem  Selected="True"  Value="False">Students</asp:ListItem>
                                             <asp:ListItem    Value="True">Teachers/Staff</asp:ListItem>
                                        </asp:RadioButtonList>
                                            </div>
                                            </div>
                                        <div class="row tbl-controlPanel">
                                            <label class="col-sm-3">Shift Name</label>
                                            <div class="col-sm-9">
                                                <asp:TextBox ID="txtShiftName" runat="server" ClientIDMode="Static" 
                                                CssClass="input controlLength form-control"></asp:TextBox>
                                            </div>
                                            </div>
                                        <div class="row tbl-controlPanel">
                                            <label class="col-sm-3">Start Time</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlStartHour" runat="server" AutoPostBack="false" ClientIDMode="Static" 
                                                    CssClass="input form-control">
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
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlStartMinute" runat="server" AutoPostBack="false" ClientIDMode="Static" 
                                                    CssClass="input form-control">
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
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlStartAMPM" runat="server" AutoPostBack="false" ClientIDMode="Static" 
                                                    CssClass="input form-control">
                                                    <asp:ListItem>AM</asp:ListItem>
                                                    <asp:ListItem>PM</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row tbl-controlPanel">
                                            <label class="col-sm-3">Close Time</label>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlCloseHour" runat="server" AutoPostBack="false" ClientIDMode="Static" 
                                                    CssClass="input form-control">
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
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlCloseMinute" runat="server" AutoPostBack="false" ClientIDMode="Static" 
                                                    CssClass="input form-control">
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
                                            </div>
                                            <div class="col-sm-3">
                                                <asp:DropDownList ID="ddlCloseAMPM" runat="server" AutoPostBack="false" ClientIDMode="Static" 
                                                    CssClass="input form-control">
                                                    <asp:ListItem>AM</asp:ListItem>
                                                    <asp:ListItem>PM</asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                        </div>
                                        <div class="row tbl-controlPanel">
                                            <label class="col-sm-3">Late Time</label>
                                            <div class="col-sm-3">
                                                 <asp:DropDownList ID="ddlLateMinute" runat="server" AutoPostBack="false" ClientIDMode="Static" 
                                                    CssClass="input form-control">
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
                                            </div>
                                            <div class="col-sm-3">
                                                (AS Minute)
                                            </div>
                                            <div class="col-sm-3">
            
                                            </div>
                                        </div>
                                    <div class="row tbl-controlPanel">
                                            <label class="col-sm-3">Absent Time</label>
                                            <div class="col-sm-3">
                                                 <asp:DropDownList ID="ddlAbsentTime" runat="server" AutoPostBack="false" ClientIDMode="Static" 
                                                    CssClass="input form-control">
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
                                                    <asp:ListItem>31</asp:ListItem>
                                                    <asp:ListItem>32</asp:ListItem>
                                                    <asp:ListItem>33</asp:ListItem>
                                                    <asp:ListItem>34</asp:ListItem>
                                                    <asp:ListItem>35</asp:ListItem>
                                                    <asp:ListItem>36</asp:ListItem>
                                                    <asp:ListItem>37</asp:ListItem>
                                                    <asp:ListItem>38</asp:ListItem>
                                                    <asp:ListItem>39</asp:ListItem>
                                                    <asp:ListItem>40</asp:ListItem>
                                                    <asp:ListItem>41</asp:ListItem>
                                                    <asp:ListItem>42</asp:ListItem>
                                                    <asp:ListItem>43</asp:ListItem>
                                                    <asp:ListItem>44</asp:ListItem>
                                                    <asp:ListItem>45</asp:ListItem>
                                                    <asp:ListItem>46</asp:ListItem>
                                                    <asp:ListItem>47</asp:ListItem>
                                                    <asp:ListItem>48</asp:ListItem>
                                                    <asp:ListItem>49</asp:ListItem>
                                                    <asp:ListItem>50</asp:ListItem>
                                                    <asp:ListItem>51</asp:ListItem>
                                                    <asp:ListItem>52</asp:ListItem>
                                                    <asp:ListItem>53</asp:ListItem>
                                                    <asp:ListItem>54</asp:ListItem>
                                                    <asp:ListItem>55</asp:ListItem>
                                                    <asp:ListItem>56</asp:ListItem>
                                                    <asp:ListItem>57</asp:ListItem>
                                                    <asp:ListItem>58</asp:ListItem>
                                                    <asp:ListItem>59</asp:ListItem>
                                                    <asp:ListItem>60</asp:ListItem>
                                                    <asp:ListItem></asp:ListItem>
                                                </asp:DropDownList>
                                            </div>
                                            <div class="col-sm-3">
                                                (AS Minute)
                                            </div>
                                            <div class="col-sm-3">
            
                                            </div>
                                        </div>
                                        <div class="row tbl-controlPanel">
                                            <div class="col-sm-offset-3 col-sm-9">
                                                 <asp:Button ID="btnSave" CssClass="btn btn-primary" runat="server" ClientIDMode="Static" Text="Save"
                                            OnClientClick="return validateInputs();" OnClick="btnSave_Click" />
                                        <input id="tnReset" type="reset" value="Reset" class="btn btn-default" onclick="clearIt();" />
                                            </div>
                                        </div>
                                </div>
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
        $(document).ready(function () {
            $(document).on("keyup", '.Search_New', function () {
                searchTable($(this).val(), 'ConfigId', '');
            });
            $('#ConfigId').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        });
        function loaddatatable() {
            $('#ConfigId').dataTable({
                "iDisplayLength": 10,
                "lengthMenu": [10, 20, 30, 40, 50, 100]
            });
        }
        function validateInputs() {
            if (validateText('txtShiftName', 1, 30, 'Enter a Valid Shift Name') == false) return false;
            if (validateText('ddlStartHour', 1, 30, 'Please Select Start Houre') == false) return false;
            if (validateText('ddlCloseHour', 1, 30, 'Please Select Close Houre') == false) return false;            
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
            $('#ddlAbsentTime').val($('#r_' + ConfigId + ' td:nth-child(5)').html());
            var SType = $('#r_' + ConfigId + ' td:nth-child(6)').html();
            if (SType=='Students')
                $('#<%=rblShiftType.ClientID %>').find("input[value='False']").prop("checked", true);
            else if (SType == 'Teachers/Staff')
                $('#<%=rblShiftType.ClientID %>').find("input[value='True']").prop("checked", true);
            $("#btnSave").val('Update');
        }
        function clearIt() {
            loaddatatable();
            $('#hfShiftConfigId').val('');
            $('#txtShiftName').val('');
            setFocus('txtShiftName');
            $('#ddlStartHour').val('');
            $('#ddlCloseHour').val('');
            $('#ddlLateMinute').val('');
            $('#ddlAbsentTime').val('');
            $("#btnSave").val('Save');
        }
        function updateSuccess() {
            loaddatatable();
            showMessage('Update successfully', 'success');
            clearIt();
        }
    </script>
</asp:Content>
