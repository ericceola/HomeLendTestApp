using Newtonsoft.Json;

namespace Homelend.Domain.AppConfiguration
{
    public class EndPoint
    {
        [JsonProperty("nome")]
        public string Nome { get; set; }

        [JsonProperty("metodo")]
        public string Metodo { get; set; }

        [JsonProperty("timeOut")]
        public int TimeOut { get; set; }

        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
