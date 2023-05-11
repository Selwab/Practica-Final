using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;
using UPB.CoreLogic.Managers;
using UPB.CoreLogic.Models;

namespace UPB.Final.Controllers;

[ApiController]
[Route("[controller]")]
public class ClientController : ControllerBase
{
    private readonly HttpClient _httpClient;
    private readonly ClientManager _clientManager;
    public ClientController(ClientManager clientManager, HttpClient httpClient)
    {
        _clientManager = clientManager;
        _httpClient = httpClient;
    }

    [HttpGet]
    public List<Client> Get()
    {
        return _clientManager.GetAll();  
    }

    [HttpPost]
    [SwaggerRequestExample(null, typeof(ClientCreateModel))]
    public Client Post([FromBody] ClientCreateModel clientToCreate)
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
    [SwaggerRequestExample(null, typeof(ClientUpdateModel))]
    public Client Put([FromRoute] int ci, [FromBody]ClientUpdateModel clientToUpdate)
    {
        return _clientManager.Update(ci,clientToUpdate.Name,clientToUpdate.LastName,clientToUpdate.SecondLastName,clientToUpdate.Address,clientToUpdate.Telephone);
    }

    [HttpDelete]
    [Route("{ci}")]
    public Client Delete([FromRoute] int ci)
    {
        return _clientManager.Delete(ci);
    }

    [HttpGet]
    [Route("external-clients")]
    public Task<List<Client>> GetExternalClients()
    {
        return _clientManager.GetExternalClients(_httpClient);
    }
}
