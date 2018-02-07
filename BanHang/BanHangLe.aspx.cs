using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.Web;
using BanHang.Data;
using System.Data;

namespace BanHang
{
    public partial class BanHangLe1 : System.Web.UI.Page
    {
        public List<HoaDon> DanhSachHoaDon
        {
            get
            {
                if (ViewState["DanhSachHoaDon"] == null)
                    return new List<HoaDon>();
                else
                    return (List<HoaDon>)ViewState["DanhSachHoaDon"];
            }
            set
            {
                ViewState["DanhSachHoaDon"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            dtBanHangLe dt = new dtBanHangLe();
            ASPxGridViewInBuil.DataSource = dt.LayThongHoaDon();
            ASPxGridViewInBuil.DataBind();
            txtBarcode.Focus();
            //if (Session["KTBanLe"] == "GPMBanLe")
            //{
            if (!IsPostBack)
            {
                DanhSachHoaDon = new List<HoaDon>();
                ThemHoaDonMoi();

                string IDNhanVien = "1"; // Session["IDThuNgan"].ToString();
                if (Session["IDThuNgan"] != null)
                    IDNhanVien = Session["IDThuNgan"].ToString();
                if (Session["IDNhanVien"] != null)
                    IDNhanVien = Session["IDNhanVien"].ToString();
                dtLichSuHeThong.ThemLichSuTruyCap(IDNhanVien,"Bans hàng", "Truy cập bán hàng");
                
            }
            //btnNhanVien.Text = Session["TenThuNgan"].ToString();
            //}
            //else
            //{
            //    Response.Redirect("DangNhap.aspx");
            //}
        }

        public void BindGridChiTietHoaDon()
        {
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            UpdateSTT(MaHoaDon);
            int phuthu = dtSetting.PhuThu();
            txtPhuThu.Value = (DanhSachHoaDon[MaHoaDon].TongTien * phuthu) / 100;
            DanhSachHoaDon[MaHoaDon].PhuThu = (DanhSachHoaDon[MaHoaDon].TongTien * phuthu) / 100;
            DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia + DanhSachHoaDon[MaHoaDon].PhuThu;

            gridChiTietHoaDon.DataSource = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon;
            gridChiTietHoaDon.DataBind();
            formLayoutThanhToan.DataSource = DanhSachHoaDon[MaHoaDon];
            formLayoutThanhToan.DataBind();

            // Kết ca
            string IDNhanVien = "1"; // Session["IDThuNgan"].ToString();
            if (Session["IDThuNgan"] != null)
                IDNhanVien = Session["IDThuNgan"].ToString();
            if (Session["IDNhanVien"] != null)
                IDNhanVien = Session["IDNhanVien"].ToString();

            dtBanHangLe dt = new dtBanHangLe();
            DataTable da = dt.LaySoTienKetCa(IDNhanVien);
            if (da.Rows.Count != 0)
            {
                txtKetCaGiamGia.Value = da.Rows[0]["GiamGia"].ToString();
                txtKetCaTongTien.Value = da.Rows[0]["KhachCanTra"].ToString();
            }
            gridKetCa.DataSource = dt.DanhSachKetCa();
            gridKetCa.DataBind();
        }

        public void ThemHoaDonMoi()
        {
            HoaDon hd = new HoaDon();
            DanhSachHoaDon.Add(hd);
            Tab tabHoaDonNew = new Tab();
            int SoHoaDon = DanhSachHoaDon.Count;
            tabHoaDonNew.Name = SoHoaDon.ToString();
            tabHoaDonNew.Text = "Hóa đơn " + SoHoaDon.ToString();
            tabHoaDonNew.Index = SoHoaDon - 1;
            tabControlSoHoaDon.Tabs.Add(tabHoaDonNew);
            tabControlSoHoaDon.ActiveTabIndex = SoHoaDon - 1;
            BindGridChiTietHoaDon();
        }
        public void HuyHoaDon()
        {
            txtTienThua.Text = "";
            txtKhachThanhToan.Text = "";
            int indexTabActive = tabControlSoHoaDon.ActiveTabIndex;
            DanhSachHoaDon.RemoveAt(indexTabActive);
            tabControlSoHoaDon.Tabs.RemoveAt(indexTabActive);
            for (int i = 0; i < tabControlSoHoaDon.Tabs.Count; i++)
            {
                tabControlSoHoaDon.Tabs[i].Text = "Hóa đơn " + (i + 1).ToString();
            }
            if (DanhSachHoaDon.Count == 0)
            {
                ThemHoaDonMoi();
            }
            else
            {
                BindGridChiTietHoaDon();
            }
        }
        public void ThemHangVaoChiTietHoaDon(DataTable tbThongTin)
        {
            dtHangHoa dt = new dtHangHoa();
            int MaHang = Int32.Parse(dt.LayIDHangHoa_MaHang(tbThongTin.Rows[0]["MaHang"].ToString()));
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.MaHang == MaHang);
            if (exitHang != null)
            {
                int SoLuong = exitHang.SoLuong + int.Parse(txtSoLuong.Text);
                float ThanhTienOld = exitHang.SoLuong * exitHang.DonGia;
                exitHang.SoLuong = SoLuong;
                exitHang.ThanhTien = SoLuong * exitHang.DonGia;
                DanhSachHoaDon[MaHoaDon].TongTien += exitHang.ThanhTien - ThanhTienOld;
            }
            else
            {
                ChiTietHoaDon cthd = new ChiTietHoaDon();
                cthd.STT = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Count + 1;
                cthd.MaHang = MaHang;
                cthd.TenHang = tbThongTin.Rows[0]["TenHangHoa"].ToString();
                cthd.SoLuong = int.Parse(txtSoLuong.Text);
                cthd.DonViTinh = tbThongTin.Rows[0]["TenDonViTinh"].ToString();
                cthd.DonGia = float.Parse(tbThongTin.Rows[0]["GiaBan1"].ToString());
                cthd.ThanhTien = int.Parse(txtSoLuong.Text) * float.Parse(tbThongTin.Rows[0]["GiaBan1"].ToString());

                DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Add(cthd);
                DanhSachHoaDon[MaHoaDon].TongTien += cthd.ThanhTien;
            }
            
        }

        protected void btnInsertHang_Click(object sender, EventArgs e)
        {
            try
            {
                dtBanHangLe dt = new dtBanHangLe();
                if (txtBarcode.Text.Trim() != "")
                {
                    DataTable tbThongTin;
                    if (txtBarcode.Value == null)
                    {
                        tbThongTin = dt.LayThongTinHangHoa(txtBarcode.Text.ToString());
                    }
                    else
                    {
                        tbThongTin = dt.LayThongTinHangHoa(txtBarcode.Value.ToString());
                        if(tbThongTin.Rows.Count == 0)
                            tbThongTin = dt.LayThongTinHangHoa2(txtBarcode.Value.ToString());
                    }

                    if (tbThongTin.Rows.Count > 0)
                    {
                        ThemHangVaoChiTietHoaDon(tbThongTin);                  
                    }
                    else
                        HienThiThongBao("Mã hàng không tồn tại!!");
                }
                txtBarcode.Focus();
                txtBarcode.Text = "";
                txtBarcode.Value = "";
                txtSoLuong.Text = "1";
            }
            catch (Exception ex)
            {
                HienThiThongBao("Error: " + ex);
            }

            //BatchUpdate();
            BindGridChiTietHoaDon();    
        }
        public void HienThiThongBao(string thongbao)
        {           
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + thongbao + "');", true);
        }
        protected void btnUpdateGridHang_Click(object sender, EventArgs e)
        {
            BatchUpdate();
            BindGridChiTietHoaDon();
        }

