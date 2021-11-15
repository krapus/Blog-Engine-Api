using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace BlogEngine.Model.Response
{
    public class PostCreationResponse
    {
        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public Error Error { get; set; }

        [JsonProperty(PropertyName = "Message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }

    public class PostResponse
    {
        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id { get; set; }

        [JsonProperty(PropertyName = "title", NullValueHandling = NullValueHandling.Ignore)]
        public string Title { get; set; }

        [JsonProperty(PropertyName = "content", NullValueHandling = NullValueHandling.Ignore)]
        public string Content { get; set; }

        [JsonProperty(PropertyName = "published", NullValueHandling = NullValueHandling.Ignore)]
        public DateTime Published { get; set; }

        [JsonProperty(PropertyName = "statusComment", NullValueHandling = NullValueHandling.Ignore)]
        public string StatusComment { get; set; }

        [JsonProperty(PropertyName = "comments", NullValueHandling = NullValueHandling.Ignore)]
        public List<PostComment> Comments { get; set; }

    }

    public class PostEditionResponse
    {
        [JsonProperty(PropertyName = "error", NullValueHandling = NullValueHandling.Ignore)]
        public Error Error { get; set; }

        [JsonProperty(PropertyName = "Message", NullValueHandling = NullValueHandling.Ignore)]
        public string Message { get; set; }
    }

    public class PostComment
    {
        [JsonProperty(PropertyName = "description", NullValueHandling = NullValueHandling.Ignore)]
        public string Description { get; set; }

        [JsonProperty(PropertyName = "id", NullValueHandling = NullValueHandling.Ignore)]
        public long Id { get; set; }
    }
}
