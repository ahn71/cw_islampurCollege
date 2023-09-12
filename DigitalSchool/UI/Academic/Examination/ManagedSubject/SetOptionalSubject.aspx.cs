using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DS.DAL.AdviitDAL;
using DS.BLL.GeneralSettings;
using DS.BLL.ManagedClass;
using DS.PropertyEntities.Model.ManagedClass;
using System.Data;
using DS.BLL.ManagedBatch;
using DS.BLL.ManagedSubject;
using DS.PropertyEntities.Model.ManagedSubject;
using DS.PropertyEntities.Model.Admission;
using DS.PropertyEntities.Model.ManagedBatch;

namespace DS.UI.Academic.Students
{
    public partial class SetOptionalSubject : System.Web.UI.Page
    {
        DataTable dt;
        ClassGroupEntry clsgrpEntry;
        OptionalSubjectEntry opsubEntry;
        List<OptionalSubjectEntities> opsubList;
        List<ClassSubject> SubjectList;      
        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!IsPostBack)
                {
                    ShiftEntry.GetDropDownList(dlShift);
                    BatchEntry.GetDropdownlist(dlBatch,"True");                  
                }
            lblMessage.InnerText = "";
        }
        protected void dlBatch_SelectedIndexChanged(object sender, EventArgs e)
        {
            string[] batchClassId = dlBatch.SelectedValue.Split('_');
            if (clsgrpEntry == null)
            {
                clsgrpEntry = new ClassGroupEntry();
            }
            clsgrpEntry.GetDropDownList(int.Parse(batchClassId[1]), dlGroup);
        }
        protected void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                string[] batchClassId = dlBatch.SelectedValue.Split('_');
                SubjectList = ClassSubjectEntry.GetClassSubjectListByFiltering(int.Parse(batchClassId[1])).FindAll(c => c.IsOptional == true);
                List<SubjectEntities> AllList = new List<SubjectEntities>();
                for (int i = 0; i < SubjectList.Count; i++)
                {
                    SubjectEntities subentry = new SubjectEntities();
                    subentry.SubjectId = SubjectList[i].Subject.SubjectId;
                    subentry.SubjectName = SubjectList[i].Subject.SubjectName;
                    AllList.Add(subentry);
                }
                if (AllList.Count == 0)
                {
                    lblMessage.InnerText = "warning->Optional Subject Not Available";
                    return;
                }
                var DistinctItems = AllList.GroupBy(x => x.SubjectId).Select(y => y.First());
                if (DistinctItems.Count() == 1)
                {
                    if (opsubEntry == null)
                    {
                        opsubEntry = new OptionalSubjectEntry();
                    }
                    opsubList = opsubEntry.GetEntitiesData().FindAll
                (c => c.Shift.ShiftConfigId == int.Parse(dlShift.SelectedValue)
                && c.Batch.BatchId == int.Parse(batchClassId[0])
                && c.Group.GroupID == int.Parse(dlGroup.SelectedValue)
                && (c.OpBatchId == int.Parse(batchClassId[0])
                || c.OpBatchId == 0));
                    int subId = 0;
                    foreach (var item in DistinctItems)
                    {
                        subId = item.SubjectId;
                    }
                    for (int i = 0; i < opsubList.Count; i++)
                    {
                        if (opsubEntry == null)
                        {
                            opsubEntry = new OptionalSubjectEntry();
                        }
                        OptionalSubjectEntities opsubEntities = new OptionalSubjectEntities();
                        opsubEntities.Student = new CurrentStdEntities()
                        {
                            StudentID = opsubList[i].Student.StudentID
                        };
                        opsubEntities.Batch = new BatchEntities()
                        {
                            BatchId = opsubList[i].Batch.BatchId
                        };
                       
                        opsubEntities.Subject = new SubjectEntities()
                        {
                            
                            SubjectId =subId
                        };
                        opsubEntry.AddEntities = opsubEntities;
                        bool result = opsubEntry.Delete();
                        if (result == true)
                        {
                            opsubEntry.Insert();
                        }
                    }
                }
                if (opsubEntry == null)
                {
                    opsubEntry = new OptionalSubjectEntry();
                }
                opsubList = opsubEntry.GetEntitiesData().FindAll
                    (c=>c.Shift.ShiftConfigId==int.Parse(dlShift.SelectedValue)
                    && c.Batch.BatchId == int.Parse(batchClassId[0])
                    && c.Group.GroupID==int.Parse(dlGroup.SelectedValue)
                    &&(c.OpBatchId==int.Parse(batchClassId[0])
                    ||c.OpBatchId==0));
                            
                string divInfo = "";
                divInfo = " <table id='tblStudentInfo' class='display'  style='width:100%;margin:0px auto;' > ";
                divInfo += "<thead>";
                divInfo += "<tr>";
                divInfo += "<th class='numeric' style='width:50px;'>SL</th>";
                divInfo += "<th>Name</th>";
                divInfo += "<th class='numeric'>Roll</th>";
                divInfo += "<th class='numeric'>Section</th>";
                divInfo += "<th style='text-align: center;'>Choose Optional Subject</th>";
                divInfo += "</tr>";
                divInfo += "</thead>";
                if (opsubList.Count == 0)
                {
                    divInfo += "<td colspan='11'>No Student Available</td></table>";
                    divStduentInfo.Controls.Add(new LiteralControl(divInfo));
                    return;
                }
                divInfo += "<tbody>";
                int id=0;
                for (int x = 0; x < opsubList.Count; x++)
                {
                    id++;                  
                    divInfo += "<tr id='r_" + id + "'>";
                    divInfo += "<td class='numeric'>" + id + "</td>";
                    divInfo += "<td>" + opsubList[x].Student.FullName + "</td>";
                    divInfo += "<td class='numeric'>" + opsubList[x].Student.RollNo + "</td>";
                    divInfo += "<td class='numeric'>" + opsubList[x].Section.SectionName + "</td>";
                    divInfo += "<td class='text-center'><select onchange='setOS(this)' class='input' style='width: 180px;' id=OptionalSubjectInfo_SubId_" + opsubList[x].Student.StudentID + "_" + opsubList[x].Batch.BatchId + ">";
                    bool status = false;
                    foreach(var item in DistinctItems)
                    {
                        if (opsubList[x].Subject.SubjectId.Equals(item.SubjectId))
                        {
                            divInfo += "<option selected='selected' value=" + item.SubjectId + ">" + item.SubjectName + "</option>";
                            status = true;
                        }
                        else divInfo += "<option value=" + item.SubjectId + ">" + item.SubjectName + "</option>";                        
                    }
                    if (status != true)
                    {
                        divInfo += "<option value='0' selected='selected'>Select</option>";
                    }
                    else
                    {
                        divInfo += "<option value='0'>Select</option>";
                    }
                    divInfo += "</select></td></tr>";
                }
                divInfo += "</tbody>";
                divInfo += "<tfoot>";
                divInfo += "</table>";
                divInfo += "<div class='dataTables_wrapper'><div class='head'></div></div>";
                divStduentInfo.Controls.Add(new LiteralControl(divInfo));              
            }
            catch { }
        }      
    }
}