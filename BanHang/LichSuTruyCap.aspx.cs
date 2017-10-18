using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class LichSuTruyCap : System.Web.UI.Page
    {
        dtLichSuTruyCap data = new dtLichSuTruyCap();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["KTDangNhap"] != "GPM")
            {
                Response.Redirect("DangNhap.aspx");
            }
            else
            {
                LoadGrid();
            }
        }

        private void LoadGrid()
        {
            data = new dtLichSuTruyCap();
            gridLichSuTruyCap.DataSource = data.LayDanhSach();
            gridLichSuTruyCap.DataBind();
        }
    }
}