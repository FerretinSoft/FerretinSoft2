using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class Usuario
    {
        public String estadoAux
        {
            get
            {
                if (estado == 1) return "Activo";
                else
                    return "Inactivo";
            }
            set
            {
                if (value == "Activo") estado = 1;
                else
                    estado = 2;
                this.SendPropertyChanged("estadoAux");
            }

        }
    }
}
