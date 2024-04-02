namespace Wpm.Management.Domain;
public record Weight
{
    public decimal Value { get; init; }
    public Weight(decimal value)
    {
        if (value < 0)
        {
            throw new ArgumentException("Peso no es válido");
        }
        Value = value;
    }
}