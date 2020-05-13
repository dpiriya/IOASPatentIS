using System.Collections.Generic;

namespace IOAS.Models.PatentIS
{
    public class CopyRightrptVM
    {      
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
        public List<CRAuthorrptVM> Author { get; set; }
        public List<CRPublishrptVM> Publish { get; set; }
        public CopyRightrptVM()
        {
            Author = new List<CRAuthorrptVM>();
            Publish = new List<CRPublishrptVM>();
        }
    }
}