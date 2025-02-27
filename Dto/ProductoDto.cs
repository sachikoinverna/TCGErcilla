using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.Dto
{
    [Serializable]
    public class ProductoDto
    {
        [JsonProperty("id", NullValueHandling = NullValueHandling.Ignore)]
        public int? Id { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        [JsonProperty("fecha_lanzamiento")]
        public DateTime FechaLanzamiento { get; set; }
        [JsonProperty("url_imagen")]
        public string UrlImagen { get; set; }
        [JsonProperty("producto_distribuidor")]
        public ObservableCollection<DistribuidorDto> Distribuidores { get; set; }
        public ProductoDto(int id, string nombre, string urlImagen)
        {
            Id = id;
            Nombre = nombre;
            UrlImagen = urlImagen;
        }

        public ProductoDto()
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
        public class TipoProductoDto
        {
            [JsonProperty("id")]
            public int Id { get; }
            public TipoProductoDto(int id)
            {
                Id = id;
            }

        }
    }
}
