namespace bai1c9
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            
            grpThongTin = new GroupBox();
            lblTenHang = new Label();
            txtTenHang = new TextBox();
            lblLoaiHang = new Label();
            cboLoaiHang = new ComboBox();
            lblDonVi = new Label();
            cboDonVi = new ComboBox();
            lblGiaBan = new Label();
            nudGiaBan = new NumericUpDown();
            lblSoLuongTon = new Label();
            nudSoLuongTon = new NumericUpDown();
            lblNgayNhap = new Label();
            dtpNgayNhap = new DateTimePicker();
            chkConKinhDoanh = new CheckBox();

            grpTimKiem = new GroupBox();
            lblTimKiem = new Label();
            txtTimKiem = new TextBox();
            lblTimLoaiHang = new Label();
            cboTimLoaiHang = new ComboBox();
            chkTimConKinhDoanh = new CheckBox();

            dgvHangHoa = new DataGridView();
            colMaHang = new DataGridViewTextBoxColumn();
            colTenHang = new DataGridViewTextBoxColumn();
            colLoaiHang = new DataGridViewTextBoxColumn();
            colDonVi = new DataGridViewTextBoxColumn();
            colGiaBan = new DataGridViewTextBoxColumn();
            colSoLuongTon = new DataGridViewTextBoxColumn();
            colNgayNhap = new DataGridViewTextBoxColumn();
            colConKinhDoanh = new DataGridViewCheckBoxColumn();

            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            btnLamMoi = new Button();
            btnXemBangGia = new Button();
            btnXuatPDF = new Button();
            btnXuatExcelLuoi = new Button();
            btnXuatExcelTonKho = new Button();

            lblTrangThai = new Label();
            timerSearch = new System.Windows.Forms.Timer(components);

            grpThongTin.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)nudGiaBan).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSoLuongTon).BeginInit();
            grpTimKiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHangHoa).BeginInit();
            SuspendLayout();

            // 
            // grpThongTin
            // 
            grpThongTin.Controls.Add(lblTenHang);
            grpThongTin.Controls.Add(txtTenHang);
            grpThongTin.Controls.Add(lblLoaiHang);
            grpThongTin.Controls.Add(cboLoaiHang);
            grpThongTin.Controls.Add(lblDonVi);
            grpThongTin.Controls.Add(cboDonVi);
            grpThongTin.Controls.Add(lblGiaBan);
            grpThongTin.Controls.Add(nudGiaBan);
            grpThongTin.Controls.Add(lblSoLuongTon);
            grpThongTin.Controls.Add(nudSoLuongTon);
            grpThongTin.Controls.Add(lblNgayNhap);
            grpThongTin.Controls.Add(dtpNgayNhap);
            grpThongTin.Controls.Add(chkConKinhDoanh);
            grpThongTin.Location = new Point(14, 12);
            grpThongTin.Name = "grpThongTin";
            grpThongTin.Size = new Size(570, 155);
            grpThongTin.TabIndex = 0;
            grpThongTin.TabStop = false;
            grpThongTin.Text = "Thông tin hàng hóa";

            // lblTenHang
            lblTenHang.AutoSize = true;
            lblTenHang.Location = new Point(12, 26);
            lblTenHang.Name = "lblTenHang";
            lblTenHang.Size = new Size(59, 15);
            lblTenHang.Text = "Tên hàng:";

            // txtTenHang
            txtTenHang.Location = new Point(85, 23);
            txtTenHang.Name = "txtTenHang";
            txtTenHang.Size = new Size(470, 23);
            txtTenHang.TabIndex = 1;

            // lblLoaiHang
            lblLoaiHang.AutoSize = true;
            lblLoaiHang.Location = new Point(12, 57);
            lblLoaiHang.Name = "lblLoaiHang";
            lblLoaiHang.Size = new Size(62, 15);
            lblLoaiHang.Text = "Loại hàng:";

            // cboLoaiHang
            cboLoaiHang.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLoaiHang.FormattingEnabled = true;
            cboLoaiHang.Location = new Point(85, 54);
            cboLoaiHang.Name = "cboLoaiHang";
            cboLoaiHang.Size = new Size(180, 23);
            cboLoaiHang.TabIndex = 2;

            // lblDonVi
            lblDonVi.AutoSize = true;
            lblDonVi.Location = new Point(12, 88);
            lblDonVi.Name = "lblDonVi";
            lblDonVi.Size = new Size(44, 15);
            lblDonVi.Text = "Đơn vị:";

            // cboDonVi
            cboDonVi.DropDownStyle = ComboBoxStyle.DropDownList;
            cboDonVi.FormattingEnabled = true;
            cboDonVi.Location = new Point(85, 85);
            cboDonVi.Name = "cboDonVi";
            cboDonVi.Size = new Size(180, 23);
            cboDonVi.TabIndex = 3;

            // lblGiaBan
            lblGiaBan.AutoSize = true;
            lblGiaBan.Location = new Point(12, 119);
            lblGiaBan.Name = "lblGiaBan";
            lblGiaBan.Size = new Size(50, 15);
            lblGiaBan.Text = "Giá bán:";

            // nudGiaBan
            nudGiaBan.Increment = new decimal(new int[] { 500, 0, 0, 0 });
            nudGiaBan.Location = new Point(85, 117);
            nudGiaBan.Maximum = new decimal(new int[] { 1000000000, 0, 0, 0 });
            nudGiaBan.Name = "nudGiaBan";
            nudGiaBan.Size = new Size(180, 23);
            nudGiaBan.TabIndex = 4;
            nudGiaBan.ThousandsSeparator = true;

            // lblSoLuongTon
            lblSoLuongTon.AutoSize = true;
            lblSoLuongTon.Location = new Point(285, 57);
            lblSoLuongTon.Name = "lblSoLuongTon";
            lblSoLuongTon.Size = new Size(78, 15);
            lblSoLuongTon.Text = "Số lượng tồn:";

            // nudSoLuongTon
            nudSoLuongTon.Location = new Point(375, 54);
            nudSoLuongTon.Maximum = new decimal(new int[] { 1000000, 0, 0, 0 });
            nudSoLuongTon.Name = "nudSoLuongTon";
            nudSoLuongTon.Size = new Size(180, 23);
            nudSoLuongTon.TabIndex = 5;
            nudSoLuongTon.ThousandsSeparator = true;

            // lblNgayNhap
            lblNgayNhap.AutoSize = true;
            lblNgayNhap.Location = new Point(285, 88);
            lblNgayNhap.Name = "lblNgayNhap";
            lblNgayNhap.Size = new Size(68, 15);
            lblNgayNhap.Text = "Ngày nhập:";

            // dtpNgayNhap
            dtpNgayNhap.CustomFormat = "dd/MM/yyyy";
            dtpNgayNhap.Format = DateTimePickerFormat.Custom;
            dtpNgayNhap.Location = new Point(375, 85);
            dtpNgayNhap.Name = "dtpNgayNhap";
            dtpNgayNhap.Size = new Size(180, 23);
            dtpNgayNhap.TabIndex = 6;

            // chkConKinhDoanh
            chkConKinhDoanh.AutoSize = true;
            chkConKinhDoanh.Checked = true;
            chkConKinhDoanh.CheckState = CheckState.Checked;
            chkConKinhDoanh.Location = new Point(375, 118);
            chkConKinhDoanh.Name = "chkConKinhDoanh";
            chkConKinhDoanh.Size = new Size(110, 19);
            chkConKinhDoanh.TabIndex = 7;
            chkConKinhDoanh.Text = "Còn kinh doanh";
            chkConKinhDoanh.UseVisualStyleBackColor = true;

            // 
            // grpTimKiem
            // 
            grpTimKiem.Controls.Add(lblTimKiem);
            grpTimKiem.Controls.Add(txtTimKiem);
            grpTimKiem.Controls.Add(lblTimLoaiHang);
            grpTimKiem.Controls.Add(cboTimLoaiHang);
            grpTimKiem.Controls.Add(chkTimConKinhDoanh);
            grpTimKiem.Location = new Point(595, 12);
            grpTimKiem.Name = "grpTimKiem";
            grpTimKiem.Size = new Size(335, 155);
            grpTimKiem.TabIndex = 1;
            grpTimKiem.TabStop = false;
            grpTimKiem.Text = "Tìm kiếm";

            // lblTimKiem
            lblTimKiem.AutoSize = true;
            lblTimKiem.Location = new Point(15, 26);
            lblTimKiem.Name = "lblTimKiem";
            lblTimKiem.Size = new Size(59, 15);
            lblTimKiem.Text = "Tên hàng:";

            // txtTimKiem
            txtTimKiem.Location = new Point(85, 23);
            txtTimKiem.Name = "txtTimKiem";
            txtTimKiem.PlaceholderText = "Nhập tên hàng cần tìm...";
            txtTimKiem.Size = new Size(235, 23);
            txtTimKiem.TabIndex = 8;
            txtTimKiem.TextChanged += txtTimKiem_TextChanged;

            // lblTimLoaiHang
            lblTimLoaiHang.AutoSize = true;
            lblTimLoaiHang.Location = new Point(15, 60);
            lblTimLoaiHang.Name = "lblTimLoaiHang";
            lblTimLoaiHang.Size = new Size(62, 15);
            lblTimLoaiHang.Text = "Loại hàng:";

            // cboTimLoaiHang
            cboTimLoaiHang.DropDownStyle = ComboBoxStyle.DropDownList;
            cboTimLoaiHang.FormattingEnabled = true;
            cboTimLoaiHang.Location = new Point(85, 57);
            cboTimLoaiHang.Name = "cboTimLoaiHang";
            cboTimLoaiHang.Size = new Size(235, 23);
            cboTimLoaiHang.TabIndex = 9;
            cboTimLoaiHang.SelectedIndexChanged += cboTimLoaiHang_SelectedIndexChanged;

            // chkTimConKinhDoanh
            chkTimConKinhDoanh.AutoSize = true;
            chkTimConKinhDoanh.Location = new Point(15, 100);
            chkTimConKinhDoanh.Name = "chkTimConKinhDoanh";
            chkTimConKinhDoanh.Size = new Size(181, 19);
            chkTimConKinhDoanh.TabIndex = 10;
            chkTimConKinhDoanh.Text = "Chỉ hiện hàng còn kinh doanh";
            chkTimConKinhDoanh.UseVisualStyleBackColor = true;
            chkTimConKinhDoanh.CheckedChanged += chkTimConKinhDoanh_CheckedChanged;

            // 
            // dgvHangHoa
            // 
            dgvHangHoa.AllowUserToAddRows = false;
            dgvHangHoa.AllowUserToDeleteRows = false;
            dgvHangHoa.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvHangHoa.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvHangHoa.BackgroundColor = SystemColors.Window;
            dgvHangHoa.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvHangHoa.Columns.AddRange(new DataGridViewColumn[] {
                colMaHang,
                colTenHang,
                colLoaiHang,
                colDonVi,
                colGiaBan,
                colSoLuongTon,
                colNgayNhap,
                colConKinhDoanh
            });
            dgvHangHoa.Location = new Point(14, 180);
            dgvHangHoa.MultiSelect = false;
            dgvHangHoa.Name = "dgvHangHoa";
            dgvHangHoa.ReadOnly = true;
            dgvHangHoa.RowHeadersWidth = 40;
            dgvHangHoa.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvHangHoa.Size = new Size(916, 320);
            dgvHangHoa.TabIndex = 11;
            dgvHangHoa.CellClick += dgvHangHoa_CellClick;
            dgvHangHoa.SelectionChanged += dgvHangHoa_SelectionChanged;

            // colMaHang
            colMaHang.DataPropertyName = "MaHang";
            colMaHang.FillWeight = 8F;
            colMaHang.HeaderText = "Mã";
            colMaHang.Name = "colMaHang";
            colMaHang.ReadOnly = true;

            // colTenHang
            colTenHang.DataPropertyName = "TenHang";
            colTenHang.FillWeight = 28F;
            colTenHang.HeaderText = "Tên hàng";
            colTenHang.Name = "colTenHang";
            colTenHang.ReadOnly = true;

            // colLoaiHang
            colLoaiHang.DataPropertyName = "LoaiHang";
            colLoaiHang.FillWeight = 15F;
            colLoaiHang.HeaderText = "Loại hàng";
            colLoaiHang.Name = "colLoaiHang";
            colLoaiHang.ReadOnly = true;

            // colDonVi
            colDonVi.DataPropertyName = "DonVi";
            colDonVi.FillWeight = 10F;
            colDonVi.HeaderText = "Đơn vị";
            colDonVi.Name = "colDonVi";
            colDonVi.ReadOnly = true;

            // colGiaBan
            colGiaBan.DataPropertyName = "GiaBan";
            colGiaBan.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colGiaBan.DefaultCellStyle.Format = "N0";
            colGiaBan.FillWeight = 14F;
            colGiaBan.HeaderText = "Giá bán";
            colGiaBan.Name = "colGiaBan";
            colGiaBan.ReadOnly = true;

            // colSoLuongTon
            colSoLuongTon.DataPropertyName = "SoLuongTon";
            colSoLuongTon.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
            colSoLuongTon.DefaultCellStyle.Format = "N0";
            colSoLuongTon.FillWeight = 10F;
            colSoLuongTon.HeaderText = "Tồn";
            colSoLuongTon.Name = "colSoLuongTon";
            colSoLuongTon.ReadOnly = true;

            // colNgayNhap
            colNgayNhap.DataPropertyName = "NgayNhap";
            colNgayNhap.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNgayNhap.DefaultCellStyle.Format = "dd/MM/yyyy";
            colNgayNhap.FillWeight = 15F;
            colNgayNhap.HeaderText = "Ngày nhập";
            colNgayNhap.Name = "colNgayNhap";
            colNgayNhap.ReadOnly = true;

            // colConKinhDoanh
            colConKinhDoanh.DataPropertyName = "ConKinhDoanh";
            colConKinhDoanh.FillWeight = 12F;
            colConKinhDoanh.HeaderText = "Kinh doanh";
            colConKinhDoanh.Name = "colConKinhDoanh";
            colConKinhDoanh.ReadOnly = true;

            // 
            // Buttons
            // 
            btnThem.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnThem.Location = new Point(14, 515);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(80, 32);
            btnThem.TabIndex = 12;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;

            btnSua.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnSua.Location = new Point(100, 515);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(80, 32);
            btnSua.TabIndex = 13;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;

            btnXoa.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXoa.Location = new Point(186, 515);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(80, 32);
            btnXoa.TabIndex = 14;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;

            btnLamMoi.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnLamMoi.Location = new Point(272, 515);
            btnLamMoi.Name = "btnLamMoi";
            btnLamMoi.Size = new Size(80, 32);
            btnLamMoi.TabIndex = 15;
            btnLamMoi.Text = "Làm mới";
            btnLamMoi.UseVisualStyleBackColor = true;
            btnLamMoi.Click += btnLamMoi_Click;

            btnXemBangGia.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXemBangGia.Location = new Point(370, 515);
            btnXemBangGia.Name = "btnXemBangGia";
            btnXemBangGia.Size = new Size(110, 32);
            btnXemBangGia.TabIndex = 16;
            btnXemBangGia.Text = "Xem bảng giá";
            btnXemBangGia.UseVisualStyleBackColor = true;
            btnXemBangGia.Click += btnXemBangGia_Click;

            btnXuatPDF.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXuatPDF.Location = new Point(486, 515);
            btnXuatPDF.Name = "btnXuatPDF";
            btnXuatPDF.Size = new Size(95, 32);
            btnXuatPDF.TabIndex = 17;
            btnXuatPDF.Text = "Xuất PDF";
            btnXuatPDF.UseVisualStyleBackColor = true;
            btnXuatPDF.Click += btnXuatPDF_Click;

            btnXuatExcelLuoi.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXuatExcelLuoi.Location = new Point(595, 515);
            btnXuatExcelLuoi.Name = "btnXuatExcelLuoi";
            btnXuatExcelLuoi.Size = new Size(125, 32);
            btnXuatExcelLuoi.TabIndex = 18;
            btnXuatExcelLuoi.Text = "Xuất Excel (lưới)";
            btnXuatExcelLuoi.UseVisualStyleBackColor = true;
            btnXuatExcelLuoi.Click += btnXuatExcelLuoi_Click;

            btnXuatExcelTonKho.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXuatExcelTonKho.Location = new Point(726, 515);
            btnXuatExcelTonKho.Name = "btnXuatExcelTonKho";
            btnXuatExcelTonKho.Size = new Size(135, 32);
            btnXuatExcelTonKho.TabIndex = 19;
            btnXuatExcelTonKho.Text = "Xuất Excel tồn kho";
            btnXuatExcelTonKho.UseVisualStyleBackColor = true;
            btnXuatExcelTonKho.Click += btnXuatExcelTonKho_Click;

            // 
            // lblTrangThai
            // 
            lblTrangThai.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            lblTrangThai.AutoSize = true;
            lblTrangThai.Location = new Point(14, 560);
            lblTrangThai.Name = "lblTrangThai";
            lblTrangThai.Size = new Size(116, 15);
            lblTrangThai.Text = "Tìm thấy 0 mặt hàng";

            // 
            // timerSearch
            // 
            timerSearch.Interval = 350;
            timerSearch.Tick += timerSearch_Tick;

            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(944, 591);
            Controls.Add(lblTrangThai);
            Controls.Add(btnXuatExcelTonKho);
            Controls.Add(btnXuatExcelLuoi);
            Controls.Add(btnXuatPDF);
            Controls.Add(btnXemBangGia);
            Controls.Add(btnLamMoi);
            Controls.Add(btnXoa);
            Controls.Add(btnSua);
            Controls.Add(btnThem);
            Controls.Add(dgvHangHoa);
            Controls.Add(grpTimKiem);
            Controls.Add(grpThongTin);
            MinimumSize = new Size(960, 630);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý hàng hóa - Tạp hóa UTC2";
            Load += Form1_Load;

            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)nudGiaBan).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSoLuongTon).EndInit();
            grpTimKiem.ResumeLayout(false);
            grpTimKiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvHangHoa).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private GroupBox grpThongTin;
        private Label lblTenHang;
        private TextBox txtTenHang;
        private Label lblLoaiHang;
        private ComboBox cboLoaiHang;
        private Label lblDonVi;
        private ComboBox cboDonVi;
        private Label lblGiaBan;
        private NumericUpDown nudGiaBan;
        private Label lblSoLuongTon;
        private NumericUpDown nudSoLuongTon;
        private Label lblNgayNhap;
        private DateTimePicker dtpNgayNhap;
        private CheckBox chkConKinhDoanh;

        private GroupBox grpTimKiem;
        private Label lblTimKiem;
        private TextBox txtTimKiem;
        private Label lblTimLoaiHang;
        private ComboBox cboTimLoaiHang;
        private CheckBox chkTimConKinhDoanh;

        private DataGridView dgvHangHoa;
        private DataGridViewTextBoxColumn colMaHang;
        private DataGridViewTextBoxColumn colTenHang;
        private DataGridViewTextBoxColumn colLoaiHang;
        private DataGridViewTextBoxColumn colDonVi;
        private DataGridViewTextBoxColumn colGiaBan;
        private DataGridViewTextBoxColumn colSoLuongTon;
        private DataGridViewTextBoxColumn colNgayNhap;
        private DataGridViewCheckBoxColumn colConKinhDoanh;

        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private Button btnLamMoi;
        private Button btnXemBangGia;
        private Button btnXuatPDF;
        private Button btnXuatExcelLuoi;
        private Button btnXuatExcelTonKho;

        private Label lblTrangThai;
        private System.Windows.Forms.Timer timerSearch;
    }
}
