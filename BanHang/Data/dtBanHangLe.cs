using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace BanHang.Data
{
    public class dtBanHangLe
    {
        public DataTable LayThongHoaDon()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select TOP 10 GPM_HoaDon.ID,GPM_HoaDon.TongTien,GPM_KhachHang.TenKhachHang,GPM_HoaDon.NgayBan from GPM_HoaDon,GPM_KhachHang WHERE GPM_HoaDon.IDKhachHang = GPM_KhachHang.ID ORDER BY GPM_HoaDon.ID DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
        }

        public string LayMaHoaDon_ID(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select MaHoaDon from GPM_HoaDon where ID = '" + ID + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb.Rows[0]["MaHoaDon"].ToString();
                    }
                }
            }
        }

        public void CapNhatKetCa(string IDNhanVien, string GioBD, string GioKT, string TongTien, string GiamGia, string Tong)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                try
                {
                    object ID = "";
                    myConnection.Open();
                    string strSQL = "INSERT INTO GPM_KetCa(GioBatDau,GioKetThuc,IDNhanVien,TongTienTruoc,GiamGia,TongTienSau)  OUTPUT INSERTED.ID VALUES (@GioBatDau,@GioKetThuc,@IDNhanVien,@TongTienTruoc,@GiamGia,@TongTienSau) ";
                    using (SqlCommand myCommand = new SqlCommand(strSQL, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@GioBatDau", GioBD);
                        myCommand.Parameters.AddWithValue("@GioKetThuc", GioKT);
                        myCommand.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                        myCommand.Parameters.AddWithValue("@TongTienTruoc", TongTien);
                        myCommand.Parameters.AddWithValue("@GiamGia", GiamGia);
                        myCommand.Parameters.AddWithValue("@TongTienSau", Tong);
                        ID = myCommand.ExecuteScalar();
                    }

                    string strSQL1 = "UPDATE GPM_HoaDon SET KetCa = @KetCa WHERE IDNhanVien = @IDNhanVien AND KetCa = 0";
                    using (SqlCommand myCommand = new SqlCommand(strSQL1, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@KetCa", ID);
                        myCommand.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                        myCommand.ExecuteNonQuery();
                    }
                }
                catch (Exception e)
                {
                    throw new Exception("Lỗi: Quá trình Xóa dữ liệu gặp lỗi, hãy tải lại trang");
                }
            }
        }

        public DataTable LaySoTienKetCa(string IDNhanVien)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT SUM(GiamGia) as GiamGia, SUM(KhachCanTra) as KhachCanTra FROM GPM_HoaDon WHERE KetCa = 0 AND IDNhanVien ='" + IDNhanVien + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
        }

        public DataTable DanhSachKetCa()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM GPM_KetCa WHERE GioKetThuc >= '" + DateTime.Now.ToString("yyyy-MM-dd 00:00:00.000") + "' AND GioKetThuc <= '" + DateTime.Now.ToString("yyyy-MM-dd 23:23:59.000") + "' ORDER BY ID DESC";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
        }

        public DataTable LayThoiGianKetCa(string IDNhanVien)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT * FROM GPM_HoaDon WHERE KetCa = 0 AND IDNhanVien ='" + IDNhanVien + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
        }

        public DataTable LayThongHoaDon_BaoCao(string NgayBD, string NgayKT, string IDKho)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select GPM_ChiTietHoaDon.GiaMua,GPM_ChiTietHoaDon.GiaBan,GPM_ChiTietHoaDon.SoLuong from GPM_ChiTietHoaDon,GPM_HoaDon where GPM_ChiTietHoaDon.IDHangHoa = GPM_HoaDon.ID and GPM_HoaDon.IDKho = '" + IDKho + "' and GPM_HoaDon.NgayBan >= '" + NgayBD + "' and GPM_HoaDon.NgayBan <= '" + NgayKT + "'";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
        }

        public DataTable LayThongChiTietHoaDon_ID(string IDHoaDon)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select GPM_ChiTietHoaDon.DonGia as GiaBan,GPM_ChiTietHoaDon.SoLuong,GPM_ChiTietHoaDon.TongTien as ThanhTien,GPM_HangHoa.TenHangHoa,GPM_HangHoa.MaHang,GPM_DonViTinh.TenDonViTinh from GPM_ChiTietHoaDon,GPM_HangHoa,GPM_DonViTinh WHERE GPM_HangHoa.ID = GPM_ChiTietHoaDon.IDHangHoa AND GPM_HangHoa.IDDonViTinh = GPM_DonViTinh.ID AND GPM_ChiTietHoaDon.IDHoaDon = " + IDHoaDon;
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
        }
        public DataTable LayThongTinHangHoa(string Barcode)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoa.IDDonViTinh, GPM_HangHoa.GiaBan1, GPM_DonViTinh.TenDonViTinh, GPM_HangHoa_Barcode.Barcode from GPM_HangHoa, GPM_DonViTinh, GPM_HangHoa_Barcode where GPM_HangHoa.ID = GPM_HangHoa_Barcode.IDHangHoa and GPM_HangHoa_Barcode.Barcode = '" + Barcode + "' and GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    command.Parameters.AddWithValue("@Barcode", Barcode);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
        }

        public DataTable LayThongTinHangHoa2(string ID)
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "select GPM_HangHoa.ID, GPM_HangHoa.MaHang, GPM_HangHoa.TenHangHoa, GPM_HangHoa.IDDonViTinh, GPM_HangHoa.GiaBan1, GPM_DonViTinh.TenDonViTinh, GPM_HangHoa_Barcode.Barcode from GPM_HangHoa, GPM_DonViTinh, GPM_HangHoa_Barcode where GPM_HangHoa.ID = GPM_HangHoa_Barcode.IDHangHoa and GPM_HangHoa.ID = '" + ID + "' and GPM_DonViTinh.ID = GPM_HangHoa.IDDonViTinh";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                {
                    command.Parameters.AddWithValue("@Barcode", ID);
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        DataTable tb = new DataTable();
                        tb.Load(reader);
                        return tb;
                    }
                }
            }
        }

        public object InsertHoaDon(string IDNhanVien, string IDKhachHang, HoaDon hoaDon)
        {
            object IDHoaDon = null;
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                SqlTransaction trans = null;
                try
                {
                    con.Open();
                    trans = con.BeginTransaction();
                    string CompuMaHoaDon = @"SELECT 
                                          REPLICATE('0', 6 - LEN((count(ID) + 1))) + 
                                          CAST((count(ID) + 1) AS varchar) + '-' + 
                                          FORMAT(GETDATE() , 'ddMMyy')
                                          as 'Mã Hóa Đơn'  
                                          from GPM_HoaDon 
                                          where DATEDIFF(dd,NgayBan, GetDate()) = 0";
                    object MaHoaDon;
                    using (SqlCommand cmd = new SqlCommand(CompuMaHoaDon, con, trans))
                    {
                        MaHoaDon = cmd.ExecuteScalar();
                    }
                    if (MaHoaDon != null)
                    {


                        string InsertHoaDon = "INSERT INTO [GPM_HoaDon] ([MaHoaDon],[IDKhachHang],[IDNhanVien],[NgayBan],[TongTien],[HinhThucGiamGia],[GiamGia],[PhuThu],[KhachCanTra],[KhachThanhToan],[TienThua]) " +
                                              "OUTPUT INSERTED.ID " +
                                              "VALUES (@MaHoaDon,@IDKhachHang, @IDNhanVien, getdate(), @TongTien, @HinhThucGiamGia, @GiamGia, @PhuThu, @KhachCanTra, @KhachThanhToan, @TienThua)";

                        using (SqlCommand cmd = new SqlCommand(InsertHoaDon, con, trans))
                        {
                            cmd.Parameters.AddWithValue("@MaHoaDon", MaHoaDon);
                            cmd.Parameters.AddWithValue("@IDKhachHang", IDKhachHang);
                            cmd.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                            cmd.Parameters.AddWithValue("@TongTien", hoaDon.TongTien);
                            cmd.Parameters.AddWithValue("@HinhThucGiamGia", hoaDon.HinhThucGiamGia);
                            cmd.Parameters.AddWithValue("@GiamGia", hoaDon.GiamGia);
                            cmd.Parameters.AddWithValue("@PhuThu", hoaDon.PhuThu);
                            cmd.Parameters.AddWithValue("@KhachCanTra", hoaDon.KhachCanTra);
                            cmd.Parameters.AddWithValue("@KhachThanhToan", hoaDon.KhachThanhToan);
                            cmd.Parameters.AddWithValue("@TienThua", hoaDon.TienThua);
                            IDHoaDon = cmd.ExecuteScalar();
                        }

                        dtBanVe dt = new dtBanVe();
                        float diem = dt.DiemTichLuy(IDKhachHang);
                        string s = "INSERT INTO GPM_LichSuQuyDoiDiem ([IDKhachHang],[SoDiemCu],[SoDiemMoi],[NoiDung],[Ngay],[HinhThuc]) VALUES (@IDKhachHang,@SoDiemCu,@SoDiemMoi,@NoiDung,@Ngay,@HinhThuc) " +
                            "INSERT INTO GPM_LichSuQuyDoiDiem ([IDKhachHang],[SoDiemCu],[SoDiemMoi],[NoiDung],[Ngay],[HinhThuc]) VALUES (@IDKhachHang,@SoDiemCu1,@SoDiemMoi1,@NoiDung,@Ngay,@HinhThuc1)";
                        using (SqlCommand cmd = new SqlCommand(s, con, trans))
                        {
                            cmd.Parameters.AddWithValue("@IDKhachHang", IDKhachHang);
                            cmd.Parameters.AddWithValue("@SoDiemCu", diem);
                            cmd.Parameters.AddWithValue("@SoDiemMoi", diem - hoaDon.SoDiemGiam);
                            cmd.Parameters.AddWithValue("@SoDiemCu1", diem - hoaDon.SoDiemGiam);
                            cmd.Parameters.AddWithValue("@SoDiemMoi1", (diem - hoaDon.SoDiemGiam) + hoaDon.SoDiemTang);
                            cmd.Parameters.AddWithValue("@NoiDung", "Thanh toán bán hàng");
                            cmd.Parameters.AddWithValue("@Ngay", DateTime.Now);
                            cmd.Parameters.AddWithValue("@HinhThuc", "Trừ");
                            cmd.Parameters.AddWithValue("@HinhThuc1", "Cộng");
                            if (int.Parse(IDKhachHang) != 1)
                                cmd.ExecuteNonQuery();
                        }

                        //DateTime date = DateTime.Now;
                        //string strDate = date.ToString("ddMMyyyy") + "-";
                        //string MaHD = strDate + ((int)IDHoaDon * 0.0001).ToString().Replace(".", "");

                        //string strUpdateMaHoaDon = "update GPM_HoaDon set MaHoaDon = @MaHoaDon where ID = @ID";
                        //using (SqlCommand cmd = new SqlCommand(strUpdateMaHoaDon, con, trans))
                        //{
                        //    cmd.Parameters.AddWithValue("@MaHoaDon", MaHD);
                        //    cmd.Parameters.AddWithValue("@ID", IDHoaDon);
                        //    cmd.ExecuteNonQuery();
                        //}

                        string strUpdateDiemKH = "update GPM_KhachHang set DiemTichLuy = DiemTichLuy - @dTL where ID = @IDKhachHang and ID != 1";
                        using (SqlCommand cmd = new SqlCommand(strUpdateDiemKH, con, trans))
                        {
                            cmd.Parameters.AddWithValue("@dTL", hoaDon.SoDiemGiam);
                            cmd.Parameters.AddWithValue("@IDKhachHang", IDKhachHang);
                            cmd.ExecuteNonQuery();
                        }

                        string strUpdateDiemKHTang = "update GPM_KhachHang set DiemTichLuy = DiemTichLuy + @dTLTang where ID = @IDKhachHang and ID != 1";
                        using (SqlCommand cmd = new SqlCommand(strUpdateDiemKHTang, con, trans))
                        {
                            cmd.Parameters.AddWithValue("@dTLTang", hoaDon.SoDiemTang);
                            cmd.Parameters.AddWithValue("@IDKhachHang", IDKhachHang);
                            cmd.ExecuteNonQuery();
                        }



                        if (IDHoaDon != null)
                        {
                            foreach (ChiTietHoaDon cthd in hoaDon.ListChiTietHoaDon)
                            {
                                dtHangHoa dtHH = new dtHangHoa();
                                dtLichSuHeThong.ThemLichSuBanHang(IDNhanVien + "", IDKhachHang + "", "Bán hàng", cthd.MaHang + "", cthd.SoLuong + "", cthd.DonGia + "", "Bán hàng");

                                string InsertChiTietHoaDon = "INSERT INTO [GPM_ChiTietHoaDon] ([IDHoaDon],[IDHangHoa] ,[SoLuong],[DonGia],[TongTien]) " +
                                                             "VALUES (@IDHoaDon, @IDHangHoa, @SoLuong, @DonGia, @TongTien)";
                                using (SqlCommand cmd = new SqlCommand(InsertChiTietHoaDon, con, trans))
                                {
                                    cmd.Parameters.AddWithValue("@IDHoaDon", IDHoaDon);
                                    cmd.Parameters.AddWithValue("@IDHangHoa", cthd.MaHang + "");
                                    cmd.Parameters.AddWithValue("@SoLuong", cthd.SoLuong);
                                    cmd.Parameters.AddWithValue("@DonGia", cthd.DonGia);
                                    cmd.Parameters.AddWithValue("@TongTien", cthd.ThanhTien);
                                    cmd.ExecuteNonQuery();
                                }

                                string UpdateLichSuBanHang = "update GPM_HangHoaTonKho set SoLuongCon = SoLuongCon - @SL where IDHangHoa = (select ID from GPM_HangHoa where TrangThaiHang != 2 and ID = @IDHangHoa)";
                                using (SqlCommand cmd = new SqlCommand(UpdateLichSuBanHang, con, trans))
                                {
                                    cmd.Parameters.AddWithValue("@SL", cthd.SoLuong);
                                    cmd.Parameters.AddWithValue("@IDHangHoa", cthd.MaHang + "");
                                    cmd.ExecuteNonQuery();
                                }


                            }
                        }
                        trans.Commit();
                        con.Close();
                    }
                }
                catch (Exception ex)
                {
                    if (trans != null) trans.Rollback();
                    throw new Exception("Quá trình lưu dữ liệu có lỗi xảy ra, vui lòng tải lại trang và thanh toán lại !!");
                }
            }
            return IDHoaDon;
        }
        
    }
}