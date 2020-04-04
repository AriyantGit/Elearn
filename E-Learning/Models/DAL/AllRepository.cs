using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using System.Linq.Expressions;

namespace E_Learning.Models.DAL
{
    public class AllRepository<T> :IDisposable, _IAllRepository<T> where T : class
    {
        DbModel db;
        //IDbset<T> entity;
        private IDbSet<T> dbentity;
        
        public AllRepository()
        {
            db = new DbModel();
            dbentity = db.Set<T>();
        }

        public void DeleteModel(int modelID)
        {
            T model = dbentity.Find(modelID);
            dbentity.Remove(model);
        }
        public  async Task DeleteAsyn(int modelID)
        {
            T model =await GetModelByIdasync(modelID);
            db.Set<T>().Remove(model);
             await SaveAsync();
        }
        public void Dispose()
        {
            if (db != null)
            {
                db.Dispose();
                db = null;
            }
        }
       
        public  IEnumerable<T> GetAll()
        {
            return dbentity.ToList();
        }
        //public async Task<ICollection<T>> GetAllaync()
        //{
        //    var a = await dbentity.ToListAsync();
        //    return await dbentity.ToListAsync();
        //}
        public virtual async  Task<IEnumerable<T>> GetAllaync()
        {
            return await db.Set<T>().ToListAsync();
        }
        public virtual async Task<ICollection<T>> Getcollectionaync()
        {
            return await db.Set<T>().ToListAsync();
        }
        public IQueryable<T> FindByCondition(Expression<Func<T, bool>> expression)
        {
            return this.db.Set<T>()
                .Where(expression).AsNoTracking();
        }


        public T GetModelById(int modelID)
        {

            return dbentity.Find(modelID);
        }
        public Task<T> GetModelByIdasync(int id) => db.Set<T>().FindAsync(id);
      

        public void InsertModel(T model)
        {
            dbentity.Add(model);
        }
        public async Task InsertModelasync(T model)
        {
             dbentity.Add(model);
            await SaveAsync();
        }
       


        public void Save()
        {
            db.SaveChanges();
        }
        public async virtual Task SaveAsync()
        {
             await db.SaveChangesAsync();
        }


        public void UpdateModel(T model)
        {
            db.Entry(model).State = EntityState.Modified;
        }
        public  async Task UpdateAsyn(T t)
        {
            db.Entry(t).State = EntityState.Modified;
            await SaveAsync();
            //if (t == null)
            //    return null;
            //T exist;
            ////T exist = await db.Set<T>().FindAsync(key);
            ////if (exist != null)
            ////{
            //    db.Entry(exist).CurrentValues.SetValues(t);
            //    await SaveAsync();
            ////}
            //return exist;
        }
    }
}