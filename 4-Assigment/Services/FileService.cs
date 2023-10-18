using _4_Assigment.Interfaces;
using _4_Assigment.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace _4_Assigment.Services;

public class FileService // Det här är en klass som hjälper oss att spara in och läsa JSON data till och från filen. 
{
   private static readonly string filePath = @"c:\Skolan\Code\customer.json"; // Här har vi vart vi sparar in informationen 
   // Här sparar vi in i JSON-Data som en sträng och till en fil, vi använder oss av StreamWriter för att skriva strängar till filen
    public static void SaveToFile(string contentAsJson, string filePath)
    {
        using (var sw = new StreamWriter(filePath))
        {
            sw.WriteLine(contentAsJson);
        }
    }

    public static string ReadFromFile(string filePath)
    {
        //Denna metopden används för att man ska kunna läsa informationen från filen 
        if (File.Exists(filePath))
        {
            using (var sr = new StreamReader(filePath))
            {
                return sr.ReadToEnd();
            }
               
        }
        return null!;
    }
}

