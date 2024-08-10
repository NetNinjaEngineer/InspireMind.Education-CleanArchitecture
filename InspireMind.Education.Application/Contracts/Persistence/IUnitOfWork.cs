using InspireMind.Education.Domain.Entities.Common;

namespace InspireMind.Education.Application.Contracts.Persistence;

public interface IUnitOfWork : IAsyncDisposable
{
    public ITopicRepository TopicRepository { get; }
    IGenericRepository<T>? Repository<T>() where T : BaseEntity;
    Task<int> SaveAsync();
}
