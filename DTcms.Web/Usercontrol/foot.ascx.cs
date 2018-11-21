using DTcms.Common;
using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web.Usercontrol
{
    public partial class foot : System.Web.UI.UserControl
    {
        public string HostUrl = string.Empty;
        protected int TodayCount, TotalCount;
        protected void Page_Load(object sender, EventArgs e)
        {
            HostUrl = "http://" + Utils.GetHomeUrl();
            LoadAccess();
        }


        private void LoadAccess()
        {
            BLL.ipAccess bll = new BLL.ipAccess();

            TotalCount = bll.GetAllCount();
            TodayCount = bll.GetTodayCount();
        }
    }
}