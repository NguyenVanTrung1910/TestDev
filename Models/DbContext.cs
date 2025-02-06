using Microsoft.EntityFrameworkCore;

namespace TestDev.Models
{
    public class DBContext : DbContext
    {
        public DbSet<SanPham> SanPham { get; set; }
        public DbSet<LoaiSanPham> LoaiSanPham { get; set; }
        public DbSet<SanPham_LoaiSanPham> SanPham_LoaiSanPham { get; set; }

        public DBContext(DbContextOptions<DBContext> options) : base(options) { }
        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    var random = new Random();
        //    var listLoaiSanPham = new List<LoaiSanPham>();
        //    var listSanPham = new List<SanPham>();
        //    var listSanPhamLoaiSanPham = new List<SanPham_LoaiSanPham>();

        //    // 1. Tạo 20 loại sản phẩm
        //    for (int i = 0; i < 20; i++)
        //    {
        //        listLoaiSanPham.Add(new LoaiSanPham
        //        {
        //            Ten = $"Loại Sản Phẩm {i + 1}",
        //            NgayNhap = DateTime.Now.AddDays(-random.Next(1, 365))
        //        });
        //    }

        //    // 2. Tạo 10.000 sản phẩm
        //    for (int i = 0; i < 10000; i++)
        //    {
        //        listSanPham.Add(new SanPham
        //        {
        //            Ten = $"Sản phẩm {i + 1}",
        //            Gia = random.Next(100, 10000), // Giá từ 100 - 10.000
        //            NgayNhap = DateTime.Now.AddDays(-random.Next(1, 365))
        //        });
        //    }

        //    modelBuilder.Entity<LoaiSanPham>().HasData(listLoaiSanPham);
        //    modelBuilder.Entity<SanPham>().HasData(listSanPham);
        //}
        //public void SeedData()
        //{
        //    if (!LoaiSanPham.Any()) // Kiểm tra nếu chưa có dữ liệu
        //    {
        //        var random = new Random();

        //        var loaiSanPhams = new List<LoaiSanPham>();
        //        for (int i = 0; i < 20; i++)
        //        {
        //            loaiSanPhams.Add(new LoaiSanPham
        //            {
        //                Ten = $"Loại Sản Phẩm {i + 1}",
        //                NgayNhap = DateTime.Now.AddDays(-random.Next(1, 365))
        //            });
        //        }
        //        LoaiSanPham.AddRange(loaiSanPhams);
        //        SaveChanges();

        //        var sanPhams = new List<SanPham>();
        //        for (int i = 0; i < 10000; i++)
        //        {
        //            sanPhams.Add(new SanPham
        //            {
        //                Ten = $"Sản phẩm {i + 1}",
        //                Gia = random.Next(100, 10000),
        //                NgayNhap = DateTime.Now.AddDays(-random.Next(1, 365))
        //            });
        //        }
        //        SanPham.AddRange(sanPhams);
        //        SaveChanges();

        //        // Gán sản phẩm vào loại sản phẩm ngẫu nhiên
        //        var sanPham_LoaiSanPham = new List<SanPham_LoaiSanPham>();
        //        var allSanPhams = SanPham.ToList();
        //        var allLoaiSanPhams = LoaiSanPham.ToList();

        //        foreach (var sp in allSanPhams)
        //        {
        //            var soLoai = random.Next(1, 4); // Mỗi sản phẩm có từ 1 đến 3 loại
        //            var loaiDuocChon = allLoaiSanPhams.OrderBy(x => random.Next()).Take(soLoai).ToList();
        //            foreach (var loai in loaiDuocChon)
        //            {
        //                sanPham_LoaiSanPham.Add(new SanPham_LoaiSanPham
        //                {
        //                    SanPhamId = sp.Id,
        //                    LoaiSanPhamId = loai.Id
        //                });
        //            }
        //        }

        //        SanPham_LoaiSanPham.AddRange(sanPham_LoaiSanPham);
        //        SaveChanges();
        //    }
        //}


    }

}
