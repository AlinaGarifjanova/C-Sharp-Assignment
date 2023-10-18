using _4_Assigment.Models;
using _4_Assigment.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_04.Tests.UnitTests;

public class ContactServiceTest
{
    [Fact]
    public void TestGetContactByEmail_ReturnTrue()
    {
        //Arrange 
        var contactService = new ContactService();

        Contact testContact = new Contact
        {
            FirstName = "Test",
            LastName = "Test",
            Email = "Test@domain.se",
            PhoneNumber = "Test"
        };

        contactService.AddNewContact(testContact);

        //Act
        Contact foundContact = contactService.GetContactByEmail("Test@domain.se");

        //Assert
        Assert.NotNull(foundContact);
    }
   
    [Fact]
    public void TestGetContactByEmail_ReturnFalse()
    {
        // Arrange
        var contactService = new ContactService();

        //Act
        Contact foundContact = contactService.GetContactByEmail("TestFailed@domain.com");

        //Assert
        Assert.Null(foundContact);
    }

}
