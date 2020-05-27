using IOAS.DataModel;
using IOAS.Models.PatentIS;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;

namespace IOAS.GenericServices
{
    public class PatentService
    {
        #region PatentIDFRequest
        public static IDFRequestVM EditIDFRequest(IDFRequestVM vm)
        {
            try
            {
                IDFRequestVM idf = new IDFRequestVM();
                using (var context = new PatentISEntities())
                {
                    var query = context.tblIDFRequest.Where(m => m.FileNo == vm.FileNo).FirstOrDefault();
                    if (query != null)
                    {
                        idf.FileNo = query.FileNo;
                        idf.IDFType = query.IDFType;
                        idf.FieldOfInvention = query.FieldOfInvention;
                        idf.BiologicalMaterial = query.BiologicalMaterial;
                        idf.Description = query.Description;
                        idf.DetailsOfBiologicalMaterial = query.DetailsOfBiologicalMaterial;
                        idf.Disclosure = query.Disclosure;
                        idf.FirstApplicantAddress = query.FirstApplicantAddress;
                        idf.FirstApplicantContactNo = query.FirstApplicantContactNo;
                        idf.FirstApplicantEmailId = query.FirstApplicantEmailId;
                        idf.FirstApplicantName = query.FirstApplicantName;
                        idf.FirstApplicantOrganisation = query.FirstApplicantOrganisation;
                        idf.FirstApplicantPosition = query.FirstApplicantPosition;
                        idf.PIContactNo = query.PIContactNo;
                        idf.PIDepartment = query.PIDepartment;
                        idf.PIEmailId = query.PIEmailId;
                        idf.PIInstId = query.PIInstId;
                        idf.PrimaryInventorType = query.PrimaryInventorType;
                        idf.PrimaryInventorName = query.PrimaryInventorName;
                        idf.PriorPublication = query.PriorPublication;
                        idf.RelevantInformation = query.RelevantInformation;
                        //idf.RequestedAction = query.RequestedAction;

                        if (idf.IDFType == "Trademark")
                            idf.RequestedTMAction = query.RequestedAction;
                        else if (idf.IDFType == "Copyright")
                        {
                            idf.RequestedCRAction = query.RequestedAction;
                            idf.RequestedCRtxtAction = query.RequestedActionOthers;
                        }
                        else if (idf.IDFType == "DesignPatent")
                        {
                            idf.RequestedDPAction = query.RequestedAction;
                            idf.RequestedDPtxtAction = query.RequestedActionOthers;                            
                        }
                        else
                        {
                            idf.RequestedAction = query.RequestedAction;
                            idf.RequestedtxtAction = query.RequestedActionOthers;
                        }
                        idf.SourceOfInvention = query.SourceOfInvention;
                        idf.Summary = query.Summary;
                        idf.SupportInformation = query.SupportInformation;
                        idf.Title = query.Title;
                        idf.formUpdate = true;
                        var anex = context.tblAnnexureB1.Where(m => m.FileNo == vm.FileNo).ToList();
                        foreach (var a in anex)
                        {
                            idf.Annex.BriefDescription = a.BriefDescription;
                            idf.Annex.Comments = a.Comments;
                            idf.Annex.DevelopmentStage = a.DevelopmentStage;
                            idf.Annex.L1Search = a.L1Search;
                            idf.Annex.OtherInfo = a.OtherInfo;
                            idf.Annex.Outcome = a.Outcome;
                            idf.Annex.Party = a.Party;
                            idf.Annex.Tool = a.Tool;
                        }
                        List<CoInventorVM> coinventor = new List<CoInventorVM>();
                        var coin = context.tblCoInventor.Where(m => m.FileNo == vm.FileNo).Select(m => m.SNo).ToList();
                        if (coin.Count > 0)
                        {
                            foreach (var a in coin)
                            {
                                var codetail = context.tblCoInventor.Where(m => m.SNo == a && m.FileNo == vm.FileNo).FirstOrDefault();
                                coinventor.Add(new CoInventorVM()
                                {
                                    FileNo = codetail.FileNo,
                                    SNo = codetail.SNo,
                                    Dept = codetail.Dept,
                                    Name = codetail.Name,
                                    Type = codetail.Type,
                                    Mail = codetail.EmailId,
                                    Ph = codetail.ContactNo,
                                    InstId = codetail.InstId
                                });
                            }
                            idf.CoIn = coinventor;
                        }
                        List<ApplicantVM> applicant = new List<ApplicantVM>();
                        var appln = context.tblApplicants.Where(m => m.FileNo == vm.FileNo).Select(m => m.Sno).ToList();
                        if (appln.Count > 0)
                        {
                            foreach (var a in appln)
                            {
                                var appdetail = context.tblApplicants.Where(m => m.Sno == a && m.FileNo == vm.FileNo).FirstOrDefault();
                                applicant.Add(new ApplicantVM()
                                {
                                    Sno = appdetail.Sno,
                                    Position = appdetail.Position,
                                    Address = appdetail.Address,
                                    ContactName = appdetail.ContactName,
                                    Organisation = appdetail.Organisation,
                                    EmailId = appdetail.EmailId,
                                    ContactNo = appdetail.ContactNo,
                                    FileNo = appdetail.FileNo
                                });
                            }
                            idf.Appl = applicant;
                        }
                        idf.Annex.ListIndustry = vm.Annex.ListIndustry;
                        idf.Annex.ListIndustry1 = vm.Annex.ListIndustry1;
                        var area = context.tblApplicationAreas.Where(m => m.FileNo == vm.FileNo).Select(m => new { m.Index, m.Category }).ToList();
                        if (area.Count > 0)
                        {
                            foreach (var a in area)
                            {
                                if (a.Category == "ApplicationIndustry")
                                {
                                    idf.Annex.ListIndustry[a.Index].Selected = true;
                                }
                                else
                                {
                                    idf.Annex.ListIndustry1[a.Index].Selected = true;
                                }
                            }
                        }
                        idf.Annex.IITMode = vm.Annex.IITMode;
                        idf.Annex.JointMode = vm.Annex.JointMode;
                        var mode = context.tblCommercialisationMode.Where(m => m.FileNo == vm.FileNo).Select(m => new { m.IndNo, m.Category }).ToList();
                        if (mode.Count > 0)
                        {
                            foreach (var a in mode)
                            {
                                if (a.Category == "IIT")
                                {
                                    idf.Annex.IITMode[a.IndNo].Selected = true;
                                }
                                else
                                {
                                    idf.Annex.JointMode[a.IndNo].Selected = true;
                                }
                            }
                        }
                        if (idf.IDFType == "Copyright")
                        {
                            var cr = context.tbl_Copyright.Where(m => m.FileNo == vm.FileNo).FirstOrDefault();
                            if (cr != null)
                            {
                                idf.CR.Category = cr.Category;
                                idf.CR.ClassofWork = cr.ClassofWork;
                                idf.CR.Description = cr.Description;
                                idf.CR.Details = cr.Details;
                                idf.CR.isPublished = cr.isPublished;
                                idf.CR.isRegistered = cr.isRegistered;
                                idf.CR.Language = cr.Language;
                                idf.CR.Nature = cr.Nature;
                                idf.CR.Original = cr.Original;
                                idf.CR.Title = cr.Title;
                            }
                            List<CRAuthorVM> authvm = new List<CRAuthorVM>();
                            var auth = context.tbl_CR_Author.Where(m => m.FileNo == vm.FileNo).Select(m => m.SNo).ToList();
                            if (auth.Count > 0)
                            {
                                foreach (var a in auth)
                                {
                                    var authdetail = context.tbl_CR_Author.Where(m => m.FileNo == vm.FileNo && m.SNo == a).FirstOrDefault();
                                    authvm.Add(new CRAuthorVM()
                                    {
                                        AUAddress = authdetail.AUAddress,
                                        AUName = authdetail.AUName,
                                        AUNationality = authdetail.AUNationality,
                                        isDeceased = authdetail.isDeceased,
                                        SNo = authdetail.SNo,
                                        deceasedDt = authdetail.deceasedDt
                                    });
                                }
                                idf.CR.Author = authvm;
                            }
                            List<CRPublishVM> pubvm = new List<CRPublishVM>();
                            var pub = context.tbl_CR_Publish.Where(m => m.FileNo == vm.FileNo).Select(m => m.Sno).ToList();
                            if (pub.Count > 0)
                            {
                                foreach (var a in pub)
                                {
                                    var pubdetail = context.tbl_CR_Publish.Where(m => m.FileNo == vm.FileNo && m.Sno == a).FirstOrDefault();
                                    pubvm.Add(new CRPublishVM()
                                    {
                                        Country = pubdetail.Country,
                                        PUAddress = pubdetail.PUAddress,
                                        PUName = pubdetail.PUName,
                                        PUNationality = pubdetail.PUNationality,
                                        Year = pubdetail.Year
                                    });
                                }
                                idf.CR.Publish = pubvm;
                            }
                        }
                        else if (idf.IDFType == "Trademark")
                        {
                            var tr = context.tbl_Trademark.Where(m => m.FileNo == vm.FileNo).FirstOrDefault();
                            if (tr != null)
                            {
                                idf.Trade.FileNo = vm.FileNo;
                                idf.Trade.Language = tr.Language;
                                idf.Trade.TMName = tr.TMName;
                                idf.Trade.TMImage = tr.TMImage;
                                idf.Trade.TMStatement = tr.TMStatement;
                                idf.Trade.Category = tr.Category;
                                idf.Trade.Class = tr.Class;
                                idf.Trade.Description = tr.Description;
                            }
                            List<TradeApplicantVM> tradevm = new List<TradeApplicantVM>();
                            var trade = context.tbl_Trade_Applicantdetail.Where(m => m.FileNo == vm.FileNo).Select(m => m.Sno).ToList();
                            if (trade.Count > 0)
                            {
                                foreach (var a in trade)
                                {
                                    var appdetail = context.tbl_Trade_Applicantdetail.Where(m => m.FileNo == vm.FileNo && m.Sno == a).FirstOrDefault();
                                    tradevm.Add(new TradeApplicantVM()
                                    {
                                        AddressOfService = appdetail.AddressOfService,
                                        Country = appdetail.Country,
                                        Organisation = appdetail.Organisation,
                                        Nature = appdetail.Nature,
                                        Sno = appdetail.Sno,
                                        Jurisdiction = appdetail.Jurisdiction,
                                        LegalStatus = appdetail.LegalStatus
                                    });
                                }
                                idf.Trade.TAppl = tradevm;
                            }
                        }
                        if (idf.IDFType == "DesignPatent")
                        {
                            idf.DesignClass.ClassList = vm.DesignClass.ClassList;
                            var dp = context.tbl_DesignClasses.Where(m => m.FileNo == vm.FileNo).Select(m =>m.Index).ToList();
                            if (dp.Count > 0)
                            {
                                foreach(var cl in dp)
                                {
                                    idf.DesignClass.ClassList[cl].Selected = true;
                                }
                            }                            
                        }
                        List<PatFilesVM> fvm = new List<PatFilesVM>();
                        var upfiles = context.tbl_files_PatentRequest.Where(m => m.FileNo == vm.FileNo).Select(m => m.DocId).ToList();
                        if (upfiles.Count > 0)
                        {
                            foreach (var v in upfiles)
                            {
                                var filedetail = context.tbl_files_PatentRequest.Where(m => m.DocId == v && m.FileNo == vm.FileNo).FirstOrDefault();
                                fvm.Add(new PatFilesVM()
                                {
                                    FileNo = vm.FileNo,
                                    DocId = filedetail.DocId,
                                    DocName = filedetail.DocName,
                                    DocPath = filedetail.DocPath
                                });
                            }
                            idf.Files = fvm;
                        }
                    }

                    return idf;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string InsertIDFRequest(IDFRequestVM model, HttpPostedFileBase[] file, string uname)
        {

            using (PatentISEntities pat = new PatentISEntities())
            {

                using (var transaction = pat.Database.BeginTransaction())
                {
                    try
                    {
                        //Master update                        
                        tblIDFRequest dis = new tblIDFRequest()
                        {
                            FileNo = model.FileNo,
                            IDFType = model.IDFType,
                            PrimaryInventorName = model.PrimaryInventorName,
                            PrimaryInventorType = model.PrimaryInventorType,
                            PIDepartment = model.PIDepartment,
                            PIContactNo = model.PIContactNo,
                            PIEmailId = model.PIEmailId,
                            PIInstId = model.PIInstId,
                            Remarks = model.Remarks,
                            FirstApplicantName = model.FirstApplicantName,
                            FirstApplicantPosition = model.FirstApplicantPosition,
                            FirstApplicantAddress = model.FirstApplicantAddress,
                            FirstApplicantContactNo = model.FirstApplicantContactNo,
                            FirstApplicantEmailId = model.FirstApplicantEmailId,
                            FirstApplicantOrganisation = model.FirstApplicantOrganisation,
                            Title = model.Title,
                            FieldOfInvention = model.FieldOfInvention,
                            Summary = model.Summary,
                            Description = model.Description,
                            PriorPublication = model.PriorPublication,
                            SourceOfInvention = model.SourceOfInvention,
                            SupportInformation = model.SupportInformation,
                            Disclosure = model.Disclosure,
                            BiologicalMaterial = model.BiologicalMaterial,
                            DetailsOfBiologicalMaterial = model.DetailsOfBiologicalMaterial,
                            RelevantInformation = model.RelevantInformation,
                            RequestedAction = model.RequestedAction,
                            RequestedActionOthers = model.RequestedtxtAction ?? model.RequestedCRtxtAction,
                            Status = "Submitted to IPOffice",
                            CreatedBy = uname,
                            CreatedOn = DateTime.Now
                        };
                        pat.tblIDFRequest.Add(dis);
                        pat.SaveChanges();
                        tblAnnexureB1 annex = new tblAnnexureB1()
                        {
                            FileNo = model.FileNo,
                            BriefDescription = model.Annex.BriefDescription,
                            DevelopmentStage = model.Annex.DevelopmentStage,
                            OtherInfo = model.Annex.OtherInfo,
                            L1Search = model.Annex.L1Search,
                            Tool = model.Annex.Tool,
                            Party = model.Annex.Party,
                            Outcome = model.Annex.Outcome,
                            Comments = model.Annex.Comments
                        };
                        pat.tblAnnexureB1.Add(annex);
                        pat.SaveChanges();
                        int i = 1;
                        foreach (var coinven in model.CoIn)
                        {
                            if (coinven.Type != null)
                            {
                                tblCoInventor coinventor = new tblCoInventor()
                                {
                                    SNo = i,
                                    Type = coinven.Type,
                                    Name = coinven.Name,
                                    FileNo = model.FileNo,
                                    Dept = coinven.Dept,
                                    ContactNo = coinven.Ph,
                                    EmailId = coinven.Mail,
                                    InstId = coinven.InstId,
                                    CreatedBy = uname,
                                    CreatedOn = DateTime.Now
                                };
                                pat.tblCoInventor.Add(coinventor);
                                pat.SaveChanges();
                            }
                            ++i;
                        }
                        int j = 1;
                        foreach (var appln in model.Appl)
                        {
                            if (appln.Organisation != null)
                            {
                                tblApplicants applicants = new tblApplicants()
                                {
                                    FileNo = model.FileNo,
                                    Sno = j,
                                    Organisation = appln.Organisation,
                                    ContactName = appln.ContactName,
                                    Position = appln.Position,
                                    Address = appln.Address,
                                    ContactNo = appln.ContactNo,
                                    EmailId = appln.EmailId,
                                    CreatedBy = uname,
                                    CreatedOn = DateTime.Now
                                };
                                pat.tblApplicants.Add(applicants);
                                pat.SaveChanges();
                                ++j;
                            }
                        }
                        int z = 1; int index = 0;
                        foreach (var ind in model.Annex.ListIndustry)
                        {
                            if (ind.Selected == true)
                            {
                                tblApplicationAreas a = new tblApplicationAreas()
                                {
                                    Sno = z,
                                    FileNo = model.FileNo,
                                    Areas = ind.Value,
                                    Index = index,
                                    Category = "ApplicationIndustry"
                                };
                                pat.tblApplicationAreas.Add(a);
                                pat.SaveChanges();
                                ++z; ++index;
                            }
                            else { ++index; }
                        }
                        int ax = 1; int index1 = 0;
                        foreach (var ind1 in model.Annex.ListIndustry1)
                        {
                            if (ind1.Selected == true)
                            {
                                tblApplicationAreas a = new tblApplicationAreas()
                                {
                                    Sno = ax,
                                    FileNo = model.FileNo,
                                    Areas = ind1.Value,
                                    Index = index1,
                                    Category = "SubIndustry"
                                };
                                pat.tblApplicationAreas.Add(a);
                                pat.SaveChanges();
                                ++ax; ++index1;
                            }
                            else { ++index1; }
                        }
                        int ay = 1; int ModeInd = 0;
                        foreach (var m1 in model.Annex.IITMode)
                        {
                            if (m1.Selected == true)
                            {
                                tblCommercialisationMode a = new tblCommercialisationMode()
                                {
                                    Sno = ay,
                                    FileNo = model.FileNo,
                                    Mode = m1.Value,
                                    IndNo = ModeInd,
                                    Category = "IIT"
                                };
                                pat.tblCommercialisationMode.Add(a);
                                pat.SaveChanges();
                                ++ay; ++ModeInd;
                            }
                            else { ++ModeInd; }
                        }
                        int az = 1; int ModeJoint = 0;
                        foreach (var m1 in model.Annex.JointMode)
                        {
                            if (m1.Selected == true)
                            {
                                tblCommercialisationMode a = new tblCommercialisationMode()
                                {
                                    Sno = az,
                                    FileNo = model.FileNo,
                                    Mode = m1.Value,
                                    IndNo = ModeJoint,
                                    Category = "Joint"
                                };
                                pat.tblCommercialisationMode.Add(a);
                                pat.SaveChanges();
                                ++az; ++ModeJoint;
                            }
                            else { ++ModeJoint; }
                        }
                        if (model.IDFType == "Copyright")
                        {
                            if (model.CR != null)
                            {
                                tbl_Copyright tcr = new tbl_Copyright()
                                {
                                    Category = model.CR.Category,
                                    ClassofWork = model.CR.ClassofWork,
                                    Description = model.CR.Description,
                                    Details = model.CR.Details,
                                    FileNo = model.FileNo,
                                    isPublished = model.CR.isPublished,
                                    isRegistered = model.CR.isRegistered,
                                    Language = model.CR.Language,
                                    Nature = model.CR.Nature,
                                    Original = model.CR.Original,
                                    Title = model.CR.Title
                                };
                                pat.tbl_Copyright.Add(tcr);
                                pat.SaveChanges();
                            }
                            int cr = 1;
                            foreach (var auth in model.CR.Author)
                            {
                                if (auth.AUName != null)
                                {
                                    tbl_CR_Author author = new tbl_CR_Author()
                                    {
                                        SNo = cr,
                                        AUName = auth.AUName,
                                        AUAddress = auth.AUAddress,
                                        AUNationality = auth.AUNationality,
                                        isDeceased = auth.isDeceased,
                                        deceasedDt = auth.deceasedDt,
                                        FileNo = model.FileNo,
                                        createdBy = uname,
                                        createdOn = DateTime.Now
                                    };
                                    pat.tbl_CR_Author.Add(author);
                                    pat.SaveChanges();
                                }
                            }
                            int pub = 1;
                            foreach (var publ in model.CR.Publish)
                            {
                                if (publ.PUName != null)
                                {
                                    tbl_CR_Publish publish = new tbl_CR_Publish()
                                    {
                                        PUName = publ.PUName,
                                        Country = publ.Country,
                                        FileNo = model.FileNo,
                                        PUAddress = publ.PUAddress,
                                        PUNationality = publ.PUNationality,
                                        Sno = pub,
                                        Year = publ.Year
                                    };
                                    pat.tbl_CR_Publish.Add(publish);
                                    pat.SaveChanges();
                                }
                            }
                        }                        
                        if(model.IDFType== "DesignPatent")
                        {                           
                         int sn = 1; int indexcl = 0;
                         foreach (var dc in model.DesignClass.ClassList)
                         {
                           if (dc.Selected == true)
                           {
                               tbl_DesignClasses a = new tbl_DesignClasses()
                               {
                                  Sno = sn,
                                   FileNo = model.FileNo,
                                   Class =dc.Value,
                                   Index = indexcl
                               };
                               pat.tbl_DesignClasses.Add(a);
                               pat.SaveChanges();
                               ++sn; ++indexcl;
                           }
                           else { ++indexcl; }
                         }                                                     
                        }
                        if (model.IDFType == "Trademark")
                        {
                            if (model.Trade != null)
                            {
                                tbl_Trademark ttm = new tbl_Trademark()
                                {
                                    Category = model.Trade.Category,
                                    Class = model.Trade.Class,
                                    Description = model.Trade.Description,
                                    FileNo = model.FileNo,
                                    Language = model.Trade.Language,
                                    TMImage = model.Trade.TMImage,
                                    TMName = model.Trade.TMName,
                                    TMStatement = model.Trade.TMStatement
                                };
                                pat.tbl_Trademark.Add(ttm);
                            }
                            pat.SaveChanges();
                            if (model.Trade.TAppl.Count > 0)
                            {
                                int aj = 1;
                                foreach (var appln in model.Trade.TAppl)
                                {
                                    if (appln.Organisation != null)
                                    {
                                        tbl_Trade_Applicantdetail tappl = new tbl_Trade_Applicantdetail()
                                        {
                                            FileNo = model.FileNo,
                                            Sno = aj,
                                            Organisation = appln.Organisation,
                                            AddressOfService = appln.AddressOfService,
                                            Country = appln.Country,
                                            Jurisdiction = appln.Jurisdiction,
                                            LegalStatus = appln.LegalStatus,
                                            Nature = appln.Nature,
                                            CreatedBy = uname,
                                            CreatedOn = DateTime.Now
                                        };
                                        pat.tbl_Trade_Applicantdetail.Add(tappl);
                                        pat.SaveChanges();
                                        ++aj;
                                    }
                                }
                            }
                        }
                        // Version history 
                        tbl_trx_IDFRequest tdis = new tbl_trx_IDFRequest()
                        {
                            FileNo = model.FileNo,
                            IDFType = model.IDFType,
                            PrimaryInventorName = model.PrimaryInventorName,
                            PrimaryInventorType = model.PrimaryInventorType,
                            PIDepartment = model.PIDepartment,
                            PIContactNo = model.PIContactNo,
                            PIEmailId = model.PIEmailId,
                            FirstApplicantName = model.FirstApplicantName,
                            FirstApplicantPosition = model.FirstApplicantPosition,
                            FirstApplicantAddress = model.FirstApplicantAddress,
                            FirstApplicantContactNo = model.FirstApplicantContactNo,
                            FirstApplicantEmailId = model.FirstApplicantEmailId,
                            FirstApplicantOrganisation = model.FirstApplicantOrganisation,
                            Title = model.Title,
                            FieldOfInvention = model.FieldOfInvention,
                            Summary = model.Summary,
                            Description = model.Description,
                            PriorPublication = model.PriorPublication,
                            SourceOfInvention = model.SourceOfInvention,
                            SupportInformation = model.SupportInformation,
                            Disclosure = model.Disclosure,
                            BiologicalMaterial = model.BiologicalMaterial,
                            DetailsOfBiologicalMaterial = model.DetailsOfBiologicalMaterial,
                            RelevantInformation = model.RelevantInformation,
                            RequestedAction = model.RequestedAction,
                            RequestedActionOthers = model.RequestedtxtAction ?? model.RequestedCRtxtAction,
                            VersionId = 1,
                            Status="Submitted to IPOffice",
                            CreatedBy = uname,
                            CreatedOn = DateTime.Now
                        };
                        pat.tbl_trx_IDFRequest.Add(tdis);
                        pat.SaveChanges();
                        tbl_trx_AnnexureB1 tannex = new tbl_trx_AnnexureB1()
                        {
                            FileNo = model.FileNo,
                            BriefDescription = model.Annex.BriefDescription,
                            DevelopmentStage = model.Annex.DevelopmentStage,
                            OtherInfo = model.Annex.OtherInfo,
                            L1Search = model.Annex.L1Search,
                            Tool = model.Annex.Tool,
                            Party = model.Annex.Party,
                            Outcome = model.Annex.Outcome,
                            VersionId = 1,
                            Comments = model.Annex.Comments

                        };
                        pat.tbl_trx_AnnexureB1.Add(tannex);
                        pat.SaveChanges();
                        int ti = 1;
                        foreach (var coinven in model.CoIn)
                        {
                            if (coinven.Type != null)
                            {
                                tbl_trx_CoInventor tcoinventor = new tbl_trx_CoInventor()
                                {
                                    SNo = ti,
                                    Type = coinven.Type,
                                    Name = coinven.Name,
                                    FileNo = model.FileNo,
                                    Dept = coinven.Dept,
                                    ContactNo = coinven.Ph,
                                    EmailId = coinven.Mail,
                                    VersionId = 1,
                                    CreatedBy = uname,
                                    CreatedOn = DateTime.Now
                                };
                                pat.tbl_trx_CoInventor.Add(tcoinventor);
                                pat.SaveChanges();
                            }
                            ++ti;
                        }
                        int tj = 1;
                        foreach (var appln in model.Appl)
                        {
                            if (appln.Organisation != null)
                            {
                                tbl_trx_Applicants tapplicants = new tbl_trx_Applicants()
                                {
                                    FileNo = model.FileNo,
                                    Sno = tj,
                                    Organisation = appln.Organisation,
                                    ContactName = appln.ContactName,
                                    Position = appln.Position,
                                    Address = appln.Address,
                                    ContactNo = appln.ContactNo,
                                    EmailId = appln.EmailId,
                                    VersionId = 1,
                                    CreatedBy = uname,
                                    CreatedOn = DateTime.Now
                                };
                                pat.tbl_trx_Applicants.Add(tapplicants);
                                pat.SaveChanges();
                                ++tj;
                            }
                        }
                        int tz = 1; int tindex = 0;
                        foreach (var ind in model.Annex.ListIndustry)
                        {
                            if (ind.Selected == true)
                            {
                                tbl_trx_ApplicationAreas ta = new tbl_trx_ApplicationAreas()
                                {
                                    Sno = tz,
                                    FileNo = model.FileNo,
                                    Areas = ind.Value,
                                    Index = tindex,
                                    VersionId = 1,
                                    Category = "ApplicationIndustry"
                                };
                                pat.tbl_trx_ApplicationAreas.Add(ta);
                                pat.SaveChanges();
                                ++tz; ++tindex;
                            }
                            else { ++tindex; }
                        }
                        int tax = 1; int tindex1 = 0;
                        foreach (var ind1 in model.Annex.ListIndustry1)
                        {
                            if (ind1.Selected == true)
                            {
                                tbl_trx_ApplicationAreas ta = new tbl_trx_ApplicationAreas()
                                {
                                    Sno = tax,
                                    FileNo = model.FileNo,
                                    Areas = ind1.Value,
                                    Index = tindex1,
                                    VersionId = 1,
                                    Category = "SubIndustry"
                                };
                                pat.tbl_trx_ApplicationAreas.Add(ta);
                                pat.SaveChanges();
                                ++tax; ++tindex1;
                            }
                            else { ++tindex1; }
                        }
                        int tay = 1; int tModeInd = 0;
                        foreach (var m1 in model.Annex.IITMode)
                        {
                            if (m1.Selected == true)
                            {
                                tbl_trx_CommercialisationMode a = new tbl_trx_CommercialisationMode()
                                {
                                    Sno = tay,
                                    FileNo = model.FileNo,
                                    Mode = m1.Value,
                                    IndNo = tModeInd,
                                    VersionId = 1,
                                    Category = "IIT"
                                };
                                pat.tbl_trx_CommercialisationMode.Add(a);
                                pat.SaveChanges();
                                ++tay; ++tModeInd;
                            }
                            else { ++tModeInd; }
                        }
                        int taz = 1; int tModeJoint = 0;
                        foreach (var m1 in model.Annex.JointMode)
                        {
                            if (m1.Selected == true)
                            {
                                tbl_trx_CommercialisationMode a = new tbl_trx_CommercialisationMode()
                                {
                                    Sno = taz,
                                    FileNo = model.FileNo,
                                    Mode = m1.Value,
                                    IndNo = tModeJoint,
                                    VersionId = 1,
                                    Category = "Joint"
                                };
                                pat.tbl_trx_CommercialisationMode.Add(a);
                                pat.SaveChanges();
                                ++taz; ++tModeJoint;
                            }
                            else { ++tModeJoint; }
                        }
                        if (model.IDFType == "Copyright")
                        {
                            if (model.CR != null)
                            {
                                tbl_trx_Copyright txcr = new tbl_trx_Copyright()
                                {
                                    Category = model.CR.Category,
                                    VersionId = 1,
                                    ClassofWork = model.CR.ClassofWork,
                                    Description = model.CR.Description,
                                    Details = model.CR.Details,
                                    FileNo = model.FileNo,
                                    isPublished = model.CR.isPublished,
                                    isRegistered = model.CR.isRegistered,
                                    Language = model.CR.Language,
                                    Nature = model.CR.Nature,
                                    Original = model.CR.Original,
                                    Title = model.CR.Title
                                };
                                pat.tbl_trx_Copyright.Add(txcr);
                                pat.SaveChanges();
                            }
                            int cr = 1;
                            foreach (var auth in model.CR.Author)
                            {
                                if (auth.AUName != null)
                                {
                                    tbl_trx_CR_Author txauthor = new tbl_trx_CR_Author()
                                    {
                                        SNo = cr,
                                        VersionId = 1,
                                        AUName = auth.AUName,
                                        AUAddress = auth.AUAddress,
                                        AUNationality = auth.AUNationality,
                                        isDeceased = auth.isDeceased,
                                        deceasedDt = auth.deceasedDt,
                                        FileNo = model.FileNo,
                                        createdBy = uname,
                                        createdOn = DateTime.Now
                                    };
                                    pat.tbl_trx_CR_Author.Add(txauthor);
                                    pat.SaveChanges();
                                }
                            }
                            int pub = 1;
                            foreach (var publ in model.CR.Publish)
                            {
                                if (publ.PUName != null)
                                {
                                    tbl_trx_CR_Publish txpublish = new tbl_trx_CR_Publish()
                                    {
                                        PUName = publ.PUName,
                                        Country = publ.Country,
                                        FileNo = model.FileNo,
                                        PUAddress = publ.PUAddress,
                                        PUNationality = publ.PUNationality,
                                        Sno = pub,
                                        VersionId = 1,
                                        Year = publ.Year
                                    };
                                    pat.tbl_trx_CR_Publish.Add(txpublish);
                                    pat.SaveChanges();
                                }
                            }
                        }
                        if (model.IDFType == "Trademark")
                        {
                            if (model.Trade != null)
                            {
                                tbl_trx_Trademark txtm = new tbl_trx_Trademark()
                                {
                                    VersionId = 1,
                                    Category = model.Trade.Category,
                                    Class = model.Trade.Class,
                                    Description = model.Trade.Description,
                                    FileNo = model.FileNo,
                                    Language = model.Trade.Language,
                                    TMImage = model.Trade.TMImage,
                                    TMName = model.Trade.TMName,
                                    TMStatement = model.Trade.TMStatement
                                };
                                pat.tbl_trx_Trademark.Add(txtm);
                            }
                            pat.SaveChanges();
                            if (model.Trade.TAppl.Count > 0)
                            {
                                int aj = 1;
                                foreach (var appln in model.Trade.TAppl)
                                {
                                    if (appln.Organisation != null)
                                    {
                                        tbl_trx_Trade_Applicantdetail tappl = new tbl_trx_Trade_Applicantdetail()
                                        {
                                            FileNo = model.FileNo,
                                            Sno = aj,
                                            VersionId = 1,
                                            Organisation = appln.Organisation,
                                            AddressOfService = appln.AddressOfService,
                                            Country = appln.Country,
                                            Jurisdiction = appln.Jurisdiction,
                                            LegalStatus = appln.LegalStatus,
                                            Nature = appln.Nature,
                                            CreatedBy = uname,
                                            CreatedOn = DateTime.Now
                                        };
                                        pat.tbl_trx_Trade_Applicantdetail.Add(tappl);
                                        pat.SaveChanges();
                                        ++aj;
                                    }
                                }
                            }
                        }
                        if (model.IDFType == "DesignPatent")
                        {
                            int sn = 1; int indexcl = 0;
                            foreach (var dc in model.DesignClass.ClassList)
                            {
                                if (dc.Selected == true)
                                {
                                    tbl_DesignClasses a = new tbl_DesignClasses()
                                    {
                                        Sno = sn,
                                        FileNo = model.FileNo,
                                        Class =dc.Value,
                                        Index = indexcl
                                    };
                                    pat.tbl_DesignClasses.Add(a);
                                    pat.SaveChanges();
                                    ++sn; ++indexcl;
                                }
                                else { ++indexcl; }
                            }
                        }
                        if (model.IDFType == "DesignPatent")
                        {
                            int des = 1;int ind = 0;
                            foreach (var dp in model.DesignClass.ClassList)
                            {
                                if (dp.Selected == true)
                                {
                                    tbl_trx_DesignClasses dc = new tbl_trx_DesignClasses()
                                    {
                                        Class =dp.Value,                                        
                                        FileNo = model.FileNo,
                                        Index = ind,
                                        Sno = des
                                    };
                                    pat.tbl_trx_DesignClasses.Add(dc);
                                    pat.SaveChanges();
                                    ++des;++ind;
                                }
                                else { ++ind; }
                            }
                        }
                        if (file[0] != null)
                        {
                            string filepath = pat.tbl_mst_filepath.Where(m => m.Category == "PatentDocument").Select(m => m.FilePath).FirstOrDefault();
                            if (!Directory.Exists(filepath))
                            {
                                return "Path Error";
                            }
                            filepath = filepath + model.FileNo + "\\IDFRequest\\";
                            if (!Directory.Exists(filepath))
                            {
                                Directory.CreateDirectory(filepath);
                            }
                            int f = 1;
                            foreach (var item in file)
                            {
                                if (item != null && item.ContentLength > 0)
                                {
                                    tbl_files_PatentRequest ufiles = new tbl_files_PatentRequest()
                                    {
                                        FileNo = model.FileNo,
                                        DocId = f,
                                        DocName = item.FileName,
                                        DocPath = filepath
                                    };
                                    pat.tbl_files_PatentRequest.Add(ufiles);
                                    item.SaveAs(filepath + item.FileName);
                                    pat.SaveChanges();
                                }
                            }
                        }
                        transaction.Commit();
                        return "Success";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return ex.ToString();
                    }
                }
            }
        }
        public static string UpdateIDFRequest(IDFRequestVM model, HttpPostedFileBase[] file, string uname)
        {

            using (PatentISEntities pat = new PatentISEntities())
            {

                using (var transaction = pat.Database.BeginTransaction())
                {
                    try
                    {
                        var idf = pat.tblIDFRequest.Where(m => m.FileNo == model.FileNo).FirstOrDefault();
                        if (idf != null)
                        {
                            idf.FileNo = model.FileNo;
                            idf.PrimaryInventorName = model.PrimaryInventorName;
                            idf.PrimaryInventorType = model.PrimaryInventorType;
                            idf.PIDepartment = model.PIDepartment;
                            idf.PIContactNo = model.PIContactNo;
                            idf.PIEmailId = model.PIEmailId;
                            idf.PIInstId = model.PIInstId;
                            idf.FirstApplicantName = model.FirstApplicantName;
                            idf.FirstApplicantPosition = model.FirstApplicantPosition;
                            idf.FirstApplicantAddress = model.FirstApplicantAddress;
                            idf.FirstApplicantContactNo = model.FirstApplicantContactNo;
                            idf.FirstApplicantEmailId = model.FirstApplicantEmailId;
                            idf.FirstApplicantOrganisation = model.FirstApplicantOrganisation;
                            idf.Title = model.Title;
                            idf.FieldOfInvention = model.FieldOfInvention;
                            idf.Summary = model.Summary;
                            idf.Description = model.Description;
                            idf.PriorPublication = model.PriorPublication;
                            idf.SourceOfInvention = model.SourceOfInvention;
                            idf.SupportInformation = model.SupportInformation;
                            idf.Disclosure = model.Disclosure;
                            idf.BiologicalMaterial = model.BiologicalMaterial;
                            idf.DetailsOfBiologicalMaterial = model.DetailsOfBiologicalMaterial;
                            idf.RelevantInformation = model.RelevantInformation;
                            idf.RequestedAction = model.RequestedAction;
                            idf.RequestedActionOthers = model.RequestedtxtAction ?? model.RequestedCRtxtAction;                            
                            idf.ModifiedBy = uname;
                            idf.ModifiedOn = DateTime.Now;
                            pat.SaveChanges();
                        }
                        if (model.Annex != null)
                        {
                            var anex = pat.tblAnnexureB1.Where(m => m.FileNo == model.FileNo).FirstOrDefault();
                            if (anex != null)
                            {
                                anex.FileNo = model.FileNo;
                                anex.BriefDescription = model.Annex.BriefDescription;
                                anex.DevelopmentStage = model.Annex.DevelopmentStage;
                                anex.OtherInfo = model.Annex.OtherInfo;
                                anex.L1Search = model.Annex.L1Search;
                                anex.Tool = model.Annex.Tool;
                                anex.Party = model.Annex.Party;
                                anex.Outcome = model.Annex.Outcome;
                                anex.Comments = model.Annex.Comments;
                                pat.SaveChanges();
                            }
                        }
                        if (model.CoIn.Count > 0)
                        {
                            int a = 0;
                            var co = pat.tblCoInventor.Where(m => m.FileNo == model.FileNo).ToList();
                            if (co.Count == model.CoIn.Count)
                            {
                                foreach (var m in co)
                                {
                                    m.FileNo = model.FileNo;
                                    m.Type = model.CoIn[a].Type;
                                    m.Name = model.CoIn[a].Name;
                                    m.Dept = model.CoIn[a].Dept;
                                    m.EmailId = model.CoIn[a].Mail;
                                    m.ContactNo = model.CoIn[a].Ph;
                                    m.InstId = model.CoIn[a].InstId;
                                    m.SNo = ++a;
                                    m.ModifiedBy = uname;
                                    m.ModifiedOn = DateTime.Now.Date;
                                }
                                pat.SaveChanges();
                            }
                            else
                            {
                                if (co != null)
                                {
                                    pat.tblCoInventor.RemoveRange(co);
                                    pat.SaveChanges();
                                }
                                foreach (var m in model.CoIn)
                                {
                                    if (m.Type != null)
                                    {
                                        tblCoInventor tco = new tblCoInventor()
                                        {
                                            FileNo = model.FileNo,
                                            Type = model.CoIn[a].Type,
                                            Dept = model.CoIn[a].Dept,
                                            EmailId = model.CoIn[a].Mail,
                                            ContactNo = model.CoIn[a].Ph,
                                            InstId = model.CoIn[a].InstId,
                                            Name = model.CoIn[a].Name,
                                            SNo = ++a,
                                            ModifiedBy = uname,
                                            ModifiedOn = DateTime.Now.Date
                                        };
                                        pat.tblCoInventor.Add(tco);
                                    }
                                }
                                pat.SaveChanges();
                            }
                        }
                        else
                        {
                            var co = pat.tblCoInventor.Where(m => m.FileNo == model.FileNo).ToList();
                            if (co != null)
                            {
                                pat.tblCoInventor.RemoveRange(co);
                                pat.SaveChanges();
                            }
                        }
                        if (model.Appl.Count > 0)
                        {
                            int a = 0;
                            var ap = pat.tblApplicants.Where(m => m.FileNo == model.FileNo).ToList();
                            if (ap.Count == model.Appl.Count)
                            {
                                foreach (var m in ap)
                                {
                                    m.FileNo = model.FileNo;
                                    m.Address = model.Appl[a].Address;
                                    m.ContactName = model.Appl[a].ContactName;
                                    m.ContactNo = model.Appl[a].ContactNo;
                                    m.EmailId = model.Appl[a].EmailId;
                                    m.Organisation = model.Appl[a].Organisation;
                                    m.Position = model.Appl[a].Position;
                                    m.Sno = ++a;
                                    m.ModifiedOn = DateTime.Now.Date;
                                    m.ModifiedBy = uname;
                                }
                                pat.SaveChanges();
                            }
                            else
                            {
                                if (ap != null)
                                {
                                    pat.tblApplicants.RemoveRange(ap);
                                    pat.SaveChanges();
                                }
                                foreach (var m in model.Appl)
                                {
                                    if (m.Organisation != null || m.ContactName != null)
                                    {
                                        tblApplicants tsec = new tblApplicants()
                                        {
                                            FileNo = model.FileNo,
                                            Address = model.Appl[a].Address,
                                            ContactName = model.Appl[a].ContactName,
                                            ContactNo = model.Appl[a].ContactNo,
                                            EmailId = model.Appl[a].EmailId,
                                            Organisation = model.Appl[a].Organisation,
                                            Position = model.Appl[a].Position,
                                            Sno = ++a,
                                            ModifiedBy = uname,
                                            ModifiedOn = DateTime.Now.Date
                                        };
                                        pat.tblApplicants.Add(tsec);
                                    }
                                }
                                pat.SaveChanges();
                            }
                        }
                        else
                        {
                            var ap = pat.tblApplicants.Where(m => m.FileNo == model.FileNo).ToList();
                            if (ap != null)
                            {
                                pat.tblApplicants.RemoveRange(ap);
                                pat.SaveChanges();
                            }
                        }
                        int z = 1; int index = 0;
                        var indus = pat.tblApplicationAreas.Where(m => m.FileNo == model.FileNo && m.Category == "ApplicationIndustry").ToList();
                        if (indus.Count > 0)
                        {
                            pat.tblApplicationAreas.RemoveRange(indus);
                            pat.SaveChanges();
                        }
                        foreach (var ind in model.Annex.ListIndustry)
                        {
                            if (ind.Selected == true)
                            {
                                tblApplicationAreas a = new tblApplicationAreas()
                                {
                                    Sno = z,
                                    FileNo = model.FileNo,
                                    Areas = ind.Value,
                                    Index = index,
                                    Category = "ApplicationIndustry"
                                };
                                pat.tblApplicationAreas.Add(a);
                                pat.SaveChanges();
                                ++z; ++index;
                            }
                            else { ++index; }
                        }
                        pat.SaveChanges();
                        int ax = 1; int index1 = 0;
                        var indus1 = pat.tblApplicationAreas.Where(m => m.FileNo == model.FileNo && m.Category == "SubIndustry").ToList();
                        if (indus1.Count > 0)
                        {
                            pat.tblApplicationAreas.RemoveRange(indus1);
                            pat.SaveChanges();
                        }
                        foreach (var ind1 in model.Annex.ListIndustry1)
                        {
                            if (ind1.Selected == true)
                            {
                                tblApplicationAreas a = new tblApplicationAreas()
                                {
                                    Sno = ax,
                                    FileNo = model.FileNo,
                                    Areas = ind1.Value,
                                    Index = index1,
                                    Category = "SubIndustry"
                                };
                                pat.tblApplicationAreas.Add(a);
                                pat.SaveChanges();
                                ++ax; ++index1;
                            }
                            else { ++index1; }
                        }
                        pat.SaveChanges();
                        var imode = pat.tblCommercialisationMode.Where(m => m.FileNo == model.FileNo && m.Category == "IIT").ToList();
                        if (imode.Count > 0)
                        {
                            pat.tblCommercialisationMode.RemoveRange(imode);
                            pat.SaveChanges();
                        }
                        int ay = 1; int ModeInd = 0;
                        foreach (var m1 in model.Annex.IITMode)
                        {
                            if (m1.Selected == true)
                            {
                                tblCommercialisationMode a = new tblCommercialisationMode()
                                {
                                    Sno = ay,
                                    FileNo = model.FileNo,
                                    Mode = m1.Value,
                                    IndNo = ModeInd,
                                    Category = "IIT"
                                };
                                pat.tblCommercialisationMode.Add(a);
                                pat.SaveChanges();
                                ++ay; ++ModeInd;
                            }
                            else { ++ModeInd; }
                        }
                        pat.SaveChanges();
                        var jmode = pat.tblCommercialisationMode.Where(m => m.FileNo == model.FileNo && m.Category == "Joint").ToList();
                        if (jmode.Count > 0)
                        {
                            pat.tblCommercialisationMode.RemoveRange(jmode);
                            pat.SaveChanges();
                        }
                        int az = 1; int ModeJoint = 0;
                        foreach (var m1 in model.Annex.JointMode)
                        {
                            if (m1.Selected == true)
                            {
                                tblCommercialisationMode a = new tblCommercialisationMode()
                                {
                                    Sno = az,
                                    FileNo = model.FileNo,
                                    Mode = m1.Value,
                                    IndNo = ModeJoint,
                                    Category = "Joint"
                                };
                                pat.tblCommercialisationMode.Add(a);
                                pat.SaveChanges();
                                ++az; ++ModeJoint;
                            }
                            else { ++ModeJoint; }
                        }
                        if (model.IDFType == "Copyright")
                        {
                            if (model.CR != null)
                            {
                                var cr = pat.tbl_Copyright.Where(m => m.FileNo == model.FileNo).FirstOrDefault();
                                if (cr != null)
                                {
                                    cr.Category = model.CR.Category;
                                    cr.ClassofWork = model.CR.ClassofWork;
                                    cr.Description = model.CR.Description;
                                    cr.Details = model.CR.Details;
                                    cr.isPublished = model.CR.isPublished;
                                    cr.isRegistered = model.CR.isRegistered;
                                    cr.Language = model.CR.Language;
                                    cr.Nature = model.CR.Nature;
                                    cr.Original = model.CR.Original;
                                    cr.Title = model.CR.Title;
                                    pat.SaveChanges();
                                }
                                else
                                {
                                    tbl_Copyright tcr = new tbl_Copyright()
                                    {
                                        Category = model.CR.Category,
                                        ClassofWork = model.CR.ClassofWork,
                                        Description = model.CR.Description,
                                        Details = model.CR.Details,
                                        FileNo = model.FileNo,
                                        isPublished = model.CR.isPublished,
                                        isRegistered = model.CR.isRegistered,
                                        Language = model.CR.Language,
                                        Nature = model.CR.Nature,
                                        Original = model.CR.Original,
                                        Title = model.CR.Title
                                    };
                                    pat.tbl_Copyright.Add(tcr);
                                    pat.SaveChanges();
                                }
                                var auth = pat.tbl_CR_Author.Where(m => m.FileNo == model.FileNo).ToList();
                                if (model.CR.Author.Count > 0)
                                {
                                    int i = 0;
                                    if (auth.Count == model.CR.Author.Count)
                                    {
                                        foreach (var a in auth)
                                        {
                                            a.FileNo = model.FileNo;
                                            a.AUAddress = model.CR.Author[i].AUAddress;
                                            a.AUName = model.CR.Author[i].AUName;
                                            a.AUNationality = model.CR.Author[i].AUNationality;
                                            a.deceasedDt = model.CR.Author[i].deceasedDt;
                                            a.isDeceased = model.CR.Author[i].isDeceased;
                                            a.createdBy = uname;
                                            a.createdOn = DateTime.Now;
                                            a.SNo = ++i;
                                        }
                                        pat.SaveChanges();
                                    }
                                    else
                                    {
                                        if (auth.Count > 0)
                                        {
                                            pat.tbl_CR_Author.RemoveRange(auth);
                                            pat.SaveChanges();
                                        }
                                        foreach (var a in model.CR.Author)
                                        {
                                            if (a.AUName != null)
                                            {
                                                tbl_CR_Author tauth = new tbl_CR_Author()
                                                {
                                                    AUName = model.CR.Author[i].AUName,
                                                    FileNo = model.FileNo,
                                                    AUAddress = model.CR.Author[i].AUAddress,
                                                    AUNationality = model.CR.Author[i].AUNationality,
                                                    isDeceased = model.CR.Author[i].isDeceased,
                                                    deceasedDt = model.CR.Author[i].deceasedDt,
                                                    createdBy = uname,
                                                    createdOn = DateTime.Now,
                                                    SNo = ++i
                                                };
                                                pat.tbl_CR_Author.Add(tauth);
                                            }
                                        }
                                        pat.SaveChanges();
                                    }
                                }
                                else
                                {
                                    if (auth.Count > 0)
                                    {
                                        pat.tbl_CR_Author.RemoveRange(auth);
                                        pat.SaveChanges();
                                    }
                                }
                                var pub = pat.tbl_CR_Publish.Where(m => m.FileNo == model.FileNo).ToList();
                                if (model.CR.Publish.Count > 0)
                                {
                                    int j = 0;
                                    if (pub.Count == model.CR.Publish.Count)
                                    {
                                        foreach (var p in pub)
                                        {
                                            p.FileNo = model.FileNo;
                                            p.Country = model.CR.Publish[j].Country;
                                            p.PUAddress = model.CR.Publish[j].PUAddress;
                                            p.PUName = model.CR.Publish[j].PUName;
                                            p.PUNationality = model.CR.Publish[j].PUNationality;
                                            p.Year = model.CR.Publish[j].Year;
                                            p.Sno = ++j;
                                        }
                                        pat.SaveChanges();
                                    }
                                    else
                                    {
                                        if (pub.Count > 0)
                                        {
                                            pat.tbl_CR_Publish.RemoveRange(pub);
                                            pat.SaveChanges();
                                        }
                                        foreach (var p in model.CR.Publish)
                                        {
                                            if (p.PUName != null)
                                            {
                                                tbl_CR_Publish tpub = new tbl_CR_Publish()
                                                {
                                                    Country = model.CR.Publish[j].Country,
                                                    PUName = model.CR.Publish[j].PUName,
                                                    FileNo = model.FileNo,
                                                    PUAddress = model.CR.Publish[j].PUAddress,
                                                    PUNationality = model.CR.Publish[j].PUNationality,
                                                    Year = model.CR.Publish[j].Year,
                                                    Sno = ++j
                                                };
                                                pat.tbl_CR_Publish.Add(tpub);
                                            }
                                        }
                                        pat.SaveChanges();
                                    }
                                }
                                else
                                {
                                    if (pub.Count > 0)
                                    {
                                        pat.tbl_CR_Publish.RemoveRange(pub);
                                        pat.SaveChanges();
                                    }
                                }
                            }
                        }
                        else if (model.IDFType == "Trademark")
                        {
                            if (model.Trade != null)
                            {
                                var tr = pat.tbl_Trademark.Where(m => m.FileNo == model.FileNo).FirstOrDefault();
                                if (tr != null)
                                {
                                    tr.Category = model.Trade.Category;
                                    tr.Class = model.Trade.Class;
                                    tr.Description = model.Trade.Description;
                                    tr.FileNo = model.FileNo;
                                    tr.Language = model.Trade.Language;
                                    tr.TMImage = model.Trade.TMImage;
                                    tr.TMName = model.Trade.TMName;
                                    tr.TMStatement = model.Trade.TMStatement;
                                    pat.SaveChanges();
                                }
                                else
                                {
                                    tbl_Trademark ttr = new tbl_Trademark()
                                    {
                                        Category = model.Trade.Category,
                                        Class = model.Trade.Class,
                                        Description = model.Trade.Description,
                                        TMStatement = model.Trade.TMStatement,
                                        TMName = model.Trade.TMName,
                                        Language = model.Trade.Language,
                                        FileNo = model.Trade.FileNo,
                                        TMImage = model.Trade.TMImage
                                    };
                                    pat.tbl_Trademark.Add(ttr);
                                    pat.SaveChanges();
                                }
                            }
                            if (model.Trade.TAppl.Count > 0)
                            {
                                int a = 0;
                                var ap = pat.tbl_Trade_Applicantdetail.Where(m => m.FileNo == model.FileNo).ToList();
                                if (ap.Count == model.Trade.TAppl.Count)
                                {
                                    foreach (var m in ap)
                                    {
                                        m.FileNo = model.FileNo;
                                        m.AddressOfService = model.Trade.TAppl[a].AddressOfService;
                                        m.Country = model.Trade.TAppl[a].Country;
                                        m.Jurisdiction = model.Trade.TAppl[a].Jurisdiction;
                                        m.LegalStatus = model.Trade.TAppl[a].LegalStatus;
                                        m.Organisation = model.Trade.TAppl[a].Organisation;
                                        m.Nature = model.Trade.TAppl[a].Nature;
                                        m.Sno = ++a;
                                        m.CreatedOn = DateTime.Now.Date;
                                        m.CreatedBy = uname;
                                    }
                                    pat.SaveChanges();
                                }
                                else
                                {
                                    if (ap != null)
                                    {
                                        pat.tbl_Trade_Applicantdetail.RemoveRange(ap);
                                        pat.SaveChanges();
                                    }
                                    foreach (var m in model.Trade.TAppl)
                                    {
                                        if (m.Organisation != null)
                                        {
                                            tbl_Trade_Applicantdetail ttap = new tbl_Trade_Applicantdetail()
                                            {
                                                FileNo = model.FileNo,
                                                AddressOfService = model.Trade.TAppl[a].AddressOfService,
                                                Country = model.Trade.TAppl[a].Country,
                                                Jurisdiction = model.Trade.TAppl[a].Jurisdiction,
                                                LegalStatus = model.Trade.TAppl[a].LegalStatus,
                                                Organisation = model.Trade.TAppl[a].Organisation,
                                                Nature = model.Trade.TAppl[a].Nature,
                                                Sno = ++a,
                                                CreatedBy = uname,
                                                CreatedOn = DateTime.Now.Date
                                            };
                                            pat.tbl_Trade_Applicantdetail.Add(ttap);
                                        }
                                    }
                                    pat.SaveChanges();
                                }
                            }
                            else
                            {
                                var ap = pat.tbl_Trade_Applicantdetail.Where(m => m.FileNo == model.FileNo).ToList();
                                if (ap != null)
                                {
                                    pat.tbl_Trade_Applicantdetail.RemoveRange(ap);
                                    pat.SaveChanges();
                                }
                            }
                        }
                        else if (model.IDFType == "DesignPatent")
                        {
                            int sn = 1; int indexcl = 0;
                            var cl = pat.tbl_DesignClasses.Where(m => m.FileNo == model.FileNo).ToList();
                            if (cl.Count > 0)
                            {
                                pat.tbl_DesignClasses.RemoveRange(cl);
                                pat.SaveChanges();
                            }
                            foreach (var dc in model.DesignClass.ClassList)
                            {
                                if (dc.Selected == true)
                                {
                                    tbl_DesignClasses a = new tbl_DesignClasses()
                                    {
                                        Sno = sn,
                                        FileNo = model.FileNo,
                                        Class =dc.Value,
                                        Index = indexcl                                        
                                    };
                                    pat.tbl_DesignClasses.Add(a);
                                    pat.SaveChanges();
                                    ++sn; ++indexcl;
                                }
                                else { ++indexcl; }
                            }
                            pat.SaveChanges();

                        }
                        // Version history only if IP team needs clarification
                        var status = pat.tblIDFRequest.FirstOrDefault(m => m.FileNo == model.FileNo);
                        if (status.Status == "Clarification needed")
                        {
                            int ver = pat.tbl_trx_IDFRequest.Where(m => m.FileNo == model.FileNo).OrderByDescending(m => m.VersionId).Select(m => m.VersionId).FirstOrDefault();
                            ver += 1;
                            tbl_trx_IDFRequest tdis = new tbl_trx_IDFRequest()
                            {
                                FileNo = model.FileNo,
                                PrimaryInventorName = model.PrimaryInventorName,
                                PrimaryInventorType = model.PrimaryInventorType,
                                PIDepartment = model.PIDepartment,
                                PIContactNo = model.PIContactNo,
                                PIEmailId = model.PIEmailId,
                                FirstApplicantName = model.FirstApplicantName,
                                FirstApplicantPosition = model.FirstApplicantPosition,
                                FirstApplicantAddress = model.FirstApplicantAddress,
                                FirstApplicantContactNo = model.FirstApplicantContactNo,
                                FirstApplicantEmailId = model.FirstApplicantEmailId,
                                FirstApplicantOrganisation = model.FirstApplicantOrganisation,
                                Title = model.Title,
                                FieldOfInvention = model.FieldOfInvention,
                                Summary = model.Summary,
                                Description = model.Description,
                                PriorPublication = model.PriorPublication,
                                SourceOfInvention = model.SourceOfInvention,
                                SupportInformation = model.SupportInformation,
                                Disclosure = model.Disclosure,
                                BiologicalMaterial = model.BiologicalMaterial,
                                DetailsOfBiologicalMaterial = model.DetailsOfBiologicalMaterial,
                                RelevantInformation = model.RelevantInformation,
                                RequestedAction = model.RequestedAction,
                                VersionId = ver,
                                IDFType=model.IDFType,
                                Status="Submitted to IPOffice",
                                CreatedBy = uname,
                                CreatedOn = DateTime.Now
                            };
                            pat.tbl_trx_IDFRequest.Add(tdis);
                            pat.SaveChanges();
                            tbl_trx_AnnexureB1 tannex = new tbl_trx_AnnexureB1()
                            {
                                FileNo = model.FileNo,
                                BriefDescription = model.Annex.BriefDescription,
                                DevelopmentStage = model.Annex.DevelopmentStage,
                                OtherInfo = model.Annex.OtherInfo,
                                L1Search = model.Annex.L1Search,
                                Tool = model.Annex.Tool,
                                Party = model.Annex.Party,
                                Outcome = model.Annex.Outcome,
                                VersionId = ver,
                                Comments = model.Annex.Comments
                            };
                            pat.tbl_trx_AnnexureB1.Add(tannex);
                            pat.SaveChanges();
                            int ti = 1;
                            foreach (var coinven in model.CoIn)
                            {
                                if (coinven.Type != null)
                                {
                                    tbl_trx_CoInventor tcoinventor = new tbl_trx_CoInventor()
                                    {
                                        SNo = ti,
                                        Type = coinven.Type,
                                        Name = coinven.Name,
                                        FileNo = model.FileNo,
                                        Dept = coinven.Dept,
                                        ContactNo = coinven.Ph,
                                        EmailId = coinven.Mail,
                                        InstId=coinven.InstId,
                                        VersionId = ver,
                                        CreatedBy = uname,
                                        CreatedOn = DateTime.Now
                                    };
                                    pat.tbl_trx_CoInventor.Add(tcoinventor);
                                    pat.SaveChanges();
                                }
                                ++ti;
                            }
                            int tj = 1;
                            foreach (var appln in model.Appl)
                            {
                                if (appln.Organisation != null)
                                {
                                    tbl_trx_Applicants tapplicants = new tbl_trx_Applicants()
                                    {
                                        FileNo = model.FileNo,
                                        Sno = tj,
                                        Organisation = appln.Organisation,
                                        ContactName = appln.ContactName,
                                        Position = appln.Position,
                                        Address = appln.Address,
                                        ContactNo = appln.ContactNo,
                                        EmailId = appln.EmailId,
                                        VersionId = ver,
                                        CreatedBy = uname,
                                        CreatedOn = DateTime.Now
                                    };
                                    pat.tbl_trx_Applicants.Add(tapplicants);
                                    pat.SaveChanges();
                                    ++tj;
                                }
                            }
                            int tz = 1; int tindex = 0;
                            foreach (var ind in model.Annex.ListIndustry)
                            {
                                if (ind.Selected == true)
                                {
                                    tbl_trx_ApplicationAreas ta = new tbl_trx_ApplicationAreas()
                                    {
                                        Sno = tz,
                                        FileNo = model.FileNo,
                                        Areas = ind.Value,
                                        Index = tindex,
                                        VersionId = ver,
                                        Category = "ApplicationIndustry"
                                    };
                                    pat.tbl_trx_ApplicationAreas.Add(ta);
                                    pat.SaveChanges();
                                    ++tz; ++tindex;
                                }
                                else { ++tindex; }
                            }
                            int tax = 1; int tindex1 = 0;
                            foreach (var ind1 in model.Annex.ListIndustry1)
                            {
                                if (ind1.Selected == true)
                                {
                                    tbl_trx_ApplicationAreas ta = new tbl_trx_ApplicationAreas()
                                    {
                                        Sno = tax,
                                        FileNo = model.FileNo,
                                        Areas = ind1.Value,
                                        Index = tindex1,
                                        VersionId = ver,
                                        Category = "SubIndustry"
                                    };
                                    pat.tbl_trx_ApplicationAreas.Add(ta);
                                    pat.SaveChanges();
                                    ++tax; ++tindex1;
                                }
                                else { ++tindex1; }
                            }
                            int tay = 1; int tModeInd = 0;
                            foreach (var m1 in model.Annex.IITMode)
                            {
                                if (m1.Selected == true)
                                {
                                    tbl_trx_CommercialisationMode a = new tbl_trx_CommercialisationMode()
                                    {
                                        Sno = tay,
                                        FileNo = model.FileNo,
                                        Mode = m1.Value,
                                        IndNo = tModeInd,
                                        VersionId = ver,
                                        Category = "IIT"
                                    };
                                    pat.tbl_trx_CommercialisationMode.Add(a);
                                    pat.SaveChanges();
                                    ++tay; ++tModeInd;
                                }
                                else { ++tModeInd; }
                            }
                            int taz = 1; int tModeJoint = 0;
                            foreach (var m1 in model.Annex.JointMode)
                            {
                                if (m1.Selected == true)
                                {
                                    tbl_trx_CommercialisationMode a = new tbl_trx_CommercialisationMode()
                                    {
                                        Sno = taz,
                                        FileNo = model.FileNo,
                                        Mode = m1.Value,
                                        IndNo = tModeJoint,
                                        VersionId = ver,
                                        Category = "Joint"
                                    };
                                    pat.tbl_trx_CommercialisationMode.Add(a);
                                    pat.SaveChanges();
                                    ++taz; ++tModeJoint;
                                }
                                else { ++tModeJoint; }
                            }
                            if (model.IDFType == "Copyright")
                            {
                                if (model.CR != null)
                                {
                                    tbl_trx_Copyright tcr = new tbl_trx_Copyright()
                                    {
                                        VersionId = ver,
                                        Category = model.CR.Category,
                                        ClassofWork = model.CR.ClassofWork,
                                        Description = model.CR.Description,
                                        Details = model.CR.Details,
                                        FileNo = model.FileNo,
                                        isPublished = model.CR.isPublished,
                                        isRegistered = model.CR.isRegistered,
                                        Language = model.CR.Language,
                                        Nature = model.CR.Nature,
                                        Original = model.CR.Original,
                                        Title = model.CR.Title
                                    };
                                    pat.tbl_trx_Copyright.Add(tcr);
                                    pat.SaveChanges();
                                    int cra = 1;
                                    foreach (var a in model.CR.Author)
                                    {
                                        if (a.AUName != null)
                                        {
                                            tbl_trx_CR_Author txauth = new tbl_trx_CR_Author()
                                            {
                                                AUName = a.AUName,
                                                AUAddress = a.AUAddress,
                                                AUNationality = a.AUNationality,
                                                FileNo = model.FileNo,
                                                SNo = cra,
                                                VersionId = ver,
                                                deceasedDt = a.deceasedDt,
                                                isDeceased = a.isDeceased,
                                                createdBy = uname,
                                                createdOn = DateTime.Now
                                            }; pat.tbl_trx_CR_Author.Add(txauth);
                                            pat.SaveChanges();
                                            ++cra;
                                        }


                                    }
                                    int crp = 1;
                                    foreach (var p in model.CR.Publish)
                                    {
                                        if (p.PUName != null)
                                        {
                                            tbl_trx_CR_Publish txpub = new tbl_trx_CR_Publish()
                                            {
                                                PUName = p.PUName,
                                                Country = p.Country,
                                                FileNo = model.FileNo,
                                                PUAddress = p.PUAddress,
                                                PUNationality = p.PUNationality,
                                                Sno = crp,
                                                VersionId = ver,
                                                Year = p.Year
                                            };
                                            pat.tbl_trx_CR_Publish.Add(txpub);
                                            pat.SaveChanges();
                                            ++crp;
                                        }
                                    }


                                }
                            }
                            if (model.IDFType == "Trademark")
                            {
                                if (model.Trade != null)
                                {
                                    tbl_trx_Trademark ttr = new tbl_trx_Trademark()
                                    {
                                        VersionId = ver,
                                        Category = model.Trade.Category,
                                       Class=model.Trade.Class,
                                       Description=model.Trade.Description,
                                       FileNo=model.FileNo,
                                       Language=model.Trade.Language,
                                       TMImage=model.Trade.TMImage,
                                       TMName=model.Trade.TMName,
                                       TMStatement=model.Trade.TMStatement
                                    };
                                    pat.tbl_trx_Trademark.Add(ttr);
                                    pat.SaveChanges();
                                    int tap = 1;
                                    foreach (var a in model.Trade.TAppl)
                                    {
                                        if (a.Organisation != null)
                                        {
                                            tbl_trx_Trade_Applicantdetail txapp = new tbl_trx_Trade_Applicantdetail()
                                            {
                                                Organisation=a.Organisation,
                                                FileNo = model.FileNo,
                                                AddressOfService=a.AddressOfService,
                                                Country=a.Country,
                                                Jurisdiction=a.Jurisdiction,
                                                LegalStatus=a.LegalStatus,
                                                Nature=a.Nature,
                                                Sno=tap,
                                                VersionId = ver,
                                                CreatedBy=uname,
                                                CreatedOn=DateTime.Now                                                
                                            }; pat.tbl_trx_Trade_Applicantdetail.Add(txapp);
                                            pat.SaveChanges();
                                            ++tap;
                                        }


                                    }
                                    int crp = 1;
                                    foreach (var p in model.CR.Publish)
                                    {
                                        if (p.PUName != null)
                                        {
                                            tbl_trx_CR_Publish txpub = new tbl_trx_CR_Publish()
                                            {
                                                PUName = p.PUName,
                                                Country = p.Country,
                                                FileNo = model.FileNo,
                                                PUAddress = p.PUAddress,
                                                PUNationality = p.PUNationality,
                                                Sno = crp,
                                                VersionId = ver,
                                                Year = p.Year
                                            };
                                            pat.tbl_trx_CR_Publish.Add(txpub);
                                            pat.SaveChanges();
                                            ++crp;
                                        }
                                    }
                                }                            }
                            else if (model.IDFType == "DesignPatent")
                            {
                                int tsn = 1; int tindexcl = 0;
                                var tcl = pat.tbl_trx_DesignClasses.Where(m => m.FileNo == model.FileNo).ToList();
                                if (tcl.Count > 0)
                                {
                                    pat.tbl_trx_DesignClasses.RemoveRange(tcl);
                                    pat.SaveChanges();
                                }
                                foreach (var dc in model.DesignClass.ClassList)
                                {
                                    if (dc.Selected == true)
                                    {
                                        tbl_trx_DesignClasses a = new tbl_trx_DesignClasses()
                                        {
                                            Sno = tsn,
                                            FileNo = model.FileNo,
                                            Class =dc.Value,
                                            Index = tindexcl
                                        };
                                        pat.tbl_trx_DesignClasses.Add(a);
                                        pat.SaveChanges();
                                        ++tsn; ++tindexcl;
                                    }
                                    else { ++tindexcl; }
                                }
                                pat.SaveChanges();

                            }
                            string filepath = pat.tbl_mst_filepath.Where(m => m.Category == "PatentDocument").Select(m => m.FilePath).FirstOrDefault();
                            if (!Directory.Exists(filepath))
                            {
                                return "Path Error";
                            }
                            filepath = filepath + model.FileNo + "\\IDFRequest\\";
                            if (!Directory.Exists(filepath))
                            {
                                Directory.CreateDirectory(filepath);
                            }
                            int upfiles = pat.tbl_files_PatentRequest.Where(m => m.FileNo == model.FileNo).Count();
                            int f = upfiles + 1;
                            foreach (var item in file)
                            {
                                if (item != null && item.ContentLength > 0)
                                {
                                    tbl_files_PatentRequest ufiles = new tbl_files_PatentRequest()
                                    {
                                        FileNo = model.FileNo,
                                        DocId = f,
                                        DocName = item.FileName,
                                        DocPath = filepath
                                    };
                                    pat.tbl_files_PatentRequest.Add(ufiles);
                                    item.SaveAs(filepath + item.FileName);
                                    pat.SaveChanges();
                                }
                            }

                            pat.SaveChanges();
                        }
                        else
                        {
                            var tridf = pat.tbl_trx_IDFRequest.Where(m => m.FileNo == model.FileNo).OrderByDescending(m=>m.VersionId).FirstOrDefault();
                            if (tridf != null)
                            {   
                                tridf.PrimaryInventorName = model.PrimaryInventorName;
                                tridf.PrimaryInventorType = model.PrimaryInventorType;
                                tridf.PIDepartment = model.PIDepartment;
                                tridf.PIContactNo = model.PIContactNo;
                                tridf.PIEmailId = model.PIEmailId;
                                tridf.PIInstId = model.PIInstId;
                                tridf.FirstApplicantName = model.FirstApplicantName;
                                tridf.FirstApplicantPosition = model.FirstApplicantPosition;
                                tridf.FirstApplicantAddress = model.FirstApplicantAddress;
                                tridf.FirstApplicantContactNo = model.FirstApplicantContactNo;
                                tridf.FirstApplicantEmailId = model.FirstApplicantEmailId;
                                tridf.FirstApplicantOrganisation = model.FirstApplicantOrganisation;
                                tridf.Title = model.Title;
                                tridf.FieldOfInvention = model.FieldOfInvention;
                                tridf.Summary = model.Summary;
                                tridf.Description = model.Description;
                                tridf.PriorPublication = model.PriorPublication;
                                tridf.SourceOfInvention = model.SourceOfInvention;
                                tridf.SupportInformation = model.SupportInformation;
                                tridf.Disclosure = model.Disclosure;
                                tridf.BiologicalMaterial = model.BiologicalMaterial;
                                tridf.DetailsOfBiologicalMaterial = model.DetailsOfBiologicalMaterial;
                                tridf.RelevantInformation = model.RelevantInformation;
                                tridf.RequestedAction = model.RequestedAction;
                                tridf.RequestedActionOthers = model.RequestedtxtAction ?? model.RequestedCRtxtAction;
                                tridf.ModifiedBy = uname;
                                tridf.ModifiedOn = DateTime.Now;
                                pat.SaveChanges();
                            }
                            if (model.Annex != null)
                            {
                                var tanex = pat.tbl_trx_AnnexureB1.Where(m => m.FileNo == model.FileNo).OrderByDescending(m=>m.VersionId).FirstOrDefault();
                                if (tanex != null)
                                {
                                    tanex.FileNo = model.FileNo;
                                    tanex.BriefDescription = model.Annex.BriefDescription;
                                    tanex.DevelopmentStage = model.Annex.DevelopmentStage;
                                    tanex.OtherInfo = model.Annex.OtherInfo;
                                    tanex.L1Search = model.Annex.L1Search;
                                    tanex.Tool = model.Annex.Tool;
                                    tanex.Party = model.Annex.Party;
                                    tanex.Outcome = model.Annex.Outcome;
                                    tanex.Comments = model.Annex.Comments;                                    
                                    pat.SaveChanges();
                                }
                            }
                            if (model.CoIn.Count > 0)
                            {
                                int a = 0;
                                var co = pat.tbl_trx_CoInventor.Where(m => m.FileNo == model.FileNo && m.VersionId==tridf.VersionId).ToList();
                                if (co.Count == model.CoIn.Count)
                                {
                                    foreach (var m in co)
                                    {
                                        m.FileNo = model.FileNo;
                                        m.Type = model.CoIn[a].Type;
                                        m.Name = model.CoIn[a].Name;
                                        m.Dept = model.CoIn[a].Dept;
                                        m.EmailId = model.CoIn[a].Mail;
                                        m.ContactNo = model.CoIn[a].Ph;
                                        m.InstId = model.CoIn[a].InstId;
                                        m.SNo = ++a;
                                        m.ModifiedBy = uname;
                                        m.ModifiedOn = DateTime.Now.Date;
                                    }
                                    pat.SaveChanges();
                                }
                                else
                                {
                                    if (co != null)
                                    {
                                        pat.tbl_trx_CoInventor.RemoveRange(co);
                                        pat.SaveChanges();
                                    }
                                    foreach (var m in model.CoIn)
                                    {
                                        if (m.Type != null)
                                        {
                                            tbl_trx_CoInventor tco = new tbl_trx_CoInventor()
                                            {
                                                FileNo = model.FileNo,
                                                VersionId=tridf.VersionId,
                                                Type = model.CoIn[a].Type,
                                                Dept = model.CoIn[a].Dept,
                                                EmailId = model.CoIn[a].Mail,
                                                ContactNo = model.CoIn[a].Ph,
                                                InstId = model.CoIn[a].InstId,
                                                Name = model.CoIn[a].Name,
                                                SNo = ++a,
                                                ModifiedBy = uname,                                                
                                                ModifiedOn = DateTime.Now.Date
                                            };
                                            pat.tbl_trx_CoInventor.Add(tco);
                                        }
                                    }
                                    pat.SaveChanges();
                                }
                            }
                            else
                            {
                                var co = pat.tbl_trx_CoInventor.Where(m => m.FileNo == model.FileNo && m.VersionId==tridf.VersionId).ToList();
                                if (co != null)
                                {
                                    pat.tbl_trx_CoInventor.RemoveRange(co);
                                    pat.SaveChanges();
                                }
                            }
                            if (model.Appl.Count > 0)
                            {
                                int a = 0;
                                var ap = pat.tbl_trx_Applicants.Where(m => m.FileNo == model.FileNo && m.VersionId==tridf.VersionId).ToList();
                                if (ap.Count == model.Appl.Count)
                                {
                                    foreach (var m in ap)
                                    {
                                        m.FileNo = model.FileNo;
                                        m.Address = model.Appl[a].Address;
                                        m.ContactName = model.Appl[a].ContactName;
                                        m.ContactNo = model.Appl[a].ContactNo;
                                        m.EmailId = model.Appl[a].EmailId;
                                        m.Organisation = model.Appl[a].Organisation;
                                        m.Position = model.Appl[a].Position;
                                        m.Sno = ++a;
                                        m.ModifiedOn = DateTime.Now.Date;
                                        m.ModifiedBy = uname;
                                    }
                                    pat.SaveChanges();
                                }
                                else
                                {
                                    if (ap != null)
                                    {
                                        pat.tbl_trx_Applicants.RemoveRange(ap);
                                        pat.SaveChanges();
                                    }
                                    foreach (var m in model.Appl)
                                    {
                                        if (m.Organisation != null || m.ContactName != null)
                                        {
                                            tbl_trx_Applicants tsec = new tbl_trx_Applicants()
                                            {
                                                FileNo = model.FileNo,
                                                VersionId=tridf.VersionId,
                                                Address = model.Appl[a].Address,
                                                ContactName = model.Appl[a].ContactName,
                                                ContactNo = model.Appl[a].ContactNo,
                                                EmailId = model.Appl[a].EmailId,
                                                Organisation = model.Appl[a].Organisation,
                                                Position = model.Appl[a].Position,
                                                Sno = ++a,
                                                ModifiedBy = uname,
                                                ModifiedOn = DateTime.Now.Date
                                            };
                                            pat.tbl_trx_Applicants.Add(tsec);
                                        }
                                    }
                                    pat.SaveChanges();
                                }
                            }
                            else
                            {
                                var ap = pat.tbl_trx_Applicants.Where(m => m.FileNo == model.FileNo && m.VersionId==tridf.VersionId).ToList();
                                if (ap != null)
                                {
                                    pat.tbl_trx_Applicants.RemoveRange(ap);
                                    pat.SaveChanges();
                                }
                            }
                            int tz = 1; int tindex = 0;
                            var tindus = pat.tbl_trx_ApplicationAreas.Where(m => m.FileNo == model.FileNo && m.Category == "ApplicationIndustry" && m.VersionId==tridf.VersionId).ToList();
                            if (tindus.Count > 0)
                            {
                                pat.tbl_trx_ApplicationAreas.RemoveRange(tindus);
                                pat.SaveChanges();
                            }
                            foreach (var ind in model.Annex.ListIndustry)
                            {
                                if (ind.Selected == true)
                                {
                                    tbl_trx_ApplicationAreas a = new tbl_trx_ApplicationAreas()
                                    {
                                        Sno = tz,
                                        FileNo = model.FileNo,
                                        Areas = ind.Value,
                                        Index = tindex,
                                        VersionId=tridf.VersionId,
                                        Category = "ApplicationIndustry"
                                    };
                                    pat.tbl_trx_ApplicationAreas.Add(a);
                                    pat.SaveChanges();
                                    ++tz; ++tindex;
                                }
                                else { ++tindex; }
                            }
                            pat.SaveChanges();
                            int tax = 1; int tindex1 = 0;
                            var tindus1 = pat.tbl_trx_ApplicationAreas.Where(m => m.FileNo == model.FileNo && m.Category == "SubIndustry" && m.VersionId==tridf.VersionId).ToList();
                            if (tindus1.Count > 0)
                            {
                                pat.tbl_trx_ApplicationAreas.RemoveRange(tindus1);
                                pat.SaveChanges();
                            }
                            foreach (var ind1 in model.Annex.ListIndustry1)
                            {
                                if (ind1.Selected == true)
                                {
                                    tbl_trx_ApplicationAreas a = new tbl_trx_ApplicationAreas()
                                    {
                                        Sno = tax,
                                        FileNo = model.FileNo,
                                        Areas = ind1.Value,
                                        Index = tindex1,
                                        VersionId=tridf.VersionId,
                                        Category = "SubIndustry"
                                    };
                                    pat.tbl_trx_ApplicationAreas.Add(a);
                                    pat.SaveChanges();
                                    ++tax; ++tindex1;
                                }
                                else { ++tindex1; }
                            }
                            pat.SaveChanges();
                            var timode = pat.tbl_trx_CommercialisationMode.Where(m => m.FileNo == model.FileNo && m.Category == "IIT" && m.VersionId==tridf.VersionId).ToList();
                            if (timode.Count > 0)
                            {
                                pat.tbl_trx_CommercialisationMode.RemoveRange(timode);
                                pat.SaveChanges();
                            }
                            int tay = 1; int tModeInd = 0;
                            foreach (var m1 in model.Annex.IITMode)
                            {
                                if (m1.Selected == true)
                                {
                                    tbl_trx_CommercialisationMode a = new tbl_trx_CommercialisationMode()
                                    {
                                        Sno = tay,
                                        FileNo = model.FileNo,
                                        Mode = m1.Value,
                                        IndNo = tModeInd,
                                        VersionId=tridf.VersionId,
                                        Category = "IIT"
                                    };
                                    pat.tbl_trx_CommercialisationMode.Add(a);
                                    pat.SaveChanges();
                                    ++tay; ++tModeInd;
                                }
                                else { ++tModeInd; }
                            }
                            pat.SaveChanges();
                            var tjmode = pat.tbl_trx_CommercialisationMode.Where(m => m.FileNo == model.FileNo && m.Category == "Joint" && m.VersionId==tridf.VersionId).ToList();
                            if (tjmode.Count > 0)
                            {
                                pat.tbl_trx_CommercialisationMode.RemoveRange(tjmode);
                                pat.SaveChanges();
                            }
                            int taz = 1; int tModeJoint = 0;
                            foreach (var m1 in model.Annex.JointMode)
                            {
                                if (m1.Selected == true)
                                {
                                    tbl_trx_CommercialisationMode a = new tbl_trx_CommercialisationMode()
                                    {
                                        Sno = taz,
                                        FileNo = model.FileNo,
                                        Mode = m1.Value,
                                        IndNo = tModeJoint,
                                        VersionId=tridf.VersionId,
                                        Category = "Joint"
                                    };
                                    pat.tbl_trx_CommercialisationMode.Add(a);
                                    pat.SaveChanges();
                                    ++taz; ++tModeJoint;
                                }
                                else { ++tModeJoint; }
                            }
                            if (model.IDFType == "Copyright")
                            {
                                if (model.CR != null)
                                {
                                    var cr = pat.tbl_trx_Copyright.Where(m => m.FileNo == model.FileNo && m.VersionId==tridf.VersionId).FirstOrDefault();
                                    if (cr != null)
                                    {
                                        cr.Category = model.CR.Category;
                                        cr.ClassofWork = model.CR.ClassofWork;
                                        cr.Description = model.CR.Description;
                                        cr.Details = model.CR.Details;
                                        cr.isPublished = model.CR.isPublished;
                                        cr.isRegistered = model.CR.isRegistered;
                                        cr.Language = model.CR.Language;
                                        cr.Nature = model.CR.Nature;
                                        cr.Original = model.CR.Original;
                                        cr.Title = model.CR.Title;                                        
                                        pat.SaveChanges();
                                    }
                                    else
                                    {
                                        tbl_trx_Copyright tcr = new tbl_trx_Copyright()
                                        {
                                            Category = model.CR.Category,
                                            ClassofWork = model.CR.ClassofWork,
                                            Description = model.CR.Description,
                                            Details = model.CR.Details,
                                            FileNo = model.FileNo,
                                            isPublished = model.CR.isPublished,
                                            isRegistered = model.CR.isRegistered,
                                            Language = model.CR.Language,
                                            Nature = model.CR.Nature,
                                            Original = model.CR.Original,
                                            VersionId=tridf.VersionId,
                                            Title = model.CR.Title
                                        };
                                        pat.tbl_trx_Copyright.Add(tcr);
                                        pat.SaveChanges();
                                    }
                                    var auth = pat.tbl_trx_CR_Author.Where(m => m.FileNo == model.FileNo && m.VersionId==tridf.VersionId).ToList();
                                    if (model.CR.Author.Count > 0)
                                    {
                                        int i = 0;
                                        if (auth.Count == model.CR.Author.Count)
                                        {
                                            foreach (var a in auth)
                                            {
                                                a.FileNo = model.FileNo;
                                                a.AUAddress = model.CR.Author[i].AUAddress;
                                                a.AUName = model.CR.Author[i].AUName;
                                                a.AUNationality = model.CR.Author[i].AUNationality;
                                                a.deceasedDt = model.CR.Author[i].deceasedDt;
                                                a.isDeceased = model.CR.Author[i].isDeceased;
                                                a.createdBy = uname;
                                                a.createdOn = DateTime.Now;                                                
                                                a.SNo = ++i;
                                            }
                                            pat.SaveChanges();
                                        }
                                        else
                                        {
                                            if (auth.Count > 0)
                                            {
                                                pat.tbl_trx_CR_Author.RemoveRange(auth);
                                                pat.SaveChanges();
                                            }
                                            foreach (var a in model.CR.Author)
                                            {
                                                if (a.AUName != null)
                                                {
                                                    tbl_trx_CR_Author tauth = new tbl_trx_CR_Author()
                                                    {
                                                        AUName = model.CR.Author[i].AUName,
                                                        FileNo = model.FileNo,
                                                        AUAddress = model.CR.Author[i].AUAddress,
                                                        AUNationality = model.CR.Author[i].AUNationality,
                                                        isDeceased = model.CR.Author[i].isDeceased,
                                                        deceasedDt = model.CR.Author[i].deceasedDt,
                                                        createdBy = uname,
                                                        createdOn = DateTime.Now,
                                                        VersionId=tridf.VersionId,
                                                        SNo = ++i
                                                    };
                                                    pat.tbl_trx_CR_Author.Add(tauth);
                                                }
                                            }
                                            pat.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        if (auth.Count > 0)
                                        {
                                            pat.tbl_trx_CR_Author.RemoveRange(auth);
                                            pat.SaveChanges();
                                        }
                                    }
                                    var pub = pat.tbl_trx_CR_Publish.Where(m => m.FileNo == model.FileNo && m.VersionId==tridf.VersionId).ToList();
                                    if (model.CR.Publish.Count > 0)
                                    {
                                        int j = 0;
                                        if (pub.Count == model.CR.Publish.Count)
                                        {
                                            foreach (var p in pub)
                                            {
                                                p.FileNo = model.FileNo;
                                                p.Country = model.CR.Publish[j].Country;
                                                p.PUAddress = model.CR.Publish[j].PUAddress;
                                                p.PUName = model.CR.Publish[j].PUName;
                                                p.PUNationality = model.CR.Publish[j].PUNationality;
                                                p.Year = model.CR.Publish[j].Year;
                                                p.Sno = ++j;
                                            }
                                            pat.SaveChanges();
                                        }
                                        else
                                        {
                                            if (pub.Count > 0)
                                            {
                                                pat.tbl_trx_CR_Publish.RemoveRange(pub);
                                                pat.SaveChanges();
                                            }
                                            foreach (var p in model.CR.Publish)
                                            {
                                                if (p.PUName != null)
                                                {
                                                    tbl_trx_CR_Publish tpub = new tbl_trx_CR_Publish()
                                                    {
                                                        Country = model.CR.Publish[j].Country,
                                                        PUName = model.CR.Publish[j].PUName,
                                                        FileNo = model.FileNo,
                                                        PUAddress = model.CR.Publish[j].PUAddress,
                                                        PUNationality = model.CR.Publish[j].PUNationality,
                                                        Year = model.CR.Publish[j].Year,
                                                        VersionId=tridf.VersionId,
                                                        Sno = ++j
                                                    };
                                                    pat.tbl_trx_CR_Publish.Add(tpub);
                                                }
                                            }
                                            pat.SaveChanges();
                                        }
                                    }
                                    else
                                    {
                                        if (pub.Count > 0)
                                        {
                                            pat.tbl_trx_CR_Publish.RemoveRange(pub);
                                            pat.SaveChanges();
                                        }
                                    }
                                }
                            }
                            else if (model.IDFType == "Trademark")
                            {
                                if (model.Trade != null)
                                {
                                    var tr = pat.tbl_trx_Trademark.Where(m => m.FileNo == model.FileNo).OrderByDescending(m=>m.VersionId).FirstOrDefault();
                                    if (tr != null)
                                    {
                                        tr.Category = model.Trade.Category;
                                        tr.Class = model.Trade.Class;
                                        tr.Description = model.Trade.Description;
                                        tr.FileNo = model.FileNo;
                                        tr.Language = model.Trade.Language;
                                        tr.TMImage = model.Trade.TMImage;
                                        tr.TMName = model.Trade.TMName;
                                        tr.TMStatement = model.Trade.TMStatement;
                                        pat.SaveChanges();
                                    }
                                    else
                                    {
                                        tbl_trx_Trademark ttr = new tbl_trx_Trademark()
                                        {
                                            Category = model.Trade.Category,
                                            Class = model.Trade.Class,
                                            Description = model.Trade.Description,
                                            TMStatement = model.Trade.TMStatement,
                                            TMName = model.Trade.TMName,
                                            Language = model.Trade.Language,
                                            FileNo = model.Trade.FileNo,
                                            TMImage = model.Trade.TMImage,
                                            VersionId=tridf.VersionId
                                        };
                                        pat.tbl_trx_Trademark.Add(ttr);
                                        pat.SaveChanges();
                                    }
                                }
                                if (model.Trade.TAppl.Count > 0)
                                {
                                    int a = 0;
                                    var ap = pat.tbl_trx_Trade_Applicantdetail.Where(m => m.FileNo == model.FileNo && m.VersionId==tridf.VersionId).ToList();
                                    if (ap.Count == model.Trade.TAppl.Count)
                                    {
                                        foreach (var m in ap)
                                        {
                                            m.FileNo = model.FileNo;
                                            m.AddressOfService = model.Trade.TAppl[a].AddressOfService;
                                            m.Country = model.Trade.TAppl[a].Country;
                                            m.Jurisdiction = model.Trade.TAppl[a].Jurisdiction;
                                            m.LegalStatus = model.Trade.TAppl[a].LegalStatus;
                                            m.Organisation = model.Trade.TAppl[a].Organisation;
                                            m.Nature = model.Trade.TAppl[a].Nature;
                                            m.Sno = ++a;
                                            m.CreatedOn = DateTime.Now.Date;
                                            m.CreatedBy = uname;
                                        }
                                        pat.SaveChanges();
                                    }
                                    else
                                    {
                                        if (ap != null)
                                        {
                                            pat.tbl_trx_Trade_Applicantdetail.RemoveRange(ap);
                                            pat.SaveChanges();
                                        }
                                        foreach (var m in model.Trade.TAppl)
                                        {
                                            if (m.Organisation != null)
                                            {
                                                tbl_trx_Trade_Applicantdetail ttap = new tbl_trx_Trade_Applicantdetail()
                                                {
                                                    FileNo = model.FileNo,
                                                    VersionId=tridf.VersionId,
                                                    AddressOfService = model.Trade.TAppl[a].AddressOfService,
                                                    Country = model.Trade.TAppl[a].Country,
                                                    Jurisdiction = model.Trade.TAppl[a].Jurisdiction,
                                                    LegalStatus = model.Trade.TAppl[a].LegalStatus,
                                                    Organisation = model.Trade.TAppl[a].Organisation,
                                                    Nature = model.Trade.TAppl[a].Nature,
                                                    Sno = ++a,                                                    
                                                    CreatedBy = uname,
                                                    CreatedOn = DateTime.Now.Date
                                                };
                                                pat.tbl_trx_Trade_Applicantdetail.Add(ttap);
                                            }
                                        }
                                        pat.SaveChanges();
                                    }
                                }
                                else
                                {
                                    var ap = pat.tbl_trx_Trade_Applicantdetail.Where(m => m.FileNo == model.FileNo).ToList();
                                    if (ap != null)
                                    {
                                        pat.tbl_trx_Trade_Applicantdetail.RemoveRange(ap);
                                        pat.SaveChanges();
                                    }
                                }
                            }
                        }
                        transaction.Commit();
                        return "Success";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return ex.ToString();
                    }
                }
            }
        }
        public static List<IDFRequestVM> GetIDFRequestDetails(string uname)
        {
            List<IDFRequestVM> idf = new List<IDFRequestVM>();
            using (var context = new PatentISEntities())
            {
                var query = context.tblIDFRequest.Where(m => m.CreatedBy == uname).Select(m => new { m.FileNo, m.IDFType, m.PrimaryInventorType, m.PrimaryInventorName, m.PIDepartment, m.Status, m.Remarks }).OrderByDescending(m=>m.FileNo).ToList();
                if (query.Count > 0)
                {
                    for (int i = 0; i < query.Count; i++)
                    {
                        idf.Add(new IDFRequestVM()
                        {
                            IDFType = query[i].IDFType,
                            Status = query[i].Status,
                            Remarks = query[i].Remarks,
                            PIDepartment = query[i].PIDepartment,
                            PrimaryInventorName = query[i].PrimaryInventorName,
                            PrimaryInventorType = query[i].PrimaryInventorType,
                            FileNo = query[i].FileNo
                        });
                    }
                }
            }
            return idf;
        }
        public static bool VerifyOpenStatus(long fno)
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var q = pat.tblIDFRequest.Where(m => m.FileNo == fno && (m.Status == "Submitted to IPOffice" || m.Status == "Clarification needed")).FirstOrDefault();
                if (q == null)
                {
                    return false;
                }
                return true;
            }
        }
        public static List<IDFRequestReport> GenerateIDFReport(long fno)
        {
            try
            {
                List<IDFRequestReport> idf = new List<IDFRequestReport>();
                using (PatentISEntities context = new PatentISEntities())
                {
                    idf = context.tblIDFRequest.Where(m => m.FileNo == fno).Select(m => new IDFRequestReport()
                    {
                        FileNo = m.FileNo,
                        IDFType=m.IDFType,
                        FieldOfInvention = m.FieldOfInvention,
                        BiologicalMaterial = m.BiologicalMaterial??false,
                        Description = m.Description,
                        DetailsOfBiologicalMaterial = m.DetailsOfBiologicalMaterial,
                        Disclosure = m.Disclosure,
                        FirstApplicantAddress = m.FirstApplicantAddress,
                        FirstApplicantContactNo = m.FirstApplicantContactNo,
                        FirstApplicantEmailId = m.FirstApplicantEmailId,
                        FirstApplicantName = m.FirstApplicantName,
                        FirstApplicantOrganisation = m.FirstApplicantOrganisation,
                        FirstApplicantPosition = m.FirstApplicantPosition,
                        PIContactNo = m.PIContactNo,
                        PIDepartment = m.PIDepartment,
                        PIEmailId = m.PIEmailId,
                        PIInstId=m.PIInstId,
                        PrimaryInventorType = m.PrimaryInventorType,
                        PrimaryInventorName = m.PrimaryInventorName,
                        PriorPublication = m.PriorPublication,
                        RelevantInformation = m.RelevantInformation,
                        RequestedAction = m.RequestedAction,
                        Summary = m.Summary,
                        SupportInformation = m.SupportInformation,
                        Title = m.Title,
                        SourceOfInvention=m.SourceOfInvention??false
                    }).ToList();
                    if (idf.Count > 0)
                    {
                        var anex = context.tblAnnexureB1.Where(m => m.FileNo == fno).FirstOrDefault();
                        if (anex != null)
                        {
                            idf[0].Annex = (new AnnexrptVM()
                            {
                                BriefDescription = anex.BriefDescription,
                                Comments = anex.Comments,
                                DevelopmentStage = anex.DevelopmentStage,
                                L1Search = anex.L1Search?? false,
                                OtherInfo = anex.OtherInfo,
                                Outcome = anex.Outcome,
                                Party = anex.Party,
                                Tool = anex.Tool
                            });
                        }
                        List<CoInventorVM> coinventor = new List<CoInventorVM>();
                        var coin = context.tblCoInventor.Where(m => m.FileNo == fno).Select(m => m.SNo).ToList();
                        if (coin.Count > 0)
                        {
                            foreach (var a in coin)
                            {
                                var codetail = context.tblCoInventor.Where(m => m.SNo == a && m.FileNo == fno).FirstOrDefault();
                                coinventor.Add(new CoInventorVM()
                                {
                                    FileNo = codetail.FileNo,
                                    SNo = codetail.SNo,
                                    Dept = codetail.Dept,
                                    Name = codetail.Name,
                                    Type = codetail.Type,
                                    Mail = codetail.EmailId,
                                    InstId = codetail.InstId,
                                    Ph = codetail.ContactNo

                                });
                            }
                            idf[0].CoIn = coinventor;
                        }
                        List<ApplicantVM> applicant = new List<ApplicantVM>();
                        var appln = context.tblApplicants.Where(m => m.FileNo == fno).Select(m => m.Sno).ToList();
                        if (appln.Count > 0)
                        {
                            foreach (var a in appln)
                            {
                                var appdetail = context.tblApplicants.Where(m => m.Sno == a && m.FileNo == fno).FirstOrDefault();
                                applicant.Add(new ApplicantVM()
                                {
                                    Sno = appdetail.Sno,
                                    Position = appdetail.Position,
                                    Address = appdetail.Address,
                                    ContactName = appdetail.ContactName,
                                    Organisation = appdetail.Organisation,
                                    EmailId = appdetail.EmailId,
                                    ContactNo = appdetail.ContactNo,
                                    FileNo = appdetail.FileNo
                                });
                            }
                            idf[0].Appl = applicant;
                        }
                        List<ApplicationAreasVM> areas = new List<ApplicationAreasVM>();
                        var area = context.tblApplicationAreas.Where(m => m.FileNo == fno).Select(m => new { m.Areas }).ToList();
                        if (area.Count > 0)
                        {
                            int n = 0;
                            foreach (var a in area)
                            {
                                areas.Add(new ApplicationAreasVM()
                                {
                                    Sno = ++n,
                                    Areas = a.Areas
                                });
                            }
                            idf[0].Annex.AppAreas = areas;
                        }
                        List<CommercialModeVM> modes = new List<CommercialModeVM>();
                        var mode = context.tblCommercialisationMode.Where(m => m.FileNo == fno).Select(m => new { m.Mode }).ToList();
                        if (mode.Count > 0)
                        {
                            foreach (var a in mode)
                            {
                                modes.Add(new CommercialModeVM()
                                {
                                    Mode = a.Mode
                                });
                            }
                            idf[0].Annex.Mode = modes;
                        }
                        if (idf[0].IDFType == "Trademark")
                        {
                            var tr = context.tbl_Trademark.Where(m => m.FileNo == fno).FirstOrDefault();
                            if (tr != null)
                            {
                                idf[0].Trade = (new TraderptVM()
                                {
                                    Category = tr.Category,
                                    Class = tr.Class,
                                    Description = tr.Description,
                                    Language = tr.Language,
                                    TMName = tr.TMName,
                                    TMImage = tr.TMImage,
                                    TMStatement = tr.TMStatement
                                });
                            }
                            List<TradeApplicantrptVM> tapps = new List<TradeApplicantrptVM>();
                            var tapp = context.tbl_Trade_Applicantdetail.Where(m => m.FileNo == fno).ToList();
                            if (tapp.Count > 0)
                            {
                                int n = 0;
                                foreach (var a in tapp)
                                {
                                    tapps.Add(new TradeApplicantrptVM()
                                    {
                                        AddressOfService = a.AddressOfService,
                                        Country = a.Country,
                                        Jurisdiction = a.Jurisdiction,
                                        LegalStatus = a.LegalStatus,
                                        Nature = a.Nature,
                                        Organisation = a.Organisation,
                                        Sno = ++n
                                    });
                                }
                                idf[0].Trade.TAppl = tapps;
                            }
                        }
                        else if (idf[0].IDFType == "Copyright")
                        {
                            var cr = context.tbl_Copyright.Where(m => m.FileNo == fno).FirstOrDefault();
                            if (cr != null)
                            {
                                idf[0].CR = (new CopyRightrptVM()
                                {
                                    Category = cr.Category,
                                    ClassofWork = cr.ClassofWork,
                                    Description = cr.Description,
                                    Details = cr.Details,
                                    isPublished = cr.isPublished,
                                    Language = cr.Language,
                                    isRegistered = cr.isRegistered,
                                    Nature = cr.Nature,
                                    Original = cr.Original,
                                    Title = cr.Title
                                });
                            }
                            List<CRAuthorrptVM> authors = new List<CRAuthorrptVM>();
                            var author = context.tbl_CR_Author.Where(m => m.FileNo == fno).ToList();
                            if (author.Count > 0)
                            {
                                int n = 0;
                                foreach (var a in author)
                                {
                                    authors.Add(new CRAuthorrptVM()
                                    {
                                        AUAddress = a.AUAddress,
                                        AUName = a.AUName,
                                        AUNationality = a.AUNationality,
                                        isDeceased = a.isDeceased ?? false,
                                        deceasedDt = a.deceasedDt ?? DateTime.Now,
                                        SNo = ++n
                                    });
                                }
                                idf[0].CR.Author = authors;
                            }
                            List<CRPublishrptVM> pubs = new List<CRPublishrptVM>();
                            var pub = context.tbl_CR_Publish.Where(m => m.FileNo == fno).ToList();
                            if (pub.Count > 0)
                            {
                                int n = 0;
                                foreach (var a in pub)
                                {
                                    pubs.Add(new CRPublishrptVM()
                                    {
                                        Country = a.Country,
                                        PUAddress = a.PUAddress,
                                        PUName = a.PUName,
                                        PUNationality = a.PUNationality,
                                        Year = a.Year ?? 0,
                                        Sno = ++n
                                    });
                                }
                                idf[0].CR.Publish = pubs;
                            }
                        }
                    }
                    else
                        return null;
                }

                return idf;

            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region Draft
        public static IDFRequestVM EditDraft(IDFRequestVM vm)
        {
            try
            {
                IDFRequestVM idf = new IDFRequestVM();
                using (var context = new PatentISEntities())
                {
                    var query = context.tbl_draft_IDFReq.Where(m => m.DraftNo == vm.DraftNo).FirstOrDefault();
                    if (query != null)
                    {
                        idf.DraftNo = vm.DraftNo;
                        idf.IDFType = query.IDFType;
                        idf.FileNo = vm.FileNo;
                        idf.FieldOfInvention = query.FieldOfInvention;
                        idf.BiologicalMaterial = query.BiologicalMaterial;
                        idf.Description = query.Description;
                        idf.DetailsOfBiologicalMaterial = query.DetailsOfBiologicalMaterial;
                        idf.Disclosure = query.Disclosure;
                        idf.FirstApplicantAddress = query.FirstApplicantAddress;
                        idf.FirstApplicantContactNo = query.FirstApplicantContactNo;
                        idf.FirstApplicantEmailId = query.FirstApplicantEmailId;
                        idf.FirstApplicantName = query.FirstApplicantName;
                        idf.FirstApplicantOrganisation = query.FirstApplicantOrganisation;
                        idf.FirstApplicantPosition = query.FirstApplicantPosition;
                        idf.PIContactNo = query.PIContactNo;
                        idf.PIDepartment = query.PIDepartment;
                        idf.PIEmailId = query.PIEmailId;
                        idf.PIInstId = query.InstId;
                        idf.PrimaryInventorType = query.PrimaryInventorType;
                        idf.PrimaryInventorName = query.PrimaryInventorName;
                        idf.PriorPublication = query.PriorPublication;
                        idf.RelevantInformation = query.RelevantInformation;
                        idf.RequestedAction = query.RequestedAction;
                        if (idf.IDFType == "Trademark")
                            idf.RequestedTMAction = idf.RequestedAction;
                        else if (idf.IDFType == "Copyright")
                            idf.RequestedCRAction = idf.RequestedAction;
                        else if (idf.IDFType == "DesignPatent")                        
                            idf.RequestedDPAction = idf.RequestedAction;
                        
                        idf.SourceOfInvention = query.SourceOfInvention;
                        idf.Summary = query.Summary;
                        idf.SupportInformation = query.SupportInformation;
                        idf.Title = query.Title;
                        idf.draftUpdate = true;
                        var anex = context.tbl_draft_AnnexB1.Where(m => m.DraftNo == vm.DraftNo).ToList();
                        foreach (var a in anex)
                        {
                            idf.Annex.BriefDescription = a.BriefDescription;
                            idf.Annex.Comments = a.Comments;
                            idf.Annex.DevelopmentStage = a.DevelopmentStage;
                            idf.Annex.L1Search = a.L1Search;
                            idf.Annex.OtherInfo = a.OtherInfo;
                            idf.Annex.Outcome = a.Outcome;
                            idf.Annex.Party = a.Party;
                            idf.Annex.Tool = a.Tool;
                        }
                        List<CoInventorVM> coinventor = new List<CoInventorVM>();
                        var coin = context.tbl_draft_Coinventor.Where(m => m.DraftNo == vm.DraftNo).Select(m => m.SNo).ToList();
                        if (coin.Count > 0)
                        {
                            foreach (var a in coin)
                            {
                                var codetail = context.tbl_draft_Coinventor.Where(m => m.SNo == a && m.DraftNo == vm.DraftNo).FirstOrDefault();
                                coinventor.Add(new CoInventorVM()
                                {
                                    SNo = codetail.SNo,
                                    Dept = codetail.Dept,
                                    Name = codetail.Name,
                                    Type = codetail.Type,
                                    Mail = codetail.EmailId,
                                    InstId = codetail.InstId,
                                    Ph = codetail.ContactNo
                                });
                            }
                            idf.CoIn = coinventor;
                        }
                        List<ApplicantVM> applicant = new List<ApplicantVM>();
                        var appln = context.tbl_draft_applicants.Where(m => m.DraftNo == vm.DraftNo).Select(m => m.Sno).ToList();
                        if (appln.Count > 0)
                        {
                            foreach (var a in appln)
                            {
                                var appdetail = context.tbl_draft_applicants.Where(m => m.Sno == a && m.DraftNo == vm.DraftNo).FirstOrDefault();
                                applicant.Add(new ApplicantVM()
                                {
                                    Sno = appdetail.Sno,
                                    Position = appdetail.Position,
                                    Address = appdetail.Address,
                                    ContactName = appdetail.ContactName,
                                    Organisation = appdetail.Organisation,
                                    EmailId = appdetail.EmailId,
                                    ContactNo = appdetail.ContactNo
                                });
                            }
                            idf.Appl = applicant;
                        }
                        idf.Annex.ListIndustry = vm.Annex.ListIndustry;
                        idf.Annex.ListIndustry1 = vm.Annex.ListIndustry1;
                        var area = context.tbl_draft_AppAreas.Where(m => m.DraftNo == vm.DraftNo).Select(m => new { m.Index, m.Category }).ToList();
                        if (area.Count > 0)
                        {
                            foreach (var a in area)
                            {
                                if (a.Category == "ApplicationIndustry")
                                {
                                    idf.Annex.ListIndustry[a.Index].Selected = true;
                                }
                                else
                                {
                                    idf.Annex.ListIndustry1[a.Index].Selected = true;
                                }
                            }
                        }
                        idf.Annex.IITMode = vm.Annex.IITMode;
                        idf.Annex.JointMode = vm.Annex.JointMode;
                        var mode = context.tbl_draft_comMode.Where(m => m.DraftNo == vm.DraftNo).Select(m => new { m.IndNo, m.Category }).ToList();
                        if (mode.Count > 0)
                        {
                            foreach (var a in mode)
                            {
                                if (a.Category == "IIT")
                                {
                                    idf.Annex.IITMode[a.IndNo].Selected = true;
                                }
                                else
                                {
                                    idf.Annex.JointMode[a.IndNo].Selected = true;
                                }
                            }
                        }
                        if (idf.IDFType == "Copyright")
                        {
                            var cr = context.tbl_draft_Copyright.Where(m => m.DraftNo == vm.DraftNo).FirstOrDefault();
                            if (cr != null)
                            {
                                idf.CR.Category = cr.Category;
                                idf.CR.ClassofWork = cr.ClassofWork;
                                idf.CR.Description = cr.Description;
                                idf.CR.Details = cr.Details;
                                idf.CR.isPublished = cr.isPublished ?? false;
                                idf.CR.isRegistered = cr.isRegistered ?? false;
                                idf.CR.Language = cr.Language;
                                idf.CR.Nature = cr.Nature;
                                idf.CR.Original = cr.Original;
                                idf.CR.Title = cr.Title;
                            }
                            List<CRAuthorVM> authvm = new List<CRAuthorVM>();
                            var auth = context.tbl_draft_CR_Author.Where(m => m.DraftNo == vm.DraftNo).Select(m => m.SNo).ToList();
                            if (auth.Count > 0)
                            {
                                foreach (var a in auth)
                                {
                                    var authdetail = context.tbl_draft_CR_Author.Where(m => m.DraftNo == vm.DraftNo && m.SNo == a).FirstOrDefault();
                                    authvm.Add(new CRAuthorVM()
                                    {
                                        AUAddress = authdetail.AUAddress,
                                        AUName = authdetail.AUName,
                                        AUNationality = authdetail.AUNationality,
                                        isDeceased = authdetail.isDeceased,
                                        SNo = authdetail.SNo,
                                        deceasedDt = authdetail.deceasedDt
                                    });
                                }
                                idf.CR.Author = authvm;
                            }
                            List<CRPublishVM> pubvm = new List<CRPublishVM>();
                            var pub = context.tbl_draft_CR_Publish.Where(m => m.DraftNo == vm.DraftNo).Select(m => m.Sno).ToList();
                            if (pub.Count > 0)
                            {
                                foreach (var a in pub)
                                {
                                    var pubdetail = context.tbl_draft_CR_Publish.Where(m => m.DraftNo == vm.DraftNo && m.Sno == a).FirstOrDefault();
                                    pubvm.Add(new CRPublishVM()
                                    {
                                        Country = pubdetail.Country,
                                        PUAddress = pubdetail.PUAddress,
                                        PUName = pubdetail.PUName,
                                        PUNationality = pubdetail.PUNationality,
                                        Year = pubdetail.Year
                                    });
                                }
                                idf.CR.Publish = pubvm;
                            }
                        }
                        else if (idf.IDFType == "Trademark")
                        {
                            var tr = context.tbl_draft_Trademark.Where(m => m.DraftNo == vm.DraftNo).FirstOrDefault();
                            if (tr != null)
                            {
                                idf.Trade.FileNo = vm.FileNo;
                                idf.Trade.Language = tr.Language;
                                idf.Trade.TMName = tr.TMName;
                                idf.Trade.TMImage = tr.TMImage ?? false;
                                idf.Trade.TMStatement = tr.TMStatement;
                                idf.Trade.Category = tr.Category;
                                idf.Trade.Class = tr.Class;
                                idf.Trade.Description = tr.Description;
                            }
                            List<TradeApplicantVM> tradevm = new List<TradeApplicantVM>();
                            var trade = context.tbl_draft_Trade_Applicantdetail.Where(m => m.DraftNo == vm.DraftNo).Select(m => m.Sno).ToList();
                            if (trade.Count > 0)
                            {
                                foreach (var a in trade)
                                {
                                    var appdetail = context.tbl_draft_Trade_Applicantdetail.Where(m => m.DraftNo == vm.DraftNo && m.Sno == a).FirstOrDefault();
                                    tradevm.Add(new TradeApplicantVM()
                                    {
                                        AddressOfService = appdetail.AddressOfService,
                                        Country = appdetail.Country,
                                        Organisation = appdetail.Organisation,
                                        Nature = appdetail.Nature,
                                        Sno = appdetail.Sno,
                                        Jurisdiction = appdetail.Jurisdiction,
                                        LegalStatus = appdetail.LegalStatus
                                    });
                                }
                                idf.Trade.TAppl = tradevm;
                            }
                        }
                    }

                    return idf;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public static string InsertDraft(IDFRequestVM model, string uname)
        {

            using (PatentISEntities pat = new PatentISEntities())
            {

                using (var transaction = pat.Database.BeginTransaction())
                {
                    try
                    {
                        tbl_draft_IDFReq dis = new tbl_draft_IDFReq()
                        {
                            DraftNo = "D" + model.FileNo,
                            IDFType = model.IDFType,
                            PrimaryInventorName = model.PrimaryInventorName,
                            PrimaryInventorType = model.PrimaryInventorType,
                            PIDepartment = model.PIDepartment,
                            PIContactNo = model.PIContactNo,
                            PIEmailId = model.PIEmailId,
                            InstId = model.PIInstId,
                            FirstApplicantName = model.FirstApplicantName,
                            FirstApplicantPosition = model.FirstApplicantPosition,
                            FirstApplicantAddress = model.FirstApplicantAddress,
                            FirstApplicantContactNo = model.FirstApplicantContactNo,
                            FirstApplicantEmailId = model.FirstApplicantEmailId,
                            FirstApplicantOrganisation = model.FirstApplicantOrganisation,
                            Title = model.Title,
                            FieldOfInvention = model.FieldOfInvention,
                            Summary = model.Summary,
                            Description = model.Description,
                            PriorPublication = model.PriorPublication,
                            SourceOfInvention = model.SourceOfInvention,
                            SupportInformation = model.SupportInformation,
                            Disclosure = model.Disclosure,
                            BiologicalMaterial = model.BiologicalMaterial,
                            DetailsOfBiologicalMaterial = model.DetailsOfBiologicalMaterial,
                            RelevantInformation = model.RelevantInformation,
                            RequestedAction = model.RequestedAction,
                            Status = "Not yet submitted",
                            CreatedBy = uname,
                            CreatedOn = DateTime.Now
                        };
                        pat.tbl_draft_IDFReq.Add(dis);
                        pat.SaveChanges();
                        tbl_draft_AnnexB1 annex = new tbl_draft_AnnexB1()
                        {
                            DraftNo = "D" + model.FileNo,
                            BriefDescription = model.Annex.BriefDescription,
                            DevelopmentStage = model.Annex.DevelopmentStage,
                            OtherInfo = model.Annex.OtherInfo,
                            L1Search = model.Annex.L1Search,
                            Tool = model.Annex.Tool,
                            Party = model.Annex.Party,
                            Outcome = model.Annex.Outcome,
                            Comments = model.Annex.Comments
                        };
                        pat.tbl_draft_AnnexB1.Add(annex);
                        pat.SaveChanges();
                        int i = 1;
                        foreach (var coinven in model.CoIn)
                        {
                            if (coinven.Type != null)
                            {
                                tbl_draft_Coinventor coinventor = new tbl_draft_Coinventor()
                                {
                                    SNo = i,
                                    Type = coinven.Type,
                                    Name = coinven.Name,
                                    DraftNo = "D" + model.FileNo,
                                    Dept = coinven.Dept,
                                    ContactNo = coinven.Ph,
                                    EmailId = coinven.Mail,
                                    InstId = coinven.InstId,
                                    CreatedBy = uname,
                                    CreatedOn = DateTime.Now
                                };
                                pat.tbl_draft_Coinventor.Add(coinventor);
                                pat.SaveChanges();
                            }
                            ++i;
                        }
                        int j = 1;
                        foreach (var appln in model.Appl)
                        {
                            if (appln.Organisation != null)
                            {
                                tbl_draft_applicants applicants = new tbl_draft_applicants()
                                {
                                    DraftNo = "D" + model.FileNo,
                                    Sno = j,
                                    Organisation = appln.Organisation,
                                    ContactName = appln.ContactName,
                                    Position = appln.Position,
                                    Address = appln.Address,
                                    ContactNo = appln.ContactNo,
                                    EmailId = appln.EmailId,
                                    CreatedBy = uname,
                                    CreatedOn = DateTime.Now
                                };
                                pat.tbl_draft_applicants.Add(applicants);
                                pat.SaveChanges();
                                ++j;
                            }
                        }
                        int z = 1; int index = 0;
                        foreach (var ind in model.Annex.ListIndustry)
                        {
                            if (ind.Selected == true)
                            {
                                tbl_draft_AppAreas a = new tbl_draft_AppAreas()
                                {
                                    Sno = z,
                                    DraftNo = "D" + model.FileNo,
                                    Areas = ind.Value,
                                    Index = index,
                                    Category = "ApplicationIndustry"
                                };
                                pat.tbl_draft_AppAreas.Add(a);
                                pat.SaveChanges();
                                ++z; ++index;
                            }
                            else { ++index; }
                        }
                        int ax = 1; int index1 = 0;
                        foreach (var ind1 in model.Annex.ListIndustry1)
                        {
                            if (ind1.Selected == true)
                            {
                                tbl_draft_AppAreas a = new tbl_draft_AppAreas()
                                {
                                    Sno = ax,
                                    DraftNo = "D" + model.FileNo,
                                    Areas = ind1.Value,
                                    Index = index1,
                                    Category = "SubIndustry"
                                };
                                pat.tbl_draft_AppAreas.Add(a);
                                pat.SaveChanges();
                                ++ax; ++index1;
                            }
                            else { ++index1; }
                        }
                        int ay = 1; int ModeInd = 0;
                        foreach (var m1 in model.Annex.IITMode)
                        {
                            if (m1.Selected == true)
                            {
                                tbl_draft_comMode a = new tbl_draft_comMode()
                                {
                                    Sno = ay,
                                    DraftNo = "D" + model.FileNo,
                                    Mode = m1.Value,
                                    IndNo = ModeInd,
                                    Category = "IIT"
                                };
                                pat.tbl_draft_comMode.Add(a);
                                pat.SaveChanges();
                                ++ay; ++ModeInd;
                            }
                            else { ++ModeInd; }
                        }
                        int az = 1; int ModeJoint = 0;
                        foreach (var m1 in model.Annex.JointMode)
                        {
                            if (m1.Selected == true)
                            {
                                tbl_draft_comMode a = new tbl_draft_comMode()
                                {
                                    Sno = az,
                                    DraftNo = "D" + model.FileNo,
                                    Mode = m1.Value,
                                    IndNo = ModeJoint,
                                    Category = "Joint"
                                };
                                pat.tbl_draft_comMode.Add(a);
                                pat.SaveChanges();
                                ++az; ++ModeJoint;
                            }
                            else { ++ModeJoint; }
                        }
                        if (model.IDFType == "Copyright")
                        {
                            if (model.CR != null)
                            {
                                tbl_draft_Copyright dcr = new tbl_draft_Copyright()
                                {
                                    Category = model.CR.Category,
                                    ClassofWork = model.CR.ClassofWork,
                                    Description = model.CR.Description,
                                    Details = model.CR.Details,
                                    DraftNo = "D" + model.FileNo,
                                    isPublished = model.CR.isPublished,
                                    isRegistered = model.CR.isRegistered,
                                    Language = model.CR.Language,
                                    Nature = model.CR.Nature,
                                    Original = model.CR.Original,
                                    Title = model.CR.Title
                                };
                                pat.tbl_draft_Copyright.Add(dcr);
                                pat.SaveChanges();
                            }
                            int cr = 1;
                            foreach (var auth in model.CR.Author)
                            {
                                if (auth.AUName != null)
                                {
                                    tbl_draft_CR_Author author = new tbl_draft_CR_Author()
                                    {
                                        SNo = cr,
                                        AUName = auth.AUName,
                                        AUAddress = auth.AUAddress,
                                        AUNationality = auth.AUNationality,
                                        createdBy = uname,
                                        createdOn = DateTime.Now,
                                        deceasedDt = auth.deceasedDt,
                                        DraftNo = "D" + model.FileNo,
                                        isDeceased = auth.isDeceased
                                    };
                                    pat.tbl_draft_CR_Author.Add(author);
                                    pat.SaveChanges();
                                }
                            }
                            int pub = 1;
                            foreach (var publ in model.CR.Publish)
                            {
                                if (publ.PUName != null)
                                {
                                    tbl_draft_CR_Publish publish = new tbl_draft_CR_Publish()
                                    {
                                        PUName = publ.PUName,
                                        Country = publ.Country,
                                        DraftNo = "D" + model.FileNo,
                                        PUAddress = publ.PUAddress,
                                        PUNationality = publ.PUNationality,
                                        Sno = pub,
                                        Year = publ.Year
                                    };
                                    pat.tbl_draft_CR_Publish.Add(publish);
                                    pat.SaveChanges();
                                }
                            }
                        }
                        if (model.IDFType == "Trademark")
                        {
                            if (model.Trade != null)
                            {
                                tbl_draft_Trademark dtm = new tbl_draft_Trademark()
                                {
                                    Category = model.Trade.Category,
                                    Class = model.Trade.Class,
                                    Description = model.Trade.Description,
                                    DraftNo = "D" + model.FileNo,
                                    Language = model.Trade.Language,
                                    TMImage = model.Trade.TMImage,
                                    TMName = model.Trade.TMName,
                                    TMStatement = model.Trade.TMStatement
                                };
                                pat.tbl_draft_Trademark.Add(dtm);
                                pat.SaveChanges();
                            }
                            if (model.Trade.TAppl.Count > 0)
                            {
                                int aj = 1;
                                foreach (var appln in model.Trade.TAppl)
                                {
                                    if (appln.Organisation != null)
                                    {
                                        tbl_draft_Trade_Applicantdetail dad = new tbl_draft_Trade_Applicantdetail()
                                        {
                                            AddressOfService = appln.AddressOfService,
                                            Country = appln.Country,
                                            CreatedBy = uname,
                                            CreatedOn = DateTime.Now,
                                            DraftNo = "D" + model.FileNo,
                                            Jurisdiction = appln.Jurisdiction,
                                            LegalStatus = appln.LegalStatus,
                                            Nature = appln.Nature,
                                            Organisation = appln.Organisation,
                                            Sno = j
                                        };
                                        pat.tbl_draft_Trade_Applicantdetail.Add(dad);
                                        pat.SaveChanges();
                                        ++aj;
                                    }
                                }
                            }
                        }
                        transaction.Commit();
                        return "Success";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return ex.ToString();
                    }
                }
            }
        }
        public static string UpdateDraft(IDFRequestVM model, string uname)
        {

            using (PatentISEntities pat = new PatentISEntities())
            {

                using (var transaction = pat.Database.BeginTransaction())
                {
                    try
                    {
                        var idf = pat.tbl_draft_IDFReq.Where(m => m.DraftNo == model.DraftNo).FirstOrDefault();
                        if (idf != null)
                        {
                            idf.DraftNo = model.DraftNo;
                            idf.PrimaryInventorName = model.PrimaryInventorName;
                            idf.PrimaryInventorType = model.PrimaryInventorType;
                            idf.PIDepartment = model.PIDepartment;
                            idf.PIContactNo = model.PIContactNo;
                            idf.PIEmailId = model.PIEmailId;
                            idf.InstId = model.PIInstId;
                            idf.FirstApplicantName = model.FirstApplicantName;
                            idf.FirstApplicantPosition = model.FirstApplicantPosition;
                            idf.FirstApplicantAddress = model.FirstApplicantAddress;
                            idf.FirstApplicantContactNo = model.FirstApplicantContactNo;
                            idf.FirstApplicantEmailId = model.FirstApplicantEmailId;
                            idf.FirstApplicantOrganisation = model.FirstApplicantOrganisation;
                            idf.Title = model.Title;
                            idf.FieldOfInvention = model.FieldOfInvention;
                            idf.Summary = model.Summary;
                            idf.Description = model.Description;
                            idf.PriorPublication = model.PriorPublication;
                            idf.SourceOfInvention = model.SourceOfInvention;
                            idf.SupportInformation = model.SupportInformation;
                            idf.Disclosure = model.Disclosure;
                            idf.BiologicalMaterial = model.BiologicalMaterial;
                            idf.DetailsOfBiologicalMaterial = model.DetailsOfBiologicalMaterial;
                            idf.RelevantInformation = model.RelevantInformation;
                            idf.RequestedAction = model.RequestedAction;
                            idf.ModifiedBy = uname;
                            idf.ModifiedOn = DateTime.Now;
                            pat.SaveChanges();
                        }
                        if (model.Annex != null)
                        {
                            var anex = pat.tbl_draft_AnnexB1.Where(m => m.DraftNo == model.DraftNo).FirstOrDefault();
                            if (anex != null)
                            {
                                anex.DraftNo = model.DraftNo;
                                anex.BriefDescription = model.Annex.BriefDescription;
                                anex.DevelopmentStage = model.Annex.DevelopmentStage;
                                anex.OtherInfo = model.Annex.OtherInfo;
                                anex.L1Search = model.Annex.L1Search;
                                anex.Tool = model.Annex.Tool;
                                anex.Party = model.Annex.Party;
                                anex.Outcome = model.Annex.Outcome;
                                anex.Comments = model.Annex.Comments;
                                pat.SaveChanges();
                            }
                        }
                        if (model.CoIn.Count > 0)
                        {
                            int a = 0;
                            var co = pat.tbl_draft_Coinventor.Where(m => m.DraftNo == model.DraftNo).ToList();
                            if (co.Count == model.CoIn.Count)
                            {
                                foreach (var m in co)
                                {
                                    m.DraftNo = model.DraftNo;
                                    m.Type = model.CoIn[a].Type;
                                    m.Name = model.CoIn[a].Name;
                                    m.Dept = model.CoIn[a].Dept;
                                    m.EmailId = model.CoIn[a].Mail;
                                    m.InstId = model.CoIn[a].InstId;
                                    m.ContactNo = model.CoIn[a].Ph;
                                    m.SNo = ++a;
                                    m.ModifiedBy = uname;
                                    m.ModifiedOn = DateTime.Now.Date;
                                }
                                pat.SaveChanges();
                            }
                            else
                            {
                                if (co.Count > 0)
                                {
                                    pat.tbl_draft_Coinventor.RemoveRange(co);
                                    pat.SaveChanges();
                                }
                                foreach (var m in model.CoIn)
                                {
                                    if (m.Type != null)
                                    {
                                        tbl_draft_Coinventor tco = new tbl_draft_Coinventor()
                                        {
                                            DraftNo = model.DraftNo,
                                            Type = model.CoIn[a].Type,
                                            Dept = model.CoIn[a].Dept,
                                            EmailId = model.CoIn[a].Mail,
                                            ContactNo = model.CoIn[a].Ph,
                                            InstId = model.CoIn[a].InstId,
                                            Name = model.CoIn[a].Name,
                                            SNo = ++a,
                                            ModifiedBy = uname,
                                            ModifiedOn = DateTime.Now.Date
                                        };
                                        pat.tbl_draft_Coinventor.Add(tco);
                                    }
                                }
                                pat.SaveChanges();
                            }
                        }
                        else
                        {
                            var co = pat.tbl_draft_Coinventor.Where(m => m.DraftNo == model.DraftNo).ToList();
                            if (co != null)
                            {
                                pat.tbl_draft_Coinventor.RemoveRange(co);
                                pat.SaveChanges();
                            }
                        }
                        if (model.Appl.Count > 0)
                        {
                            int a = 0;
                            var ap = pat.tbl_draft_applicants.Where(m => m.DraftNo == model.DraftNo).ToList();
                            if (ap.Count == model.Appl.Count)
                            {
                                foreach (var m in ap)
                                {
                                    m.DraftNo = model.DraftNo;
                                    m.Address = model.Appl[a].Address;
                                    m.ContactName = model.Appl[a].ContactName;
                                    m.ContactNo = model.Appl[a].ContactNo;
                                    m.EmailId = model.Appl[a].EmailId;
                                    m.Organisation = model.Appl[a].Organisation;
                                    m.Position = model.Appl[a].Position;
                                    m.Sno = ++a;
                                    m.ModifiedOn = DateTime.Now.Date;
                                    m.ModifiedBy = uname;
                                }
                                pat.SaveChanges();
                            }
                            else
                            {
                                if (ap.Count > 0)
                                {
                                    pat.tbl_draft_applicants.RemoveRange(ap);
                                    pat.SaveChanges();
                                }
                                foreach (var m in model.Appl)
                                {
                                    if (m.Organisation != null || m.ContactName != null)
                                    {
                                        tbl_draft_applicants tsec = new tbl_draft_applicants()
                                        {
                                            DraftNo = model.DraftNo,
                                            Address = model.Appl[a].Address,
                                            ContactName = model.Appl[a].ContactName,
                                            ContactNo = model.Appl[a].ContactNo,
                                            EmailId = model.Appl[a].EmailId,
                                            Organisation = model.Appl[a].Organisation,
                                            Position = model.Appl[a].Position,
                                            Sno = ++a,
                                            ModifiedBy = uname,
                                            ModifiedOn = DateTime.Now.Date
                                        };
                                        pat.tbl_draft_applicants.Add(tsec);
                                    }
                                }
                                pat.SaveChanges();
                            }
                        }
                        else
                        {
                            var ap = pat.tbl_draft_applicants.Where(m => m.DraftNo == model.DraftNo).ToList();
                            if (ap != null)
                            {
                                pat.tbl_draft_applicants.RemoveRange(ap);
                                pat.SaveChanges();
                            }
                        }
                        int z = 1; int index = 0;
                        var indus = pat.tbl_draft_AppAreas.Where(m => m.DraftNo == model.DraftNo && m.Category == "ApplicationIndustry").ToList();
                        if (indus.Count > 0)
                        {
                            pat.tbl_draft_AppAreas.RemoveRange(indus);
                            pat.SaveChanges();
                        }
                        foreach (var ind in model.Annex.ListIndustry)
                        {
                            if (ind.Selected == true)
                            {
                                tbl_draft_AppAreas a = new tbl_draft_AppAreas()
                                {
                                    Sno = z,
                                    DraftNo = model.DraftNo,
                                    Areas = ind.Value,
                                    Index = index,
                                    Category = "ApplicationIndustry"
                                };
                                pat.tbl_draft_AppAreas.Add(a);
                                pat.SaveChanges();
                                ++z; ++index;
                            }
                            else { ++index; }
                        }
                        pat.SaveChanges();
                        int ax = 1; int index1 = 0;
                        var indus1 = pat.tbl_draft_AppAreas.Where(m => m.DraftNo == model.DraftNo && m.Category == "SubIndustry").ToList();
                        if (indus1.Count > 0)
                        {
                            pat.tbl_draft_AppAreas.RemoveRange(indus1);
                            pat.SaveChanges();
                        }
                        foreach (var ind1 in model.Annex.ListIndustry1)
                        {
                            if (ind1.Selected == true)
                            {
                                tbl_draft_AppAreas a = new tbl_draft_AppAreas()
                                {
                                    Sno = ax,
                                    DraftNo = model.DraftNo,
                                    Areas = ind1.Value,
                                    Index = index1,
                                    Category = "SubIndustry"
                                };
                                pat.tbl_draft_AppAreas.Add(a);
                                pat.SaveChanges();
                                ++ax; ++index1;
                            }
                            else { ++index1; }
                        }
                        pat.SaveChanges();
                        var imode = pat.tbl_draft_comMode.Where(m => m.DraftNo == model.DraftNo && m.Category == "IIT").ToList();
                        if (imode.Count > 0)
                        {
                            pat.tbl_draft_comMode.RemoveRange(imode);
                            pat.SaveChanges();
                        }
                        int ay = 1; int ModeInd = 0;
                        foreach (var m1 in model.Annex.IITMode)
                        {
                            if (m1.Selected == true)
                            {
                                tbl_draft_comMode a = new tbl_draft_comMode()
                                {
                                    Sno = ay,
                                    DraftNo = model.DraftNo,
                                    Mode = m1.Value,
                                    IndNo = ModeInd,
                                    Category = "IIT"
                                };
                                pat.tbl_draft_comMode.Add(a);
                                pat.SaveChanges();
                                ++ay; ++ModeInd;
                            }
                            else { ++ModeInd; }
                        }
                        pat.SaveChanges();
                        var jmode = pat.tbl_draft_comMode.Where(m => m.DraftNo == model.DraftNo && m.Category == "Joint").ToList();
                        if (jmode.Count > 0)
                        {
                            pat.tbl_draft_comMode.RemoveRange(jmode);
                            pat.SaveChanges();
                        }
                        int az = 1; int ModeJoint = 0;
                        foreach (var m1 in model.Annex.JointMode)
                        {
                            if (m1.Selected == true)
                            {
                                tbl_draft_comMode a = new tbl_draft_comMode()
                                {
                                    Sno = az,
                                    DraftNo = model.DraftNo,
                                    Mode = m1.Value,
                                    IndNo = ModeJoint,
                                    Category = "Joint"
                                };
                                pat.tbl_draft_comMode.Add(a);
                                pat.SaveChanges();
                                ++az; ++ModeJoint;
                            }
                            else { ++ModeJoint; }
                        }
                        pat.SaveChanges();
                        if (model.IDFType == "Copyright")
                        {
                            if (model.CR != null)
                            {
                                var cr = pat.tbl_draft_Copyright.Where(m => m.DraftNo == model.DraftNo).FirstOrDefault();
                                if (cr != null)
                                {
                                    cr.Category = model.CR.Category;
                                    cr.ClassofWork = model.CR.ClassofWork;
                                    cr.Description = model.CR.Description;
                                    cr.Details = model.CR.Details;
                                    cr.isPublished = model.CR.isPublished;
                                    cr.isRegistered = model.CR.isRegistered;
                                    cr.Language = model.CR.Language;
                                    cr.Nature = model.CR.Nature;
                                    cr.Original = model.CR.Original;
                                    cr.Title = model.CR.Title;
                                    pat.SaveChanges();
                                }
                                else
                                {
                                    tbl_draft_Copyright dcr = new tbl_draft_Copyright()
                                    {
                                        Category = model.CR.Category,
                                        ClassofWork = model.CR.ClassofWork,
                                        Description = model.CR.Description,
                                        Details = model.CR.Details,
                                        DraftNo = model.DraftNo,
                                        isPublished = model.CR.isPublished,
                                        isRegistered = model.CR.isRegistered,
                                        Language = model.CR.Language,
                                        Nature = model.CR.Nature,
                                        Original = model.CR.Original,
                                        Title = model.CR.Title
                                    };
                                    pat.tbl_draft_Copyright.Add(dcr);
                                    pat.SaveChanges();
                                }
                                var auth = pat.tbl_draft_CR_Author.Where(m => m.DraftNo == model.DraftNo).ToList();
                                if (model.CR.Author.Count > 0)
                                {
                                    int i = 0;
                                    if (auth.Count == model.CR.Author.Count)
                                    {
                                        foreach (var a in auth)
                                        {
                                            a.DraftNo = model.DraftNo;
                                            a.AUAddress = model.CR.Author[i].AUAddress;
                                            a.AUName = model.CR.Author[i].AUName;
                                            a.AUNationality = model.CR.Author[i].AUNationality;
                                            a.deceasedDt = model.CR.Author[i].deceasedDt;
                                            a.isDeceased = model.CR.Author[i].isDeceased;
                                            a.createdBy = uname;
                                            a.createdOn = DateTime.Now;
                                            a.SNo = ++i;
                                        }
                                        pat.SaveChanges();
                                    }
                                    else
                                    {
                                        if (auth.Count > 0)
                                        {
                                            pat.tbl_draft_CR_Author.RemoveRange(auth);
                                            pat.SaveChanges();
                                        }
                                        foreach (var a in model.CR.Author)
                                        {
                                            if (a.AUName != null)
                                            {
                                                tbl_draft_CR_Author dauth = new tbl_draft_CR_Author()
                                                {
                                                    AUName = model.CR.Author[i].AUName,
                                                    DraftNo = model.DraftNo,
                                                    AUAddress = model.CR.Author[i].AUAddress,
                                                    AUNationality = model.CR.Author[i].AUNationality,
                                                    isDeceased = model.CR.Author[i].isDeceased,
                                                    deceasedDt = model.CR.Author[i].deceasedDt,
                                                    createdBy = uname,
                                                    createdOn = DateTime.Now,
                                                    SNo = ++i
                                                };
                                                pat.tbl_draft_CR_Author.Add(dauth);
                                            }
                                        }
                                        pat.SaveChanges();
                                    }
                                }
                                else
                                {
                                    if (auth.Count > 0)
                                    {
                                        pat.tbl_draft_CR_Author.RemoveRange(auth);
                                        pat.SaveChanges();
                                    }
                                }
                                var pub = pat.tbl_draft_CR_Publish.Where(m => m.DraftNo == model.DraftNo).ToList();
                                if (model.CR.Publish.Count > 0)
                                {
                                    int j = 0;
                                    if (pub.Count == model.CR.Publish.Count)
                                    {
                                        foreach (var p in pub)
                                        {
                                            p.DraftNo = model.DraftNo;
                                            p.Country = model.CR.Publish[j].Country;
                                            p.PUAddress = model.CR.Publish[j].PUAddress;
                                            p.PUName = model.CR.Publish[j].PUName;
                                            p.PUNationality = model.CR.Publish[j].PUNationality;
                                            p.Year = model.CR.Publish[j].Year;
                                            p.Sno = ++j;
                                        }
                                        pat.SaveChanges();
                                    }
                                    else
                                    {
                                        if (pub.Count > 0)
                                        {
                                            pat.tbl_draft_CR_Publish.RemoveRange(pub);
                                            pat.SaveChanges();
                                        }
                                        foreach (var p in model.CR.Publish)
                                        {
                                            if (p.PUName != null)
                                            {
                                                tbl_draft_CR_Publish tpub = new tbl_draft_CR_Publish()
                                                {
                                                    Country = model.CR.Publish[j].Country,
                                                    PUName = model.CR.Publish[j].PUName,
                                                    DraftNo = model.DraftNo,
                                                    PUAddress = model.CR.Publish[j].PUAddress,
                                                    PUNationality = model.CR.Publish[j].PUNationality,
                                                    Year = model.CR.Publish[j].Year,
                                                    Sno = ++j
                                                };
                                                pat.tbl_draft_CR_Publish.Add(tpub);
                                            }
                                        }
                                        pat.SaveChanges();
                                    }
                                }
                                else
                                {
                                    if (pub.Count > 0)
                                    {
                                        pat.tbl_draft_CR_Publish.RemoveRange(pub);
                                        pat.SaveChanges();
                                    }
                                }
                            }
                        }
                        else if (model.IDFType == "Trademark")
                        {
                            if (model.Trade != null)
                            {
                                var tr = pat.tbl_draft_Trademark.Where(m => m.DraftNo == model.DraftNo).FirstOrDefault();
                                if (tr != null)
                                {
                                    tr.Category = model.Trade.Category;
                                    tr.Class = model.Trade.Class;
                                    tr.Description = model.Trade.Description;
                                    tr.DraftNo = model.DraftNo;
                                    tr.Language = model.Trade.Language;
                                    tr.TMImage = model.Trade.TMImage;
                                    tr.TMName = model.Trade.TMName;
                                    tr.TMStatement = model.Trade.TMStatement;
                                    pat.SaveChanges();
                                }
                                else
                                {
                                    tbl_draft_Trademark dtr = new tbl_draft_Trademark()
                                    {
                                        Category = model.Trade.Category,
                                        Class = model.Trade.Class,
                                        Description = model.Trade.Description,
                                        TMStatement = model.Trade.TMStatement,
                                        TMName = model.Trade.TMName,
                                        Language = model.Trade.Language,
                                        DraftNo = model.DraftNo,
                                        TMImage = model.Trade.TMImage
                                    };
                                    pat.tbl_draft_Trademark.Add(dtr);
                                    pat.SaveChanges();
                                }
                            }
                            if (model.Trade.TAppl.Count > 0)
                            {
                                int a = 0;
                                var ap = pat.tbl_draft_Trade_Applicantdetail.Where(m => m.DraftNo == model.DraftNo).ToList();
                                if (ap.Count == model.Trade.TAppl.Count)
                                {
                                    foreach (var m in ap)
                                    {
                                        m.DraftNo = model.DraftNo;
                                        m.AddressOfService = model.Trade.TAppl[a].AddressOfService;
                                        m.Country = model.Trade.TAppl[a].Country;
                                        m.Jurisdiction = model.Trade.TAppl[a].Jurisdiction;
                                        m.LegalStatus = model.Trade.TAppl[a].LegalStatus;
                                        m.Organisation = model.Trade.TAppl[a].Organisation;
                                        m.Nature = model.Trade.TAppl[a].Nature;
                                        m.Sno = ++a;
                                        m.CreatedOn = DateTime.Now.Date;
                                        m.CreatedBy = uname;
                                    }
                                    pat.SaveChanges();
                                }
                                else
                                {
                                    if (ap != null)
                                    {
                                        pat.tbl_draft_Trade_Applicantdetail.RemoveRange(ap);
                                        pat.SaveChanges();
                                    }
                                    foreach (var m in model.Trade.TAppl)
                                    {
                                        if (m.Organisation != null)
                                        {
                                            tbl_draft_Trade_Applicantdetail ttap = new tbl_draft_Trade_Applicantdetail()
                                            {
                                                DraftNo = model.DraftNo,
                                                AddressOfService = model.Trade.TAppl[a].AddressOfService,
                                                Country = model.Trade.TAppl[a].Country,
                                                Jurisdiction = model.Trade.TAppl[a].Jurisdiction,
                                                LegalStatus = model.Trade.TAppl[a].LegalStatus,
                                                Organisation = model.Trade.TAppl[a].Organisation,
                                                Nature = model.Trade.TAppl[a].Nature,
                                                Sno = ++a,
                                                CreatedBy = uname,
                                                CreatedOn = DateTime.Now.Date
                                            };
                                            pat.tbl_draft_Trade_Applicantdetail.Add(ttap);
                                        }
                                    }
                                    pat.SaveChanges();
                                }
                            }
                            else
                            {
                                var ap = pat.tbl_draft_Trade_Applicantdetail.Where(m => m.DraftNo == model.DraftNo).ToList();
                                if (ap != null)
                                {
                                    pat.tbl_draft_Trade_Applicantdetail.RemoveRange(ap);
                                    pat.SaveChanges();
                                }
                            }
                        }
                        transaction.Commit();
                        return "Success";
                    }
                    catch (Exception ex)
                    {
                        transaction.Rollback();
                        return ex.ToString();
                    }
                }
            }
        }
        public static List<DraftVM> GetIDFDraftDetails(string uname)
        {
            List<DraftVM> draft = new List<DraftVM>();
            using (var context = new PatentISEntities())
            {
                var query = context.tbl_draft_IDFReq.Where(m => m.CreatedBy == uname).Select(m => new { m.DraftNo, m.IDFType, m.PrimaryInventorType, m.PrimaryInventorName, m.PIDepartment, m.Status, m.Remarks }).ToList();
                if (query.Count > 0)
                {
                    for (int i = 0; i < query.Count; i++)
                    {
                        draft.Add(new DraftVM()
                        {
                            IDFType = query[i].IDFType,
                            Status = query[i].Status,
                            Remarks = query[i].Remarks,
                            PIDepartment = query[i].PIDepartment,
                            PrimaryInventorName = query[i].PrimaryInventorName,
                            PrimaryInventorType = query[i].PrimaryInventorType,
                            DraftNo = query[i].DraftNo
                        });
                    }
                }
            }
            return draft;
        }
        public static bool DeleteDraft(string dno)
        {
            try
            {
                using (PatentISEntities pat = new PatentISEntities())
                {
                    var d = pat.tbl_draft_IDFReq.Where(m => m.DraftNo == dno).FirstOrDefault();
                    if (d != null)
                    {
                        pat.tbl_draft_IDFReq.Remove(d);
                    }
                    var an = pat.tbl_draft_AnnexB1.Where(m => m.DraftNo == dno).FirstOrDefault();
                    if (an != null)
                    {
                        pat.tbl_draft_AnnexB1.Remove(an);
                    }
                    var co = pat.tbl_draft_Coinventor.Where(m => m.DraftNo == dno).ToList();
                    if (co.Count > 0)
                    {
                        pat.tbl_draft_Coinventor.RemoveRange(co);
                    }
                    var ap = pat.tbl_draft_applicants.Where(m => m.DraftNo == dno).ToList();
                    if (ap.Count > 0)
                    {
                        pat.tbl_draft_applicants.RemoveRange(ap);
                    }
                    var area = pat.tbl_draft_AppAreas.Where(m => m.DraftNo == dno).ToList();
                    if (area.Count > 0)
                    {
                        pat.tbl_draft_AppAreas.RemoveRange(area);
                    }
                    var mode = pat.tbl_draft_comMode.Where(m => m.DraftNo == dno).ToList();
                    if (mode.Count > 0)
                    {
                        pat.tbl_draft_comMode.RemoveRange(mode);

                    }
                    pat.SaveChanges();
                    return true;
                }
            }
            catch (Exception ex)
            { return false; }
        }
        #endregion
    }
}