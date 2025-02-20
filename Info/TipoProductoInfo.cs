using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.Info
{
    public class TipoProductoInfo : ICloneable
    {
        public TipoProductoInfo(int id,string tipo)
        {
            this.Id = id;
            this.Tipo = tipo;
        }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("tipo")]
        public string Tipo { get; set; }
        public TipoProductoInfo()
        {

        }
        public object Clone()
        {
            return new TipoProductoInfo
            {
                Id = this.Id,
                Tipo = this.Tipo
            };
        }
    }
}
