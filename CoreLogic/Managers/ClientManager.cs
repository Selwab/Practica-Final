using UPB.CoreLogic.Models;
using Microsoft.Extensions.Configuration;
using System.Text.Json;

namespace UPB.CoreLogic.Managers;

public class ClientManager
{
    private readonly string _path;
    private readonly string _backingService;

    public ClientManager(IConfiguration configuration)
    {
        List<Client> clients = new List<Client>();
        _path = configuration.GetSection("PathClients").Value;
        _backingService = configuration.GetSection("BackingService").Value;

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
        List<Client> clients = JsonSerializer.Deserialize<List<Client>>(jsonFile);
        return clients;
    }

    public Client GetById(int ci)
    {
        if(ci <= 0)
        {
            throw new Exception("Invalid CI");
        }

        string jsonFile = File.ReadAllText(_path);
        List<Client> clients = JsonSerializer.Deserialize<List<Client>>(jsonFile);

        Client clientFound = clients.Find(client => client.CI == ci);

        if(clientFound == null)
        {
            throw new Exception("Client not found.");
        } 

        return clientFound;  
    }

    public Client Update(int ci, string name, string lastName, string secondLastName, string address, string telephone)
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
        List<Client> clients = JsonSerializer.Deserialize<List<Client>>(jsonFile);
        Client clientFound = clients.Find(client => client.CI == ci); 

        if(clientFound == null)
        {
            throw new Exception("Client not found.");
        }

        clientFound.Name = name;
        clientFound.LastName = lastName;
        clientFound.SecondLastName = secondLastName;
        clientFound.Address = address;
        clientFound.Telephone = telephone;
        clientFound.ClientID = GetClientID(name, lastName, secondLastName, ci);

        string updatedJsonFile = JsonSerializer.Serialize(clients);
        File.WriteAllText(_path, updatedJsonFile);

        return clientFound;
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

    public Client Create(string name, string lastName, string secondLastName, int ci, string address, string telephone)
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

        string jsonFile = File.ReadAllText(_path);
        List<Client> clients = JsonSerializer.Deserialize<List<Client>>(jsonFile);

        foreach (Client client in clients)
        {
            if (client.CI == ci)
            {
                throw new Exception("The CI: " + ci + " already exists.");
            }
        }

        Client createdClient = new Client(name, lastName, secondLastName, ci, address, telephone, ranking, clientID);
        clients.Add(createdClient);

        string updatedJsonFile = JsonSerializer.Serialize(clients);
        File.WriteAllText(_path, updatedJsonFile);

        return createdClient;
    }

    public int GetRanking()
    {
        int[] rankingOptions = new int[] {1,2,3,4,5};
        Random random = new Random();
        int index = random.Next(0,rankingOptions.Length);
        return rankingOptions[index];
    }

    public Client Delete(int ci)
    {
        if(ci <= 0)
        {
            throw new Exception("Invalid CI");
        }

        string jsonFile = File.ReadAllText(_path);
        List<Client> clients = JsonSerializer.Deserialize<List<Client>>(jsonFile);
        int clientToDeleteIndex = clients.FindIndex(client => client.CI == ci); 

        if(clientToDeleteIndex == -1)
        {
            throw new Exception("Client not found.");
        } 

        Client clientToDelete = clients[clientToDeleteIndex];
        clients.RemoveAt(clientToDeleteIndex);

        string updatedJsonFile = JsonSerializer.Serialize(clients);
        File.WriteAllText(_path, updatedJsonFile);

        return clientToDelete;
    }

    public async Task<List<Client>> GetExternalClients(HttpClient _httpClient)
    {
        var response = await _httpClient.GetAsync(_backingService);

        response.EnsureSuccessStatusCode();

        string json = await response.Content.ReadAsStringAsync();

        List<ExternalClient> externalClients = JsonSerializer.Deserialize<List<ExternalClient>>(json);
        List<Client> clients = new List<Client>();

        foreach (ExternalClient ec in externalClients)
        {
            clients.Add(new Client(ec.first_name,ec.last_name,"",ec.id,$"{ec.address.street_name}, {ec.address.city}, {ec.address.state}",ec.phone_number,GetRanking(),GetClientID(ec.first_name,ec.last_name,"",ec.id)));
        }

        return clients;
    }
}