        private void BatchUpdate()
        {
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            for (int i = 0; i < gridChiTietHoaDon.VisibleRowCount; i++)
            {
                object SoLuong = gridChiTietHoaDon.GetRowValues(i, "SoLuong");
                ASPxSpinEdit spineditSoLuong = gridChiTietHoaDon.FindRowCellTemplateControl(i, (GridViewDataColumn)gridChiTietHoaDon.Columns["SoLuong"], "txtSoLuongChange") as ASPxSpinEdit;
                object SoLuongMoi = spineditSoLuong.Value;
                int STT = Convert.ToInt32(gridChiTietHoaDon.GetRowValues(i, "STT"));
                var exitHang = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.FirstOrDefault(item => item.STT == STT);
                int SoLuongOld = exitHang.SoLuong;
                float ThanhTienOld = exitHang.ThanhTien;
                exitHang.SoLuong = Convert.ToInt32(SoLuongMoi);
                exitHang.ThanhTien = Convert.ToInt32(SoLuongMoi) * exitHang.DonGia;
                DanhSachHoaDon[MaHoaDon].TongTien += exitHang.ThanhTien - ThanhTienOld;
                DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia + DanhSachHoaDon[MaHoaDon].PhuThu;
            }
        }

        protected void txtKhachThanhToan_TextChanged(object sender, EventArgs e)
        {
            txtTienThua.Text = "";
            float TienKhachThanhToan;
            bool isNumeric = float.TryParse(txtKhachThanhToan.Text, out TienKhachThanhToan);
            if (!isNumeric)
            {
                HienThiThongBao("Nhập không đúng số tiền !!"); return;
            }

            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            //DanhSachHoaDon[MaHoaDon].GiamGia = float.Parse(txtGiamGia.Value + "");
            DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia + DanhSachHoaDon[MaHoaDon].PhuThu;

            DanhSachHoaDon[MaHoaDon].TienThua = TienKhachThanhToan - DanhSachHoaDon[MaHoaDon].KhachCanTra;
            txtTienThua.Text = DanhSachHoaDon[MaHoaDon].TienThua.ToString();

        }
        protected void UpdateSTT(int MaHoaDon)
        {
            for (int i = 1; i <= DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Count; i++)
            {
                DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon[i - 1].STT = i;
            }
        }
        protected void BtnXoaHang_Click(object sender, EventArgs e)
        {
            try
            {
                int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
                int STT = Convert.ToInt32(((ASPxButton)sender).CommandArgument);
                var itemToRemove = DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Single(r => r.STT == STT);
                DanhSachHoaDon[MaHoaDon].TongTien = DanhSachHoaDon[MaHoaDon].TongTien - itemToRemove.ThanhTien;
                DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien;
                DanhSachHoaDon[MaHoaDon].ListChiTietHoaDon.Remove(itemToRemove);

                DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia + DanhSachHoaDon[MaHoaDon].PhuThu;

                BindGridChiTietHoaDon();
            }
            catch (Exception ex)
            {
                Response.Write(ex.ToString());
            }
        }

