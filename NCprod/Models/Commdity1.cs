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
        public string option2 { get; set; }
        
    }

    public class Commdity
    {
        public decimal? com_ID { get; set; }
        public decimal? com_price { get; set; }
        public string com_name { get; set; }
        public string com_belongu { get; set; }
        public string com_belongp { get; set; }
        public decimal? com_postage { get; set; }
        public string option1 { get; set; }
        public string com_content { get; set; }
        public string option2 { get; set; }
        public decimal? cart_ID { get; set; }
        public decimal? coms_num { get; set; }

    }

    public class Commdity2
    {
        public decimal? cart_ID { get; set; }
        public decimal? com_ID { get; set; }
        public decimal? com_price { get; set; }
        public string com_name { get; set; }
        public string com_belongu { get; set; }
        public string com_belongp { get; set; }
        public decimal? com_postage { get; set; }
        public string option1 { get; set; }
        public string com_content { get; set; }
        public string option2 { get; set; }
        public decimal? coms_num { get; set; }
        public decimal? consumers_ID { get; set; }
        public decimal? shop_id { get; set; }
        public decimal? adress_ID { get; set; }
        

    }
    public class Commdity3
    {
        public decimal? shop_id { get; set; }
        public decimal? option1 { get; set; }

    }
    public class Commdity4
    {
        public string useraddr { get; set; }

    }

    public class Commdity5
    {
        public decimal orderid { get; set; }//订单ID
        public decimal com_ID { get; set; }//商品ID
        public decimal? com_price { get; set; }//订单金额
        public decimal? com_postage { get; set; }//商品邮费

        public string com_name { get; set; }//商品名称
        public string com_content { get; set; }//商品图片
        public decimal? con_id { get; set; }//消费者ID
        public string con_name { get; set; }//消费者名字
        public System.DateTime? happendate { get; set; }//下订单时间
        public string adress { get; set; }//收货地址
        public decimal? commodity_num { get; set; }//商品数量
        public string option3 { get; set; }//快递单号
        public string option4 { get; set; }//快递公司
        public decimal? shop_id { get; set; }
        public string option5 { get; set; }
        public decimal? option1 { get; set; }
        public string logs_id { get; set; }

    }

    public class Massage
    {
        public decimal com_ID { get; set; }
        public string com_content { get; set; }
        public bool? isdel { get; set; }

    }
}
