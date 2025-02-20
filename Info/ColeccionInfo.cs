using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.Info
{
    public class ColeccionInfo : ICloneable
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        [JsonProperty("numero_cartas")]
        public int NumeroCartas { get; set; }
        [JsonProperty("fecha_lanzamiento")]
        public DateTime FechaLanzamiento { get; set; }
        [JsonProperty("url_imagen")]
        public string UrlImagen { get; set; }
        public object Clone()
        {
            return new ColeccionInfo
            {
                Id = this.Id,
                Nombre = this.Nombre,
                NumeroCartas = this.NumeroCartas,
                FechaLanzamiento = this.FechaLanzamiento,
                UrlImagen = this.UrlImagen
            };
        }
        public ColeccionInfo() { }
    }
}
