﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using DTcms.Common;

namespace DTcms.Web.UI.Page
{
    public partial class payment : Web.UI.BasePage
    {
        protected string action;
        protected string order_type = string.Empty;
        protected string order_no = string.Empty;
        protected decimal order_amount = 0;

        protected Model.users userModel;
        protected Model.orders orderModel;
        protected Model.amount_log amountModel;
        protected Model.payment payModel;

        /// <summary>
        /// 重写父类的虚方法,此方法将在Init事件前执行
        /// </summary>
        protected override void ShowPage()
        {
            this.Init += new EventHandler(payment_Init); //加入Init事件
        }

        /// <summary>
        /// 将在Init事件执行
        /// </summary>
        protected void payment_Init(object sender, EventArgs e)
        {
            //取得处事类型
            action = DTRequest.GetString("action");
            order_type = DTRequest.GetString("order_type");
            order_no = DTRequest.GetString("order_no");
            
            switch (action)
            {
                case "confirm":
                    if (string.IsNullOrEmpty(action) || string.IsNullOrEmpty(order_type) || string.IsNullOrEmpty(order_no))
                    {
                        HttpContext.Current.Response.Redirect(config.webpath + "error.aspx?msg=" + Utils.UrlEncode("出錯啦，URL傳輸參數有誤！"));
                        return;
                    }
                    //检查用户是否登录
                    userModel = new Web.UI.BasePage().GetUserInfo();
                    if (userModel == null)
                    {
                        //用户未登录
                        HttpContext.Current.Response.Redirect(linkurl("payment", "login"));
                        return;
                    }
                    //检查订单的类型(充值或购物)
                    if (order_type == DTEnums.AmountTypeEnum.Recharge.ToString()) //充值
                    {
                        amountModel = new BLL.amount_log().GetModel(order_no);
                        if (amountModel == null)
                        {
                            HttpContext.Current.Response.Redirect(config.webpath + "error.aspx?msg=" + Utils.UrlEncode("出錯啦，訂單號不存在或已刪除！"));
                            return;
                        }
                        //检查订单号是否已支付
                        if (amountModel.status == 1)
                        {
                            HttpContext.Current.Response.Redirect(linkurl("payment1", "succeed", order_type, amountModel.order_no));
                            return;
                        }
                        //检查支付方式
                        payModel = new BLL.payment().GetModel(amountModel.payment_id);
                        if (payModel == null)
                        {
                            HttpContext.Current.Response.Redirect(config.webpath + "error.aspx?msg=" + Utils.UrlEncode("出錯啦，支付方式不存在或已被刪除！"));
                            return;
                        }
                        //检查是否线上支付
                        if (payModel.type == 2)
                        {
                            HttpContext.Current.Response.Redirect(config.webpath + "error.aspx?msg=" + Utils.UrlEncode("出錯啦，帳戶充值不允許線下支付！"));
                            return;
                        }
                        order_amount = amountModel.value; //订单金额
                    }
                    else if (order_type == DTEnums.AmountTypeEnum.BuyGoods.ToString()) //购物
                    {
                        //检查订单是否存在
                        orderModel = new BLL.orders().GetModel(order_no);
                        if (orderModel == null)
                        {
                            HttpContext.Current.Response.Redirect(config.webpath + "error.aspx?msg=" + Utils.UrlEncode("出錯啦，訂單號不存在或已刪除！"));
                            return;
                        }
                        //检查是否已支付过
                        if (orderModel.payment_status == 2)
                        {
                            HttpContext.Current.Response.Redirect(linkurl("payment1", "succeed", order_type, orderModel.order_no));
                            return;
                        }
                        //检查支付方式
                        payModel = new BLL.payment().GetModel(orderModel.payment_id);
                        if (payModel == null)
                        {
                            HttpContext.Current.Response.Redirect(config.webpath + "error.aspx?msg=" + Utils.UrlEncode("出錯啦，支付方式不存在或已被刪除！"));
                            return;
                        }
                        //检查是否线下付款
                        if (payModel.type == 2)
                        {
                            HttpContext.Current.Response.Redirect(linkurl("payment1", "succeed", order_type, orderModel.order_no));
                            return;
                        }
                        //检查是否积分换购，直接跳转成功页面
                        if (orderModel.order_amount == 0)
                        {
                            //修改订单状态
                            bool result = new BLL.orders().UpdateField(orderModel.order_no, "payment_status=2,payment_time='" + DateTime.Now + "'");
                            if (!result)
                            {
                                HttpContext.Current.Response.Redirect(linkurl("payment", "error"));
                                return;
                            }
                            HttpContext.Current.Response.Redirect(linkurl("payment1", "succeed", order_type, orderModel.order_no));
                            return;
                        }
                        order_amount = orderModel.order_amount; //订单金额
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect(config.webpath + "error.aspx?msg=" + Utils.UrlEncode("出錯啦，找不到您要提交的訂單類型！"));
                        return;
                    }
                    break;
                case "succeed":
                    //检查订单的类型(充值或购物)
                    if (order_type == DTEnums.AmountTypeEnum.Recharge.ToString()) //充值
                    {
                        amountModel = new BLL.amount_log().GetModel(order_no);
                        if (amountModel == null)
                        {
                            HttpContext.Current.Response.Redirect(config.webpath + "error.aspx?msg=" + Utils.UrlEncode("出錯啦，訂單號不存在或已刪除！"));
                            return;
                        }

                    }
                    else if (order_type == DTEnums.AmountTypeEnum.BuyGoods.ToString()) //购物
                    {
                        orderModel = new BLL.orders().GetModel(order_no);
                        if (orderModel == null)
                        {
                            HttpContext.Current.Response.Redirect(config.webpath + "error.aspx?msg=" + Utils.UrlEncode("出錯啦，訂單號不存在或已刪除！"));
                            return;
                        }
                    }
                    else
                    {
                        HttpContext.Current.Response.Redirect(config.webpath + "error.aspx?msg=" + Utils.UrlEncode("出錯啦，找不到您要提交的訂單類型！"));
                        return;
                    }
                    break;
            }
        }
    }
}