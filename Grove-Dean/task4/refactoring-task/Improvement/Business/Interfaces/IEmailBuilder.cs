using System;

public interface IEmailBuilder
{
    EmailBuilder BuildFromAddress(string sender);

    EmailBuilder BuildSubject(string subject);

    EmailBuilder BuildReceiver(string toAddresses);

    EmailBuilder BuildBody();

    EmailBuilder BuildBcc(string bccAddress);

    EmailBuilder BuildAttachments(Attachment[] attachmentList);

    MailMessage GetMailMessage();
}