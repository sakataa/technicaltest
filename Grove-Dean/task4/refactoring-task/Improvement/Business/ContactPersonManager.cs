using System;

public class ContactPersonManager
{
    public IEnumerable<string> GetEmailList(IList<ContactPerson> contactPersonList, string municipality)
    {
        return contactPersonList.Where(person => person.Municipality.Equals(municipality, StringComparison.InvariantCultureIgnoreCase))
            .Select(p => p.Email);
    }

    public IList<ContactPerson> InitDataList()
    {
        var contactPersonList = new List<ContactPerson>();
        contactPersonList.Add(new ContactPerson("S�rfold", "Nordland", "Kjell.Stokbakken@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Gildesk�l", "Nordland", "Kjell.Stokbakken@Legacy.com"));
        contactPersonList.Add(new ContactPerson("R�d�y", "Nordland", "Kjell.Stokbakken@Legacy.com"));
        contactPersonList.Add(new ContactPerson("D�nna", "Nordland", "Kjell.Stokbakken@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Her�y", "Nordland", "Kjell.Stokbakken@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Alstahaug", "Nordland", "Kjell.Stokbakken@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Br�nn�y", "Nordland", "Kjell.Stokbakken@Legacy.com"));
        contactPersonList.Add(new ContactPerson("S�mna", "Nordland", "Kjell.Stokbakken@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Leka", "Nord Tr�ndelag", "Kjell.Stokbakken@Legacy.com"));
        contactPersonList.Add(new ContactPerson("N�r�y", "Nord Tr�ndelag", "Kjell.Stokbakken@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Mel�y", "Nordland", "Kjell.Stokbakken@Legacy.com"));
        contactPersonList.Add(new ContactPerson("H�ylandet", "Nord Tr�ndelag", "Kjell.Stokbakken@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Bod�", "Nordland", "Kjell.Stokbakken@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Fosnes", "Nord Tr�ndelag", "knut.utheim@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Flatanger", "Nord Tr�ndelag", "knut.utheim@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Osen", "S�r Tr�ndelag", "knut.utheim@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Fr�ya", "S�r Tr�ndelag", "knut.utheim@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Hitra", "S�r Tr�ndelag", "knut.utheim@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Sm�la", "M�re og Romsdal", "knut.utheim@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Aver�y", "M�re og Romsdal", "knut.utheim@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Roan", "S�r Tr�ndelag", "knut.utheim@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Snillfjord", "S�r Tr�ndelag", "knut.utheim@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Aure", "M�re og Romsdal", "knut.utheim@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Bjugn", "S�r Tr�ndelag", "knut.utheim@Legacy.com"));
        contactPersonList.Add(new ContactPerson("mrHeroy", "M�re og Romsdal", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Volda", "M�re og Romsdal", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Vanylven", "M�re og Romsdal", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Selje", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("V�gs�y", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Bremanger", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("�rsta", "M�re og Romsdal", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Ulstein", "M�re og Romsdal", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Flora", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Leikanger", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("H�yanger", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Fjaler", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Solund", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Hyllestad", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Gulen", "Sogn og Fjordane", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("�lesund", "M�re og Romsdal", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Aukra", "M�re og Romsdal", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Fr�na", "M�re og Romsdal", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Haram", "M�re og Romsdal", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Giske", "M�re og Romsdal", "Per-Roar.Gjerde@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Ask�y", "Hordaland", "astrid.sande@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Fjell", "Hordaland", "astrid.sande@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Sund", "Hordaland", "astrid.sande@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Etne", "Hordaland", "astrid.sande@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Jondal", "Hordaland", "astrid.sande@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Kvinnherad", "Hordaland", "astrid.sande@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Tysv�r", "Rogaland", "astrid.sande@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Vindafjord", "Rogaland", "astrid.sande@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Finn�y", "Rogaland", "astrid.sande@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Hjelmeland", "Rogaland", "astrid.sande@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Flekkefjord", "Vest Agder", "astrid.sande@Legacy.com"));
        contactPersonList.Add(new ContactPerson("Masfjorden", "Hordaland", "astrid.sande@Legacy.com"));
        contactPersonList.Add(new ContactPerson("�ygarden", "Hordaland", "astrid.sande@Legacy.com"));
    }
}