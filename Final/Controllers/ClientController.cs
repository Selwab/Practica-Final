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
}
