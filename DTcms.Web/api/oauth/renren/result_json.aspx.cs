﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.OAuth;
using DTcms.Common;
using LitJson;

namespace DTcms.Web.api.oauth.renren
{
    public partial class result_json : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string oauth_access_token = string.Empty;
            string oauth_openid = string.Empty;
            string oauth_name = string.Empty;

            if (Session["oauth_name"] == null || Session["oauth_access_token"] == null || Session["oauth_openid"] == null)
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出錯啦，Access Token已過期或不存在！\"}");
                return;
            }
            oauth_name = Session["oauth_name"].ToString();
            oauth_access_token = Session["oauth_access_token"].ToString();
            oauth_openid = Session["oauth_openid"].ToString();
            JsonData jd1 = renren_helper.get_info(oauth_access_token, "uid,name,sex,birthday,mainurl");
            if (jd1 == null)
            {
                Response.Write("{\"ret\":\"1\", \"msg\":\"出錯啦，無法獲取授權使用者資料！\"}");
                return;
            }
            JsonData jd = jd1[0];

            StringBuilder str = new StringBuilder();
            str.Append("{");
            str.Append("\"ret\": \"0\", ");
            str.Append("\"msg\": \"獲得使用者資料成功！\", ");
            str.Append("\"oauth_name\": \"" + oauth_name + "\", ");
            str.Append("\"oauth_access_token\": \"" + oauth_access_token + "\", ");
            str.Append("\"oauth_openid\": \"" + jd["uid"].ToString() + "\", ");
            str.Append("\"nick\": \"" + jd["name"].ToString() + "\", ");
            str.Append("\"avatar\": \"" + jd["mainurl"].ToString() + "\", ");
            if (jd["sex"].ToString() == "1")
            {
                str.Append("\"sex\": \"男\", ");
            }
            else if (jd["sex"].ToString() == "0")
            {
                str.Append("\"sex\": \"女\", ");
            }
            else
            {
                str.Append("\"sex\": \"保密\", ");
            }
            str.Append("\"birthday\": \"" + jd["birthday"].ToString() + "\"");
            str.Append("}");

            Response.Write(str.ToString());
            return;
        }
    }
}
