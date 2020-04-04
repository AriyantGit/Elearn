using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace E_Learning.Models.DAL
{
    public interface IMyAsyncDbSet<TEntity>
    where TEntity : class
    {
        

        // Copy other methods from IDbSet<T> as needed.

        Task<Object> FindAsync(params Object[] keyValues);
    }
}
