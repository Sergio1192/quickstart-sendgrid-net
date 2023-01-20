namespace QuickstartSendgrid.Models;

public class SmtpOptions : OptionsBase
{
    public class SmtpFrom
    {
        public string Email { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
    }
    
    public bool Active { get; set; }
    public string ApiKey { get; set; } = string.Empty;
    public SmtpFrom From { get; set; } = default!;
    public string SendAllEmailsTo { get; set; } = string.Empty;
}
