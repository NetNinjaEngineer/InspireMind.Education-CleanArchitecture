using InspireMind.Education.Application.Contracts.Persistence;
using InspireMind.Education.Application.Specifications;
using InspireMind.Education.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace InspireMind.Education.Persistence.Repos;

public class GenericRepository<TEntity>(ApplicationDbContext context) : IGenericRepository<TEntity> where TEntity : BaseEntity
{
    protected readonly ApplicationDbContext _context = context;

    public async Task<IReadOnlyList<TEntity>> GetAllAsync() => await _context.Set<TEntity>().ToListAsync();

    public async Task<TEntity?> GetEntityAsync(Guid id) => await _context.Set<TEntity>().FindAsync(id);

    public void Create(TEntity entity) => _context.Set<TEntity>().Add(entity);

    public void Delete(TEntity entity) => _context.Set<TEntity>().Remove(entity);

    public void Update(TEntity entity) => _context.Set<TEntity>().Update(entity);

    public async Task<TEntity?> GetEntityWithSpecificationAsync(ISpecification<TEntity> specification)
        => await SpecificationQueryEvaluator<TEntity>.GetQuery(_context.Set<TEntity>(), specification).FirstOrDefaultAsync();

    public async Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specification)
        => await SpecificationQueryEvaluator<TEntity>.GetQuery(_context.Set<TEntity>(), specification).ToListAsync();

    public async Task<TEntity?> GetEntityByIdWithSpecificationAsync(Guid id, ISpecification<TEntity> specification)
        => await SpecificationQueryEvaluator<TEntity>.GetQuery(_context.Set<TEntity>(), specification).FirstOrDefaultAsync(x => x.Id == id);

    public async Task<int> CountWithSpecificationAsync(ISpecification<TEntity> specification)
        => await SpecificationQueryEvaluator<TEntity>.GetQuery(_context.Set<TEntity>(), specification).CountAsync();
}
