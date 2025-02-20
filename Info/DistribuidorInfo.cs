using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.Info
{
    public class DistribuidorInfo : ICloneable
    {
        [JsonProperty("id_distribuidor")]
        public int Id { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        public DistribuidorInfo()
        {

        }
        public object Clone()
        {
            return new DistribuidorInfo
            {
                Id = this.Id,
                Nombre = this.Nombre
            };
        }
    }
}