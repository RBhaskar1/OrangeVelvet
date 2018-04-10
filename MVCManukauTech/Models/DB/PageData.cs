using System;
using System.Collections.Generic;

namespace MVCManukauTech.Models.DB
{
    public partial class PageData
    {
        public int PageId { get; set; }
        public string Pagename { get; set; }
        public string Image { get; set; }
        public string PageText { get; set; }
        public string PageText2 { get; set; }
    }
}
