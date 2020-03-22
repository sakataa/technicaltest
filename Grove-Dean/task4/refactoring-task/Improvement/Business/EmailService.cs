using System;

public class EmailService
{
    private EmailBuilder _emailBuilder = new EmailBuilder();

    /// <summary>
    /// Sends an email
    /// </summary>
    /// <param name="emailParam">emailParam</param>
    /// <returns></returns>
    public bool SendMail(EmailParam emailParam)
    {
        MailMessage mail = BuildMail(emailParam);
        if (!IsValidEmail(mail))
        {
            return false;
        }

        SmtpClient smtp = new SmtpClient();
        mail.IsBodyHtml = emailParam.IsBodyHtml;
        try
        {
            if (mail.To.Any(to => !StringValidationUtil.IsValidEmailAddress(to.Address)))
            {
                return false;
            }

            smtp.Send(mail);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    /// <summary>
    /// Builds the MailMessage.
    /// TODO: Need to pass more params for building content
    /// </summary>
    /// <param name="emailParam">EmailParam object</param>
    /// <returns>MailMessage</returns>
    private MailMessage BuildMail(EmailParam emailParam)
    {
        var mailMessage = _emailBuilder.BuildFromAddress(emailParam.FromAdress)
                                    .BuildSubject(emailParam.Subject)
                                    .BuildReceiver(emailParam.ToAddresses)
                                    .BuildBody()
                                    .BuildBcc(emailParam.BccAddress)
                                    .BuildAttachments(emailParam.AttachmentList)
                                    .GetMailMessage();

        return mailMessage;
    }

    private bool IsValidEmail(MailMessage mail)
    {
        return mail.To.Count > 0 && mail.From.ToString().Length > 0 && mail.Subject.Length > 0;
    }
}