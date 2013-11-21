using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class GuiaRemisionProducto : INotifyPropertyChanged
    {
        partial void OncantidadRecibidaChanged()
        {
            if (cantidadRecibida < 0)
            {
                cantidadRecibida = 0;
                return;
            }


            if (DocumentoCompraProducto != null)
            {
                if (cantidadRecibida > DocumentoCompraProducto.cantidadRestante)
                {
                    cantidadRecibida = DocumentoCompraProducto.cantidadRestante;
                }
            }
        }
    }
}
