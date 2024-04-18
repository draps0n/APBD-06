using APBD_06.DTOs;
using APBD_06.Enums;
using APBD_06.Repositories;

namespace APBD_06.Services;

public class AnimalService(IAnimalRepository animalRepository) : IAnimalService
{
    private IAnimalRepository _animalRepository = animalRepository;

    public IEnumerable<GetAnimalsResponse> GetAnimals(OrderBy? orderBy)
    {
        var animals = _animalRepository.GetAnimals(orderBy);
        return animals;
    }

    public int AddAnimal(CreateAnimalRequest animalRequest)
    {
        return _animalRepository.AddAnimal(animalRequest);
    }

    public int UpdateAnimal(int id, ReplaceAnimalRequest animalRequest)
    {
        return _animalRepository.UpdateAnimal(id, animalRequest);
    }

    public int DeleteAnimal(int id)
    {
        return _animalRepository.DeleteAnimal(id);
    }
}