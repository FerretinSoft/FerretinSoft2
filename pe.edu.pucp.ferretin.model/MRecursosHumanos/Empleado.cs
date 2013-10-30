﻿using System;
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
                //estado == 2, activo, 1==inactivo
                return this.EmpleadoTienda.Single(et => et.estado == 2).Tienda.nombre;
            }
        }
        public String nombreCargo
        {
            get
            {
                //estado == 2, activo, 1==inactivo
                return this.EmpleadoTienda.Single(et => et.estado == 2).Cargo.nombre;
            }
        }
        public String nombreEstado
        {
            get
            {
                return this.estado == 1 ? "Activo" : "Inactivo";
            }
        }

        private decimal _ultimoSueldo;
        public decimal ultimoSueldo
        {
            get
            {
                _ultimoSueldo = this.EmpleadoTienda.Single(et => et.estado == 2).sueldo.Value;
                return _ultimoSueldo;
            }
            set
            {
                _ultimoSueldo = value;
                //Agregar logica para cambiar el ultimo sueldo
            }
        }

        public DateTime ultimafechaIngreso
        {
            get
            {
                return this.EmpleadoTienda.Single(et => et.estado == 2).fecInicio.Value;
            }
        }

        public Tienda _tiendaActual;
        public Tienda tiendaActual
        {
            get
            {
                _tiendaActual = this.EmpleadoTienda.Single(et => et.estado == 2).Tienda;
                return _tiendaActual;
            }
            set
            {
                //codigo cuando se cambie de tienda al empledo
                _tiendaActual = value;
            }
        }

        public Cargo _cargoActual;
        public Cargo cargoActual
        {
            get
            {
                _cargoActual =  this.EmpleadoTienda.Single(et => et.estado == 2).Cargo;
                return _cargoActual;
            }
            set
            {
                //codigo cuando se cambie de tienda al empledo
                _cargoActual = value;
            }
        }
    }
}
