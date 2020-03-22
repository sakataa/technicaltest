using System;

public interface IEmailService
{
    bool SendMail(EmailParam emailParam);
}