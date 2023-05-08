using UPB.CoreLogic.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace UPB.CoreLogic.Managers;

public class ClientManager
{
    private List<Client> _clients;

    private readonly string _path;

    public ClientManager(IConfiguration configuration)
    {
        _clients = new List<Client>();
        _path = configuration.GetSection("PathClients").Value;

        string directory = Path.GetDirectoryName(_path);
        if(!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        if(!File.Exists(_path))
        {
            JsonElement json = JsonDocument.Parse("[]").RootElement;
            File.WriteAllText(_path, json.ToString());
        }
    }   

    public List<Client> GetAll()
    {
        string jsonFile = File.ReadAllText(_path);
        _clients = JsonSerializer.Deserialize<List<Client>>(jsonFile);
        return _clients;
    }

    public Client GetById(int ci)
    {
        if(ci <= 0)
        {
            throw new Exception("Invalid CI");
        }

        string jsonFile = File.ReadAllText(_path);
        _clients = JsonSerializer.Deserialize<List<Client>>(jsonFile);

        Client clientFound = _clients.Find(client => client.CI == ci);

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

        string jsonFile = File.ReadAllText(_path);
        _clients = JsonSerializer.Deserialize<List<Client>>(jsonFile);
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

        string updatedJsonFile = JsonSerializer.Serialize(_clients);
        File.WriteAllText(_path, updatedJsonFile);

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
        string code = (n.ToString() + ln.ToString() + sln.ToString() + "-" + ci);
        return code;
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
        
        string jsonFile = File.ReadAllText(_path);
        _clients = JsonSerializer.Deserialize<List<Client>>(jsonFile);
        _clients.Add(createdClient);

        string updatedJsonFile = JsonSerializer.Serialize(_clients);
        File.WriteAllText(_path, updatedJsonFile);

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

        string jsonFile = File.ReadAllText(_path);
        _clients = JsonSerializer.Deserialize<List<Client>>(jsonFile);
        int clientToDeleteIndex = _clients.FindIndex(client => client.CI == ci); 

        if(clientToDeleteIndex == -1)
        {
            throw new Exception("Client not found.");
        } 

        Client clientToDelete = _clients[clientToDeleteIndex];
        _clients.RemoveAt(clientToDeleteIndex);

        string updatedJsonFile = JsonSerializer.Serialize(_clients);
        File.WriteAllText(_path, updatedJsonFile);

        return clientToDelete;
    }
}