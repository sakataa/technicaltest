using System;

public interface IContactPersonManager
{
    IEnumerable<string> GetEmailList(IList<ContactPerson> contactPersonList, string municipality);

    IList<ContactPerson> InitDataList();
}