using Wpm.Clinic.Domain.Entities;
using Wpm.Clinic.Domain.ValueObjects;

namespace Wpm.Clinic.Domain.Tests;

public class UnitTest1
{
    [Fact]
    public void Consulta_debe_estar_iniciada()
    {
        var c = new Consultation(Guid.NewGuid());
        Assert.True(c.Status == ConsultationStatus.Started);
    }

    [Fact]
    public void Consulta_no_debe_tener_fecha_de_finalizacion()
    {
        var c = new Consultation(Guid.NewGuid());
        Assert.Null(c.ConsultationEnd);
    }

    [Fact]
    public void Consulta_no_debe_finalizar_si_faltan_datos()
    {
        var c = new Consultation(Guid.NewGuid());
        Assert.Throws<InvalidOperationException>(c.End);
    }

    [Fact]
    public void Consulta_debe_finalizar_con_datos_completos()
    {
        var c = new Consultation(Guid.NewGuid());
        c.SetTreatment("Tratamiento");
        c.SetDiagnosis("Diagnóstico");
        c.SetWeight(18.5m);
        c.End();
        Assert.True(c.Status == ConsultationStatus.Finalized);
    }
    
    [Fact]
    public void Consulta_no_debe_permitir_cambiar_peso_cuando_esta_finalizada()
    {
        var c = new Consultation(Guid.NewGuid());
        c.SetTreatment("Tratamiento");
        c.SetDiagnosis("Diagnóstico");
        c.SetWeight(18.5m);
        c.End();
        Assert.Throws<InvalidOperationException>(()=> c.SetWeight(19.2m));
    }

    [Fact]
    public void Consulta_no_debe_permitir_cambiar_diagnostico_cuando_esta_finalizada()
    {
        var c = new Consultation(Guid.NewGuid());
        c.SetTreatment("Tratamiento");
        c.SetDiagnosis("Diagnóstico");
        c.SetWeight(18.5m);
        c.End();
        Assert.Throws<InvalidOperationException>(() => c.SetDiagnosis("Nuevo diagnóstico"));
    }

    [Fact]
    public void Consulta_no_debe_permitir_cambiar_tratamiento_cuando_esta_finalizada()
    {
        var c = new Consultation(Guid.NewGuid());
        c.SetTreatment("Tratamiento");
        c.SetDiagnosis("Diagnóstico");
        c.SetWeight(18.5m);
        c.End();
        Assert.Throws<InvalidOperationException>(() => c.SetTreatment("Nuevo tratamiento"));
    }

    [Fact]
    public void Consulta_agrega_medicamento()
    {
        var drugId = new DrugId(Guid.NewGuid());
        var c = new Consultation(Guid.NewGuid());
        c.AdministerDrug(drugId, new Dose(1, Dose.UnitOfMeasure.tablet));
        Assert.True(c.AdministeredDrugs.Count == 1);
        Assert.True(c.AdministeredDrugs.First().DrugId == drugId);
    }

    [Fact]
    public void Consulta_agrega_lectura_signos_vitales()
    {
        var c = new Consultation(Guid.NewGuid());
        IEnumerable<VitalSigns> vitalSigns = [new VitalSigns(38.8m, 100, 120)];
        c.RegisterVitalSigns(vitalSigns);
        Assert.True(c.VitalSignReadings.Count == 1);
        Assert.True(c.VitalSignReadings.First() == vitalSigns.First());
    }
}