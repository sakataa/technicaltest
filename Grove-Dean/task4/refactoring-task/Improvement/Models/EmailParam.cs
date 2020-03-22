using System;

public class EmailParam
{
    public string ToAddresses { get; set; }

    public string Subject { get; set; }

    public string Content { get; set; }

    public string FromAdress { get; set; }

    public string BccAddress { get; set; }

    public Attachment[] AttachmentList { get; set; }

    public bool IsBodyHtml { get; set; }
}