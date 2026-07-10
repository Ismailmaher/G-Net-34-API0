using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace E_Commerce.Infrastructure.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext dbContext) : IGenericRepository<TEntity, TKey> 
        where TEntity : BaseEntity<TKey> 
    {
        public void Add(TEntity entity)=> dbContext.Set<TEntity>().Add(entity);


        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);


        public void Remove(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);




        public async Task<IReadOnlyList<TEntity>> GetByIdAsync(CancellationToken ct = default)
            =>await dbContext.Set<TEntity>().AsNoTracking().ToListAsync(ct);
        

        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default)
            =>await dbContext.Set<TEntity>().FindAsync( [ id!], ct).AsTask();

        public Task GetAllAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }

        Task<IReadOnlyList<TEntity>> IGenericRepository<TEntity, TKey>.GetAllAsync(CancellationToken ct)
        {
            throw new NotImplementedException();
        }
    }
}
