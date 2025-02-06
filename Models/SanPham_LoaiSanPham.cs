namespace TestDev.Models
{
    public class SanPham_LoaiSanPham : CoreEntity
    {
        public int SanPhamId { get; set; }
        public SanPham SanPham { get; set; }
        public int LoaiSanPhamId { get; set; }
        public LoaiSanPham LoaiSanPham { get; set; }
    }
}
