using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class Cliente
    {

        public int tipo
        {
            get
            {
                if (tipoDocumento == "DNI")
                {
                    return 1;
                }
                else if (tipoDocumento == "RUC")
                {
                    return 2;
                }
                else
                    return 0;
            }
            set
            {
                if (value == 1)
                {
                    tipoDocumento = "DNI";
                }
                else if (value == 2)
                {
                    tipoDocumento = "RUC";
                }
                else
                    tipoDocumento = "";
                this.SendPropertyChanged("tipo");
            }
        }

        public String nombreCompleto
        {
            get
            {
                return String.Join(" ", nombre, apPaterno, apMaterno);
            }
        }
    }
}
