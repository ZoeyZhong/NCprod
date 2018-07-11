using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NCprod.Models;
using Newtonsoft.Json;
using System.Text;
using System.Data;

namespace NCprod.Controllers
{
    public class HomeController : Controller
    {
        //
        // GET: /Home/

        public ActionResult Index()
        {
            if (Session["user"] == null)
                return RedirectToAction("Logon", "Home");
            return View();
        }
        public ActionResult Header()
        {
            return View();
        }
        public ActionResult Left()
        {
            return View();
        }
        public ActionResult Main()
        {
            return View();
            ////VIP客户生日提示
            //var db = new NCtecanEntities();
            //zy_basic_users users = Session["user"] as zy_basic_users;
            //var userid = users.userid;
            //var role = db.zy_sys2_role_users.Where(p => p.userid == userid).FirstOrDefault().rid;
            //if (role == 2) //团队长
            //{
            //    string today = DateTime.Now.ToString("yyyyMMdd").Substring(4, 4);
            //    var info = from a in db.f_renewalinsur
            //               where a.insuredidcard.Substring(10, 4) == today
            //               select SqlFunctions.StringConvert((decimal)a.renewalinsurid).Trim();
            //    var vip = from a in db.f_customerinfo
            //              where a.option1 == "VIP" && a.infosource == 2 && a.iftrue != 0 && info.Contains(a.infoid)
            //              select a;
            //    ViewData["num"] = vip.Count();
            //    ViewData["result"] = "1";
            //    return View(vip);
            //}
            //else
            //{
            //    ViewData["result"] = "0";
            //    return View();
            //}

        }

        
        public void LogOff()//注销
        {
            Session.Abandon();  //把当前Session对象删除了
            Session.Clear();  //把Session对象中的所有项目都删除了
            Response.Redirect("../Index.htm");//  RedirectToAction("LogOn", "Account");
        }

        #region 登录
        public ActionResult Logon()
        {
            return View();
        }
        [HttpPost]
        public string LogOn(decimal userid, string pwd)
        {
            try
            {
                using (var db = new NCtecanEntities())
                {
                    var user = db.dl_basic_users.Where(p => p.userid == userid && p.userpwd == pwd && p.isDel == false).FirstOrDefault();
                    if (user != null)
                    {
                        Session["user"] = user;
                        return "success";
                    }
                    else
                    {
                        return "密码有误！";
                    }
                }
            }
            catch (Exception ex)
            {
                return "用户名有误！" + ex.Message;
            }
        }
        #endregion

        #region 注册
        public ActionResult Regist()
        {
            return View();
        }
        [HttpPost]//保存new user
        public string Save(string json)
        {
            JsonObject obj = new JsonObject(json);
            var roleid = decimal.Parse(obj["roleid"].Value.ToString());
            var userid1 = decimal.Parse(obj["phone"].Value.ToString());
            string exname1 = obj["user"].Value.ToString();
            string userpwd1 = obj["passwd"].Value.ToString();
            try
            {
                using (var db = new NCtecanEntities())
                {
                    var uuser = db.dl_basic_users.FirstOrDefault(x => x.userid == userid1 && x.isDel == false);
                    if (uuser == null)
                    {
                        var user = new dl_basic_users()
                        {
                            userid = userid1,
                            exname = exname1,
                            userpwd = userpwd1
                        };
                        db.dl_basic_users.Add(user);
                        db.SaveChanges();

                        var nuser = new sys_user_role()
                        {
                            userid = user.userid,
                            rid = roleid
                        };
                        db.sys_user_role.Add(nuser);
                        db.SaveChanges();
                        return "success";
                    }
                    else
                    {
                        return "existed";
                    }
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
