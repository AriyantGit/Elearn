using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Models.DAL
{
    interface _IAllRepository<T> where T : class
    {

        IEnumerable<T> GetAll();
        T GetModelById(int modelID);
        void InsertModel(T model);
        void UpdateModel(T model);
        void DeleteModel(int modelID);
        void Dispose();
        void Save();
        // Task<T> GetModelByIdAsync(int id);
        //Task<ICollection<T>> GetAllAsyn();
        //Task<T> FindAsync(Expression<Func<T, bool>> match);
        //Task<T> AddAsyn(T t);
        //Task<T> UpdateAsyn(T t, object key);
        //Task<int> DeleteAsyn(T entity);
        //Task<T> FindAsync(Expression<Func<T, bool>> match);
        Task<IEnumerable<T>> GetAllaync();
        Task<ICollection<T>> Getcollectionaync();
        IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression);
        Task<T> GetModelByIdasync(int id);
        Task InsertModelasync(T model);
        Task UpdateAsyn(T t);
        Task DeleteAsyn(int id);
        Task SaveAsync();
    }
}
