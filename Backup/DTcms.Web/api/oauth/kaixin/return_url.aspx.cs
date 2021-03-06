﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using DTcms.API.OAuth;
using DTcms.Common;
using LitJson;

namespace DTcms.Web.api.oauth.kaixin
{
    public partial class return_url : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //取得返回參數
            string state = DTRequest.GetQueryString("state");
            string code = DTRequest.GetQueryString("code");

            string access_token = string.Empty;
            string expires_in = string.Empty;
            string client_id = string.Empty;
            string openid = string.Empty;

            if (Session["oauth_state"] == null || Session["oauth_state"].ToString() == "" || state != Session["oauth_state"].ToString())
            {
                Response.Write("出錯啦，state未初始化！");
                return;
            }
            
            //第一步：獲取Access Token
            JsonData jd = kaixin_helper.get_access_token(code, state);
            if (jd == null)
            {
                Response.Write("出錯了，無法獲取Access Token，請檢查App Key是否正確！");
                return;
            }
            access_token = jd["access_token"].ToString();
            expires_in = jd["expires_in"].ToString();

            //第二步：通過Access Token來獲取用戶的ID
            JsonData jd2 = kaixin_helper.get_info(access_token, "uid");
            if (jd2 == null)
            {
                Response.Write("出錯啦，無法獲取用戶授權uid！");
                return;
            }
            openid = jd2["uid"].ToString();
            //儲存獲取資料用到的資訊
            Session["oauth_name"] = "kaixin";
            Session["oauth_access_token"] = access_token;
            Session["oauth_openid"] = openid;

            //第三步：跳轉到指定頁面
            Response.Redirect(new Web.UI.BasePage().linkurl("oauth_login"));
            return;

        }
    }
}
