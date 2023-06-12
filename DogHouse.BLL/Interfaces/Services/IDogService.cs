using DogHouse.Common.Models;

namespace DogHouse.BLL.Interfaces.Services;

public interface IDogService
{
    Task<IEnumerable<Dog>> GetAll(string attribute, string order, int? pageNumber, int? pageSize);
    Task<Dog> CreateDog(Dog dog);
}