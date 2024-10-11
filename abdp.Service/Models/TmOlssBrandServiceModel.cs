using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace abdp.Service.Models
{
    public class TmOlssBrandServiceModel
    {
        public int tm_olss_brand_id { get; set; }
        public Nullable<int> tm_olss_brand_id_prev { get; set; }
        public string brand_name { get; set; }
        public string brand_desc { get; set; }
    }
}