        protected void btnHuyHoaDon_Click(object sender, EventArgs e)
        {
            HuyHoaDon();
        }

        protected void btnThemHoaDon_Click(object sender, EventArgs e)
        {
            ThemHoaDonMoi();
        }

        protected void tabControlSoHoaDon_ActiveTabChanged(object source, TabControlEventArgs e)
        {
            BindGridChiTietHoaDon();
        }

        protected void btnThanhToan_Click(object sender, EventArgs e)
        {
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            float TienKhachThanhToan;
            bool isNumeric = float.TryParse(txtKhachThanhToan.Text, out TienKhachThanhToan);
            if (!isNumeric)
            {
                HienThiThongBao("Nhập không đúng số tiền !!"); return;
            }
            if (TienKhachThanhToan < DanhSachHoaDon[MaHoaDon].KhachCanTra)
            {
                HienThiThongBao("Thanh toán chưa đủ số tiền !!"); return;
            }

            DanhSachHoaDon[MaHoaDon].KhachThanhToan = TienKhachThanhToan;

            dtBanHangLe dt = new dtBanHangLe();
            string IDNhanVien = "1"; // Session["IDThuNgan"].ToString();
            if (Session["IDThuNgan"] != null)
                IDNhanVien = Session["IDThuNgan"].ToString();
            if (Session["IDNhanVien"] != null)
                IDNhanVien = Session["IDNhanVien"].ToString();

            string IDKhachHang = "1";

            string Diem = dtSetting.LayTienQuyDoiDiem();
            DanhSachHoaDon[MaHoaDon].SoDiemTang = (int)(DanhSachHoaDon[MaHoaDon].KhachCanTra / float.Parse(Diem));        

            if (ccbKhachHang.Value != null)
                IDKhachHang = ccbKhachHang.Value.ToString();
            object IDHoaDon = dt.InsertHoaDon(IDNhanVien, IDKhachHang, DanhSachHoaDon[MaHoaDon]);

            dtLichSuHeThong.ThemLichSuTruyCap(IDNhanVien, "Bán hàng", "Thanh toán hóa đơn: " + IDHoaDon);

            HuyHoaDon();
            ccbKhachHang.Text = "";
            //chitietbuilInLai.ContentUrl = "~/InHoaDonBanLe.aspx?IDHoaDon=" + IDHoaDon;
            //chitietbuilInLai.ShowOnPageLoad = true;
            string jsInHoaDon = "window.open(\"InHoaDonBanLe.aspx?IDHoaDon=" + IDHoaDon + "\", \"PrintingFrame\");";
            ClientScript.RegisterStartupScript(this.GetType(), "Print", jsInHoaDon, true);
        }

