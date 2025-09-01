using Core.Entities;
using Core.Entities.Partials;

namespace sistema.Storage
{
    public interface IClienteStorage
    {
        Resultado<Cliente> Obter(int id);
        Resultado<List<Cliente>> Listar();     // TODO (Andre): filtro e paginacao
        Cliente? Atualizar(int id, PartialCliente partialCliente);  // retorna cliente atualizado somente apos a gravacao com sucesso
        Resultado<bool> Inserir(Cliente cliente);
        bool Excluir(int id);
    }
}