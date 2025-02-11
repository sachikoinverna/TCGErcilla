using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.Info
{
    public class DistribuidorInfo
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public DistribuidorInfo(int id, string nombre)
        {
            this.Id = id;
            this.Nombre = nombre;
        }
    }
}
