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
            if (Session["user"] == null)
                //return RedirectToAction("Index", "Front");
                return View();
            //收藏
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            NCtecanEntities db = new NCtecanEntities();
            var colle = db.t_f_collect.Where(s => s.con_id == userid1 && s.isdel==false);
            var num = colle.Count();
            ViewData["numbercolle"] = num;
            //订单
            //购物车
            var cart = db.t_f_cart.Where(s => s.cons_ID == userid1 && s.isdel==false);
            var num1 = cart.Count();
            ViewData["numbercart"] = num1;//购物车数量
            var data = (from s in db.t_f_cart
                        join m in db.t_f_commodity on s.coms_ID equals m.com_ID
                        where s.cons_ID == userid1 && s.isdel == false && m.isdel == false
                        select m.com_price).Sum();
            ViewData["catprice"] = data;//购物车总价

            return View();
        }
        //热销商品展示栏
        
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


        #region 加入购物车
        [HttpPost]
        public string JoinCart(string json)
        {
            if (Session["user"] == null)
                return "请先登录！";
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid = user.userid; //消费者ID
            JsonObject obj = new JsonObject(json);
            decimal coms_ID = decimal.Parse(obj["coms_ID"].Value.ToString());//商品ID
            decimal coms_num = decimal.Parse(obj["coms_num"].Value.ToString());//商品数量
            System.DateTime happendate = new System.DateTime();
            happendate = DateTime.Now;
            try
            {
                using (var db = new NCtecanEntities())
                {
                    var cart = new t_f_cart()
                    {
                        cons_ID = userid,
                        coms_ID = coms_ID,
                        coms_num = coms_num,
                        isdel = false,
                        happendate = happendate
                    };
                    db.t_f_cart.Add(cart);
                    db.SaveChanges();
                    return "success";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        #endregion

        #region 加入收藏
        [HttpPost]
        public string JoinCollect(string json)
        {
            if (Session["user"] == null)
                return "请先登录！";
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid = user.userid; //消费者ID
            JsonObject obj = new JsonObject(json);
            decimal commodity_id = decimal.Parse(obj["coms_ID"].Value.ToString());//商品ID
            System.DateTime happendate = new System.DateTime();
            happendate = DateTime.Now;
            try
            {
                using (var db = new NCtecanEntities())
                {
                    var cart = new t_f_collect()
                    {
                        con_id = userid,
                        commodity_id = commodity_id,
                        isdel = false,
                        happendate = happendate
                    };
                    db.t_f_collect.Add(cart);
                    db.SaveChanges();
                    return "success";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

        }
        #endregion


    }
}
