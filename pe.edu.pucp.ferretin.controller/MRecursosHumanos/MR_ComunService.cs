using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MRecursosHumanos
{
    /// <summary>
    /// Esta Clase contiene todos los servicios Comunes para el Módulo de Recursos Humanos
    /// además los servicios proporcionados a otros módulos
    /// </summary>
    public class MR_ComunService : MR_SharedService
    {

        public static IEnumerable<GradoInstruccion> gradosInstruccion
        {
            get
            {
                return db.GradoInstruccion;
            }
        }
    }
}
