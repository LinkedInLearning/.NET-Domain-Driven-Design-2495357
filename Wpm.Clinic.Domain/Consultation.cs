using Wpm.Clinic.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain;

public class Consultation : AggregateRoot
{
    public DateTime ConsultationStart { get; init; }
    public DateTime? ConsultationEnd { get; set; }
    public PatientId PatientId { get; init; }
    public Text Diagnosis { get; private set; }
    public Text Treatment { get; private set; }
    public Weight CurrentWeight { get; private set; }
    public ConsultationStatus Status { get; private set; }

    public Consultation(PatientId patientId)
    {
        Id = Guid.NewGuid();
        PatientId = patientId;
        Status = ConsultationStatus.Started;
        ConsultationStart = DateTime.UtcNow;
    }

    public void SetWeight(Weight weight)
    {
        ValidateConsultationStatus();
        CurrentWeight = weight;
    }

    public void SetDiagnosis(Text diagnosis)
    {
        ValidateConsultationStatus();
        Diagnosis = diagnosis;
    }

    public void SetTreatment(Text treatment)
    {
        ValidateConsultationStatus();
        Treatment = treatment;
    }

    public void End()
    {
        ValidateConsultationStatus();

        if (Diagnosis == null || Treatment == null || CurrentWeight == null)
        {
            throw new InvalidOperationException("La consulta no puede ser finalizada.");
        }

        Status = ConsultationStatus.Finalized;
        ConsultationEnd = DateTime.UtcNow;
    }

    private void ValidateConsultationStatus()
    {
        if (Status == ConsultationStatus.Finalized)
        {
            throw new InvalidOperationException("La consulta ya está finalizada.");
        }
    }
}

public enum ConsultationStatus
{
    Started,
    Finalized
}