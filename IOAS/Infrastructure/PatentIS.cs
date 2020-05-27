using IOAS.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOAS.Infrastructure
{
    public class PatentIS
    {
        #region IDFRequest
        public static List<string> IDFTypeList()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tbl_mst_ListItem.Where(m => m.Category=="IDF" && m.Grouping=="Type").OrderBy(m=>m.ItemValue).Select(m=>m.ItemText).ToList();
                return query;
            }
        }
        public static List<string> GetPatentInventorType()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tbl_mst_ListItem.Where(m => m.Category == "PatentIDF" && m.Grouping == "InventorType").Select(m => m.ItemText).ToList();
                return query;
            }
        }
        public static List<string> DepartmentList()
        {
            using (WFADSEntities pat = new  WFADSEntities())
            {
                var query = pat.DepartmentMaster.Select(m => m.DepartmentCode).ToList();
                return query;
            }
        }
        public static List<string> PINameList(string prefix,string type)
            {
            using (WFADSEntities pat = new WFADSEntities())
            {
                if (type == "Faculty")
                {
                    var query = pat.Faculty_Details.Where(m=>m.EmployeeName.Contains(prefix)).Select(m => m.EmployeeName).ToList();
                    return query;
                }
                else if (type == "Institute Staff")
                {
                    var query = pat.Staff_Details.Where(m => m.EmployeeName.Contains(prefix)).Select(m => m.EmployeeName).ToList();
                    return query;
                }
                else if (type == "Student")
                {
                    var query = pat.Student_Details.Where(m => m.StudentName.Contains(prefix)).Select(m => m.StudentName).ToList();
                    return query;
                }
                else
                    return null;
            }
        }
        public static object GetPIData(string type,string name)
        {
            using (WFADSEntities pat = new WFADSEntities())
            {
                if (type == "Faculty") {
                    var query = pat.Faculty_Details.Where(m => m.EmployeeName == name).Select(m =>new { Dept = m.DepartmentCode.Trim(), Id = m.EmployeeId , Ph=m.ContactNumber,Mail=m.Email}).FirstOrDefault();
                    return query; }
                else if (type == "Institute Staff")
                {
                    var query = pat.Staff_Details.Where(m => m.EmployeeName == name).Select(m =>new { Dept = m.DepartmentCode.Trim(), Id = m.EmployeeId, Ph = m.ContactNumber, Mail = m.Email }).FirstOrDefault();
                    return query;
                }
                else if (type == "Student")
                {
                    var query = pat.Student_Details.Where(m => m.StudentName == name).Select(m =>new { Dept = m.DepartmentCode.Trim(), Id = m.RollNumber, Ph = m.ContactNumber, Mail = m.EmailID }).FirstOrDefault();
                    return query;
                }
                else
                {
                    return null;
                }
            }
        }
        public static List<string> ActionList()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tbl_mst_ListItem.Where(m => m.Category == "Patent" && m.Grouping == "RequestedAction").Select(m => m.ItemText).ToList();
                return query;
            }
        }

        public static List<string> TMActionList()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tbl_mst_ListItem.Where(m => m.Category == "Trademark" && m.Grouping == "RequestedAction").Select(m => m.ItemText).ToList();
                return query;
            }
        }
        public static List<string> CRActionList()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tbl_mst_ListItem.Where(m => m.Category == "copyright" && m.Grouping == "RequestedAction").Select(m => m.ItemText).ToList();
                return query;
            }
        }
        public static List<string> DesignActionList()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tbl_mst_ListItem.Where(m => m.Category == "DesignPatent" && m.Grouping == "RequestedAction").Select(m => m.ItemText).ToList();
                return query;
            }
        }
        public static List<string> StageList()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tbl_mst_ListItem.Where(m => m.Category == "AnexB" && m.Grouping == "DevelopmentStage").Select(m => (m.ItemValue+"-"+m.ItemText)).ToList();
                return query;
            }
        }
        public static List<SelectListItem> IndustryList()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tbl_mst_ListItem.Where(m => m.Category == "AnexB" && m.Grouping == "ApplicationIndustry").Select(m=>new SelectListItem {Value= m.ItemText,Text= m.ItemValue }).ToList();
                return query;
            }
        }
        public static List<SelectListItem> IndustryList1()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tbl_mst_ListItem.Where(m => m.Category == "AnexB" && m.Grouping == "SubIndustry").Select(m =>new SelectListItem { Value = m.ItemText, Text = m.ItemValue }).ToList();
                return query;
            }
        }
        public static List<SelectListItem> CommericaliseMode()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tbl_mst_ListItem.Where(m => m.Category == "AnexB" && m.Grouping == "CommercialisationModeIIT").Select(m =>new SelectListItem { Value = m.ItemText, Text = m.ItemValue }).ToList();
                return query;
            }
        }
        public static List<SelectListItem> CommericaliseMode1()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tbl_mst_ListItem.Where(m => m.Category == "AnexB" && m.Grouping == "CommercialisationModeJoint").Select(m =>new SelectListItem { Value = m.ItemText, Text = m.ItemValue }).ToList();
                return query;
            }
        }
        public static List<string> TMCategory()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tbl_mst_ListItem.Where(m => m.Category == "Trademark" && m.Grouping == "Category").Select(m => m.ItemText).ToList();
                return query;
            }
        }
        public static List<string> CRCategory()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tbl_mst_ListItem.Where(m => m.Category == "copyright" && m.Grouping == "ApplicantCategory").Select(m => m.ItemText).ToList();
                return query;
            }
        }
        public static List<string> CRNature()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tbl_mst_ListItem.Where(m => m.Category == "copyright" && m.Grouping == "ApplicantNature").Select(m =>m.ItemText).ToList();
                return query;
            }
        }
        public static List<string> CRClass()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tbl_mst_ListItem.Where(m => m.Category == "copyright" && m.Grouping == "ClassOfWork").Select(m =>m.ItemText).ToList();
                return query;
            }
        }
        public static List<SelectListItem> DPClass()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tbl_mst_ListItem.Where(m => m.Category == "DesignPatent" && m.Grouping == "Classes").Select(m => new SelectListItem { Value = (m.ItemValue + "-" + m.ItemText), Text = (m.ItemValue+"-"+m.ItemText)}).ToList();
                return query;
            }
        }
        public static long GetNewFileNo()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tblIDFRequest.OrderByDescending(m => m.FileNo).Select(m => m.FileNo).FirstOrDefault();
                return query;
            }
        }
        public static long GetNewDraftNo()
        {
            using (PatentISEntities pat = new PatentISEntities())
            {
                var query = pat.tbl_draft_IDFReq.OrderByDescending(m => m.DraftNo).Select(m => m.DraftNo).FirstOrDefault();
                if (query != null)
                {
                    long q = Convert.ToInt64(query.Substring(1));
                    return q;
                }
                else
                {
                    var q = pat.tblIDFRequest.OrderByDescending(m => m.FileNo).Select(m => m.FileNo).FirstOrDefault();
                    return q;
                }
            }
        }
        #endregion
    }
}