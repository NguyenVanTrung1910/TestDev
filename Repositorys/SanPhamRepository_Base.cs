

using Microsoft.EntityFrameworkCore;
using TestDev.Models;

namespace Cl.DataAccess.EF.Repository
{
    public partial class SanPhamRepository :EFRepository<SanPham>
    {
        public int TotalRecord = 0;

        public SanPhamRepository(IUnitOfWork unitOfWork)
        {
            UnitOfWork = unitOfWork;
        }
        public SanPham GetById(int Id)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                var a = context.SanPham.First(dk => dk.Id == Id);
                return a;
            }
            catch { return null; }
        }


        public int DeleteById(int Id)
        {
            try
            {
                SanPham entity = this.GetById(Id);
                if (entity != null)
                    this.Delete(entity);
                return 1;
            }
            catch { return 0; }
        }
        public SanPham SaveReturnToObject(SanPham obj)
        {
            try
            {
                DBContext context = (DBContext)UnitOfWork.Context;
                context.SanPham.Add(obj);
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
