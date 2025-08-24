using System.Net.Http.Json;
using sistema.frontend.Models;

namespace sistema.frontend.Services;

public class ClienteApiService
{
    private readonly HttpClient _http;

    public ClienteApiService(HttpClient http)
    {
        _http = http;
    }

    public async Task<List<Cliente>> GetClientesAsync()
        => await _http.GetFromJsonAsync<List<Cliente>>("api/clientes") ?? new();

    public async Task AddClienteAsync(Cliente cliente)
        => await _http.PostAsJsonAsync("api/clientes", cliente);
}
