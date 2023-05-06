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
        if(ci < 0)
        {
            throw new Exception("CI invalid.");
        }

        Client clientFound = _clients.Find(client => client.CI == ci);

        if(clientFound == null)
        {
            throw new Exception("Client not found.");
        } 

        return clientFound;  
    }

    public Client Update(int ci, string name, string lastName, string secondLastName, string address, int telephone)
    {
        if(ci < 0)
        {
            throw new Exception("CI invalid.");
        }
        if(name == "" || lastName == "")
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
}