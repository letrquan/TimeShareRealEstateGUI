using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIDataAccess.DTO
{
    public class ResponseMessage
    {
        [JsonPropertyName("type")]
        public string Type { get; set; }
        [JsonPropertyName("title")]
        public string Title { get; set; }
        [JsonPropertyName("status")]
        public int Status { get; set; }
        [JsonPropertyName("instance")]
        public string Instance { get; set; }
        [JsonPropertyName("traceId")]
        public string TraceId { get; set; }
    }
}
