namespace Console.Module.Email.Configurations
{
    public class EmailConfig
    {
        public string Provider { get; set; }
        public SmtpConfig Smtp { get; set; }
    }

    public class SmtpConfig
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
