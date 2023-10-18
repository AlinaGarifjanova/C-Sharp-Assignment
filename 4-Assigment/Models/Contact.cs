using _4_Assigment.Interfaces;

namespace _4_Assigment.Models
{
    public class Contact : IContact
    {

        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PhoneNumber { get; set; } = null!;
        public Address? Address { get; set; } = null!;

     
        public Contact()
        {
            Address = new Address();
        }


    }
}
