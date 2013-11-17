using Microsoft.Win32;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.model;
//using pe.edu.pucp.ferretin.tdd.MSeguridad;
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


namespace pe.edu.pucp.ferretin.viewmodel.MSeguridad
{
    public class MS_AdministrarTiendasViewModel : ViewModelBase
    {
        /*
        private static Tienda _tnd;
        public static Tienda tnd
        {
            get
            {
                return _tnd;
            }
            set
            {
                _tnd = value;                  
            }
        }
        */

        
        private string _NameAlmacen=null;
        public string NameAlmacen
        {
            get
            {
                 if (_NameAlmacen==null) _NameAlmacen = almacen.nombre;
                return _NameAlmacen;
            }
            set
            {
                _NameAlmacen = value;               
                NotifyPropertyChanged("NameAlmacen");
            }
        }
        



        //public MS_AdministrarTiendasViewModel()
        //{
        //    this.Valor = almacen.nombre;
        //}


        #region Valores para el cuadro de Búsqueda

        public String _dniJefe = "";
        public String dniJefe { get { return _dniJefe; } set { _dniJefe = value; NotifyPropertyChanged("dniJefe"); } }
        private String _searchCodTienda = "";
        public String searchCodTienda { get { return _searchCodTienda; } set { _searchCodTienda = value; } }
        public String _searchNombre = "";
        public String searchNombre { get { return _searchNombre; } set { _searchNombre = value; } }
        private int _searchEstado = 0;
        public int searchEstado { get { return _searchEstado; } set { _searchEstado = value; NotifyPropertyChanged("searchEstado"); } }
        
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
            }
            set
            {
                _searchProvincia = value;
                NotifyPropertyChanged("searchProvincia");
                distritos = from d in ComunService.distritos where d.id_ubig_provincia == value.id select d;
            }
        }
        private int _searchTipo = 0;
        public int searchTipo { get { return _searchTipo; } set { _searchTipo = value; NotifyPropertyChanged("searchTipo"); } }
        public UbigeoDistrito _searchDistrito;
        public UbigeoDistrito searchDistrito
        {
            get
            {
                return _searchDistrito;
            }
            set
            {
                _searchDistrito = value;
                NotifyPropertyChanged("searchDistrito");
                NotifyPropertyChanged("distritos");
            }
        }
        #endregion


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
                    case Tab.BUSQUEDA: detallesTabHeader = "Agregar"; almacen = new Tienda(); tiendaImagen = null; break;//Si es agregar, creo un nuevo objeto Almacen
                    case Tab.AGREGAR: detallesTabHeader = "Agregar"; almacen = new Tienda();                        
                        selectedTipo = -1;
                        almacen.fecCreacion = DateTime.Today;
                        almacen.estado = 1;
                        tiendaImagen = null;                        
                        break;//Si es agregar, creo un nuevo objeto Almacen
                    case Tab.MODIFICAR: detallesTabHeader = "Modificar"; tiendaImagen = null; break;
                    case Tab.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; almacen = new Tienda(); break;//Si es agregar, creo un nuevo objeto Almacen
                }
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
                NotifyPropertyChanged("selectedTipo");                
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

        #region Lista de Tiendas y Edición de Tiendas
        private Tienda _almacen;
        public Tienda almacen
        {
            get
            {                
                return _almacen;
            }
            set
            {
                _almacen = value;                
                if (value.id_ubigeo != null)
                {
                    String id_distrito = value.id_ubigeo;
                    String id_provincia = value.UbigeoDistrito.id_ubig_provincia;
                    String id_departamento = value.UbigeoDistrito.UbigeoProvincia.id_ubig_departamento;
                    distritos = MS_TiendaService.distritos.Where(distrito => distrito.id_ubig_provincia.Equals(id_provincia));
                }
                if (almacen.Empleado != null)
                {
                    almacenEmpleadoDni = almacen.Empleado.dni;
                }
                NotifyPropertyChanged("almacen");
                NotifyPropertyChanged("tiendaImagen");
            }
        }

        private IEnumerable<Tienda> _listaAlmacenes;
        public IEnumerable<Tienda> listaAlmacenes
        {
            get
            {
                _listaAlmacenes = MS_TiendaService.buscar(searchCodTienda, searchNombre,searchDistrito,searchTipo,searchEstado);
                return _listaAlmacenes;
            }
            set
            {
                _listaAlmacenes = value;
                NotifyPropertyChanged("listaAlmacenes");
            }
        }
        #endregion

        #region Para la seleccion del Jefe de Tienda
        private String _almacenEmpleadoDni = "";
        public String almacenEmpleadoDni
        {
            get
            {
                return _almacenEmpleadoDni;
            }
            set
            {
                _almacenEmpleadoDni = value;
                if (value.Length > 0) //Se puede hacer una validacion para que sea igual a 8
                {
                    Empleado empleado = MR_SharedService.obtenerEmpleadoPorDNI(value);;
                    if (empleado == null)
                    {
                        MessageBox.Show("No existe un empleado con el DNI " + value + ".");
                    }
                    else
                    {
                        almacen.Empleado = empleado;
                    }
                }
                NotifyPropertyChanged("almacenEmpleadoDni");
                NotifyPropertyChanged("almacen");
            }
        }
        #endregion

        #region RalayCommand
        RelayCommand _actualizarListaAlmacenesCommand;
        public ICommand actualizarListaAlmacenesCommand
        {
            get
            {
                if (_actualizarListaAlmacenesCommand == null)
                {
                    _actualizarListaAlmacenesCommand = new RelayCommand(param => NotifyPropertyChanged("listaAlmacenes"));
                }
                return _actualizarListaAlmacenesCommand;
            }
        }
        RelayCommand _agregarAlmacenCommand;
        public ICommand agregarAlmacenCommand
        {
            get
            {
                if (_agregarAlmacenCommand == null)
                {
                    _agregarAlmacenCommand = new RelayCommand(p=>statusTab=Tab.AGREGAR);
                }
                return _agregarAlmacenCommand;
            }
        }
        RelayCommand _viewEditAlmacenCommand;
        public ICommand viewEditAlmacenCommand
        {
            get
            {
                if (_viewEditAlmacenCommand == null)
                {
                    _viewEditAlmacenCommand = new RelayCommand(viewEditAlmacen);
                }
                return _viewEditAlmacenCommand;
            }
        }
        RelayCommand _saveAlmacenCommand;
        public ICommand saveAlmacenCommand
        {
            get
            {
                if (_saveAlmacenCommand == null)
                {
                    _saveAlmacenCommand = new RelayCommand(saveAlmacen);
                }
                return _saveAlmacenCommand;
            }
        }
      
        /**************************************************/
        RelayCommand _buscarJefeCommand;
        public ICommand buscarJefeCommand
        {
            get
            {
                if (_buscarJefeCommand == null)
                {
                    _buscarJefeCommand = new RelayCommand(buscarJefe);
                }
                return _buscarJefeCommand;
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

        #region Comandos
        /**************************************************/
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
                almacen.foto = file_binary;
                NotifyPropertyChanged("tiendaImagen");
            }
        }
        /**************************************************/
        public void viewEditAlmacen(Object id)
        {
            try
            {
                this.almacen = listaAlmacenes.Single(almacen => almacen.id == (int)id);                
                if (this.almacen.id_ubigeo != null)
                {
                    selectedProvincia = this.almacen.UbigeoDistrito.UbigeoProvincia;
                    selectedDepartamento = selectedProvincia.UbigeoDepartamento;
                }
                this.statusTab = Tab.MODIFICAR;                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /**************************************************/
        public void saveAlmacen(Object obj)
        {
            if (almacen.estado == 0) almacen.fecCierre = DateTime.Today; else { almacen.fecCierre = null; }

            if (almacen.id > 0)//Actualizar Tienda
            {
                ComunService.idVentana(8);
                if (VerificaCamposObligatorios(almacen) && VerificaHorario(almacen))
                {
                    if (!MS_TiendaService.enviarCambios())
                    {
                        MessageBox.Show("No se pudo actualizar la tienda");
                    }
                    else
                    {
                        MessageBox.Show("La tienda fue guardada con éxito");
                        this.statusTab = Tab.BUSQUEDA;
                    }
                }
            }
            else
            {
                //Para una tienda nueva
                /*Se envia el almacen para analizar sus valores ingresados TDD*/
                //ComunService.tiendaAg(almacen);
                //MS_AdministrarTiendasTest.nombre_tienda_debe_ser_diferente_de_vacio();
                ComunService.idVentana(7);
                if (VerificaCamposObligatorios(almacen) && VerificaHorario(almacen))
                {
                    if (!MS_TiendaService.insertarAlmacen(almacen))
                    {
                        MessageBox.Show("No se pudo agregar la nueva tienda porque esta ya existe");
                    }
                    else
                    {
                        MessageBox.Show("La Tienda fue agregada con éxito");
                        this.statusTab = Tab.BUSQUEDA;
                    }
                }
            }
            NotifyPropertyChanged("listaAlmacenes");
        }
        /**************************************************/
        //Mensaje de Advertencia al presionar el boton CANCELAR
        public void cancelAlmacen(Object obj)
        {
            MessageBoxResult result =MessageBox.Show("Al salir, perderá todos los datos ingresados. ¿Desea continuar?",
            "ATENCIÓN", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
            if (result == MessageBoxResult.OK)
            {
                this.statusTab = Tab.BUSQUEDA;
                listaAlmacenes = MS_TiendaService.listaTiendas;
            }
        }


        #endregion

        /**************************************************/
        void buscarJefe(object var)
        {
            if (dniJefe.Trim().Length > 0)
            {                
                Empleado empleado = MS_SharedService.obtenerJefePorDNI(dniJefe);
                if (empleado != null)
                {                                                       
                    almacen.Empleado = empleado;
                }
                else
                {
                    MessageBox.Show("El DNI ingresado no le pertenece a un Jefe");
                    almacen.Empleado = null;
                }
            }
            else
            {
                MessageBox.Show("Debe ingresar el DNI de algún Jefe");                
            }
        }
        /**************************************************/
        private Tienda _selectedTienda;
        public Tienda selectedTienda
        {
            get
            {                   
                _selectedTienda = almacenes.Single(t => t.id==this.almacen.id_abastecedor);
                return _selectedTienda;
            }
            set
            {
                _selectedTienda = value;
                NotifyPropertyChanged("selectedTienda");
                NotifyPropertyChanged("almacenes");
                this.almacen.id_abastecedor = _selectedTienda.id;
            }
        }
        /**************************************************/
        private ImageSource _tiendaImagen;
        public ImageSource tiendaImagen
        {
            get
            {
                try
                {
                    if (this.almacen.foto != null)
                    {
                        MemoryStream strm = new MemoryStream();
                        strm.Write(almacen.foto.ToArray(), 0, almacen.foto.Length);
                        strm.Position = 0;
                        System.Drawing.Image img = System.Drawing.Image.FromStream(strm);

                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        MemoryStream memoryStream = new MemoryStream();
                        img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.EndInit();

                        _tiendaImagen = bitmapImage;
                    }
                    return _tiendaImagen;
                } catch(Exception ){
                    return _tiendaImagen;
                }
             
            }
            set
            {
                _tiendaImagen = value;
                NotifyPropertyChanged("tiendaImagen");
            }
        }
        /**************************************************/
        private int _selectedTipo = -1;
        public int selectedTipo
        {
            get
            {
                try{
                    if (this.almacen.tipo == null)
                        return _selectedTipo;
                    else
                   {
                        _selectedTipo = (int)this.almacen.tipo;
                        if (_selectedTipo == 0) comboEnabledAlmacen = true; else comboEnabledAlmacen = false;                
                        return _selectedTipo;
                
                    }
                }
                catch (Exception )
                {
                    return _selectedTipo;

                }
            }
            set
            {
                _selectedTipo = value;
                if (_selectedTipo == 0)
                    comboEnabledAlmacen = true;
                else
                {
                    comboEnabledAlmacen = false;                   
                    almacen.Tienda1 = null;
                    _defaultcomboIndex = -1;
                }
                NotifyPropertyChanged("selectedTipo");                
                NotifyPropertyChanged("comboEnabledAlmacen");
                NotifyPropertyChanged("defaultcomboIndex");
                this.almacen.tipo = _selectedTipo;
            }
        }
        /**************************************************/
        private bool _comboEnabledAlmacen = false;
        public bool comboEnabledAlmacen
        {
            get
            {               
                return _comboEnabledAlmacen;          
            }
            set
            {
                _comboEnabledAlmacen = value;               
                NotifyPropertyChanged("comboEnabledAlmacen");                
            }
        }
        /**************************************************/
        private int _defaultcomboIndex = -1;
        public int defaultcomboIndex{
            get
            {
                return _defaultcomboIndex;
            }
            set
            {
                _defaultcomboIndex = value;
                NotifyPropertyChanged("defaultcomboIndex");
            }
        }
        /**************************************************/
        public bool VerificaCamposObligatorios(Tienda tienda)
        {
            if (tienda.nombre == null||(tienda.tipo == -1 || tienda.tipo == null)
                    ||tienda.direccion==null||tienda.telefono1==null|| "".Equals(tienda.direccion)
                    || "".Equals(tienda.nombre) || "".Equals(tienda.telefono1)
                    ||tienda.id_ubigeo == null || "".Equals(tienda.id_ubigeo)
                    ||tienda.nombre==null||tienda.telefono1==null||
                    (tienda.tipo == 0 && tienda.Tienda1 == null))
            {                
                MessageBox.Show("Completar todos los datos obligatorios");
                return false;
            }
            return true;
        }
        /**************************************************/
        public bool VerificaHorario(Tienda tienda)
        {            
            if ((tienda.lunesHoraInicio!=-1 || tienda.lunesHoraFin!=-1)
                && (tienda.lunesHoraInicio >= tienda.lunesHoraFin )||
                (tienda.martesHoraInicio!=-1 || tienda.martesHoraFin!=-1)
                && (tienda.martesHoraInicio >= tienda.martesHoraFin)||
                (tienda.miercolesHoraInicio!=-1 || tienda.miercolesHoraFin!=-1)
                && (tienda.miercolesHoraInicio >= tienda.miercolesHoraFin)||
                (tienda.juevesHoraInicio!=-1 || tienda.juevesHoraFin!=-1)
                && (tienda.juevesHoraInicio >= tienda.juevesHoraFin) ||
                (tienda.viernesHoraInicio!=-1 || tienda.viernesHoraFin!=-1)
                && (tienda.viernesHoraInicio >= tienda.viernesHoraFin) ||
                (tienda.sabadoHoraInicio!=-1 || tienda.sabadoHoraFin!=-1)
                && (tienda.sabadoHoraInicio >= tienda.sabadoHoraFin) ||
                (tienda.domingoHoraInicio!=-1 || tienda.domingoHoraFin!=-1)
                && (tienda.domingoHoraInicio >= tienda.domingoHoraFin))
            {
                MessageBox.Show("La hora Fin debe ser mayor a la hora Inicio");
                return false;
            }
            return true;
        }
        /**************************************************/

    }
}
