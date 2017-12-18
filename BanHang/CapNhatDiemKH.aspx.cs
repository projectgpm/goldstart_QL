using BanHang.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class CapNhatDiemKH : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCapNhat_Click(object sender, EventArgs e)
        {
            dtKhachHang dt = new dtKhachHang();
            float soTien = dt.laySoTienQuyDoi();
            float soDiem = float.Parse(txtSoTien.Value.ToString()) / soTien;
            dt.CapNhatDiemTichLuy(cmbKhachHang.Value.ToString(), soDiem);
            txtSoTien.Value = 0;
            //ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert( Cập nhật thành công! );", true);
        }
    }
}