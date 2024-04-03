using Wpm.Clinic.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain;

public class Consultation : AggregateRoot
{
    public PatientId PatientId { get; init; }
    public Text Diagnosis { get; private set; }
    public Text Treatment { get; private set; }
    public Weight CurrentWeight { get; private set; }
    public ConsultationStatus Status { get; private set; }

    public Consultation(PatientId patientId)
    {
        Id = Guid.NewGuid();
        PatientId = patientId;
    }

    public void SetWeight(Weight weight)
    {
        CurrentWeight = weight;
    }
}

public enum ConsultationStatus
{
    Open,
    Closed
}