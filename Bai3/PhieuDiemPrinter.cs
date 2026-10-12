using System.Drawing.Printing;

namespace bai73;

public class DongDiemIn
{
    public int Stt { get; set; }
    public string TenMon { get; set; } = "";
    public int SoTinChi { get; set; }
    public decimal Diem { get; set; }
}

/// <summary>
/// In phiếu báo điểm bằng PrintDocument.
/// Mọi tọa độ đều tính theo e.MarginBounds nên đổi khổ giấy (dọc/ngang) bản in vẫn hợp lệ.
/// </summary>
public class PhieuDiemPrinter
{
    private const int ChieuCaoDong = 24;        // 1/100 inch
    private const int ChieuCaoTieuDe = 185;     // tên khoa + tiêu đề + thông tin SV
    private const int ChieuCaoTongKet = 120;    // điểm TB + xếp loại
    private const int ChieuCaoKyTen = 80;       // hai chữ ký
    private const int ChieuCaoChanTrang = 25;   // "Trang x/y"

    public string TenKhoa { get; set; } = "KHOA CÔNG NGHỆ THÔNG TIN";
    public string MaSV { get; set; } = "";
    public string HoTen { get; set; } = "";
    public string NgaySinh { get; set; } = "";
    public string GioiTinh { get; set; } = "";
    public string TenLop { get; set; } = "";
    public List<DongDiemIn> BangDiem { get; set; } = new();
    public string DiemTrungBinh { get; set; } = "";
    public string XepLoai { get; set; } = "";

    public PrintDocument Document { get; }

    private int _trangHienTai;
    private int _tongSoTrang;
    private int _dongTiepTheo;
    private int _soDongMoiTrang;

    public PhieuDiemPrinter()
    {
        Document = new PrintDocument();
        Document.DocumentName = "Phieu bao diem";
        Document.BeginPrint += Document_BeginPrint;
        Document.PrintPage += Document_PrintPage;
    }

    private void Document_BeginPrint(object? sender, PrintEventArgs e)
    {
        var page = Document.DefaultPageSettings;
        int chieuRong = page.Bounds.Width - page.Margins.Left - page.Margins.Right;
        int chieuCao = page.Bounds.Height - page.Margins.Top - page.Margins.Bottom;

        _soDongMoiTrang = Math.Max(1,
            (chieuCao - ChieuCaoTieuDe - ChieuCaoTongKet - ChieuCaoKyTen - ChieuCaoChanTrang) / ChieuCaoDong);
        _tongSoTrang = Math.Max(1, (int)Math.Ceiling(BangDiem.Count / (double)_soDongMoiTrang));
        _trangHienTai = 0;
        _dongTiepTheo = 0;
    }

