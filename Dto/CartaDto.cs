using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGErcilla.Info;

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
        [JsonProperty("urlImagen")]
        public string UrlImagen { get; set; }
        [JsonProperty("idColeccion")]
        public ColeccionDto Coleccion { get; set; }

        public string NombreColeccion { get; set; }
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
        
    }

}
