using APBD_06.DTOs;
using APBD_06.Enums;
using APBD_06.Services;
using Microsoft.AspNetCore.Mvc;

namespace APBD_06.Controllers;

[Route("/api/animals")]
[ApiController]
public class AnimalsController(IAnimalService animalService) : ControllerBase
{
    private IAnimalService _animalService = animalService;

    [HttpGet]
    public IActionResult GetAnimals(string? orderBy)
    {
        if (orderBy is null)
        {
            var animals = _animalService.GetAnimals(OrderBy.Name);
            return Ok(animals);
        }

        if (Enum.TryParse(orderBy, true, out OrderBy orderByEnum))
        {
            var animals = _animalService.GetAnimals(orderByEnum);
            return Ok(animals);
        }

        return BadRequest();
    }

    [HttpPost]
    public IActionResult AddAnimal([FromBody] CreateAnimalRequest animalRequest)
    {
        _animalService.AddAnimal(animalRequest);
        return StatusCode(StatusCodes.Status201Created);
    }

    [HttpPut("{id:int}")]
    public IActionResult UpdateAnimal(int id, [FromBody] ReplaceAnimalRequest animalRequest)
    {
        _animalService.UpdateAnimal(id, animalRequest);
        return NoContent();
    }
    
    [HttpDelete("{id:int}")]
    public IActionResult DeleteAnimal(int id)
    {
        _animalService.DeleteAnimal(id);
        return NoContent();
    }
}