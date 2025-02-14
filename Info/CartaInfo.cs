using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.Info
{
    public class CartaInfo
    {
        public int Id;
        public string Nombre;
        public int NumeroCartas;
        public DateTime FechaLanzamiento;
        public CartaInfo(int id, string nombre, int numerocartas) {
         this.Id = id;
            this.Nombre = nombre;
            this.NumeroCartas = numerocartas;
            this.FechaLanzamiento = DateTime.Now;
        }
        public CartaInfo() { 

        }
        public object Clone()
        {
            return new CartaInfo
            {
                Id = this.Id,
                Nombre = this.Nombre,
            };
        }
    }
}
