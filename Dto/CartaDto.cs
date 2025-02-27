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
        [JsonProperty("idColeccion")]
        public ColeccionDto Coleccion { get; set; }
        public CartaDto(int id, string nombre, int numeroColeccion,string urlImagen,ColeccionDto coleccion)
        {
            Id = id;
            Nombre = nombre;
            Coleccion = coleccion;
            NumeroColeccion = numeroColeccion;
            UrlImagen = urlImagen;
        }

        public CartaDto()
        {
        }
        
    }

}
