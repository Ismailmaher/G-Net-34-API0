using E_Commerce.Domain.Common;
using E_Commerce.Domain.Contracts;
using E_Commerce.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using E_Commerce.Application.Specifications;

namespace E_Commerce.Infrastructure.Repositories
{
    public class GenericRepository<TEntity, TKey>(StoreDbContext dbContext) : IGenericRepository<TEntity, TKey>
        where TEntity : BaseEntity<TKey>
    {
        public void Add(TEntity entity) => dbContext.Set<TEntity>().Add(entity);

        public void Update(TEntity entity) => dbContext.Set<TEntity>().Update(entity);

        public void Remove(TEntity entity) => dbContext.Set<TEntity>().Remove(entity);

        //  تطبيق GetAllAsync الصحيح
        public async Task<IReadOnlyList<TEntity>> GetAllAsync(CancellationToken ct = default)
            => await dbContext.Set<TEntity>().AsNoTracking().ToListAsync(ct);

        public async Task<TEntity?> GetByIdAsync(TKey id, CancellationToken ct = default)
            => await dbContext.Set<TEntity>().FindAsync([id!], ct).AsTask();

        public async Task<IReadOnlyList<TEntity>> GetAllAsync(ISpecification<TEntity, TKey> spec, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.CreateQuery(dbContext.Set<TEntity>(), spec);
            return await query.ToListAsync(ct);
        }

        public async Task<TEntity?> GetByIdAsync(ISpecification<TEntity, TKey> spec, CancellationToken ct = default)
        {
            var query = SpecificationEvaluator.CreateQuery(dbContext.Set<TEntity>(), spec);
            return await query.FirstOrDefaultAsync();
        }
    }
}