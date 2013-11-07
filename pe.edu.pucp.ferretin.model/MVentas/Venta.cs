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

        public double igvActual = 0;

        partial void OntotalChanged()
        {
            this.igv = Decimal.Round((this.total * (decimal)igvActual/100).Value,2);
            this.subTotal = this.total - this.igv;
            this.cobrado = 0;
            this.diferencia = this.total - this.cobrado;
        }

        partial void OncobradoChanged()
        {
            this.diferencia = this.total - this.cobrado;
        }

        public string fechaString
        {
            get
            {
                if (fecha != null)
                {
                    string fechaString = fecha.ToString();
                    DateTime fechaFormat = Convert.ToDateTime(fechaString);
                    fechaString = fechaFormat.ToString("d");
                    return fechaString;
                }
                else
                    return null;
            }

        }
        
    }
}
