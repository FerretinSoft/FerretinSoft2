using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class LoteVale
    {
        public string monedaString
        {
            get
            {
                return moneda == 1 ? "Soles" : "Dolares";
            }
        }

    }
}
