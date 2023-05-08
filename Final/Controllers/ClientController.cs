using Microsoft.AspNetCore.Mvc;
using UPB.CoreLogic.Managers;
using UPB.CoreLogic.Models;

namespace UPB.Final.Controllers;

[ApiController]
[Route("clients")]
public class ClientController : ControllerBase
{
    private readonly ClientManager _clientManager;
    public ClientController(ClientManager clientManager)
    {
        _clientManager = clientManager;
    }

    [HttpGet]
    public List<Client> Get()
    {
        return _clientManager.GetAll();  
    }

    [HttpPost]
    public Client Post([FromBody] Client clientToCreate)
    {
        return _clientManager.Create(clientToCreate.Name, clientToCreate.LastName, clientToCreate.SecondLastName, clientToCreate.CI, clientToCreate.Address, clientToCreate.Telephone);
    }

    [HttpGet]
    [Route("{ci}")]
    public Client GetById([FromRoute] int ci)
    {
        return _clientManager.GetById(ci);  
    }

    [HttpPut]
    [Route("{ci}")]
    public Client Put([FromRoute] int ci, [FromBody]Client clientToUpdate)
    {
        return _clientManager.Update(ci,clientToUpdate.Name,clientToUpdate.LastName,clientToUpdate.SecondLastName,clientToUpdate.Address,clientToUpdate.Telephone);
    }

    [HttpDelete]
    [Route("{ci}")]
    public Client Delete([FromRoute] int ci)
    {
        return _clientManager.Delete(ci);
    }
}
