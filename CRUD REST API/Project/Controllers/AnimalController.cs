using Cwiczenia6;
using Microsoft.AspNetCore.Mvc;
namespace Cwiczenia_5.Properties.Controllers;

[Route("api/animals")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private IAnimalRepository _animalRepository;
    
    public AnimalsController(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
    }
    
    [HttpGet]
    public IActionResult GetAnimals(string orderBy = "Name")
    {
        if (orderBy != "Name" && orderBy != "Description" && orderBy != "Category" && orderBy != "Area")
        {
            return BadRequest();
        }
        
        List<Animal> sorted = _animalRepository.GetAnimalsFromDatabase(orderBy).ToList();
        return Ok(sorted);
    }
    
    [HttpPost]
    public IActionResult AddAnimal([FromBody] Animal animal)
    {
        if (animal == null)
        {
            return BadRequest("Animal is null.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        _animalRepository.AddAnimalToDatabase(animal);
        return Created();
    }
    

    [HttpDelete("{idAnimal}")]
    public IActionResult DeleteAnimal(int idAnimal)
    {
        var existingAnimal = _animalRepository.GetAnimalByIdFromDatabase(idAnimal);

        if (existingAnimal == null)
        {
            return NotFound();
        }

        _animalRepository.DeleteAnimalFromDatabase(idAnimal);
        return NoContent();
    }

    [HttpPut("{idAnimal}")]
    public IActionResult UpdateAnimal(int idAnimal, [FromBody] Animal animal)
    {
        if (animal == null || idAnimal != animal.IdAnimal)
        {
            return BadRequest("Invalid animal data.");
        }

        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var existingAnimal = _animalRepository.GetAnimalByIdFromDatabase(idAnimal);

        if (existingAnimal == null)
        {
            return NotFound();
        }

        _animalRepository.UpdateAnimalInDatabase(animal);
        return NoContent();
    }
    
}