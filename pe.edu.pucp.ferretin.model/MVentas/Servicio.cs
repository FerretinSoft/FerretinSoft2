using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class Servicio
    {
        public string estadoString
        {
            get
            {
                return (estado==1?"Pendiente":(estado==2?"Facturado":(estado==3?"Finalizado":(estado==4?"Anulado":"Unknow"))));
            }
        }

    }
}
