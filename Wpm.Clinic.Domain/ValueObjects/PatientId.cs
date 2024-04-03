namespace Wpm.Clinic.Domain.ValueObjects;
public record PatientId
{
    public Guid Value { get; init; }
    public PatientId(Guid value)
    {
        if (value == Guid.Empty)
        {
            throw new ArgumentNullException("value", "El identificador no es válido.");
        }
        Value = value;
    }

    public static implicit operator PatientId(Guid value)
    {
        return new PatientId(value);
    }
}