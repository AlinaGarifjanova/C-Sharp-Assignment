using _4_Assigment.Interfaces;
using _4_Assigment.Models;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Contracts;
using _4_Assigment.Services;

namespace _4_Assigment.Services;

public class MenuService
{
    // En instans för IContactService för hanteringen av kontakter
    private static readonly IContactService contactService = new ContactService();

    //Huvudmenyn för applikationen 
    public static void MainMenu()
    {
        string email = "email";
        do
        {
            Console.Clear();
            Console.WriteLine("\nVälkommen till Adressboken, välj något av nedanstående valen!");
            Console.WriteLine("*******************************************************************\n");
            Console.WriteLine("1. Skapa en ny användare. \n");
            Console.WriteLine("2. Visa alla användare. \n");
            Console.WriteLine("3. Visa en specifik användare. \n");
            Console.WriteLine("4. Uppdatera en befintlig kontakt \n");
            Console.WriteLine("5. Ta bort en användare. \n");
            Console.WriteLine("0. Avsluta. \n");
            var option = Console.ReadLine();

            Console.Clear();

            switch (option)
            {
                case "1":
                    MenuService.AddNewContactMenu();
                    break;

                case "2":
                    MenuService.GetAllContactsMenu();
                    break;

                case "3":
                    Console.WriteLine("\nSkriv in e-postadressen på den kontakt du söker efter: \n");
                    email = Console.ReadLine();
                    MenuService.GetOneContactMenu(email!);
                    break;
                case "4":
                    Console.WriteLine("\nSkriv in e-postadressen på kontakten du vill uppdatera:\n ");
                    string emailToUpdate = Console.ReadLine();
                    Contact updatedContact = new Contact();
                    MenuService.UpdateContact(emailToUpdate, updatedContact); // Skicka med e-postadressen
                    break;

                case "5":
                    MenuService.RemoveContactMenu();
                    break;

                case "0":
                    Environment.Exit(0);
                    break;
                default:
                    Console.WriteLine("Du gjorde ett ogiltigt val, gör ett nytt försök. \n\n");
                    break;
            }
            Console.ReadKey();

        } while (true);
    }

    //Lägg till en ny kontakt 
    public static void AddNewContactMenu()
    {
        //Skapa en ny kontakt och adress
        Contact contact = new Contact();
        contact.Address = new Address();

        //Läs in användarens svar
        Console.Write("\nFörnamn: ");
        contact.FirstName = Console.ReadLine().Trim();
        Console.Write("\nEfternamn: ");
        contact.LastName = Console.ReadLine().Trim();
        Console.Write("\nE-postadress: ");
        contact.Email = Console.ReadLine().Trim().ToLower();
        Console.Write("\nTelefonnummer: ");
        contact.PhoneNumber = Console.ReadLine();

        Console.Write("\nGatunamn: ");
        contact.Address.StreetName = Console.ReadLine().Trim();
        Console.Write("\nGatunummer: ");
        contact.Address.StreetNumber = Console.ReadLine().Trim();
        Console.Write("\nPostnummer: ");
        contact.Address.PostalCode = Console.ReadLine().Trim();
        Console.Write("\nStad/ort: ");
        contact.Address.City = Console.ReadLine().Trim();
        Console.WriteLine("\nLand: ");
        contact.Address.Country = Console.ReadLine().Trim();

        Console.WriteLine("*************************************************");

        Console.WriteLine($"\nNu är {contact.FirstName} {contact.LastName} tillagd!");
        // Lägg till kontakten i ContactService
        contactService.AddNewContact(contact);

    }
    // Ta fram en kontakt från listan
    public static void GetOneContactMenu(string email)
    {
        Contact contacts = contactService.GetContactByEmail(email);

        if (contacts != null)
        {
            Console.WriteLine($"Kontakt hittad:\n\nFörnamn: {contacts.FirstName} \nEfternamn: {contacts.LastName} \nE-postadress: {contacts.Email} \nTelefonnummer: {contacts.PhoneNumber}");
            Console.WriteLine($"Adress: \nGatunamn: {contacts.Address.StreetName} \nGatunummer: {contacts.Address.StreetNumber} \nPostnummer: {contacts.Address.PostalCode} \nStad/ort: {contacts.Address.City}");
            Console.WriteLine("*******************************************************************************");
        }
        
    }

    // Ta fram alla kontakter 
    public static void GetAllContactsMenu()
    {
        // Anropa ContactServicr för att hämta ut alla kontakter
        var contacts = contactService.GetAllContacts();
        //Loopar igenom och skriver ut info om varje kontakt 
        foreach (var contact in contacts)
        {
            Console.WriteLine($"Kontakt hittad:\n\nFörnamn: {contact.FirstName} \nEfternamn: {contact.LastName} \nE-postadress: {contact.Email} \nTelefonnummer: {contact.PhoneNumber}");
            Console.WriteLine($"\nAdress\n\nGatunamn: {contact.Address.StreetName} \nGatunummer: {contact.Address.StreetNumber} \nPostnummer: {contact.Address.PostalCode} \nStad/ort: {contact.Address.City}");
            Console.WriteLine("************************************************************************\n");
        }
    }

