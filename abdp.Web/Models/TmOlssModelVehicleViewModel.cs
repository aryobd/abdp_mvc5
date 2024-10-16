using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace abdp.Web.Models
{
    public class TmOlssModelVehicleViewModel
    {
        public int tm_olss_model_vehicle_id { get; set; }
        public Nullable<int> tm_olss_model_vehicle_id_prev { get; set; }
        public int tm_olss_brand_id { get; set; }
        public string model_vehicle_name { get; set; }
        public string model_vehicle_desc { get; set; }

        public string brand_name { get; set; }
    }
}