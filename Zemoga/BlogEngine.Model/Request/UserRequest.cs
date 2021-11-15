using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Model.Request
{
    public class UserSignUpRequest : UserBase
    {
        [Required(ErrorMessage = "Please enter rol")]
        [JsonProperty(PropertyName = "rol", NullValueHandling = NullValueHandling.Ignore)]
        public int Rol { get; set; }
    }

    public class UserBase
    {
        [Required(ErrorMessage = "Please enter email")]
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Please enter password")]
        [JsonProperty(PropertyName = "password", NullValueHandling = NullValueHandling.Ignore)]
        public string Password { get; set; }
    }
}
