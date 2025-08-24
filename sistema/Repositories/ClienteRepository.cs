using sistema.Entities;

namespace sistema.Repositories;
public class ClienteRepository
{
    private readonly List<Cliente> _clientes = [];
    private int _nextId = 1;

    public List<Cliente> ObterTodos() => _clientes;

    public Cliente Adicionar(Cliente cliente)
    {
        cliente.Id = _nextId++;
        _clientes.Add(cliente);
        return cliente;
    }
}
