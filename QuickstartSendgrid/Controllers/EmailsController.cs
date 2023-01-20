namespace QuickstartSendgrid.Controllers;

[ApiController]
[Route("[controller]")]
public class EmailsController : ControllerBase
{
    private readonly SendEmailsService services;

    public EmailsController(SendEmailsService service)
    {
        this.services = service;
    }

    [HttpPost(Name = nameof(Send))]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> Send([FromForm] SendEmailRequest request)
    {
        await services.Send(request);
        return NoContent();
    }
}