    private void Document_PrintPage(object? sender, PrintPageEventArgs e)
    {
        _trangHienTai++;
        var g = e.Graphics!;
        var b = e.MarginBounds;

        using var fontKhoa = new Font("Times New Roman", 12, FontStyle.Bold);
        using var fontTieuDe = new Font("Times New Roman", 18, FontStyle.Bold);
        using var fontThongTin = new Font("Times New Roman", 11);
        using var fontTieuDeCot = new Font("Times New Roman", 11, FontStyle.Bold);
        using var fontTongKet = new Font("Times New Roman", 12, FontStyle.Bold);

        using var sfTrai = new StringFormat { Alignment = StringAlignment.Near, LineAlignment = StringAlignment.Center };
        using var sfGiua = new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
        using var sfPhai = new StringFormat { Alignment = StringAlignment.Far, LineAlignment = StringAlignment.Center };

        float y = b.Top;

        g.DrawString(TenKhoa, fontKhoa, Brushes.Black, b.Left, y, sfTrai);
        y += 45;
        g.DrawString("PHIẾU BÁO ĐIỂM", fontTieuDe, Brushes.Black,
            new RectangleF(b.Left, y, b.Width, 35), sfGiua);
        y += 50;

        g.DrawString($"MSSV: {MaSV}", fontThongTin, Brushes.Black, b.Left, y, sfTrai);
        g.DrawString($"Họ tên: {HoTen}", fontThongTin, Brushes.Black, b.Left + b.Width / 2f, y, sfTrai);
        y += 25;
        g.DrawString($"Ngày sinh: {NgaySinh}", fontThongTin, Brushes.Black, b.Left, y, sfTrai);
        g.DrawString($"Giới tính: {GioiTinh}          Lớp: {TenLop}", fontThongTin, Brushes.Black,
            b.Left + b.Width / 2f, y, sfTrai);
        y += 40;

        // Độ rộng 4 cột tính theo tỷ lệ trên khổ giấy đang dùng
        int[] w =
        {
            (int)(b.Width * 0.08f),
            (int)(b.Width * 0.56f),
            (int)(b.Width * 0.16f),
            b.Width - (int)(b.Width * 0.08f) - (int)(b.Width * 0.56f) - (int)(b.Width * 0.16f)
        };

        // Dòng tiêu đề cột
        VeKhungDong(g, b.Left, y, w);
        float x = b.Left;
        string[] tieuDe = { "STT", "Môn học", "Số TC", "Điểm" };
        var sfCot = new[] { sfGiua, sfTrai, sfGiua, sfGiua };
        for (int i = 0; i < 4; i++)
        {
            g.DrawString(tieuDe[i], fontTieuDeCot, Brushes.Black, new RectangleF(x + 5, y, w[i] - 10, ChieuCaoDong), sfCot[i]);
            x += w[i];
        }
        y += ChieuCaoDong;

        // Các dòng điểm của trang này
        int cuoiTrang = Math.Min(_dongTiepTheo + _soDongMoiTrang, BangDiem.Count);
        while (_dongTiepTheo < cuoiTrang)
        {
            var d = BangDiem[_dongTiepTheo];
            VeKhungDong(g, b.Left, y, w);
            x = b.Left;
            string[] giaTri = { d.Stt.ToString(), d.TenMon, d.SoTinChi.ToString(), d.Diem.ToString("0.00") };
            for (int i = 0; i < 4; i++)
            {
                g.DrawString(giaTri[i], fontThongTin, Brushes.Black, new RectangleF(x + 5, y, w[i] - 10, ChieuCaoDong), sfCot[i]);
                x += w[i];
            }
            y += ChieuCaoDong;
            _dongTiepTheo++;
        }

        bool laTrangCuoi = _dongTiepTheo >= BangDiem.Count;
        if (laTrangCuoi)
        {
            y += 20;
            g.DrawString($"Điểm trung bình: {DiemTrungBinh}     -     Xếp loại: {XepLoai}",
                fontTongKet, Brushes.Black, new RectangleF(b.Left, y, b.Width, 30), sfGiua);
            y += 70;

            g.DrawString("Giáo vụ khoa", fontTieuDeCot, Brushes.Black,
                new RectangleF(b.Left, y, b.Width / 2f, 25), sfGiua);
            g.DrawString("Sinh viên", fontTieuDeCot, Brushes.Black,
                new RectangleF(b.Left + b.Width / 2f, y, b.Width / 2f, 25), sfGiua);
            y += 28;
            g.DrawString("(Ký và ghi rõ họ tên)", fontThongTin, Brushes.Black,
                new RectangleF(b.Left, y, b.Width / 2f, 22), sfGiua);
            g.DrawString("(Ký và ghi rõ họ tên)", fontThongTin, Brushes.Black,
                new RectangleF(b.Left + b.Width / 2f, y, b.Width / 2f, 22), sfGiua);
        }

        g.DrawString($"Trang {_trangHienTai}/{_tongSoTrang}", fontThongTin, Brushes.Black,
            new RectangleF(b.Left, b.Bottom - ChieuCaoChanTrang + 6, b.Width, ChieuCaoChanTrang), sfGiua);

        e.HasMorePages = !laTrangCuoi;
    }

    private static void VeKhungDong(Graphics g, int left, float top, int[] widths)
    {
        using var but = new Pen(Color.Black, 1);
        float x = left;
        g.DrawRectangle(but, left, top, widths.Sum(), ChieuCaoDong);
        for (int i = 0; i < widths.Length - 1; i++)
        {
            x += widths[i];
            g.DrawLine(but, x, top, x, top + ChieuCaoDong);
        }
    }
}
