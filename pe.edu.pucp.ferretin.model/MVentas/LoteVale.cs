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
                return moneda == 0 ? "Soles" : "Dolares";
            }
        }

        public string fechaEmisionString
        {
            get
            {
                if (fechaEmision != null)
                {
                    string fechaString = fechaEmision.ToString();
                    DateTime fecha = Convert.ToDateTime(fechaString);
                    fechaString = fecha.ToString("d");
                    return fechaString;
                }
                else
                    return null;
            }

        }

    }
}
