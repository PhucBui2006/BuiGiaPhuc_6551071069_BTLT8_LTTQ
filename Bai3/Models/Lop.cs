namespace bai73.Models;

public partial class Lop
{
    public Lop()
    {
        SinhViens = new HashSet<SinhVien>();
    }

    public string MaLop { get; set; } = null!;
    public string TenLop { get; set; } = null!;

    public virtual ICollection<SinhVien> SinhViens { get; set; }
}
