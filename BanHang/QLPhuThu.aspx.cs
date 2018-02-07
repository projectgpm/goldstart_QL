using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class QLPhuThu : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            LoadGrid();

        }
        public void LoadGrid()
        {
            gridQLPhuThu.DataSource = dtSetting.getPhuThu();
            gridQLPhuThu.DataBind();
        }

        protected void gridQLPhuThu_RowUpdating(object sender, DevExpress.Web.Data.ASPxDataUpdatingEventArgs e)
        {
            string ID = e.Keys["ID"].ToString();
            string phuThu = e.NewValues["PhuThuQL"].ToString();
            dtSetting.CapNhatPhuThu(phuThu);
            e.Cancel = true;
            gridQLPhuThu.CancelEdit();
            LoadGrid();
        }
    }
}