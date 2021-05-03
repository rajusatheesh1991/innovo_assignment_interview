using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace InnovoAssignment.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T:class
    {

        Task<T> GetByIdAsync(int id);

        Task<T> GetByIdAsync(Guid id);
        Task<T> AddAsync(T entity);
        Task<IReadOnlyList<T>> ListAllAsync();
        Task UpdateAsync(T entity);
        Task DeleteAsync(T entity);

        void Update(T entity);

        T Add(T entity);

      
    }
}
