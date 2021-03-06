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
                if (ultimoEmpleadoTienda != null)
                    return ultimoEmpleadoTienda.Tienda.nombre;
                else
                    return "";
            }
        }

        public String nombreEstado
        {
            get
            {
                ///EStado INACTIVO es 0 y estado ACTIVO es 1
                if (this.estado == 0)
                    return "Inactivo";
                else
                    if (this.estado == 1)
                        return "Activo";
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

        public int nombreSexo
        {
            get
            {
                if (this.sexo == 'M')
                    return 0;
                else
                   if (this.sexo == 'F')
                    return 1;
                else
               
                   return -1;
             
                
            }

            set
            {
                if (value == 0)
                    this.sexo = 'M';
                else
              
                        this.sexo = 'F';
                       
            }
        
        
        }

        private EmpleadoTienda ultimoEmpleadoTienda
        {
            get
            {
                if (this.EmpleadoTienda.Count(et => et.estado == 1) > 0)
                {
                    return this.EmpleadoTienda.Last(et => et.estado == 1);
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
                if (_ultimoSueldo <= 0 && ultimoEmpleadoTienda != null && ultimoEmpleadoTienda.sueldo !=null)
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

            //Lleno el cargo nuevoEmpleoTienda con lo leido de la vista
            if (nuevoEmpleoTienda.Cargo == null)
            {

                nuevoEmpleoTienda.Cargo = cargoActual;
                //////modificacion
                ////nuevoEmpleoTienda.Tienda = tiendaActual;
                ////nuevoEmpleoTienda.sueldo = ultimoSueldo;

                //////Finmodificacion
            }

            //Lleno la tienda nuevoEmpleoTienda con lo leido de la vista
            if (nuevoEmpleoTienda.Tienda == null)
            {
                nuevoEmpleoTienda.Tienda = tiendaActual;
                //////modificacion
                ////nuevoEmpleoTienda.Cargo = cargoActual;
                ////nuevoEmpleoTienda.sueldo = ultimoSueldo;

                //////Finmodificacion
            }

            //Lleno el sueldo nuevoEmpleoTienda con lo leido de la vista
            if (nuevoEmpleoTienda.sueldo <= 0 || nuevoEmpleoTienda.sueldo==null)
            {
                nuevoEmpleoTienda.sueldo = ultimoSueldo;
                //////modificacion
                ////nuevoEmpleoTienda.Cargo = cargoActual;
                ////nuevoEmpleoTienda.Tienda = tiendaActual;

                //////Finmodificacion
            }


            //Lleno nuevoEmpleoTienda sino esta contenido
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
                _nuevoEmpleoTienda.estado = 1;
                _nuevoEmpleoTienda.fecInicio = DateTime.Today;
                if (ultimoEmpleadoTienda != null)
                {
                    ultimoEmpleadoTienda.fecFin = DateTime.Today;
                    ultimoEmpleadoTienda.estado = 0; //inactivar
                }
            }
        }


        /************EmpleadoTurno******************/

        public IEnumerable<EmpleadoTurno> empleadoTurnos = null;
        public void empleadoT()
        {
                if ((this.EmpleadoTurno.Where(t => t.id_empleado == this.id)).Count() <= 0)
                {
                    //Creo una nueva secuencia
                    var sequence = Enumerable.Empty<EmpleadoTurno>();
                    //Primero agrego un item de Todos para que salga al inicio
                    //Pongo el ID en 0 para que al buscar, no filtre nada cuando se selecciona todos
                    IEnumerable<EmpleadoTurno> empleadoTurnoL = new EmpleadoTurno[] { new EmpleadoTurno { id_empleado = this.id, Empleado = this, id_dia = 1, estado = 0 } };
                    IEnumerable<EmpleadoTurno> empleadoTurnoM = new EmpleadoTurno[] { new EmpleadoTurno { id_empleado = this.id, Empleado = this, id_dia = 2, estado = 0 } };
                    IEnumerable<EmpleadoTurno> empleadoTurnoMi = new EmpleadoTurno[] { new EmpleadoTurno { id_empleado = this.id, Empleado = this, id_dia = 3, estado = 0 } };
                    IEnumerable<EmpleadoTurno> empleadoTurnoJ = new EmpleadoTurno[] { new EmpleadoTurno { id_empleado = this.id, Empleado = this, id_dia = 4, estado = 0 } };
                    IEnumerable<EmpleadoTurno> empleadoTurnoV = new EmpleadoTurno[] { new EmpleadoTurno { id_empleado = this.id, Empleado = this, id_dia = 5, estado = 0 } };
                    IEnumerable<EmpleadoTurno> empleadoTurnoS = new EmpleadoTurno[] { new EmpleadoTurno { id_empleado = this.id, Empleado = this, id_dia = 6, estado = 0 } };
                    IEnumerable<EmpleadoTurno> empleadoTurnoD = new EmpleadoTurno[] { new EmpleadoTurno { id_empleado = this.id, Empleado = this, id_dia = 7, estado = 0 } };
                    sequence.Concat(empleadoTurnoL).Concat(empleadoTurnoM).Concat(empleadoTurnoMi).Concat(empleadoTurnoJ).Concat(empleadoTurnoV).Concat(empleadoTurnoS).Concat(empleadoTurnoD);
                    //empleadoT = sequence;
                    this.empleadoTurnos = sequence;
                }
        }



        ////#region Zona de Validaciones

        ////#region Código por default de la interfaz heredada
        ////public string Error
        ////{
        ////    get { throw new NotImplementedException(); }
        ////}

        ////protected Dictionary<string, string> _errors = new Dictionary<string, string>();
        ////public IDictionary<string, string> Errors
        ////{
        ////    get { return _errors; }
        ////}
        ////#endregion

        ////public string this[string columnName]
        ////{
        ////    get
        ////    {
        ////        string errorMessage = string.Empty;
        ////        this.Errors.Remove(columnName);

        ////        switch (columnName)
        ////        {
        ////            case "dni":
        ////                if (String.IsNullOrEmpty(this.dni))
        ////                {
        ////                    errorMessage = "Debe ingresar el DNI.";
        ////                }
        ////                else
        ////                {
        ////                    Int64 nro = 0;
        ////                    Int64.TryParse(dni, out nro);

        ////                    if (nro < 10000000 || nro > 99999999)
        ////                    {
        ////                        errorMessage = "El DNI debe tener 8 números";
        ////                    }
        ////                }
        ////                break;

        ////            case "nombre":
        ////                if (String.IsNullOrEmpty(this.nombre))
        ////                {
        ////                    errorMessage = "Debe ingresar un nombre del personal";
        ////                }
        ////                break;
        ////            case "apPaterno":
        ////                if (String.IsNullOrEmpty(this.apPaterno))
        ////                {
        ////                    errorMessage = "Debe ingresar un Apellido Paterno del Personal";
        ////                }
        ////                break;
        ////            case "apMaterno":
        ////                if (String.IsNullOrEmpty(this.apMaterno))
        ////                {
        ////                    errorMessage = "Debe ingresar un Apellido Materno del Personal";
        ////                }
        ////                break;

        ////            case "telefono1":
        ////                if (String.IsNullOrEmpty(this.telefono1))
        ////                {
        ////                    errorMessage = "El Teléfono 1 no debe estar vacio.";
        ////                }

        ////                break;

        ////            case "telefono2":
        ////                if (String.IsNullOrEmpty(this.telefono2))
        ////                {
        ////                    errorMessage = "El Teléfono 2 no debe estar vacio.";
        ////                }
        ////                break;

        ////            case "email":
        ////                if (String.IsNullOrEmpty(this.email))
        ////                {
        ////                    errorMessage = "Debe ingresar un correo electronico";
        ////                }
        ////                break;
        ////            case "direccion":
        ////                if (String.IsNullOrEmpty(this.direccion))
        ////                {
        ////                    errorMessage = "Debe ingresar la direccion del personal";
        ////                }
        ////                break;

        ////            case "UbigeoDistrito":
        ////                if (UbigeoDistrito == null || String.IsNullOrEmpty(UbigeoDistrito.id))
        ////                {
        ////                    errorMessage = "Debe seleccionar un Pais, Provincia, Ciudad y Distrito";
        ////                }
        ////                break;

        ////            case "ultimoSueldo":
        ////                string strsueldo = "";
        ////                this.ultimoSueldo.ToString(strsueldo);
        ////                if (String.IsNullOrEmpty(strsueldo))
        ////                {
        ////                    errorMessage = "Debe ingresar el Sueldo del empleado";
        ////                }
        ////                break;

        ////            case "cargoActual":
        ////                if (cargoActual == null || String.IsNullOrEmpty(this.cargoActual.nombre))
        ////                {
        ////                    errorMessage = "Debe seleccionar un Cargo";
        ////                }
        ////                break;

        ////            case "tiendaActual":
        ////                if (tiendaActual == null || String.IsNullOrEmpty(this.tiendaActual.nombre))
        ////                {
        ////                    errorMessage = "Debe seleccionar una Tienda";
        ////                }
        ////                break;
        ////            case "GradoInstruccion":
        ////                if (GradoInstruccion == null || String.IsNullOrEmpty(this.GradoInstruccion.nombre))
        ////                {
        ////                    errorMessage = "Debe seleccionar un Grado de  Instruccion";
        ////                }
        ////                break;
        ////            case "estado":
        ////                if (String.IsNullOrEmpty(Convert.ToString(this.estado)))
        ////                {
        ////                    errorMessage = "Debe seleccionar el estado";
        ////                }
        ////                break;

        ////        }

        ////        if (!string.IsNullOrEmpty(errorMessage))
        ////        {
        ////            this.Errors.Add(columnName, errorMessage);
        ////        }

        ////        return errorMessage;
        ////    }
        ////}

        ////#endregion




        
    }
}
