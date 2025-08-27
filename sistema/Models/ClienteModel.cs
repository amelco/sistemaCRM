namespace sistema.Models
{
    public class ClienteModel
    {
        public string Nome { get; set; } = "";
        public int Idade { get; set; }
        public decimal Salario { get; set; }
        public string Telefone { get; set; } = string.Empty;
    }
}