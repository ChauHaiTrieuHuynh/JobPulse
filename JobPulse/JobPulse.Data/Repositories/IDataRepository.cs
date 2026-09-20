using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Data.Repositories
{
    public interface IDataRepository<T>
    {
        Task AddJobAsync(T entity);
        Task<T?> GetByIdAsync(string id);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
