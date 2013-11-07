using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MVentas
{
    public class MV_ProformasService : MV_ComunService
    {
        public static bool insertarProforma(Proforma proforma){
            if (!db.Proforma.Contains(proforma))
            {
                db.Proforma.InsertOnSubmit(proforma);
                return enviarCambios();
            }
            else
            {
                return false;
            }
        }
    }
}
