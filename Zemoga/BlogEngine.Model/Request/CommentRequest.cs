using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;

namespace BlogEngine.Model.Request
{
    public class CommentRequest
    {
        [Required(ErrorMessage = "Please enter postId")]
        [JsonProperty(PropertyName = "postId", NullValueHandling = NullValueHandling.Ignore)]
        public long PostId { get; set; }

        [Required(ErrorMessage = "Please enter description")]
        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }
    }
}
