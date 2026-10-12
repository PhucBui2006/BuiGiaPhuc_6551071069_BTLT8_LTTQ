namespace bai73
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle1 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle2 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle3 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            lblLop = new Label();
            lstLop = new ListBox();
            lblTim = new Label();
            txtTim = new TextBox();
            dgvSinhVien = new DataGridView();
            colMaSV = new DataGridViewTextBoxColumn();
            colHoTen = new DataGridViewTextBoxColumn();
            colNgaySinh = new DataGridViewTextBoxColumn();
            colGioiTinh = new DataGridViewTextBoxColumn();
            colTenLop = new DataGridViewTextBoxColumn();
            grpThongTin = new GroupBox();
            lblMaSV = new Label();
            txtMaSV = new TextBox();
            lblHoTen = new Label();
            txtHoTen = new TextBox();
            lblNgaySinh = new Label();
            dtpNgaySinh = new DateTimePicker();
            lblGioiTinh = new Label();
            radNam = new RadioButton();
            radNu = new RadioButton();
            lblLopSv = new Label();
            cboLop = new ComboBox();
            btnThem = new Button();
            btnSua = new Button();
            btnXoa = new Button();
            grpDiem = new GroupBox();
            dgvDiem = new DataGridView();
            colStt = new DataGridViewTextBoxColumn();
            colTenMon = new DataGridViewTextBoxColumn();
            colSoTinChi = new DataGridViewTextBoxColumn();
            colDiem = new DataGridViewTextBoxColumn();
            lblTenMon = new Label();
            txtTenMon = new TextBox();
            lblSoTinChi = new Label();
            nudSoTinChi = new NumericUpDown();
            lblDiem = new Label();
            nudDiem = new NumericUpDown();
            btnThemMon = new Button();
            btnSuaDiem = new Button();
            btnXoaMon = new Button();
            lblTongTinChi = new Label();
            lblDiemTB = new Label();
            lblXepLoai = new Label();
            btnXemTruoc = new Button();
            btnIn = new Button();
            btnCaiDatTrang = new Button();
            btnXuatExcel = new Button();
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).BeginInit();
            grpThongTin.SuspendLayout();
            grpDiem.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDiem).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudSoTinChi).BeginInit();
            ((System.ComponentModel.ISupportInitialize)nudDiem).BeginInit();
            SuspendLayout();
            // 
            // lblLop
            // 
            lblLop.AutoSize = true;
            lblLop.Location = new Point(12, 12);
            lblLop.Name = "lblLop";
            lblLop.Size = new Size(31, 15);
            lblLop.TabIndex = 0;
            lblLop.Text = "Lớp";
            // 
            // lstLop
            // 
            lstLop.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            lstLop.FormattingEnabled = true;
            lstLop.ItemHeight = 15;
            lstLop.Location = new Point(12, 34);
            lstLop.Name = "lstLop";
            lstLop.Size = new Size(140, 274);
            lstLop.TabIndex = 1;
            lstLop.SelectedIndexChanged += lstLop_SelectedIndexChanged;
            // 
            // lblTim
            // 
            lblTim.AutoSize = true;
            lblTim.Location = new Point(164, 12);
            lblTim.Name = "lblTim";
            lblTim.Size = new Size(157, 15);
            lblTim.TabIndex = 2;
            lblTim.Text = "Tìm theo MSSV hoặc họ tên";
            // 
            // txtTim
            // 
            txtTim.Location = new Point(164, 34);
            txtTim.Name = "txtTim";
            txtTim.Size = new Size(420, 23);
            txtTim.TabIndex = 3;
            txtTim.TextChanged += txtTim_TextChanged;
            // 
            // dgvSinhVien
            // 
            dgvSinhVien.AllowUserToAddRows = false;
            dgvSinhVien.AllowUserToDeleteRows = false;
            dgvSinhVien.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left;
            dgvSinhVien.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvSinhVien.Columns.AddRange(new DataGridViewColumn[] { colMaSV, colHoTen, colNgaySinh, colGioiTinh, colTenLop });
            dgvSinhVien.Location = new Point(164, 66);
            dgvSinhVien.MultiSelect = false;
            dgvSinhVien.Name = "dgvSinhVien";
            dgvSinhVien.ReadOnly = true;
            dgvSinhVien.RowHeadersVisible = false;
            dgvSinhVien.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvSinhVien.Size = new Size(420, 242);
            dgvSinhVien.TabIndex = 4;
            dgvSinhVien.SelectionChanged += dgvSinhVien_SelectionChanged;
            // 
            // colMaSV
            // 
            colMaSV.HeaderText = "MSSV";
            colMaSV.Name = "colMaSV";
            colMaSV.ReadOnly = true;
            colMaSV.Width = 70;
            // 
            // colHoTen
            // 
            colHoTen.HeaderText = "Họ tên";
            colHoTen.Name = "colHoTen";
            colHoTen.ReadOnly = true;
            colHoTen.Width = 150;
            // 
            // colNgaySinh
            // 
            dataGridViewCellStyle1.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colNgaySinh.DefaultCellStyle = dataGridViewCellStyle1;
            colNgaySinh.HeaderText = "Ngày sinh";
            colNgaySinh.Name = "colNgaySinh";
            colNgaySinh.ReadOnly = true;
            colNgaySinh.Width = 80;
            // 
            // colGioiTinh
            // 
            colGioiTinh.HeaderText = "Giới tính";
            colGioiTinh.Name = "colGioiTinh";
            colGioiTinh.ReadOnly = true;
            colGioiTinh.Width = 55;
            // 
            // colTenLop
            // 
            colTenLop.HeaderText = "Lớp";
            colTenLop.Name = "colTenLop";
            colTenLop.ReadOnly = true;
            colTenLop.Width = 60;
            // 
            // grpThongTin
            // 
            grpThongTin.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            grpThongTin.Controls.Add(lblMaSV);
            grpThongTin.Controls.Add(txtMaSV);
            grpThongTin.Controls.Add(lblHoTen);
            grpThongTin.Controls.Add(txtHoTen);
            grpThongTin.Controls.Add(lblNgaySinh);
            grpThongTin.Controls.Add(dtpNgaySinh);
            grpThongTin.Controls.Add(lblGioiTinh);
            grpThongTin.Controls.Add(radNam);
            grpThongTin.Controls.Add(radNu);
            grpThongTin.Controls.Add(lblLopSv);
            grpThongTin.Controls.Add(cboLop);
            grpThongTin.Controls.Add(btnThem);
            grpThongTin.Controls.Add(btnSua);
            grpThongTin.Controls.Add(btnXoa);
            grpThongTin.Location = new Point(596, 12);
            grpThongTin.Name = "grpThongTin";
            grpThongTin.Size = new Size(572, 296);
            grpThongTin.TabIndex = 5;
            grpThongTin.TabStop = false;
            grpThongTin.Text = "Thông tin sinh viên";
            // 
            // lblMaSV
            // 
            lblMaSV.AutoSize = true;
            lblMaSV.Location = new Point(16, 32);
            lblMaSV.Name = "lblMaSV";
            lblMaSV.Size = new Size(38, 15);
            lblMaSV.TabIndex = 0;
            lblMaSV.Text = "MSSV";
            // 
            // txtMaSV
            // 
            txtMaSV.Location = new Point(140, 29);
            txtMaSV.MaxLength = 10;
            txtMaSV.Name = "txtMaSV";
            txtMaSV.Size = new Size(180, 23);
            txtMaSV.TabIndex = 1;
            // 
            // lblHoTen
            // 
            lblHoTen.AutoSize = true;
            lblHoTen.Location = new Point(16, 66);
            lblHoTen.Name = "lblHoTen";
            lblHoTen.Size = new Size(43, 15);
            lblHoTen.TabIndex = 2;
            lblHoTen.Text = "Họ tên";
            // 
            // txtHoTen
            // 
            txtHoTen.Location = new Point(140, 63);
            txtHoTen.MaxLength = 100;
            txtHoTen.Name = "txtHoTen";
            txtHoTen.Size = new Size(400, 23);
            txtHoTen.TabIndex = 3;
            // 
            // lblNgaySinh
            // 
            lblNgaySinh.AutoSize = true;
            lblNgaySinh.Location = new Point(16, 100);
            lblNgaySinh.Name = "lblNgaySinh";
            lblNgaySinh.Size = new Size(60, 15);
            lblNgaySinh.TabIndex = 4;
            lblNgaySinh.Text = "Ngày sinh";
            // 
            // dtpNgaySinh
            // 
            dtpNgaySinh.CustomFormat = "dd/MM/yyyy";
            dtpNgaySinh.Format = DateTimePickerFormat.Custom;
            dtpNgaySinh.Location = new Point(140, 97);
            dtpNgaySinh.Name = "dtpNgaySinh";
            dtpNgaySinh.Size = new Size(180, 23);
            dtpNgaySinh.TabIndex = 5;
            dtpNgaySinh.Value = new DateTime(2005, 1, 1, 0, 0, 0, 0);
            // 
            // lblGioiTinh
            // 
            lblGioiTinh.AutoSize = true;
            lblGioiTinh.Location = new Point(16, 134);
            lblGioiTinh.Name = "lblGioiTinh";
            lblGioiTinh.Size = new Size(54, 15);
            lblGioiTinh.TabIndex = 6;
            lblGioiTinh.Text = "Giới tính";
            // 
            // radNam
            // 
            radNam.AutoSize = true;
            radNam.Checked = true;
            radNam.Location = new Point(140, 132);
            radNam.Name = "radNam";
            radNam.Size = new Size(51, 19);
            radNam.TabIndex = 7;
            radNam.TabStop = true;
            radNam.Text = "Nam";
            radNam.UseVisualStyleBackColor = true;
            // 
            // radNu
            // 
            radNu.AutoSize = true;
            radNu.Location = new Point(215, 132);
            radNu.Name = "radNu";
            radNu.Size = new Size(40, 19);
            radNu.TabIndex = 8;
            radNu.Text = "Nữ";
            radNu.UseVisualStyleBackColor = true;
            // 
            // lblLopSv
            // 
            lblLopSv.AutoSize = true;
            lblLopSv.Location = new Point(16, 170);
            lblLopSv.Name = "lblLopSv";
            lblLopSv.Size = new Size(31, 15);
            lblLopSv.TabIndex = 9;
            lblLopSv.Text = "Lớp";
            // 
            // cboLop
            // 
            cboLop.DropDownStyle = ComboBoxStyle.DropDownList;
            cboLop.FormattingEnabled = true;
            cboLop.Location = new Point(140, 167);
            cboLop.Name = "cboLop";
            cboLop.Size = new Size(300, 23);
            cboLop.TabIndex = 10;
            // 
            // btnThem
            // 
            btnThem.Location = new Point(16, 214);
            btnThem.Name = "btnThem";
            btnThem.Size = new Size(90, 34);
            btnThem.TabIndex = 11;
            btnThem.Text = "Thêm";
            btnThem.UseVisualStyleBackColor = true;
            btnThem.Click += btnThem_Click;
            // 
            // btnSua
            // 
            btnSua.Location = new Point(116, 214);
            btnSua.Name = "btnSua";
            btnSua.Size = new Size(90, 34);
            btnSua.TabIndex = 12;
            btnSua.Text = "Sửa";
            btnSua.UseVisualStyleBackColor = true;
            btnSua.Click += btnSua_Click;
            // 
            // btnXoa
            // 
            btnXoa.Location = new Point(216, 214);
            btnXoa.Name = "btnXoa";
            btnXoa.Size = new Size(90, 34);
            btnXoa.TabIndex = 13;
            btnXoa.Text = "Xóa";
            btnXoa.UseVisualStyleBackColor = true;
            btnXoa.Click += btnXoa_Click;
            // 
            // grpDiem
            // 
            grpDiem.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            grpDiem.Controls.Add(dgvDiem);
            grpDiem.Controls.Add(lblTenMon);
            grpDiem.Controls.Add(txtTenMon);
            grpDiem.Controls.Add(lblSoTinChi);
            grpDiem.Controls.Add(nudSoTinChi);
            grpDiem.Controls.Add(lblDiem);
            grpDiem.Controls.Add(nudDiem);
            grpDiem.Controls.Add(btnThemMon);
            grpDiem.Controls.Add(btnSuaDiem);
            grpDiem.Controls.Add(btnXoaMon);
            grpDiem.Controls.Add(lblTongTinChi);
            grpDiem.Controls.Add(lblDiemTB);
            grpDiem.Controls.Add(lblXepLoai);
            grpDiem.Controls.Add(btnXemTruoc);
            grpDiem.Controls.Add(btnIn);
            grpDiem.Controls.Add(btnCaiDatTrang);
            grpDiem.Controls.Add(btnXuatExcel);
            grpDiem.Location = new Point(12, 320);
            grpDiem.Name = "grpDiem";
            grpDiem.Size = new Size(1156, 388);
            grpDiem.TabIndex = 6;
            grpDiem.TabStop = false;
            grpDiem.Text = "Điểm của sinh viên";
            // 
            // dgvDiem
            // 
            dgvDiem.AllowUserToAddRows = false;
            dgvDiem.AllowUserToDeleteRows = false;
            dgvDiem.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            dgvDiem.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvDiem.Columns.AddRange(new DataGridViewColumn[] { colStt, colTenMon, colSoTinChi, colDiem });
            dgvDiem.Location = new Point(16, 30);
            dgvDiem.MultiSelect = false;
            dgvDiem.Name = "dgvDiem";
            dgvDiem.ReadOnly = true;
            dgvDiem.RowHeadersVisible = false;
            dgvDiem.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgvDiem.Size = new Size(600, 290);
            dgvDiem.TabIndex = 0;
            dgvDiem.SelectionChanged += dgvDiem_SelectionChanged;
            // 
            // colStt
            // 
            dataGridViewCellStyle2.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colStt.DefaultCellStyle = dataGridViewCellStyle2;
            colStt.HeaderText = "STT";
            colStt.Name = "colStt";
            colStt.ReadOnly = true;
            colStt.Width = 50;
            // 
            // colTenMon
            // 
            colTenMon.AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill;
            colTenMon.HeaderText = "Môn học";
            colTenMon.Name = "colTenMon";
            colTenMon.ReadOnly = true;
            // 
            // colSoTinChi
            // 
            dataGridViewCellStyle3.Alignment = DataGridViewContentAlignment.MiddleCenter;
            colSoTinChi.DefaultCellStyle = dataGridViewCellStyle3;
            colSoTinChi.HeaderText = "Số TC";
            colSoTinChi.Name = "colSoTinChi";
            colSoTinChi.ReadOnly = true;
            colSoTinChi.Width = 70;
            // 
            // colDiem
            // 
            dataGridViewCellStyle4.Alignment = DataGridViewContentAlignment.MiddleRight;
            dataGridViewCellStyle4.Format = "N2";
            colDiem.DefaultCellStyle = dataGridViewCellStyle4;
            colDiem.HeaderText = "Điểm";
            colDiem.Name = "colDiem";
            colDiem.ReadOnly = true;
            colDiem.Width = 70;
            // 
            // lblTenMon
            // 
            lblTenMon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTenMon.AutoSize = true;
            lblTenMon.Location = new Point(632, 34);
            lblTenMon.Name = "lblTenMon";
            lblTenMon.Size = new Size(54, 15);
            lblTenMon.TabIndex = 1;
            lblTenMon.Text = "Tên môn";
            // 
            // txtTenMon
            // 
            txtTenMon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            txtTenMon.Location = new Point(720, 31);
            txtTenMon.MaxLength = 100;
            txtTenMon.Name = "txtTenMon";
            txtTenMon.Size = new Size(300, 23);
            txtTenMon.TabIndex = 2;
            // 
            // lblSoTinChi
            // 
            lblSoTinChi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblSoTinChi.AutoSize = true;
            lblSoTinChi.Location = new Point(632, 68);
            lblSoTinChi.Name = "lblSoTinChi";
            lblSoTinChi.Size = new Size(60, 15);
            lblSoTinChi.TabIndex = 3;
            lblSoTinChi.Text = "Số tín chỉ";
            // 
            // nudSoTinChi
            // 
            nudSoTinChi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nudSoTinChi.Location = new Point(720, 65);
            nudSoTinChi.Maximum = new decimal(new int[] { 6, 0, 0, 0 });
            nudSoTinChi.Minimum = new decimal(new int[] { 1, 0, 0, 0 });
            nudSoTinChi.Name = "nudSoTinChi";
            nudSoTinChi.Size = new Size(80, 23);
            nudSoTinChi.TabIndex = 4;
            nudSoTinChi.Value = new decimal(new int[] { 1, 0, 0, 0 });
            // 
            // lblDiem
            // 
            lblDiem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDiem.AutoSize = true;
            lblDiem.Location = new Point(632, 102);
            lblDiem.Name = "lblDiem";
            lblDiem.Size = new Size(34, 15);
            lblDiem.TabIndex = 5;
            lblDiem.Text = "Điểm";
            // 
            // nudDiem
            // 
            nudDiem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            nudDiem.DecimalPlaces = 1;
            nudDiem.Increment = new decimal(new int[] { 5, 0, 0, 65536 });
            nudDiem.Location = new Point(720, 99);
            nudDiem.Maximum = new decimal(new int[] { 10, 0, 0, 0 });
            nudDiem.Name = "nudDiem";
            nudDiem.Size = new Size(80, 23);
            nudDiem.TabIndex = 6;
            // 
            // btnThemMon
            // 
            btnThemMon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnThemMon.Location = new Point(632, 140);
            btnThemMon.Name = "btnThemMon";
            btnThemMon.Size = new Size(110, 32);
            btnThemMon.TabIndex = 7;
            btnThemMon.Text = "Thêm môn";
            btnThemMon.UseVisualStyleBackColor = true;
            btnThemMon.Click += btnThemMon_Click;
            // 
            // btnSuaDiem
            // 
            btnSuaDiem.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnSuaDiem.Location = new Point(752, 140);
            btnSuaDiem.Name = "btnSuaDiem";
            btnSuaDiem.Size = new Size(110, 32);
            btnSuaDiem.TabIndex = 8;
            btnSuaDiem.Text = "Sửa điểm";
            btnSuaDiem.UseVisualStyleBackColor = true;
            btnSuaDiem.Click += btnSuaDiem_Click;
            // 
            // btnXoaMon
            // 
            btnXoaMon.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            btnXoaMon.Location = new Point(872, 140);
            btnXoaMon.Name = "btnXoaMon";
            btnXoaMon.Size = new Size(110, 32);
            btnXoaMon.TabIndex = 9;
            btnXoaMon.Text = "Xóa môn";
            btnXoaMon.UseVisualStyleBackColor = true;
            btnXoaMon.Click += btnXoaMon_Click;
            // 
            // lblTongTinChi
            // 
            lblTongTinChi.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblTongTinChi.AutoSize = true;
            lblTongTinChi.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblTongTinChi.Location = new Point(632, 196);
            lblTongTinChi.Name = "lblTongTinChi";
            lblTongTinChi.Size = new Size(110, 17);
            lblTongTinChi.TabIndex = 10;
            lblTongTinChi.Text = "Tổng tín chỉ: 0";
            // 
            // lblDiemTB
            // 
            lblDiemTB.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblDiemTB.AutoSize = true;
            lblDiemTB.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblDiemTB.Location = new Point(632, 224);
            lblDiemTB.Name = "lblDiemTB";
            lblDiemTB.Size = new Size(120, 17);
            lblDiemTB.TabIndex = 11;
            lblDiemTB.Text = "Điểm TB: Chưa có điểm";
            // 
            // lblXepLoai
            // 
            lblXepLoai.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            lblXepLoai.AutoSize = true;
            lblXepLoai.Font = new Font("Segoe UI", 9.75F, FontStyle.Bold);
            lblXepLoai.Location = new Point(632, 252);
            lblXepLoai.Name = "lblXepLoai";
            lblXepLoai.Size = new Size(120, 17);
            lblXepLoai.TabIndex = 12;
            lblXepLoai.Text = "Xếp loại: Chưa có điểm";
            // 
            // btnXemTruoc
            // 
            btnXemTruoc.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnXemTruoc.Location = new Point(16, 336);
            btnXemTruoc.Name = "btnXemTruoc";
            btnXemTruoc.Size = new Size(150, 36);
            btnXemTruoc.TabIndex = 13;
            btnXemTruoc.Text = "Xem trước phiếu";
            btnXemTruoc.UseVisualStyleBackColor = true;
            btnXemTruoc.Click += btnXemTruoc_Click;
            // 
            // btnIn
            // 
            btnIn.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnIn.Location = new Point(176, 336);
            btnIn.Name = "btnIn";
            btnIn.Size = new Size(100, 36);
            btnIn.TabIndex = 14;
            btnIn.Text = "In...";
            btnIn.UseVisualStyleBackColor = true;
            btnIn.Click += btnIn_Click;
            // 
            // btnCaiDatTrang
            // 
            btnCaiDatTrang.Anchor = AnchorStyles.Bottom | AnchorStyles.Left;
            btnCaiDatTrang.Location = new Point(286, 336);
            btnCaiDatTrang.Name = "btnCaiDatTrang";
            btnCaiDatTrang.Size = new Size(140, 36);
            btnCaiDatTrang.TabIndex = 15;
            btnCaiDatTrang.Text = "Cài đặt trang...";
            btnCaiDatTrang.UseVisualStyleBackColor = true;
            btnCaiDatTrang.Click += btnCaiDatTrang_Click;
            // 
            // btnXuatExcel
            // 
            btnXuatExcel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
            btnXuatExcel.Location = new Point(940, 336);
            btnXuatExcel.Name = "btnXuatExcel";
            btnXuatExcel.Size = new Size(200, 36);
            btnXuatExcel.TabIndex = 16;
            btnXuatExcel.Text = "Xuất Excel điểm lớp";
            btnXuatExcel.UseVisualStyleBackColor = true;
            btnXuatExcel.Click += btnXuatExcel_Click;
            // 
            // Form1
            // 
            AutoScaleMode = AutoScaleMode.None;
            ClientSize = new Size(1180, 720);
            Controls.Add(grpDiem);
            Controls.Add(grpThongTin);
            Controls.Add(dgvSinhVien);
            Controls.Add(txtTim);
            Controls.Add(lblTim);
            Controls.Add(lstLop);
            Controls.Add(lblLop);
            MinimumSize = new Size(1000, 640);
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "Quản lý sinh viên và in phiếu báo điểm";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvSinhVien).EndInit();
            grpThongTin.ResumeLayout(false);
            grpThongTin.PerformLayout();
            grpDiem.ResumeLayout(false);
            grpDiem.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)dgvDiem).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudSoTinChi).EndInit();
            ((System.ComponentModel.ISupportInitialize)nudDiem).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label lblLop;
        private ListBox lstLop;
        private Label lblTim;
        private TextBox txtTim;
        private DataGridView dgvSinhVien;
        private DataGridViewTextBoxColumn colMaSV;
        private DataGridViewTextBoxColumn colHoTen;
        private DataGridViewTextBoxColumn colNgaySinh;
        private DataGridViewTextBoxColumn colGioiTinh;
        private DataGridViewTextBoxColumn colTenLop;
        private GroupBox grpThongTin;
        private Label lblMaSV;
        private TextBox txtMaSV;
        private Label lblHoTen;
        private TextBox txtHoTen;
        private Label lblNgaySinh;
        private DateTimePicker dtpNgaySinh;
        private Label lblGioiTinh;
        private RadioButton radNam;
        private RadioButton radNu;
        private Label lblLopSv;
        private ComboBox cboLop;
        private Button btnThem;
        private Button btnSua;
        private Button btnXoa;
        private GroupBox grpDiem;
        private DataGridView dgvDiem;
        private DataGridViewTextBoxColumn colStt;
        private DataGridViewTextBoxColumn colTenMon;
        private DataGridViewTextBoxColumn colSoTinChi;
        private DataGridViewTextBoxColumn colDiem;
        private Label lblTenMon;
        private TextBox txtTenMon;
        private Label lblSoTinChi;
        private NumericUpDown nudSoTinChi;
        private Label lblDiem;
        private NumericUpDown nudDiem;
        private Button btnThemMon;
        private Button btnSuaDiem;
        private Button btnXoaMon;
        private Label lblTongTinChi;
        private Label lblDiemTB;
        private Label lblXepLoai;
        private Button btnXemTruoc;
        private Button btnIn;
        private Button btnCaiDatTrang;
        private Button btnXuatExcel;
    }
}
