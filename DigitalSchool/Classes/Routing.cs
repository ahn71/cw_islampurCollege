using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace DS.Classes
{
    public static class Routing
    {
        //website->
        private static string IndexRouteName = "IndexRoute";
        public static string IndexRouteUrl = "index";
        private static string IndexRoutePhysicalFile = "~/Index.aspx";

        private static string BackgroundRouteName = "BackgroundRoute";
        public static string BackgroundRouteUrl = "about/background";
        private static string BackgroundRoutePhysicalFile = "~/UI/DSWS/background.aspx";

        private static string ChairmanMessageRouteName = "ChairmanMessageRoute";
        public static string ChairmanMessageRouteUrl = "about/chairman-message";
        private static string ChairmanMessageRoutePhysicalFile = "~/UI/DSWS/ChairmanMessage.aspx";

        private static string PrincipalMessageRouteName = "PrincipalMessageRoute";
        public static string PrincipalMessageRouteUrl = "about/principal-message";
        private static string PrincipalMessageRoutePhysicalFile = "~/UI/DSWS/PrincipalMessage.aspx";

        private static string GoverningBodyRouteName = "GoverningBodyRoute";
        public static string GoverningBodyRouteUrl = "about/governing-body";
        private static string GoverningBodyRoutePhysicalFile = "~/UI/DSWS/GoverningBody.aspx";

        private static string LabRouteName = "LabRoute";
        public static string LabRouteUrl = "facilities/lab";
        private static string LabRoutePhysicalFile = "~/UI/DSWS/Lab.aspx";

        private static string LibraryRouteName = "LibraryRoute";
        public static string LibraryRouteUrl = "facilities/library";
        private static string LibraryRoutePhysicalFile = "~/UI/DSWS/Library.aspx";

        private static string StudentsRouteName = "StudentsRoute";
        public static string StudentsRouteUrl = "students";
        private static string StudentsRoutePhysicalFile = "~/UI/DSWS/StudentContact.aspx";

        private static string TeachersRouteName = "TeachersRoute";
        public static string TeachersRouteUrl = "teachers";
        private static string TeachersRoutePhysicalFile = "~/UI/DSWS/TeacherContacts.aspx";

        private static string AcademicCalendarRouteName = "AcademicCalendar";
        public static string AcademicCalendarRouteUrl = "academic-calendar";
        private static string AcademicCalendarRoutePhysicalFile = "~/UI/DSWS/AcademicCalendar.aspx";

        private static string ExamScheduleRouteName = "ExamSchedule";
        public static string ExamScheduleRouteUrl = "exam-schedule";
        private static string ExamScheduleRoutePhysicalFile = "~/UI/DSWS/ExamSchedule.aspx";

        private static string ResultRouteName = "Result";
        public static string ResultRouteUrl = "result";
        private static string ResultRoutePhysicalFile = "~/UI/DSWS/Result.aspx";

        private static string ContactRouteName = "Contact";
        public static string ContactRouteUrl = "contact";
        private static string ContactRoutePhysicalFile = "~/UI/DSWS/Contact.aspx";

        private static string AdmissionFormRouteName = "AdmissionForm";
        public static string AdmissionFormRouteUrl = "admission-form";
        private static string AdmissionFormRoutePhysicalFile = "~/UI/DSWS/AdmissionForm.aspx";

        private static string AdmissionFormDownloadRouteName = "AdmissionFormDownload";
        public static string AdmissionFormDownloadRouteUrl = "admission-form/download/{id}";
        private static string AdmissionFormDownloadRoutePhysicalFile = "~/UI/DSWS/admission-form.aspx";

        private static string PaymentTermsAndConditionsRouteName = "OnlinePaymentTermsAndConditions";
        public static string PaymentTermsAndConditionsRouteUrl = "payment/terms-conditions";
        private static string PaymentTermsAndConditionsRoutePhysicalFile = "~/UI/DSWS/TermsAndConditions.aspx";

        private static string PaymentRouteName = "OnlinePayment";
        public static string PaymentRouteUrl = "payment";
        private static string PaymentRoutePhysicalFile = "~/UI/DSWS/Payment.aspx";

        private static string PaymentAdmissionRouteName = "OnlinePaymentAdmission";
        public static string PaymentAdmissionRouteUrl = "admission-payment/{id}";
        private static string PaymentAdmissionRoutePhysicalFile = "~/UI/DSWS/Payment.aspx";

        private static string PaymentOpenRouteName = "OnlinePaymentOpen";
        public static string PaymentOpenRouteUrl = "open-payment";
        private static string PaymentOpenRoutePhysicalFile = "~/UI/DSWS/Payment.aspx";

        private static string PaymentFailedRouteName = "OnlinePaymentFailed";
        public static string PaymentFailedRouteUrl = "payment/failed";
        private static string PaymentFailedRoutePhysicalFile = "~/UI/DSWS/PaymentFailed.aspx";

        private static string PaymentSuccessRouteName = "OnlinePaymentSuccess";
        public static string PaymentSuccessRouteUrl = "payment/success/{id}";
        private static string PaymentSuccessRoutePhysicalFile = "~/UI/DSWS/PaymentSuccess.aspx";


        private static string InvoiceRouteName = "Invoice";
        public static string InvoiceRouteUrl = "payment/invoice/{id}";
        private static string InvoiceRoutePhysicalFile = "~/UI/DSWS/Invoice.aspx";

        private static string AdmitCardRouteName = "AdmitCard";
        public static string AdmitCardRouteUrl = "admit-card";
        private static string AdmitCardRoutePhysicalFile = "~/UI/DSWS/admit-card.aspx";
        //website<-

        private static string LoginRouteName = "LoginRoute";
        public static string LoginRouteUrl = "login";
        private static string LoginRoutePhysicalFile = "~/UserLogin.aspx";

        private static string DashboardRouteName = "DashboardRoute";
        public static string DashboardRouteUrl = "dashboard";
        private static string DashboardRoutePhysicalFile = "~/Dashboard.aspx";

        private static string AcademicRouteName = "AcademicRoute";
        public static string AcademicRouteUrl = "dashboard/academic-module";
        private static string AcademicRoutePhysicalFile = "~/UI/Academic/AcademicHome.aspx";

        private static string AdministrationRouteName = "AdministrationRoute";
        public static string AdministrationRouteUrl = "dashboard/administration-module";
        private static string AdministrationRoutePhysicalFile = "~/UI/Administration/AdministrationHome.aspx";

        //....student start....
        private static string StudentHomeRouteName = "StudentHomeRoute";
        public static string StudentHomeRouteUrl = "student/home";
        private static string StudentHomeRoutePhysicalFile = "~/UI/Academic/Students/StdHome.aspx";

        private static string StudentAddRouteName = "StudentAddRoute";
        public static string StudentAddRouteUrl = "student/add";
        private static string StudentAddRoutePhysicalFile = "~/UI/Academic/Students/student-entry.aspx";

        private static string StudentEditRouteName = "StudentEditRoute";
        public static string StudentEditRouteUrl = "student/edit/{id}-{requestFrom}";
        private static string StudentEditRoutePhysicalFile = "~/UI/Academic/Students/student-entry.aspx";

        private static string StudentListRouteName = "StudentListRoute";
        public static string StudentListRouteUrl = "student/list";
        private static string StudentListRoutePhysicalFile = "~/UI/Academic/Students/CurrentStudentInfo.aspx";

        private static string StudentProfileRouteName = "StudentProfileRoute";
        public static string StudentProfileRouteUrl = "student/profile/{id}";
        private static string StudentProfileRoutePhysicalFile = "~/UI/Academic/Students/StdProfile.aspx";

        private static string StudentActivationRouteName = "StudentActivationRoute";
        public static string StudentActivationRouteUrl = "student/activation";
        private static string StudentActivationRoutePhysicalFile = "~/UI/Academic/Students/StdActivation.aspx";

        private static string StudentAdmissionApprovalRouteName = "StudentAdmissionApprovalRoute";
        public static string StudentAdmissionApprovalRouteUrl = "student/admission-approval";
        private static string StudentAdmissionApprovalRoutePhysicalFile = "~/UI/Academic/Students/admission-approval.aspx";

        private static string StudentSectionChangeRouteName = "StudentSectionChangeRoute";
        public static string StudentSectionChangeRouteUrl = "student/section-change";
        private static string StudentSectionChangeRoutePhysicalFile = "~/UI/Academic/Students/StdSectionChange.aspx";

        private static string StudentPromotionRouteName = "StudentPromotionRoute";
        public static string StudentPromotionRouteUrl = "student/promotion";
        private static string StudentPromotionRoutePhysicalFile = "~/UI/Academic/Students/StdPromotion.aspx";
        //....student end....
        //.... Examination ->....
        private static string ExaminationHomeRouteName = "ExaminationHomeRoute";
        public static string ExaminationHomeRouteUrl = "examination/home";
        private static string ExaminationHomeRoutePhysicalFile = "~/UI/Academic/Examination/ExamHome.aspx";

        private static string ExamTypeRouteName = "ExamTypeRoute";
        public static string ExamTypeRouteUrl = "examination/type";
        private static string ExamTypeRoutePhysicalFile = "~/UI/Academic/Examination/AddExam.aspx";

        private static string QuestionPatternRouteName = "QuestionPatternAddRoute";
        public static string QuestionPatternRouteUrl = "examination/question-pattern";
        private static string QuestionPatternRoutePhysicalFile = "~/UI/Academic/Examination/QuestionPattern.aspx";

        private static string SubjectQuestionPatternRouteName = "SubjectQuestionPatternRoute";
        public static string SubjectQuestionPatternRouteUrl = "examination/subject-question-pattern";
        private static string SubjectQuestionPatternRoutePhysicalFile = "~/UI/Academic/Examination/SubjectQuestionPattern.aspx";

        private static string ExamInfoRouteName = "ExamInfoAddRoute";
        public static string ExamInfoRouteUrl = "examination/info";
        private static string ExamInfoRoutePhysicalFile = "~/UI/Academic/Examination/ExamInfo.aspx";

        private static string ExamRoutineRouteName = "ExamRoutineRoute";
        public static string ExamRoutineRouteUrl = "examination/routine";
        private static string ExamRoutineRoutePhysicalFile = "~/UI/Academic/Examination/ExamRoutine.aspx";

        private static string ExamineeSelectionRouteName = "ExamineeSelectionRoute";
        public static string ExamineeSelectionRouteUrl = "examination/examinee-selection";
        private static string ExamineeSelectionRoutePhysicalFile = "~/UI/Academic/Examination/ExamineeSelection.aspx";

        private static string ExamGradingRouteName = "ExamGradingRoute";
        public static string ExamGradingRouteUrl = "examination/grading";
        private static string ExamGradingRoutePhysicalFile = "~/UI/Academic/Examination/Grading.aspx";

        private static string ExamMarksEntryRouteName = "ExamMarksEntryRoute";
        public static string ExamMarksEntryRouteUrl = "examination/mark-entry";
        private static string ExamMarksEntryRoutePhysicalFile = "~/UI/Academic/Examination/MarksEntryPanel.aspx";

        private static string ExamMarksEntryUpdateRouteName = "ExamMarksEntryUpdateRoute";
        public static string ExamMarksEntryUpdateRouteUrl = "examination/mark-entry/update";
        private static string ExamMarksEntryUpdateRoutePhysicalFile = "~/UI/Academic/Examination/ForUpdate.aspx";

        private static string ResultPublishRouteName = "ResultPublishRoute";
        public static string ResultPublishRouteUrl = "examination/result-publish";
        private static string ResultPublishRoutePhysicalFile = "~/UI/Academic/Examination/ResultPublish.aspx";
        //.... Examination -<....
        //.... Website Admin -<....
        private static string WSHomeRouteName = "WSHomeRoute";
        public static string WSHomeRouteUrl = "ws-admin/home";
        private static string WSHomeRoutePhysicalFile = "~/UI/Administration/DSWS/DSWSHome.aspx";

        private static string NoticeListRouteName = "NoticeListRoute";
        public static string NoticeListRouteUrl = "ws-admin/notice";
        private static string NoticeListRoutePhysicalFile = "~/UI/Administration/DSWS/Notice.aspx";

        private static string NoticeAddRouteName = "NoticeAddRoute";
        public static string NoticeAddRouteUrl = "ws-admin/notice/add";
        private static string NoticeAddRoutePhysicalFile = "~/UI/Administration/DSWS/AddNotice.aspx";

        private static string NoticeEditRouteName = "NoticeEditRoute";
        public static string NoticeEditRouteUrl = "ws-admin/notice/edit/{id}";
        private static string NoticeEditRoutePhysicalFile = "~/UI/Administration/DSWS/AddNotice.aspx";

        private static string AddSpeechesRouteName = "AddSpeechesRoute";
        public static string AddSpeechesRouteUrl = "ws-admin/add-speeches";
        private static string AddSpeechesRoutePhysicalFile = "~/UI/Administration/DSWS/AddPresidentSpeech.aspx";

        private static string SliderListRouteName = "SliderListRoute";
        public static string SliderListRouteUrl = "ws-admin/slider";
        private static string SliderListRoutePhysicalFile = "~/UI/Administration/DSWS/Slider.aspx";

        private static string SliderAddRouteName = "SliderAddRoute";
        public static string SliderAddRouteUrl = "ws-admin/slider/add";
        private static string SliderAddRoutePhysicalFile = "~/UI/Administration/DSWS/SliderAdd.aspx";

        private static string SliderEditRouteName = "SliderEditRoute";
        public static string SliderEditRouteUrl = "ws-admin/slider/edit/{id}";
        private static string SliderEditRoutePhysicalFile = "~/UI/Administration/DSWS/SliderAdd.aspx";

        private static string QuickLinkListRouteName = "QuickLinkListRoute";
        public static string QuickLinkListRouteUrl = "ws-admin/quicklink";
        private static string QuickLinkListRoutePhysicalFile = "~/UI/Administration/DSWS/QuickLink.aspx";

        private static string QuickLinkAddRouteName = "QuickLinkAddRoute";
        public static string QuickLinkAddRouteUrl = "ws-admin/quicklink/add";
        private static string QuickLinkAddRoutePhysicalFile = "~/UI/Administration/DSWS/AddQuickLink.aspx";

        private static string QuickLinkEditRouteName = "QuickLinkEditRoute";
        public static string QuickLinkEditRouteUrl = "ws-admin/quicklink/edit/{id}";
        private static string QuickLinkEditRoutePhysicalFile = "~/UI/Administration/DSWS/AddQuickLink.aspx";       

        private static string WSGeneralSettingsRouteName = "WSGeneralSettingsRoute";
        public static string WSGeneralSettingsRouteUrl = "ws-admin/general-settings";
        private static string WSGeneralSettingsRoutePhysicalFile = "~/UI/Administration/DSWS/website-general-settings.aspx";
        //.... Website Admin -<....

        public static void RegisterRoutes(RouteCollection routes)
        {
           
            //....website start....
            routes.MapPageRoute(IndexRouteName, IndexRouteUrl, IndexRoutePhysicalFile);
            routes.MapPageRoute(BackgroundRouteName, BackgroundRouteUrl, BackgroundRoutePhysicalFile);
            routes.MapPageRoute(ChairmanMessageRouteName, ChairmanMessageRouteUrl, ChairmanMessageRoutePhysicalFile);
            routes.MapPageRoute(PrincipalMessageRouteName, PrincipalMessageRouteUrl, PrincipalMessageRoutePhysicalFile);
            routes.MapPageRoute(GoverningBodyRouteName, GoverningBodyRouteUrl, GoverningBodyRoutePhysicalFile);

            routes.MapPageRoute(LabRouteName, LabRouteUrl,LabRoutePhysicalFile);
            routes.MapPageRoute(LibraryRouteName, LibraryRouteUrl, LibraryRoutePhysicalFile);
            routes.MapPageRoute(StudentsRouteName, StudentsRouteUrl, StudentsRoutePhysicalFile);
            routes.MapPageRoute(TeachersRouteName, TeachersRouteUrl, TeachersRoutePhysicalFile);
            routes.MapPageRoute(AcademicCalendarRouteName, AcademicCalendarRouteUrl, AcademicCalendarRoutePhysicalFile);
            routes.MapPageRoute(ExamScheduleRouteName, ExamScheduleRouteUrl, ExamScheduleRoutePhysicalFile);
            routes.MapPageRoute(ResultRouteName, ResultRouteUrl, ResultRoutePhysicalFile);
            routes.MapPageRoute(ContactRouteName, ContactRouteUrl, ContactRoutePhysicalFile);
            routes.MapPageRoute(AdmissionFormRouteName, AdmissionFormRouteUrl, AdmissionFormRoutePhysicalFile);
            routes.MapPageRoute(AdmissionFormDownloadRouteName, AdmissionFormDownloadRouteUrl, AdmissionFormDownloadRoutePhysicalFile);
            routes.MapPageRoute(PaymentTermsAndConditionsRouteName, PaymentTermsAndConditionsRouteUrl, PaymentTermsAndConditionsRoutePhysicalFile);
            routes.MapPageRoute(PaymentRouteName, PaymentRouteUrl, PaymentRoutePhysicalFile);
            routes.MapPageRoute(PaymentAdmissionRouteName, PaymentAdmissionRouteUrl, PaymentAdmissionRoutePhysicalFile);
            routes.MapPageRoute(PaymentOpenRouteName, PaymentOpenRouteUrl, PaymentOpenRoutePhysicalFile);
            routes.MapPageRoute(PaymentFailedRouteName,PaymentFailedRouteUrl,PaymentFailedRoutePhysicalFile);
            routes.MapPageRoute(PaymentSuccessRouteName, PaymentSuccessRouteUrl, PaymentSuccessRoutePhysicalFile);
            routes.MapPageRoute(InvoiceRouteName, InvoiceRouteUrl, InvoiceRoutePhysicalFile);
            routes.MapPageRoute(AdmitCardRouteName, AdmitCardRouteUrl, AdmitCardRoutePhysicalFile);
            //....website end....

            //....application start....
            routes.MapPageRoute(LoginRouteName, LoginRouteUrl, LoginRoutePhysicalFile);
            routes.MapPageRoute(DashboardRouteName, DashboardRouteUrl, DashboardRoutePhysicalFile);
            routes.MapPageRoute(AcademicRouteName, AcademicRouteUrl, AcademicRoutePhysicalFile);
            routes.MapPageRoute(AdministrationRouteName, AdministrationRouteUrl,AdministrationRoutePhysicalFile);
                    // student ->
            routes.MapPageRoute(StudentHomeRouteName, StudentHomeRouteUrl, StudentHomeRoutePhysicalFile);
            routes.MapPageRoute(StudentAddRouteName, StudentAddRouteUrl, StudentAddRoutePhysicalFile);
            routes.MapPageRoute(StudentEditRouteName, StudentEditRouteUrl, StudentEditRoutePhysicalFile);
            routes.MapPageRoute(StudentListRouteName, StudentListRouteUrl, StudentListRoutePhysicalFile);
            routes.MapPageRoute(StudentProfileRouteName, StudentProfileRouteUrl, StudentProfileRoutePhysicalFile);
            routes.MapPageRoute(StudentActivationRouteName, StudentActivationRouteUrl, StudentActivationRoutePhysicalFile);
            routes.MapPageRoute(StudentAdmissionApprovalRouteName, StudentAdmissionApprovalRouteUrl, StudentAdmissionApprovalRoutePhysicalFile);
            routes.MapPageRoute(StudentSectionChangeRouteName, StudentSectionChangeRouteUrl, StudentSectionChangeRoutePhysicalFile);
            routes.MapPageRoute(StudentPromotionRouteName, StudentPromotionRouteUrl, StudentPromotionRoutePhysicalFile);
            // student <-

            // examination >-
            routes.MapPageRoute(ExaminationHomeRouteName,ExaminationHomeRouteUrl,ExaminationHomeRoutePhysicalFile);
            routes.MapPageRoute(ExamTypeRouteName, ExamTypeRouteUrl, ExamTypeRoutePhysicalFile);
            routes.MapPageRoute(QuestionPatternRouteName, QuestionPatternRouteUrl, QuestionPatternRoutePhysicalFile);
            routes.MapPageRoute(SubjectQuestionPatternRouteName, SubjectQuestionPatternRouteUrl, SubjectQuestionPatternRoutePhysicalFile);
            routes.MapPageRoute(ExamInfoRouteName, ExamInfoRouteUrl, ExamInfoRoutePhysicalFile);
            routes.MapPageRoute(ExamRoutineRouteName, ExamRoutineRouteUrl, ExamRoutineRoutePhysicalFile);
            routes.MapPageRoute(ExamineeSelectionRouteName, ExamineeSelectionRouteUrl, ExamineeSelectionRoutePhysicalFile);
            routes.MapPageRoute(ExamGradingRouteName, ExamGradingRouteUrl, ExamGradingRoutePhysicalFile);
            routes.MapPageRoute(ExamMarksEntryRouteName, ExamMarksEntryRouteUrl, ExamMarksEntryRoutePhysicalFile);
            routes.MapPageRoute(ExamMarksEntryUpdateRouteName, ExamMarksEntryUpdateRouteUrl, ExamMarksEntryUpdateRoutePhysicalFile);
            routes.MapPageRoute(ResultPublishRouteName, ResultPublishRouteUrl, ResultPublishRoutePhysicalFile);
            // examination <-
            // website-admin ->
            routes.MapPageRoute(WSHomeRouteName, WSHomeRouteUrl, WSHomeRoutePhysicalFile);
            routes.MapPageRoute(NoticeListRouteName, NoticeListRouteUrl, NoticeListRoutePhysicalFile);
            routes.MapPageRoute(NoticeAddRouteName, NoticeAddRouteUrl, NoticeAddRoutePhysicalFile);
            routes.MapPageRoute(NoticeEditRouteName, NoticeEditRouteUrl, NoticeEditRoutePhysicalFile);
            routes.MapPageRoute(AddSpeechesRouteName, AddSpeechesRouteUrl, AddSpeechesRoutePhysicalFile);
            routes.MapPageRoute(SliderListRouteName, SliderListRouteUrl, SliderListRoutePhysicalFile);
            routes.MapPageRoute(SliderAddRouteName, SliderAddRouteUrl, SliderAddRoutePhysicalFile);
            routes.MapPageRoute(SliderEditRouteName, SliderEditRouteUrl, SliderEditRoutePhysicalFile);

            routes.MapPageRoute(QuickLinkListRouteName, QuickLinkListRouteUrl, QuickLinkListRoutePhysicalFile);
            routes.MapPageRoute(QuickLinkAddRouteName, QuickLinkAddRouteUrl, QuickLinkAddRoutePhysicalFile);
            routes.MapPageRoute(QuickLinkEditRouteName, QuickLinkEditRouteUrl, QuickLinkEditRoutePhysicalFile);

            routes.MapPageRoute(WSGeneralSettingsRouteName, WSGeneralSettingsRouteUrl, WSGeneralSettingsRoutePhysicalFile);
            // website-admin <-

            //....application end....
        }
    }
}