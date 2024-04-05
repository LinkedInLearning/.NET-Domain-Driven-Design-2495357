using Wpm.Clinic.Domain.ValueObjects;
using Wpm.SharedKernel;

namespace Wpm.Clinic.Domain;

public class Consultation : AggregateRoot
{
    private readonly List<DrugAdministration> administeredDrugs = new();
    private readonly List<VitalSigns> vitalSignReadings = new();
    public DateTime ConsultationStart { get; init; }
    public DateTime? ConsultationEnd { get; private set; }
    public PatientId PatientId { get; init; }
    public Text? Diagnosis { get; private set; }
    public Text? Treatment { get; private set; }
    public Weight? CurrentWeight { get; private set; }
    public ConsultationStatus Status { get; private set; }
    public IReadOnlyCollection<DrugAdministration> AdministeredDrugs => administeredDrugs;
    public IReadOnlyCollection<VitalSigns> VitalSignReadings => vitalSignReadings;
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

    public void AdministerDrug(DrugId drugId, Dose dose)
    {
        ValidateConsultationStatus();
        var newDrugAdministration = new DrugAdministration(drugId, dose);
        administeredDrugs.Add(newDrugAdministration);
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

    public void RegisterVitalSigns(IEnumerable<VitalSigns> vitalSigns)
    {
        ValidateConsultationStatus();
        vitalSignReadings.AddRange(vitalSigns);
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