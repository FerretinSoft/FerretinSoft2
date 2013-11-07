using Microsoft.Win32;
using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace pe.edu.pucp.ferretin.viewmodel.MRecursosHumanos
{
    public class MR_AdministrarPersonalViewModel : ViewModelBase
    {
        #region Constructor
        public MR_AdministrarPersonalViewModel()
        {
            _empleado = new Empleado();
        }
        #endregion

        #region Valores para el cuadro de Búsqueda
        public String _searchDNI = "";
        public String searchDNI { get { return _searchDNI; } set { _searchDNI = value; NotifyPropertyChanged("searchDNI"); } }

        public String _searchNombre = "";
        public String searchNombre { get { return _searchNombre; } set { _searchNombre = value; NotifyPropertyChanged("searchNombre"); } }

        public String _searchCodigo = "";
        public String searchCodigo { get { return _searchCodigo; } set { _searchCodigo = value; NotifyPropertyChanged("searchCodigo"); } }

        public Cargo _searchCargo;
        public Cargo searchCargo { get { return _searchCargo; } set { _searchCargo = value; NotifyPropertyChanged("searchCargo"); } }

        public Tienda _searchTienda;
        public Tienda searchTienda { get { return _searchTienda; } set { _searchTienda = value; NotifyPropertyChanged("searchTienda"); } }

        private bool _soloSeleccionarVendedor = false;
        public bool soloSeleccionarVendedor
        {
            get
            {
                return _soloSeleccionarVendedor;
            }
            set
            {
                _soloSeleccionarVendedor = value;
                NotifyPropertyChanged("soloSeleccionarVendedor");
                NotifyPropertyChanged("nombreBotonGuardar");
                detallesTabHeader = value ? "Detalles" : "Agregar";
            }
        }

        public String nombreBotonGuardar
        {
            get
            {
                return soloSeleccionarVendedor ? "SELECCIONAR" : "GUARDAR";
            }
        }
        
        
        #endregion


        public IEnumerable<Cargo> listaCargosAdd
        {
            get
            {
                //Creo una nueva secuencia
                var sequence = Enumerable.Empty<Cargo>();
                //Primero agrego un item de Todos para que salga al inicio
                //Pongo el ID en 0 para que al buscar, no filtre nada cuando se selecciona todos
                IEnumerable<Cargo> items = new List<Cargo>();
                //Luego concateno el itemcon los elementos del combobox
                return items.Concat(MR_CargTieService.cargos);
            }
        }

        public IEnumerable<Cargo> listaCargos
        {
            get
            {
                //Creo una nueva secuencia
                var sequence = Enumerable.Empty<Cargo>();
                //Primero agrego un item de Todos para que salga al inicio
                //Pongo el ID en 0 para que al buscar, no filtre nada cuando se selecciona todos
                IEnumerable<Cargo> items = new Cargo[] { new Cargo { id = 0, nombre = "Todos" } };
                //Luego concateno el itemcon los elementos del combobox
                return items.Concat(MR_CargTieService.cargos);
            }
        }


        public IEnumerable<Tienda> listaTiendasAdd
        {
            get
            {
                //Creo una nueva secuencia
                var sequence = Enumerable.Empty<Tienda>();
                //Primero agrego un item de Todos para que salga al inicio
                //Pongo el ID en 0 para que al buscar, no filtre nada cuando se selecciona todos
                IEnumerable<Tienda> items = new List<Tienda>();
                //Luego concateno el itemcon los elementos del combobox
                return items.Concat(almacenes);
            }
        }

        public IEnumerable<Tienda> listaTiendas
        {
            get
            {
                //Creo una nueva secuencia
                var sequence = Enumerable.Empty<Tienda>();
                //Primero agrego un item de Todos para que salga al inicio
                //Pongo el ID en 0 para que al buscar, no filtre nada cuando se selecciona todos
                IEnumerable<Tienda> items = new Tienda[] { new Tienda { id = 0, nombre = "Todos" } };
                //Luego concateno el itemcon los elementos del combobox
                return items.Concat(almacenes);
            }
        }

        public IEnumerable<GradoInstruccion> gradosInstruccion
        {
            get
            {
                //return MR_ComunService.gradosInstruccion;
                //Creo una nueva secuencia
                var sequence = Enumerable.Empty<GradoInstruccion>();
                //Primero agrego un item de Todos para que salga al inicio
                //Pongo el ID en 0 para que al buscar, no filtre nada cuando se selecciona todos
                IEnumerable<GradoInstruccion> items = new GradoInstruccion[] { new GradoInstruccion { id = 0, nombre = "Todos" } };
                //Luego concateno el itemcon los elementos del combobox
                return items.Concat(MR_ComunService.gradosInstruccion);
                
            }
        }

        public IEnumerable<Turno> listaTurnos
        {
            get
            {
                //return MR_ComunService.gradosInstruccion;
                //Creo una nueva secuencia
                var sequence = Enumerable.Empty<Turno>();
                //Primero agrego un item de Todos para que salga al inicio
                //Pongo el ID en 0 para que al buscar, no filtre nada cuando se selecciona todos
                IEnumerable<Turno> items = new Turno[] { new Turno { id = 0, nombre = "Seleccionar" } };
                //Luego concateno el itemcon los elementos del combobox
                return items.Concat(MR_CargTieService.turnos);

            }
        }

        public bool isCreating
        {
            get
            {
                if (statusTab == Tab.AGREGAR)
                {
                    return true; //Se Activaran
                }
                else
                {
                    return false; //Se bloquearan par que no sean editables
                }
            }
        }

        #region Manejo de los Tabs
        public enum Tab
        {
            //Pestañas virtuales:
            //0       1        2          3
            BUSQUEDA, AGREGAR, MODIFICAR, DETALLES
        }
        private Tab _statusTab = Tab.BUSQUEDA; //pestaña default 
        public Tab statusTab
        {
            get
            {
                return _statusTab;
            }
            set
            {
                _statusTab = value;
                //Si cambió el estado de las pestañas también cambio los Header
                //Si la pestaña es para agregar nuevo, limpio los input
                switch (_statusTab)
                {
                    case Tab.BUSQUEDA: detallesTabHeader = "Agregar"; empleado = new Empleado();  break;//Si es agregar, creo un nuevo objeto Empleado
                    case Tab.AGREGAR: detallesTabHeader = "Agregar"; empleado = new Empleado(); break;//Si es agregar, creo un nuevo objeto Empleado
                    case Tab.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case Tab.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; empleado = new Empleado(); break;//Si es agregar, creo un nuevo objeto Empleado
                }
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
                NotifyPropertyChanged("isCreating"); //Para que se activen o desactiven los inputs
                NotifyPropertyChanged("listaEmpleadoTurno");

            }
        }
        //Usado para mover los tabs de acuerdo a las acciones realizadas
        public int currentIndexTab
        {
            get { return _statusTab == Tab.BUSQUEDA ? 0 : 1; }
            set { statusTab = value == 0 ? Tab.BUSQUEDA : Tab.AGREGAR; }
        }
        private String _detallesTabHeader = "Agregar"; //Default
        public String detallesTabHeader
        {
            get
            {
                return _detallesTabHeader;
            }
            set
            {
                _detallesTabHeader = value;
                NotifyPropertyChanged("detallesTabHeader");
            }
        }
        #endregion

        #region Lista Empleados y Edicion de Empleado
        private Empleado _empleado;
        public Empleado empleado
        {
            get
            {
                return _empleado;
            }
            set
            {
                _empleado = value;
                if (value.id_ubigeo_distrito != null)
                {
                    String id_distrito = value.id_ubigeo_distrito;
                    String id_provincia = value.UbigeoDistrito.id_ubig_provincia;
                    String id_departamento = value.UbigeoDistrito.UbigeoProvincia.id_ubig_departamento;
                    distritos = MR_EmpleadoService.distritos.Where(distrito => distrito.id_ubig_provincia.Equals(id_provincia));
                }
                NotifyPropertyChanged("empleado");
            }
        }
        
        private IEnumerable<Empleado> _listaEmpleados;
        public IEnumerable<Empleado> listaEmpleados
        {
            get
            {
                int cod = 0;
                if (searchCodigo.Length > 0)
                {
                    cod = int.Parse(searchCodigo);
                }
                _listaEmpleados = MR_EmpleadoService.buscarEmpleados(searchDNI, searchNombre, cod, searchCargo, searchTienda);

                return _listaEmpleados;
            }
            set
            {
                _listaEmpleados = value;
                NotifyPropertyChanged("listaEmpleados");
            }
        }
        #endregion


        private IEnumerable<EmpleadoTurno> _listaEmpleadoTurno;
        private List<EmpleadoTurno> _listaTemp = new List<EmpleadoTurno>();
        public IEnumerable<EmpleadoTurno> listaEmpleadoTurno
        {
            get
            {
                if (empleado.EmpleadoTurno.Count() > 0)
                    return empleado.EmpleadoTurno;
                else
                {
                    empleado.empleadoT();
                    _listaEmpleadoTurno = empleado.empleadoTurnos;
                    return _listaEmpleadoTurno;
                }
                /*
                EmpleadoTurno et2 = new EmpleadoTurno(); et2.Dia.id = 2; et2.Dia.nombre = "Lunes"; et2.estado = 1;
                EmpleadoTurno et3 = new EmpleadoTurno(); et3.Dia.id = 3; et3.Dia.nombre = "Lunes"; et3.estado = 1;
                EmpleadoTurno et4 = new EmpleadoTurno();  et4.Dia.id = 4; et4.Dia.nombre = "Lunes"; et4.estado = 1;
                EmpleadoTurno et5 = new EmpleadoTurno();  et5.Dia.id = 5; et5.Dia.nombre = "Lunes"; et5.estado = 1;
                EmpleadoTurno et6 = new EmpleadoTurno();  et6.Dia.id = 6; et6.Dia.nombre = "Lunes"; et6.estado = 1;
                EmpleadoTurno et7 = new EmpleadoTurno();  et7.Dia.id = 7; et7.Dia.nombre = "Lunes"; et7.estado = 1;
                */
            
                
                /*
                _listaEmpleadoTurno.ToList().Add(et2);
                _listaEmpleadoTurno.ToList().Add(et3);
                _listaEmpleadoTurno.ToList().Add(et4);
                _listaEmpleadoTurno.ToList().Add(et5);
                _listaEmpleadoTurno.ToList().Add(et6);
                _listaEmpleadoTurno.ToList().Add(et7);
                */
                //return _listaEmpleadoTurno;
                
            }

            set
            {
                _listaEmpleadoTurno = value;
    
            }
        }

        #region RelayCommand
        RelayCommand _actualizarListaEmpleadosCommand;
        public ICommand actualizarListaEmpleadosCommand
        {
            get
            {
                if (_actualizarListaEmpleadosCommand == null)
                {
                    _actualizarListaEmpleadosCommand = new RelayCommand(param => NotifyPropertyChanged("listaEmpleados"));
                }
                return _actualizarListaEmpleadosCommand;
            }
        }

        RelayCommand _agregarEmpleadoCommand;
        public ICommand agregarEmpleadoCommand
        {
            get
            {
                if (_agregarEmpleadoCommand == null)
                {
                    _agregarEmpleadoCommand = new RelayCommand(p => statusTab = Tab.AGREGAR);
                }
                return _agregarEmpleadoCommand;
            }
        }
        RelayCommand _viewEditEmpleadoCommand;
        public ICommand viewEditEmpleadoCommand
        {
            get
            {
                if (_viewEditEmpleadoCommand == null)
                {
                    _viewEditEmpleadoCommand = new RelayCommand(viewEditEmpleado);
                }
                return _viewEditEmpleadoCommand;
            }
        }
        RelayCommand _saveEmpleadoCommand;
        public ICommand saveEmpleadoCommand
        {
            get
            {
                if (_saveEmpleadoCommand == null)
                {
                    _saveEmpleadoCommand = new RelayCommand(saveEmpleado,canSaveExecute);
                }
                return _saveEmpleadoCommand;
            }
        }
        RelayCommand _cancelEmpleadoCommand;
        public ICommand cancelEmpleadoCommand
        {
            get
            {
                if (_cancelEmpleadoCommand == null)
                {
                    _cancelEmpleadoCommand = new RelayCommand(cancelEmpleado);
                }
                return _cancelEmpleadoCommand;
            }
        }

        RelayCommand _uploadImageCommand;
        public ICommand uploadImageCommand
        {
            get
            {
                if (_uploadImageCommand == null)
                {
                    _uploadImageCommand = new RelayCommand(uploadImage);
                }
                return _uploadImageCommand;
            }
        }
        #endregion


        private ImageSource _empleadoImagen;
        public ImageSource empleadoImagen
        {
            get
            {
                if (this.empleado.foto != null)
                {
                    MemoryStream strm = new MemoryStream();
                    strm.Write(empleado.foto.ToArray(), 0, empleado.foto.Length);
                    strm.Position = 0;
                    System.Drawing.Image img = System.Drawing.Image.FromStream(strm);

                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    MemoryStream memoryStream = new MemoryStream();
                    img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.EndInit();

                    _empleadoImagen = bitmapImage;
                }
                return _empleadoImagen;
            }
            set
            {
                _empleadoImagen = value;
                NotifyPropertyChanged("empleadoImagen");
            }
        }




        #region Comandos

        public void uploadImage(Object id)
        {
            OpenFileDialog op = new OpenFileDialog();
            op.Title = "Select a picture";
            op.Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
                "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
                "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
            {
                var bitmapImage = new BitmapImage(new Uri(op.FileName));
                byte[] file_byte;
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(bitmapImage));
                using (MemoryStream ms = new MemoryStream())
                {
                    encoder.Save(ms);
                    file_byte = ms.ToArray();
                }
                System.Data.Linq.Binary file_binary = new System.Data.Linq.Binary(file_byte);
                empleado.foto = file_binary;
                NotifyPropertyChanged("empleadoImagen");
            }
        }
        public void viewEditEmpleado(Object codEmpleado)
        {
            try
            {
                this.empleado = listaEmpleados.Single(empleado => empleado.codEmpleado == (int)codEmpleado);
                if (this.empleado.id_ubigeo_distrito != null)
                {
                    selectedProvincia = this.empleado.UbigeoDistrito.UbigeoProvincia;
                    selectedDepartamento = selectedProvincia.UbigeoDepartamento;
                }
                this.statusTab = Tab.MODIFICAR;
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        public void saveEmpleado(Object obj)
        {
            if (soloSeleccionarVendedor)
            {

            }
            else
            {
                /*Para actualizar un empleado existente*/
                if (empleado.id > 0)//Si existe
                {
                    ComunService.idVentana(2);                    
                    if (!MR_EmpleadoService.enviarCambios())
                    {
                        MessageBox.Show("No se pudo actualizar el empleado");
                    }
                    else
                    {
                        MessageBox.Show("Se actualizó el empleado con éxito");
                        this.statusTab = Tab.BUSQUEDA;
                        listaEmpleados = MR_EmpleadoService.listaEmpleados;
                    }
                }
                /*Para agregar un empleado nuevo*/
                else
                {
                    //Validacion de Campos Obligatorios//

                    ComunService.idVentana(1);
                    if (empleado.dni == null || empleado.dni == "") MessageBox.Show("Debe ingresar el campo DNI");
                    else if (empleado.nombre == null || empleado.nombre == "") MessageBox.Show("Debe ingresar el campo Nombre");
                    else if (empleado.apPaterno == null || empleado.nombre == "") MessageBox.Show("Debe ingresar el campo Apellido Paterno");
                    else if (empleado.apMaterno == null || empleado.nombre == "") MessageBox.Show("Debe ingresar el campo Apellido Materno");
                    else if (empleado.direccion == null || empleado.direccion == "") MessageBox.Show("Debe ingresar el campo Dirección");

                    else if (selectedDepartamento == null) MessageBox.Show("Debe ingresar el campo Departamento");
                    else if (selectedProvincia == null) MessageBox.Show("Debe ingresar el campo Provincia");
                    else if (empleado.UbigeoDistrito == null) MessageBox.Show("Debe ingresar el campo Distrito");
                    else if (empleado.telefono1 == null || empleado.telefono1 == "") MessageBox.Show("Debe ingresar el campo Teléfono");

                    else if (empleado.fecNacimiento == null) MessageBox.Show("Debe ingresar el campo Fecha de Nacimiento");
                    else if (empleado.sexo == null) MessageBox.Show("Debe ingresar el campo Sexo");


                    else if (empleado.cargoActual == null) MessageBox.Show("Debe ingresar el campo Cargo");
                    else if (empleado.tiendaActual == null) MessageBox.Show("Debe ingresar el campo Tienda");
                    else if (empleado.GradoInstruccion == null) MessageBox.Show("Debe ingresar el campo Grado de instrucción");
                    else if (empleado.ultimoSueldo <= 0) MessageBox.Show("Debe ingresar el campo Sueldo");

                    else if (empleado.estado == null) MessageBox.Show("Debe ingresar el campo Estado");
                    else
                    {
                        if (empleado.dni != null && empleado.nombre != null && empleado.apPaterno != null && empleado.apMaterno != null)
                        {
                            empleado.codEmpleado = 100060 + listaEmpleados.Count();

                            if (!MR_EmpleadoService.insertarEmpleado(empleado))
                            {
                                MessageBox.Show("No se pudo agregar el nuevo empleado");
                            }
                            else
                            {

                                MessageBox.Show("El empleado fue agregado con éxito");
                                this.statusTab = Tab.BUSQUEDA;
                                listaEmpleados = MR_EmpleadoService.listaEmpleados;
                                //NotifyPropertyChanged("EmpleadoTienda");//Para el historial de empleos
                            }
                        }
                    }
                }
            }
            NotifyPropertyChanged("listaEmpleados");
        }
        public void cancelEmpleado(Object obj)
        {
            this.statusTab = Tab.BUSQUEDA;
            listaEmpleados = MR_EmpleadoService.listaEmpleados;
        }


        private bool canSaveExecute(object obj)
        {
            return base.UIValidationErrorCount == 0 && this.empleado.Errors.Count == 0;
        }
        #endregion



    

    }
}
