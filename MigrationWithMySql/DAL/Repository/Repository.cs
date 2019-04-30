using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MigrationWithMySql.DAL.Repository
{
    /// <summary>
    /// EntityFramework için hazırlıyor olduğumuz bu repositoryi daha önceden tasarladığımız generic repositorimiz olan IRepository arayüzünü implemente ederek tasarladık.
    /// Bu şekilde tanımlamamızın sebebi veritabanına  independent (bağımsız) bir durumda kalabilmek.
    /// </summary>
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _DbContext;
        private readonly DbSet<T> _DbSet;

        /// <summary>
        /// Repository örneğini başlatır.
        /// </summary>
        /// <param name="dbContext">Veri tabanı bağlantı nesnesi</param>
        public Repository(DbContext dbContext)
        {
            _DbContext = dbContext;
            _DbSet = dbContext.Set<T>();
        }

        #region IRepository Methods

        /// <summary>
        /// Verilen veriyi context üzerine ekler.
        /// </summary>
        /// <param name="entity">Eklenecek entity nesnesi</param>
        public void Add(T entity)
        {
            _DbSet.Add(entity);
        }

        /// <summary>
        /// Verilen veriyi context üzerinden siler.
        /// </summary>
        /// <param name="entity">Silinecek entity nesnesi</param>
        /// <param name="forceDelete">Nesneyi veri tabanından gerçekten sil</param>
        public void Delete(T entity, bool forceDelete)
        {
            EntityEntry<T> dbEntityEntry = _DbContext.Entry(entity);
            if (dbEntityEntry.State !=EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                _DbSet.Remove(entity);
            }
        }

        /// <summary>
        /// Şarta göre tek veri getirir.
        /// </summary>
        /// <param name="predicate">Veri şartı.</param>
        /// <returns></returns>
        public T Get(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> iQueryable = _DbSet.Where(predicate);
            return iQueryable.ToList().FirstOrDefault();
        }

        /// <summary>
        /// Tüm verileri getirir.
        /// Select * From T
        /// </summary>
        /// <returns></returns>
        public IQueryable<T> GetAll()
        {
            IQueryable<T> iQueryable = _DbSet;
            return iQueryable;
        }

        /// <summary>
        /// Şarta göre tüm verileri getirir.
        /// Select * From T Where predicate
        /// </summary>
        /// <param name="predicate">Veri şartı</param>
        /// <returns></returns>
        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            IQueryable<T> iQueryable = _DbSet.Where(predicate);
            return iQueryable;
        }

        /// <summary>
        /// Verilen veriyi context üzerinde günceller.
        /// </summary>
        /// <param name="entity">Güncellenecek entity</param>
        public void Update(T entity)
        {
            _DbContext.Entry(entity).State = EntityState.Modified;
        }
    }
    #endregion
}
