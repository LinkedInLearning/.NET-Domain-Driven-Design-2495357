using Wpm.Clinic.Api.Commands;
using Wpm.Clinic.Api.Infrastructure;
using Wpm.Clinic.Domain;

namespace Wpm.Clinic.Api.Application;

public class ClinicApplicationService(ClinicDbContext dbContext)
{
    public async Task Handle(StartConsultationCommand command)
    {
        var newConsultation = new Consultation(command.PatientId);
        await dbContext.Consultations.AddAsync(newConsultation);
        await dbContext.SaveChangesAsync();
    }
}