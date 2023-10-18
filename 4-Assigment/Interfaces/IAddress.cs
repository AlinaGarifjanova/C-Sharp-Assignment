namespace _4_Assigment.Interfaces
{
    public interface IAddress // Det här är ett gränssnitt för Adressen 
    {
        string City { get; set; }
        string PostalCode { get; set; }
        string StreetName { get; set; }
        string StreetNumber { get; set; }
        string Country { get; set; } 

        
    }
}