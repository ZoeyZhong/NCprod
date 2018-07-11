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

namespace NCprod.Controllers
{
    public class PeopleController : Controller
    {
        //
        // GET: /People/

        public ActionResult Index()
        {
            return View();
        }
        #region 修改密码
        public ActionResult EditPsd()
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            NCtecanEntities db = new NCtecanEntities();
            dl_basic_users dd = db.dl_basic_users.Where(s => s.userid == userid1).First();
            ViewData["username"] = dd.username;//获取用户密码的值
            ViewData["userid"] = dd.userid;
            ViewData["userpwd"] = dd.userpwd;
            return View();
        }
        [HttpPost]//保存SaveNewPassword
        public ActionResult SaveNewPassword(string newPassword, string old, decimal userid)
        {
            var result = new ResultInfo(false);
            try
            {
                using (var db = new NCtecanEntities())
                {
                    var user = db.dl_basic_users.FirstOrDefault(s => s.userid == userid);
                    if (user.userpwd != old)
                    {
                        result.IsSucceed = false;
                        result.Message = "原密码错误！";
                        return Json(result);
                    }
                    user.userpwd = newPassword;
                    if (db.SaveChanges() > 0)
                    {
                        result.IsSucceed = true;
                        result.Message = "保存成功";
                    }
                    else
                    {
                        result.IsSucceed = false;
                        result.Message = "保存失败db.savechangs()<=0";
                    }
                }
            }
            catch (Exception e)
            {
                result.IsSucceed = false;
                result.Message = e.Message;
            }
            return Json(result);
        }
        #endregion
        #region 资料修改
         public ActionResult EditMassage()
        {
             dl_basic_users user = Session["user"] as dl_basic_users;
             decimal userid1 = user.userid;
             NCtecanEntities db = new NCtecanEntities();
             dl_basic_users dd = db.dl_basic_users.Where(s => s.userid == userid1).First();
             ViewData["userid"] = dd.userid;//用户ID，手机号码
             ViewData["userphone"] = dd.userphone;
             ViewData["exname"] = dd.exname;
             ViewData["username"] = dd.username;
             ViewData["idnum"] = dd.idnum;
             ViewData["shopid"] = dd.shopid;
             ViewData["jointime"] = dd.jointime;
             ViewData["useraddr"] = dd.useraddr;
             ViewData["userQQ"] = dd.userQQ;
             ViewData["comment"] = dd.comment;
             return View();
        }
         [HttpPost]//保存新资料
         public string SaveM(string json)
         {
             JsonObject obj = new JsonObject(json);
             dl_basic_users user = Session["user"] as dl_basic_users;
             decimal userid = user.userid;
             string userphone = obj["userphone"].Value.ToString();
             string exname = obj["exname"].Value.ToString();
             string username = obj["username"].Value.ToString();
             string idnum = obj["idnum"].Value.ToString();
             string useraddr = obj["useraddr"].Value.ToString();
             string userQQ = obj["userQQ"].Value.ToString();
             string comment = obj["comment"].Value.ToString();
             try
             {
                 using (var db = new NCtecanEntities())
                 {
                     var uuser = db.dl_basic_users.FirstOrDefault(x => x.userid == userid);
                     if (uuser != null)
                     {
                         uuser.userphone = userphone;
                         uuser.exname = exname;
                         uuser.username = username;
                         uuser.idnum = idnum;
                         uuser.useraddr = useraddr;
                         uuser.userQQ = userQQ;
                         uuser.comment = comment;
                     }
                     if (db.SaveChanges() > 0)
                         return "success";
                     else
                         return "existed";
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
