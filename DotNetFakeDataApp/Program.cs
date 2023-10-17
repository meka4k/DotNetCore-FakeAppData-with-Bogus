// See https://aka.ms/new-console-template for more information
using Bogus;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Unicode;

var addressFaker = new Faker<Address>("tr")
    .RuleFor(i => i.City, i => i.Address.City())
    .RuleFor(i => i.StreetName, i => i.Address.StreetName())
    .RuleFor(i => i.ZipCode, i => i.Address.ZipCode());

var userFaker = new Faker<User>("tr")
    .RuleFor(i => i.Id, i => i.Random.Guid())
    .RuleFor(i => i.Age, i => i.Random.Int(18, 70))
    .RuleFor(i => i.FirstName, i => i.Person.FirstName)
    .RuleFor(i => i.LastName, i => i.Person.LastName)
    .RuleFor(i => i.UserName, i => i.Person.UserName)
    .RuleFor(i => i.Email, i => i.Person.Email)
    .RuleFor(i => i.Gender, i => i.PickRandom<Gender>())
    .RuleFor(i => i.Address, addressFaker);

var generatedObject=userFaker.Generate(2);

var opt = new JsonSerializerOptions()
{
    WriteIndented = true,
    Encoder = JavaScriptEncoder.Create(UnicodeRanges.All)
};

string serializeJson=JsonSerializer.Serialize(generatedObject,opt);
Console.WriteLine(serializeJson);

public enum Gender
{
    Male=1,
    Female=2
}
public class User
{
    public Guid Id { get; set; }
    public int Age { get; set; }
    public String FirstName { get; set; }
    public String LastName { get; set; }
    public String UserName { get; set; }
    public String Email { get; set; }
    public Gender Gender { get; set; }
    public Address Address { get; set; }
}


public class Address
{
    public String City { get; set; }
    public String ZipCode { get; set; }
    public String StreetName { get; set; }
}