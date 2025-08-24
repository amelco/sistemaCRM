using System.Threading.Tasks;
using Core.Entities;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend
{
    [Controller]
    public class ClienteController : Controller
    {
        ClienteRepository repo = new ClienteRepository(); // TODO (Andre): fazer injecao de dependencia

        [HttpGet]
        [Route("/cliente/{id}")]
        public IActionResult Get(int id)
        {
            var cliente = repo.Obter(1);
            if (cliente is null) return Ok();

            var clienteMap = Cliente.Map(cliente);
            return Ok(clienteMap);
        }

        [HttpPost]
        [Route("/cliente")]
        public IActionResult Post(ClienteModel model)
        {
            var cliente = Cliente.Map(model);
            if (cliente is null || String.IsNullOrEmpty(cliente.Nome) || cliente.Idade == 0) return Ok();

            var sucesso = repo.Inserir(cliente);
            if (sucesso) return Created();

            return Ok();
        }

        [HttpGet]
        [Route("/cliente")]
        public IActionResult List()
        {
            var lista = repo.Listar();
            return Ok(lista);
        }
    }
}