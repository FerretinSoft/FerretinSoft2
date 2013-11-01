using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class EmpleadoTienda
    {
        public String fechaInicioString
        {
            get
            {
                return fecInicio.Value.ToString("d/MMM/yyyy");
            }
        }
        public String fechaFinString
        {
            get
            {
                if (fecFin != null)
                    return fecFin.Value.ToString("d/MMM/yyyy");
                else
                    return "";
            }
        }
    }
}
