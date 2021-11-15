using Newtonsoft.Json;

namespace BlogEngine.Model.Request
{
    public class UserSignUpExternalRequest
    {
        [JsonProperty(PropertyName = "email")]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "password")]
        public string Password { get; set; }

        [JsonProperty(PropertyName = "returnSecureToken")]
        public bool ReturnSecureToken { get; set; }
    }
}
