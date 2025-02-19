using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.Info
{
    public class DistribuidorInfo
    {
        [JsonProperty("id_distribuidor")]
        public int Id { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
    }
}