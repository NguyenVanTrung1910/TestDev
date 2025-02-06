using Cl.DataAccess.EF.Query;
using System.ComponentModel.DataAnnotations;

namespace TestDev.Models
{
    public class LoaiSanPham : CoreEntity
    {
        public string? Ten {  get; set; }
        public DateTime NgayNhap { get; set; }
        public List<SanPham_LoaiSanPham> SanPham_LoaiSanPhams { get; set; } = new();
    }
    public class CoreEntity
    {
        [Key]
        public int Id { get; set; }
    }
    public class LoaiSanPhamQuery : BaseQuery
    {

    }
    public class LoaiSanPhamEntity : LoaiSanPham
    {
        public int SoLuongSanPham { get; set; }   
    }
}
