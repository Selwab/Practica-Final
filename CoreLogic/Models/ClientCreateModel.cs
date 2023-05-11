namespace UPB.CoreLogic.Models;

public class ClientCreateModel
{
    public string Name { get; set;}
    public string LastName { get; set;}
    public string SecondLastName { get; set;}
    public int CI { get; set;}
    public string Address { get; set;}
    public string Telephone { get; set;}

    public ClientCreateModel(string Name, string LastName, string SecondLastName, int CI, string Address, string Telephone)
    {
        this.Name = Name;
        this.LastName = LastName;
        this.SecondLastName = SecondLastName;
        this.CI = CI;
        this.Address = Address;
        this.Telephone = Telephone;
    }

}