using Core.Entities;
using Core.Entities.Partials;
using Storage;

namespace Backend
{
    public class ClienteRepository : IClienteStorage
    {
        const string MensagemErroMapeamento = "Mapeamento de cliente inexistente";

        public Cliente? Atualizar(int id, PartialCliente partialCliente)
        {
            throw new NotImplementedException();
        }

        public bool Excluir(int id)
        {
            throw new NotImplementedException();
        }

        public Resultado<bool> Inserir(Cliente cliente)
        {
            var sql = $"insert into Cliente (nome, idade) values ('{cliente.Nome}', {cliente.Idade})";
            var retorno = RawSql.NonQuery(sql);
            return Resultado<bool>.Ok(retorno > 0);
        }

        public Resultado<List<Cliente>> Listar()
        {
            var sql = $"select * from Cliente";
            var retorno = RawSql.Query(sql, ListClienteMapping);
            return retorno;
        }

        public Resultado<Cliente> Obter(int id)
        {
            var sql = $"select * from Cliente where id = {id}";
            var retorno = RawSql.Query(sql, ClienteMapping);
            return retorno;
        }

        private Resultado<Cliente> ClienteMapping(Microsoft.Data.Sqlite.SqliteDataReader? reader)
        {
            if (reader is null) return Resultado<Cliente>.Falha(MensagemErroMapeamento);

            int it = 0;
            var id = reader.GetInt32(it++);
            var nome = reader.GetString(it++);
            var idade = reader.GetInt32(it++);
            var salario = reader.IsDBNull(it) ? default : reader.GetDecimal(it++);  // (Andre): tratando coluna opcional da tabela.

            return Resultado<Cliente>.Ok(
                new Cliente(id, nome, idade)
                {
                    Salario = salario
                }
            );
        }

        private Resultado<List<Cliente>> ListClienteMapping(Microsoft.Data.Sqlite.SqliteDataReader? reader)
        {
            var clientes = new List<Cliente>();
            if (reader is null) return Resultado<List<Cliente>>.Falha(MensagemErroMapeamento);

            do
            {
                var cliente = ClienteMapping(reader);
                if (cliente.TemErro) return Resultado<List<Cliente>>.Falha(MensagemErroMapeamento);
                clientes.Add(cliente.Sucesso!);
            } while (reader.Read());

            return Resultado<List<Cliente>>.Ok(clientes);
        }
    }
}