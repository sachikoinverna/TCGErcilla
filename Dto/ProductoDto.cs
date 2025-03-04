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
        [JsonProperty("idTipo")]
        public TipoProductoDto TipoProducto { get; set; }
        [JsonProperty("idColeccion")]
        public ColeccionDto Coleccion { get; set; }
        [JsonProperty("urlImagen")]
        public string UrlImagen { get; set; }
        [JsonProperty("distribuidores")]
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



    }
}
