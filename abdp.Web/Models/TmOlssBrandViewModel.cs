using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace abdp.Web.Models
{
    public class TmOlssBrandViewModel
    {
        public int tm_olss_brand_id { get; set; }
        public Nullable<int> tm_olss_brand_id_prev { get; set; }
        public string brand_name { get; set; }
        public string brand_desc { get; set; }
    }
}