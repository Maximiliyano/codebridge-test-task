using AutoMapper;
using DogHouse.BLL.Interfaces.Repositories;
using DogHouse.Common.BaseEntities;

namespace DogHouse.BLL.BaseEntities;

public abstract class BaseService<TRepository, TEntity>
    where TRepository : IRepository<TEntity>
    where TEntity : BaseModel
{
    protected readonly IMapper _mapper;

    protected readonly TRepository _repository;

    protected BaseService(TRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    protected Task Create(TEntity model)
    {
        return _repository.CreateAsync(model);
    }

    protected Task Update(TEntity model)
    {
        model.UpdatedAt = DateTime.Now;

        return _repository.UpdateAsync(model);
    }

    protected Task Delete(TEntity model)
    {
        return _repository.DeleteAsync(model);
    }
}