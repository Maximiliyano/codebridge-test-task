using DogHouse.BLL.BaseEntities;
using DogHouse.BLL.Interfaces.Repositories;
using DogHouse.Common.Models;
using DogHouse.DAL.Context;

namespace DogHouse.BLL.Repositories;

public class DogRepository : BaseRepository<Dog>, IDogRepository
{
    private readonly DogHouseDbContext _context;
    
    public DogRepository(DogHouseDbContext context) : base(context)
    {
        _context = context;
    }
}