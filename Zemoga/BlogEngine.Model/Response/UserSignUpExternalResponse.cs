using Newtonsoft.Json;

namespace BlogEngine.Model.Response
{
    public class UserSignUpExternalResponse
    {
        [JsonProperty(PropertyName = "email", NullValueHandling = NullValueHandling.Ignore)]
        public string Email { get; set; }

        [JsonProperty(PropertyName = "localId", NullValueHandling = NullValueHandling.Ignore)]
        public string LocalId { get; set; }

        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public Error Error { get; set; }

        [JsonProperty(PropertyName = "token", NullValueHandling = NullValueHandling.Ignore)]
        public string Token { get; set; }
    }

    public class Error
    {
        [JsonProperty(PropertyName = "code", NullValueHandling = NullValueHandling.Ignore)]
        public int Code { get; set; }

        [JsonProperty(PropertyName = "Message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }

}
