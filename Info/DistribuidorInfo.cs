using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TCGErcilla.Info
{
    public class DistribuidorInfo
    {
        public int id { get; set; }
        public string nombre { get; set; }
        public DistribuidorInfo(int id, string nombre)
        {
            this.id = id;
            this.nombre = nombre;
        }
    }
}
