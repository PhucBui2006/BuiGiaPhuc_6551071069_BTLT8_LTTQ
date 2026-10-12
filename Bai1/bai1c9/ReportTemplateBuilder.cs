using System;
using System.Data;
using System.Drawing;
using System.IO;
using FastReport;
using FastReport.Data;
using FastReport.Format;

namespace bai1c9;

public static class ReportTemplateBuilder
{
    public static void CreateBangGiaTemplate(string filePath)
    {
        Report report = new Report();

        // Parameter NgayIn
        FastReport.Data.Parameter paramNgayIn = new FastReport.Data.Parameter("NgayIn")
        {
            DataType = typeof(string)
        };
        report.Parameters.Add(paramNgayIn);

        // Dummy data table to register schema so FastReport knows HangHoa fields
        DataTable dt = new DataTable("HangHoa");
        dt.Columns.Add("STT", typeof(int));
        dt.Columns.Add("TenHang", typeof(string));
        dt.Columns.Add("DonVi", typeof(string));
        dt.Columns.Add("GiaBan", typeof(decimal));
        report.RegisterData(dt, "HangHoa");

        ReportPage page = new ReportPage();
        page.Name = "Page1";
        page.PaperWidth = 210; // A4
        page.PaperHeight = 297;
        report.Pages.Add(page);

        // 1. ReportTitle
        ReportTitleBand titleBand = new ReportTitleBand();
        titleBand.Name = "ReportTitle1";
        titleBand.Height = 70;
        page.ReportTitle = titleBand;

        TextObject titleText = new TextObject();
        titleText.Name = "TextTitle";
        titleText.Bounds = new RectangleF(0, 15, page.PaperWidth * 3.78f - 75f, 25);
        titleText.Text = "BẢNG GIÁ HÀNG HÓA – TẠP HÓA NHÀ TRƯỜNG UTC2";
        titleText.Font = new Font("Arial", 14, FontStyle.Bold);
        titleText.HorzAlign = HorzAlign.Center;
        titleBand.Objects.Add(titleText);

        TextObject dateText = new TextObject();
        dateText.Name = "TextNgayIn";
        dateText.Bounds = new RectangleF(0, 45, page.PaperWidth * 3.78f - 75f, 20);
        dateText.Text = "Ngày in: [NgayIn]";
        dateText.Font = new Font("Arial", 10, FontStyle.Italic);
        dateText.HorzAlign = HorzAlign.Center;
        titleBand.Objects.Add(dateText);

        // 2. PageHeader
        PageHeaderBand pageHeader = new PageHeaderBand();
        pageHeader.Name = "PageHeader1";
        pageHeader.Height = 25;
        page.PageHeader = pageHeader;

        float startX = 75;
        float wSTT = 45;
        float wTenHang = 260;
        float wDonVi = 90;
        float wGiaBan = 140;

        TextObject hSTT = new TextObject
        {
            Name = "HeaderSTT",
            Bounds = new RectangleF(startX, 0, wSTT, 25),
            Text = "STT",
            Font = new Font("Arial", 10, FontStyle.Bold),
            HorzAlign = HorzAlign.Center,
            VertAlign = VertAlign.Center,
            Border = { Lines = BorderLines.All }
        };
        pageHeader.Objects.Add(hSTT);

        TextObject hTen = new TextObject
        {
            Name = "HeaderTenHang",
            Bounds = new RectangleF(startX + wSTT, 0, wTenHang, 25),
            Text = "Tên hàng",
            Font = new Font("Arial", 10, FontStyle.Bold),
            HorzAlign = HorzAlign.Center,
            VertAlign = VertAlign.Center,
            Border = { Lines = BorderLines.All }
        };
        pageHeader.Objects.Add(hTen);

        TextObject hDonVi = new TextObject
        {
            Name = "HeaderDonVi",
            Bounds = new RectangleF(startX + wSTT + wTenHang, 0, wDonVi, 25),
            Text = "Đơn vị",
            Font = new Font("Arial", 10, FontStyle.Bold),
            HorzAlign = HorzAlign.Center,
            VertAlign = VertAlign.Center,
            Border = { Lines = BorderLines.All }
        };
        pageHeader.Objects.Add(hDonVi);

        TextObject hGia = new TextObject
        {
            Name = "HeaderGiaBan",
            Bounds = new RectangleF(startX + wSTT + wTenHang + wDonVi, 0, wGiaBan, 25),
            Text = "Giá bán",
            Font = new Font("Arial", 10, FontStyle.Bold),
            HorzAlign = HorzAlign.Center,
            VertAlign = VertAlign.Center,
            Border = { Lines = BorderLines.All }
        };
        pageHeader.Objects.Add(hGia);

        // 3. DataBand
        DataBand dataBand = new DataBand();
        dataBand.Name = "Data1";
        dataBand.Height = 22;
        dataBand.DataSource = report.GetDataSource("HangHoa");
        page.Bands.Add(dataBand);

        TextObject dSTT = new TextObject
        {
            Name = "DataSTT",
            Bounds = new RectangleF(startX, 0, wSTT, 22),
            Text = "[Row#]",
            Font = new Font("Arial", 10, FontStyle.Regular),
            HorzAlign = HorzAlign.Center,
            VertAlign = VertAlign.Center,
            Border = { Lines = BorderLines.All }
        };
        dataBand.Objects.Add(dSTT);

        TextObject dTen = new TextObject
        {
            Name = "DataTenHang",
            Bounds = new RectangleF(startX + wSTT, 0, wTenHang, 22),
            Text = "[HangHoa.TenHang]",
            Font = new Font("Arial", 10, FontStyle.Regular),
            HorzAlign = HorzAlign.Left,
            VertAlign = VertAlign.Center,
            Border = { Lines = BorderLines.All },
            Padding = new Padding(5, 0, 0, 0)
        };
        dataBand.Objects.Add(dTen);

        TextObject dDonVi = new TextObject
        {
            Name = "DataDonVi",
            Bounds = new RectangleF(startX + wSTT + wTenHang, 0, wDonVi, 22),
            Text = "[HangHoa.DonVi]",
            Font = new Font("Arial", 10, FontStyle.Regular),
            HorzAlign = HorzAlign.Center,
            VertAlign = VertAlign.Center,
            Border = { Lines = BorderLines.All }
        };
        dataBand.Objects.Add(dDonVi);

        NumberFormat numFmt = new NumberFormat
        {
            UseLocale = false,
            DecimalDigits = 0,
            DecimalSeparator = ",",
            GroupSeparator = "."
        };

        TextObject dGia = new TextObject
        {
            Name = "DataGiaBan",
            Bounds = new RectangleF(startX + wSTT + wTenHang + wDonVi, 0, wGiaBan, 22),
            Text = "[HangHoa.GiaBan]",
            Format = numFmt,
            Font = new Font("Arial", 10, FontStyle.Regular),
            HorzAlign = HorzAlign.Right,
            VertAlign = VertAlign.Center,
            Border = { Lines = BorderLines.All },
            Padding = new Padding(0, 0, 5, 0)
        };
        dataBand.Objects.Add(dGia);

        // 4. PageFooter
        PageFooterBand pageFooter = new PageFooterBand();
        pageFooter.Name = "PageFooter1";
        pageFooter.Height = 25;
        page.PageFooter = pageFooter;

        TextObject fPage = new TextObject
        {
            Name = "TextFooter",
            Bounds = new RectangleF(0, 5, page.PaperWidth * 3.78f - 75f, 20),
            Text = "Trang [Page#]/[TotalPages#]",
            Font = new Font("Arial", 9, FontStyle.Regular),
            HorzAlign = HorzAlign.Center,
            VertAlign = VertAlign.Center
        };
        pageFooter.Objects.Add(fPage);

        string? dir = Path.GetDirectoryName(filePath);
        if (!string.IsNullOrEmpty(dir) && !Directory.Exists(dir))
        {
            Directory.CreateDirectory(dir);
        }

        report.Save(filePath);
    }
}
