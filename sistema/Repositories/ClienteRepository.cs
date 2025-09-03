using sistema.Entities;
using sistema.Entities.Partials;
using sistema.Storage;
using System.Security.AccessControl;

namespace sistema.Repositories;
public class ClienteRepository : IClienteStorage
{
    private readonly List<Cliente> _clientes = [];
    private int _nextId = 1;

    public Resultado<List<Cliente>> ObterTodos() 
    {
        string query = "SELECT Id, Nome, Idade FROM Cliente";

        var clientes = RawSql.QueryAll(query, reader => new Cliente
        (
             reader.GetInt32(0),
             reader.GetString(1),
             reader.IsDBNull(2) ? 0 : reader.GetInt32(2)
        ));
        return Resultado<List<Cliente>>.Ok(clientes);
    }

    public Cliente? Atualizar(int id, PartialCliente partialCliente)
    {
        throw new NotImplementedException();
    }

    public bool Excluir(int id)
    {
        throw new NotImplementedException();
    }

    public Cliente Inserir(Cliente cliente)
    {
        var sql = $"insert into Cliente (Nome, Idade, Salario)" +
            $" values ('{cliente.Nome}', {cliente.Idade}, {cliente.Salario})";
        var retorno = RawSql.NonQuery(sql);
        return cliente;
    }

    //public List<Cliente>? Listar()
    //{
    //    var sql = $"select * from Cliente";
    //    var retorno = RawSql.Query(sql, ListClienteMapping);
    //    return retorno;
    //}

    public Resultado<Cliente>? Obter(int id)
    {
        var sql = $"select * from Cliente where id = {id}";
        var retorno = RawSql.Query(sql, ClienteMapping);
        return Resultado<Cliente>.Ok(retorno);
    }

    private Cliente ClienteMapping(Microsoft.Data.Sqlite.SqliteDataReader? reader)
    {
        // TODO (Andre): adicionar error handling
        if (reader is null) return new Cliente(-1, "", 0);

        int it = 0;
        var id = reader.GetInt32(it++);
        var nome = reader.GetString(it++);
        var idade = reader.GetInt32(it++);
        var salario = reader.IsDBNull(it) ? default : reader.GetDecimal(it++);  // (Andre): tratando coluna opcional da tabela.

        return new Cliente(id, nome, idade)
        {
            Salario = salario
        };
    }

    private Resultado<List<Cliente>> ListClienteMapping(Microsoft.Data.Sqlite.SqliteDataReader? reader)
    {
        // TODO (Andre): adicionar error handling
        var clientes = new List<Cliente>();
        if (reader is null) return Resultado<List<Cliente>>.Ok(clientes);

        do
        {
            var cliente = ClienteMapping(reader);
            clientes.Add(cliente);
        } while (reader.Read());

        return Resultado<List<Cliente>>.Ok(clientes);
    }
}
