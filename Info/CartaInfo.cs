using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TCGErcilla.Utils;

namespace TCGErcilla.Info
{
    public class CartaInfo
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("nombre")]
        public string Nombre { get; set; }
        [JsonProperty("numeroColeccion")]
        public int NumeroColeccion { get; set; }
        private string _imagenUrl;

        [JsonProperty("urlImagen")]
        public string UrlImagen
        {
            set
            {
                if (value is not null)
                {
                    _imagenUrl = value;
                }
                else
                {
                    _imagenUrl = "carta_default.png";
                }
            }
            get { return _imagenUrl; }
        }
        [JsonProperty("idColeccion")]
        public ColeccionInfo SelectedColeccion { get; set; }
        //public ColeccionInfo Coleccion { get; set; }
        
        //public ColeccionInfo SelectedColeccion { get; set; }
        
        public string NombreColeccion { get; set; }

        public object Clone()
        {
            return new CartaInfo
            {
                Id = this.Id,
                Nombre = this.Nombre,
                NumeroColeccion = this.NumeroColeccion,
                UrlImagen = this.UrlImagen,
                SelectedColeccion = this.SelectedColeccion
            };
        }

        //public class ColeccionInfo1
        //{
        //    [JsonProperty("id")]

        //    public int Id { get; set; }
        //    [JsonProperty("nombre")]
        //    public string Nombre { get; set; }
        //    [JsonProperty("numeroCartas")]
        //    public int NumeroCartas { get; set; }
        //    [Newtonsoft.Json.JsonConverter(typeof(DateConverter))]
        //    [JsonProperty("fechaLanzamiento")]
        //    public DateTime FechaLanzamiento { get; set; } = DateTime.Now;
        //    [JsonProperty("urlImagen")]
        //    public string UrlImagen { get; set; }


        //}
        public override string ToString()
        {
            return Nombre;
        }
    }
}
