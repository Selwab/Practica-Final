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
}