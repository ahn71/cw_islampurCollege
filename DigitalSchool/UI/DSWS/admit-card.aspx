<%@ Page Title="" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="admit-card.aspx.cs" Inherits="DS.UI.DSWS.admit_card" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/css/bootstrap.css" />
    <style>
        .main-content-inner {
            float: left;
            min-height: 468px;
            min-width: 70%;
        }

        .img-responsive {
            border: 1px solid #ddd;
            height: 130px;
            margin: 5px auto 0;
            padding: 4px;
        }

        .tblStudentInfo {
            font-family: 'Times New Roman';
            /*margin-left:10px;*/
            margin: 0px auto;
            /*min-width:50%;*/
            width: 60%;
        }

            .tblStudentInfo tr {
                border: 1px solid #ddd
            }

                .tblStudentInfo tr:nth-child(even) {
                    background: #fafafa;
                }

                .tblStudentInfo tr td {
                    padding: 5px;
                }

                    .tblStudentInfo tr td:first-child {
                        font-weight: bold;
                        color: #1CA59E;
                    }

        .divStdInfo {
            width: 100%;
            margin: 0px auto;
            /*border:1px solid silver;*/
        }

        .tblStudentInfo tr td:nth-child(4n) {
            font-weight: bold;
            color: #1CA59E;
        }

        .divStdInfo {
            width: 100%;
            margin: 0px auto;
            /*border:1px solid silver;*/
        }

        .divHeader {
            min-height: 100px;
            text-align: center;
        }

        .divTable {
            border: 1px solid red;
            width: auto;
            /*margin:0px auto;*/
        }

        .title {
            color: #1fb5ad;
            /*margin-left:10px;*/
            text-align: center;
        }

        .hMsg {
            color: red;
            text-align: center;
        }
        @media only screen and (max-width: 600px) {
            .tblStudentInfo {
                width: 100%;
                margin: auto;
            }
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ForLeftSideMenuList" runat="server">
    
        <asp:UpdatePanel ID="UpdatePanel1" runat="server">
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="btnShow" />
<%--                <asp:AsyncPostBackTrigger ControlID="btnSearch" />--%>
                <asp:AsyncPostBackTrigger ControlID="ddlYear" />
                <asp:AsyncPostBackTrigger ControlID="ddlClass" />
            </Triggers>
            <ContentTemplate>
                <div runat="server" class="main-content">

                    <!-- ================Start New Design Code ================ -->
                    <section>
                        <div class="container">
                            <div style="padding: 50px 0px">
                                <div class="row">
                                    <div class="col-md-12">
                                        <div class="main-content">
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="student-info-page-title text-center">
                                                        <h4 style="font-size: 24px; color: green;font-weight:600">ডাউনলোড এডমিট কার্ড </h4>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <div class="col-md-12">
                                                    <div class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4>তথ্য সার্চ করুন </h4>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="row">
                                                               <div class="col-md-2"></div>
                                                                <div class="col-md-2">
                                                                    <div class="form-group row">
                                                                        <label for="" class="col-md-12">Session Year</label>
                                                                        <div class="col-md-12">
                                                                            <asp:DropDownList runat="server" AutoPostBack="true" ClientIDMode="Static" ID="ddlYear" class="form-control" OnSelectedIndexChanged="ddlYear_SelectedIndexChanged"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <div class="form-group row">
                                                                        <label for="" class="col-md-12">Class</label>
                                                                        <div class="col-md-12">
                                                                            <asp:DropDownList runat="server" AutoPostBack="true" ClientIDMode="Static" ID="ddlClass" class="form-control" OnSelectedIndexChanged="ddlClass_SelectedIndexChanged"></asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-2">
                                                                    <div class="form-group row">
                                                                        <label for="" class="col-md-12">Exam</label>
                                                                        <div class="col-md-12">
                                                                            <asp:DropDownList ID="ddlExamList" runat="server" ClientIDMode="Static"  CssClass="input controlLength form-control">
                        </asp:DropDownList>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                
                                                                <div class="col-md-2">
                                                                    <div class="form-group row">
                                                                        <label for="" class="col-md-12">Class Roll</label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox runat="server" ID="txtAdmNo" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>
                                                                <div class="col-md-2" runat="server" visible="false">
                                                                    <div class="form-group row">
                                                                        <label for="" class="col-md-12">Payment Invoice No.                                        </label>
                                                                        <div class="col-md-12">
                                                                            <asp:TextBox runat="server" ID="txtInvoice" CssClass="form-control" ClientIDMode="Static"></asp:TextBox>
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                                <div class="col-md-2">
                                                                    <div class="form-group row">
                                                                        <label for="" class="col-md-12">&nbsp;</label>
                                                                        <div class="col-md-12">
                                                                            <asp:Button Visible="false" runat="server" Text="Preview" ClientIDMode="Static" ID="btnShow" CssClass="btn btn-primary" OnClick="btnShow_Click" />
                                                                            <asp:Button runat="server" Text="Search" ClientIDMode="Static" ID="btnSearch" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
                                                                        </div>
                                                                    </div>
                                                                </div>

                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="row">
                                                <h4 runat="server" id="hMessage" visible="false" class='hMsg'>
                                                    Student Not Found</h4>
                                                <div  class="divStdInfo" >
                                                    <body style="background-color: #ededed">
    <main style="width: 800px; margin: 0 auto; padding: 10px;background-color: #fff;" id="printtable">
        
        <div class="admit-card" runat="server" visible="false" id="divStudentAdmit" >
            <header class="payment-header" style="margin: 5px 0;padding: 5px;">
                <!-- <div class="text-center">
                    <h3 style="font-size: 24px">Payment Information</h3>
                </div> -->
                <table style="width: 100%">
                    <tr>
                        <td style="width: 120px">
                            <img style="width: 120px;"
                                src="../../websitedesign/assets/images/logo.png" alt="">
                        </td>
                        <td style="text-align: center">
                            <h2 style="margin: 0;text-transform: uppercase; font-weight:700;">Govt. Islampur College</h2>
                            <p style="margin-bottom: 0;">Islampur, Jamalpur</p>
                            <div><h4 style="font-size: 18px;margin: 0;" runat="server" id="hExamName"></h4></div>
                            <div style="margin-top: 18px"><h5 style="font-size: 20px"><u>Admit Card</u></h5></div>
                        </td>
                        <td style="text-align: right">

                        </td>
                    </tr>
                </table>
            </header>
            
            <section>
                
            <table style="width: 100%;">
                <tr>
                    <td>
                        <table class="table table-bordered">
                            <thead>                               
                                <tr>
                                    <th>Name</th>
                                    <td style="width: 5px;">:</td>
                                    <td><asp:Label runat="server" ClientIDMode="Static" ID="lblName"></asp:Label></td>
                                </tr>
                                 <tr>
                                    <th>Class</th>
                                    <td style="width: 5px;">:</td>
                                    <td><asp:Label runat="server" ClientIDMode="Static" ID="lblClass"></asp:Label></td>
                                </tr>
                                <tr>
                                    <th>Section</th>
                                    <td style="width: 5px;">:</td>
                                    <td><asp:Label runat="server" ClientIDMode="Static" ID="lblSection"></asp:Label></td>
                                </tr>                        
                            </thead>
                            <tbody>
        
                            </tbody>
                        </table>
                    </td>
                    <td style="width: 15px"></td>
                    <td>
                        <table class="table table-bordered">
                            <thead>
                                <tr>
                                    <th>Roll No</th>
                                    <td style="width: 5px;">:</td>
                                    <td><asp:Label runat="server" ClientIDMode="Static" ID="lblClassRoll"></asp:Label></td>
                                </tr>                               
                                <tr>
                                    <th>Group</th>
                                    <td style="width: 5px;">:</td>
                                    <td><asp:Label runat="server" ClientIDMode="Static" ID="lblGroup"></asp:Label></td>
                                </tr>
                                <tr>
                                    <th>Guardian Mobile No</th>
                                    <td style="width: 5px;">:</td>
                                    <td><asp:Label runat="server" ClientIDMode="Static" ID="lblGuardianMobileNo"></asp:Label></td>
                                </tr>                  
                                
                            </thead>
                            <tbody>
        
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
                <div runat="server" id="divExamRoutine">
                
                    </div>
            </section>
            <section>
                <table style="width: 100%;">
                    <tr>
                        <td style="text-align: center">
                            <p><img style="height: 60px;"  src="../../websitedesign/assets/images/principal.png" alt=""></p>
                            <strong>Seal and Signature of Principal</strong>
                        </td>
                        <td style="text-align: center">
                            <p><img style="height: 60px;" src="../../websitedesign/assets/images/examController.png" alt=""></p>
                            <strong>Controller of Examination</strong>
                        </td>
                    </tr>
                </table>
            </section>
            <hr>
            <section>
                <h4 style="text-align:center">পরীক্ষার্থীদের জন্য নির্দেশনাবলী</h4>
                <ul style="list-style: bengali;">
                    <li>প্রবেশপত্রে  কোন প্রকার ভুলত্রুুটি পরিলক্ষিত হলে তা পরীক্ষা শুরুর পূর্বেই সংশ্লিষ্ট শাখা  থেকে সংশোধন করে নিতে 
                    হবে। ভুল বা অসম্পূর্ণ প্রবেশপত্র দ্বারা কোনক্রমে পরীক্ষায় অংশ গ্রহণ করা যাবে না। এক্ষেত্রে পরীক্ষার্থী নিজে আবেদন 
                     ফরমে ভুল করলে নির্ধারিত ফি প্রদান করে প্রবেশপত্র সংশোধন  করে নিতে হবে। </li>
                    <li>কলেজ কর্তৃক নির্ধারিত দিন, তারিখ ও সময়সূচী অনুযায়ী পরীক্ষায় অংশ গ্রহণ করতে হবে। </li>
                    <li>পরীক্ষার দিনগুলিতে আধাঘণ্টা আগে খোলা হবে। পরীক্ষার হলে পরীক্ষার্থী ছাড়া অন্য কেহ প্রবেশ করতে পারবে না। </li>
                    <li>প্রবেশ পত্রে উল্লেখিত বিষয় ব্যতিত অন্য কোন বিষয়ে পরীক্ষায় অংশ গ্রহণ করা যাবে না। </li>
                    <li>মূল উত্তরপত্র ও ও.এম.আর ফরম এর নির্দিষ্ট স্থানে প্রয়োজনীয় সকল তথ্য নির্ভূলভাবে লিখতে হবে এবং সংশ্লিষ্ট বৃত্ত  ভরাট করতে হবে। </li>
                    <li> প্রতিটি পরীক্ষার জন্য পরীক্ষার্থীকে উপস্থিতি পত্রে স্বাক্ষর করতে হবে, অন্যথায় পরীক্ষায় অনুপস্থিত বলে গণ্য হবে। </li>
                    <li> পরীক্ষার হলে প্রবেশ পত্র, পরিচয় পত্র, কলম, পেন্সিল ব্যতিত অন্য কোন কিছু রাখা যাবে না। </li>
                    <li>মোবাইল ফোন নিয়ে পরীক্ষার হলে প্রবেশ করা সম্পূর্ণ নিষিদ্ধ।</li>
                    </ul>
            </section>
        </div>
    
    </main>
                                                        <div runat="server" visible="false" id="divPrintButton" style="text-align: center; margin-top: 20px">
        <a style="text-decoration:none" href="javascript:void(0)" onclick="PortraitPrintHTML()" class="btn-lg btn-primary"> Print Admit </a>


    </div>
                                                        <script src="extensions/print/bootstrap-table-print.js"></script>
    
    <script>
        function PortraitPrintHTML() {
            

      var mywindow = window.open('', 'PRINT', 'height=400,width=600');
      mywindow.document.write('<html><head><title></title>');
      mywindow.document.write('<link href="https://cdnjs.cloudflare.com/ajax/libs/twitter-bootstrap/3.3.7/css/bootstrap.css" rel="stylesheet">');
      mywindow.document.write('<style>');
      mywindow.document.write('@media print{');
      mywindow.document.write('@page {size:auto;-webkit-print-color-adjust: exact;}');
      mywindow.document.write('table {width:100%}');
      mywindow.document.write('table th{padding:2px !important;}');
      mywindow.document.write('table td{padding:2px !important;}');
      mywindow.document.write('.table-bordered th{vertical-align: middle!important;background: #ededed!important}');
      mywindow.document.write('.table-bordered th, .table-bordered td {border: 1px solid #000 !important;font-size:15px!important;color:#000!important}');
      mywindow.document.write('.print-report-header table td {font-size:18px!important;font-weight:500!important;}');
      mywindow.document.write('.table-footer td {background: #ededed!important}');
      mywindow.document.write('}');
      mywindow.document.write('</style>');
      mywindow.document.write('</head><body style="background-color: #fff !important;" >');
      mywindow.document.write(document.getElementById("printtable").innerHTML);
      mywindow.document.write('</body></html>');
      mywindow.document.close(); // necessary for IE >= 10
      mywindow.focus(); // necessary for IE >= 10*/

      setTimeout(function () {
        mywindow.print();
        var ival = setInterval(function () {
          mywindow.close();
          clearInterval(ival);
        }, 200);
      }, 500);
      return true;
    }
  </script>
</body>

                                                </div>

                                            </div>
                                            <%--<div class="row">
                                                <div class="col-md-12">
                                                    <div  class="panel panel-default">
                                                        <div class="panel-heading">
                                                            <h4>শিক্ষার্থীর তথ্য</h4>
                                                        </div>
                                                        <div class="panel-body">
                                                            <div class="row">
                                                                <div class="col-md-12">
                                                                    <div runat="server" id="ForLeftSideMenuList_divStudentContacts" class="divStdInfo">
                                                                        
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>--%>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </section>
                    <!-- ================End New Design Code ================ -->


                </div>
            </ContentTemplate>
        </asp:UpdatePanel>
    
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>
