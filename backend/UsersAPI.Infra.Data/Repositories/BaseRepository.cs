using UsersAPI.Domain.Interfaces.Repositories;
using UsersAPI.Infra.Data.Contexts;

namespace UsersAPI.Infra.Data.Repositories
{
  public abstract class BaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey>
      where TEntity : class
  {
    private readonly DataContext? _dataContext;

    protected BaseRepository(DataContext? dataContext)
    {
      _dataContext = dataContext;
    }

    public virtual void Add(TEntity entity)
    {
      _dataContext?.Add(entity);
    }

    public virtual void Update(TEntity entity)
    {
      _dataContext?.Update(entity);
    }

    public virtual void Delete(TEntity entity)
    {
      _dataContext?.Remove(entity);
    }

    public virtual List<TEntity> GetAll()
    {
      return _dataContext?.Set<TEntity>().ToList();
    }

    public virtual TEntity? GetById(TKey id)
    {
      return _dataContext?.Set<TEntity>().Find(id);
    }

    public virtual TEntity? Get(Func<TEntity, bool> where)
    {
      return _dataContext?.Set<TEntity>().FirstOrDefault(where);
    }

    public virtual void Dispose()
    {
      _dataContext?.Dispose();
    }
  }
}
