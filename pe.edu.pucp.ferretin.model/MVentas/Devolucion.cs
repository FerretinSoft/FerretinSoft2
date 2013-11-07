using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class Devolucion
    {
        public string fecEmisionString
        {
            get
            {
                if (fecEmision != null)
                {
                    string fechaString = fecEmision.ToString();
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
