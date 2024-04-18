using APBD_06.Enums;
using APBD_06.Services;
using Microsoft.AspNetCore.Http.HttpResults;
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
}