        protected void btnHuyKhachHang_Click(object sender, EventArgs e)
        {
            popupThemKhachHang.ShowOnPageLoad = false;
        }

        protected void ASPxButton1_Click(object sender, EventArgs e)
        {
            popupThemKhachHang.ShowOnPageLoad = true;
        }

        protected void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            if (cmbNhomKhachHang.Text != "" && txtTenKhachHang.Text != "")
            {
                int IDNhom = Int32.Parse(cmbNhomKhachHang.Value.ToString());
                string TenKH = txtTenKhachHang.Text;
                string SDT = txtSoDienThoai.Text == null ? "" : txtSoDienThoai.Text;
                string DC = txtDiaChi.Text == null ? "" : txtDiaChi.Text;
                dtKhachHang dtkh = new dtKhachHang();

                DateTime date = DateTime.Now;
                string sDate = date.ToString("MMddyyyy");
                int MaKh = 0;
                Random dr = new Random();
                while (MaKh == 0)
                {
                    int sR = dr.Next(10000, 99999);
                    int kt = dtkh.KiemTraMaKhachHang(sDate + sR);
                    if (kt == 0)
                        MaKh = sR;
                }

                object ID = dtkh.ThemKhachHang(IDNhom, MaKh + "", TenKH, DateTime.Now, "", DC, SDT, "");

                txtTenKhachHang.Text = "";
                cmbNhomKhachHang.Text = "";
                txtSoDienThoai.Text = "";
                txtDiaChi.Text = "";
                HienThiThongBao("Thêm khách hàng thành công !!"); return;
                popupThemKhachHang.ShowOnPageLoad = false;
            }
            else
            {
                HienThiThongBao("Vui lòng nhập thông tin đầy đủ !!"); return;
            }
        }

        //protected void ckbGiamGia_CheckedChanged(object sender, EventArgs e)
        //{
        //    if (ccbKhachHang.Value != null)
        //    {
        //        if (ckbGiamGia.Checked == true)
        //        {
        //            string idKH = ccbKhachHang.Value + "";
        //            dtKhachHang dt = new dtKhachHang();
        //            string Diem = dt.layDiemTichLuy(idKH);
        //            txtSoDiemHienCo.Text = "Bạn có: " + Diem + " điểm; 1 điểm bằng " + dtSetting.LayDiemQuyDoiTien() + " VND";
        //            txtGiamGia.Enabled = true;
        //            txtSoDiem.Enabled = true;
        //        }
        //        else
        //        {
        //            txtGiamGia.Enabled = false;
        //            txtSoDiem.Enabled = false;
        //            txtSoDiemHienCo.Value = "";
        //            txtSoDiem.Value = 0;

