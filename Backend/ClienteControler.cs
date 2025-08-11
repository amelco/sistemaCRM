using Core.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Backend
{
    [Controller]
    public class ClienteController : Controller
    {
        [HttpGet]
        [Route("/cliente")]
        public IActionResult Get()
        {
            // 1. buscar a lista de clientes no storage -> List<Cliente>
            List<Cliente> clientes = new();
            clientes.Add(new Cliente("Fulano", 18));
            clientes.Add( new Cliente("Beltrano", 22));

            // 2. Mapeamento da List<Cliente> para List<ClienteModel>
            var clientesMap = Cliente.MapList(clientes);

            // 3. Retorna a lista de cliente model
            return Ok(clientesMap);
        }
    }
}