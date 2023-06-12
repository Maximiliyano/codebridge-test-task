using DogHouse.BLL.Interfaces.Services;
using DogHouse.Common.BaseEntities;
using DogHouse.Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace DogHouse.Api.Controllers;

[ApiController]
[Route("api/dog")]
public class DogController : BaseController
{
    private readonly IDogService _dogService;
    
    public DogController(IDogService dogService)
    {
        _dogService = dogService;
    }

    [HttpPost]
    public async Task<IActionResult> Create(Dog dog)
    {
        var request = await _dogService.CreateDog(dog);
        return CreatedAtAction(nameof(GetAll), new { id = request.Id }, request);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(
        [FromQuery] string attribute,
        [FromQuery] string order,
        [FromQuery] int? pageNumber,
        [FromQuery] int? pageSize)
    {
        return Ok(await _dogService.GetAll(attribute, order, pageNumber, pageSize));
    }
}