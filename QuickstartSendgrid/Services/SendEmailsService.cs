using Microsoft.Extensions.Options;
using SendGrid;
using SendGrid.Helpers.Mail;
using System.Net.Mail;

namespace QuickstartSendgrid.Services;

public class SendEmailsService
{
    private readonly SmtpOptions options;
    private readonly ISendGridClient client;

    public SendEmailsService(IOptionsSnapshot<SmtpOptions> options, ISendGridClient client)
    {
        this.options = options.Value;
        this.client = client;
    }

    public async Task Send(SendEmailRequest email)
    {
        if (!options.Active)
            return;
        
        var mail = new SendGridMessage();
        mail.SetFrom(new EmailAddress(options.From.Email, options.From.Name));

        IEnumerable<EmailAddress> tos;
        if (string.IsNullOrEmpty(options.SendAllEmailsTo))
        {
            tos = GetRecipients(email.Tos);
        }
        else
        {
            tos = GetRecipients(options.SendAllEmailsTo.Split(';'));  
        }

        if (!tos.Any())
            return;

        mail.AddTos(tos.ToList());
        mail.SetSubject(email.Subject);

        var content = $"<p>{email.Body.Replace("\r\n", "<br/>")}</p>";
        content += "<p style=\"display:none;\">™</p>"; // Hack to allow character ñ (and others) in attachments filename
        mail.AddContent(MimeType.Html, content);

        var attachments = email.Attachments ?? Enumerable.Empty<IFormFile>();
        foreach (var attachment in attachments)
        {
            using var ms = new MemoryStream();
            attachment.CopyTo(ms);
            var fileBytes = ms.ToArray();
            mail.AddAttachment(attachment.FileName, Convert.ToBase64String(fileBytes));
        }

        await client.SendEmailAsync(mail);
    }

    private static IEnumerable<EmailAddress> GetRecipients(IEnumerable<string> addresses)
        => addresses
            .Select(address => new EmailAddress(address.Trim()))
            .DistinctBy(a => a.Email);
}