    //Ta bort en befintlig kontakt 
    public static void RemoveContactMenu()
    {
        // Läs in e-postadressen för kontakten du vill ta bort 
        Console.WriteLine("\nAnge e-postadressen på kontakten du vill radera: \n");
        string? email = Console.ReadLine();
        // om e-postadressen hittat ta bort, annars skriv ut att ogilitg e-postadress
        if (email != null)
        {
            contactService.RemoveContact(email);
        }
        else
        {
            Console.WriteLine("\nDu skrev in ett ogiltig e-postadress ");
        }

    }
    // Uppdatera kontakt 
    public static void UpdateContact(string email, Contact updatedContact)
    {
 
        //Anropa ContactService för att hämta befintlig kontakt 
        Contact contactToUpdate = contactService.GetContactByEmail(email);
        // Användare väljer vilken info dem vill byta ut 
        if (contactToUpdate != null)
        {
            Console.WriteLine("\nKontakt hittad:\n");
            Console.WriteLine($"\nFörnamn: {contactToUpdate.FirstName}");
            Console.WriteLine($"\nEfternamn: {contactToUpdate.LastName}");
            Console.WriteLine($"\nTelefonnummer: {contactToUpdate.PhoneNumber}");
            Console.WriteLine($"\nEmail: {contactToUpdate.Email}");
            Console.WriteLine($"\nGatuadress: {contactToUpdate.Address?.StreetName}");
            Console.WriteLine($"\nGatunummer: {contactToUpdate.Address?.StreetNumber}");
            Console.WriteLine($"\nPostnummer: {contactToUpdate.Address?.PostalCode}");
            Console.WriteLine($"\nStad/ort: {contactToUpdate.Address?.City}");
            Console.WriteLine($"\nLand: {contactToUpdate.Address?.Country}");
            Console.WriteLine("´***************************************************");
            // Utskrift av övriga kontaktuppgifter
            Console.Clear();
            Console.WriteLine("\nVilket fält vill du uppdatera?\n");
            Console.WriteLine("1. Förnamn");
            Console.WriteLine("2. Efternamn");
            Console.WriteLine("3. Telefonnummer");
            Console.WriteLine("4. E-postadress");
            Console.WriteLine("5. Gatuadress");
            Console.WriteLine("6. Gatunummer");
            Console.WriteLine("7. Postnummer");
            Console.WriteLine("8. Stad/ort");
            Console.WriteLine("9. Land");
            Console.WriteLine("***********************************************************");
            // Lägg till fler alternativ för andra uppdateringar

            string choice = Console.ReadLine()!.Trim();

            switch (choice)
            {
                case "1":
                    Console.Write("Nytt förnamn: ");
                    string newFirstName = Console.ReadLine().Trim();
                    contactToUpdate.FirstName = newFirstName;
                    break;
                case "2":
                    Console.Write("Nytt efternamn: ");
                    string newLastName = Console.ReadLine().Trim();
                    contactToUpdate.LastName = newLastName;
                    break;
                case "3":
                    Console.Write("Nytt telefonnummer: ");
                    string newPhoneNumber = Console.ReadLine()!.Trim();
                    contactToUpdate.PhoneNumber = newPhoneNumber;
                    break;
                case "4":
                    Console.Write("Ny e-postadress: ");
                    string newEmail = Console.ReadLine()!.Trim();
                    contactToUpdate.Email = newEmail;
                    break;
                case "5":
                    Console.Write("Ny gatuadress: ");
                    string newStreetName = Console.ReadLine()!.Trim();
                    contactToUpdate.Address!.StreetName= newStreetName;
                    break;
                case "6":
                    Console.Write("Nytt gatunummer: ");
                    string newStreetNumber = Console.ReadLine()!.Trim();
                    contactToUpdate.Address!.StreetNumber = newStreetNumber;
                    break;
                case "7":
                    Console.Write("Nytt postnummer: ");
                    string newPostalCode = Console.ReadLine()!.Trim();
                    contactToUpdate.Address!.PostalCode = newPostalCode;
                    break;
                case "8":
                    Console.Write("Ny stad/ort: ");
                    string newCity = Console.ReadLine()!.Trim();
                    contactToUpdate.Address!.City= newCity;
                    break;
                case "9":
                    Console.Write("Nytt land: ");
                    string newCountry = Console.ReadLine()!.Trim();
                    contactToUpdate.Address!.Country = newCountry;
                    break;

                default:
                    Console.WriteLine("Ogiltigt val.");
                    break;
            }

            // Uppdatera kontakten i ContactService
            contactService.UpdateContact(email, contactToUpdate);
            // Skriv ut all info om kunden, för bekräftelse 
            Console.WriteLine("Kontakten har uppdaterats:\n\n");
            Console.WriteLine("**********************************************");
            Console.WriteLine($"Förnamn: {contactToUpdate.FirstName}");
            Console.WriteLine($"Efternamn: {contactToUpdate.LastName}");
            Console.WriteLine($"E-postadress: {contactToUpdate.Email}");
            Console.WriteLine($"Telefonnummer: {contactToUpdate.PhoneNumber}");
            Console.WriteLine($"Garunamn: {contactToUpdate.Address?.StreetName}");
            Console.WriteLine($"Gatunummer: {contactToUpdate.Address?.StreetNumber}");
            Console.WriteLine($"Postnummer: {contactToUpdate.Address?.PostalCode}");
            Console.WriteLine($"Stad/ort: {contactToUpdate.Address?.City}");
            Console.WriteLine($"Land: {contactToUpdate.Address?.Country}");
            Console.WriteLine("***********************************************");



            Console.WriteLine("Är du nöjd med uppdateringen? (ja/nej): ");
            string confirmationAfterUpdate = Console.ReadLine()!.Trim().ToLower();

            if (confirmationAfterUpdate == "ja")
            {
                contactService.RemoveContact(email);
                contactService.AddNewContact(contactToUpdate);
                Console.WriteLine("Kontakten har uppdaterats och sparats!");

            }
            else
            {
                Console.WriteLine("Ingen ändring gjordes, kontakten sparades inte.");
            }
        }
        else
        {
            Console.WriteLine("Ingen kontakt hittades med den angivna e-postadressen.");
        }
    }
}

