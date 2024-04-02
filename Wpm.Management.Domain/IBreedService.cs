using Wpm.Management.Domain.ValueObjects;

namespace Wpm.Management.Domain;
public interface IBreedService
{
    Breed? GetBreed(Guid id);
}

public class FakeBreedService : IBreedService
{
    public readonly List<Breed> breeds =
       [
            new Breed (Guid.NewGuid(), "Beagle", new WeightRange(10m, 11m), new WeightRange (9m, 10m)),
            new Breed (Guid.NewGuid(), "Staffordshire Terrier", new WeightRange(28m, 40m), new WeightRange (24m,34m))
       ];

    public Breed? GetBreed(Guid id)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException("La raza no es válida.");
        }
        var result = breeds.Find(breeds => breeds.Id == id);
        return result ?? throw new ArgumentException("La raza no se encontró.");
    }
}
