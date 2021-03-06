﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Web;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class userorder_show : Web.UI.UserPage
    {
        protected int id;
        protected Model.orders model;
        protected Model.payment payModel;

        /// <summary>
        /// 重写虚方法,此方法在Init事件执行
        /// </summary>
        protected override void InitPage()
        {
            id = DTRequest.GetQueryInt("id");
            BLL.orders bll = new BLL.orders();
            if (!bll.Exists(id))
            {
                HttpContext.Current.Response.Redirect(config.webpath + "error.aspx?msg=" + Utils.UrlEncode("出錯啦，您要瀏覽的頁面不存在或已刪除啦！"));
                return;
            }
            model = bll.GetModel(id);
            payModel = new BLL.payment().GetModel(model.payment_id);
            if (payModel == null)
            {
                payModel = new Model.payment();
            }
        }

    }
}
