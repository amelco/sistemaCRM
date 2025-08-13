using Core.Entities;
using Core.Entities.Partials;
using Core.Models;
using Storage;

namespace Backend
{
    public class ClienteRepository : IClienteStorage
    {

        public Cliente? Atualizar(int id, PartialCliente partialCliente)
        {
            throw new NotImplementedException();
        }

        public bool Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public bool Inserir(Cliente cliente)
        {
            var sql = $"insert into Cliente (nome, idade) values ('{cliente.Nome}', {cliente.Idade})";
            var retorno = RawSql.NonQuery(sql);
            return retorno > 0;
        }

        public List<Cliente>? Listar()
        {
            var sql = $"select * from Cliente";
            var retorno = RawSql.Query(sql, ListClienteMapping);
            return retorno;
        }

        public Cliente? Obter(int id)
        {
            var sql = $"select * from Cliente where id = {id}";
            var retorno = RawSql.Query(sql, ClienteMapping);
            return retorno;
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

        private List<Cliente> ListClienteMapping(Microsoft.Data.Sqlite.SqliteDataReader? reader)
        {
            // TODO (Andre): adicionar error handling
            var clientes = new List<Cliente>();
            if (reader is null) return clientes;

            do
            {
                var cliente = ClienteMapping(reader);
                clientes.Add(cliente);
            } while (reader.Read());

            return clientes;
        }
    }
}