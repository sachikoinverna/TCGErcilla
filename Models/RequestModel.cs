﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.Models
{
    internal class RequestModel
    {
        // Propiedad que representa el método HTTP de la solicitud (GET, POST, etc.).
        public string Method { get; set; }

        // Propiedad que almacena la ruta de la solicitud dentro del servidor.
        public string Route { get; set; }

        // Propiedad que contiene los datos asociados a la solicitud (puede ser cualquier tipo de datos).
        public object Data { get; set; }

        // Constructor parametrizado de la clase RequestModel.
        public RequestModel(string method, string route, object data)
        {
            Method = method;
            Route = route;
            Data = data;
        }

        public RequestModel()
        {
        }
    }
}
