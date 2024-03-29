﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace IOAS.Models.PatentIS
{
    public class DraftAnnexVM
    {
        public long tranx_id { get; set; }
        public long DraftNo { get; set; }
        public string BriefDescription { get; set; }
        public string DevelopmentStage { get; set; }
        public string OtherInfo { get; set; }
        public Nullable<bool> L1Search { get; set; }
        public string Tool { get; set; }
        public string Party { get; set; }
        public string Outcome { get; set; }
        public string Comments { get; set; }
        public List<string> ListStage { get; set; }
        public List<SelectListItem> ListIndustry { get; set; }
        public List<SelectListItem> ListIndustry1 { get; set; }
        public List<SelectListItem> IITMode { get; set; }
        public List<SelectListItem> JointMode { get; set; }
        public List<DraftcomModeVM> Mode { get; set; }
        public List<DraftAppAreasVM> AppAreas { get; set; }
        public DraftAnnexVM()
        {
            AppAreas = new List<DraftAppAreasVM>();
            Mode = new List<DraftcomModeVM>();
        }
    }
}