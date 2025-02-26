using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.Dto
{
    [Serializable]
    public class CartaDto
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int? Id { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        [JsonProperty("numeroColeccion")]
        public int NumeroColeccion { get; set; }
        [JsonProperty("url_imagen")]
        public string UrlImagen { get; set; }
        public CartaDto(int id, string nombre, int numeroColeccion,string urlImagen)
        {
            Id = id;
            Nombre = nombre;
            NumeroColeccion = numeroColeccion;
            UrlImagen = urlImagen;
        }

        public CartaDto()
        {
        }

        public class ColeccionDto
        {
            [JsonProperty("id")]
            public int Id { get; }
            public ColeccionDto(int id)
            {
                Id = id;
            }

        }
    }

}
