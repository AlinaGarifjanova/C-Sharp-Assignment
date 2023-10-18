using _4_Assigment.Interfaces;
using _4_Assigment.Models;
using _4_Assigment.Services;
using Newtonsoft.Json;
using System.ComponentModel.Design;
using System.Diagnostics;

namespace _4_Assigment.Services;

public class ContactService : IContactService
{
    private List<Contact> _contact = new List<Contact>();
    private readonly string filePath = @"c:\Skolan\Code\customer.json";

    public void AddNewContact(Contact contacts)
    {
        //Lägger till en kontakt i listan _contact och sparar till en Json fil med hjälp utav FIleService
        _contact.Add(contacts);
        var json = JsonConvert.SerializeObject(_contact);
        FileService.SaveToFile(json, filePath);
    }


    IEnumerable<Contact> IContactService.GetAllContacts()
    {
        try
        { // Här hämtar vi alla kontakter från Json filen som har sparats och om det inte finns några kontakter så får vi tillbaka null
            var content = FileService.ReadFromFile(filePath);
            if (!string.IsNullOrEmpty(content))
            {
                var contact = JsonConvert.DeserializeObject<List<Contact>>(content)!;
                return contact.Cast<Contact>();
            }
        }catch (Exception ex) { Debug.WriteLine(ex.Message); }
       
        return null!;
    }

    public void RemoveContact(string email)
    {
        // Här tar vi bort en kontakt från listan _contact baserat på email och sedan uppdaterar vi json-filen efter att vi har tagit bort en användare
        var content = FileService.ReadFromFile(filePath);
        if (!string.IsNullOrEmpty(content))
        {
            _contact = JsonConvert.DeserializeObject<List<Contact>>(content)!;

            Contact contactRemove = _contact.FirstOrDefault(x => x.Email == email)!;
            email = email.Trim();

            if (contactRemove != null)
            {
                _contact.Remove(contactRemove);

                var json = JsonConvert.SerializeObject(_contact);
                FileService.SaveToFile(json, filePath);

                Console.WriteLine($"\nKontakten med e-postadressen {email} har tagits bort");
                Console.WriteLine("*********************************************************");
            }
            else
            {
                Console.WriteLine($"\nKontakten med e-postadressen {email} kunde inte hittas.");
                Console.WriteLine("*************************************************************");
            }
        }

    }

    public Contact UpdateContact(string email, Contact updatedContact)
    {
        // Här hämtar vi en användare från Json-Filen baserat på e-postadressen vi har angivit och sedan uppdaterar vi kontakten och sparar de uppdaterade värden
        try
        {
            var content = FileService.ReadFromFile(filePath);
            if (!string.IsNullOrEmpty(content))
            {
                _contact = JsonConvert.DeserializeObject<List<Contact>>(content)!;
                Contact foundContact = _contact.FirstOrDefault(contact => contact.Email == email)!;
                if (foundContact != null)
                {
                    _contact.Remove(foundContact);
                   
                    foundContact.FirstName = updatedContact.FirstName;
                    foundContact.LastName = updatedContact.LastName;
                    foundContact.Email = updatedContact.Email;
                    foundContact.PhoneNumber = updatedContact.PhoneNumber;
                    foundContact.Address!.StreetName = updatedContact.Address!.StreetName;
                    foundContact.Address!.StreetNumber = updatedContact.Address!.StreetNumber;
                    foundContact.Address!.PostalCode = updatedContact.Address!.PostalCode;
                    foundContact.Address!.City = updatedContact.Address!.City;
                    foundContact.Address!.Country = updatedContact.Address!.Country;

                    _contact.Add(foundContact);
                    FileService.SaveToFile(filePath, JsonConvert.SerializeObject(_contact));

                    return foundContact!;
                }
                

            }
        } catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;


    }

    public Contact GetContactByEmail(string email)
    {
        // Här hämtar vi en kontakt från Json-filen baserat på e-postadressen, om en sådan användare inte hittats så returnerar vi null
        try
        {
            var content = FileService.ReadFromFile(filePath);

            if (!string.IsNullOrEmpty(content))
            {
                var contacts = JsonConvert.DeserializeObject<List<Contact>>(content);

                Contact foundContact = contacts!.FirstOrDefault(contact => contact.Email == email)!;

                return foundContact;
            }
        }catch (Exception ex) { Debug.WriteLine(ex.Message); }

        return null!;

    }

}
