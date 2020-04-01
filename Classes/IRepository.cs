using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SGRSalary.Classes
{
    public interface IRepository<T>
    {
        IQueryable<T> All();
        IQueryable<T> All(string includes);
        void Add(T entity);
        void AddAndSave(T entity);
        void Remove(T entity);
        void Save();
        IEnumerable<T> Find(Expression<Func<T, bool>> where);
        T First(Expression<Func<T, bool>> where);
        IQueryable<T> Find(Expression<Func<T, bool>> where, string includes);

        /// <summary>
        /// تابعی که کالکشن را بر اساس مقادیر ذخیره شده در دیتابیس بروز رسانی می کند
        /// </summary>
        /// <param name="collection">کالکشن</param>
        void UpdateStoreWins(IEnumerable<T> collection);

        /// <summary>
        /// تابعی که اینتیتی را بر اساس مقادیر ذخیره شده در دیتابیس بروز رسانی می کند
        /// </summary>
        /// <param name="entity"></param>
        void UpdateStoreWins(T entity);

        void UpdateClientWins(T entity);



        void Cancel(T entity);
    }
}
