using InspireMind.Education.Application.Specifications;
using InspireMind.Education.Domain.Entities.Common;

namespace InspireMind.Education.Application.Contracts.Persistence;

public interface IGenericRepository<TEntity> where TEntity : BaseEntity
{
    Task<IReadOnlyList<TEntity>> GetAllAsync();
    Task<TEntity?> GetEntityAsync(Guid id);
    void Create(TEntity entity);
    void Update(TEntity entity);
    void Delete(TEntity entity);
    Task<TEntity?> GetEntityWithSpecificationAsync(ISpecification<TEntity> specification);
    Task<TEntity?> GetEntityByIdWithSpecificationAsync(Guid id, ISpecification<TEntity> specification);
    Task<IReadOnlyList<TEntity>> GetAllWithSpecificationAsync(ISpecification<TEntity> specification);
    Task<int> CountWithSpecificationAsync(ISpecification<TEntity> specification);
}
