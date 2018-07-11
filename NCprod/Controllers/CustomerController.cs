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
    public class CustomerController : Controller
    {
        //
        // GET: /Customer/

        public ActionResult Index()
        {
            return View();
        }
        #region 我的收藏夹
        public ActionResult CollectFile()
        {
            if (Session["user"] == null)
                return RedirectToAction("Logon", "Home");
            return View();
        }
        [GridAction]//商品列表
        public ActionResult ProductList()
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            var data = from m in db.t_f_collect
                       join b in db.t_f_commodity on m.commodity_id equals b.com_ID
                       join a in db.dl_basic_users on b.com_belong equals a.userid
                       where m.con_id == userid1 && m.isdel == false && b.isdel == false
                       orderby m.collect_id ascending
                       select new Commdity
                       {
                           com_ID = m.commodity_id,//商品ID
                           com_price = b.com_price,//商品价格
                           com_belongu = a.username,//商家姓名
                           com_belongp = a.userphone,//商家电话
                           com_postage = b.com_postage,//商品邮费
                           com_name = b.com_name,//商品名称
                           com_content = b.com_content,
                           option2 = b.option2
                       };
            return View(new GridModel()
            {
                Data = data,
                Total = data.Count()
            });
        }
        #endregion
        #region 我的购物车
        public ActionResult BuyCart()
        {
            if (Session["user"] == null)
                return RedirectToAction("Logon", "Home");
            return View();
        }
        public string GetPro(string json)
        {
            JsonObject obj = new JsonObject(json);
            decimal ID = decimal.Parse(obj["id"].Value.ToString());
            decimal com_num = decimal.Parse(obj["num"].Value.ToString());
            try
            {
                using (var db = new NCtecanEntities())
                {
                    var product = db.t_f_cart.FirstOrDefault(s => s.cart_ID == ID);
                    product.coms_num = com_num;
                    product.option1 = "1";
                    db.SaveChanges();
                    return "success";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [GridAction]//购物车商品列表
        public ActionResult GProductList()
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            var data = from m in db.t_f_cart
                       join b in db.t_f_commodity on m.coms_ID equals b.com_ID
                       join a in db.dl_basic_users on b.com_belong equals a.userid
                       where m.cons_ID == userid1 && m.isdel == false && b.isdel == false
                       orderby m.cart_ID ascending
                       select new Commdity2
                       {
                           com_ID = m.coms_ID,//商品ID
                           cart_ID = m.cart_ID,//购物车ID
                           com_price = b.com_price,//商品价格
                           com_belongu = a.username,//商家姓名
                           com_belongp = a.userphone,//商家电话
                           com_postage = b.com_postage,//商品邮费
                           com_name = b.com_name,//商品名称
                           com_content = b.com_content, //商品图片
                           option2 = b.option2,//商品重量
                           coms_num = m.coms_num
                       };
            return View(new GridModel()
            {
                Data = data,
                Total = data.Count()
            });
        }
        //结算
        public ActionResult jiesuanCart(string i)
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            var cool = from a in db.t_f_cart
                       where a.option1 == "1"
                       select a;
            foreach (var q in cool)
            {
                q.isdel = true;
            }
            db.SaveChanges();
            var data = from m in db.t_f_cart
                       join b in db.t_f_commodity on m.coms_ID equals b.com_ID
                       join a in db.dl_basic_users on b.com_belong equals a.userid
                       where m.cons_ID == userid1 && m.isdel == false && b.isdel == false
                       orderby m.cart_ID ascending
                       select new Commdity
                       {
                           com_ID = m.coms_ID,//商品ID
                           cart_ID = m.cart_ID,//购物车ID
                           com_price = b.com_price,//商品价格
                           com_belongu = a.username,//商家姓名
                           com_belongp = a.userphone,//商家电话
                           com_postage = b.com_postage,//商品邮费
                           com_name = b.com_name,//商品名称
                           com_content = b.com_content, //商品图片
                           option2 = b.option2,//商品重量
                           coms_num = m.coms_num
                       };
            return Json(new GridModel()
            {
                Data = data,
                Total = data.Count()
            });
        }
        //保存订单

        #endregion
        #region 我的订单
        public ActionResult MyOrder()
        {
            if (Session["user"] == null)
                return RedirectToAction("Logon", "Home");
            return View();
        }
        [GridAction]//订单商品列表
        public ActionResult OProductList()
        {
            if (Session["user"] == null)
            {
            }
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            var data = from a in db.t_f_order
                       join b in db.t_f_commodity on a.commoditys_ID equals b.com_ID
                       join c in db.dl_basic_users on a.shop_id equals c.userid
                       where a.consumers_ID == userid1 && a.option2 != "交易成功"
                       orderby a.orders_ID ascending
                       select new Commdity5
                       {
                           orderid = a.orders_ID,//订单ID
                           com_ID = b.com_ID,//商品ID
                           com_price = a.option1,//订单金额
                           com_postage = b.com_postage,//商品邮费
                           com_name = b.com_name,//商品名称
                           com_content = b.com_content, //商品图片
                           con_id = a.consumers_ID,//消费者ID
                           con_name = c.username,//商家名字
                           option1 = c.userid,//商家名字
                           happendate = a.happendate,//下订单时间
                           adress = a.adress_ID,//收货地址
                           commodity_num = a.Commodity_num,//商品数量
                           option5 = b.option2,//商品重量
                           logs_id = a.logs_ID//收货人
                       };
            return View(new GridModel()
            {
                Data = data,
                Total = data.Count()
            });
        }

        #endregion
        #region 加入购物车
        [HttpPost]
        public string JoinCart(string id)
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid = user.userid; //消费者ID
            decimal productid = decimal.Parse(id);//商品ID
            System.DateTime happendate = new System.DateTime();
            happendate = DateTime.Now;
            try
            {
                using (var db = new NCtecanEntities())
                {
                    var cart = new t_f_cart()
                    {
                        cons_ID = userid,
                        coms_ID = productid,
                        coms_num = 1,
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
        #region 收藏夹删除
        public ActionResult DeleteFile(string id)
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            decimal productid = decimal.Parse(id);
            var q = db.t_f_collect.FirstOrDefault(m => m.commodity_id == productid);
            q.isdel = true;
            db.SaveChanges();
            var data = from m in db.t_f_collect
                       join b in db.t_f_commodity on m.commodity_id equals b.com_ID
                       join a in db.dl_basic_users on b.com_belong equals a.userid
                       where m.con_id == userid1 && m.isdel == false && b.isdel == false
                       orderby m.collect_id ascending
                       select new Commdity
                       {
                           com_ID = m.commodity_id,//商品ID
                           com_price = b.com_price,//商品价格
                           com_belongu = a.username,//商家姓名
                           com_belongp = a.userphone,//商家电话
                           com_postage = b.com_postage,//商品邮费
                           com_name = b.com_name,//商品名称
                       };
            return Json(new GridModel()
            {
                Data = data,
                Total = data.Count()
            });
        }
        #endregion
        #region 购物车删除
        public ActionResult DeleteCart(string id)
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            decimal productid = decimal.Parse(id);
            var q = db.t_f_cart.FirstOrDefault(m => m.cart_ID == productid);
            q.isdel = true;
            db.SaveChanges();
            var data = from m in db.t_f_cart
                       join b in db.t_f_commodity on m.coms_ID equals b.com_ID
                       join a in db.dl_basic_users on b.com_belong equals a.userid
                       where m.cons_ID == userid1 && m.isdel == false && b.isdel == false
                       orderby m.cart_ID ascending
                       select new Commdity
                       {
                           com_ID = m.coms_ID,//商品ID
                           cart_ID = m.cart_ID,//购物车ID
                           com_price = b.com_price,//商品价格
                           com_belongu = a.username,//商家姓名
                           com_belongp = a.userphone,//商家电话
                           com_postage = b.com_postage,//商品邮费
                           com_name = b.com_name,//商品名称
                           com_content = b.com_content, //商品图片
                           option2 = b.option2,//商品重量
                           coms_num = m.coms_num
                       };
            return Json(new GridModel()
            {
                Data = data,
                Total = data.Count()
            });
        }
        #endregion
        #region 我的物流
        public ActionResult MyWuliu(decimal id)
        {
            NCtecanEntities db = new NCtecanEntities();
            var data = (from a in db.t_f_order
                        where a.orders_ID == id
                        select new
                            {
                                a.option3,
                                a.option4
                            }).ToList();
            ViewData["option3"] = data[0].option3;
            ViewData["option4"] = data[0].option4;
            return View();
        }
        //确认收货

        public ActionResult EnterP(string id)
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            decimal orderid = decimal.Parse(id);
            var q = db.t_f_order.FirstOrDefault(m => m.orders_ID == orderid);
            q.option2 = "交易成功";
            db.SaveChanges();
            var data = from a in db.t_f_order
                       join b in db.t_f_commodity on a.commoditys_ID equals b.com_ID
                       join c in db.dl_basic_users on a.consumers_ID equals c.userid
                       where a.consumers_ID == userid1 && a.option2 != "交易成功"
                       orderby a.orders_ID ascending
                       select new Commdity5
                       {
                           orderid = a.orders_ID,//订单ID
                           com_ID = b.com_ID,//商品ID
                           com_price = a.option1,//订单金额
                           com_postage = b.com_postage,//商品邮费
                           com_name = b.com_name,//商品名称
                           com_content = b.com_content, //商品图片
                           con_id = a.consumers_ID,//消费者ID
                           con_name = c.username,//消费者名字
                           happendate = a.happendate,//下订单时间
                           adress = a.adress_ID,//收货地址
                           commodity_num = a.Commodity_num,//商品数量
                       };
            return Json(new GridModel()
            {
                Data = data,
                Total = data.Count()
            }, JsonRequestBehavior.AllowGet);

        }

        #endregion
        #region 已完成订单
        public ActionResult TheSOrder()
        {
            return View();
        }
        public ActionResult theSOrderList()
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            var data = from a in db.t_f_order
                       join b in db.t_f_commodity on a.commoditys_ID equals b.com_ID
                       join c in db.dl_basic_users on a.shop_id equals c.userid
                       where a.consumers_ID == userid1 && a.option2 == "交易成功"
                       orderby a.orders_ID ascending
                       select new Commdity5
                       {
                           orderid = a.orders_ID,//订单ID
                           com_ID = b.com_ID,//商品ID
                           com_price = a.option1,//订单金额
                           com_postage = b.com_postage,//商品邮费
                           com_name = b.com_name,//商品名称
                           com_content = b.com_content, //商品图片
                           con_id = a.shop_id,//商家ID
                           shop_id = a.shop_id,
                           con_name = c.username,//商家名字
                           happendate = a.happendate,//下订单时间
                           adress = a.adress_ID,//收货地址
                           commodity_num = a.Commodity_num,//商品数量
                           option3 = a.option3,//快递单号
                           option4 = a.option4,//物流公司
                           option5 = b.option2
                       };
            return Json(new GridModel()
            {
                Data = data,
                Total = data.Count()
            });
        }
        #endregion
        #region 送货地址
        public ActionResult Adress()
        {
            NCtecanEntities db = new NCtecanEntities();
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal? userid1 = user.userid;
            var data = from a in db.t_f_cart
                       join b in db.t_f_commodity on a.coms_ID equals b.com_ID
                       where a.option1 == "1" && a.cons_ID == userid1
                       select new Commdity2
                                    {
                                        com_ID = a.coms_ID,//商品ID
                                        cart_ID = a.cart_ID,//购物车ID
                                        com_price = b.com_price,//商品价格
                                        com_postage = b.com_postage,//商品邮费
                                        com_name = b.com_name,//商品名称
                                        com_content = b.com_content, //商品图片
                                        option2 = b.option2,//商品重量
                                        coms_num = a.coms_num//商品数量
                                    };
            decimal? sum = 0;
            foreach (var q in data)
            {
                sum += (q.coms_num * q.com_price + q.com_postage);
            }
            ViewData["sum"] = sum;//
            return View();
        }
        ////新增送货地址
        [HttpPost]


        public string AddAdress(string json)
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            JsonObject obj = new JsonObject(json);
            string address = obj["adress"].Value.ToString();
            string comment = obj["comment"].Value.ToString();
            System.DateTime happendate = new System.DateTime();
            happendate = DateTime.Now;
            try
            {
                using (var db = new NCtecanEntities())
                {
                    var cart = new t_f_adress()
                    {
                        adress = address,
                        conss_ID = userid1,
                        comment = comment,
                        isdel = false,
                        happendate = happendate
                    };
                    db.t_f_adress.Add(cart);
                    db.SaveChanges();
                    return "success";
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        ///删除地址
        public ActionResult DeleteAdress(string id)
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            decimal adrid = decimal.Parse(id);
            var q = db.t_f_adress.FirstOrDefault(m => m.adr_ID == adrid);
            q.isdel = true;
            db.SaveChanges();
            var data = from a in db.t_f_adress
                       where a.conss_ID == userid1 && a.isdel == false
                       select new
                       {
                           a.adress,
                           a.conss_ID,
                           a.happendate,
                           a.comment
                       };
            return Json(new GridModel()
            {
                Data = data,
                Total = data.Count()
            });
        }
        #endregion
        #region 提交订单——新增订单和删除购物车商品和修改商品剩余数量
        public string SaveOrder(string id)
        {
            ///
            ///新增订单
            ///
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;//消费者ID
            decimal productid = decimal.Parse(id);//物流id
            var db = new NCtecanEntities();
            var root = (from a in db.t_f_cart
                        join b in db.t_f_commodity on a.coms_ID equals b.com_ID
                        join c in db.dl_basic_users on b.com_belong equals c.userid
                        where a.option1 == "1" && a.cons_ID == userid1
                        select new Commdity2
                        {
                            consumers_ID = userid1,//消费者ID
                            shop_id = b.com_belong,//商家ID
                            com_ID = a.coms_ID,//商品ID
                            cart_ID = a.cart_ID,//购物车ID
                            adress_ID = productid,//物流ID
                            coms_num = a.coms_num,//商品数量
                            com_price = b.com_price,//商品价格
                            com_postage = b.com_postage,//商品邮费

                        }).Distinct().OrderBy(ob => ob.cart_ID);
            System.DateTime happendate = new System.DateTime();//发生时间
            happendate = DateTime.Now;
            try
            {
                using (db = new NCtecanEntities())
                {
                    foreach (var q in root)
                    {
                        var address = (from a in db.t_f_adress
                                       where a.adr_ID == q.adress_ID
                                       select a).ToList();
                        decimal? opton1 = q.coms_num * q.com_price + q.com_postage;
                        var theorder = new t_f_order()
                        {
                            consumers_ID = userid1,//消费者ID
                            shop_id = q.shop_id,//商家ID
                            commoditys_ID = q.com_ID,//商品ID
                            adress_ID = address[0].adress,//收货地址
                            logs_ID = address[0].comment,//收货人
                            Commodity_num = q.coms_num,//商品数量
                            isdel = false,
                            happendate = happendate,//发生时间
                            option1 = opton1,//交易金额
                            option2 = "已下订单",//状态：已下订单，配送中，交易成功
                        };
                        db.t_f_order.Add(theorder);
                    }
                    foreach (var p in root)
                    {
                        var m = db.t_f_cart.Where(c => c.cart_ID == p.cart_ID).First();
                        db.t_f_cart.Remove(m);
                    }
                    foreach (var m in root)
                    {
                        var h = db.t_f_commodity.Where(c => c.com_ID == m.com_ID).First();
                        var num = decimal.Parse(h.com_number);
                        h.com_number = (num - m.coms_num).Value.ToString();
                    }
                    db.SaveChanges();
                }
                return "success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        #endregion

    }
}
