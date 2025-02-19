using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.Dto
{
    [Serializable]
    public class TipoProductoDto
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int? Id { get; set; }
        [JsonProperty("tipo")]
        public string Tipo { get; set; }
        public TipoProductoDto(string tipo)
        {
            Tipo = tipo;
        }
        public TipoProductoDto() { }
    }
}
