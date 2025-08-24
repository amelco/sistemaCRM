using Microsoft.AspNetCore.Mvc;
using sistema.Entities;
using sistema.Repositories;

namespace sistema.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ClientesController : ControllerBase
{
    private readonly ClienteRepository _repo;

    public ClientesController(ClienteRepository repo)
    {
        _repo = repo;
    }

    [HttpGet]
    public ActionResult<IEnumerable<Cliente>> Get()
        => Ok(_repo.ObterTodos());

    [HttpPost]
    public ActionResult<Cliente> Post([FromBody] Cliente cliente)
    {
        var novo = _repo.Adicionar(cliente);
        return CreatedAtAction(nameof(Get), new { id = novo.Id }, novo);
    }
}
