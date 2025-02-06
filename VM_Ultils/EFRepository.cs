using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using TestDev.Models;
namespace Cl.DataAccess.EF.Repository
{



	public class EFUnitOfWork : IUnitOfWork
	{
		public DBContext Context { get; set; }

		public EFUnitOfWork(DBContext dBContext)
		{
			Context = dBContext;
		}

		public void Commit()
		{
			Context.SaveChanges();
		}

		public bool LazyLoadingEnabled
		{
			get { return Context.ChangeTracker.LazyLoadingEnabled; }
			set { Context.ChangeTracker.LazyLoadingEnabled = value; }
		}

		public bool ProxyCreationEnabled
		{
			get { return Context.ChangeTracker.LazyLoadingEnabled; }
			set { Context.ChangeTracker.LazyLoadingEnabled = value; }
		}

		public string ConnectionString
		{
			get { return Context.Database.GetConnectionString(); }
			set { Context.Database.SetConnectionString(value); }
		}
	}

	public interface IUnitOfWork
	{
		DBContext Context { get; set; }
		void Commit();
		bool LazyLoadingEnabled { get; set; }
		bool ProxyCreationEnabled { get; set; }
		string ConnectionString { get; set; }
	}

	public interface IRepository<T>
	{
		IUnitOfWork UnitOfWork { get; set; }
		IQueryable<T> All();
		IQueryable<T> Where(Expression<Func<T, bool>> expression);
		void Add(T entity);
		void Delete(T entity);
		void Save();
	}
	public class EFRepository<T> : IRepository<T> where T : class
	{

		public IUnitOfWork UnitOfWork { get; set; }

		private DbSet<T> _objectset;
		private DbSet<T> ObjectSet
		{
			get
			{
				if (_objectset == null)
				{
					_objectset = UnitOfWork.Context.Set<T>();
				}
				return _objectset;
			}
		}

		public virtual IQueryable<T> All()
		{
			return ObjectSet.AsQueryable();
		}

		public IQueryable<T> Where(Expression<Func<T, bool>> expression)
		{
			return ObjectSet.Where(expression);
		}

		public void Add(T entity)
		{
			ObjectSet.Add(entity);
		}
		public void AddReturnID(T entity)
		{
			ObjectSet.Add(entity);
			UnitOfWork.Context.SaveChanges();

		}
		public void Delete(T entity)
		{
			ObjectSet.Remove(entity);
		}

		public void Save()
		{
			UnitOfWork.Context.SaveChanges();
		}
	}
}
