﻿namespace HIPO.Infrastructure;

public class EmailSettings
{
    public string SmtpServer { get; set; } = string.Empty;
    public int Port { get; set; }
    public string FromEmail { get; set; } = string.Empty;
    public string AppPassword { get; set; } = string.Empty;
    public bool EnableSsl { get; set; }
}

