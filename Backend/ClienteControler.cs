using Core.Entities;
using Core.Models;
using Microsoft.AspNetCore.Mvc;

namespace Backend
{
    [Controller]
    public class ClienteController : Controller
    {
        ClienteRepository repo = new ClienteRepository();

        [HttpGet]
        [Route("/cliente/{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var cliente = repo.Obter(id);
                if (cliente.TemErro) return BadRequest(cliente.Erro);   // (Andre) TODO: encontrar uma resposta mais adequada que BadRequest

                var clienteMap = Cliente.Map(cliente.Sucesso!);
                return Ok(clienteMap);
            }
            catch (Exception e)
            {
                return BadRequest(Resultado<Cliente>.Falha(e.Message).Erro); // (Andre) TODO: melhorar o uso de quando o resultado eh falho OU adicionar midware pra capturar excecoes nao tratadas
            }
        }

        [HttpPost]
        [Route("/cliente")]
        public IActionResult Post(ClienteModel model)
        {
            try
            {
                var cliente = Cliente.Map(model);
                if (cliente is null || String.IsNullOrEmpty(cliente.Nome) || cliente.Idade == 0) return Ok();

                var res = repo.Inserir(cliente);
                if (res.TemErro) return BadRequest(res.Erro);  // (Andre) TODO: encontrar uma resposta mais adequada que BadRequest

                return Ok(res.Sucesso);
            }
            catch (Exception e)
            {
                return BadRequest(Resultado<Cliente>.Falha(e.Message).Erro); // (Andre) TODO: melhorar o uso de quando o resultado eh falho
            }
        }

        [HttpGet]
        [Route("/cliente")]
        public IActionResult List()
        {
            try
            {
                var lista = repo.Listar();
                if (lista.TemErro) return BadRequest(lista.Erro);

                return Ok(lista.Sucesso);
            }
            catch (Exception e)
            {
                return BadRequest(Resultado<Cliente>.Falha(e.Message).Erro); // (Andre) TODO: melhorar o uso de quando o resultado eh falho
            }
        }
    }
}