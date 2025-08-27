using sistema.Models;

namespace sistema.Entities
{
    public class Cliente
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public int Idade { get; set; }
        public string? Email { get; set; }
        public decimal Salario { get; set; }
        public string? Telefone { get; set; } = string.Empty;

        public Cliente(int id, string Nome, int Idade)
        {
            this.Id = id;
            this.Nome = Nome;
            this.Idade = Idade;
        }

        public static ClienteModel Map(Cliente cliente)
        {
            // TODO (Andre): mapear somente campos existentes no model. Dessa forma, precisamos apenas mudar um local (model) 
            //       para exibir ou nao qualquer campo existente na Entidade.
            return new ClienteModel
            {
                Nome = cliente.Nome,
                Idade = cliente.Idade,
                Salario = cliente.Salario
            };
        }

        public static List<ClienteModel> MapList(List<Cliente> clientes)
        {
            List<ClienteModel> models = new();
            clientes.ForEach(cliente =>
            {
                models.Add(Map(cliente));
            });
            return models;
        }

        public static Cliente Map(ClienteModel model)
        {
            // TODO (Andre): adiciocat logica de mapeamento em outro local que nao o core, ja que ele esta muito acoplado com o sqlite
            // (Andre): -1 indica que nao importa o valor, ele vai ser automaticamente adicionado pelo sqlite
            return new Cliente(-1, model.Nome, model.Idade);
        }
    }
}