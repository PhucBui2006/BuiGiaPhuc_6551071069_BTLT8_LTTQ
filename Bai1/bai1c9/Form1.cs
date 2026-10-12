using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using bai1c9.Models;
using ClosedXML.Excel;
using FastReport;
using FastReport.Export.Pdf;
using Microsoft.EntityFrameworkCore;

namespace bai1c9
{
    public partial class Form1 : Form
    {
        private readonly TiemTapHoaDbContext _context;
        private int? _selectedMaHang = null;
        private bool _isBinding = false;

        public Form1()
        {
            InitializeComponent();
            _context = new TiemTapHoaDbContext();
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            try
            {
                // Ensure report template exists
                EnsureReportTemplate();

                // Setup ComboBoxes
                cboLoaiHang.Items.Clear();
                cboLoaiHang.Items.AddRange(new object[] { "Đồ uống", "Thực phẩm khô", "Gia vị", "Đồ gia dụng" });

                cboDonVi.Items.Clear();
                cboDonVi.Items.AddRange(new object[] { "Chai", "Gói", "Hộp", "Túi" });

                cboTimLoaiHang.Items.Clear();
                cboTimLoaiHang.Items.AddRange(new object[] { "Tất cả các loại", "Đồ uống", "Thực phẩm khô", "Gia vị", "Đồ gia dụng" });
                cboTimLoaiHang.SelectedIndex = 0;

                // Load initial data
                await LoadDataAsync();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi khởi động ứng dụng: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void EnsureReportTemplate()
        {
            try
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory;
                string reportPath = Path.Combine(baseDir, "Reports", "BangGia.frx");
                if (!File.Exists(reportPath))
                {
                    ReportTemplateBuilder.CreateBangGiaTemplate(reportPath);
                }
            }
            catch
            {
                // Ignore fallback creation error if handled later
            }
        }

        private string GetReportPath()
        {
            string baseDir = AppDomain.CurrentDomain.BaseDirectory;
            string reportPath = Path.Combine(baseDir, "Reports", "BangGia.frx");
            if (File.Exists(reportPath))
            {
                return reportPath;
            }

            string currentDir = Directory.GetCurrentDirectory();
            string reportPathCurrent = Path.Combine(currentDir, "Reports", "BangGia.frx");
            if (File.Exists(reportPathCurrent))
            {
                return reportPathCurrent;
            }

            // If not found, create it
            ReportTemplateBuilder.CreateBangGiaTemplate(reportPath);
            return reportPath;
        }

        /// <summary>
        /// Nạp và lọc dữ liệu bất đồng bộ
        /// </summary>
        private async Task LoadDataAsync()
        {
            try
            {
                string keyword = txtTimKiem.Text.Trim();
                string selectedLoai = cboTimLoaiHang.SelectedItem?.ToString() ?? "Tất cả các loại";
                bool chiConKinhDoanh = chkTimConKinhDoanh.Checked;

                var query = _context.HangHoas.AsNoTracking().AsQueryable();

                if (!string.IsNullOrEmpty(keyword))
                {
                    query = query.Where(h => EF.Functions.Like(h.TenHang, $"%{keyword}%"));
                }

                if (!string.IsNullOrEmpty(selectedLoai) && selectedLoai != "Tất cả các loại" && selectedLoai != "Tất cả")
                {
                    query = query.Where(h => h.LoaiHang == selectedLoai);
                }

                if (chiConKinhDoanh)
                {
                    query = query.Where(h => h.ConKinhDoanh);
                }

                var list = await query.OrderBy(h => h.MaHang).ToListAsync();

                _isBinding = true;
                dgvHangHoa.DataSource = list;
                _isBinding = false;

                lblTrangThai.Text = $"Tìm thấy {list.Count} mặt hàng";

                // Re-select if previously selected item is in the list
                if (_selectedMaHang.HasValue)
                {
                    foreach (DataGridViewRow row in dgvHangHoa.Rows)
                    {
                        if (row.DataBoundItem is HangHoa hh && hh.MaHang == _selectedMaHang.Value)
                        {
                            row.Selected = true;
                            dgvHangHoa.CurrentCell = row.Cells[0];
                            break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi tải dữ liệu: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void dgvHangHoa_SelectionChanged(object sender, EventArgs e)
        {
            if (_isBinding) return;
            PopulateControlsFromGrid();
        }

        private void dgvHangHoa_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                PopulateControlsFromGrid();
            }
        }

        private void PopulateControlsFromGrid()
        {
            if (dgvHangHoa.CurrentRow?.DataBoundItem is HangHoa hh)
            {
                _selectedMaHang = hh.MaHang;
                txtTenHang.Text = hh.TenHang;
                cboLoaiHang.SelectedItem = hh.LoaiHang;
                cboDonVi.SelectedItem = hh.DonVi;
                nudGiaBan.Value = hh.GiaBan ?? 0;
                nudSoLuongTon.Value = hh.SoLuongTon ?? 0;
                dtpNgayNhap.Value = hh.NgayNhap ?? DateTime.Today;
                chkConKinhDoanh.Checked = hh.ConKinhDoanh;
            }
        }

        private void ClearInputs()
        {
            _selectedMaHang = null;
            txtTenHang.Clear();
            cboLoaiHang.SelectedIndex = -1;
            cboDonVi.SelectedIndex = -1;
            nudGiaBan.Value = 0;
            nudSoLuongTon.Value = 0;
            dtpNgayNhap.Value = DateTime.Today;
            chkConKinhDoanh.Checked = true;
            dgvHangHoa.ClearSelection();
            txtTenHang.Focus();
        }

        private async void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(txtTenHang.Text))
                {
                    MessageBox.Show("Tên hàng không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenHang.Focus();
                    return;
                }

                if (nudGiaBan.Value <= 0)
                {
                    MessageBox.Show("Giá bán phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nudGiaBan.Focus();
                    return;
                }

                var hangHoa = new HangHoa
                {
                    TenHang = txtTenHang.Text.Trim(),
                    LoaiHang = cboLoaiHang.SelectedItem?.ToString(),
                    DonVi = cboDonVi.SelectedItem?.ToString(),
                    GiaBan = nudGiaBan.Value,
                    SoLuongTon = (int)nudSoLuongTon.Value,
                    NgayNhap = dtpNgayNhap.Value.Date,
                    ConKinhDoanh = chkConKinhDoanh.Checked
                };

                _context.HangHoas.Add(hangHoa);
                await _context.SaveChangesAsync();

                _selectedMaHang = hangHoa.MaHang;
                await LoadDataAsync();

                MessageBox.Show("Thêm hàng hóa mới thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi thêm hàng hóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_selectedMaHang.HasValue)
                {
                    MessageBox.Show("Vui lòng chọn hàng hóa cần sửa từ danh sách!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                if (string.IsNullOrWhiteSpace(txtTenHang.Text))
                {
                    MessageBox.Show("Tên hàng không được để trống!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    txtTenHang.Focus();
                    return;
                }

                if (nudGiaBan.Value <= 0)
                {
                    MessageBox.Show("Giá bán phải lớn hơn 0!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    nudGiaBan.Focus();
                    return;
                }

                var item = await _context.HangHoas.FindAsync(_selectedMaHang.Value);
                if (item == null)
                {
                    MessageBox.Show("Không tìm thấy hàng hóa cần sửa trong cơ sở dữ liệu!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                item.TenHang = txtTenHang.Text.Trim();
                item.LoaiHang = cboLoaiHang.SelectedItem?.ToString();
                item.DonVi = cboDonVi.SelectedItem?.ToString();
                item.GiaBan = nudGiaBan.Value;
                item.SoLuongTon = (int)nudSoLuongTon.Value;
                item.NgayNhap = dtpNgayNhap.Value.Date;
                item.ConKinhDoanh = chkConKinhDoanh.Checked;

                await _context.SaveChangesAsync();
                await LoadDataAsync();

                MessageBox.Show("Cập nhật thông tin hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi sửa hàng hóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                if (!_selectedMaHang.HasValue)
                {
                    MessageBox.Show("Vui lòng chọn hàng hóa cần xóa!", "Cảnh báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                var item = await _context.HangHoas.FindAsync(_selectedMaHang.Value);
                if (item == null)
                {
                    MessageBox.Show("Không tìm thấy hàng hóa cần xóa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                var confirm = MessageBox.Show(
                    $"Bạn có chắc chắn muốn xóa mặt hàng \"{item.TenHang}\" (Mã: {item.MaHang}) không?",
                    "Xác nhận xóa",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (confirm == DialogResult.Yes)
                {
                    _context.HangHoas.Remove(item);
                    await _context.SaveChangesAsync();
                    ClearInputs();
                    await LoadDataAsync();
                    MessageBox.Show("Đã xóa hàng hóa thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xóa hàng hóa: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ClearInputs();
        }

        private void txtTimKiem_TextChanged(object sender, EventArgs e)
        {
            timerSearch.Stop();
            timerSearch.Start();
        }

        private async void cboTimLoaiHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isBinding)
            {
                await LoadDataAsync();
            }
        }

        private async void chkTimConKinhDoanh_CheckedChanged(object sender, EventArgs e)
        {
            if (!_isBinding)
            {
                await LoadDataAsync();
            }
        }

        private async void timerSearch_Tick(object sender, EventArgs e)
        {
            timerSearch.Stop();
            await LoadDataAsync();
        }

        /// <summary>
        /// Tạo và chuẩn bị Report FastReport cho bảng giá
        /// </summary>
        private async Task<Report> PrepareBangGiaReportAsync()
        {
            // Truy vấn các mặt hàng còn kinh doanh bằng EF Core
            var list = await _context.HangHoas.AsNoTracking()
                                     .Where(h => h.ConKinhDoanh)
                                     .OrderBy(h => h.MaHang)
                                     .ToListAsync();

            DataTable dt = new DataTable("HangHoa");
            dt.Columns.Add("MaHang", typeof(int));
            dt.Columns.Add("TenHang", typeof(string));
            dt.Columns.Add("LoaiHang", typeof(string));
            dt.Columns.Add("DonVi", typeof(string));
            dt.Columns.Add("GiaBan", typeof(decimal));
            dt.Columns.Add("SoLuongTon", typeof(int));
            dt.Columns.Add("NgayNhap", typeof(DateTime));
            dt.Columns.Add("ConKinhDoanh", typeof(bool));

            foreach (var h in list)
            {
                dt.Rows.Add(h.MaHang, h.TenHang, h.LoaiHang, h.DonVi, h.GiaBan ?? 0, h.SoLuongTon ?? 0, h.NgayNhap ?? DateTime.Now, h.ConKinhDoanh);
            }

            string reportPath = GetReportPath();
            Report report = new Report();
            report.Load(reportPath);

            // Đăng ký DataTable vào report với tên HangHoa và bật Enabled = true
            report.RegisterData(dt, "HangHoa");
            var ds = report.GetDataSource("HangHoa");
            if (ds != null)
            {
                ds.Enabled = true;
            }

            // Đảm bảo DataBand Data1 liên kết đúng DataSource HangHoa
            if (report.FindObject("Data1") is DataBand dataBand)
            {
                dataBand.DataSource = ds;
            }

            // Truyền tham số NgayIn
            report.SetParameterValue("NgayIn", DateTime.Now.ToString("dd/MM/yyyy"));

            return report;
        }

        private async void btnXemBangGia_Click(object sender, EventArgs e)
        {
            try
            {
                using var report = await PrepareBangGiaReportAsync();
                report.Show();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi mở xem trước bảng giá: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXuatPDF_Click(object sender, EventArgs e)
        {
            try
            {
                using var sfd = new SaveFileDialog
                {
                    Filter = "PDF files (*.pdf)|*.pdf",
                    FileName = "BangGia.pdf",
                    Title = "Lưu file PDF Bảng giá"
                };

                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    using var report = await PrepareBangGiaReportAsync();
                    report.Prepare();

                    using var pdfExport = new PDFExport();
                    report.Export(pdfExport, sfd.FileName);

                    var openFile = MessageBox.Show(
                        "Xuất file PDF thành công! Bạn có muốn mở file vừa xuất không?",
                        "Thông báo",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (openFile == DialogResult.Yes)
                    {
                        Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                    }
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("File PDF đang được mở bởi chương trình khác. Vui lòng đóng file rồi thử lại!\nChi tiết: " + ex.Message, "Lỗi ghi file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất file PDF: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnXuatExcelLuoi_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvHangHoa.Rows.Count == 0)
                {
                    MessageBox.Show("Không có dữ liệu trên lưới để xuất!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using var sfd = new SaveFileDialog
                {
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = "DanhSachHangHoa.xlsx",
                    Title = "Xuất danh sách hàng hóa từ lưới"
                };

                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("HangHoa");

                // Header
                int colCount = dgvHangHoa.Columns.Count;
                for (int col = 0; col < colCount; col++)
                {
                    var headerCell = worksheet.Cell(1, col + 1);
                    headerCell.Value = dgvHangHoa.Columns[col].HeaderText;
                    headerCell.Style.Font.Bold = true;
                    headerCell.Style.Fill.BackgroundColor = XLColor.FromArgb(220, 230, 242);
                    headerCell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    headerCell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                // Data rows
                int rowIdx = 2;
                foreach (DataGridViewRow dgvRow in dgvHangHoa.Rows)
                {
                    for (int col = 0; col < colCount; col++)
                    {
                        var cell = worksheet.Cell(rowIdx, col + 1);
                        var val = dgvRow.Cells[col].Value;
                        string colName = dgvHangHoa.Columns[col].Name;

                        if (colName == "colMaHang")
                        {
                            if (val != null && int.TryParse(val.ToString(), out int ma))
                            {
                                cell.Value = ma;
                                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            }
                        }
                        else if (colName == "colGiaBan")
                        {
                            if (val != null && decimal.TryParse(val.ToString(), out decimal gia))
                            {
                                cell.Value = (double)gia;
                                cell.Style.NumberFormat.Format = "#,##0";
                                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                            }
                        }
                        else if (colName == "colSoLuongTon")
                        {
                            if (val != null && int.TryParse(val.ToString(), out int ton))
                            {
                                cell.Value = ton;
                                cell.Style.NumberFormat.Format = "#,##0";
                                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;
                            }
                        }
                        else if (colName == "colNgayNhap")
                        {
                            if (val is DateTime dt)
                            {
                                cell.Value = dt;
                                cell.Style.DateFormat.Format = "dd/MM/yyyy";
                                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            }
                            else if (val != null && DateTime.TryParse(val.ToString(), out DateTime parsedDt))
                            {
                                cell.Value = parsedDt;
                                cell.Style.DateFormat.Format = "dd/MM/yyyy";
                                cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                            }
                        }
                        else if (colName == "colConKinhDoanh")
                        {
                            bool kd = val != null && Convert.ToBoolean(val);
                            cell.Value = kd ? "Còn bán" : "Ngừng bán";
                            cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        }
                        else
                        {
                            cell.Value = val?.ToString() ?? "";
                        }

                        cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }
                    rowIdx++;
                }

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(sfd.FileName);

                var openFile = MessageBox.Show(
                    "Xuất file Excel thành công! Bạn có muốn mở file không?",
                    "Thông báo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (openFile == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("File Excel đang được mở bởi chương trình khác. Vui lòng đóng file rồi thử lại!\nChi tiết: " + ex.Message, "Lỗi ghi file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel từ lưới: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void btnXuatExcelTonKho_Click(object sender, EventArgs e)
        {
            try
            {
                using var sfd = new SaveFileDialog
                {
                    Filter = "Excel Workbook (*.xlsx)|*.xlsx",
                    FileName = "BaoCaoTonKho.xlsx",
                    Title = "Xuất báo cáo tồn kho"
                };

                if (sfd.ShowDialog() != DialogResult.OK)
                {
                    return;
                }

                // Truy vấn lại bằng EF Core các hàng còn kinh doanh
                var items = await _context.HangHoas.AsNoTracking()
                                          .Where(h => h.ConKinhDoanh)
                                          .OrderBy(h => h.MaHang)
                                          .ToListAsync();

                using var workbook = new XLWorkbook();
                var worksheet = workbook.Worksheets.Add("TonKho");

                // Headers
                string[] headers = { "STT", "Tên hàng", "Đơn vị", "Giá bán", "Số lượng tồn", "Giá trị tồn" };
                for (int i = 0; i < headers.Length; i++)
                {
                    var cell = worksheet.Cell(1, i + 1);
                    cell.Value = headers[i];
                    cell.Style.Font.Bold = true;
                    cell.Style.Fill.BackgroundColor = XLColor.FromArgb(220, 230, 242);
                    cell.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    cell.Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                int dataRowCount = items.Count;
                for (int i = 0; i < dataRowCount; i++)
                {
                    int r = i + 2;
                    var hh = items[i];

                    // Cột A: STT
                    var cSTT = worksheet.Cell(r, 1);
                    cSTT.Value = i + 1;
                    cSTT.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    // Cột B: Tên hàng
                    var cTen = worksheet.Cell(r, 2);
                    cTen.Value = hh.TenHang;

                    // Cột C: Đơn vị
                    var cDonVi = worksheet.Cell(r, 3);
                    cDonVi.Value = hh.DonVi ?? "";
                    cDonVi.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;

                    // Cột D: Giá bán (Số)
                    var cGia = worksheet.Cell(r, 4);
                    cGia.Value = (double)(hh.GiaBan ?? 0);
                    cGia.Style.NumberFormat.Format = "#,##0";
                    cGia.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    // Cột E: Số lượng tồn (Số)
                    var cTon = worksheet.Cell(r, 5);
                    cTon.Value = (double)(hh.SoLuongTon ?? 0);
                    cTon.Style.NumberFormat.Format = "#,##0";
                    cTon.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    // Cột F: Giá trị tồn (Công thức: =D{r}*E{r})
                    var cGiaTri = worksheet.Cell(r, 6);
                    cGiaTri.FormulaA1 = $"=D{r}*E{r}";
                    cGiaTri.Style.NumberFormat.Format = "#,##0";
                    cGiaTri.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                    for (int c = 1; c <= 6; c++)
                    {
                        worksheet.Cell(r, c).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                    }
                }

                // Dòng Tổng dùng công thức SUM
                int totalRow = dataRowCount + 2;
                var cTongLabel = worksheet.Cell(totalRow, 2);
                cTongLabel.Value = "Tổng";
                cTongLabel.Style.Font.Bold = true;

                var cTongVal = worksheet.Cell(totalRow, 6);
                if (dataRowCount > 0)
                {
                    cTongVal.FormulaA1 = $"=SUM(F2:F{totalRow - 1})";
                }
                else
                {
                    cTongVal.Value = 0;
                }
                cTongVal.Style.Font.Bold = true;
                cTongVal.Style.NumberFormat.Format = "#,##0";
                cTongVal.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Right;

                for (int c = 1; c <= 6; c++)
                {
                    worksheet.Cell(totalRow, c).Style.Border.OutsideBorder = XLBorderStyleValues.Thin;
                }

                // Cố định dòng tiêu đề (Freeze)
                worksheet.SheetView.FreezeRows(1);

                // AutoFilter cho vùng dữ liệu (từ dòng 1 đến dòng dữ liệu cuối cùng, không tính dòng Tổng)
                if (dataRowCount > 0)
                {
                    worksheet.Range(1, 1, dataRowCount + 1, 6).SetAutoFilter();
                }

                worksheet.Columns().AdjustToContents();
                workbook.SaveAs(sfd.FileName);

                var openFile = MessageBox.Show(
                    "Xuất file Excel tồn kho thành công! Bạn có muốn mở file không?",
                    "Thông báo",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question);

                if (openFile == DialogResult.Yes)
                {
                    Process.Start(new ProcessStartInfo(sfd.FileName) { UseShellExecute = true });
                }
            }
            catch (IOException ex)
            {
                MessageBox.Show("File Excel đang được mở bởi chương trình khác. Vui lòng đóng file rồi thử lại!\nChi tiết: " + ex.Message, "Lỗi ghi file", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi xuất Excel tồn kho: " + ex.Message, "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            base.OnFormClosing(e);
            _context?.Dispose();
        }
    }
}
