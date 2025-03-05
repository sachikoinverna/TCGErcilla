using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
                    _imagenUrl = "product_default.png";
                }
            }
            get { return _imagenUrl; }
        }
        [JsonProperty("idTipo")]
        public TipoProductoInfo SelectedTipoProducto { get; set; }
        [JsonProperty("idColeccion")]
        public ColeccionInfo SelectedColeccion { get; set; }
        [JsonProperty("distribuidores")]
        public ObservableCollection<DistribuidorInfo> Distribuidores { get; set; }
        public DistribuidorInfo SelectedDistribuidor { get; set; }

        //public class TipoProductoInfo
        //{
        //    [JsonProperty("id")]
        //    public int Id { get; set; }
        //    [JsonProperty("tipo")]
        //    public string tipo { get; set; }


        //}


        //public class ColeccionInfo
        //{
        //    [JsonProperty("id")]
        //    public int Id { get; set; }
        //    [JsonProperty("nombre")]
        //    public string Nombre { get; set; }

        //}


        //public class DistribuidorInfo
        //{
        //    [JsonProperty("id")]
        //    public int Id { get; set; }
        //    [JsonProperty("nombre")]
        //    public string Nombre { get; set; }

        //}
        public object Clone()
        {
            return new ProductoInfo
            {
                Id = this.Id,
                Nombre = this.Nombre,
                UrlImagen = this.UrlImagen,
                SelectedTipoProducto = this.SelectedTipoProducto,
                SelectedColeccion = this.SelectedColeccion,
                Distribuidores = this.Distribuidores
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
        public override string ToString()
        {
            return Nombre;
        }
    }
}
