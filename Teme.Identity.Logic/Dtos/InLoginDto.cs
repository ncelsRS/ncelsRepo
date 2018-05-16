using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace Teme.Identity.Logic.Dtos
{
    public class InLoginDto
    {
        [Required]
        [JsonProperty("username")]
        public string UserName { get; set; }

        [Required]
        [JsonProperty("password")]
        public string Password { get; set; }
    }
}
