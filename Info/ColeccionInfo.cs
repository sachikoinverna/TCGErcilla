using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.Info
{
    public class ColeccionInfo
    {
        public int Id;
        public string Nombre;
        public int NumeroCartas;
        public DateTime FechaLanzamiento;
        public ColeccionInfo(int id, string nombre, int numerocartas)
        {
            this.Id = id;
            this.Nombre = nombre;
            this.NumeroCartas = numerocartas;
            this.FechaLanzamiento = DateTime.Now;
        }
    }
}
