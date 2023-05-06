namespace UPB.CoreLogic.Models;

public class Client
{
    public string Name { get; set;}
    public string LastName { get; set;}
    public string SecondLastName { get; set;}
    public int CI { get; set;}
    public string Address { get; set;}
    public int Telephone { get; set;}
    public int Ranking { get; set;} //1-5
    public string ClientID { get; set;}
    public Client(string Name, string LastName, string SecondLastName, int CI, string Address, int Telephone, int Ranking, string ClientID)
    {
        this.Name = Name;
        this.LastName = LastName;
        this.SecondLastName = SecondLastName;
        this.CI = CI;
        this.Address = Address;
        this.Telephone = Telephone;
        this.Ranking = Ranking;
        this.ClientID = ClientID;
    }

}
