﻿using System;
using System.Text;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.admin.goods
{
    public partial class edit_bj : Web.UI.ManagePage
    {
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作類型
        private int channel_id;
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            this.channel_id = DTRequest.GetQueryInt("channel_id");

            if (this.channel_id == 0)
            {
                JscriptMsg("頻道參數不正確！", "back", "Error");
                return;
            }
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改類型
                this.id = DTRequest.GetQueryInt("id");
                if (this.id == 0)
                {
                    JscriptMsg("傳輸參數不正確！", "back", "Error");
                    return;
                }
                if (!new BLL.article().Exists(this.id))
                {
                    JscriptMsg("資料不存在或已被刪除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                GetArea();
                TreeBind(this.channel_id); //綁定類別
                GroupBind(""); //綁定用戶組
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
                else
                {
                    LitAttributeList.Text = GetAttributeHtml(null, this.channel_id, this.id);
                }
            }
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                if (channel_id == 5)
                {
                    //trid.Visible = true;
                }
            }
            else
            {
                // trid.Visible = false;
            }
        }

        #region 賦值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.article bll = new BLL.article();
            Model.article_goods model = bll.GetGoodsModel(_id);

            ddlCategoryId.SelectedValue = model.category_id.ToString();
            txtTitle.Text = model.title;
            // txtGoodsNo.Text = model.goods_no;
            // txtStockQuantity.Text = model.stock_quantity.ToString();
            //txtMarketPrice.Text = model.market_price.ToString();
            //txtSellPrice.Text = model.sell_price.ToString();
            //txtPoint.Text = model.point.ToString();
            txtLinkUrl.Text = model.link_url;
            txtTime.Text = model.add_time.ToString();
            //txtuser.Text = model.lianxiren;
            // txtshangpintype.Text = model.shangpinType;
            //if (model.is_msg == 1)
            //{
            //    cblItem.Items[0].Selected = true;
            //}
            //if (model.is_top == 1)
            //{
            //    cblItem.Items[1].Selected = true;
            //}
            //if (model.is_red == 1)
            //{
            //    cblItem.Items[2].Selected = true;
            //}
            //if (model.is_hot == 1)
            //{
            //    cblItem.Items[3].Selected = true;
            //}
            //if (model.is_slide == 1)
            //{
            //    cblItem.Items[4].Selected = true;
            //}
            //if (model.is_lock == 1)
            //{
            //    cblItem.Items[5].Selected = true;
            //}
            //txtSortId.Text = model.sort_id.ToString();
            //txtClick.Text = model.click.ToString();
            //txtDiggGood.Text = model.digg_good.ToString();
            //txtDiggBad.Text = model.digg_bad.ToString();
            //txtContent.Value = model.content;
            //txtSeoTitle.Text = model.seo_title;
            //txtSeoKeywords.Text = model.seo_keywords;
            //txtSeoDescription.Text = model.seo_description;
            //賦值用戶組價格
            //if (model.goods_group_prices != null)
            //{
            //    for (int i = 0; i < this.rptPrice.Items.Count; i++)
            //    {
            //        int hideId = Convert.ToInt32(((HiddenField)this.rptPrice.Items[i].FindControl("hideGroupId")).Value);
            //        foreach (Model.goods_group_price modelt in model.goods_group_prices)
            //        {
            //            if (hideId == modelt.group_id)
            //            {
            //                ((HiddenField)this.rptPrice.Items[i].FindControl("hidePriceId")).Value = modelt.id.ToString();
            //                ((TextBox)this.rptPrice.Items[i].FindControl("txtGroupPrice")).Text = modelt.price.ToString();
            //            }
            //        }
            //    }
            //}
            //賦值上傳的相冊
            focus_photo.Value = model.img_url; //封面圖片
            LitAlbumList.Text = GetAlbumHtml(model.albums, model.img_url);
            //賦值屬性清單
            LitAttributeList.Text = GetAttributeHtml(model.attribute_values, this.channel_id, _id);
            //cblItem.SelectedValue = model.Type.ToString();
            //CheckBoxList1.SelectedValue = model.Postid;

            //ddlquyu.SelectedValue = model.quyu.ToString();
            //ddljiaqian.SelectedValue = model.jiaqianQJ.ToString();
            //ddlmianji.SelectedValue = model.mianji.ToString();
            //ddlhuxing.SelectedValue = model.huxing.ToString();
            //ddlfangshi.SelectedValue = model.fangshi.ToString();
            //ddlditie.SelectedValue = model.xianlu.ToString();

            //txtyajin.Text = model.yajin.ToString();
            //txtzuoxiang.Text = model.zuoxiang;
            //txtlouceng.Text = model.louceng;
            //txtxingneng.Text = model.xingneng;
            //txtyongtu.Text = model.yongtu;
            //txtchewei.Text = model.chewei;
            //txtshequ.Text = model.shequ;
            txtdizhi.Text = model.dizhi;

            //txtgongsi.Text = model.gongsi;
            txtfuwxiangmu.Text = model.fuwuxiangju;
            txtdianhua.Text = model.dianhua;
            //ddlAreaid.SelectedValue = model.Areaid.ToString();
            //xiajiacheck.Checked = model.Status == 2 ? true : false;
            //xiajiatext.Value = model.xiajialiyou;
        }
        #endregion

        #region 綁定類別=================================
        private void TreeBind(int _channel_id)
        {
            BLL.category bll = new BLL.category();
            DataTable dt = bll.GetList(0, _channel_id);

            this.ddlCategoryId.Items.Clear();
            this.ddlCategoryId.Items.Add(new ListItem("請選擇類別...", "0"));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = Utils.StringToNum(dr["class_layer"].ToString());
                string Title = dr["title"].ToString().Trim();

                if (ClassLayer == 1)
                {
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
                else
                {
                    Title = "├ " + Title;
                    Title = Utils.StringOfChar(ClassLayer - 1, "　") + Title;
                    this.ddlCategoryId.Items.Add(new ListItem(Title, Id));
                }
            }
        }
        #endregion

        /// <summary>
        /// 綁定市區
        /// </summary>
        private void GetArea()
        {
            DAL.AreaDal dalare = new DAL.AreaDal();
            int count = 0;
            var table = dalare.GetDatalistpage(99999, 1, " parent=0", " sort", out count);
            if (table.Tables.Count > 0)
            {
                foreach (DataRow dr in table.Tables[0].Rows)
                {
                    string Id = dr["id"].ToString();
                    string Title = dr["title"].ToString().Trim();
                    //ddlAreaid.Items.Add(new ListItem(Title, Id));

                    var table2 = dalare.GetDatalistpage(99999, 1, " parent=" + Id, " sort", out count);
                    if (table2.Tables.Count > 0)
                    {
                        foreach (DataRow dr2 in table2.Tables[0].Rows)
                        {
                            string Id2 = dr2["id"].ToString();
                            string Title2 = dr2["title"].ToString().Trim();
                            Title2 = "├ " + Title2;
                            Title2 = Utils.StringOfChar(2 - 1, "　") + Title2;
                            //ddlAreaid.Items.Add(new ListItem(Title2, Id2));
                        }
                    }
                }
            }
        }

        #region 綁定會員組===============================
        private void GroupBind(string strWhere)
        {
            BLL.user_groups bll = new BLL.user_groups();
            //DataSet ds = bll.GetList(0, strWhere, "grade asc,id desc");
            //if (ds.Tables[0].Rows.Count > 0)
            //{
            //    this.rptPrice.DataSource = ds;
            //    this.rptPrice.DataBind();
            //}
        }
        #endregion

        #region 返回相簿清單HMTL=========================
        private string GetAlbumHtml(List<Model.article_albums> models, string focus_photo)
        {
            StringBuilder strTxt = new StringBuilder();
            if (models != null)
            {
                foreach (Model.article_albums modelt in models)
                {
                    strTxt.Append("<li>\n");
                    strTxt.Append("<input type=\"hidden\" name=\"hide_photo_name\" value=\"" + modelt.id + "|" + modelt.big_img + "|" + modelt.small_img + "\" />\n");
                    strTxt.Append("<input type=\"hidden\" name=\"hide_photo_remark\" value=\"" + modelt.remark + "\" />\n");
                    strTxt.Append("<div onclick=\"focus_img(this);\" class=\"img_box");
                    if (focus_photo == modelt.small_img)
                    {
                        strTxt.Append(" current");
                    }
                    strTxt.Append("\">\n");
                    strTxt.Append("<img bigsrc=\"" + modelt.big_img + "\" src=\"" + modelt.small_img + "\" />");
                    strTxt.Append("<span class=\"remark\"><i>");
                    if (!string.IsNullOrEmpty(modelt.remark))
                    {
                        strTxt.Append(modelt.remark);
                    }
                    else
                    {
                        strTxt.Append("暫無描述...");
                    }
                    strTxt.Append("</i></span></div>\n");
                    strTxt.Append("<a onclick=\"show_remark(this);\" href=\"javascript:;\">描述</a><a onclick=\"del_img(this);\" href=\"javascript:;\">刪除</a>\n");
                    strTxt.Append("</li>\n");
                }
            }
            return strTxt.ToString();
        }
        #endregion

        #region 返回屬性清單HMTL=========================
        private string GetAttributeHtml(List<Model.attribute_value> models, int _channel_id, int _article_id)
        {
            StringBuilder strTxt = new StringBuilder();
            BLL.attributes bll = new BLL.attributes();
            DataSet ds = bll.GetList("channel_id=" + _channel_id);

            if (ds.Tables[0].Rows.Count > 0)
            {
                strTxt.Append("<tr><th>擴展屬性：</th><td>\n");
                strTxt.Append("<table border=\"0\" cellspacing=\"0\" cellpadding=\"0\" class=\"border_table\">\n");
                strTxt.Append(" <tbody><col width=\"80px\"><col>\n");
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    int _value_id = 0;
                    string _value_content = "";
                    if (models != null)
                    {
                        foreach (Model.attribute_value modelt in models)
                        {
                            if (modelt.attribute_id == Convert.ToInt32(dr["id"]) && modelt.article_id == _article_id)
                            {
                                _value_id = modelt.id;
                                _value_content = modelt.content;
                            }
                        }
                    }
                    strTxt.Append("<tr><th>" + dr["title"] + "</th><td>\n");
                    strTxt.Append(GetAttributeType(Convert.ToInt32(dr["id"]), dr["title"].ToString(), dr["default_value"].ToString(), Convert.ToInt32(dr["type"]),
                        _value_id, _value_content));
                    strTxt.Append("</td></tr>\n");
                }
                strTxt.Append("</tbody>\n");
                strTxt.Append("</table>\n");
                strTxt.Append("</td></tr>\n");
            }
            return strTxt.ToString();
        }
        #endregion

        #region 返回屬性類型=============================
        /// <summary>
        /// 返回屬性類型HTML
        /// </summary>
        /// <param name="_id">屬性ID</param>
        /// <param name="_title">屬性標題</param>
        /// <param name="_default_value">屬性預設值</param>
        /// <param name="_type">屬性類型</param>
        /// <param name="_value_id">屬性值ID</param>
        /// <param name="_value">屬性值內容</param>
        /// <returns>HTML代碼</returns>
        private string GetAttributeType(int _id, string _title, string _default_value, int _type, int _value_id, string _value)
        {
            //分解預設值
            string[] valueArr = _default_value.Split(',');
            StringBuilder str = new StringBuilder();
            str.Append("<input type=\"hidden\" name=\"value_" + _id + "\" value=\"" + _value_id + "\"/>\n");
            switch (_type)
            {
                case (int)DTEnums.AttributeEnum.Text:
                    if (_value_id > 0)
                        _default_value = _value;
                    str.Append("<input type=\"text\" name=\"content_" + _id + "\" value=\"" + _default_value + "\" class=\"txtInput middle\" />\n");
                    break;
                case (int)DTEnums.AttributeEnum.Select:
                    str.Append("<select name=\"content_" + _id + "\" class=\"select2\">\n");
                    for (int i = 0; i < valueArr.Length; i++)
                    {
                        str.Append("<option value=\"" + valueArr[i] + "\"");
                        if (_value_id > 0 && _value == valueArr[i])
                            str.Append(" selected");
                        str.Append(">" + valueArr[i] + "</option>\n");
                    }
                    str.Append("</select>\n");
                    break;
                case (int)DTEnums.AttributeEnum.Radio:
                    for (int i = 0; i < valueArr.Length; i++)
                    {
                        str.Append("<label class=\"attr\"><input type=\"radio\" name=\"content_" + _id + "\" value=\"" + valueArr[i] + "\"");
                        if (_value_id > 0 && _value == valueArr[i])
                            str.Append(" checked");
                        str.Append("  />" + valueArr[i] + "</label>\n");
                    }
                    break;
                case (int)DTEnums.AttributeEnum.CheckBox:
                    for (int i = 0; i < valueArr.Length; i++)
                    {
                        str.Append("<label class=\"attr\"><input type=\"checkbox\" name=\"content_" + _id + "\" value=\"" + valueArr[i] + "\"");
                        if (_value_id > 0 && !string.IsNullOrEmpty(_value))
                        {
                            string[] _valueArr = _value.Split(',');
                            for (int j = 0; j < _valueArr.Length; j++)
                            {
                                if (valueArr[i] == _valueArr[j])
                                    str.Append(" checked");
                            }
                        }
                        str.Append(" />" + valueArr[i] + "</label>\n");
                    }
                    break;
            }
            return str.ToString();
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.article_goods model = new Model.article_goods();
            BLL.article bll = new BLL.article();

            model.channel_id = this.channel_id;
            model.title = txtTitle.Text.Trim();
            model.category_id = Utils.StringToNum(ddlCategoryId.SelectedValue);
            //model.goods_no = txtGoodsNo.Text;
            //model.stock_quantity = Utils.StringToNum(txtStockQuantity.Text);
            //model.market_price = decimal.Parse(txtMarketPrice.Text);
            //model.sell_price = decimal.Parse(txtSellPrice.Text);
            // model.point = Utils.StringToNum(txtPoint.Text);
            model.link_url = txtLinkUrl.Text.Trim();
            model.img_url = focus_photo.Value;
            //model.content = txtContent.Value;
            //model.seo_title = txtSeoTitle.Text.Trim();
            //model.seo_keywords = txtSeoKeywords.Text.Trim();
            //model.seo_description = txtSeoDescription.Text.Trim();
            //model.sort_id = Utils.StringToNum(txtSortId.Text.Trim());
            //model.click = Utils.StringToNum(txtClick.Text.Trim());
            //model.digg_good = Utils.StringToNum(txtDiggGood.Text.Trim());
            //model.digg_bad = Utils.StringToNum(txtDiggBad.Text.Trim());
            model.add_time = DateTime.Parse(txtTime.Text.ToString());
            model.is_msg = 0;
            model.is_top = 0;
            model.is_red = 0;
            model.is_hot = 0;
            model.is_slide = 0;
            model.is_lock = 0;
            //model.lianxiren = txtuser.Text;
            // model.shangpinType = txtshangpintype.Text;
            //if (cblItem.Items[0].Selected == true)
            //{
            //    model.is_msg = 1;
            //}
            //if (cblItem.Items[1].Selected == true)
            //{
            //    model.is_top = 1;
            //}
            //if (cblItem.Items[2].Selected == true)
            //{
            //    model.is_red = 1;
            //}
            //if (cblItem.Items[3].Selected == true)
            //{
            //    model.is_hot = 1;
            //}
            //if (cblItem.Items[4].Selected == true)
            //{
            //    model.is_slide = 1;
            //}
            //if (cblItem.Items[5].Selected == true)
            //{
            //    model.is_lock = 1;
            //}
            //會員組價格
            List<Model.goods_group_price> priceList = new List<Model.goods_group_price>();
            //for (int i = 0; i < rptPrice.Items.Count; i++)
            //{
            //    int _groupid = Convert.ToInt32(((HiddenField)rptPrice.Items[i].FindControl("hideGroupId")).Value);
            //    decimal _price = Convert.ToDecimal(((TextBox)rptPrice.Items[i].FindControl("txtGroupPrice")).Text.Trim());
            //    priceList.Add(new Model.goods_group_price { group_id = _groupid, price = _price });
            //}
            model.goods_group_prices = priceList;
            //儲存相冊
            string[] albumArr = Request.Form.GetValues("hide_photo_name");
            string[] remarkArr = Request.Form.GetValues("hide_photo_remark");
            if (albumArr != null && albumArr.Length > 0)
            {
                List<Model.article_albums> ls = new List<Model.article_albums>();
                for (int i = 0; i < albumArr.Length; i++)
                {
                    string[] imgArr = albumArr[i].Split('|');
                    if (imgArr.Length == 3)
                    {
                        if (!string.IsNullOrEmpty(remarkArr[i]))
                        {
                            ls.Add(new Model.article_albums { big_img = imgArr[1], small_img = imgArr[2], remark = remarkArr[i] });
                        }
                        else
                        {
                            ls.Add(new Model.article_albums { big_img = imgArr[1], small_img = imgArr[2] });
                        }
                    }
                }
                model.albums = ls;
            }

            //擴展屬性
            BLL.attributes bll2 = new BLL.attributes();
            DataSet ds2 = bll2.GetList("channel_id=" + this.channel_id);

            List<Model.attribute_value> attrls = new List<Model.attribute_value>();
            foreach (DataRow dr in ds2.Tables[0].Rows)
            {
                int attr_id = Utils.StringToNum(dr["id"].ToString());
                string attr_title = dr["title"].ToString();
                string attr_value = Request.Form["content_" + attr_id];
                if (!string.IsNullOrEmpty(attr_value))
                {
                    attrls.Add(new Model.attribute_value { attribute_id = attr_id, title = attr_title, content = attr_value });
                }
            }
            model.attribute_values = attrls;

            // model.Postid = CheckBoxList1.SelectedValue;
            // model.Type = Utils.StringToNum(cblItem.SelectedValue);
            model.AddType = 0;
            model.Status = 1;

            //if (!string.IsNullOrEmpty(ddlquyu.SelectedValue))
            //{
            //    model.quyu = Convert.ToInt32(ddlquyu.SelectedValue);
            //}
            //if (!string.IsNullOrEmpty(ddljiaqian.SelectedValue))
            //{
            //    model.jiaqianQJ = Convert.ToInt32(ddljiaqian.SelectedValue);
            //}
            //if (!string.IsNullOrEmpty(ddlmianji.SelectedValue))
            //{
            //    model.mianji = Convert.ToInt32(ddlmianji.SelectedValue);
            //}
            //if (!string.IsNullOrEmpty(ddlhuxing.SelectedValue))
            //{
            //    model.huxing = Convert.ToInt32(ddlhuxing.SelectedValue);
            //}
            //if (!string.IsNullOrEmpty(ddlfangshi.SelectedValue))
            //{
            //    model.fangshi = Convert.ToInt32(ddlfangshi.SelectedValue);
            //}
            //if (!string.IsNullOrEmpty(ddlditie.SelectedValue))
            //{
            //    model.xianlu = Convert.ToInt32(ddlditie.SelectedValue);
            //}

            // model.yajin = string.IsNullOrEmpty(txtyajin.Text) ? 0 : Convert.ToInt32(txtyajin.Text);
            // model.zuoxiang = txtzuoxiang.Text;
            // model.louceng = txtlouceng.Text;
            //model.xingneng = txtxingneng.Text;
            // model.yongtu = txtyongtu.Text;
            // model.chewei = txtchewei.Text;
            // model.shequ = txtshequ.Text;
            model.dizhi = txtdizhi.Text;
            //model.gongsi = txtgongsi.Text;
            model.fuwuxiangju = txtfuwxiangmu.Text;
          model.dianhua = txtdianhua.Text;
            //model.Areaid = Convert.ToInt32(ddlAreaid.SelectedValue);
            //if (!bll.Update(model))
            //{
            //    result = false;
            //}


            if (bll.Add(model) < 1)
            {
                result = false;
            }
            return result;
        }
        #endregion

        #region 修改操作=================================
        private bool DoEdit(int _id)
        {
            bool result = true;
            BLL.article bll = new BLL.article();
            Model.article_goods model = bll.GetGoodsModel(_id);

            model.channel_id = this.channel_id;
            model.title = txtTitle.Text.Trim();
            model.category_id = Utils.StringToNum(ddlCategoryId.SelectedValue);
            //model.goods_no = txtGoodsNo.Text;
            //model.stock_quantity = Utils.StringToNum(txtStockQuantity.Text);
            //model.market_price = decimal.Parse(txtMarketPrice.Text);
            //model.sell_price = decimal.Parse(txtSellPrice.Text);
            // model.point = Utils.StringToNum(txtPoint.Text);
            model.link_url = txtLinkUrl.Text.Trim();
            model.img_url = focus_photo.Value;
            //model.content = txtContent.Value;
            //model.seo_title = txtSeoTitle.Text.Trim();
            //model.seo_keywords = txtSeoKeywords.Text.Trim();
            //model.seo_description = txtSeoDescription.Text.Trim();
            //model.sort_id = Utils.StringToNum(txtSortId.Text.Trim());
            //model.click = Utils.StringToNum(txtClick.Text.Trim());
            //model.digg_good = Utils.StringToNum(txtDiggGood.Text.Trim());
            //model.digg_bad = Utils.StringToNum(txtDiggBad.Text.Trim());
            model.add_time = DateTime.Parse(txtTime.Text.ToString());
            model.is_msg = 0;
            model.is_top = 0;
            model.is_red = 0;
            model.is_hot = 0;
            model.is_slide = 0;
            model.is_lock = 0;
            //model.lianxiren = txtuser.Text;
            //model.shangpinType = txtshangpintype.Text;
            //if (cblItem.Items[0].Selected == true)
            //{
            //    model.is_msg = 1;
            //}
            //if (cblItem.Items[1].Selected == true)
            //{
            //    model.is_top = 1;
            //}
            //if (cblItem.Items[2].Selected == true)
            //{
            //    model.is_red = 1;
            //}
            //if (cblItem.Items[3].Selected == true)
            //{
            //    model.is_hot = 1;
            //}
            //if (cblItem.Items[4].Selected == true)
            //{
            //    model.is_slide = 1;
            //}
            //if (cblItem.Items[5].Selected == true)
            //{
            //    model.is_lock = 1;
            //}
            //用戶組價格
            List<Model.goods_group_price> priceList = new List<Model.goods_group_price>();
            //for (int i = 0; i < rptPrice.Items.Count; i++)
            //{
            //    int hidPriceId = 0;
            //    if (!string.IsNullOrEmpty(((HiddenField)rptPrice.Items[i].FindControl("hidePriceId")).Value))
            //    {
            //        hidPriceId = Convert.ToInt32(((HiddenField)rptPrice.Items[i].FindControl("hidePriceId")).Value);
            //    }
            //    int hidGroupId = Convert.ToInt32(((HiddenField)rptPrice.Items[i].FindControl("hideGroupId")).Value);
            //    decimal _price = Convert.ToDecimal(((TextBox)rptPrice.Items[i].FindControl("txtGroupPrice")).Text.Trim());
            //    priceList.Add(new Model.goods_group_price { id = hidPriceId, article_id = _id, group_id = hidGroupId, price = _price });
            //}
            //model.goods_group_prices = priceList;
            //儲存相冊
            if (model.albums != null)
                model.albums.Clear();
            string[] albumArr = Request.Form.GetValues("hide_photo_name");
            string[] remarkArr = Request.Form.GetValues("hide_photo_remark");
            if (albumArr != null)
            {
                List<Model.article_albums> ls = new List<Model.article_albums>();
                for (int i = 0; i < albumArr.Length; i++)
                {
                    string[] imgArr = albumArr[i].Split('|');
                    int img_id = Utils.StringToNum(imgArr[0]);
                    if (imgArr.Length == 3)
                    {
                        if (!string.IsNullOrEmpty(remarkArr[i]))
                        {
                            ls.Add(new Model.article_albums { id = img_id, article_id = _id, big_img = imgArr[1], small_img = imgArr[2], remark = remarkArr[i] });
                        }
                        else
                        {
                            ls.Add(new Model.article_albums { id = img_id, article_id = _id, big_img = imgArr[1], small_img = imgArr[2] });
                        }
                    }
                }
                model.albums = ls;
            }

            //擴展屬性
            BLL.attributes bll2 = new BLL.attributes();
            DataSet ds2 = bll2.GetList("channel_id=" + this.channel_id);

            List<Model.attribute_value> attrls = new List<Model.attribute_value>();
            foreach (DataRow dr in ds2.Tables[0].Rows)
            {
                int attr_id = Utils.StringToNum(dr["id"].ToString());
                string attr_title = dr["title"].ToString();
                string attr_value_id = Request.Form["value_" + attr_id];
                string attr_value_content = Request.Form["content_" + attr_id];
                if (!string.IsNullOrEmpty(attr_value_id) && !string.IsNullOrEmpty(attr_value_content))
                {
                    attrls.Add(new Model.attribute_value { id = Convert.ToInt32(attr_value_id), article_id = _id, attribute_id = attr_id, title = attr_title, content = attr_value_content });
                }
            }
            model.attribute_values = attrls;



            //model.Postid = CheckBoxList1.SelectedValue;
            //model.Type = Utils.StringToNum(cblItem.SelectedValue);

            //if (!string.IsNullOrEmpty(ddlquyu.SelectedValue))
            //{
            //    model.quyu = Convert.ToInt32(ddlquyu.SelectedValue);
            //}
            //if (!string.IsNullOrEmpty(ddljiaqian.SelectedValue))
            //{
            //    model.jiaqianQJ = Convert.ToInt32(ddljiaqian.SelectedValue);
            //}
            //if (!string.IsNullOrEmpty(ddlmianji.SelectedValue))
            //{
            //    model.mianji = Convert.ToInt32(ddlmianji.SelectedValue);
            //}
            //if (!string.IsNullOrEmpty(ddlhuxing.SelectedValue))
            //{
            //    model.huxing = Convert.ToInt32(ddlhuxing.SelectedValue);
            //}
            //if (!string.IsNullOrEmpty(ddlfangshi.SelectedValue))
            //{
            //    model.fangshi = Convert.ToInt32(ddlfangshi.SelectedValue);
            //}
            //if (!string.IsNullOrEmpty(ddlditie.SelectedValue))
            //{
            //    model.xianlu = Convert.ToInt32(ddlditie.SelectedValue);
            //}

            //model.yajin = string.IsNullOrEmpty(txtyajin.Text) ? 0 : Convert.ToInt32(txtyajin.Text);
            //model.zuoxiang = txtzuoxiang.Text;
            //model.louceng = txtlouceng.Text;
            //model.xingneng = txtxingneng.Text;
            //model.yongtu = txtyongtu.Text;
            //model.chewei = txtchewei.Text;
            //model.shequ = txtshequ.Text;
            model.dizhi = txtdizhi.Text;
            //model.gongsi = txtgongsi.Text;
             model.fuwuxiangju = txtfuwxiangmu.Text;
            model.dianhua = txtdianhua.Text;

            //model.Areaid = Convert.ToInt32(ddlAreaid.SelectedValue);

            //model.Status = xiajiacheck.Checked ? 2 : model.Status;
            //model.xiajialiyou = xiajiatext.Value;
            if (!bll.Update(model))
            {
                result = false;
            }
            return result;
        }
        #endregion

        //儲存
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
            {
                ChkAdminLevel(channel_id, DTEnums.ActionEnum.Edit.ToString()); //檢查許可權
                if (!DoEdit(this.id))
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("修改商品成功！", "list.aspx?channel_id=" + this.channel_id, "Success");
            }
            else //添加
            {
                ChkAdminLevel(channel_id, DTEnums.ActionEnum.Add.ToString()); //檢查許可權
                if (!DoAdd())
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("添加商品成功！", "list.aspx?channel_id=" + this.channel_id, "Success");
            }
        }
    }
}
