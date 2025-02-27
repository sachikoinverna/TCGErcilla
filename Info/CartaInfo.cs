﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        [JsonProperty("urlImagen")]
        public string UrlImagen { get; set; }
        [JsonProperty("idColeccion")]
        public ColeccionInfo Coleccion { get; set; }
        public object Clone()
        {
            return new CartaInfo
            {
                Id = this.Id,
                Nombre = this.Nombre,
                NumeroColeccion = this.NumeroColeccion,
                UrlImagen = this.UrlImagen,
                Coleccion = this.Coleccion
            };
        }
    }
}
