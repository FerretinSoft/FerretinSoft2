using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MRecursosHumanos
{
    public class MR_SharedService : ComunService
    {

        /// <summary>
        /// Obtiene un empleado segun su DNI
        /// </summary>
        /// <param name="dni">el DNI del empleado a Buscar</param>
        /// <returns></returns>
        public static Empleado obtenerEmpleadoPorDNI(String dni)
        {
            try
            {
                return db.Empleado.Single(e => e.dni.Equals(dni));
            }catch{
                return null;
            }
        }
    }
}
