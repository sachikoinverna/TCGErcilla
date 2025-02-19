using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.Info
{
    public class CartaInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        [JsonProperty("numero_coleccion")]
        public int NumeroColeccion { get; set; }
        [JsonProperty("url_imagen")]
        public string UrlImagen { get; set; }
        
    }
}
