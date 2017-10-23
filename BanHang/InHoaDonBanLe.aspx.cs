using BanHang.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DevExpress.XtraReports.UI;
using System.IO;
using DevExpress.XtraPrinting;
using System.Data.SqlClient;
using BanHang.Data;

namespace BanHang
{
    public partial class InHoaDonBanLe : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {            
            //using (MemoryStream ms = new MemoryStream())
            //{
            //    //rpHoaDonBanHangLe r = new rpHoaDonBanHangLe();
            //    //r.Parameters["IDHoaDon"].Value = Request.QueryString["IDHoaDon"];
            //    //r.Parameters["IDHoaDon"].Visible = false;
            //    //viewerReport.Report = r;

                  
            //}    
            //using (SqlConnection con = new SqlConnection(StaticContext.ConnectionString))
            //{
            //    con.Open();
            //    string cmdtext = "UPDATE [dbo].[GPM_HoaDon] SET [SoLanIn] = [SoLanIn] + 1 WHERE ID = @ID";
            //    using (SqlCommand cmd = new SqlCommand(cmdtext, con))
            //    {
            //        cmd.Parameters.AddWithValue("@ID", Request.QueryString["IDHoaDon"]);
            //        cmd.ExecuteNonQuery();
            //    }
            //    con.Close();
            //}
            using (MemoryStream ms = new MemoryStream())
            {
                rpHoaDonBanHangLe r = new rpHoaDonBanHangLe();
                r.Parameters["IDHoaDon"].Value = Request.QueryString["IDHoaDon"];
                //r.Parameters["IDKho"].Value = Session["IDKho"].ToString();
                r.CreateDocument();
                PdfExportOptions opts = new PdfExportOptions();
                opts.ShowPrintDialogOnOpen = true;
                r.ExportToPdf(ms, opts);
                ms.Seek(0, SeekOrigin.Begin);
                byte[] report = ms.ToArray();
                Page.Response.ContentType = "application/pdf";
                Page.Response.Clear();
                Page.Response.OutputStream.Write(report, 0, report.Length);
                Page.Response.End();
            } 
        }
    }
}