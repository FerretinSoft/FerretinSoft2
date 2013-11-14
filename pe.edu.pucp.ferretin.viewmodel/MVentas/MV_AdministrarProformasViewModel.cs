using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MVentas;
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

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_AdministrarProformasViewModel : ViewModelBase
    {
        #region Atributos del Buscador
        public string codProformaSearch { get; set; }
        private Cliente _clienteSearch;
        public Cliente clienteSearch { get { return _clienteSearch; } set { _clienteSearch = value; NotifyPropertyChanged("clienteSearch"); } }

        private DateTime _fechaDesdeSearch = DateTime.Today.AddDays(-30);
        public DateTime fechaDesdeSearch
        {
            get
            {
                return _fechaDesdeSearch;
            }
            set
            {
                _fechaDesdeSearch = value;
                NotifyPropertyChanged("fechaDesdeSearch");
            }
        }

        private DateTime _fechaHastaSearch = DateTime.Today;
        public DateTime fechaHastaSearch
        {
            get
            {
                return _fechaHastaSearch;
            }
            set
            {
                _fechaHastaSearch = value;
                NotifyPropertyChanged("fechaHastaSearch");
            }
        }

        public Usuario usuarioSearch { get; set; }

        #endregion

        private bool _soloSeleccionarProforma = false;
        public bool soloSeleccionarProforma
        {
            get
            {
                return _soloSeleccionarProforma;
            }
            set
            {
                _soloSeleccionarProforma = value;
                NotifyPropertyChanged("soloSeleccionarProforma");
                NotifyPropertyChanged("nombreBotonGuardar");
                NotifyPropertyChanged("noSoloSeleccionarProforma");
                detallesTabHeader = value ? "Detalles" : "Agregar";
            }
        }
        public bool noSoloSeleccionarProforma
        {
            get
            {
                return !soloSeleccionarProforma;
            }
        }
        public String nombreBotonGuardar
        {
            get
            {
                return soloSeleccionarProforma ? "SELECCIONAR" : "GUARDAR";
            }
        }

        public string codProdAgregar { get; set; }
        private long? _nroDocSeleccionado = null;
        public long? nroDocSeleccionado
        {
            get
            {
                return _nroDocSeleccionado;
            }
            set
            {
                _nroDocSeleccionado = value;
                cargarCliente(null);
                NotifyPropertyChanged("nroDocSeleccionado");
            }
        }
        #region Lista Proformas y Edicion de Proforma
        private Proforma _proforma;
        public Proforma proforma
        {
            get
            {
                return _proforma;
            }
            set
            {
                _proforma = value;
                NotifyPropertyChanged("proforma");
                NotifyPropertyChanged("proformaImagen");
            }
        }

        private IEnumerable<Proforma> _listaProformas;
        public IEnumerable<Proforma> listaProformas
        {
            get
            {
                _listaProformas = MV_ProformasService.buscarProformas(codProformaSearch, usuarioSearch, clienteSearch, fechaDesdeSearch, fechaHastaSearch);
                return _listaProformas;
            }
            set
            {
                _listaProformas = value;
                NotifyPropertyChanged("listaProformas");
            }
        }
        #endregion

        public bool esAgregar
        {
            get
            {
                return statusTab == Tab.AGREGAR;
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
                if (value == Tab.DETALLES && proforma == null)
                {
                    _statusTab = Tab.BUSQUEDA;
                }
                _statusTab = value;
                //Si cambió el estado de las pestañas también cambio los Header
                //Si la pestaña es para agregar nuevo, limpio los input
                switch (_statusTab)
                {
                    case Tab.BUSQUEDA: { 
                        detallesTabHeader = soloSeleccionarProforma ? "Detalles" : "Agregar";
                        NotifyPropertyChanged("listaProformas");
                        
                        break;
                    };
                    case Tab.AGREGAR:
                        {
                            if (proforma == null || proforma.id > 0)
                            {
                                detallesTabHeader = "Agregar";
                                var miproforma = new Proforma()
                                {
                                    codigo = MV_ProformasService.newCodProforma,
                                    Usuario = usuarioLogueado,
                                    fecEmision = DateTime.Now,
                                    fecVencimiento = DateTime.Now.AddDays(5),
                                    Cliente = new Cliente(),
                                    igvActual = MS_SharedService.obtenerIGV(),
                                    igv = 0,
                                    subTotal = 0,
                                    total = 0,
                                };
                                miproforma.ProformaProducto.ListChanged += actualizarMontosProforma;
                                proforma = miproforma;
                            }
                            
                            break;
                        }
                    case Tab.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case Tab.DETALLES:
                        {

                            detallesTabHeader = "Detalles"; 
                            break;
                        }
                }
                NotifyPropertyChanged("esAgregar");
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
            }
        }

        public void actualizarMontosProforma(object sender, object e)
        {
            //elimino si algun producto tiene cantidad = 0
            foreach (var pp in proforma.ProformaProducto)
            {
                if (pp.cantidad == 0)
                {
                    proforma.ProformaProducto.Remove(pp);
                }
            }

            //Actualizo el total
            proforma.total = Decimal.Round(proforma.ProformaProducto.Sum(pp=> pp.montoParcial).Value, 2);

            
        }
        //Usado para mover los tabs de acuerdo a las acciones realizadas
        public int currentIndexTab
        {
            get
            {
                return _statusTab == Tab.BUSQUEDA ? 0 : 1;
            }
            set
            {
                if (soloSeleccionarProforma)
                {
                    statusTab = value == 0 ? Tab.BUSQUEDA : Tab.DETALLES;
                }
                else
                {
                    statusTab = value == 0 ? Tab.BUSQUEDA : Tab.AGREGAR;
                }
            }
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



        public GridLength widthClienteBar
        {
            get
            {
                return (proforma == null || proforma.Cliente == null) ? new GridLength(0) : GridLength.Auto;
            }
        }

        private ImageSource _clienteImagen;
        public ImageSource clienteImagen
        {
            get
            {
                
                if (proforma != null && proforma.Cliente != null && proforma.Cliente.imagen != null)
                {
                    MemoryStream strm = new MemoryStream();
                    strm.Write(proforma.Cliente.imagen.ToArray(), 0, proforma.Cliente.imagen.Length);
                    strm.Position = 0;
                    System.Drawing.Image img = System.Drawing.Image.FromStream(strm);

                    BitmapImage bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    MemoryStream memoryStream = new MemoryStream();
                    img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                    memoryStream.Seek(0, SeekOrigin.Begin);
                    bitmapImage.StreamSource = memoryStream;
                    bitmapImage.EndInit();

                    _clienteImagen = bitmapImage;
                }
                return _clienteImagen;
            }
            set
            {
                _clienteImagen = value;
                NotifyPropertyChanged("clienteImagen");
            }
        }

        #region RalayCommand
        RelayCommand _cargarClienteCommand;
        public ICommand cargarClienteCommand
        {
            get
            {
                if (_cargarClienteCommand == null)
                {
                    _cargarClienteCommand = new RelayCommand(cargarCliente);
                }
                return _cargarClienteCommand;
            }
        }

        RelayCommand _agregarProductoCommand;
        public ICommand agregarProductoCommand
        {
            get
            {
                if (_agregarProductoCommand == null)
                {
                    _agregarProductoCommand = new RelayCommand(agregarProducto);
                }
                return _agregarProductoCommand;
            }
        }

        RelayCommand _registrarCommand;
        public ICommand registrarCommand
        {
            get
            {
                if (_registrarCommand == null)
                {
                    _registrarCommand = new RelayCommand(registrar, canRegistrar);
                }
                return _registrarCommand;
            }
        }

        RelayCommand _actualizarListaCommand;
        public ICommand actualizarListaCommand
        {
            get
            {
                if (_actualizarListaCommand == null)
                {
                    _actualizarListaCommand = new RelayCommand(p => NotifyPropertyChanged("listaProformas"));
                }
                return _actualizarListaCommand;
            }
        }

        RelayCommand _viewDetailProformaCommand;
        public ICommand viewDetailProformaCommand
        {
            get
            {
                if (_viewDetailProformaCommand == null)
                {
                    _viewDetailProformaCommand = new RelayCommand(viewDetailProforma);
                }
                return _viewDetailProformaCommand;
            }
        }
        RelayCommand _cancelarCommand;
        public ICommand cancelarCommand
        {
            get
            {
                if (_cancelarCommand == null)
                {
                    _cancelarCommand = new RelayCommand(p=>statusTab=Tab.BUSQUEDA);
                }
                return _cancelarCommand;
            }
        }

        RelayCommand _irAgregarCommand;
        public ICommand irAgregarCommand
        {
            get
            {
                if (_irAgregarCommand == null)
                {
                    _irAgregarCommand = new RelayCommand(p => statusTab = Tab.AGREGAR,p=> !soloSeleccionarProforma);
                }
                return _irAgregarCommand;
            }
        }

        #endregion

        #region Comandos


        private void viewDetailProforma(object id)
        {
            try
            {
                this.proforma = MV_ProformasService.db.Proforma.Single(p => p.id == (int)id);

                
                this.statusTab = Tab.DETALLES;
                
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public void registrar(object param)
        {
            if (soloSeleccionarProforma)
            {

            }
            else
            {

                if (proforma.id > 0)//Si existe
                {
                   MessageBox.Show("No se puede modificar una proforma, puede en su lugar puede crear otra");
                }
                else
                {
                    ComunService.idVentana(48);
                    if (!MV_ProformasService.insertarProforma(proforma))
                    {
                        MessageBox.Show("No se pudo agregar la nueva Proforma");
                    }
                    else
                    {
                        MessageBox.Show("La proforma fue agregada con éxito");
                        statusTab = Tab.BUSQUEDA;
                    }
                }
            }
        }

        public bool canRegistrar(object param)
        {
            if (soloSeleccionarProforma)
            {
                return true;
            }
            return esAgregar && proforma!=null && proforma.Cliente!=null && proforma.ProformaProducto.Count>0
                ;
        }

        public void cargarCliente(Object id)
        {
            Cliente buscado = null;
            try
            {
                buscado = MV_ClienteService.obtenerClienteByNroDoc(nroDocSeleccionado);
            }
            catch { }

            if (buscado == null)
            {
                MessageBox.Show("No se encontro ningún Cliente con el número de documento proporcionado", "No se encontro", MessageBoxButton.OK, MessageBoxImage.Question);
            }
            proforma.Cliente = buscado;
            NotifyPropertyChanged("clienteImagen");
            NotifyPropertyChanged("widthClienteBar");
        }

        public void agregarProducto(Object id)
        {
            if (codProdAgregar != null && codProdAgregar.Length > 0)
            {
                Producto producto = null;
                try
                {
                    producto = MA_SharedService.obtenerProductoxCodigo(codProdAgregar);
                }
                catch { }

                if (producto != null)
                {
                    if (proforma.ProformaProducto.Count(vp => vp.Producto.id == producto.id) == 1)
                    {
                        proforma.ProformaProducto.Single(vp => vp.Producto.id == producto.id).cantidad++;
                    }
                    else
                    {
                        ProformaProducto proformaProducto = new ProformaProducto();
                        proformaProducto.PromocionActual = MV_PromocionService.ultimaPromocionPorProducto(producto, MS_SharedService.usuarioL.Empleado.tiendaActual);
                        proformaProducto.tipoCambio = (decimal)MS_SharedService.obtenerTipodeCambio();
                        proformaProducto.Proforma = proforma;
                        proformaProducto.Producto = producto;
                        proformaProducto.cantidad = 1;
                        
                        proformaProducto.PropertyChanged += actualizarMontosProforma;
                        
                        proforma.ProformaProducto.Add(proformaProducto);
                        proforma.ProformaProducto.ListChanged += actualizarMontosProforma;

                    }
                    NotifyPropertyChanged("proforma");
                }
            }
        }

        

        #endregion
    }
}
