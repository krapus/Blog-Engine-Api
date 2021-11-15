using Newtonsoft.Json;

namespace BlogEngine.Model.Response
{
    public class CommentCreationResponse
    {
        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public Error Error { get; set; }

        [JsonProperty(PropertyName = "Message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }
}
