using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text;
using System.Web.Script.Serialization;

namespace BasicLearn.Javascript.Part1
{
    public partial class Index : System.Web.UI.Page
    {
        protected string ItemValue;
        protected string ParamsValue;

        protected void Page_Load(object sender, EventArgs e)
        {
            string[] allKeys = Request.QueryString.AllKeys;
            if (allKeys.Length == 0)
            {
                //Response.Redirect("Index.aspx?name=keily", true);
                Response.Redirect("Index.aspx?name=keily&name=" + HttpUtility.UrlEncode("5,6,7"), true);
            }

            ItemValue = Request["name"];
            ParamsValue = Request.Params["name"];

            StringBuilder sb = new StringBuilder();
            foreach (string key in allKeys)
                sb.AppendFormat("{0} = {1}<br />",
                    HttpUtility.HtmlEncode(key), HttpUtility.HtmlEncode(Request.QueryString[key]));

            Response.Write(sb.ToString());

            //ie9 每个域下的cookie不能超过50，否则会覆盖
            //chrome每个域下的cookie不能超过150，否则会覆盖
            /*
            for (int i = 0; i < 10; i++)
            {
                char[] a=new char[1024];
                a[0] = (char)i;
                HttpCookie cookie = new HttpCookie("mytestcookie" + i.ToString(), new string(a));
                cookie.Expires = new DateTime(1900, 1, 1);
                Response.Cookies.Add(cookie);
            }
            */
            WriteCookie_2b();
            ReadCookie_2b();
            
        }

        private void WriteCookie_2b()
        {
            DisplaySettings setting = new DisplaySettings { Style = 2, Size = 48 };

            HttpCookie cookie = new HttpCookie("DisplaySettings2", setting.ToJson());
            Response.Cookies.Add(cookie);
        }

        private void ReadCookie_2b()
        {
            HttpCookie cookie = Request.Cookies["DisplaySettings2"];
            if (cookie == null)
                Response.Write("未定义");
            else
            {
                DisplaySettings setting = cookie.FromJson<DisplaySettings>();
                Response.Write(setting.ToString()); 
            }
        }
    }

    public class DisplaySettings
    {
        public int Style;
        public int Size;
        public override string ToString()
        {
            return string.Format("Style = {0}, Size = {1}", this.Style, this.Size);
        }
    }
}