<%@ Page Title="" Language="C#" MasterPageFile="~/DSWS.Master" AutoEventWireup="true" CodeBehind="admission-form.aspx.cs" Inherits="DS.UI.DSWS.admission_form" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <meta charset="utf-8">
    <meta http-equiv="X-UA-Compatible" content="IE=edge">
    <title>Registration View</title>
    <link rel="stylesheet" href="">
    <link href="https://fonts.googleapis.com/css2?family=Open+Sans:ital,wght@0,400;0,600;0,700;1,300;1,400;1,600&display=swap" rel="stylesheet">
    <script src="https://cdnjs.cloudflare.com/ajax/libs/html2pdf.js/0.9.2/html2pdf.bundle.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jspdf/2.1.1/jspdf.umd.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/jquery/3.7.1/jquery.min.js"></script>

    <style>
        body {
            font-family: 'Open Sans', sans-serif;
            font-size: 13px;
            color: #000;
            background-color: #fff;
        }

        h4 {
            font-weight: 600;
            font-size:14px;
        }

        .watermark-img {
            display: none;
        }

        .table-row table tr td {
        }

        table, th, td {
            border-collapse: collapse;
            padding: 1px 2px;
            color: #000;
            border: 1px solid #000 !important;
        }

        .ofc-copy {
            padding-top: 15px;
        }

        .page-break {
            page-break-after: always;
        }
        .divform{
            margin-top:20px;
        }
        
    </style>
    <script type="text/javascript">

        //  function printDiv() {

        //    var divToPrint = document.getElementById('page-wrapper1');

        //    var newWin = window.open('', 'Print-Window');
        //    newWin.document.open();
        //    newWin.document.write('<html><body onload="window.print()">' + divToPrint.innerHTML + '</body></html>');
        //    newWin.document.close();
        //    setTimeout(function () { newWin.close(); }, 10);

        //}


        window.onload = function () {
            document.getElementById("btnDownload")
                .addEventListener("click", () => {
                    const invoice = this.document.getElementById("page-wrapper1");
                    console.log(invoice);
                    console.log(window);
                    var opt = {
                        margin: 0,
                        filename: 'admission form.pdf',
                        image: { type: 'jpeg', quality: 1 },
                        html2canvas: { scale: 2 },
                        jsPDF: { unit: 'in', format: 'a4', orientation: 'portrait' }
                    };
                    html2pdf().from(invoice).set(opt).save();
                    // html2pdf().from(invoice).save();

                    // const doc = new jsPDF();
                    // var HTMLelement = $(".divform").html();
                    // doc.fromHTML(HTMLelement);
                    //// doc.text("Hello world!", 10, 10);
                    // doc.save("a4.pdf");
                })
        }

    </script>
    <script>
        function generatePDFHTML(divPrint) {


            var mywindow = window.open('', 'PRINT', 'height=400,width=600');

            mywindow.document.write('<html><head><title></title>');
            mywindow.document.write('<link rel="stylesheet" href="./assets/css/bootstrap/dist/css/bootstrap.css">');
            mywindow.document.write('<link href="./styles.css" rel="stylesheet">');

            mywindow.document.write('<style>');

            mywindow.document.write('@media print{');
            mywindow.document.write('.page-break{page-break-after: always;}');
            mywindow.document.write('h4{font-size:16px}');
            mywindow.document.write('table {border: 1px solid #000 !important;-moz-border: 1px solid #000 !important;}');
            mywindow.document.write('th, td {border: 1px solid #000 !important;-moz-border: 1px solid #000 !important;padding:0px 2px}');
            mywindow.document.write('}');
            mywindow.document.write('</style>');
            mywindow.document.write('</head><body style="background-color: #fff !important;" >');
            mywindow.document.write(document.getElementById(divPrint).innerHTML);
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

            //$(".hidePrint").css("display", "block");

            return true;
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div runat="server" id="divForm">
        <div id="page-wrapper1" class="divform">
            <div id="AdmPrintableArea">
                <div style="padding: 0 10px">
                    <style>
                        table, th, td {
                            border-collapse: collapse;
                            padding: 1px 2px;
                            color: #000;
                            border: 1px solid #000 !important;
                        }
                    </style>
                    <!-- A496dpi= 794 x 1123 -->
                    <div class="reg-view-main" style="width: 775px; margin: 3px auto; background: url(/websitedesign/assets/images/ic-wtr.png); background-size: 300px; color: #000; background-repeat: no-repeat; background-position-y: center; background-position-x: center;">
                        <div class="table-row">
                            <div style="text-align: center;">
                                <h4 style="font-size: 14px; margin: 3px;">Admission Form(Student Copy)</h4>
                            </div>
                        </div>
                        <div class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: center;">
                                        <p style="margin: 1px;">
                                            <img src="/websitedesign/assets/images/logo.png" style="height: 80px;" alt="">
                                        </p>
                                    </td>
                                    <td style="text-align: center;">
                                        <h1 style="margin: 0 0 4px; font-size: 24px; font-weight: 700;">Govt. Islampur College</h1>
                                        <h5 style="margin: 0; font-size: 16px;">Islampur, Jamalpur</h5>
                                        <h5 style="margin: 0; font-size: 14px; font-weight: 500;">EIIN: 109857  | Mobile :  +88 01768595800</h5>
                                    </td>
                                    <td style="text-align: center;">
                                        <p style="margin: 1px;">
                                            <img runat="server" id="imgStudent" src="" alt="" style="height: 80px;">
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="table-row" style="background: #fff; margin-top: 5px">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 35%;">Money Receipt No : <strong>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblMoneyReceiptNo"></asp:Label></strong></td>
                                    <td style="width: 30%">Roll No : <strong></strong></td>
                                    <td style="text-align: right;">Class: <strong>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblClass"></asp:Label></strong>
                                        
                                        Session : <strong> <asp:Label runat="server" ClientIDMode="Static" ID="lblSession"></asp:Label></strong>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td >Admission Form No: <strong>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblAdmissionFormNo"></asp:Label></strong>
                                    </td>
                                    <td >
                                         Board Admission Roll : <strong>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblNuAdmissionRoll"></asp:Label></strong>

                                        

                                    </td>
                                    <td style="text-align: right;">Admission  Date: <strong>
                                        
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblAdmissionDate"></asp:Label></strong>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="title" style="text-align: center;">
                            <h4 style="margin: 3px">Student Information (শিক্ষার্থীর তথ্য)</h4>
                        </div>
                        <div class="table-row">
                            <table style="width: 100%;">

                                <tr>
                                    <td style="background: #fff; width: 115px">Student Name</td>
                                    <td style="text-transform: uppercase;">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblStudentsName"></asp:Label></td>
                                    <td style="background: #fff; width: 115px">ছাত্র/ছাত্রীর নাম</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblStudentsNameBn"></asp:Label></td>
                                </tr>
                                <tr>

                                    <td>Group</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblGroup"></asp:Label></td>
                                    <td>Gender</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblGender"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Date of Birth </td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblDateOfBirth"></asp:Label></td>
                                    <td>Religion</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblReligion"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Student Mobile </td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblStudentMobile"></asp:Label></td>
                                    <td>Blood Group</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblBloodGroup"></asp:Label></td>
                                </tr>
                            </table>
                        </div>
                        <div runat="server" visible="false" class="title" style="text-align: center;">
                            <h4 style="margin: 3px">Parents Information (পিতা-মাতার তথ্য)</h4>
                        </div>
                        <div runat="server" visible="false" class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 140px">Father's Name</td>
                                    <td style="text-transform: uppercase;">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblFathersName"></asp:Label></td>
                                    <td style="width: 140px">পিতার নাম</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblFathersNameBn"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 140px">Father's Occupation</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblFathersOccupation"></asp:Label></td>
                                    <td style="width: 140px">পিতার পেশা</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblFathersOccupationBn"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Mother's Name</td>
                                    <td style="text-transform: uppercase;">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblMothersName"></asp:Label></td>
                                    <td>মাতার নাম</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblMothersNameBn"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Mother's Occupation</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblMothersOccupation"></asp:Label></td>
                                    <td>মাতার পেশা</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblMothersOccupationBn"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Village</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblParentsVillage"></asp:Label></td>
                                    <td>গ্রাম</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblParentsVillageBn"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="width: 155px">Post Office </td>
                                    <td style="width: 240px;">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblParentsPostOffice"></asp:Label></td>
                                    <td style="width: 155px">ডাকঘর</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblParentsPostOfficeBn"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Upazila</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblParentsUpazila"></asp:Label></td>
                                    <td>উপজেলা </td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblParentsUpazilaBn"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>District</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblParentsDistrict"></asp:Label></td>
                                    <td>জেলা </td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblParentsDistrictBn"></asp:Label></td>
                                </tr>

                            </table>
                        </div>
                        <div  class="title" style="text-align: center;">
                            <h4 style="margin: 3px">Guardian Information (অভিভাবকের তথ্য)</h4>
                        </div>
                        <div  class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td>Guardian Name</td>
                                    <td style="">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblGuardianName"></asp:Label></td>
                                    <td>Relation</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblGuardianRelation"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Mobile No</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblGuardianMobile"></asp:Label></td>
                                    <td>Address</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblGuardianAddress"></asp:Label></td>
                                </tr>

                            </table>
                        </div>
                        <div class="title" style="text-align: center;">
                            <h4 style="margin: 3px">Permanent address (স্থায়ী ঠিকানা)</h4>
                        </div>
                        <div class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td>Village</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPermanentVillage"></asp:Label></td>
                                    <td>গ্রাম</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPermanentVillageBn"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="background: #fff; width: 155px">Post Office </td>
                                    <td style="width: 240px;">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPermanentPostOffice"></asp:Label></td>
                                    <td style="width: 155px">ডাকঘর</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPermanentPostOfficeBn"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Upazila</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPermanentUpazila"></asp:Label></td>
                                    <td>উপজেলা </td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPermanentUpazilaBn"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>District</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPermanentDistrict"></asp:Label></td>
                                    <td>জেলা </td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPermanentDistrictBn"></asp:Label></td>
                                </tr>

                            </table>
                        </div>
                        <div class="title" style="text-align: center;">
                            <h4 style="margin: 3px">Present address (বর্তমান ঠিকানা)</h4>
                        </div>
                        <div class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td>Village</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPresentVillage"></asp:Label></td>
                                    <td>গ্রাম</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPresentVillageBn"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="background: #fff; width: 155px">Post Office </td>
                                    <td style="width: 240px;">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPresentPostOffice"></asp:Label></td>
                                    <td style="background: #fff; width: 155px">ডাকঘর</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPresentPostOfficeBn"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Upazila</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPresentUpazila"></asp:Label></td>
                                    <td>উপজেলা </td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPresentUpazilaBn"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>District</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPresentDistrict"></asp:Label></td>
                                    <td>জেলা </td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPresentDistrictBn"></asp:Label></td>
                                </tr>

                            </table>
                        </div>
                        <div class="title" style="text-align: center;">
                            <h4 style="margin: 3px">SSC Information (এসএসসি তথ্য)</h4>
                        </div>
                        <div class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td>School Name</td>
                                    <td style="">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreSchoolName"></asp:Label></td>
                                    <td>Board</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreBoard"></asp:Label></td>

                                </tr>
                                <tr>
                                    <td>Registration</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreRegistration"></asp:Label></td>
                                    <td>Roll</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreRoll"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Passing Year</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPrePassingYear"></asp:Label>
                                    </td>
                                    <td>GPA</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreGPA"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        

                         <div runat="server" id="divHscInfo1">
                        <div class="title" style="text-align: center;">
                            <h4 style="margin: 3px">HSC Information (এইচএসসি তথ্য)</h4>
                        </div>
                        <div class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td>College Name</td>
                                    <td style="">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreSchoolNameHSC"></asp:Label></td>
                                    <td>Board</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreBoardHSC"></asp:Label></td>

                                </tr>
                                <tr>
                                    <td>Registration</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreRegistrationHSC"></asp:Label></td>
                                    <td>Roll</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreRollHSC"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Passing Year</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPrePassingYearHSC"></asp:Label>
                                    </td>
                                    <td>GPA</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreGPAHSC"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        </div>
                        <div runat="server" id="divHonorsInfo1">
                        <div class="title" style="text-align: center;">
                            <h4 style="margin: 3px">Honours /Preliminary Information</h4>
                        </div>
                        <div class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td>College Name</td>
                                    <td style="">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreSchoolNameHonours"></asp:Label></td>
                                    <td>Board</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreBoardHonours"></asp:Label></td>

                                </tr>
                                <tr>
                                    <td>Registration</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreRegistrationHonours"></asp:Label></td>
                                    <td>Roll</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreRollHonours"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Passing Year</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPrePassingYearHonours"></asp:Label>
                                    </td>
                                    <td>CGPA</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreGPAHonours"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        </div>

                        <div class="title" style="text-align: center;">
                            <h4 style="margin: 3px">TC Information </h4>
                        </div>
                        <div class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="background: #fff; width: 155px">College Name</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblTCCollege"></asp:Label></td>
                                    <td style="background: #fff; width: 80px">Date</td>
                                    <td>
                                        <asp:Label runat="server" Style="min-width: 150px" ClientIDMode="Static" ID="lblTCDate"></asp:Label></td>
                                </tr>
                            </table>
                        </div>
                        <asp:Panel runat="server" ClientIDMode="Static" ID="pnlSubjectList">
                            <div class="title" style="text-align: center;">
                                <h4 style="margin: 3px">Subject Assaign</h4>
                            </div>
                            <div class="table-row">
                                <table style="width: 100%;">
                                    <tr>
                                        <th style="background: #fff; width: 30px">SN</th>
                                        <th>Subject Name & Code</th>
                                        <th style="background: #fff; width: 30px">SN</th>
                                        <th>Subject Name & Code</th>
                                        <th style="background: #fff; width: 30px">SN</th>
                                        <th>Subject Name & Code</th>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">01</td>
                                        <td>Bangla (101)</td>
                                        <td style="text-align: center;">02</td>
                                        <td>English (106)</td>
                                        <td style="text-align: center;">03</td>
                                        <td>ICT (275)</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">04</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblManSub1"></asp:Label>
                                            
                                        </td>
                                        <td style="text-align: center;">05</td>
                                        <td>
                                             <asp:Label runat="server" ID="lblManSub2"></asp:Label>
                                           
                                        </td>
                                        <td style="text-align: center;">06</td>
                                        <td>
                                             <asp:Label runat="server" ID="lblManSub3"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">07</td>
                                        <td colspan="5">4<sup>th</sup> Subject :
                                            <asp:Label runat="server" ID="lblOptSubject"></asp:Label>
                                        </td>
                                    </tr>

                                </table>
                            </div>
                        </asp:Panel>
                        <div class="table-row fixed-footer" style="width: 100%; margin-top: 35px;">
                            <span style="width: 32%; float: left; border-top: 1px solid #000; text-align: center; margin-right: 20px">Guardian Signature</span>
                            <span style="width: 32%; float: left; border-top: 1px solid #000; text-align: center; margin-right: 20px">Student Signature</span>
                            <span style="width: 30%; float: left; border-top: 1px solid #000; text-align: center;">Principal Sign & Seal</span>
                        </div>

                    </div>

                    <%------------------------ Office copy-------------------%>
                    <%-- <div class="watermark-img" style="position: absolute;z-index: 0;opacity: .09;;left:28%;top:30%">
		<p><img src="/websitedesign/assets/images/logo.png" style="width: 350px;" alt=""></p>
	</div>--%>
                    <div class="page-break"></div>
                    <!-- A496dpi= 794 x 1123 -->
                    <div class="reg-view-main ofc-copy" style="width: 775px; margin: 3px auto; color: #000; background: url(/websitedesign/assets/images/ic-wtr.png); background-size: 300px; color: #000; background-repeat: no-repeat; background-position-y: center; background-position-x: center;">
                        <div class="table-row">
                            <div style="text-align: center;">
                                <h4 style="font-size: 14px; margin: 3px;">Admission Form(Office Copy)</h4>
                            </div>
                        </div>
                        <div class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="text-align: center;">
                                        <p style="margin: 1px;">
                                            <img src="/websitedesign/assets/images/logo.png" style="height: 80px;" alt="">
                                        </p>
                                    </td>
                                    <td style="text-align: center;">
                                        <h1 style="margin: 0 0 4px; font-size: 24px; font-weight: 700;">Govt. Islampur College</h1>
                                        <h5 style="margin: 0; font-size: 16px;">Islampur, Jamalpur</h5>
                                        <h5 style="margin: 0; font-size: 14px; font-weight: 500;">EIIN: 109857  | Mobile :  +88 01768595800</h5>
                                    </td>
                                    <td style="text-align: center;">
                                        <p style="margin: 1px;">
                                            <img runat="server" id="imgStudent1" src="" alt="" style="height: 80px;">
                                        </p>
                                    </td>
                                </tr>
                            </table>
                        </div>

                        <div class="table-row" style="background: #fff; margin-top: 5px">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="width: 35%;">Money Receipt No : <strong>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblMoneyReceiptNo1"></asp:Label></strong></td>
                                    <td style="width: 30%">Roll No :</td>
                                    <td style="text-align: right;">Class: <strong>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblClass1"></asp:Label></strong>                                         
                                        Session : <strong><asp:Label runat="server" ClientIDMode="Static" ID="lblSession1"></asp:Label></strong>
                                       
                                    </td>
                                </tr>
                                <tr>
                                    <td >Admission Form No:<strong>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblAdmissionFormNo1"></asp:Label></strong>
                                    </td>
                                    <td >
                                         Board Admission Roll : <strong>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblNuAdmissionRoll1"></asp:Label></strong>
</td>
                                    <td style="text-align: right;">Admission  Date: <strong>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblAdmissionDate1"></asp:Label></strong>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div class="title" style="text-align: center;">
                            <h4 style="margin: 3px">Student Information (শিক্ষার্থীর তথ্য)</h4>
                        </div>
                        <div class="table-row">
                            <table style="width: 100%;">

                                <tr>
                                    <td style="width: 115px">Student Name</td>
                                    <td style="text-transform: uppercase;">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblStudentsName1"></asp:Label></td>
                                    <td style="width: 115px">ছাত্র/ছাত্রীর নাম</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblStudentsNameBn1"></asp:Label></td>
                                </tr>
                                <tr>

                                    <td>Group</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblGroup1"></asp:Label></td>
                                    <td>Gender</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblGender1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Date of Birth </td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblDateOfBirth1"></asp:Label></td>
                                    <td>Religion</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblReligion1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Student Mobile </td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblStudentMobile1"></asp:Label></td>
                                    <td>Blood Group</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblBloodGroup1"></asp:Label></td>
                                </tr>
                            </table>
                        </div>
                        <div runat="server" visible="false" class="title" style="text-align: center;">
                            <h4 style="margin: 3px">Parents Information (পিতা-মাতার তথ্য)</h4>
                        </div>
                        <div runat="server" visible="false" class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="background: #fff; width: 140px">Father's Name</td>
                                    <td style="text-transform: uppercase;">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblFathersName1"></asp:Label></td>
                                    <td style="background: #fff; width: 140px">পিতার নাম</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblFathersNameBn1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="background: #fff; width: 140px">Father's Occupation</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblFathersOccupation1"></asp:Label></td>
                                    <td style="background: #fff; width: 140px">পিতার পেশা</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblFathersOccupationBn1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Mother's Name</td>
                                    <td style="text-transform: uppercase;">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblMothersName1"></asp:Label></td>
                                    <td>মাতার নাম</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblMothersNameBn1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Mother's Occupation</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblMothersOccupation1"></asp:Label></td>
                                    <td>মাতার পেশা</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblMothersOccupationBn1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Village</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblParentsVillage1"></asp:Label></td>
                                    <td>গ্রাম</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblParentsVillageBn1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="background: #fff; width: 155px">Post Office </td>
                                    <td style="width: 240px;">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblParentsPostOffice1"></asp:Label></td>
                                    <td style="width: 155px">ডাকঘর</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblParentsPostOfficeBn1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Upazila</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblParentsUpazila1"></asp:Label></td>
                                    <td>উপজেলা </td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblParentsUpazilaBn1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>District</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblParentsDistrict1"></asp:Label></td>
                                    <td>জেলা </td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblParentsDistrictBn1"></asp:Label></td>
                                </tr>

                            </table>
                        </div>
                        <div class="title" style="text-align: center;">
                            <h4 style="margin: 3px">Guardian Information (অভিভাবকের তথ্য)</h4>
                        </div>
                        <div class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td>Guardian Name</td>
                                    <td style="">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblGuardianName1"></asp:Label></td>
                                    <td>Relation</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblGuardianRelation1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Mobile No</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblGuardianMobile1"></asp:Label></td>
                                    <td>Address</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblGuardianAddress1"></asp:Label></td>
                                </tr>

                            </table>
                        </div>
                        <div class="title" style="text-align: center;">
                            <h4 style="margin: 3px">Permanent address (স্থায়ী ঠিকানা)</h4>
                        </div>
                        <div class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td>Village</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPermanentVillage1"></asp:Label></td>
                                    <td>গ্রাম</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPermanentVillageBn1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="background: #fff; width: 155px">Post Office </td>
                                    <td style="width: 240px;">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPermanentPostOffice1"></asp:Label></td>
                                    <td style="width: 155px">ডাকঘর</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPermanentPostOfficeBn1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Upazila</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPermanentUpazila1"></asp:Label></td>
                                    <td>উপজেলা </td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPermanentUpazilaBn1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>District</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPermanentDistrict1"></asp:Label></td>
                                    <td>জেলা </td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPermanentDistrictBn1"></asp:Label></td>
                                </tr>

                            </table>
                        </div>
                        <div class="title" style="text-align: center;">
                            <h4 style="margin: 3px">Present address (বর্তমান ঠিকানা)</h4>
                        </div>
                        <div class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td>Village</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPresentVillage1"></asp:Label></td>
                                    <td>গ্রাম</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPresentVillageBn1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td style="background: #fff; width: 155px">Post Office </td>
                                    <td style="width: 240px;">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPresentPostOffice1"></asp:Label></td>
                                    <td style="background: #fff; width: 155px">ডাকঘর</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPresentPostOfficeBn1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Upazila</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPresentUpazila1"></asp:Label></td>
                                    <td>উপজেলা </td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPresentUpazilaBn1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>District</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPresentDistrict1"></asp:Label></td>
                                    <td>জেলা </td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPresentDistrictBn1"></asp:Label></td>
                                </tr>

                            </table>
                        </div>
                        <div class="title" style="text-align: center;">
                            <h4 style="margin: 3px">SSC Information (এসএসসি তথ্য)</h4>
                        </div>
                        <div class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td>School Name</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreSchoolName1"></asp:Label></td>
                                    <td>Board</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreBoard1"></asp:Label></td>

                                </tr>
                                <tr>
                                    <td>Registration</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreRegistration1"></asp:Label></td>
                                    <td>Roll</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreRoll1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Passing Year</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPrePassingYear1"></asp:Label>
                                    </td>
                                    <td>GPA</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreGPA1"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                       <div runat="server" id="divHscInfo">

                        <div class="title" style="text-align: center;">
                            <h4 style="margin: 3px">HSC Information (এইচএসসি তথ্য)</h4>
                        </div>
                        <div class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td>College Name</td>
                                    <td style="">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreSchoolNameHSC1"></asp:Label></td>
                                    <td>Board</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreBoardHSC1"></asp:Label></td>

                                </tr>
                                <tr>
                                    <td>Registration</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreRegistrationHSC1"></asp:Label></td>
                                    <td>Roll</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreRollHSC1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Passing Year</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPrePassingYearHSC1"></asp:Label>
                                    </td>
                                    <td>GPA</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreGPAHSC1"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                           </div>
                        <div runat="server" id="divHonorsInfo">
                        <div class="title" style="text-align: center;">
                            <h4 style="margin: 3px">Honours /Preliminary Information</h4>
                        </div>
                        <div class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td>College Name</td>
                                    <td style="">
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreSchoolNameHonours1"></asp:Label></td>
                                    <td>Board</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreBoardHonours1"></asp:Label></td>

                                </tr>
                                <tr>
                                    <td>Registration</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreRegistrationHonours1"></asp:Label></td>
                                    <td>Roll</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreRollHonours1"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td>Passing Year</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPrePassingYearHonours1"></asp:Label>
                                    </td>
                                    <td>CGPA</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblPreGPAHonours1"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>

                     </div>

                        <div class="title" style="text-align: center;">
                            <h4 style="margin: 3px">TC Information </h4>
                        </div>
                        <div class="table-row">
                            <table style="width: 100%;">
                                <tr>
                                    <td style="background: #fff; width: 155px">College Name</td>
                                    <td>
                                        <asp:Label runat="server" ClientIDMode="Static" ID="lblTCCollege1"></asp:Label></td>
                                    <td style="background: #fff; width: 80px">Date</td>
                                    <td>
                                        <asp:Label runat="server" Style="min-width: 150px" ClientIDMode="Static" ID="lblTCDate1"></asp:Label></td>
                                </tr>

                            </table>
                        </div>
                        <asp:Panel runat="server" ClientIDMode="Static" ID="pnlSubjectList1" >
                            <div class="title" style="text-align: center;">
                                <h4 style="margin: 3px">Subject Assaign</h4>
                            </div>
                            <div class="table-row">
                                <table style="width: 100%;">
                                    <tr>
                                        <th style="background: #fff; width: 30px">SN</th>
                                        <th>Subject Name & Code</th>
                                        <th style="background: #fff; width: 30px">SN</th>
                                        <th>Subject Name & Code</th>
                                        <th style="background: #fff; width: 30px">SN</th>
                                        <th>Subject Name & Code</th>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">01</td>
                                        <td>Bangla (101)</td>
                                        <td style="text-align: center;">02</td>
                                        <td>English (106)</td>
                                        <td style="text-align: center;">03</td>
                                        <td>ICT (275)</td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">04</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblManSub1_1"></asp:Label>
                                           
                                        </td>
                                        <td style="text-align: center;">05</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblManSub2_1"></asp:Label>
                                           
                                        </td>
                                        <td style="text-align: center;">06</td>
                                        <td>
                                            <asp:Label runat="server" ID="lblManSub3_1"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="text-align: center;">07</td>
                                        <td colspan="5">4<sup>th</sup> Subject :
                                            <asp:Label runat="server" ID="lblOptSubject1" ></asp:Label>
                                           
                                        </td>

                                    </tr>

                                </table>
                            </div>
                        </asp:Panel>
                        <div class="table-row fixed-footer" style="width: 100%; margin-top: 35px;">
                            <span style="width: 32%; float: left; border-top: 1px solid #000; text-align: center; margin-right: 20px">Guardian Signature</span>
                            <span style="width: 32%; float: left; border-top: 1px solid #000; text-align: center; margin-right: 20px">Student Signature</span>
                            <span style="width: 30%; float: left; border-top: 1px solid #000; text-align: center;">Principal Sign & Seal</span>
                        </div>

                    </div>

                </div>
            </div>
        </div>
    </div>
    <div class="col-md-12">
        <div style="text-align: center; line-height: 130px;">
            <span>
                <%--<button type='button' class='btn-lg btn-success print-button' id='btnDownload' onclick='printDiv();'>Download</button>--%>
                <button type='button' class='btn-lg btn-success print-button' id='btnDownload'><i class="fa fa-download"></i> Download</button>
            </span>
            <span>
                <%--<button type='button' class='btn-lg btn-success print-button' id='btnDownload' onclick='printDiv();'>Download</button>--%>
                <button type='button' onclick="generatePDFHTML('AdmPrintableArea')" class='btn-lg btn-primary '><i class="fa fa-print"></i> Print</button>
            </span> 
            <span>
              <a runat="server" id="aPayNow" href="javascript:void(0)" class="btn-lg btn-danger text-white" style=" padding: 10px 16px; border: 2px solid #a71111;margin-left:25px"  target="_blank"><i class="fa fa-money" aria-hidden="true"></i> Pay Now</a>   
            </span>
            <asp:Button runat="server" ClientIDMode="Static" Visible="false" ID="btnDowloadAsp" Text="download" OnClick="btnDowloadAsp_Click" />
        </div>
    </div>

   


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ForLeftSideMenuList" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="ForFoterSlider" runat="server">
</asp:Content>
