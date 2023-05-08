using UPB.CoreLogic.Models;


namespace UPB.CoreLogic.Managers;

public class ClientManager
{
    private List<Client> _clients;

    public ClientManager()
    {
        _clients = new List<Client>();
    }   

    public List<Client> GetAll()
    {
        return _clients;
    }

    public Client GetById(int ci)
    {
        if(ci <= 0)
        {
            throw new Exception("Invalid CI");
        }

        Client clientFound;
        clientFound = _clients.Find(client => client.CI == ci);

        if(clientFound == null)
        {
            throw new Exception("Client not found.");
        } 

        return clientFound;  
    }

    public Client Update(int ci, string name, string lastName, string secondLastName, string address, int telephone)
    {
        if(ci <= 0)
        {
            throw new Exception("Invalid CI");
        }
        else if(name == "" || lastName == "")
        {
            throw new Exception("Name and LastName are mandatory.");
        }

        Client clientFound = _clients.Find(client => client.CI == ci); 

        if(clientFound == null)
        {
            throw new Exception("Client not found.");
        }

        clientFound.Name = name;
        clientFound.LastName = lastName;
        clientFound.SecondLastName = secondLastName;
        clientFound.Address = address;
        clientFound.Telephone = telephone;
        clientFound.ClientID = GenerateClientID(ci, name, lastName, secondLastName);

        return clientFound;
    }

    private string GenerateClientID(int ci, string name, string lastName, string secondLastName)
    {
        char n = name[0];
        char ln = lastName[0];
        char sln;
        
        if(secondLastName == "")
        {
            sln = '_';
        }
        else
        {
            sln = secondLastName[0];
        }

        return (n + ln + sln + "-" + ci);
    }

    public string GetClientID(string name, string lastName, string secondLastName, int ci)
    {
        string clientCI = Convert.ToString(ci);
        string code = "";
        string secondLN = "";
        if(secondLastName == "")
        {
            secondLN = "_";
        }
        else
        {
            secondLN = secondLastName.Substring(0,1);
        }
        code += name.Substring(0,1) + lastName.Substring(0,1) + secondLN + "-" + clientCI;
        return code;
    }

    public Client Create(string name, string lastName, string secondLastName, int ci, string address, int telephone)
    {
        if(ci <= 0)
        {
            throw new Exception("Invalid CI");
        }
        else if(name == "" || lastName == "")
        {
            throw new Exception("Name and LastName are mandatory.");
        }
        
        int ranking = GetRanking();
        //string clientID = GenerateClientID(ci, name, lastName, secondLastName);
        string clientID = GetClientID(name, lastName, secondLastName, ci);
        Client createdClient = new Client(name, lastName, secondLastName, ci, address, telephone, ranking, clientID);
        _clients.Add(createdClient);
        return createdClient;
    }

    public int GetRanking()
    {
        int[] rankingOptions = new int[] {1,2,3,4,5};
        Random random = new Random();
        int index = random.Next(0,rankingOptions.Length);
        Console.WriteLine("Random: " + rankingOptions[index]);
        return rankingOptions[index];
    }

    public Client Delete(int ci)
    {
        if(ci <= 0)
        {
            throw new Exception("Invalid CI");
        }
        int clientToDelteIdex = _clients.FindIndex(client => client.CI == ci);

        if(clientToDelteIdex == -1)
        {
            throw new Exception("Client not found.");
        } 

        Client clientToDelete = _clients[clientToDelteIdex];
        _clients.RemoveAt(clientToDelteIdex);
        return clientToDelete;
    }
}