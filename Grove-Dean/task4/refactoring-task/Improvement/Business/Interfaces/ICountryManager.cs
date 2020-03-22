using System;

public interface ICountryManager
{
    string[] GetCountryList();

    IDictionary<string, string> GetMunicipalityList(string country, IList<ContactPersonList> contactPersonList);
}