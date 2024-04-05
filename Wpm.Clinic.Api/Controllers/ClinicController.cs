using Microsoft.AspNetCore.Mvc;
using Wpm.Clinic.Api.Application;
using Wpm.Clinic.Api.Commands;

namespace Wpm.Clinic.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class ClinicController(ClinicApplicationService applicationService) : ControllerBase
{
    [HttpPost]
    public async Task<ActionResult> Post(StartConsultationCommand command)
    {
        await applicationService.Handle(command);
        return Ok();
    }
}