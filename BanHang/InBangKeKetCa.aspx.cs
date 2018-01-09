using BanHang.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class InBangKeKetCa : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string NgayBD = Request.QueryString["ngayBD"];
            string NgayKT = Request.QueryString["NgayKT"];
            string IDNhanVien = Request.QueryString["IDNhanVien"];

            string strNgay = DateTime.Parse(NgayBD).ToString("dd-MM-yyyy") + " - " + DateTime.Parse(NgayKT).ToString("dd-MM-yyyy");
            rpBangKeKetCa rp = new rpBangKeKetCa();
            rp.Parameters["strNgay"].Value = strNgay;
            rp.Parameters["strNgay"].Visible = false;
            rp.Parameters["NgayBD"].Value = NgayBD;
            rp.Parameters["NgayBD"].Visible = false;
            rp.Parameters["NgayKT"].Value = NgayKT;
            rp.Parameters["NgayKT"].Visible = false;
            rp.Parameters["IDNhanVien"].Value = IDNhanVien;
            rp.Parameters["IDNhanVien"].Visible = false;
            viewerReport.Report = rp;
        }
    }
}