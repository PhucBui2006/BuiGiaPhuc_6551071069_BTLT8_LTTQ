using System;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using bai1c9.Models;
using ClosedXML.Excel;
using FastReport;
using FastReport.Export.Pdf;
using Microsoft.EntityFrameworkCore;

namespace bai1c9;

public static class TestRunner
{
    public static async Task RunTests()
    {
        Console.WriteLine("=== BẮT ĐẦU KIỂM THỬ TỰ ĐỘNG BÀI 1 ===");

        using var context = new TiemTapHoaDbContext();

        // 1. Kiểm tra CSDL
        var allItems = await context.HangHoas.AsNoTracking().ToListAsync();
        Console.WriteLine($"[1] Tổng số mặt hàng trong CSDL: {allItems.Count}");

        // 2. Kiểm tra lọc "nước"
        var nuocItems = await context.HangHoas.AsNoTracking()
            .Where(h => EF.Functions.Like(h.TenHang, "%nước%"))
            .ToListAsync();
        Console.WriteLine($"[2] Lọc theo từ khóa 'nước': {nuocItems.Count} dòng");

        // 3. Kiểm tra lọc "nước" + ConKinhDoanh == true
        var nuocConBan = await context.HangHoas.AsNoTracking()
            .Where(h => EF.Functions.Like(h.TenHang, "%nước%") && h.ConKinhDoanh)
            .ToListAsync();
        Console.WriteLine($"[3] Lọc 'nước' + 'Còn kinh doanh': {nuocConBan.Count} dòng");

        // 4. Kiểm tra dữ liệu report bảng giá (chỉ lấy hàng còn kinh doanh)
        var reportItems = await context.HangHoas.AsNoTracking()
            .Where(h => h.ConKinhDoanh)
            .OrderBy(h => h.MaHang)
            .ToListAsync();
        Console.WriteLine($"[4] Danh sách in bảng giá: {reportItems.Count} dòng");

        // 5. Test FastReport nạp BangGia.frx, gán DataTable và xuất PDF
        string baseDir = AppDomain.CurrentDomain.BaseDirectory;
        string reportPath = Path.Combine(baseDir, "Reports", "BangGia.frx");
        if (!File.Exists(reportPath))
        {
            reportPath = Path.Combine(Directory.GetCurrentDirectory(), "Reports", "BangGia.frx");
        }

        Console.WriteLine($"[5] Nạp mẫu report: {reportPath}");
        DataTable dt = new DataTable("HangHoa");
        dt.Columns.Add("MaHang", typeof(int));
        dt.Columns.Add("TenHang", typeof(string));
        dt.Columns.Add("LoaiHang", typeof(string));
        dt.Columns.Add("DonVi", typeof(string));
        dt.Columns.Add("GiaBan", typeof(decimal));
        dt.Columns.Add("SoLuongTon", typeof(int));
        dt.Columns.Add("NgayNhap", typeof(DateTime));
        dt.Columns.Add("ConKinhDoanh", typeof(bool));
        foreach (var h in reportItems)
        {
            dt.Rows.Add(h.MaHang, h.TenHang, h.LoaiHang, h.DonVi, h.GiaBan ?? 0, h.SoLuongTon ?? 0, h.NgayNhap ?? DateTime.Now, h.ConKinhDoanh);
        }

        using (Report report = new Report())
        {
            report.Load(reportPath);
            report.RegisterData(dt, "HangHoa");
            var ds = report.GetDataSource("HangHoa");
            if (ds != null) ds.Enabled = true;
            if (report.FindObject("Data1") is DataBand dataBand)
            {
                dataBand.DataSource = ds;
            }
            report.SetParameterValue("NgayIn", DateTime.Now.ToString("dd/MM/yyyy"));
            report.Prepare();

            string testPdf = Path.Combine(baseDir, "BangGia_Test.pdf");
            using var pdfExport = new PDFExport();
            report.Export(pdfExport, testPdf);
            Console.WriteLine($"[5] Xuất PDF thành công tại: {testPdf} ({new FileInfo(testPdf).Length} bytes)");
        }

        // 6. Test xuất Excel tồn kho với ClosedXML, công thức và tổng 7.300.000
        string testExcel = Path.Combine(baseDir, "BaoCaoTonKho_Test.xlsx");
        using (var wb = new XLWorkbook())
        {
            var ws = wb.Worksheets.Add("TonKho");
            string[] headers = { "STT", "Tên hàng", "Đơn vị", "Giá bán", "Số lượng tồn", "Giá trị tồn" };
            for (int i = 0; i < headers.Length; i++)
            {
                var cell = ws.Cell(1, i + 1);
                cell.Value = headers[i];
                cell.Style.Font.Bold = true;
            }

            for (int i = 0; i < reportItems.Count; i++)
            {
                int r = i + 2;
                var hh = reportItems[i];
                ws.Cell(r, 1).Value = i + 1;
                ws.Cell(r, 2).Value = hh.TenHang;
                ws.Cell(r, 3).Value = hh.DonVi ?? "";
                ws.Cell(r, 4).Value = (double)(hh.GiaBan ?? 0);
                ws.Cell(r, 4).Style.NumberFormat.Format = "#,##0";
                ws.Cell(r, 5).Value = (double)(hh.SoLuongTon ?? 0);
                ws.Cell(r, 5).Style.NumberFormat.Format = "#,##0";
                ws.Cell(r, 6).FormulaA1 = $"=D{r}*E{r}";
                ws.Cell(r, 6).Style.NumberFormat.Format = "#,##0";
            }

            int totalRow = reportItems.Count + 2;
            ws.Cell(totalRow, 2).Value = "Tổng";
            ws.Cell(totalRow, 6).FormulaA1 = $"=SUM(F2:F{totalRow - 1})";
            ws.Cell(totalRow, 6).Style.NumberFormat.Format = "#,##0";
            ws.SheetView.FreezeRows(1);
            ws.Range(1, 1, reportItems.Count + 1, 6).SetAutoFilter();
            ws.Columns().AdjustToContents();
            wb.SaveAs(testExcel);
        }

        Console.WriteLine($"[6] Xuất Excel tồn kho thành công: {testExcel}");

        // Kiểm tra tính toán công thức tổng giá trị tồn
        double tongGiaTri = reportItems.Sum(h => (double)(h.GiaBan ?? 0) * (double)(h.SoLuongTon ?? 0));
        Console.WriteLine($"[7] Tổng giá trị tồn kỳ vọng: {tongGiaTri:N0} VNĐ (Kỳ vọng: 7.300.000 VNĐ)");
        if (tongGiaTri != 7300000) throw new Exception("Tổng giá trị tồn không khớp 7.300.000!");

        Console.WriteLine("=== TẤT CẢ CÁC TEST ĐÃ VƯỢT QUA XUẤT SẮC! ===");
    }
}
