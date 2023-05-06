namespace UPB.CoreLogic.Models;

public class Client
{
    public string Name;
    public string LastName;
    public string SecondLastName;
    public int CI;
    public string Address;
    public int Telephone;
    public int Ranking; //1-5
    public string ClientID;
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
