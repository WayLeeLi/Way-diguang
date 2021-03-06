﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Common;
using DTcms.Common;
using System.Data;

namespace DTcms.Web
{
    public partial class upFile : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            BLL.users bllUser = new BLL.users();
            if (!WEBUserCurrent.IsLogin)
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('請先登入');window.location.href='login_vip.aspx'</script>");
            }
            else
            {

                string ids = WEBUserCurrent.UserID.ToString();
                if (!bllUser.isVip(Utils.StringToNum(ids)))
                {
                    this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('你不是VIP會員，無法訪問次頁');window.location.href='userinfo.aspx'</script>");
                }


            }
            if (!IsPostBack)
            {
                TreeBind(9);
            }
        }

        #region 綁定類別=================================
        private void TreeBind(int _channel_id)
        {
            BLL.category bll = new BLL.category();
            DataTable dt = bll.GetList(0, _channel_id);

            this.ddlCategoryId.Items.Clear();
            this.ddlCategoryId.Items.Add(new ListItem("請選擇類別...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                string Id = dr["id"].ToString();
                int ClassLayer = int.Parse(dr["class_layer"].ToString());
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
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            bool result = true;
            Model.article_download model = new Model.article_download();
            BLL.article bll = new BLL.article();

            model.channel_id = 9;
            model.title = txtTitle.Text.Trim();
            model.category_id = int.Parse(ddlCategoryId.SelectedValue);
            model.link_url = txtLinkUrl.Text.Trim();
            model.img_url = txtImgUrl.Text.Trim();
            model.content = txtContent.Value;
            model.seo_title = txtSeoTitle.Text.Trim();
            model.seo_keywords = txtSeoKeywords.Text.Trim();
            model.seo_description = txtSeoDescription.Text.Trim();
            model.sort_id = int.Parse(txtSortId.Text.Trim());
            model.click = int.Parse(txtClick.Text.Trim());
            model.digg_good = int.Parse(txtDiggGood.Text.Trim());
            model.digg_bad = int.Parse(txtDiggBad.Text.Trim());
            model.is_msg = 0;
            model.is_red = 0;
            model.is_lock = 0;
            if (cblItem.Items[0].Selected == true)
            {
                model.is_msg = 1;
            }
            if (cblItem.Items[1].Selected == true)
            {
                model.is_red = 1;
            }
            if (cblItem.Items[2].Selected == true)
            {
                model.is_lock = 1;
            }
            //儲存附件
            string hidFileList = Request.Params["hidFileName"];
            if (!string.IsNullOrEmpty(hidFileList))
            {
                string[] fileListArr = hidFileList.Split(',');
                string[] pointArr = Request.Form.GetValues("txtPoint");
                List<Model.download_attach> ls = new List<Model.download_attach>();
                for (int i = 0; i < fileListArr.Length; i++)
                {
                    string[] fileArr = fileListArr[i].Split('|');
                    if (fileArr.Length == 3)
                    {
                        int fileSize = Utils.GetFileSize(fileArr[2]);
                        string fileExt = Utils.GetFileExt(fileArr[2]);
                        int _point = int.Parse(pointArr[i]);
                        ls.Add(new Model.download_attach { id = int.Parse(fileArr[0]), title = fileArr[1], file_path = fileArr[2], file_size = fileSize, file_ext = fileExt, point = _point });
                    }
                }
                model.download_attachs = ls;
            }

            if (bll.Add(model) < 1)
            {
                result = false;
            }
            if (!result)
            {
                this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('儲存過程中發生錯誤');window.location.href='upFile.aspx'</script>");
                return;
            }
            this.Page.ClientScript.RegisterStartupScript(GetType(), "", "<script>alert('添加成功');window.location.href='upFile.aspx'</script>");
        }

       
    }
}