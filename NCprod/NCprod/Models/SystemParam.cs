using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NCprod.Models
{
    public class SystemParam
    {
        /// <summary>
        ///  获取当前登录用户
        /// </summary>
        public static dl_basic_users CurrentUser
        {
            get { return (HttpContext.Current.Session["user"] as dl_basic_users); }
        }
        public static string EnvironmentPath
        {
            get { return HttpContext.Current.Server.MapPath("~"); }
        }
    }
}