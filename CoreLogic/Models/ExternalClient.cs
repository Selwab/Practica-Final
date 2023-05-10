namespace UPB.CoreLogic.Models;

public class ExternalClient
{
    public string first_name { get; set; }
    public string last_name { get; set; }
    public int id { get; set; }
    public Address address { get; set; }
    public string phone_number { get; set; }
}

public class Address
{
    public string street_name { get; set; }
    public string city { get; set; }
    public string state { get; set; }
}