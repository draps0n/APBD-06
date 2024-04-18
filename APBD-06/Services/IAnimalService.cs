using APBD_06.Enums;
using APBD_06.Models;

namespace APBD_06.Services;

public interface IAnimalService
{
    IEnumerable<Animal> GetAnimals(OrderBy? orderBy);
}