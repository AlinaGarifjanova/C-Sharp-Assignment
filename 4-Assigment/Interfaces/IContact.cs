using _4_Assigment.Models;

namespace _4_Assigment.Interfaces;

public interface IContact // Gränssnittet för en kontaktperson, där jag även har lagt till adressen som en del
{
    string Email { get; set; }
    string FirstName { get; set; }
    string LastName { get; set; }
    string PhoneNumber { get; set; }
    Address Address { get; set; }
   
   
}
