namespace QuickstartSendgrid.Models;

public record SendEmailRequest(
    IEnumerable<string> Tos,
    string Subject,
    string Body,
    IEnumerable<IFormFile>? Attachments
);