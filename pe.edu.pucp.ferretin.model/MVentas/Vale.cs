using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class Vale
    {
        public string estadoString
        {
            get
            {
                return estado == 0 ? "Canjeable" : "Canjeado";
            }
        }
    }
}
