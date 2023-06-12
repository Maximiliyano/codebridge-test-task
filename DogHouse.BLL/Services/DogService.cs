using AutoMapper;
using DogHouse.BLL.BaseEntities;
using DogHouse.BLL.Interfaces.Repositories;
using DogHouse.BLL.Interfaces.Services;
using DogHouse.Common.Models;
using Microsoft.EntityFrameworkCore;

namespace DogHouse.BLL.Services;

public class DogService : BaseService<IDogRepository, Dog>, IDogService
{
    public DogService(
        IDogRepository repository, 
        IMapper mapper) : base(repository, mapper)
    {
    }

    public async Task<IEnumerable<Dog>> GetAll(string attribute, string order, int? pageNumber, int? pageSize)
    {
        var dogsQuery = await _repository.GetAllAsync();
        
        if (!string.IsNullOrEmpty(attribute))
        {
            dogsQuery = ApplySorting(dogsQuery, attribute, order);
        }

        if (pageNumber.HasValue && pageSize.HasValue)
        {
            dogsQuery = ApplyPagination(dogsQuery, pageNumber.Value, pageSize.Value);
        }
        
        return dogsQuery;
    }

    public async Task<Dog> CreateDog(Dog dog)
    {
        if (string.IsNullOrEmpty(dog.Name) || string.IsNullOrEmpty(dog.Color))
        {
            throw new Exception("Name and Color are required fields."); // BadRequest
        }

        if (await _repository.FirstOrDefaultAsync(d => d.Name == dog.Name) is not null)
        {
            throw new Exception("A dog with the same name already exists."); // Conflict
        }

        if (dog.TailLength <= 0 || dog.Weight <= 0)
        {
            throw new Exception("Tail length and weight must be positive numbers."); // BadRequest
        }

        try
        {
            await _repository.CreateAsync(dog);
        }
        catch (DbUpdateException)
        {
            Console.WriteLine("An error occurred while saving the dog."); // 500 error
        }

        var response = await _repository.FirstOrDefaultAsync(x => x.Name == dog.Name);
        return response;
    }
    
    private static IEnumerable<Dog> ApplySorting(IEnumerable<Dog> query, string attribute, string order)
    {
        return attribute.ToLower() switch
        {
            "name" => order.ToLower() == "desc" ? query.OrderByDescending(d => d.Name) : query.OrderBy(d => d.Name),
            "color" => order.ToLower() == "desc" ? query.OrderByDescending(d => d.Color) : query.OrderBy(d => d.Color),
            "tail_length" => order.ToLower() == "desc"
                ? query.OrderByDescending(d => d.TailLength)
                : query.OrderBy(d => d.TailLength),
            "weight" => order.ToLower() == "desc"
                ? query.OrderByDescending(d => d.Weight)
                : query.OrderBy(d => d.Weight),
            _ => query
        };
    }

    private static IEnumerable<Dog> ApplyPagination(IEnumerable<Dog> query, int pageNumber, int pageSize)
    {
        return query.Skip((pageNumber - 1) * pageSize).Take(pageSize);
    }
}