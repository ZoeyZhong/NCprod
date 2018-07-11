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
    public class ContentController : Controller
    {
        //
        // GET: /Content/

        public ActionResult Index()
        {
            return View();
        }
        #region 商品列表
        
        public ActionResult Product()
        {
            return View();
        }
        [GridAction]// 商品列表
        public ActionResult ProductList()
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            var data = from b in db.t_f_commodity
                       join a in db.sys_role on b.option1 equals a.option1
                       where b.com_belong == userid1 && b.isdel == false
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
                    Total = data.Count()
                });
        }
        #endregion
        #region 新增商品
        public ActionResult Creat()
        {
            // 加验证
            NCtecanEntities db = new NCtecanEntities();
            List<SelectListItem> product = new List<SelectListItem>();
            List<SelectListItem> prod = new List<SelectListItem>();
            var m = from a in db.sys_role
                    where a.rid == 4 || a.rid == 5 || a.rid == 6 || a.rid == 7 || a.rid == 8 || a.rid == 9
                    select new
                    {
                        rid = a.option1,
                        rolename = a.rolename
                    };
            foreach (var o in m)
            {
                product.Add(new SelectListItem
                    {
                        Text = o.rolename,
                        Value = o.rid.ToString()
                    });
            }
            ViewData["product"] = new SelectList(product,"Value","Text");
            return View();
        }
        [HttpPost]// 保存new product
        public string Save(string json)
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            JsonObject obj = new JsonObject(json);
            decimal com_price = decimal.Parse(obj["com_price"].Value.ToString());
            decimal  com_belong = userid1;
            string com_name = obj["com_name"].Value.ToString();
            string com_number = obj["com_number"].Value.ToString();
            string option1 = obj["option1"].Value.ToString();
            decimal com_postage = decimal.Parse(obj["com_postage"].Value.ToString());
            string com_content = obj["com_content"].Value.ToString(); 
            System.DateTime com_starttime = new System.DateTime();
            com_starttime = DateTime.Now;
            //decimal shop_ID 
            string comment = obj["com_comment"].Value.ToString();
            try
            {
                using (var db = new NCtecanEntities())
                {
                    var product = new t_f_commodity()
                    {
                        com_price = com_price,
                        com_belong = com_belong,
                        com_name = com_name,
                        com_number = com_number,
                        com_postage = com_postage,
                        com_content = com_content,
                        com_starttime = com_starttime,
                        comment = comment,
                        option1 = option1,
                        isdel = false
                    };
                    db.t_f_commodity.Add(product);
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
        #region 编辑商品信息
        public ActionResult Edit(string id)
        {
            NCtecanEntities db = new NCtecanEntities();
            List<SelectListItem> product = new List<SelectListItem>();
            var m = from a in db.sys_role
                    where a.rid == 4 || a.rid == 5 || a.rid == 6 || a.rid == 7 || a.rid == 8 || a.rid == 9
                    select new
                    {
                        rid = a.option1,
                        rolename = a.rolename
                    };
            foreach (var o in m)
            {
                product.Add(new SelectListItem
                {
                    Text = o.rolename,
                    Value = o.rid.ToString()
                });
            }
            ViewData["product"] = new SelectList(product, "Value", "Text");



            decimal productid = decimal .Parse(id);
            t_f_commodity dd = db.t_f_commodity.Where(s => s.com_ID == productid).First();
            ViewData["com_ID"] = dd.com_ID;//商品ID
            ViewData["com_price"] = dd.com_price;//商品价格
            ViewData["com_belong"] = dd.com_belong;//商品所属用户
            ViewData["com_name"] = dd.com_name;//商品名称
            ViewData["com_number"] = dd.com_number;//商品数量
            ViewData["com_postage"] = dd.com_postage;//商品邮费
            ViewData["com_content"] = dd.com_content;//商品图片
            ViewData["com_state"] = dd.com_state;//商品状态
            ViewData["com_starttime"] = dd.com_starttime;//新增时间
            ViewData["comment"] = dd.comment;//商品介绍
            ViewData["option1"] = dd.option1;//商品类别
            return View();
        }
        [HttpPost]// 保存edit product
        public string SaveEdit(string json)
        {
            JsonObject obj = new JsonObject(json);
            decimal com_ID = decimal.Parse(obj["com_ID"].Value.ToString());
            decimal com_price = decimal.Parse(obj["com_price"].Value.ToString());
            string com_name = obj["com_name"].Value.ToString();
            string com_number = obj["com_number"].Value.ToString();
            decimal com_postage = decimal.Parse(obj["com_postage"].Value.ToString());
            string com_content = obj["com_content"].Value.ToString();
            //decimal shop_ID 
            string comment = obj["com_comment"].Value.ToString();
            string option1 = obj["option1"].Value.ToString();
            try
            {
                using (var db = new NCtecanEntities())
                {
                    var product = db.t_f_commodity.FirstOrDefault(s => s.com_ID == com_ID);
                    product.com_price = com_price;
                    product.com_name = com_name;
                    product.com_number = com_number;
                    product.com_postage = com_postage;
                    product.comment = comment;
                    product.option1 = option1;
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
        #region 删除
        public ActionResult Delete(string id)
        {
            dl_basic_users user = Session["user"] as dl_basic_users;
            decimal userid1 = user.userid;
            var db = new NCtecanEntities();
            decimal productid = decimal.Parse(id);
            var q = db.t_f_commodity.FirstOrDefault(m =>m.com_ID == productid);
            q.isdel = true;
            db.SaveChanges();
            var data = from b in db.t_f_commodity
                       where b.com_belong == userid1 && b.isdel == false
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
                           comment = b.comment //商品备注
                       };
            return Json(new GridModel()
                {
                    Data = data,
                    Total = data.Count()
                });
        }
        #endregion



        #region 图片传入服务器数据库
        ////案例
        //public ActionResult Photo_Save(HttpPostedFileBase pic_upload)
        //{
        //    //获取图片的类型
        //    string filetype = pic_upload.ContentType;
        //    //获取图片的路径
        //    string fileName = pic_upload.FileName;

        //    //判断格式
        //    if (filetype == ".jpg" || filetype == ".png")
        //    {

        //        //转换只取文件名，去掉路径。
        //        if (fileName.LastIndexOf("\\") > -1)
        //        {
        //            fileName = fileName.Substring(fileName.LastIndexOf("\\") + 1);
        //        }
        //        //保存到相对路径下。
        //        pic_upload.SaveAs(Server.MapPath("../../Upload/" + fileName));
        //        //以下代码是将路径保存到数据库。
        //        string ImagePath = "../../Upload/" + fileName;
        //        string sql = "insert into t_f_commodity(com_content)values('" + ImagePath + "')";
        //        //插入到数据库
        //        dl_basic_users user = Session["user"] as dl_basic_users;
        //        decimal userid1 = user.userid;
        //        try
        //        {
        //            using (var db = new NCtecanEntities())
        //            {
        //                var product = db.t_f_commodity.FirstOrDefault(s => s.com_belong == userid1);
        //                product.com_content = ImagePath;
        //                db.SaveChanges();
        //                return Content("1|上传成功!|");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            return Content("0|网络繁忙,请稍后在试！");
        //        }
        //    }
        //    else
        //    {
        //        return Content("0|请选择.jpg或.png格式的图片！");
        //    }






        //    //封装好的代码，直接调用。
        //    NCtecanEntities db = new NCtecanEntities();
        //    db.gentConn();//这是啥？
        //    int result = db.executeUpdate(sql);
        //    return View();




        //    string serverUrl = "http://http://localhost:2666/"; //IP地址
        //    string logoUrl = serverUrl + "/Upload/";
        //    string filePath = "Upload";
        //    string logoId = Guid.NewGuid().ToString();
        //    if (pic_upload != null)
        //    {
        //        string fileType = pic_upload.ContentType;
        //        Stream stream = pic_upload.InputStream;
        //        BinaryReader br = new BinaryReader(stream);
        //        byte[] fileByte = br.ReadBytes((int)stream.Length);
        //        string baseFileString = Convert.ToBase64String(fileByte);

        //        logoUrl = GetUploadImgUrl(baseFileString, serverUrl, logoId, filePath);
        //        if (string.IsNullOrEmpty(logoUrl))
        //        {
        //            return Content("0|网络繁忙,请稍后在试！");
        //        }

        //    }
        //    string imgPath = "http://http://localhost:2666/Upload/" + logoId + ".jpg";
        //    return Content("1|上传成功!|" + imgPath);
        //}
        ///// <summary>
        ///// 上传图片
        ///// </summary>
        ///// <param name="imgString">base64编码过的二进制图片</param>
        ///// <returns></returns>
        //public static string GetUploadImgUrl(string imgString, string serverUrl, string fileName, string path)
        //{
        //    //按类型设置图片大小
        //    int big_width = 360;
        //    int big_height = 265;
        //    int small_width = 150;
        //    int small_height = 100;

        //    string result = string.Empty;
        //    try
        //    {
        //        string postData = string.Format("clientKey={0}&data={1}&fileName={2}&path={3}&big_width={4}&big_height={5}&small_width={6}&small_height={7}", "www.910jq.com",
        //            imgString.Replace("+", "%2B"), fileName, path, big_width, big_height, small_width, small_height);
        //        RequestHelper p = new RequestHelper();
        //        result = p.ResponseToString(p.doPost(serverUrl + "/Pic_Upload.ashx", postData));
        //    }
        //    catch
        //    {
        //        result = "";
        //    }
        //    return result;
        //}
#endregion
    }
}
