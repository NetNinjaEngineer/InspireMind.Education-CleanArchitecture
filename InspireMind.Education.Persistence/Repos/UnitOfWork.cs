using InspireMind.Education.Application.Contracts.Persistence;
using InspireMind.Education.Domain.Entities.Common;
using System.Collections;

namespace InspireMind.Education.Persistence.Repos;

public sealed class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{
    private Hashtable _repositories = [];
    private ITopicRepository _topicRepository;

    public ITopicRepository TopicRepository => _topicRepository ??= new TopicRepository(context);

    public async ValueTask DisposeAsync() => await context.DisposeAsync();

    public IGenericRepository<T>? Repository<T>() where T : BaseEntity
    {
        var typeName = typeof(T).Name;
        if (!_repositories.ContainsKey(typeName))
        {
            var repository = new GenericRepository<T>(context);
            _repositories.Add(typeName, repository);
            return repository;
        }

        return _repositories[typeName] as IGenericRepository<T>;

    }

    public async Task<int> SaveAsync() => await context.SaveChangesAsync();
}
