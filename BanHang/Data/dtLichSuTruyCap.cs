using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BanHang.Data
{
    public class dtLichSuTruyCap
    {
        public DataTable LayDanhSach()
        {
            using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            {
                con.Open();
                string cmdText = "SELECT TOP 1000 * FROM [GPM_LichSuTruyCap] WHERE [IDNhanVien] !=1 ORDER BY [ID] DESC ";
                using (SqlCommand command = new SqlCommand(cmdText, con))
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    DataTable tb = new DataTable();
                    tb.Load(reader);
                    return tb;
                }
            }
        }
        public static void ThemLichSu(string IDNhanVien, string IDNhom, string DoiTuong, string IDKho, string TenChucNang, string HanhDong)
        {
            using (SqlConnection myConnection = new SqlConnection(StaticContext.ConnectionString))
            {
                string myServer = Environment.MachineName; // tên máy tinh
                try
                {
                    myConnection.Open();
                    string cmdText = "INSERT INTO [GPM_LichSuTruyCap] ([IDNhanVien], [IDNhom], [NoiDung], [IDKho],[TenMay],[TenChucNang],[HanhDong], [ThoiGian]) VALUES (@IDNhanVien, @IDNhom,@NoiDung, @IDKho,@TenMay,@TenChucNang,@HanhDong, getdate())";
                    using (SqlCommand myCommand = new SqlCommand(cmdText, myConnection))
                    {
                        myCommand.Parameters.AddWithValue("@IDNhanVien", IDNhanVien);
                        myCommand.Parameters.AddWithValue("@IDNhom", IDNhom);
                        myCommand.Parameters.AddWithValue("@NoiDung", DoiTuong);
                        myCommand.Parameters.AddWithValue("@IDKho", IDKho);
                        myCommand.Parameters.AddWithValue("@TenMay", myServer);
                        myCommand.Parameters.AddWithValue("@TenChucNang", TenChucNang);
                        myCommand.Parameters.AddWithValue("@HanhDong", HanhDong);
                        myCommand.ExecuteNonQuery();
                    }
                    myConnection.Close();
                }
                catch
                {
                    throw new Exception("Lỗi: Quá trình thêm dữ liệu gặp lỗi");
                }
            }
        }
    }
}