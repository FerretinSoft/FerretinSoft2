using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class ProductoPrecio
    {


        public string fechaRegistroString
        {
            get
            {
                if (fechaRegistro != null)
                {
                    string fechaString = fechaRegistro.ToString();
                    DateTime fecha = Convert.ToDateTime(fechaString);
                    fechaString = fecha.ToString("d");
                    return fechaString;
                }
                else
                    return "";
            }

        }

        public string estadoString
        {
            get
            {
                return estado == false ? "Historico" : "Actual";
            }
        }

        public string monedaString
        {
            get
            {
                return moneda == 0 ? "Soles" : "Dolares";
            }
        }

    }
}
