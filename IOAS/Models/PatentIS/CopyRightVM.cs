using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IOAS.Models.PatentIS
{
    public class CopyRightVM
    {
        public long trx_id { get; set; }
        public long FileNo { get; set; }
        public string Category { get; set; }
        public string Nature { get; set; }
        public string ClassofWork { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public bool isPublished { get; set; }
        public string Details { get; set; }
        public bool isRegistered { get; set; }
        public string Original { get; set; }
        public List<CRAuthorVM> Author { get; set; }
        public List<CRPublishVM> Publish { get; set; } 
        public CopyRightVM()
        {
            Author = new List<CRAuthorVM>();
            Publish = new List<CRPublishVM>();
        }
    }
}