using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class VentaMedioPago
    {
        partial void OnmontoChanged()
        {
            Venta.cobrado = Venta.VentaMedioPago.Sum(vmp => vmp.monto);
        }
    }
}
