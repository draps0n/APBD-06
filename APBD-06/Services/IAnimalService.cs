using APBD_06.DTOs;
using APBD_06.Enums;

namespace APBD_06.Services;

public interface IAnimalService
{
    IEnumerable<GetAnimalsResponse> GetAnimals(OrderBy? orderBy);
    int AddAnimal(CreateAnimalRequest animalRequest);
    int UpdateAnimal(int id, ReplaceAnimalRequest animalRequest);
    int DeleteAnimal(int id);
}