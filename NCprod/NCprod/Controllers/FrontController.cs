using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NCprod.Models;
using Newtonsoft.Json;
using System.Text;
using Telerik.Web.Mvc;
using System.Data;
using System.IO;
using System.Configuration;
using System.Data.SqlClient;
using System.Web.UI.WebControls;
using System.Web.UI;

namespace NCprod.Controllers
{
    public class FrontController : Controller
    {
        //
        // GET: /Front/
        #region 首页
        public ActionResult Index()
        {
            return View();
        }
        //热销商品展示栏
        [GridAction]
        public ActionResult ProductList()
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            var data = from b in db.t_f_commodity
                       join a in db.sys_role on b.option1 equals a.option1
                       where b.isdel == false
                       orderby b.com_ID ascending
                       select new Commdity1
                       {
                           com_ID = b.com_ID,//商品ID
                           com_price = b.com_price,//商品价格
                           com_belong = b.com_belong,//商品所属用户
                           com_name = b.com_name,//商品名称
                           com_number = b.com_number,//商品数量
                           com_postage = b.com_postage,//商品邮费
                           com_content = b.com_content,//商品介绍内容
                           com_state = b.com_state,//商品状态
                           com_starttime = b.com_starttime,//新增时间
                           com_saletime = b.com_saletime,//商品销售时间
                           shop_ID = b.shop_ID,//商品所属店铺
                           comment = b.comment, //商品备注
                           option1 = a.rolename// 商品的类别
                       };
            return View(new GridModel()
                {
                    Data = data,
                    Total = 8
                });
        }
        #endregion

        #region 清新茶类
        public ActionResult Tea()
        {
            return View();
        }
        #endregion

        #region 五谷杂粮
        public ActionResult Rice()
        {
            return View();
        }
        #endregion

        #region 营养肉类
        public ActionResult Meat()
        {
            return View();
        }
        #endregion

        #region 可口小吃
        public ActionResult Jfood()
        {
            return View();
        }
        #endregion

        #region 新鲜瓜果
        public ActionResult Fruit()
        {
            return View();
        }
        #endregion

        #region 精致物件
        public ActionResult Gift()
        {
            return View();
        }
        #endregion

        #region 关于我们|帮助中心
        public ActionResult Aboutus()
        {
            return View();
        }
        #endregion

    }
}
