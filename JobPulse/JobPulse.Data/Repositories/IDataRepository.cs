using System;
using System.Collections.Generic;
using System.Text;

namespace JobPulse.Data.Repositories
{
    public interface IDataRepository<T>
    {
        Task AddJobAsync(string id, T entity);
        Task<T?> GetByIdAndTypeAsync(string id, int jobSourceType);
        Task<IEnumerable<T>> GetAllAsync();
    }
}
