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

        public UbigeoDepartamento searchDepartamento
        {
            get 
            {
                return ComunService.departamentos.Single(d => d.nombre.Equals("LIMA"));
            }
        }
        
        private UbigeoProvincia _searchProvincia;
        public UbigeoProvincia searchProvincia
        {
            get
            {
                return _searchProvincia;
                //return ComunService.provincias.Single(d => d.id.Equals("1501"));//Es pronvincia de lima
            }
            set
            {
                _searchProvincia = value;
                NotifyPropertyChanged("searchProvincia");
                distritos = from d in ComunService.distritos where d.id_ubig_provincia == value.id select d;
            }
        }
        
        public UbigeoDistrito _searchDistrito;
        public UbigeoDistrito searchDistrito
        {
            get
            {
                return _searchDistrito;
                //return ComunService.distritos.Single(d => d.id_ubig_provincia.Equals("1501"));
            }
            set
            {
                _searchDistrito = value;
                NotifyPropertyChanged("searchDistrito");
                NotifyPropertyChanged("distritos");
            }
        }
       
        
        #endregion



        public List<String> tiposTurnos
        {
            get
            {
                return new List<string>() { "", "8:00am - 2:00 pm", "2:00pm -10:00 pm", "9:00am - 5:00 pm" };
            }
        }


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
                return items.Concat(tiendas);
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
                return items.Concat(tiendas);
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
                IEnumerable<GradoInstruccion> items = new List<GradoInstruccion>();
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
                    case Tab.BUSQUEDA: detallesTabHeader = "Agregar"; empleadoImagen = null; ; empleado = new Empleado(); break;//Si es agregar, creo un nuevo objeto Empleado
                    case Tab.AGREGAR: detallesTabHeader = "Agregar"; empleado = new Empleado();
                        try { this.selectedDepartamento.id = "15"; empleado.EmpleadoTienda = null; empleadoImagen = null; }
                        catch(Exception e) { }
                            break;//Si es agregar, creo un nuevo objeto Empleado
                    case Tab.MODIFICAR: detallesTabHeader = "Modificar"; empleadoImagen = null; break;
                    case Tab.DETALLES: detallesTabHeader = "Detalles";  break;
                    default: detallesTabHeader = "Agregar"; empleado = new Empleado(); break;//Si es agregar, creo un nuevo objeto Empleado
                }
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
                NotifyPropertyChanged("isCreating"); //Para que se activen o desactiven los inputs
                ////////////NotifyPropertyChanged("listaEmpleadoTurno");
                ////////////NotifyPropertyChanged("listaEmpleadoTiendas");
                NotifyPropertyChanged("empleadoImagen");

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
                NotifyPropertyChanged("empleadoImagen");
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
                    _saveEmpleadoCommand = new RelayCommand(saveEmpleado);
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

                foreach (EmpleadoTurno et in this.empleado.EmpleadoTurno)
                {
                    if (et.estado == 1 && et.id_turno == null) et.estado = 0;
                    if (et.estado == 0) et.id_turno = null;
                    if (et.id_turno == 0) et.id_turno = null;

                }

              

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
                        foreach (EmpleadoTurno et in empleado.EmpleadoTurno)
                        {
                            if (et.estado == 1 && et.id_turno == null) et.estado = 0;
                            if (et.id_turno == 0) et.id_turno = null;


                        }
                        if (VerificaCamposObligatorios(empleado) && VerificaTurnosEmpleado(empleado))
                        {
                            

                            if (!MR_EmpleadoService.enviarCambios())
                            {
                                MessageBox.Show("No se pudo actualizar el empleado");
                               
                            }
                            else
                            {
                                MessageBox.Show("Se actualizó el empleado con éxito");
                                this.statusTab = Tab.BUSQUEDA;
                                listaEmpleados = MR_EmpleadoService.listaEmpleados;
                                ////////////NotifyPropertyChanged("listaEmpleadoTiendas");//Para el historial de empleos
                            }
                        }
                    }
                    /*Para agregar un empleado nuevo*/
                    else
                    {
                        //Validacion de Campos Obligatorios//

                        ComunService.idVentana(1);

                        if (VerificaDNIEmpleado(empleado)&& VerificaCamposObligatorios(empleado)&& (empleado.id==0) )
                        {
                            
                                empleado.empleadoT();
                                empleado.codEmpleado = 100060 + listaEmpleados.Count();

                                if (!MR_EmpleadoService.insertarEmpleado(empleado))
                                {
                                    MessageBox.Show("No se pudo agregar el nuevo empleado");
                                }
                                else
                                {

                                    MessageBox.Show("El empleado fue agregado con éxito");
                                    this.statusTab = Tab.BUSQUEDA;
                           
                                }
                        }
                        //////}
                    }
                }

        
            NotifyPropertyChanged("listaEmpleados");
            
            NotifyPropertyChanged("listaEmpleadoTiendas");


         
            
        }
        public void cancelEmpleado(Object obj)
        {
            MessageBoxResult result = MessageBox.Show("Al salir perderá todos los datos ingresados. ¿Desea continuar?",
            "ATENCIÓN", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
            if (result == MessageBoxResult.OK)
            {
                // Yes code here
                this.statusTab = Tab.BUSQUEDA;
                listaEmpleados = MR_EmpleadoService.listaEmpleados;
            }
      
        }



        private bool canSaveExecute(object obj)
        {
            //////////return base.UIValidationErrorCount == 0 && this.empleado.Errors.Count == 0;
            return base.UIValidationErrorCount == 0;
        }


        public bool VerificaCamposObligatorios(Empleado empleado)
        {

            if (empleado.nombre == null || empleado.nombre == "" || empleado.apPaterno == null || empleado.apPaterno == "" ||
                empleado.apMaterno == null || empleado.apMaterno == "" || empleado.direccion == null || empleado.direccion == "" ||
                empleado.direccion == null || empleado.direccion == "" || selectedDepartamento == null || selectedProvincia == null ||
                empleado.UbigeoDistrito == null || empleado.telefono1 == null || empleado.telefono1 == "" || empleado.telefono1 == "" ||
                empleado.fecNacimiento == null || empleado.sexo ==null || empleado.cargoActual == null || empleado.tiendaActual == null ||
                empleado.GradoInstruccion == null || empleado.ultimoSueldo <= 0  || empleado.estado == 0)

            {
                MessageBox.Show("Completar todos los datos obligatorios");
                return false;
            }
            return true;
        }

        public bool VerificaDNIEmpleado(Empleado empleado)
        {
            if (listaEmpleados.Count(et => et.dni.Equals(empleado.dni)) > 0)
            {
                MessageBox.Show("Eror : El DNI ingresado ya ha sido registrado");
                return false;
            }
            return true;
        }

        public bool VerificaTurnosEmpleado(Empleado empleado)
        {
            int numTurnos = 0;
            //et.id_turno == null && 
            foreach (var empTu in empleado.EmpleadoTurno.Where(et => et.estado < 1))
            {
                numTurnos++;
                if (empTu.id_turno > 0 && empTu.estado == 0)
                {
                    MessageBox.Show("Debe seleccionar un turno de trabajo");
                    return false;      
                }
                
                if (((empTu.id_turno != 1) && (empTu.id_turno != 2) && (empTu.id_turno != 3)) && empTu.estado == 1)
                {
                    MessageBox.Show("Debe seleccionar un turno de trabajo");
                    return false;
                }

                if (empTu.id_turno == 0 && empTu.estado == 1)
                {
                    MessageBox.Show("Debe seleccionar un turno de trabajo");
                    return false;
                }



            }

            if (numTurnos == 7)
            {
                MessageBox.Show("Debe seleccionar los turnos de trabajo");
                return false;
            }
            else
                return true;
        }


   
        #endregion




    

    }
}
