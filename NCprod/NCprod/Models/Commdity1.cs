using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCprod.Models
{
    public class Commdity1
    {
        public decimal com_ID { get; set; }
        public decimal? com_price { get; set; }
        public decimal? com_belong { get; set; }
        public string com_name { get; set; }
        public string com_number { get; set; }
        public decimal? com_postage { get; set; }
        public string com_content { get; set; }
        public int? com_state { get; set; }
        public System.DateTime? com_starttime { get; set; }
        public System.DateTime? com_saletime { get; set; }
        public decimal? shop_ID { get; set; }
        public bool isdel { get; set; }
        public string comment { get; set; }
        public string option1 { get; set; }
        
    }
}