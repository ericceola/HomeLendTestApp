using Newtonsoft.Json;

namespace Homelend.Domain.AppConfiguration
{
    public  class AppSettings
    {
        [JsonProperty("connectionString")]
        public string ConnectionString { get; set; }

        [JsonProperty("endPoint")]
        public EndPoint EndPoint { get; set; }
   
    }
}
