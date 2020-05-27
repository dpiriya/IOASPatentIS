using CrystalDecisions.CrystalReports.Engine;
using IOAS.GenericServices;
using IOAS.Infrastructure;
using IOAS.Models.PatentIS;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace IOAS.Controllers
{
    public class PatentISController : Controller
    {
        string strServer = ConfigurationManager.AppSettings["ServerName"].ToString();
        string strDatabase = ConfigurationManager.AppSettings["DataBaseName"].ToString();
        string strUserID = ConfigurationManager.AppSettings["UserId"].ToString();
        string strPwd = ConfigurationManager.AppSettings["Password"].ToString();
        // GET: PatentIS
        #region common
        [HttpPost]
        public JsonResult Getpiname(string Prefix, string source)
        {
            List<string> names = PatentIS.PINameList(Prefix, source);
            return Json(names, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region IDF Request
        public ActionResult IDFRequest(Int64 ReqNo = 0, string dno = null)
        {
            IDFRequestVM model = new IDFRequestVM();
            ViewBag.Idftype = PatentIS.IDFTypeList();
            ViewBag.pitype = PatentIS.GetPatentInventorType();
            ViewBag.dept = PatentIS.DepartmentList();
            model.Annex.ListIndustry = PatentIS.IndustryList();
            model.Annex.ListIndustry1 = PatentIS.IndustryList1();
            model.Annex.IITMode = PatentIS.CommericaliseMode();
            model.Annex.JointMode = PatentIS.CommericaliseMode1();           
            ViewBag.CRCat = PatentIS.CRCategory();
            ViewBag.Nature = PatentIS.CRNature();
            ViewBag.CRClass = PatentIS.CRClass();
            model.DesignClass.ClassList = PatentIS.DPClass();
            if (dno == null)
            {
                if (ReqNo == 0)
                {
                    var seqno = Convert.ToInt64(PatentIS.GetNewFileNo());
                    if (seqno > 0)
                    {
                        seqno += 1;
                        model.FileNo = seqno;
                        model.formUpdate = false;
                    }
                }
                else
                {
                    bool isOpen = PatentService.VerifyOpenStatus(ReqNo);
                    if(isOpen)
                    {
                        model.FileNo = ReqNo;                        
                        model = PatentService.EditIDFRequest(model);
                    }
                    else
                    {
                        TempData["FileNo"] = ReqNo;
                        return RedirectToAction("IDFReport");
                    }
                }
            }
            else
            {
                var seqno = Convert.ToInt64(PatentIS.GetNewFileNo());
                if (seqno > 0)
                {
                    seqno += 1;
                    model.FileNo = seqno;
                }
                model.DraftNo = dno;
                model = PatentService.EditDraft(model);
            }

            model.ListAction = PatentIS.ActionList();
            model.TMListAction = PatentIS.TMActionList();
            model.CRListAction = PatentIS.CRActionList();
            model.DPListAction = PatentIS.DesignActionList();
            model.Annex.ListStage = PatentIS.StageList();
            model.Trade.Catlist = PatentIS.TMCategory();
            if(model.IDFType!="DesignPatent")
                model.DesignClass.ClassList = PatentIS.DPClass();
            return View(model);

        }

        public JsonResult GetPIData(string type, string name)
        {
            object m = PatentIS.GetPIData(type, name);
            return Json(m, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult IDFRequest(IDFRequestVM idf, HttpPostedFileBase[] file)
        {
            IDFRequestVM model = new IDFRequestVM();
            ViewBag.Idftype = PatentIS.IDFTypeList();
            ViewBag.pitype = PatentIS.GetPatentInventorType();
            ViewBag.dept = PatentIS.DepartmentList();
            model.ListAction = PatentIS.ActionList();
            model.TMListAction = PatentIS.TMActionList();
            model.CRListAction = PatentIS.CRActionList();
            model.DPListAction = PatentIS.DesignActionList();
            model.Annex.ListStage = PatentIS.StageList();
            model.Annex.ListIndustry = PatentIS.IndustryList();
            model.Annex.ListIndustry1 = PatentIS.IndustryList1();
            model.Annex.IITMode = PatentIS.CommericaliseMode();
            model.Annex.JointMode = PatentIS.CommericaliseMode1();
            model.Trade.Catlist = PatentIS.TMCategory();
            model.DesignClass.ClassList = PatentIS.DPClass();
            ViewBag.CRCat = PatentIS.CRCategory();
            ViewBag.Nature = PatentIS.CRNature();
            ViewBag.CRClass = PatentIS.CRClass();
            if (idf.IDFType == "Trademark")
                idf.RequestedAction = idf.RequestedTMAction;
            else if (idf.IDFType == "Copyright")
                idf.RequestedAction = idf.RequestedCRAction;
            else if (idf.IDFType == "DesignPatent")
                idf.RequestedAction = idf.RequestedDPAction;
            if (idf.isDraft == "Form")
            {
                if (idf.formUpdate == false)    
                {
                    var seqno = Convert.ToInt64(PatentIS.GetNewFileNo());
                    if (seqno > 0)
                    {
                        seqno += 1;
                        idf.FileNo = seqno;                       
                        string IIDF = PatentService.InsertIDFRequest(idf, file, User.Identity.Name);
                        if (IIDF == "Success")
                        {
                            ViewBag.succMsg = "Your request has been created successfully with IDF Number - " + idf.FileNo + ".";
                            if (idf.DraftNo != null)
                            {
                                bool draft = PatentService.DeleteDraft(idf.DraftNo);
                            }

                            //MailMessage mail = new MailMessage();
                            //mail.To.Add("dpriyainfo@gmail.com");
                            //mail.From = new MailAddress("noreply@ioas.iitm.ac.in");
                            //mail.Subject= "Your request has been created successfully with IDF Number - " + idf.FileNo + ".";
                            //mail.Body = "Trial";
                            //TempData["FileNo"] = idf.FileNo;
                            //var report = new PatentISController().IDFReport();

                        }
                        else
                            ViewBag.errMsg = IIDF;
                    }
                    else
                        ViewBag.errMsg = "Error Connecting Database.Kindly Contact ICSR Computer Section -9741";

                }
                else
                {
                    string IIDF = PatentService.UpdateIDFRequest(idf, file, User.Identity.Name);
                    if (IIDF == "Success")
                    {
                        ViewBag.succMsg = "Your request has been created successfully with IDF Number - " + idf.FileNo + ".";

                    }
                    else
                        ViewBag.errMsg = IIDF;
                }
            }
            else if (idf.isDraft == "Draft")
            {
                if (idf.draftUpdate == false)
                {
                    var seqno = Convert.ToInt64(PatentIS.GetNewDraftNo());
                    if (seqno > 0)
                    {
                        seqno += 1;
                        idf.FileNo = seqno;
                        string IIDF = PatentService.InsertDraft(idf, User.Identity.Name);
                        if (IIDF == "Success")
                        {
                            ViewBag.succMsg = "Your request has been created successfully with draft Number - D" + idf.FileNo + ".";

                        }
                        else
                            ViewBag.errMsg = IIDF;
                    }
                    else
                        ViewBag.errMsg = "Error Connecting Database.Kindly Contact ICSR Computer Section -9741";

                }
                else
                {
                    string IIDF = PatentService.UpdateDraft(idf, User.Identity.Name);
                    if (IIDF == "Success")
                    {
                        ViewBag.succMsg = "Your request has been created successfully with Draft Number - " + idf.DraftNo + ".";
                
                    }
                    else
                        ViewBag.errMsg = IIDF;
                }
            }
             return View(model);
        }
        public ActionResult IDFRequestList()
        {
            return View();
        }
        [HttpGet]
        public JsonResult GetIDFRequestList()
        {
            try
            {
                object output = PatentService.GetIDFRequestDetails(User.Identity.Name);
                return Json(output, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public JsonResult GetIDFDraftList()
        {
            try
            {
                object output = PatentService.GetIDFDraftDetails(User.Identity.Name);
                return Json(output, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult DeleteDraft(string dno)
        {
                bool draft = PatentService.DeleteDraft(dno);
                return Json(draft, JsonRequestBehavior.AllowGet);            
        }
        public ActionResult FileLink(string f)
        {
            string mime = MimeMapping.GetMimeMapping(f);
            return File(f, mime);
        }
        public ActionResult IDFReport()
        {
            long fno = Convert.ToInt64(TempData["FileNo"]);

            List<IDFRequestReport> model = new List<IDFRequestReport>();
            ReportDocument rpt = new ReportDocument();
            rpt.FileName = Server.MapPath("~/Report/IDFRequest.rpt");
            //string conn = "PatentIS";
            
            for (int i = 0; i < rpt.DataSourceConnections.Count; i++)
                //rpt.DataSourceConnections[i].SetConnection("10.18.0.7", conn, "sa", "ICSR@123#");
                rpt.DataSourceConnections[i].SetConnection(strServer, strDatabase, strUserID, strPwd);
            model = PatentService.GenerateIDFReport(fno);

            if (model.Count > 0)
            {
                rpt.SetDataSource(model);                
                Stream stream = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
                stream.Seek(0, SeekOrigin.Begin);
                Response.AddHeader("Content-Disposition", "inline; filename=IDFRequest.pdf");
                return File(stream, "application/pdf");
            }
            else
            {
                return RedirectToAction("IDFReport", new { message = "No records found for this type of search entry" });
            }
        }

        #endregion

        //#region Mail
        //[HttpPost]       
        //[ValidateAntiForgeryToken]
        //public async Task<ActionResult> SendMail(MailModel mail)
        //{
        //    mail.ProposalList = mail.Proposals();
        //    mail.IdList = mail.IDS();
        //    string body = "<div style='font-family:Calibri;font-size: 10pt;'><p>Dear Project Investigator, </p><br/>" +
        //        "<p>Sub: In respect of Project Proposal Title: " + mail.ProposalTitle + "</p><br/>" +
        //        "<p> As per our IC & SR record, you have undertaken following agreements as witness/confirming party, which are still effective.</p><p> This is to request you to check that your new project proposal is not conflicting with the existing Agreement(s).</p>";
        //    body += "<table style='font-family:Calibri;font-size: 10pt;' border='1' style='border: 1px solid lightgrey;border-collapse: collapse; border-spacing: 0;' class='table table-striped table-bordered'><tr><th>SI.No</th><th>Industry Partner</th><th>Kind of Agreement</th><th>Effective Date</th><th>End Date</th><th>Remarks</th></tr>";
        //    foreach (var m in mail.Dean_trxList)
        //    {
        //        body += "<tr><td>" + m.Sno + "</td>" +
        //            "<td>" + m.Partner + "</td>" +
        //            "<td>" + m.Agreement_type + "</td>" +
        //            "<td>" + m.Signed_date + "</td>" +
        //            "<td>" + m.Expiry_date + "</td>" +
        //            "<td>" + m.Title + "</td>" +
        //            "</tr>";
        //    }
        //    body += "</table>";
        //    body += "<p>Best Regards</p>";
        //    body += "<p>Dean IC&SR</p>" +
        //        "<p>NOTE: This is a System generated mail, for any queries Contact Project Office IC&SR.</p></div>";
        //    var message = new MailMessage();
        //    message.To.Add(new MailAddress("cmit-icsr@iitm.ac.in"));
        //    message.CC.Add(new MailAddress("cmlegal-icsr@imail.iitm.ac.in"));
        //    message.From = new MailAddress("noreply@ioas.iitm.ac.in");
        //    message.Subject = "In respect of Project Proposal" + mail.ProposalNo + " Title: " + mail.ProposalTitle;
        //    message.Body = string.Format(body);
        //    message.IsBodyHtml = true;

        //    using (var smtp = new SmtpClient())
        //    {
        //        //var credential = new NetworkCredential
        //        //{
        //        //    UserName = "noreply@ioas.iitm.ac.in",
        //        //    Password = "welcomehbs"
        //        //};
        //        //smtp.Credentials = credential;
        //        //smtp.Host = "smtp.gmail.com";
        //        //smtp.Port = 587;
        //        //smtp.EnableSsl = true;
        //        await smtp.SendMailAsync(message);
        //        TempData["message"] = "Sent";
        //        return View("ProposalReport", mail);
        //        //return Json(new { success = true, title = "Mail Sent", message = "Mail sent to : " + message.To, JsonRequestBehavior.AllowGet });
        //    }
        //}
        //[HttpPost]       
        //public ActionResult Print(MailModel m)
        //{
        //    AgreementPrintDS ds = new AgreementPrintDS();
        //    Report.AgreementReport rpt = new Report.AgreementReport();
        //    rpt.Load();
        //    DataRow dr = ds.Tables["ProposalMail"].NewRow();
        //    dr[0] = m.ProposalNo;
        //    dr[1] = m.ProjectNo;
        //    dr[2] = m.FMail.EMPLOYEEID;
        //    dr[3] = m.ProposalTitle;
        //    dr[4] = m.FMail.DISPLAYNAME;
        //    dr[5] = m.FMail.DEPARTMENTNAME;
        //    ds.Tables["ProposalMail"].Rows.Add(dr);
        //    foreach (var r in m.Dean_trxList)
        //    {
        //        DataRow dr1 = ds.Tables["DeanModel"].NewRow();
        //        dr1[0] = r.Sno;
        //        dr1[1] = r.Partner;
        //        dr1[2] = r.Agreement_type;
        //        dr1[3] = r.Title;
        //        dr1[4] = r.Signed_date;
        //        dr1[5] = r.Expiry_date;
        //        ds.Tables["DeanModel"].Rows.Add(dr1);
        //    }
        //    rpt.SetDataSource(ds);
        //    Stream filestream = rpt.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    return File(filestream, "application/pdf");
        //}
        //#endregion


    }
}