        //            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
        //            DanhSachHoaDon[MaHoaDon].SoDiemGiam = 0;
        //            DanhSachHoaDon[MaHoaDon].GiamGia = 0;

        //            DanhSachHoaDon[MaHoaDon].HinhThucGiamGia = "";
        //            DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
        //            BindGridChiTietHoaDon();
        //        }
        //    }
        //    else
        //    {
        //        ckbGiamGia.Checked = false;
        //        HienThiThongBao("Chua chọn khách hàng thanh toán. Không thể quy đổi giảm giá. !!"); return;
        //    }
        //}

        protected void txtSoDiem_TextChanged(object sender, EventArgs e)
        {
            //string idKH = ccbKhachHang.Value + "";
            //dtKhachHang dt = new dtKhachHang();
            //string Diem = dt.layDiemTichLuy(idKH);
            //string TienQuyDoi = dtSetting.LayDiemQuyDoiTien();
            //if (float.Parse(txtSoDiem.Value + "") <= float.Parse(Diem))
            //{
            //    txtGiamGia.Value = float.Parse(txtSoDiem.Value + "") * float.Parse(TienQuyDoi);

            //    int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            //    DanhSachHoaDon[MaHoaDon].SoDiemGiam = Int32.Parse(txtSoDiem.Value + "");
            //    DanhSachHoaDon[MaHoaDon].GiamGia = float.Parse(txtGiamGia.Value + "");

            //    DanhSachHoaDon[MaHoaDon].HinhThucGiamGia = "Quy đổi: " + DanhSachHoaDon[MaHoaDon].SoDiemGiam + " điểm thành " + DanhSachHoaDon[MaHoaDon].GiamGia + " VND";
            //    DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
            //}
            //else
            //{
            //    txtSoDiem.Value = Int32.Parse(Diem);
            //    txtGiamGia.Value = Int32.Parse(txtSoDiem.Value + "") * float.Parse(TienQuyDoi);

            //    int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            //    DanhSachHoaDon[MaHoaDon].SoDiemGiam = Int32.Parse(txtSoDiem.Value + "");
            //    DanhSachHoaDon[MaHoaDon].GiamGia = float.Parse(txtGiamGia.Value + "");

            //    DanhSachHoaDon[MaHoaDon].HinhThucGiamGia = "Quy đổi: " + DanhSachHoaDon[MaHoaDon].SoDiemGiam + " điểm thành " + DanhSachHoaDon[MaHoaDon].GiamGia + " VND";
            //    DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;

            //    HienThiThongBao("Số điểm hiện có không đủ. !!"); return;
            //}
            //BindGridChiTietHoaDon();
        }

        protected void ccbKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {

            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;

            DanhSachHoaDon[MaHoaDon].SoDiemTang = 0;
            DanhSachHoaDon[MaHoaDon].SoDiemGiam = 0;
            DanhSachHoaDon[MaHoaDon].HinhThucGiamGia = cmbGiamGia.Text;
            DanhSachHoaDon[MaHoaDon].Giam = 0;
            DanhSachHoaDon[MaHoaDon].GiamGia = 0;
            DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia + DanhSachHoaDon[MaHoaDon].PhuThu;
            BindGridChiTietHoaDon();

            //if (ccbKhachHang.Value != null && ccbKhachHang.Value.ToString().CompareTo("1") != 0)
            //{
            //    string idKH = ccbKhachHang.Value + "";
            //    dtKhachHang dt = new dtKhachHang();
            //    string Diem = dt.layDiemTichLuy(idKH);
            //    //txtSoDiemHienCo.Text = "Bạn có: " + Diem + " điểm; 1 điểm bằng " + dtSetting.LayDiemQuyDoiTien() + " VND";
            //    txtGiamGia.Enabled = true;
            //    //txtSoDiem.Enabled = true;
            //    txtKhachThanhToan.Value = 0;
            //    txtTienThua.Value = 0;
            //}
            //else
            //{
            //    txtGiamGia.Enabled = false;
            //    //txtSoDiem.Enabled = false;
            //    //txtSoDiemHienCo.Value = "";
            //    //txtSoDiem.Value = 0;
            //    txtKhachThanhToan.Value = 0;
            //    txtTienThua.Value = 0;

            //    int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            //    DanhSachHoaDon[MaHoaDon].SoDiemGiam = 0;
            //    DanhSachHoaDon[MaHoaDon].GiamGia = 0;

            //    DanhSachHoaDon[MaHoaDon].HinhThucGiamGia = "";
            //    DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia;
            //    BindGridChiTietHoaDon();
            //}
        }

