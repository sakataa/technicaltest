using System;

public static class RequestHelper
{
    /// <summary>
    /// Returns a list of selected Attachments
    /// </summary>
    /// <returns></returns>
    public static Attachment[] GetAttachments(HttpRequest request, ApplicationPath path)
    {
        List<Attachment> attachmentList = new List<Attachment>();

        foreach (string postedInputName in request.Files)
        {
            var postedFile = request.Files[postedInputName];

            if (postedFile != null && postedFile.ContentLength > 0)
            {
                string fileName = path.GetFileName(postedFile.FileName);
                if (fileName != string.Empty)
                {
                    Attachment newAttachment = new Attachment(postedFile.InputStream, fileName, postedFile.ContentType);
                    attachmentList.Add(newAttachment);
                }
            }
        }

        return attachmentList.ToArray();
    }
}