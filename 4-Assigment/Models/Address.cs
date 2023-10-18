
using _4_Assigment.Interfaces;

namespace _4_Assigment.Models;

public class Address : IAddress
{
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? StreetName { get; set; }
    public string? StreetNumber { get; set; } 
    public string? Country { get; set; } 


}
