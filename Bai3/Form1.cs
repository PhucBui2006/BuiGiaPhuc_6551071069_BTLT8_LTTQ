using System.Diagnostics;
using System.Drawing.Printing;
using bai73.Models;
using ClosedXML.Excel;
using Microsoft.EntityFrameworkCore;

namespace bai73;

public partial class Form1 : Form
{
    private sealed class LopHienThi
    {
        public string? MaLop { get; init; }
        public string HienThi { get; init; } = "";
        public override string ToString() => HienThi;
    }

    private sealed record SinhVienDong(
        string MaSV, string HoTen, DateTime NgaySinh, string GioiTinh, string MaLop, string TenLop);

    private readonly PhieuDiemPrinter _printer = new();
    private readonly List<SinhVienDong> _danhSachSv = new();
    private List<DiemMon> _diemHienTai = new();
    private string? _maSvDangChon;
    private bool _dangNap;

    public Form1()
    {
        InitializeComponent();
    }

    private string? MaLopDangChon => (lstLop.SelectedItem as LopHienThi)?.MaLop;

    private async void Form1_Load(object sender, EventArgs e)
    {
        await LoadLopAsync();
        await LoadSinhVienAsync();
    }

    // ================= ĐỔ DỮ LIÊU LÊN LISTBOX LỚP / COMBOBOX LỚP =================

