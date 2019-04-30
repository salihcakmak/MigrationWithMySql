using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MigrationWithMySql.DAL.Repository;

namespace MigrationWithMySql.DAL.UnitOfWork
{
    /// <summary>
    /// EntityFramework için oluşturmuş olduğumuz UnitOfWork.
    /// EFRepository de olduğu gibi bu şekilde tasarlamamızın ana sebebi ise veri tabanına independent (bağımsız) bir durumda kalabilmek.
    /// </summary>
    public class UnitOfWork : IUnitOfWork
    {
        private DbContext dbContext;

        /// <summary>
        /// Bağlantı ismi.
        /// Web Config dosyasında hangi connectionString üzerinden veri gelecekse onu çeker.
        /// </summary>
        private string connectionName
        {
            get { return ConfigurationManager.AppSettings["ConnectionName"]; }
        }

        /// <summary>
        /// Veri bağlantımızı bağlama.MyDbConcext'i bağladık.
        /// </summary>
        private DbContext DbContext
        {
            get
            {
                if (dbContext==null)
                {
                    dbContext =new MyDbContext();
                }
                return dbContext;
            }
            set { dbContext = value; }
        }

        /// <summary>
        /// Repository ınstance 'i başlatmak için kullanılır
        /// </summary>
        /// <typeparam name="T">Veri tabanı tür nesnesi</typeparam>
        /// <returns>Tür nesnesi ile ilgili repository</returns>
        public IRepository<T> GetRepository<T>() where T : class
        {
            return new Repository<T>(DbContext);
        }

        /// <summary>
        /// DEğişiklikleri kaydet.
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            try
            {
                int result = 0;
                result = DbContext.SaveChanges();
                return result;
            }
            catch (Exception)
            {

                return -1;
            }
            
        }

        #region IDisposable Support
        private bool disposedValue = false;

        /// <summary>
        /// Nesneyi bellekten atmadan önce bağlantıyı kapatır.
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    DbContext.Dispose();
                    DbContext = null;
                }
                disposedValue = true;
            }
        }

       /// <summary>
       /// IDisposable Design Pattern ınstance'ı.
       /// </summary>
        public void Dispose()
        {
            Dispose(true);
        }
        #endregion

    }
}
