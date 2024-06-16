namespace Cwiczenia6;

public interface IAnimalRepository
{
    IEnumerable<Animal> GetAnimalsFromDatabase(string orderBy);
    Animal GetAnimalByIdFromDatabase(int idAnimal);
    void AddAnimalToDatabase(Animal animal);
    void UpdateAnimalInDatabase(Animal animal);
    void DeleteAnimalFromDatabase(int idAnimal);
}