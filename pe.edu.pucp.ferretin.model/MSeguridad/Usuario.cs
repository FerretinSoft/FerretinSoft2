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
                if (estado == true) return "Activo";
                else
                    return "Inactivo";
            }
            set
            {
                if (value == "Activo") estado = true;
                else
                    estado = false;
                this.SendPropertyChanged("estadoAux");
            }

        }
    }
}
