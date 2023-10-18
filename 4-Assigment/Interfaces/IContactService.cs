using _4_Assigment.Models;

namespace _4_Assigment.Interfaces
{
    public interface IContactService // Ett gränssnitt för hur vi hanterar en kontakt 
    {
        //Lägger till en kontakt 
        void AddNewContact(Contact contact);
        //Tar bort en kontakt från kontaktlista med hjälp av epostadress
        void RemoveContact(string email);
        //Hämtar alla kontakter i kontaktlista
        public IEnumerable<Contact> GetAllContacts();

        // Uppdaterar en befintlig kontakt 
        Contact UpdateContact(string email, Contact updateContact);
        // Hämtar en kontant med hjälp utav epostadress
        Contact GetContactByEmail (string email);

    }
}