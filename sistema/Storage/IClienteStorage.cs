using sistema.Entities;
using sistema.Entities.Partials;

namespace sistema.Storage
{
    public interface IClienteStorage
    {
        Cliente? Obter(int id);
        List<Cliente>? Listar();     // TODO (Andre): filtro e paginacao
        Cliente? Atualizar(int id, PartialCliente partialCliente);  // retorna cliente atualizado somente apos a gravacao com sucesso
        Cliente Inserir(Cliente cliente);
        bool Excluir(int id);
    }
}