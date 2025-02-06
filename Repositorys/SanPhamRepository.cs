

using Cl.DataAccess.EF.Query;
using Cl.DataAccess.EF.VM_Ultils;
using System.Linq;
using TestDev.Models;

namespace Cl.DataAccess.EF.Repository
{
    public partial class  SanPhamRepository
    {


        public List<SanPhamEntity> GetPaged(SanPhamQuery SearchOption)
        {
            var context = (DBContext)UnitOfWork.Context;

            var query = from obj in context.SanPham
                        join sp_loai in context.SanPham_LoaiSanPham
                        on obj.Id equals sp_loai.SanPhamId into spGroup
                        from sanpham_loai in spGroup.DefaultIfEmpty()
                        join loai in context.LoaiSanPham
                        on sanpham_loai.LoaiSanPhamId equals loai.Id into loaiGroup
                        from loaiSanPham in loaiGroup.DefaultIfEmpty()
                        where (SearchOption.LoaiSP.Count == 0 ||
                               (sanpham_loai != null && SearchOption.LoaiSP.Contains(sanpham_loai.LoaiSanPhamId)))
                        group new { obj, loaiSanPham } by new { obj.Id, obj.Ten, obj.Gia, obj.NgayNhap } into g
                        select new SanPhamEntity
                        {
                            Id = g.Key.Id,
                            Ten = g.Key.Ten,
                            Gia = g.Key.Gia,
                            NgayNhap = g.Key.NgayNhap,
                            DSLoai = string.Join(", ", g.Select(x => x.loaiSanPham.Ten).Where(x => x != null)) // Chuyển danh sách thành chuỗi
                        };

            return query.GetByGridRequest(SearchOption.oGridRequest, ref TotalRecord).ToList();
        }

        public void  AddSP_Loai(SanPham SanPhamInput, List<int> LoaiSP)
        {
            var context = (DBContext)UnitOfWork.Context;
            for (int i = 0; i < LoaiSP.Count; i++)
            {
                var sp_loai = new SanPham_LoaiSanPham();
                sp_loai.SanPhamId = SanPhamInput.Id;
                sp_loai.LoaiSanPhamId = LoaiSP[i];
                context.SanPham_LoaiSanPham.Add(sp_loai);
            }
            context.SaveChanges();
        }
    }
}
