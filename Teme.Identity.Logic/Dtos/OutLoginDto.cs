using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Teme.Identity.Logic.Dtos
{
    public class OutLoginDto
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }
        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }
}
