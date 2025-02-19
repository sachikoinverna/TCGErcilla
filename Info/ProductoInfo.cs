using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
