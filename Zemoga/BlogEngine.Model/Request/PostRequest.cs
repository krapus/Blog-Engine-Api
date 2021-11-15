using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Model.Request
{
    public class PostRequest
    {
        [Required(ErrorMessage = "Please enter title")]
        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter content")]
        [JsonProperty(PropertyName = "content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }
    }

    public class PostRejectRequest
    {
        [Required(ErrorMessage = "Please enter statusComment")]
        [JsonProperty(PropertyName = "statusComment", NullValueHandling = NullValueHandling.Ignore)]
        public string StatusComment { get; set; }
    }
}
