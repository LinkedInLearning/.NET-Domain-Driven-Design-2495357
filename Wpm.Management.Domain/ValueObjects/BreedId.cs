using Wpm.Management.Domain.Services;

namespace Wpm.Management.Domain.ValueObjects;
public record BreedId
{
    private readonly IBreedService breedService;

    public Guid Value { get; init; }

    public BreedId(Guid value, IBreedService breedService)
    {
        this.breedService = breedService;
        ValidateBreed(value);
        Value = value;
    }

    private BreedId(Guid value)
    {
        Value = value;
    }

    public static BreedId Create(Guid value)
    {
        return new BreedId(value);
    }

    private void ValidateBreed(Guid value)
    {
        if (breedService.GetBreed(value) == null)
        {
            throw new ArgumentException("La raza no es válida.");
        }
    }
}