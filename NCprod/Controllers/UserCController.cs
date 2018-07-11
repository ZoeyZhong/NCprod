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
    public class UserCController : Controller
    {
        //管理员对人员进行管理
        // GET: /UserC/

        public ActionResult Index()
        {
            return View();
        }
        #region 会员管理
        public ActionResult VipC()
        {
            return View();
        }
        [GridAction]// 用户列表
        public ActionResult UserList()
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            var dataq = from h in db.dl_basic_users
                        join n in db.sys_user_role on h.userid equals n.userid
                        where n.rid == 3
                        orderby h.userid ascending
                        select new UserRole1
                        {
                            userid = h.userid,
                            userphone = h.userphone,
                            username = h.username,
                            exname = h.exname,
                            userpwd = h.userpwd,
                            idnum = h.idnum,
                            shopid = h.shopid,
                            jointime = h.jointime,
                            usercode = h.usercode,
                            useraddr = h.useraddr,
                            userQQ = h.userQQ,
                            isDel = h.isDel,
                            comment = h.comment
                        };
            return View(new GridModel()
                {
                    Data = dataq,
                    Total = dataq.Count()
                });
        }

        //冻结
        public ActionResult DongJ(string id)
        {
            dl_basic_users user = Session["user"] as dl_basic_users;//获取当前登录用户
            decimal userid1 = user.userid;//获取当前登录用户ID
            var db = new NCtecanEntities();//定义数据库
            decimal userid = decimal.Parse(id);//将id转化为decimal形式
            var q = db.dl_basic_users.FirstOrDefault(m => m.userid == userid);//在一定条件下的的数据库列表
            q.isDel = true;//修改值
            db.SaveChanges();
            var dataq = from h in db.dl_basic_users
                        join n in db.sys_user_role on h.userid equals n.userid
                        where n.rid == 3
                        orderby h.userid ascending
                        select new UserRole1
                        {
                            userid = h.userid,
                            userphone = h.userphone,
                            username = h.username,
                            exname = h.exname,
                            userpwd = h.userpwd,
                            idnum = h.idnum,
                            shopid = h.shopid,
                            jointime = h.jointime,
                            usercode = h.usercode,
                            useraddr = h.useraddr,
                            userQQ = h.userQQ,
                            isDel = h.isDel,
                            comment = h.comment
                        };
            return View(new GridModel()
            {
                Data = dataq,
                Total = dataq.Count()
            });
        }

        //解冻
        public ActionResult JieD(string id)
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            decimal userid = decimal.Parse(id);
            var q = db.dl_basic_users.FirstOrDefault(m => m.userid == userid);
            q.isDel = false;
            db.SaveChanges();
            var dataq = from h in db.dl_basic_users
                        join n in db.sys_user_role on h.userid equals n.userid
                        where n.rid == 3
                        orderby h.userid ascending
                        select new UserRole1
                        {
                            userid = h.userid,
                            userphone = h.userphone,
                            username = h.username,
                            exname = h.exname,
                            userpwd = h.userpwd,
                            idnum = h.idnum,
                            shopid = h.shopid,
                            jointime = h.jointime,
                            usercode = h.usercode,
                            useraddr = h.useraddr,
                            userQQ = h.userQQ,
                            isDel = h.isDel,
                            comment = h.comment
                        };
            return View(new GridModel()
            {
                Data = dataq,
                Total = dataq.Count()
            });
        }

        public ActionResult SelectVid(string id)
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            decimal productid = decimal.Parse(id);
            var data = from h in db.dl_basic_users
                       join n in db.sys_user_role on h.userid equals n.userid
                       where n.rid == 3 && h.userid == productid
                       orderby h.userid ascending
                       select new UserRole1
                       {
                           userid = h.userid,
                           userphone = h.userphone,
                           username = h.username,
                           exname = h.exname,
                           userpwd = h.userpwd,
                           idnum = h.idnum,
                           shopid = h.shopid,
                           jointime = h.jointime,
                           usercode = h.usercode,
                           useraddr = h.useraddr,
                           userQQ = h.userQQ,
                           isDel = h.isDel,
                           comment = h.comment
                       };
            return Json(new GridModel()
            {
                Data = data,
                Total = data.Count()
            });
        }
        #endregion
        #region 商家管理
        public ActionResult Boss()
        {
            return View();
        }
        [GridAction]// 用户列表
        public ActionResult UserList1()
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            var dataq = from h in db.dl_basic_users
                        join n in db.sys_user_role on h.userid equals n.userid
                        where n.rid == 2
                        orderby h.userid ascending
                        select new UserRole1
                        {
                            userid = h.userid,
                            userphone = h.userphone,
                            username = h.username,
                            exname = h.exname,
                            userpwd = h.userpwd,
                            idnum = h.idnum,
                            shopid = h.shopid,
                            jointime = h.jointime,
                            usercode = h.usercode,
                            useraddr = h.useraddr,
                            userQQ = h.userQQ,
                            isDel = h.isDel,
                            comment = h.comment
                        };
            return View(new GridModel()
            {
                Data = dataq,
                Total = dataq.Count()
            });
        }

        //冻结
        public ActionResult DongJ1(string id)
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            decimal userid = decimal.Parse(id);
            var q = db.dl_basic_users.FirstOrDefault(m => m.userid == userid);
            q.isDel = true;
            db.SaveChanges();
            var dataq = from h in db.dl_basic_users
                        join n in db.sys_user_role on h.userid equals n.userid
                        where n.rid == 2
                        orderby h.userid ascending
                        select new UserRole1
                        {
                            userid = h.userid,
                            userphone = h.userphone,
                            username = h.username,
                            exname = h.exname,
                            userpwd = h.userpwd,
                            idnum = h.idnum,
                            shopid = h.shopid,
                            jointime = h.jointime,
                            usercode = h.usercode,
                            useraddr = h.useraddr,
                            userQQ = h.userQQ,
                            isDel = h.isDel,
                            comment = h.comment
                        };
            return View(new GridModel()
            {
                Data = dataq,
                Total = dataq.Count()
            });
        }

        //解冻
        public ActionResult JieD1(string id)
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            decimal userid = decimal.Parse(id);
            var q = db.dl_basic_users.FirstOrDefault(m => m.userid == userid);
            q.isDel = false;
            db.SaveChanges();
            var dataq = from h in db.dl_basic_users
                        join n in db.sys_user_role on h.userid equals n.userid
                        where n.rid == 2
                        orderby h.userid ascending
                        select new UserRole1
                        {
                            userid = h.userid,
                            userphone = h.userphone,
                            username = h.username,
                            exname = h.exname,
                            userpwd = h.userpwd,
                            idnum = h.idnum,
                            shopid = h.shopid,
                            jointime = h.jointime,
                            usercode = h.usercode,
                            useraddr = h.useraddr,
                            userQQ = h.userQQ,
                            isDel = h.isDel,
                            comment = h.comment
                        };
            return View(new GridModel()
            {
                Data = dataq,
                Total = dataq.Count()
            });
        }

        public ActionResult SelectSid(string id)
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            decimal productid = decimal.Parse(id);
            var data = from h in db.dl_basic_users
                        join n in db.sys_user_role on h.userid equals n.userid
                        where n.rid == 2 && h.userid == productid
                        orderby h.userid ascending
                        select new UserRole1
                        {
                            userid = h.userid,
                            userphone = h.userphone,
                            username = h.username,
                            exname = h.exname,
                            userpwd = h.userpwd,
                            idnum = h.idnum,
                            shopid = h.shopid,
                            jointime = h.jointime,
                            usercode = h.usercode,
                            useraddr = h.useraddr,
                            userQQ = h.userQQ,
                            isDel = h.isDel,
                            comment = h.comment
                        };
            return Json(new GridModel()
            {
                Data = data,
                Total = data.Count()
            });
        }
        #endregion
        #region 新增管理员
        public ActionResult CreatGuan()
        {
            return View();
        }
        #endregion
    }
}
