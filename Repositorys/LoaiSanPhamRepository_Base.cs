

using Microsoft.EntityFrameworkCore;
using TestDev.Models;

namespace Cl.DataAccess.EF.Repository
{
    public partial class LoaiSanPhamRepository : EFRepository<LoaiSanPham>
    {
        public int TotalRecord = 0;

        public LoaiSanPhamRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public LoaiSanPham GetById(int Id)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                return  context.LoaiSanPham.First(dk => dk.Id == Id);

            }
            catch { return null; }
        }


        public int DeleteById(int Id)
        {
            try
            {
                LoaiSanPham entity = this.GetById(Id);
                if (entity != null)
                    this.Delete(entity);
                return 1;
            }
            catch { return 0; }
        }
        public LoaiSanPham SaveReturnToObject(LoaiSanPham obj)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                context.LoaiSanPham.Add(obj);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message + ex.StackTrace + ex.Source);
            }
            return obj;
        }


        public List<string> DeleteByIds(string ItemId, string returnField = "TEN")
        {
            string[] arrItemID = ItemId.Split(',');
            List<string> lstObjectID = new List<string>();
            List<string> lstObjectName = new List<string>();
            DBContext context = (DBContext)UnitOfWork.Context;

            foreach (string Id in arrItemID)
            {
                try
                {
                    string deleteCommand = string.Format("DELETE FROM Task WHERE Id = '{0}'", Id);
                    context.Database.ExecuteSqlRaw(deleteCommand);
                }
                catch (Exception)
                {
                    lstObjectID.Add(Id);
                }
            }
            return lstObjectID;
        }

        public void DeleteByWhereClause(string whereClause)
        {
            DBContext context = (DBContext)UnitOfWork.Context;
            string deleteCommand = string.Format("DELETE FROM {0} WHERE {1}", "Task", whereClause);
            context.Database.ExecuteSqlRaw(deleteCommand);
        }
    }
}
