<%@ WebHandler Language="C#" Class="ValidateImageHandler" %>

using System;
using System.Web;
using System.Web.SessionState;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text;

/// <summary>
/// ValidateImageHandler 生成网站验证码功能
/// </summary>
public class ValidateImageHandler : IHttpHandler, IRequiresSessionState
{
    int intLength = 4;               //长度
    string strIdentify = "kyip"; //随机字串存储键值，以便存储到Session中
    public void ProcessRequest(HttpContext hc)
    {
        //设置输出流图片格式
        hc.Response.ContentType = "image/gif";

        Bitmap b = new Bitmap(60, 20);
        Graphics g = Graphics.FromImage(b);
        g.FillRectangle(new SolidBrush(Color.YellowGreen), 0, 0, 60, 20);
        Font font = new Font(FontFamily.GenericSerif, 20, FontStyle.Bold, GraphicsUnit.Pixel);
        Random r = new Random();

        //合法随机显示字符列表
        string strLetters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        StringBuilder s = new StringBuilder();

        //将随机生成的字符串绘制到图片上
        for (int i = 0; i < intLength; i++)
        {
            s.Append(strLetters.Substring(r.Next(0, strLetters.Length - 1), 1));
            g.DrawString(s[s.Length - 1].ToString(), font, new SolidBrush(Color.Blue), i * 12, r.Next(0, 2));
        }

        //生成干扰线条
        Pen pen = new Pen(new SolidBrush(Color.Blue), 2);
        for (int i = 0; i < 4; i++)
        {
            g.DrawLine(pen, new Point(r.Next(0, 199), r.Next(0, 59)), new Point(r.Next(0, 199), r.Next(0, 59)));
        }
        b.Save(hc.Response.OutputStream, ImageFormat.Gif);
        hc.Session[strIdentify] = s.ToString(); //先保存在Session中，验证与用户输入是否一致
        hc.Response.End();
    }

    public bool IsReusable
    {
        get
        {
            return false;
        }
    }

}