        protected void btnKetCa_Click(object sender, EventArgs e)
        {
            // Kết ca
            string IDNhanVien = "1"; // Session["IDThuNgan"].ToString();
            if (Session["IDThuNgan"] != null)
                IDNhanVien = Session["IDThuNgan"].ToString();
            if (Session["IDNhanVien"] != null)
                IDNhanVien = Session["IDNhanVien"].ToString();

            dtBanHangLe dt = new dtBanHangLe();
            DataTable da = dt.LaySoTienKetCa(IDNhanVien);
            DataTable da1 = dt.LayThoiGianKetCa(IDNhanVien);
            if (da.Rows.Count != 0 && da1.Rows.Count != 0)
            {
                dt.CapNhatKetCa(IDNhanVien, da1.Rows[0]["NgayBan"].ToString(), da1.Rows[da1.Rows.Count - 1]["NgayBan"].ToString(), (float.Parse(da.Rows[0]["GiamGia"].ToString()) + float.Parse(da.Rows[0]["KhachCanTra"].ToString())) + "", da.Rows[0]["GiamGia"].ToString(), da.Rows[0]["KhachCanTra"].ToString());
            }

            da = dt.LaySoTienKetCa(IDNhanVien);
            if (da.Rows.Count != 0)
            {
                txtKetCaGiamGia.Value = da.Rows[0]["GiamGia"].ToString();
                txtKetCaTongTien.Value = da.Rows[0]["KhachCanTra"].ToString();
            }

            gridKetCa.DataSource = dt.DanhSachKetCa();
            gridKetCa.DataBind();
        }

