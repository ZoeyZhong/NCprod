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
    public class RightController : Controller
    {
        //
        // GET: /Right/

        public ActionResult Index()
        {
            return View();
        }
        #region  系统功能
        public ActionResult FunctionManage()
        {
            return View();
        }
        public string GetFunction()
        {
            try
            {
                decimal fid;
                if (!String.IsNullOrEmpty(Request.QueryString["fid"]))
                {
                    fid = decimal.Parse(Request.QueryString["fid"].ToString());
                }
                else throw new Exception("error:传值有误！");
                using (var db = new NCtecanEntities())
                {
                    var data = (from f in db.sys_function
                                where f.fid == fid
                                select new
                                {
                                    f.fid,
                                    f.pid,
                                    funname = f.function,
                                    f.title,
                                    f.control,
                                    f.functionname,
                                    f.parameters,
                                    f.url
                                }).First();
                    return JsonHelper.ObjectToJSON(data);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public string GetFunctionTree()
        {
            try
            {
                using (var db = new NCtecanEntities())
                {
                    var data = (from f in db.sys_function
                                orderby f.pid, f.fid
                                select new
                                {
                                    id = f.fid,
                                    pId = f.pid,
                                    name = f.title,
                                    isParent = f.pid == 0
                                }).ToList();
                    return JsonConvert.SerializeObject(data);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        [HttpPost]
        public string SaveFunction(string json)
        {
            using (var db = new NCtecanEntities())
            {

                sys_function m = new sys_function();
                JsonObject obj = new JsonObject(json);
                decimal fid = decimal.Parse(obj["id"].Value);
                if (obj["id"].Value != "0")
                    m = db.sys_function.FirstOrDefault(ss => ss.fid == fid);
                m.pid = int.Parse(obj["pid"].Value);
                m.title = obj["name"].Value;
                m.function = obj["funname"].Value;
                m.control = obj["control"].Value;
                m.functionname = obj["functionname"].Value;
                m.parameters = obj["parameters"].Value;
                m.url = obj["comment"].Value;
                if (obj["id"].Value == "0")
                {
                    db.sys_function.Add(m);
                }

                if (db.SaveChanges() > 0)
                {
                    return GetFunctionTree();
                }
                else
                    return "failed";
            }
        }
        [HttpPost]
        public string DeleteByID(string fid)
        {
            using (var db = new NCtecanEntities())
            {
                decimal id = decimal.Parse(fid);
                var m = db.sys_function.Where(c => c.fid == id).First();

                db.sys_function.Remove(m);
                if (db.SaveChanges() > 0)
                {
                    return GetFunctionTree();
                }
                else return "failed";
            }
        }
        #endregion

        #region RoleFunctions
        public ActionResult RoleFunctions()//角色权限
        {
            //ViewBag.Navigation = "系统配置 → 角色定义";
            return View();
        }

        [HttpPost]
        public string SaveRoleFunction(string json)
        {
            try
            {
                using (var db = new NCtecanEntities())
                {
                    JsonObject obj = new JsonObject(json);
                    decimal rid = decimal.Parse(obj["rid"].Value);

                    foreach (JsonProperty jp in obj["funs"].Items)
                    {
                        decimal fid = decimal.Parse(jp.Items[0]["fid"].Value);
                        bool selected = jp.Items[0]["selected"].ToString().ToLower() == "true";
                        int count = db.sys_role_function.Where(c => c.rid == rid && c.fid == fid).Count();
                        if (selected)  //选中
                        {
                            if (count == 0)  //但数据库中没有
                            {
                                var m = new sys_role_function() { rid = rid, fid = fid };
                                db.sys_role_function.Add(m);
                            }
                        }
                        else   //未选中
                        {
                            if (count == 1)
                            {
                                var m = db.sys_role_function.First(c => c.rid == rid && c.fid == fid);
                                db.sys_role_function.Remove(m);
                            }
                        }
                    }
                    if (db.SaveChanges() > 0) return "success";
                    else return "failed";
                }
            }
            catch (Exception ex)
            {
                return ex.StackTrace;
            }
        }
        public string GetFunctionsByRole(string rid)
        {
            try
            {
                using (var db = new NCtecanEntities())
                {
                    decimal id = decimal.Parse(rid);
                    var data = from t in db.sys_role_function
                               where t.rid == id
                               select new { rid = t.rid, fid = t.fid };
                    return JsonConvert.SerializeObject(data);
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