    private async Task LoadLopAsync()
    {
        try
        {
            using var db = new QuanLyDiemContext();
            var lops = await db.Lops.OrderBy(l => l.MaLop).ToListAsync();

            var tatCa = new List<LopHienThi> { new() { MaLop = null, HienThi = "Tất cả" } };
            var dsl = lops.Select(l => new LopHienThi { MaLop = l.MaLop, HienThi = $"{l.MaLop} - {l.TenLop}" }).ToList();

            _dangNap = true;
            lstLop.DataSource = tatCa.Concat(dsl).ToList();
            lstLop.SelectedIndex = 0;
            cboLop.DataSource = dsl;
            if (cboLop.Items.Count > 0) cboLop.SelectedIndex = 0;
            _dangNap = false;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Không tải được danh sách lớp: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // ================= DANH SÁCH SINH VIÊN (LỌC THEO LỚP + TÌM KIẾM) =================

    private async Task LoadSinhVienAsync()
    {
        try
        {
            var maLop = MaLopDangChon;
            var tuKhoa = txtTim.Text.Trim();

            using var db = new QuanLyDiemContext();
            var query = from sv in db.SinhViens
                        join l in db.Lops on sv.MaLop equals l.MaLop
                        select new { sv.MaSV, sv.HoTen, sv.NgaySinh, sv.GioiTinh, sv.MaLop, l.TenLop };

            if (maLop is not null)
                query = query.Where(x => x.MaLop == maLop);

            if (tuKhoa.Length > 0)
                query = query.Where(x =>
                    EF.Functions.Like(x.MaSV, "%" + tuKhoa + "%") ||
                    EF.Functions.Like(x.HoTen, "%" + tuKhoa + "%"));

            var ketQua = await query.OrderBy(x => x.MaSV).ToListAsync();

            _danhSachSv.Clear();
            _danhSachSv.AddRange(ketQua.Select(x =>
                new SinhVienDong(x.MaSV, x.HoTen, x.NgaySinh, x.GioiTinh, x.MaLop, x.TenLop)));

            _dangNap = true;
            dgvSinhVien.Rows.Clear();
            for (int i = 0; i < _danhSachSv.Count; i++)
            {
                var sv = _danhSachSv[i];
                int idx = dgvSinhVien.Rows.Add(sv.MaSV, sv.HoTen, sv.NgaySinh.ToString("dd/MM/yyyy"), sv.GioiTinh, sv.TenLop);
                dgvSinhVien.Rows[idx].Tag = i;
            }

            int chon = _danhSachSv.FindIndex(x => x.MaSV == _maSvDangChon);
            if (chon < 0) chon = _danhSachSv.Count > 0 ? 0 : -1;
            if (chon >= 0) dgvSinhVien.Rows[chon].Selected = true;
            _dangNap = false;

            if (chon >= 0)
            {
                DienThongTinSinhVien(_danhSachSv[chon]);
                await LoadDiemAsync(_danhSachSv[chon].MaSV);
            }
            else
            {
                _maSvDangChon = null;
                XoaTrangDiem();
            }
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Không tải được danh sách sinh viên: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void lstLop_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_dangNap) return;
        await LoadSinhVienAsync();
    }

    private async void txtTim_TextChanged(object sender, EventArgs e)
    {
        if (_dangNap) return;
        await LoadSinhVienAsync();
    }

    private async void dgvSinhVien_SelectionChanged(object sender, EventArgs e)
    {
        if (_dangNap || dgvSinhVien.CurrentRow is null) return;
        if (dgvSinhVien.CurrentRow.Tag is not int index || index >= _danhSachSv.Count) return;

        var sv = _danhSachSv[index];
        DienThongTinSinhVien(sv);
        await LoadDiemAsync(sv.MaSV);
    }

    private void DienThongTinSinhVien(SinhVienDong sv)
    {
        _dangNap = true;
        txtMaSV.Text = sv.MaSV;
        txtHoTen.Text = sv.HoTen;
        dtpNgaySinh.Value = sv.NgaySinh;
        if (sv.GioiTinh == "Nam") radNam.Checked = true; else radNu.Checked = true;
        for (int i = 0; i < cboLop.Items.Count; i++)
        {
            if (cboLop.Items[i] is LopHienThi l && l.MaLop == sv.MaLop)
            {
                cboLop.SelectedIndex = i;
                break;
            }
        }
        _dangNap = false;
    }

    // ================= BẢNG ĐIỂM + ĐIỂM TB + XẾP LOẠI =================

    private async Task LoadDiemAsync(string maSv)
    {
        try
        {
            using var db = new QuanLyDiemContext();
            _diemHienTai = await db.DiemMons
                .Where(d => d.MaSV == maSv)
                .OrderBy(d => d.MaDiem)
                .ToListAsync();
            _maSvDangChon = maSv;
            HienThiDiem();
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Không tải được điểm của sinh viên: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void HienThiDiem()
    {
        _dangNap = true;
        dgvDiem.Rows.Clear();
        for (int i = 0; i < _diemHienTai.Count; i++)
        {
            var d = _diemHienTai[i];
            int idx = dgvDiem.Rows.Add(i + 1, d.TenMon, d.SoTinChi, d.Diem);
            dgvDiem.Rows[idx].Tag = d.MaDiem;
        }
        _dangNap = false;

        int tongTinChi = _diemHienTai.Sum(d => d.SoTinChi);
        lblTongTinChi.Text = $"Tổng tín chỉ: {tongTinChi}";

        if (tongTinChi == 0)
        {
            lblDiemTB.Text = "Điểm TB: Chưa có điểm";
            lblXepLoai.Text = "Xếp loại: Chưa có điểm";
        }
        else
        {
            decimal diemTb = TinhDiemTrungBinh();
            lblDiemTB.Text = $"Điểm TB: {diemTb:0.00}";
            lblXepLoai.Text = $"Xếp loại: {XepLoai(diemTb)}";
        }

        txtTenMon.Clear();
        nudSoTinChi.Value = 1;
        nudDiem.Value = 0;
    }

    private void XoaTrangDiem()
    {
        _diemHienTai = new List<DiemMon>();
        _dangNap = true;
        dgvDiem.Rows.Clear();
        _dangNap = false;
        lblTongTinChi.Text = "Tổng tín chỉ: 0";
        lblDiemTB.Text = "Điểm TB: Chưa có điểm";
        lblXepLoai.Text = "Xếp loại: Chưa có điểm";
    }

    private decimal TinhDiemTrungBinh()
    {
        int tongTinChi = _diemHienTai.Sum(d => d.SoTinChi);
        if (tongTinChi == 0) return 0m;
        return Math.Round(_diemHienTai.Sum(d => d.Diem * d.SoTinChi) / tongTinChi, 2);
    }

    private static string XepLoai(decimal diemTb) => diemTb switch
    {
        >= 8.0m => "Giỏi",
        >= 7.0m => "Khá",
        >= 5.0m => "Trung bình",
        _ => "Yếu"
    };

    private void dgvDiem_SelectionChanged(object sender, EventArgs e)
    {
        if (_dangNap || dgvDiem.CurrentRow is null) return;
        if (dgvDiem.CurrentRow.Tag is not int maDiem) return;

        var d = _diemHienTai.Find(x => x.MaDiem == maDiem);
        if (d is null) return;

        txtTenMon.Text = d.TenMon;
        nudSoTinChi.Value = Math.Min(nudSoTinChi.Maximum, Math.Max(nudSoTinChi.Minimum, d.SoTinChi));
        nudDiem.Value = Math.Min(nudDiem.Maximum, Math.Max(nudDiem.Minimum, d.Diem));
    }

    private int? MaDiemDangChon() => dgvDiem.CurrentRow?.Tag as int?;

    // ================= THÊM / SỬA / XÓA SINH VIÊN =================

    private async void btnThem_Click(object sender, EventArgs e)
    {
        var maSv = txtMaSV.Text.Trim();
        var hoTen = txtHoTen.Text.Trim();

        if (maSv.Length == 0)
        {
            MessageBox.Show(this, "MSSV không được để trống.", "Thiếu thông tin",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtMaSV.Focus();
            return;
        }
        if (hoTen.Length == 0)
        {
            MessageBox.Show(this, "Họ tên không được để trống.", "Thiếu thông tin",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtHoTen.Focus();
            return;
        }
        if (cboLop.SelectedItem is not LopHienThi lopChon || lopChon.MaLop is null)
        {
            MessageBox.Show(this, "Chưa chọn lớp cho sinh viên.", "Thiếu thông tin",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            using var db = new QuanLyDiemContext();
            if (await db.SinhViens.AnyAsync(s => s.MaSV == maSv))
            {
                MessageBox.Show(this, $"MSSV \"{maSv}\" đã tồn tại.", "Trùng khóa chính",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaSV.Focus();
                return;
            }

            db.SinhViens.Add(new SinhVien
            {
                MaSV = maSv,
                HoTen = hoTen,
                NgaySinh = dtpNgaySinh.Value.Date,
                GioiTinh = radNam.Checked ? "Nam" : "Nữ",
                MaLop = lopChon.MaLop
            });
            await db.SaveChangesAsync();

            _maSvDangChon = maSv;
            await LoadSinhVienAsync();
            MessageBox.Show(this, "Thêm sinh viên thành công.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Lỗi khi thêm sinh viên: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnSua_Click(object sender, EventArgs e)
    {
        if (_maSvDangChon is null)
        {
            MessageBox.Show(this, "Chưa chọn sinh viên cần sửa.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        var hoTen = txtHoTen.Text.Trim();
        if (hoTen.Length == 0)
        {
            MessageBox.Show(this, "Họ tên không được để trống.", "Thiếu thông tin",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtHoTen.Focus();
            return;
        }
        if (cboLop.SelectedItem is not LopHienThi lopChon || lopChon.MaLop is null)
        {
            MessageBox.Show(this, "Chưa chọn lớp cho sinh viên.", "Thiếu thông tin",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            using var db = new QuanLyDiemContext();
            var sv = await db.SinhViens.FirstOrDefaultAsync(s => s.MaSV == _maSvDangChon);
            if (sv is null)
            {
                MessageBox.Show(this, "Không tìm thấy sinh viên trong CSDL.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            sv.HoTen = hoTen;
            sv.NgaySinh = dtpNgaySinh.Value.Date;
            sv.GioiTinh = radNam.Checked ? "Nam" : "Nữ";
            sv.MaLop = lopChon.MaLop;
            await db.SaveChangesAsync();

            await LoadSinhVienAsync();
            MessageBox.Show(this, "Cập nhật sinh viên thành công.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Lỗi khi sửa sinh viên: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnXoa_Click(object sender, EventArgs e)
    {
        if (_maSvDangChon is null)
        {
            MessageBox.Show(this, "Chưa chọn sinh viên cần xóa.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        var hoi = MessageBox.Show(this,
            $"Xóa sinh viên {_maSvDangChon} và toàn bộ điểm của sinh viên này?",
            "Xác nhận xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
        if (hoi != DialogResult.Yes) return;

        try
        {
            using var db = new QuanLyDiemContext();
            var dsDiem = await db.DiemMons.Where(d => d.MaSV == _maSvDangChon).ToListAsync();
            db.DiemMons.RemoveRange(dsDiem);

            var sv = await db.SinhViens.FirstOrDefaultAsync(s => s.MaSV == _maSvDangChon);
            if (sv is not null) db.SinhViens.Remove(sv);

            await db.SaveChangesAsync();

            _maSvDangChon = null;
            await LoadSinhVienAsync();
            MessageBox.Show(this, "Đã xóa sinh viên.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Lỗi khi xóa sinh viên: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // ================= THÊM MÔN / SỬA ĐIỂM / XÓA MÔN =================

    private bool KiemTraNhapDiem()
    {
        if (_maSvDangChon is null)
        {
            MessageBox.Show(this, "Chưa chọn sinh viên.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        if (txtTenMon.Text.Trim().Length == 0)
        {
            MessageBox.Show(this, "Tên môn không được để trống.", "Thiếu thông tin",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            txtTenMon.Focus();
            return false;
        }
        return true;
    }

    private async void btnThemMon_Click(object sender, EventArgs e)
    {
        if (!KiemTraNhapDiem()) return;

        try
        {
            using var db = new QuanLyDiemContext();
            db.DiemMons.Add(new DiemMon
            {
                MaSV = _maSvDangChon!,
                TenMon = txtTenMon.Text.Trim(),
                SoTinChi = (int)nudSoTinChi.Value,
                Diem = nudDiem.Value
            });
            await db.SaveChangesAsync();
            await LoadDiemAsync(_maSvDangChon!);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Lỗi khi thêm môn: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnSuaDiem_Click(object sender, EventArgs e)
    {
        if (!KiemTraNhapDiem()) return;
        if (MaDiemDangChon() is not int maDiem)
        {
            MessageBox.Show(this, "Chưa chọn dòng điểm cần sửa.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            using var db = new QuanLyDiemContext();
            var diem = await db.DiemMons.FirstOrDefaultAsync(d => d.MaDiem == maDiem);
            if (diem is null)
            {
                MessageBox.Show(this, "Không tìm thấy dòng điểm trong CSDL.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            diem.TenMon = txtTenMon.Text.Trim();
            diem.SoTinChi = (int)nudSoTinChi.Value;
            diem.Diem = nudDiem.Value;
            await db.SaveChangesAsync();
            await LoadDiemAsync(_maSvDangChon!);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Lỗi khi sửa điểm: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnXoaMon_Click(object sender, EventArgs e)
    {
        if (_maSvDangChon is null)
        {
            MessageBox.Show(this, "Chưa chọn sinh viên.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }
        if (MaDiemDangChon() is not int maDiem)
        {
            MessageBox.Show(this, "Chưa chọn dòng điểm cần xóa.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return;
        }

        try
        {
            using var db = new QuanLyDiemContext();
            var diem = await db.DiemMons.FirstOrDefaultAsync(d => d.MaDiem == maDiem);
            if (diem is not null)
            {
                db.DiemMons.Remove(diem);
                await db.SaveChangesAsync();
            }
            await LoadDiemAsync(_maSvDangChon!);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Lỗi khi xóa môn: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // ================= IN PHIẾU BÁO ĐIỂM (PrintDocument) =================

    private async Task<bool> ChuanBiPhieuInAsync()
    {
        if (_maSvDangChon is null)
        {
            MessageBox.Show(this, "Chưa chọn sinh viên để in phiếu báo điểm.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }
        if (_diemHienTai.Count == 0)
        {
            MessageBox.Show(this, "Sinh viên chưa có điểm môn nào nên không in được phiếu.", "Thông báo",
                MessageBoxButtons.OK, MessageBoxIcon.Warning);
            return false;
        }

        try
        {
            using var db = new QuanLyDiemContext();
            var sv = await (from s in db.SinhViens
                            join l in db.Lops on s.MaLop equals l.MaLop
                            where s.MaSV == _maSvDangChon
                            select new { s.MaSV, s.HoTen, s.NgaySinh, s.GioiTinh, l.TenLop })
                           .FirstOrDefaultAsync();
            if (sv is null)
            {
                MessageBox.Show(this, "Không tìm thấy sinh viên trong CSDL.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            decimal diemTb = TinhDiemTrungBinh();

            _printer.MaSV = sv.MaSV;
            _printer.HoTen = sv.HoTen;
            _printer.NgaySinh = sv.NgaySinh.ToString("dd/MM/yyyy");
            _printer.GioiTinh = sv.GioiTinh;
            _printer.TenLop = sv.TenLop;
            _printer.DiemTrungBinh = diemTb.ToString("0.00");
            _printer.XepLoai = XepLoai(diemTb);
            _printer.BangDiem = _diemHienTai
                .Select((d, i) => new DongDiemIn { Stt = i + 1, TenMon = d.TenMon, SoTinChi = d.SoTinChi, Diem = d.Diem })
                .ToList();
            _printer.Document.DocumentName = $"Phieu bao diem {sv.MaSV}";
            return true;
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Lỗi khi chuẩn bị dữ liệu in: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }
    }

    private async void btnXemTruoc_Click(object sender, EventArgs e)
    {
        if (!await ChuanBiPhieuInAsync()) return;

        try
        {
            using var xemTruoc = new PrintPreviewDialog
            {
                Document = _printer.Document,
                Width = 950,
                Height = 750,
                StartPosition = FormStartPosition.CenterScreen
            };
            xemTruoc.ShowDialog(this);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Lỗi khi xem trước phiếu: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private async void btnIn_Click(object sender, EventArgs e)
    {
        if (!await ChuanBiPhieuInAsync()) return;

        try
        {
            using var hopThoaiIn = new PrintDialog { Document = _printer.Document, UseEXDialog = true };
            if (hopThoaiIn.ShowDialog(this) == DialogResult.OK)
            {
                _printer.Document.Print();
            }
        }
        catch (InvalidPrinterException)
        {
            MessageBox.Show(this,
                "Máy chưa cài máy in. Hãy cài một máy in (ví dụ \"Microsoft Print to PDF\") trong Settings → Bluetooth & devices → Printers & scanners rồi thử lại.",
                "Không tìm thấy máy in", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Lỗi khi in phiếu: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    private void btnCaiDatTrang_Click(object sender, EventArgs e)
    {
        try
        {
            using var caiDat = new PageSetupDialog { Document = _printer.Document };
            caiDat.ShowDialog(this);
        }
        catch (InvalidPrinterException)
        {
            MessageBox.Show(this,
                "Máy chưa cài máy in nên không mở được hộp thoại cài đặt trang.",
                "Không tìm thấy máy in", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Lỗi khi cài đặt trang: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }

    // ================= XUẤT EXCEL ĐIỂM LỚP (ClosedXML) =================

    private async void btnXuatExcel_Click(object sender, EventArgs e)
    {
        var maLop = MaLopDangChon;

        try
        {
            using var db = new QuanLyDiemContext();
            var query = from d in db.DiemMons
                        join s in db.SinhViens on d.MaSV equals s.MaSV
                        join l in db.Lops on s.MaLop equals l.MaLop
                        select new { s.MaSV, s.HoTen, s.MaLop, d.TenMon, d.SoTinChi, d.Diem };

            if (maLop is not null)
                query = query.Where(x => x.MaLop == maLop);

            var duLieu = await query.OrderBy(x => x.MaSV).ThenBy(x => x.TenMon).ToListAsync();
            if (duLieu.Count == 0)
            {
                MessageBox.Show(this, "Không có dòng điểm nào để xuất Excel.", "Thông báo",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            using var luuFile = new SaveFileDialog
            {
                Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                FileName = $"Diem_Lop_{maLop ?? "TatCa"}.xlsx"
            };
            if (luuFile.ShowDialog(this) != DialogResult.OK) return;

            using var wb = new XLWorkbook();
            var ws = wb.Worksheets.Add("Diem " + (maLop ?? "TatCa"));

            string[] tieuDe = { "MSSV", "Họ tên", "Lớp", "Môn học", "Số TC", "Điểm" };
            for (int c = 0; c < tieuDe.Length; c++)
            {
                ws.Cell(1, c + 1).Value = tieuDe[c];
                ws.Cell(1, c + 1).Style.Font.Bold = true;
            }

            int r = 2;
            foreach (var x in duLieu)
            {
                ws.Cell(r, 1).Value = x.MaSV;
                ws.Cell(r, 2).Value = x.HoTen;
                ws.Cell(r, 3).Value = x.MaLop;
                ws.Cell(r, 4).Value = x.TenMon;
                ws.Cell(r, 5).Value = x.SoTinChi;
                ws.Cell(r, 6).Value = (double)x.Diem;
                r++;
            }

            int dongCuoi = r - 1;
            ws.Cell(r, 1).Value = "Điểm trung bình";
            ws.Cell(r, 6).FormulaA1 = $"AVERAGE(F2:F{dongCuoi})";
            ws.Cell(r, 1).Style.Font.Bold = true;
            ws.Cell(r, 6).Style.Font.Bold = true;
            ws.Cell(r, 6).Style.NumberFormat.Format = "0.00";

            ws.Cell(r + 1, 1).Value = "Tổng số tín chỉ";
            ws.Cell(r + 1, 5).FormulaA1 = $"SUM(E2:E{dongCuoi})";
            ws.Cell(r + 1, 1).Style.Font.Bold = true;
            ws.Cell(r + 1, 5).Style.Font.Bold = true;

            ws.Range(2, 5, dongCuoi, 6).Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
            ws.Range(2, 6, dongCuoi, 6).Style.NumberFormat.Format = "0.00";
            ws.SheetView.FreezeRows(1);
            ws.Columns().AdjustToContents();

            wb.SaveAs(luuFile.FileName);

            if (MessageBox.Show(this, "Xuất Excel thành công. Mở file luôn không?", "Hoàn tất",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                Process.Start(new ProcessStartInfo(luuFile.FileName) { UseShellExecute = true });
            }
        }
        catch (IOException)
        {
            MessageBox.Show(this,
                "Không ghi được file vì file này đang mở trong Excel. Hãy đóng file rồi xuất lại.",
                "File đang bị khóa", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        catch (Exception ex)
        {
            MessageBox.Show(this, "Lỗi khi xuất Excel: " + ex.Message, "Lỗi",
                MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}