        protected void cmbGiamGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ccbKhachHang.Text != "" && Int32.Parse(ccbKhachHang.Value + "") == 1)
            {
                HienThiThongBao("Chọn khách hàng"); return;
                cmbGiamGia.Value = 1;
            }
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            DanhSachHoaDon[MaHoaDon].SoDiemTang = 0;
            DanhSachHoaDon[MaHoaDon].SoDiemGiam = 0;
            DanhSachHoaDon[MaHoaDon].HinhThucGiamGia = cmbGiamGia.Text;
            DanhSachHoaDon[MaHoaDon].Giam = 0;
            DanhSachHoaDon[MaHoaDon].GiamGia = 0;
            DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia + DanhSachHoaDon[MaHoaDon].PhuThu;
            BatchUpdate();
            BindGridChiTietHoaDon();
        }

        protected void txtNhapGiamGia_TextChanged(object sender, EventArgs e)
        {
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            float g = 0;
            if (Int32.Parse(cmbGiamGia.Value + "") == 3 && ccbKhachHang.Text != "")
            {
                // Điểm
                string idKH = ccbKhachHang.Value + "";
                dtKhachHang dt = new dtKhachHang();
                string Diem = dt.layDiemTichLuy(idKH);
                string TienQuyDoi = dtSetting.LayDiemQuyDoiTien();

                DanhSachHoaDon[MaHoaDon].Giam = Int32.Parse(txtNhapGiamGia.Value + "");
                DanhSachHoaDon[MaHoaDon].SoDiemGiam = Int32.Parse(txtNhapGiamGia.Value + "");

                if (DanhSachHoaDon[MaHoaDon].Giam <= float.Parse(Diem))
                {
                    txtGiamGia.Value = float.Parse(txtNhapGiamGia.Value + "") * float.Parse(TienQuyDoi);
                    DanhSachHoaDon[MaHoaDon].GiamGia = float.Parse(txtNhapGiamGia.Value + "") * float.Parse(TienQuyDoi);
                }
                else
                {
                    g = 0;
                    DanhSachHoaDon[MaHoaDon].Giam = 0;
                    HienThiThongBao("Số điểm hiện có không đủ. !!"); return;
                }

            }
            else if (Int32.Parse(cmbGiamGia.Value + "") == 2)
            {
                //Tiền
                DanhSachHoaDon[MaHoaDon].Giam = Int32.Parse(txtNhapGiamGia.Value + "");
                g = float.Parse(txtNhapGiamGia.Value + "");
            }
            else if (Int32.Parse(cmbGiamGia.Value + "") == 1)
            {
                // %
                DanhSachHoaDon[MaHoaDon].Giam = Int32.Parse(txtNhapGiamGia.Value + "");
                g = (DanhSachHoaDon[MaHoaDon].TongTien * DanhSachHoaDon[MaHoaDon].Giam) / 100;
            }

            DanhSachHoaDon[MaHoaDon].HinhThucGiamGia = cmbGiamGia.Text;
            DanhSachHoaDon[MaHoaDon].GiamGia = g;
            DanhSachHoaDon[MaHoaDon].PhuThu = float.Parse(txtPhuThu.Value + "");
            DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia + DanhSachHoaDon[MaHoaDon].PhuThu;
        }

        protected void txtPhuThu_TextChanged(object sender, EventArgs e)
        {
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            string tx = txtPhuThu.Value + "-" + txtPhuThu.Text;
            DanhSachHoaDon[MaHoaDon].PhuThu = float.Parse(txtPhuThu.Value + "");
            DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia + DanhSachHoaDon[MaHoaDon].PhuThu;
            BatchUpdate();
            BindGridChiTietHoaDon();
        }

        protected void txtTongTien_TextChanged(object sender, EventArgs e)
        {
            int phuthu = dtSetting.PhuThu();
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            txtPhuThu.Value = (float.Parse(txtTongTien.Value + "") * phuthu) / 100;
            DanhSachHoaDon[MaHoaDon].PhuThu = float.Parse(txtPhuThu.Value + "");
            DanhSachHoaDon[MaHoaDon].KhachCanTra = DanhSachHoaDon[MaHoaDon].TongTien - DanhSachHoaDon[MaHoaDon].GiamGia + DanhSachHoaDon[MaHoaDon].PhuThu;
        }
    }
    [Serializable]
    public class HoaDon
    {
        public int IDHoaDon { get; set; }
        public int IDKhachHang { get; set; }
        public int IDNhanVien { get; set; }
        public float TongTien { get; set; }
        public string HinhThucGiamGia { get; set; }
        public float Giam { get; set; }
        public int SoDiemGiam { get; set; }
        public int SoDiemTang { get; set; }
        public float GiamGia { get; set; }
        public float PhuThu { get; set; }
        public float KhachCanTra { get; set; }
        public float KhachThanhToan { get; set; }
        public float TienThua { get; set; }
        public List<ChiTietHoaDon> ListChiTietHoaDon { get; set; }
        public HoaDon()
        {
            IDKhachHang = 0;
            IDNhanVien = 0;
            TongTien = 0;
            GiamGia = 0;
            PhuThu = 0;
            HinhThucGiamGia = "";
            Giam = 0;
            SoDiemGiam = 0;
            SoDiemTang = 0;
            KhachCanTra = 0;
            KhachThanhToan = 0;
            TienThua = 0;
            ListChiTietHoaDon = new List<ChiTietHoaDon>();
        }
    }
    [Serializable]
    public class ChiTietHoaDon
    {
        public int STT { get; set; }
        public int MaHang { get; set; }
        public string TenHang { get; set; }
        public int MaDonViTinh { get; set; }
        public string DonViTinh { get; set; }
        public int SoLuong { get; set; }
        public float DonGia { get; set; }
        public float ThanhTien { get; set; }

        public ChiTietHoaDon()
        {
            SoLuong = 0;
            DonGia = 0;
            ThanhTien = 0;
        }
    }
}