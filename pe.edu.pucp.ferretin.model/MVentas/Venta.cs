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

        public string puntosInfoString
        {
            get
            {
                if (Cliente!=null)
                {
                    return "PUNTOS - ACTUAL: " + Cliente.puntosActual.ToString() + " GANADOS: " + puntosGanados.ToString() + (puntosCanjeados>0?" CANJEADOS: " + puntosCanjeados.ToString():"");
                }
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

        partial void OntotalChanged()
        {
            this.subTotal = Decimal.Round(this.total.Value/(1+((decimal)igvPorcentaje/100)), 2);
            this.igv = Decimal.Round(this.total.Value - this.subTotal.Value, 2);
            
            this.cobrado = 0;
            this.diferencia = Decimal.Round(this.total.Value - this.cobrado.Value,2);
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

        public string fechaHoraString
        {
            get
            {
                if (fecha != null)
                {
                    string fechaString = fecha.ToString();
                    DateTime fechaFormat = Convert.ToDateTime(fechaString);
                    fechaString = fechaFormat.ToString("d/mm/yyyy hh:mm:ss");
                    return fechaString;
                }
                else
                {
                    return null;
                }
            }
        }

        public string totalString
        {
            get
            {
                return "S/. " + total.ToString();
            }
        }

        public string subTotalString
        {
            get
            {
                return "S/. " +  subTotal;
            }
        }

        public string igvString
        {
            get
            {
                return "S/. " + igv;
            }
        }

        public string tipoDocVentaString
        {
            get
            {
                return tipoDocumento == 0 ? "Boleta" : "Factura";
            }
        }

    }
}
