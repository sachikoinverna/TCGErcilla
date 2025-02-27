using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.Dto
{
    [Serializable]
    public class ColeccionDto
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int? Id { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        [JsonProperty("numero_cartas")]
        public int NumeroCartas { get; set; }
        [JsonProperty("fecha_lanzamiento")]
        public DateTime FechaLanzamiento { get; set; } = DateTime.Now;
        [JsonProperty("url_imagen")]
        public string UrlImagen { get; set; }

        public ColeccionDto(int id, string nombre, int numeroCartas, string urlImagen)
        {
            Id = id;
            Nombre = nombre;
            NumeroCartas = numeroCartas;
            UrlImagen = urlImagen;
        }

        public ColeccionDto()
        {
        }
    }
}
