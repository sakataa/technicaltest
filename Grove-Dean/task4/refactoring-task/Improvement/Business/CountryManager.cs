using System;

public class CountryManager
{
    protected string[] countryList = { "", "Nordland", "Nord Trøndelag", "Sør Trøndelag", "Møre og Romsdal", "Sogn og Fjordane", "Hordaland", "Rogaland", "Vest Agder" };

    public string[] GetCountryList()
    {
        return countryList;
    }

    public IDictionary<string, string> GetMunicipalityList(string country, IList<ContactPersonList> contactPersonList)
    {
        var result = new Dictionary<string, string>();

        foreach (ContactPerson contactPerson in contactPersonList)
        {
            if (contactPerson.Country.Equals(country))
            {
                if (contactPerson.Municipality == "mrHeroy")
                {
                    result.Add("Herøy", contactPerson.Municipality);
                }
                else
                {
                    result.Add(contactPerson.Municipality, contactPerson.Municipality);
                }

            }
        }

        return result;
    }
}