using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TEPL.QDMS.WindowsService.Business;
using TEPL.QMS.Common;
using TEPL.QMS.Common.Constants;
using TEPL.QMS.Common.DAL;
using TEPL.QMS.DAL.Database.Component;

namespace TEPLQDMSWS
{
    public class ApproverReminder
    {
        public void GetApproverReminderDocuments()
        {
            WSAdminBLL objAdmin = new WSAdminBLL();
            DataTable dt = objAdmin.GetApprovalPendingDocuments();
            var grouped = from table in dt.AsEnumerable()
                          where table.Field<Int32>("PendingDays") > 2
                          group table by new { ApproverEmail = table["ApproverEmail"], ApproverName = table["ApproverName"] } into groupby
                          select new
                          {
                              Value = groupby.Key,
                              ColumnValues = groupby
                          };
            string strMailSubject = string.Empty;
            StringBuilder strMailBody = new StringBuilder();
            string toEmailid = string.Empty,ccEmail=string.Empty;
            foreach (var key in grouped)
            {
                Console.WriteLine(key.Value.ApproverEmail);
                Console.WriteLine("---------------------------");
                strMailBody.Append("Below are the documents waiting for your action");
                strMailBody.Append("<br/><br/>");
                strMailBody.Append("<style>#Approval table,#Approval th,#Approval td {border: 1px solid;border-collapse: collapse;padding:2px;}</style>");
                strMailBody.Append("<table id='Approval' ><tr><th>Document No</th><th>Requested User</th><th>Stage</th><th>Pending From(In days)</th></tr>");
                foreach (var columnValue in key.ColumnValues)
                {
                    strMailBody.Append("<tr><td>" + columnValue["DocumentNo"].ToString() + "</td><td>" + columnValue["UploadedUserName"].ToString() + "</td><td>" + columnValue["CurrentStage"].ToString() + "</td><td>" + columnValue["PendingDays"].ToString() + "</td></tr>");
                }
                strMailBody.Append("</table>");
                strMailBody.Append("<br/>");
                strMailBody.Append("Link to approve documents: <a style='text-decoration:underline' target='_blank' href='" +
                ConfigurationManager.AppSettings["websiteURL"].ToString() + "Inbox'>" + "Click here" + "</a>");
                toEmailid = key.Value.ApproverEmail.ToString();
                //toEmailid = "rajesh.m-ext@tataelectronics.co.in";
                //toEmailid = "siva.d-ext@tataelectronics.co.in";
                MailSend.PrepareandSendMail("ApprovalMailTemplate", key.Value.ApproverName.ToString(), toEmailid,ccEmail, "dms.support@tataelectronics.co.in", "Documents waiting for your action", "", strMailBody.ToString());
                strMailBody.Clear();
            }
        }
        public void GetApproverEsclationDocuments()
        {
            WSAdminBLL objAdmin = new WSAdminBLL();
            DataTable dt = objAdmin.GetApprovalPendingDocuments();
            string strExcelPath = QMSConstants.TempFolder +"ApprovalPendingDocuments.xlsx";
            string[] selectedColumns = new[] { "DepartmentName", "SectionName", "DocumentCategoryName", "DocumentNo", "DocumentDescription", "CurrentStage", "ApproverName", "PendingDate", "PendingDays" };
            dt.DefaultView.RowFilter = "PendingDays > 5";
            DataTable dt1 = dt.DefaultView.ToTable(false, selectedColumns);
            DataSet ds = new DataSet();
            ds.Tables.Add(dt1);
            dt1.TableName = "PendingDocuments";
            ExcelOperations.ExportDataSet(ds, strExcelPath);
            string strMailSubject = string.Empty;
            StringBuilder strMailBody = new StringBuilder();
            strMailBody.Append("Attached documents waiting for user action from various departments");
            strMailBody.Append("<br/><br/>");
            strMailBody.Append("Link to approve documents: <a style='text-decoration:underline' target='_blank' href='" +
            ConfigurationManager.AppSettings["websiteURL"].ToString() + "Inbox'>" + "Click here" + "</a>");
            MailSend.PrepareandSendMail("ApprovalMailTemplate", "Madhavi Shrivastava", "madhavi.s@tataelectronics.co.in","", "dms.support@tataelectronics.co.in", "Documents waiting for user action from various departments", strExcelPath, strMailBody.ToString());
            strMailBody.Clear();
            /*
          var grouped = from table in dt.AsEnumerable()
                        where table.Field<Int32>("PendingDays") > 5
                        group table by new { ApproverEmail = table["DepartmentHeadEmail"], ApproverName = table["DepartmentHeadDisplayName"] } into groupby
                        select new
                        {
                            Value = groupby.Key,
                            ColumnValues = groupby
                        };
          foreach (var key in grouped)
          {
              Console.WriteLine(key.Value.ApproverEmail);
              Console.WriteLine("---------------------------");
              strMailBody.Append("Below are the documents waiting for approval in your department");
              strMailBody.Append("<br/>");
              strMailBody.Append("<style>#Approval table,#Approval th,#Approval td {border: 1px solid;border-collapse: collapse;padding:2px;}</style>");
              strMailBody.Append("<table id='Approval' ><tr><th>Department</th><th>Document No</th><th>Requested User</th><th>Approval Stage</th><th>Approver Name</th><th>Pending From(In days)</th></tr>");
              foreach (var columnValue in key.ColumnValues)
              {
                  strMailBody.Append("<tr><td>" + columnValue["DepartmentName"].ToString() + "</td><td>" + columnValue["DocumentNo"].ToString() + "</td><td>" + columnValue["UploadedUserName"].ToString() + "</td><td>" + columnValue["CurrentStage"].ToString() + "</td><td>" + columnValue["ApproverName"].ToString() + "</td><td>" + columnValue["PendingDays"].ToString() + "</td></tr>");
              }
              strMailBody.Append("</table>");
              strMailBody.Append("<br/><br/>");
              strMailBody.Append("Link to approve documents: <a style='text-decoration:underline' target='_blank' href='" +
              ConfigurationManager.AppSettings["websiteURL"].ToString() + "Inbox'>" + "Click here" + "</a>");
              MailSend.PrepareandSendMail("ApprovalMailTemplate", key.Value.ApproverName.ToString(), "siva.d-ext@tataelectronics.co.in", "dms.support@tataelectronics.co.in", "Documents waiting for approval in your department", "", strMailBody.ToString());
              strMailBody.Clear();
          }*/
        }
    }
}
