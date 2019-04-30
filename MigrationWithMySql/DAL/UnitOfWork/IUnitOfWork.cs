using MigrationWithMySql.DAL.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MigrationWithMySql.DAL.UnitOfWork
{
    /// <summary>
    /// UnitOfWork sınıfı tarafından kullanılacak arayüz.
    /// </summary>
    public interface IUnitOfWork:IDisposable
    {
        IRepository<T> GetRepository<T>() where T : class;
        int SaveChanges();
    }
}
