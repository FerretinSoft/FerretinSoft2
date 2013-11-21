using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class MovimientoProducto : INotifyPropertyChanged
    {
        partial void OncantidadChanged()
        {
            //Cantidad no puede ser negativa
            if (cantidad <= 0)
            {
                cantidad = 1;
                return;
            }
        }
    }
}
