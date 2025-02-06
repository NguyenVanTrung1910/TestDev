using Cl.DataAccess.EF.Query;

namespace TestDev.Models
{
    public class SanPham : CoreEntity
    {
        public string? Ten {  get; set; }
        public decimal Gia { get; set; }
        public DateTime NgayNhap { get; set; }
        public List<SanPham_LoaiSanPham> SanPham_LoaiSanPhams { get; set; } = new();

    }
    public class SanPhamQuery : BaseQuery
    {
        public List<int> LoaiSP { get; set; } = new List<int> {};
    }
    public class SanPhamEntity : SanPham
    {
        public string DSLoai { get; set; }
    }
}
