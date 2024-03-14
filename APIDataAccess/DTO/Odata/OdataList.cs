using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace APIDataAccess.DTO.Odata
{
    public class OdataList<TModel>
    {
        [JsonPropertyName("@odata.context")]
        public string OdataContext { get; set; }

        [JsonPropertyName("value")]
        public List<TModel> Value { get; set; }
    }
}
