using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DTcms.Web.Usercontrol
{
    public partial class horse : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            RptBind(" Typeid=12");
        }

        #region 數據綁定
        private void RptBind(string _strWhere)
        {
            int totalCount;
            DAL.imagedal aredal = new DAL.imagedal();
            this.rptList.DataSource = aredal.GetDatalistpage(10000, 1, _strWhere, " sort", out totalCount);
            this.rptList.DataBind();
        }
        #endregion
    }
}