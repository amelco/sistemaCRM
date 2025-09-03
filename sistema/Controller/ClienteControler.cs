using Microsoft.AspNetCore.Mvc;
using sistema.Entities;
using sistema.Repositories;

namespace sistema.Controller;

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
    public ActionResult<List<Cliente>> Get()
    {
        var resultado = _repo.ObterTodos();

        if (!resultado.FoiSucesso)
            return BadRequest(resultado.Erro.mensagem);

        return Ok(resultado.Sucesso);
    }

    [HttpPost]
    public ActionResult<Cliente> Post([FromBody] Cliente cliente)
    {
        var novo = _repo.Inserir(cliente);
        return CreatedAtAction(nameof(Get), new { id = novo.Id }, novo);
    }
}
