using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class Venta
    {
        public String nombreCompletoCliente
        {
            get
            {
                if (Cliente != null)
                    return String.Join(" ", Cliente.nombre, Cliente.apPaterno, Cliente.apMaterno);
                else
                    return "";
            }
        }

        public String nombreCompletoVendedor
        {
            get
            {
                if (Usuario != null)
                    return String.Join(" ", Usuario.Empleado.nombreCompleto);
                else
                    return "";
            }
        }


    }
}
