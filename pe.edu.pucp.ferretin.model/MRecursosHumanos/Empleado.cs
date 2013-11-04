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
                        return 2;
                
            }

            set
            {
                if (value == 0)
                    this.sexo = 'M';
                else
                if (value == 1)
                    this.sexo = 'F';
            
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
            }

            //Lleno la tienda nuevoEmpleoTienda con lo leido de la vista
            if (nuevoEmpleoTienda.Tienda == null)
            {
                nuevoEmpleoTienda.Tienda = tiendaActual;
            }

            //Lleno el sueldo nuevoEmpleoTienda con lo leido de la vista
            if (nuevoEmpleoTienda.sueldo <= 0)
            {
                nuevoEmpleoTienda.sueldo = ultimoSueldo;         
            }


            //Lleno nuevoEmpleoTienda sino esta contenido
            if (!this.EmpleadoTienda.Contains(nuevoEmpleoTienda))
            {
                this.EmpleadoTienda.Add(nuevoEmpleoTienda);
            }

            ////if (EmpleadoTienda.Count(et => et.id_empleado == nuevoEmpleoTienda.id_empleado && et.id_cargo == nuevoEmpleoTienda.id_cargo && et.sueldo == nuevoEmpleoTienda.sueldo && et.id_tienda == nuevoEmpleoTienda.id_tienda) == 0)
            ////{
            ////    this.EmpleadoTienda.Add(nuevoEmpleoTienda);

            ////}


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


        #region Zona de Validaciones

        #region Código por default de la interfaz heredada
        public string Error
        {
            get { throw new NotImplementedException(); }
        }

        protected Dictionary<string, string> _errors = new Dictionary<string, string>();
        public IDictionary<string, string> Errors
        {
            get { return _errors; }
        }
        #endregion

        public string this[string columnName]
        {
            get
            {
                string errorMessage = string.Empty;
                this.Errors.Remove(columnName);

                switch (columnName)
                {
                    case "dni":
                        if (String.IsNullOrEmpty(this.dni))
                        {
                            errorMessage = "Debe ingresar el DNI.";
                        }
                        else
                        {
                            Int64 nro = 0;
                            Int64.TryParse(dni, out nro);

                            if (nro < 10000000 || nro > 99999999)
                            {
                                errorMessage = "El DNI debe tener 8 números";
                            }
                        }
                        break;

                    case "nombre":
                        if (String.IsNullOrEmpty(this.nombre))
                        {
                            errorMessage = "Debe ingresar un nombre del personal";
                        }
                        break;
                    case "apPaterno":
                        if (String.IsNullOrEmpty(this.apPaterno))
                        {
                            errorMessage = "Debe ingresar un Apellido Paterno del Personal";
                        }
                        break;
                    case "apMaterno":
                        if (String.IsNullOrEmpty(this.apMaterno))
                        {
                            errorMessage = "Debe ingresar un Apellido Materno del Personal";
                        }
                        break;

                    case "telefono1":
                        if (String.IsNullOrEmpty(this.telefono1))
                        {
                            errorMessage = "El Teléfono 1 no debe estar vacio.";
                        }

                        break;

                    case "telefono2":
                        if (String.IsNullOrEmpty(this.telefono2))
                        {
                            errorMessage = "El Teléfono 2 no debe estar vacio.";
                        }
                        break;

                    case "email":
                        if (String.IsNullOrEmpty(this.email))
                        {
                            errorMessage = "Debe ingresar un correo electronico";
                        }
                        break;
                    case "direccion":
                        if (String.IsNullOrEmpty(this.direccion))
                        {
                            errorMessage = "Debe ingresar la direccion del personal";
                        }
                        break;

                    case "UbigeoDistrito":
                        if (UbigeoDistrito == null || String.IsNullOrEmpty(UbigeoDistrito.id))
                        {
                            errorMessage = "Debe seleccionar un Pais, Provincia, Ciudad y Distrito";
                        }
                        break;

                    case "ultimoSueldo":
                        string strsueldo = "";
                        this.ultimoSueldo.ToString(strsueldo);
                        if (String.IsNullOrEmpty(strsueldo))
                        {
                            errorMessage = "Debe ingresar el Sueldo del empleado";
                        }
                        break;

                    case "cargoActual":
                        if (cargoActual == null || String.IsNullOrEmpty(this.cargoActual.nombre))
                        {
                            errorMessage = "Debe seleccionar un Cargo";
                        }
                        break;

                    case "tiendaActual":
                        if (tiendaActual == null || String.IsNullOrEmpty(this.tiendaActual.nombre))
                        {
                            errorMessage = "Debe seleccionar una Tienda";
                        }
                        break;
                    case "GradoInstruccion":
                        if (GradoInstruccion == null || String.IsNullOrEmpty(this.GradoInstruccion.nombre))
                        {
                            errorMessage = "Debe seleccionar un Grado de  Instruccion";
                        }
                        break;
                    case "estado":
                        if (String.IsNullOrEmpty(Convert.ToString(this.estado)))
                        {
                            errorMessage = "Debe seleccionar el estado";
                        }
                        break;

                }

                if (!string.IsNullOrEmpty(errorMessage))
                {
                    this.Errors.Add(columnName, errorMessage);
                }

                return errorMessage;
            }
        }

        #endregion




        
    }
}
