

using Cl.DataAccess.EF.Query;
using Cl.DataAccess.EF.VM_Ultils;
using TestDev.Models;

namespace Cl.DataAccess.EF.Repository
{
    public partial class LoaiSanPhamRepository
    {


        public List<LoaiSanPhamEntity> GetPaged(LoaiSanPhamQuery SearchOption)
        {
            var context = (DBContext)UnitOfWork.Context;

            var query = from loai in context.LoaiSanPham
                        join sp_loai in context.SanPham_LoaiSanPham
                        on loai.Id equals sp_loai.LoaiSanPhamId into spGroup // Nhóm sản phẩm theo loại
                        select new LoaiSanPhamEntity
                        {
                            Id = loai.Id,
                            Ten = loai.Ten,
                            NgayNhap = loai.NgayNhap,
                            SoLuongSanPham = spGroup.Count() // Đếm số lượng sản phẩm thuộc loại này
                        };

            return query.GetByGridRequest(SearchOption.oGridRequest, ref TotalRecord).ToList();
        }


    }
}
