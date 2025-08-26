using sistema.Entities;
using sistema.Entities.Partials;

namespace sistema.Storage
{
    public interface IClienteStorage
    {
        Cliente? Obter(int id);
        List<Cliente>? Listar();     // TODO (Andre): filtro e paginacao
        Cliente? Atualizar(int id, PartialCliente partialCliente);  // retorna cliente atualizado somente apos a gravacao com sucesso
        bool Inserir(Cliente cliente);
        bool Excluir(int id);

        //ToDo (Jaque): Mudar no front para usar o Inserir e excluir esse método.
        Cliente Adicionar(Cliente cliente);
    }
}