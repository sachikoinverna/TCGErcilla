using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace TCGErcilla.Info
{
    public class ProductoInfo : ICloneable
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        [JsonProperty("url_imagen")]
        public string UrlImagen { get; set; }
        [JsonProperty("tipo_producto")]
        public TipoProductoInfo TipoProducto { get; set; }
        [JsonProperty("coleccion")]
        public ColeccionInfo Coleccion { get; set; }
        [JsonProperty("distribuidores")]
        public HashSet<DistribuidorInfo> Distribuidores { get; set; }

        public class TipoProductoInfo
        {
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("tipo")]
            public string tipo { get; set; }


        }


        public class ColeccionInfo
        {
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("nombre")]
            public string Nombre { get; set; }

        }


        public class DistribuidorInfo
        {
            [JsonProperty("id")]
            public int Id { get; set; }
            [JsonProperty("nombre")]
            public string Nombre { get; set; }

        }

        public object Clone()
        {
            return new ProductoInfo
            {
                Id = this.Id,
                Nombre = this.Nombre,
            };
        }
        public ProductoInfo()
        {

        }
        public ProductoInfo(int id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
        }
    }
}
