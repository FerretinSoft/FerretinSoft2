using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class NotaCredito
    {
        public string estadoString
        {
            get
            {
                return estado == 1 ? "EXPIRADA" : "APLICABLE";
            }
        }
        public string fechaEmisionString
        { get
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

        public string fechaVencimientoString
        {
            get
            {
                if (fechaVencimiento != null)
                {
                    string fechaString = fechaVencimiento.ToString();
                    DateTime fecha = Convert.ToDateTime(fechaString);
                    fechaString = fecha.ToString("d");
                    return fechaString;
                }
                else
                    return null;
            }

        }

        public string importeString
        {
            get
            {
                if (importe != null)
                {
                    return "S/. " + importe;
                }
                else
                    return null;
            }

        }

    }
}
