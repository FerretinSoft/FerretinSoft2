using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    partial class Empleado
    {
        public String nombreCompleto
        {
            get { return String.Join(" ", nombre, apPaterno, apMaterno); }
        }
    }
}
