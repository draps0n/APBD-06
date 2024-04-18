using APBD_06.Enums;
using APBD_06.Models;
using APBD_06.Repositories;

namespace APBD_06.Services;

public class AnimalService(IAnimalRepository animalRepository) : IAnimalService
{
    private IAnimalRepository _animalRepository = animalRepository;

    public IEnumerable<Animal> GetAnimals(OrderBy? orderBy)
    {
        var animals = _animalRepository.GetAnimals(orderBy);
        return animals;
    }
}