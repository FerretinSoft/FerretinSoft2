using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;//Para el FerretinDataContext

namespace pe.edu.pucp.ferretin.controller.MRecursosHumanos
{
    public class MR_EmpleadoService : MR_ComunService
    {
        
        private static IEnumerable<Empleado> _listaEmpleados = null;

        private static IEnumerable<Empleado> listaEmpleados 
        {
            get
            {
                if (_listaEmpleados == null)
                {
                    obtenerListaEmpleados();
                }
                return _listaEmpleados;
            }
            set
            {
                _listaEmpleados = value;
            }
        }

        public static IEnumerable<Empleado> obtenerListaEmpleados()
        {
            _listaEmpleados = from p in db.Empleado
                            orderby p.codEmpleado
                            select p;
            return listaEmpleados;
        }

        public static Empleado obtenerEmpleadoByNroDoc(String dni)
        {
            IEnumerable<Empleado> empleados = (from e in listaEmpleados
                                             where e.dni != null && e.dni.Equals(dni)
                                             select e);
            if (empleados.Count() > 0)
                return empleados.First();
            else
                return null;
        }

        /*public static IEnumerable<Empleado> obtenerListaClientesBy(Empleado empleado)
        {
            return from c in listaEmpleados
                   where
                   (c.nroDoc != null && c.nroDoc.Contains(empleado.nroDoc)
                       && c.nombre != null && c.nombre.Contains(empleado.nombre)
                       && c.apPaterno != null && c.apPaterno.Contains(empleado.apPaterno)
                       && c.apMaterno != null && c.apMaterno.Contains(cliente.apMaterno)
                       && c.tipoDocumento != null && c.tipoDocumento.Contains(cliente.tipoDocumento)
                    )
                   orderby c.nroDoc
                   select c; 
        }*/

        public static void insertarEmpleado(Empleado empleado) 
        {
  
            db.Empleado.InsertOnSubmit(empleado); 
            db.SubmitChanges();
        }

        public static void actualizarEmpleado(Empleado empleado)
        {
            db.SubmitChanges();
        }
    }
}
