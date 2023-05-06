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
}