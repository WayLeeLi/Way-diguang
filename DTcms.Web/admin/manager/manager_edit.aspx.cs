﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DTcms.Common;

namespace DTcms.Web.admin.manager
{
    public partial class manager_edit : DTcms.Web.UI.ManagePage
    {
        string defaultpassword = "0|0|0|0"; //預設顯示密碼
        private string action = DTEnums.ActionEnum.Add.ToString(); //操作類型
        private int id = 0;

        protected void Page_Load(object sender, EventArgs e)
        {
            string _action = DTRequest.GetQueryString("action");
            if (!string.IsNullOrEmpty(_action) && _action == DTEnums.ActionEnum.Edit.ToString())
            {
                this.action = DTEnums.ActionEnum.Edit.ToString();//修改類型
                if (!int.TryParse(Request.QueryString["id"] as string, out this.id))
                {
                    JscriptMsg("傳輸參數不正確！", "back", "Error");
                    return;
                }
                if (!new BLL.manager().Exists(this.id))
                {
                    JscriptMsg("資料不存在或已被刪除！", "back", "Error");
                    return;
                }
            }
            if (!Page.IsPostBack)
            {
                //取得管理員資訊
                Model.manager model = GetAdminInfo();
                TreeBind(ddlRoleId, model.role_type);
                if (action == DTEnums.ActionEnum.Edit.ToString()) //修改
                {
                    ShowInfo(this.id);
                }
            }
        }

        #region 賦值操作=================================
        private void ShowInfo(int _id)
        {
            BLL.manager bll = new BLL.manager();
            Model.manager model = bll.GetModel(_id);
            ddlRoleId.SelectedValue = model.role_id.ToString();
            rblIsLock.SelectedValue = model.is_lock.ToString();
            txtUserName.Text = model.user_name;
            txtUserName.ReadOnly = true;
            if (!string.IsNullOrEmpty(model.user_pwd))
            {
                txtUserPwd.Attributes["value"] = txtUserPwd1.Attributes["value"] = defaultpassword;
            }
            txtRealName.Text = model.real_name;
            txtTelephone.Text = model.telephone;
            txtEmail.Text = model.email;
        }
        #endregion

        #region 綁定模型=================================
        private void TreeBind(DropDownList ddl, int role_type)
        {
            BLL.manager_role bll = new BLL.manager_role();
            DataTable dt = bll.GetList("").Tables[0];

            ddl.Items.Clear();
            ddl.Items.Add(new ListItem("請選擇角色...", ""));
            foreach (DataRow dr in dt.Rows)
            {
                if (Convert.ToInt32(dr["role_type"]) >= role_type)
                {
                    ddl.Items.Add(new ListItem(dr["role_name"].ToString(), dr["id"].ToString()));
                }
            }
        }
        #endregion

        #region 增加操作=================================
        private bool DoAdd()
        {
            bool result = true;
            Model.manager model = new Model.manager();
            BLL.manager bll = new BLL.manager();
            model.role_id = int.Parse(ddlRoleId.SelectedValue);
            model.role_type = new BLL.manager_role().GetModel(model.role_id).role_type;
            model.is_lock = int.Parse(rblIsLock.SelectedValue);
            model.user_name = txtUserName.Text.Trim();
            model.user_pwd = DESEncrypt.Encrypt(txtUserPwd.Text.Trim());
            model.real_name = txtRealName.Text.Trim();
            model.telephone = txtTelephone.Text.Trim();
            model.email = txtEmail.Text.Trim();
            model.add_time = DateTime.Now;
            
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
            BLL.manager bll = new BLL.manager();
            Model.manager model = bll.GetModel(_id);

            model.role_id = int.Parse(ddlRoleId.SelectedValue);
            model.role_type = new BLL.manager_role().GetModel(model.role_id).role_type;
            model.is_lock = int.Parse(rblIsLock.SelectedValue);
            //判斷密碼是否更改
            if (txtUserPwd.Text.Trim() != defaultpassword)
            {
                model.user_pwd = DESEncrypt.Encrypt(txtUserPwd.Text.Trim());
            }
            model.real_name = txtRealName.Text.Trim();
            model.telephone = txtTelephone.Text.Trim();
            model.email = txtEmail.Text.Trim();

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
                ChkAdminLevel("sys_manager", DTEnums.ActionEnum.Edit.ToString()); //檢查許可權
                if (!DoEdit(this.id))
                {
                    JscriptMsg("儲存過程中發生錯誤啦！", "", "Error");
                    return;
                }
                JscriptMsg("修改管理員成功！", "manager_list.aspx", "Success");
            }
            else //添加
            {
                ChkAdminLevel("sys_manager", DTEnums.ActionEnum.Add.ToString()); //檢查許可權
                if (!DoAdd())
                {
                    JscriptMsg("儲存過程中發生錯誤！", "", "Error");
                    return;
                }
                JscriptMsg("添加管理員成功！", "manager_list.aspx", "Success");
            }
        }

    }
}
