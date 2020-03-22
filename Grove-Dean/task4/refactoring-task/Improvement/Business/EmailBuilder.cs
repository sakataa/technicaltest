using System;

public class EmailBuilder
{
    private MailMessage _mailMessage;

    public EmailBuilder()
    {
        _mailMessage = new MailMessage();
    }

    public EmailBuilder BuildFromAddress(string sender)
    {
        var from = new MailAddress(sender, sender);
        _mailMessage.From = from;

        return this;
    }

    public EmailBuilder BuildSubject(string subject)
    {
        _mailMessage.Subject = subject;

        return this;
    }

    public EmailBuilder BuildReceiver(string toAddresses)
    {
        var receiverList = new MailAddressCollection();

        if (toAddresses.Contains(";"))
        {
            string[] addresses = toAddresses.Split(';');

            foreach (string s in addresses)
            {
                if (!s.StartsWith(";"))
                {
                    receiverList.Add(s);
                }
            }
        }
        else
        {
            receiverList.Add(toAddresses);
        }

        foreach (MailAddress attendee in receiverList)
        {
            _mailMessage.To.Add(attendee);
        }

        return this;
    }

    /// <summary>
    /// Builds the content of the email body
    /// TODO: Gather required dependencies to pass into method
    /// </summary>
    /// <returns>EmailBuilder</returns>
    public EmailBuilder BuildBody()
    {
        // pass dependencies to build email content
        _mailMessage.Body = BuildEmailContent();

        return this;
    }

    public EmailBuilder BuildBcc(string bccAddress)
    {
        if (!string.IsNullOrEmpty(bccAddress))
        {
            _mailMessage.Bcc = bccAddress;
        }

        return this;
    }

    public EmailBuilder BuildAttachments(Attachment[] attachmentList)
    {
        if (attachmentList != null)
        {
            foreach (Attachment attachment in attachmentList)
            {
                if (attachment != null)
                {
                    _mailMessage.Attachments.Add(attachment);
                }
            }
        }

        return this;
    }

    public MailMessage GetMailMessage()
    {
        return _mailMessage;
    }

    private string BuildEmailContent()
    {
        const string SummaryStart = "<table>";
        const string SummaryEnd = "</table>";
        const string ContentStart = "<html>";
        const string ContentEnd = "</html>";
        const string LabelElementStart = "<tr><td><strong>";
        const string LabelElementEnd = "</strong></td>";
        const string ValueElementStart = "<td>";
        const string ValueElementEnd = "</td></tr>";
        const string LabelElementFullWidthStart = "<tr><td colspan=\"2\"><strong>";
        const string LabelElementFullWidthEnd = "</strong></td></tr>";
        const string ValueElementFullWidthStart = "<tr><td colspan=\"2\">";
        const string ValueElementFullWidthEnd = "</td></tr>";

        StringBuilder stringBuilder = new StringBuilder();
        stringBuilder.AppendLine(ContentStart);
        stringBuilder.AppendLine(PropertyService.GetStringProperty(CurrentPage, "EmailHeader"));
        stringBuilder.AppendLine(SummaryStart);
        stringBuilder.AppendLine(LabelElementStart + GetLanguageString("/applicationform/county") + LabelElementEnd + ValueElementStart + Ddl_County.SelectedValue + ValueElementEnd);
        stringBuilder.AppendLine(LabelElementStart + GetLanguageString("/applicationform/municipality") + LabelElementEnd + ValueElementStart + Ddl_Municipality.SelectedItem + ValueElementEnd);
        stringBuilder.AppendLine(LabelElementStart + GetLanguageString("/applicationform/applicator") + LabelElementEnd + ValueElementStart + Txt_Applicator.Text + ValueElementEnd);
        stringBuilder.AppendLine(LabelElementStart + GetLanguageString("/applicationform/address") + LabelElementEnd + ValueElementStart + Txt_Address.Text + ValueElementEnd);
        stringBuilder.AppendLine(LabelElementStart + GetLanguageString("/applicationform/postcode") + " / " + GetLanguageString("/applicationform/postarea") + LabelElementEnd + ValueElementStart + Txt_PostCode.Text + " " + Txt_PostArea.Text + ValueElementEnd);
        stringBuilder.AppendLine(LabelElementStart + GetLanguageString("/applicationform/orgnobirthnumber") + LabelElementEnd + ValueElementStart + Txt_OrgNoBirthNumber.Text + ValueElementEnd);
        stringBuilder.AppendLine(LabelElementStart + GetLanguageString("/applicationform/contactperson") + LabelElementEnd + ValueElementStart + Txt_ContactPerson.Text + ValueElementEnd);
        stringBuilder.AppendLine(LabelElementStart + GetLanguageString("/applicationform/phone") + LabelElementEnd + ValueElementStart + Txt_Phone.Text + ValueElementEnd);
        stringBuilder.AppendLine(LabelElementStart + GetLanguageString("/applicationform/email") + LabelElementEnd + ValueElementStart + Txt_Email.Text + ValueElementEnd);
        stringBuilder.AppendLine(LabelElementFullWidthStart + GetLanguageString("/applicationform/description") + LabelElementFullWidthEnd + ValueElementFullWidthStart + Txt_Description.Text + ValueElementFullWidthEnd);
        stringBuilder.AppendLine(LabelElementFullWidthStart + GetLanguageString("/applicationform/financeplan") + LabelElementFullWidthEnd + ValueElementFullWidthStart + Txt_FinancePlan.Text + ValueElementFullWidthEnd);
        stringBuilder.AppendLine(LabelElementFullWidthStart + GetLanguageString("/applicationform/businessdescription") + LabelElementFullWidthEnd + ValueElementFullWidthStart + Txt_BusinessDescription.Text + ValueElementFullWidthEnd);
        stringBuilder.AppendLine(LabelElementStart + GetLanguageString("/applicationform/applicationAmount") + LabelElementEnd + ValueElementStart + Txt_ApplicationAmount.Text + ValueElementEnd);
        stringBuilder.AppendLine(SummaryEnd);
        stringBuilder.AppendLine(PropertyService.GetStringProperty(CurrentPage, "EmailFooter"));
        stringBuilder.AppendLine(ContentEnd);

        return stringBuilder.ToString();
    }
}