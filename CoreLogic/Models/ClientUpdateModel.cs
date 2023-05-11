namespace UPB.CoreLogic.Models;

public class ClientUpdateModel
{
    public string Name { get; set;}
    public string LastName { get; set;}
    public string SecondLastName { get; set;}
    public string Address { get; set;}
    public string Telephone { get; set;}

    public ClientUpdateModel(string Name, string LastName, string SecondLastName, string Address, string Telephone)
    {
        this.Name = Name;
        this.LastName = LastName;
        this.SecondLastName = SecondLastName;

        this.Address = Address;
        this.Telephone = Telephone;
    }

}