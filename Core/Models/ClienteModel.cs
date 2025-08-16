using Newtonsoft.Json;

namespace Core.Models
{
    public class ClienteModel
    {
        public string Nome { get; set; } = "";
        public int Idade { get; set; }
        // public decimal Salario { get; set; }
        
        public static ClienteModel? JsonToModel(string json)
        {
            return JsonConvert.DeserializeObject<ClienteModel>(json);
        }

        public static List<ClienteModel>? JsonToListModel(string json)
        {
            return JsonConvert.DeserializeObject<List<ClienteModel>>(json);
        }
    }
}