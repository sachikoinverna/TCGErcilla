using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGErcilla.Utils;
namespace TCGErcilla.Info
{
    public class ColeccionInfo : ICloneable
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        [JsonProperty("numeroCartas")]
        public int NumeroCartas { get; set; }
        [Newtonsoft.Json.JsonConverter(typeof(DateConverter))]
        [JsonProperty("fechaLanzamiento")]
        public DateTime FechaLanzamiento { get; set; }
        [JsonProperty("urlImagen")]
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
        public ColeccionInfo(int id, string nombre, int numeroCartas, DateTime fechaLanzamiento, string urlImagen)
        {
            Id = id;
            Nombre = nombre;
            NumeroCartas = numeroCartas;
            FechaLanzamiento = fechaLanzamiento;
            UrlImagen = urlImagen;
        }

        public ColeccionInfo() { }
        public override string ToString()
        {
            return Nombre;
        }
    }
}
