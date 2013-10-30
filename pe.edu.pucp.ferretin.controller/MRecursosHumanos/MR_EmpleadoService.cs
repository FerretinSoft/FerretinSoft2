using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;
using System.Data.Linq;//Para el FerretinDataContext

namespace pe.edu.pucp.ferretin.controller.MRecursosHumanos
{
    public class MR_EmpleadoService : MR_ComunService
    {
        
        private static IEnumerable<Empleado> _listaEmpleados = null;
        public static IEnumerable<Empleado> listaEmpleados 
        {
            get
            {
                if (_listaEmpleados == null)
                {
                    _listaEmpleados = db.Empleado;
                }

                db.Refresh(RefreshMode.OverwriteCurrentValues, _listaEmpleados);
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


        public static bool insertarEmpleado(Empleado empleado) 
        {
            if (!db.Empleado.Contains(empleado))
            {
                db.Empleado.InsertOnSubmit(empleado); 
                return enviarCambios();
                  
            }
            else
                return false;
        }

        public static void actualizarEmpleado(Empleado empleado)
        {
            db.SubmitChanges();
        }

        public static IEnumerable<Empleado> buscarEmpleados(string searchDni, string searchNombre, Cargo searchCargo, Tienda searchTienda, int searchCodigo)
        {
            return listaEmpleados.Where(e => e.dni != null && e.dni.Contains(searchDni))
               .Where(e => e.nombre != null && e.nombre.Contains(searchNombre));

            //COMPLETAR
            
        }
    }
}
