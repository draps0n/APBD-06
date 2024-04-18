using APBD_06.Enums;
using APBD_06.Models;

namespace APBD_06.Repositories;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAnimals(OrderBy? orderBy);
}