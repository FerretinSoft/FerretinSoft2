using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    partial class Empleado
    {
        public String nombreCompleto
        {
            get { return String.Join(" ", nombre, apPaterno, apMaterno); }
        }

        public String nombreTienda
        {
            get
            {
                if (ultimoEmpleadoTienda != null)
                    return ultimoEmpleadoTienda.Tienda.nombre;
                else
                    return "";
            }
        }
        public String nombreCargo
        {
            get
            {
                if (ultimoEmpleadoTienda != null)
                    return ultimoEmpleadoTienda.Cargo.nombre;
                else
                    return "";
            }
        }
        public String nombreEstado
        {
            get
            {
                return this.estado == 1 ? "Inactivo" : "Activo";
            }
        }

        private EmpleadoTienda ultimoEmpleadoTienda
        {
            get
            {
                if (this.EmpleadoTienda.Count(et => et.estado == 2) > 0)
                {
                    return this.EmpleadoTienda.Last(et => et.estado == 2);
                }
                else
                {
                    return null;
                }
            }
        }

        
        private decimal _ultimoSueldo=0;
        public decimal ultimoSueldo
        {
            get
            {
                if (_ultimoSueldo <= 0 && ultimoEmpleadoTienda != null)
                    _ultimoSueldo = ultimoEmpleadoTienda.sueldo.Value;
                return _ultimoSueldo;
            }
            set
            {
                verificaNuevoEmpleoTienda();  
                nuevoEmpleoTienda.sueldo = value;
                _ultimoSueldo = value;
            }
        }
        public DateTime ultimafechaIngreso
        {
            get
            {
                if (ultimoEmpleadoTienda != null)
                {
                    return ultimoEmpleadoTienda.fecInicio.Value;
                }
                else
                {
                    return DateTime.Today;
                }
            }
        }

        public Tienda _tiendaActual;
        public Tienda tiendaActual
        {
            get
            {
                if (_tiendaActual == null && ultimoEmpleadoTienda != null)
                    _tiendaActual = ultimoEmpleadoTienda.Tienda;
                return _tiendaActual;
            }
            set
            {
                verificaNuevoEmpleoTienda();
                nuevoEmpleoTienda.Tienda = value;
                _tiendaActual = value;
            }
        }
        public Cargo _cargoActual;
        public Cargo cargoActual
        {
            get
            {
                if (_cargoActual == null && ultimoEmpleadoTienda != null)
                    _cargoActual = ultimoEmpleadoTienda.Cargo;
                return _cargoActual;
            }
            set
            {
                verificaNuevoEmpleoTienda();
                nuevoEmpleoTienda.Cargo = value;
                _cargoActual = value;
            }
        }


        private void verificaNuevoEmpleoTienda (){

            if (nuevoEmpleoTienda == null)
            {
                nuevoEmpleoTienda = new EmpleadoTienda();
            }

            if (nuevoEmpleoTienda.Cargo == null)
            {
                nuevoEmpleoTienda.Cargo = cargoActual;
            }

            if (nuevoEmpleoTienda.Tienda == null)
            {
                nuevoEmpleoTienda.Tienda = tiendaActual;
            }

            if (nuevoEmpleoTienda.sueldo <= 0)
            {
                nuevoEmpleoTienda.sueldo = ultimoSueldo;         
            }
            if (!this.EmpleadoTienda.Contains(nuevoEmpleoTienda))
            {
                this.EmpleadoTienda.Add(nuevoEmpleoTienda);
            }
        }

       

        
        private EmpleadoTienda _nuevoEmpleoTienda;
        public EmpleadoTienda nuevoEmpleoTienda{
            get
            {
                return _nuevoEmpleoTienda;
            }
            set
            {
                _nuevoEmpleoTienda = value;
                _nuevoEmpleoTienda.estado = 2;
                _nuevoEmpleoTienda.fecInicio = DateTime.Today;
                if (ultimoEmpleadoTienda != null)
                {
                    ultimoEmpleadoTienda.fecFin = DateTime.Today;
                    ultimoEmpleadoTienda.estado = 1; //inactivar
                }
            }
        }

        
    }
}
