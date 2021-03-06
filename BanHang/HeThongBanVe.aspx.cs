﻿using BanHang.Data;
using DevExpress.Web;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BanHang
{
    public partial class HeThongBanVe : System.Web.UI.Page
    {
        
       public List<HoaDonBanVe> DanhSachBanVe
        {
            get
            {
                if (ViewState["DanhSachBanVe"] == null)
                    return new List<HoaDonBanVe>();
                else
                    return (List<HoaDonBanVe>)ViewState["DanhSachBanVe"];
            }
            set
            {
                ViewState["DanhSachBanVe"] = value;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //dtBanVe dt = new dtBanVe();   
            //ASPxGridViewInBuil.DataSource = dt.LayThongHoaDon();
            //ASPxGridViewInBuil.DataBind();

            if (Session["KTBanVe"] == "GPMBanVe")
            {
                if (!IsPostBack)
                {
                    DanhSachBanVe = new List<HoaDonBanVe>();
                    cmbKyHieu.Focus();
                    ThemHoaDonMoi();
                    btnNhanVien.Text = Session["TenThuNgan"].ToString();

                    string IDNhanVien = "1"; // Session["IDThuNgan"].ToString();
                    if (Session["IDThuNgan"] != null)
                        IDNhanVien = Session["IDThuNgan"].ToString();
                    if (Session["IDNhanVien"] != null)
                        IDNhanVien = Session["IDNhanVien"].ToString();
                    dtLichSuHeThong.ThemLichSuTruyCap(IDNhanVien, "Bán vé", "Truy cập bán vé");
                }
            }
            else
            {
                Response.Redirect("DangNhap.aspx");
            }

            // Kết ca
            string IDNhanVien1 = "1"; // Session["IDThuNgan"].ToString();
            if (Session["IDThuNgan"] != null)
                IDNhanVien1 = Session["IDThuNgan"].ToString();
            if (Session["IDNhanVien"] != null)
                IDNhanVien1 = Session["IDNhanVien"].ToString();
            dtBanVe dt = new dtBanVe();
            DataTable da1 = dt.LaySoTienKetCa(IDNhanVien1);
            if (da1.Rows.Count != 0)
            {
                txtKetCaGiamGia.Value = da1.Rows[0]["GiamGia"].ToString();
                txtKetCaTongTien.Value = da1.Rows[0]["KhachCanTra"].ToString();
            }
            gridKetCa.DataSource = dt.DanhSachKetCa();
            gridKetCa.DataBind();
        }

        public void BindGridChiTietHoaDon()
        {
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            dtBanVe dt = new dtBanVe();
            if (DanhSachBanVe[MaHoaDon].ListChiTietBanVe.Count == 0)
            {

                List<ChiTietBanVe> list = new List<ChiTietBanVe>();

                DataTable da = dt.DanhSachVe();
                for (int i = 0; i < da.Rows.Count; i++)
                    list.Add(new ChiTietBanVe(i, da.Rows[i]["TenKyHieu"].ToString(), 0, float.Parse(da.Rows[i]["GiaVe"].ToString()), 0));
                DanhSachBanVe[MaHoaDon].ListChiTietBanVe = list;

            }
            UpdateSTT(MaHoaDon);
            int phuthu = dtSetting.PhuThu();
            txtPhuThu.Value = (DanhSachBanVe[MaHoaDon].TongTien * phuthu) / 100;
            DanhSachBanVe[MaHoaDon].PhuThu = (DanhSachBanVe[MaHoaDon].TongTien * phuthu) / 100;
            DanhSachBanVe[MaHoaDon].KhachCanTra = DanhSachBanVe[MaHoaDon].TongTien - DanhSachBanVe[MaHoaDon].GiamGia + DanhSachBanVe[MaHoaDon].PhuThu;

            gridChiTietHoaDon.DataSource = DanhSachBanVe[MaHoaDon].ListChiTietBanVe;
            gridChiTietHoaDon.DataBind();
            formLayoutThanhToan.DataSource = DanhSachBanVe[MaHoaDon];
            formLayoutThanhToan.DataBind();

            // Kết ca
            string IDNhanVien = "1"; // Session["IDThuNgan"].ToString();
            if (Session["IDThuNgan"] != null)
                IDNhanVien = Session["IDThuNgan"].ToString();
            if (Session["IDNhanVien"] != null)
                IDNhanVien = Session["IDNhanVien"].ToString();

            DataTable da1 = dt.LaySoTienKetCa(IDNhanVien);
            if (da1.Rows.Count != 0)
            {
                txtKetCaGiamGia.Value = da1.Rows[0]["GiamGia"].ToString();
                txtKetCaTongTien.Value = da1.Rows[0]["KhachCanTra"].ToString();
            }
            gridKetCa.DataSource = dt.DanhSachKetCa();
            gridKetCa.DataBind();
        }
        protected void UpdateSTT(int MaHoaDon)
        {
            for (int i = 1; i <= DanhSachBanVe[MaHoaDon].ListChiTietBanVe.Count; i++)
            {
                DanhSachBanVe[MaHoaDon].ListChiTietBanVe[i - 1].STT = i;
            }
        }
        protected void btnKetCa_Click(object sender, EventArgs e)
        {
            // Kết ca
            string IDNhanVien = "1"; // Session["IDThuNgan"].ToString();
            if (Session["IDThuNgan"] != null)
                IDNhanVien = Session["IDThuNgan"].ToString();
            if (Session["IDNhanVien"] != null)
                IDNhanVien = Session["IDNhanVien"].ToString();

            dtBanVe dt = new dtBanVe();
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
        protected void btnInsertHang_Click(object sender, EventArgs e)
        {
            try
            {
                dtBanVe dt = new dtBanVe();
                if (cmbKyHieu.Text.Trim() != "")
                {
                    DataTable tbThongTin;
                    tbThongTin = dt.ThongTinKyHieu(cmbKyHieu.Value.ToString());
                    if (tbThongTin.Rows.Count > 0)
                    {
                        ThemHangVaoChiTietHoaDon(tbThongTin);
                        BindGridChiTietHoaDon();
                    }
                    else
                        HienThiThongBao("Mã vé không tồn tại!!");
                }

                cmbKyHieu.Text = "";
                cmbKyHieu.Focus();
                txtSoLuong.Text = "1";
            }
            catch (Exception ex)
            {
                cmbKyHieu.Text = "";
                cmbKyHieu.Focus();
                HienThiThongBao("Error: " + ex);
            }
        }
        public void ThemHoaDonMoi()
        {
            HoaDonBanVe hd = new HoaDonBanVe();
            DanhSachBanVe.Add(hd);
            Tab tabHoaDonNew = new Tab();
            int SoHoaDon = DanhSachBanVe.Count;
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
            //txtDiemTichLuy.Text = "0";
            int indexTabActive = tabControlSoHoaDon.ActiveTabIndex;
            DanhSachBanVe.RemoveAt(indexTabActive);
            tabControlSoHoaDon.Tabs.RemoveAt(indexTabActive);
            for (int i = 0; i < tabControlSoHoaDon.Tabs.Count; i++)
            {
                tabControlSoHoaDon.Tabs[i].Text = "Hóa đơn " + (i + 1).ToString();
            }
            if (DanhSachBanVe.Count == 0)
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
            //string TenKyHieu = tbThongTin.Rows[0]["TenKyHieu"].ToString();
            //float GiaVe = float.Parse(tbThongTin.Rows[0]["GiaVe"].ToString());
            //int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            //var exitHang = DanhSachBanVe[MaHoaDon].ListChiTietBanVe.FirstOrDefault(item => item.TenKyHieu == TenKyHieu);
            //if (exitHang != null)
            //{
            //    int SoLuong = exitHang.SoLuong + int.Parse(txtSoLuong.Text);
            //    float ThanhTienOld = exitHang.ThanhTien;
            //    exitHang.SoLuong = SoLuong;
            //    exitHang.ThanhTien = SoLuong * exitHang.DonGia;
            //    DanhSachBanVe[MaHoaDon].TongTien += SoLuong * exitHang.DonGia - ThanhTienOld;
            //    DanhSachBanVe[MaHoaDon].KhachCanTra = DanhSachBanVe[MaHoaDon].TongTien - DanhSachBanVe[MaHoaDon].GiamGia;
            //}
            //else
            //{
            //    ChiTietBanVe cthd = new ChiTietBanVe();
            //    cthd.STT = DanhSachBanVe[MaHoaDon].ListChiTietBanVe.Count + 1;
            //    cthd.TenKyHieu = TenKyHieu;
            //    cthd.SoLuong = int.Parse(txtSoLuong.Text);
            //    cthd.DonGia = GiaVe;
            //    cthd.ThanhTien = int.Parse(txtSoLuong.Text) * float.Parse(cthd.DonGia.ToString());
            //    DanhSachBanVe[MaHoaDon].ListChiTietBanVe.Add(cthd);
            //    DanhSachBanVe[MaHoaDon].SoLuongHang++;
            //    DanhSachBanVe[MaHoaDon].TongTien += cthd.ThanhTien;
            //    DanhSachBanVe[MaHoaDon].KhachCanTra = DanhSachBanVe[MaHoaDon].TongTien - DanhSachBanVe[MaHoaDon].GiamGia;
            //}
            //LamMoi();     
        }
        protected void btnUpdateGridHang_Click(object sender, EventArgs e)
        {
            BatchUpdate();
            BindGridChiTietHoaDon();
            //LamMoi();
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
                var exitHang = DanhSachBanVe[MaHoaDon].ListChiTietBanVe.FirstOrDefault(item => item.STT == STT);
                int SoLuongOld = exitHang.SoLuong;
                float ThanhTienOld = exitHang.ThanhTien;
                exitHang.SoLuong = Convert.ToInt32(SoLuongMoi);
                exitHang.ThanhTien = Convert.ToInt32(SoLuongMoi) * exitHang.DonGia;
                DanhSachBanVe[MaHoaDon].TongTien += exitHang.ThanhTien - ThanhTienOld;
                DanhSachBanVe[MaHoaDon].KhachCanTra = DanhSachBanVe[MaHoaDon].TongTien - DanhSachBanVe[MaHoaDon].GiamGia + DanhSachBanVe[MaHoaDon].PhuThu;
            }
            //LamMoi();
        }
        public void HienThiThongBao(string thongbao)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('" + thongbao + "');", true);
        }
        protected void txtKhachThanhToan_TextChanged(object sender, EventArgs e)
        {
            txtTienThua.Text = "";
            float TienKhachThanhToan;
            bool isNumeric = float.TryParse(txtKhachThanhToan.Text, out TienKhachThanhToan);
            if (!isNumeric)
            {
                txtKhachThanhToan.Text = "0";
                txtKhachThanhToan.Focus();
                HienThiThongBao("Nhập không đúng số tiền !!"); return;
            }
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            DanhSachBanVe[MaHoaDon].KhachThanhToan = TienKhachThanhToan;
            DanhSachBanVe[MaHoaDon].TienThua = TienKhachThanhToan - (DanhSachBanVe[MaHoaDon].TongTien - float.Parse(txtGiamGia.Text));
            txtTienThua.Text = DanhSachBanVe[MaHoaDon].TienThua.ToString();

        }

        protected void BtnXoaHang_Click(object sender, EventArgs e)
        {
            try
            {
                int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
                int STT = Convert.ToInt32(((ASPxButton)sender).CommandArgument);
                var itemToRemove = DanhSachBanVe[MaHoaDon].ListChiTietBanVe.Single(r => r.STT == STT);
                DanhSachBanVe[MaHoaDon].SoLuongHang--;
                DanhSachBanVe[MaHoaDon].TongTien = DanhSachBanVe[MaHoaDon].TongTien - itemToRemove.ThanhTien;
                DanhSachBanVe[MaHoaDon].KhachCanTra = DanhSachBanVe[MaHoaDon].TongTien - DanhSachBanVe[MaHoaDon].GiamGia + DanhSachBanVe[MaHoaDon].PhuThu;
                DanhSachBanVe[MaHoaDon].ListChiTietBanVe.Remove(itemToRemove);
                BindGridChiTietHoaDon();
                //LamMoi();
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
            if (DanhSachBanVe[MaHoaDon].ListChiTietBanVe.Count > 0)
            {
                float TienKhachThanhToan;
                bool isNumeric = float.TryParse(txtKhachThanhToan.Text, out TienKhachThanhToan);
                if (!isNumeric)
                {
                    HienThiThongBao("Nhập không đúng số tiền !!"); return;
                }
                if (TienKhachThanhToan < float.Parse(txtKhachCanTra.Text))
                {
                    HienThiThongBao("Thanh toán chưa đủ số tiền !!"); return;
                }
                DanhSachBanVe[MaHoaDon].KhachThanhToan = TienKhachThanhToan;
                DanhSachBanVe[MaHoaDon].GiamGia = float.Parse(txtGiamGia.Text);
                DanhSachBanVe[MaHoaDon].KhachCanTra = float.Parse(txtKhachCanTra.Text);
                dtBanVe dt = new dtBanVe();
                string IDNhanVien = Session["IDThuNgan"].ToString();
                string TenNhanVien = Session["TenThuNgan"].ToString();

                string IDKhachHang = "1";
                if (cmbKhachHang.Value != null)
                    IDKhachHang = cmbKhachHang.Value.ToString();

                string Diem = dtSetting.LayTienQuyDoiDiem();
                DanhSachBanVe[MaHoaDon].SoDiemTang = (int)(DanhSachBanVe[MaHoaDon].KhachCanTra / float.Parse(Diem));   

                object IDHoaDon = dt.InsertHoaDonBanVe(IDNhanVien, TenNhanVien, IDKhachHang, DanhSachBanVe[MaHoaDon],"");

                string IDNhanVien1 = "1"; // Session["IDThuNgan"].ToString();
                if (Session["IDThuNgan"] != null)
                    IDNhanVien1 = Session["IDThuNgan"].ToString();
                if (Session["IDNhanVien"] != null)
                    IDNhanVien1 = Session["IDNhanVien"].ToString();
                dtLichSuHeThong.ThemLichSuTruyCap(IDNhanVien1, "Bán vé", "Thanh toán hóa đơn ID: " + IDHoaDon);

                HuyHoaDon();
                cmbKhachHang.Text = "";
                //chitietbuilInLai.ContentUrl = "~/InHoaDonBanVe.aspx?IDVe=" + IDHoaDon;
                //chitietbuilInLai.ShowOnPageLoad = true;
                string jsInHoaDon = "window.open(\"InHoaDonBanVe.aspx?IDHoaDon=" + IDHoaDon + "\", \"PrintingFrame\");";
                ClientScript.RegisterStartupScript(this.GetType(), "Print", jsInHoaDon, true);
            }
            else
            {
                HienThiThongBao("Danh sách hàng hóa trống !!!");
                cmbKyHieu.Focus();
            }
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
           
        }
//        protected void cmbKhachHang_ItemRequestedByValue(object source, ListEditItemRequestedByValueEventArgs e)
//        {
//            long value = 0;
//            if (e.Value == null || !Int64.TryParse(e.Value.ToString(), out value))
//                return;
//            ASPxComboBox comboBox = (ASPxComboBox)source;
//            dsKhachHang.SelectCommand = @"SELECT ID, MaKhachHang,TenKhachHang,CMND,DienThoai,DiemTichLuy
//                                        FROM GPM_KhachHang        
//                                        WHERE (GPM_KhachHang.ID = @ID)";

//            dsKhachHang.SelectParameters.Clear();
//            dsKhachHang.SelectParameters.Add("ID", TypeCode.Int64, e.Value.ToString());
//            comboBox.DataSource = dsKhachHang;
//            comboBox.DataBind();
//        }

//        protected void cmbKhachHang_ItemsRequestedByFilterCondition(object source, ListEditItemsRequestedByFilterConditionEventArgs e)
//        {
//            ASPxComboBox comboBox = (ASPxComboBox)source;

//            dsKhachHang.SelectCommand = @"SELECT ID, MaKhachHang,TenKhachHang,CMND,DienThoai,DiemTichLuy
//                                        FROM (
//	                                        select ID, MaKhachHang,TenKhachHang,CMND,DienThoai,DiemTichLuy,
//	                                        row_number()over(order by MaKhachHang) as [rn] 
//	                                        FROM GPM_KhachHang
//	                                        WHERE ((DienThoai LIKE @DienThoai) OR (MaKhachHang LIKE @MaKhachHang) OR (TenKhachHang LIKE @TenKhachHang) OR (CMND LIKE @CMND))
//	                                        ) as st 
//                                        where st.[rn] between @startIndex and @endIndex";

//            dsKhachHang.SelectParameters.Clear();
//            dsKhachHang.SelectParameters.Add("DienThoai", TypeCode.String, string.Format("%{0}%", e.Filter));
//            dsKhachHang.SelectParameters.Add("MaKhachHang", TypeCode.String, string.Format("%{0}%", e.Filter));
//            dsKhachHang.SelectParameters.Add("TenKhachHang", TypeCode.String, string.Format("%{0}%", e.Filter));
//            dsKhachHang.SelectParameters.Add("CMND", TypeCode.String, string.Format("%{0}%", e.Filter));
//            dsKhachHang.SelectParameters.Add("startIndex", TypeCode.Int64, (e.BeginIndex + 1).ToString());
//            dsKhachHang.SelectParameters.Add("endIndex", TypeCode.Int64, (e.EndIndex + 1).ToString());
//            comboBox.DataSource = dsKhachHang;
//            comboBox.DataBind();
//        }

        protected void txtDiemTichLuy_TextChanged(object sender, EventArgs e)
        {
            //if (cmbKhachHang.Text != "")
            //{
            //    string IDKhachHang = cmbKhachHang.Value.ToString();
            //    dtBanVe data = new dtBanVe();
            //    int SoDiemCanDoi;
            //    bool isNumeric = Int32.TryParse(txtDiemTichLuy.Text, out SoDiemCanDoi);
            //    if (!isNumeric)
            //    {
            //        txtDiemTichLuy.Text = "0";
            //        txtGiamGia.Text = "0";
            //        txtDiemTichLuy.Focus();
            //        HienThiThongBao("Nhập không đúng số điểm !!"); return;
            //    }
            //    else
            //    {
            //        float DiemTichLuy = data.DiemTichLuy(IDKhachHang);
            //        if (float.Parse(txtDiemTichLuy.Text) > DiemTichLuy)
            //        {
                      
            //            txtDiemTichLuy.Text = "0";
            //            txtGiamGia.Text = "0";
            //            txtDiemTichLuy.Focus();
            //            txtKhachCanTra.Text = txtTongTien.Text.ToString();
            //            HienThiThongBao("Điểm tích lũy không đủ !!"); return;
            //        }
            //        else
            //        {
            //            float SoTienDoi = float.Parse(dtSetting.LayDiemQuyDoiTien());
            //            float TongTien = float.Parse(txtTongTien.Text);

            //            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            //            DanhSachBanVe[MaHoaDon].KhachThanhToan = (TongTien - (SoTienDoi * SoDiemCanDoi));
            //            txtKhachCanTra.Text = (TongTien - (SoTienDoi * SoDiemCanDoi)) + "";
            //            txtGiamGia.Text = (SoTienDoi * SoDiemCanDoi) + "";
            //            txtKhachThanhToan.Text = "0";
            //            txtTienThua.Text = "0";
            //        }
            //    }
            //}
            //else
            //{
            //    txtGiamGia.Text = "0";
            //    //txtDiemTichLuy.Text = "0";
            //    cmbKhachHang.Focus();
            //    HienThiThongBao("Vui lòng chọn khách hàng !!"); return;
            //}
        }

        protected void cmbKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;

            DanhSachBanVe[MaHoaDon].SoDiemTang = 0;
            DanhSachBanVe[MaHoaDon].SoDiemGiam = 0;
            DanhSachBanVe[MaHoaDon].HinhThucGiamGia = cmbGiamGia.Text;
            DanhSachBanVe[MaHoaDon].Giam = 0;
            DanhSachBanVe[MaHoaDon].GiamGia = 0;
            DanhSachBanVe[MaHoaDon].KhachCanTra = DanhSachBanVe[MaHoaDon].TongTien - DanhSachBanVe[MaHoaDon].GiamGia + DanhSachBanVe[MaHoaDon].PhuThu;
            BindGridChiTietHoaDon();
        }
        public void LamMoi()
        {
            //txtDiemTichLuy.Text = "0";
            txtGiamGia.Text = "0";
            txtKhachCanTra.Text = txtTongTien.Text;
            txtKhachThanhToan.Text = "0";
            txtTienThua.Text = "0";
        }

        protected void cmbGiamGia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbKhachHang.Text != "" && Int32.Parse(cmbKhachHang.Value + "") == 1)
            {
                HienThiThongBao("Chọn khách hàng"); return;
                cmbGiamGia.Value = 1;
            }
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            DanhSachBanVe[MaHoaDon].SoDiemTang = 0;
            DanhSachBanVe[MaHoaDon].SoDiemGiam = 0;
            DanhSachBanVe[MaHoaDon].HinhThucGiamGia = cmbGiamGia.Text;
            DanhSachBanVe[MaHoaDon].Giam = 0;
            DanhSachBanVe[MaHoaDon].GiamGia = 0;
            DanhSachBanVe[MaHoaDon].KhachCanTra = DanhSachBanVe[MaHoaDon].TongTien - DanhSachBanVe[MaHoaDon].GiamGia + DanhSachBanVe[MaHoaDon].PhuThu;
            BatchUpdate();
            BindGridChiTietHoaDon();
        }

        protected void txtNhapGiamGia_TextChanged(object sender, EventArgs e)
        {
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            float g = 0;
            if (Int32.Parse(cmbGiamGia.Value + "") == 3 && cmbKhachHang.Text != "")
            {
                // Điểm
                string idKH = cmbKhachHang.Value + "";
                dtKhachHang dt = new dtKhachHang();
                string Diem = dt.layDiemTichLuy(idKH);
                string TienQuyDoi = dtSetting.LayDiemQuyDoiTien();

                DanhSachBanVe[MaHoaDon].Giam = Int32.Parse(txtNhapGiamGia.Value + "");
                DanhSachBanVe[MaHoaDon].SoDiemGiam = Int32.Parse(txtNhapGiamGia.Value + "");

                if (DanhSachBanVe[MaHoaDon].Giam <= float.Parse(Diem))
                {
                    txtGiamGia.Value = float.Parse(txtNhapGiamGia.Value + "") * float.Parse(TienQuyDoi);
                    g = float.Parse(txtNhapGiamGia.Value + "") * float.Parse(TienQuyDoi);
                }
                else
                {
                    g = 0;
                    DanhSachBanVe[MaHoaDon].Giam = 0;
                    HienThiThongBao("Số điểm hiện có không đủ. !!"); return;
                }

            }
            else if (Int32.Parse(cmbGiamGia.Value + "") == 2)
            {
                //Tiền
                DanhSachBanVe[MaHoaDon].Giam = Int32.Parse(txtNhapGiamGia.Value + "");
                g = float.Parse(txtNhapGiamGia.Value + "");
            }
            else if (Int32.Parse(cmbGiamGia.Value + "") == 1)
            {
                // %
                DanhSachBanVe[MaHoaDon].Giam = Int32.Parse(txtNhapGiamGia.Value + "");
                g = (DanhSachBanVe[MaHoaDon].TongTien * DanhSachBanVe[MaHoaDon].Giam) / 100;
            }

            DanhSachBanVe[MaHoaDon].HinhThucGiamGia = cmbGiamGia.Text;
            DanhSachBanVe[MaHoaDon].GiamGia = g;
            DanhSachBanVe[MaHoaDon].PhuThu = float.Parse(txtPhuThu.Value + "");
            DanhSachBanVe[MaHoaDon].KhachCanTra = DanhSachBanVe[MaHoaDon].TongTien - DanhSachBanVe[MaHoaDon].GiamGia + DanhSachBanVe[MaHoaDon].PhuThu;
            BatchUpdate();
            BindGridChiTietHoaDon();
        }

        protected void txtPhuThu_TextChanged(object sender, EventArgs e)
        {
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            DanhSachBanVe[MaHoaDon].PhuThu = float.Parse(txtPhuThu.Value + "");
            DanhSachBanVe[MaHoaDon].KhachCanTra = DanhSachBanVe[MaHoaDon].TongTien - DanhSachBanVe[MaHoaDon].GiamGia + DanhSachBanVe[MaHoaDon].PhuThu;
            BatchUpdate();
            BindGridChiTietHoaDon();
        }

        protected void txtTongTien_TextChanged(object sender, EventArgs e)
        {
            int phuthu = dtSetting.PhuThu();
            int MaHoaDon = tabControlSoHoaDon.ActiveTabIndex;
            txtPhuThu.Value = (float.Parse(txtTongTien.Value + "") * phuthu) / 100;
            DanhSachBanVe[MaHoaDon].PhuThu = float.Parse(txtPhuThu.Value + "");
            DanhSachBanVe[MaHoaDon].KhachCanTra = DanhSachBanVe[MaHoaDon].TongTien - DanhSachBanVe[MaHoaDon].GiamGia + DanhSachBanVe[MaHoaDon].PhuThu;
        }
    }
    [Serializable]
    public class HoaDonBanVe
    {
        public int IDHoaDon { get; set; }
        public int SoLuongHang { get; set; }
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
        public List<ChiTietBanVe> ListChiTietBanVe { get; set; }
        public HoaDonBanVe()
        {
            SoLuongHang = 0;
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
            ListChiTietBanVe = new List<ChiTietBanVe>();
        }
    }
    [Serializable]
    public class ChiTietBanVe
    {
        public int STT { get; set; }
        public string TenKyHieu { get; set; }
        public int SoLuong { get; set; }
        public float DonGia { get; set; }
        public float ThanhTien { get; set; }
        public ChiTietBanVe(int stt, string ten, int sl, float gia, float tt)
        {
            STT = stt;
            TenKyHieu = ten;
            SoLuong = sl;
            DonGia = gia;
            ThanhTien = tt;
        }
    }
        
    
}