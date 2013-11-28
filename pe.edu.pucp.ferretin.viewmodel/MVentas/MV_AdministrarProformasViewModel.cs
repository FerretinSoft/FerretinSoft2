using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Data.Linq;
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
        public string _vendedorSearchdni;
        public string vendedorSearchdni { get { return _vendedorSearchdni; } set { _vendedorSearchdni = value; NotifyPropertyChanged("vendedorSearchdni"); } }
        private Empleado _vendedorSearch;
        public Empleado vendedorSearch { get { return _vendedorSearch; } set { _vendedorSearch = value; vendedorSearchdni = value!=null?value.dni:""; NotifyPropertyChanged("vendedorSearch"); } }
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
                        if(!soloSeleccionarProforma)
                            ComunService.Clean();
                        detallesTabHeader = soloSeleccionarProforma ? "Detalles" : "Agregar";
                        NotifyPropertyChanged("listaProformas");
                        break;
                    };
                    case Tab.AGREGAR:
                        {
                            
                                detallesTabHeader = "Agregar";
                                var miproforma = new Proforma()
                                {
                                    codigo = "Auto",
                                    Usuario = usuarioLogueado,
                                    Tienda = usuarioLogueado.Empleado.tiendaActual,
                                    fecEmision = DateTime.Now,
                                    fecVencimiento = DateTime.Now.AddDays(3),
                                    igvActual = MS_SharedService.obtenerIGV(),
                                    tipoCambio = (decimal)MS_SharedService.obtenerTipodeCambio(),
                                    igv = 0,
                                    subTotal = 0,
                                    total = 0,
                                    finalizado = false
                                };
                                miproforma.ProformaProducto.ListChanged += actualizarMontosProforma;
                                proforma = miproforma;
                            
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
            
            //Actualizo el total
            proforma.total = Decimal.Round(proforma.ProformaProducto.Sum(pp=> pp.montoParcial).Value, 2);

            NotifyPropertyChanged("proforma");
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
                    _cancelarCommand = new RelayCommand(p =>
                        {
                            if (!esAgregar)
                            {
                                statusTab = Tab.BUSQUEDA;
                            }
                            else
                            {
                                var result = MessageBox.Show("Al salir, perderá todos los datos ingresados. ¿Desea continuar?",
                                                                "ATENCIÓN", MessageBoxButton.OKCancel, MessageBoxImage.Warning);
                                if (result == MessageBoxResult.OK)//Borro si hubo algun cambio que no fue guardado
                                {
                                    statusTab = Tab.BUSQUEDA;
                                }
                            }
                        });
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

        RelayCommand _seleccionarEmpleadoSearchCommand;
        public ICommand seleccionarEmpleadoSearchCommand
        {
            get
            {
                if (_seleccionarEmpleadoSearchCommand == null)
                {
                    _seleccionarEmpleadoSearchCommand = new RelayCommand(seleccionarEmpleadoSearch);
                }
                return _seleccionarEmpleadoSearchCommand;
            }
        }

        #endregion

        #region Comandos


        private void seleccionarEmpleadoSearch(object obj)
        {
            this.vendedorSearch = MR_SharedService.obtenerEmpleadoPorDNI(vendedorSearchdni);
        }
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
                String result = String.Empty;
                if (proforma != null)
                {
                    if (proforma.Cliente==null || proforma.Cliente.id<=0)
                    {
                        result = "Asigne un cliente a la proforma";
                    }
                    else
                    {
                        if (proforma.fecVencimiento != null)
                        {
                            if (DateTime.Compare(proforma.fecVencimiento.Value, DateTime.Now) < 0)
                            {
                                result = "La fecha desde debe ser mayor a la fecha actual";
                            }
                            else
                            {
                                if (proforma.ProformaProducto.Count <= 0)
                                {
                                    result = "Debe seleccionar al menos un Producto";
                                }
                            }
                        }
                        else
                        {
                            result = "Debe seleccionar una fecha de vencimiento de la proforma";


                        }
                    }
                }
                if (result.Length > 0)
                {
                    MessageBox.Show(result, "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    return;
                }

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
            proforma.destinatario = buscado.email;
            NotifyPropertyChanged("clienteImagen");
            NotifyPropertyChanged("widthClienteBar");
        }

        public void agregarProducto(Object id)
        {
            if (codProdAgregar != null && codProdAgregar.Length > 0)
            {
                Producto producto = null;
                ProductoAlmacen productoAlmacen = null;
                try
                {
                    producto = MA_SharedService.obtenerProductoxCodigo(codProdAgregar);
                    productoAlmacen = MA_ProductoAlmacenService.ObtenerProductoAlmacenPorTiendaProducto(proforma.Tienda, producto);
                }
                catch { }
                
                //Validar si lo encuentra y si tiene como estado activo, el producto y el producto de un almacen
                if (productoAlmacen != null && producto != null && productoAlmacen.estado > 0)
                {

                    var stockDisponible = 0;

                    try
                    {
                        stockDisponible = (int)productoAlmacen.stock;
                    }
                    catch { }

                    if (stockDisponible <= 0)
                    {
                        MessageBox.Show("No se cuenta con Stock de este producto:\n" + producto.nombre.ToUpper(), "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }
                    if (!producto.precioLista.HasValue || producto.precioLista <= 0)
                    {
                        MessageBox.Show("El producto no tiene un precio asignado:\n" + producto.nombre.ToUpper(), "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        return;
                    }

                    if (proforma.ProformaProducto.Count(vp => vp.producto_id.Equals(producto.id)) == 1)
                    {
                        var prod = proforma.ProformaProducto.Single(vp => vp.producto_id.Equals(producto.id));
                        
                        if (prod.cantidad + 1 > prod.stockDisponible)
                        {
                            MessageBox.Show("No se tiene más stock de este producto:\n" + producto.nombre.ToUpper(), "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }
                        else
                        {
                            prod.cantidad++;
                        }
                    }
                    else
                    {
                        if (stockDisponible > 0)
                        {
                            ProformaProducto proformaProducto = new ProformaProducto();
                            proformaProducto.PromocionProducto = MV_PromocionService.ultimaPromocionPorProducto(productoAlmacen);
                            proformaProducto.tipoCambio = proforma.tipoCambio.Value;
                            proformaProducto.Producto = producto;
                            proformaProducto.puntosGanar = (producto.ganarPuntos ?? 0);
                            proformaProducto.puntosGanado = (producto.ganarPuntos ?? 0)*1;
                            proformaProducto.preciounitario = producto.precioLista;
                            proformaProducto.moneda = producto.moneda;
                            proformaProducto.precioPuntos = producto.precioPuntos??0;
                            proformaProducto.precioPuntosParcial = (producto.precioPuntos ?? 0)*1;
                            proformaProducto.Proforma = proforma;
                            proformaProducto.cantidad = 1;
                            proformaProducto.stockDisponible = stockDisponible;
                            proformaProducto.stockRestante = stockDisponible - 1;

                            proformaProducto.PropertyChanged += actualizarMontosProforma;

                            proforma.ProformaProducto.Add(proformaProducto);

                            actualizarMontosProforma(null, null);
                        }
                        else
                        {
                            MessageBox.Show("No hay stock de este producto:\n" + producto.nombre.ToUpper(), "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        }

                    }

                    NotifyPropertyChanged("venta");
                }
                else
                {
                    if (productoAlmacen == null || producto == null)
                        MessageBox.Show("Este producto no existe.", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    else
                        MessageBox.Show("Este producto no esta disponible para su venta.", "Mensaje", MessageBoxButton.OK, MessageBoxImage.Exclamation);
                }
            }
        }

        

        #endregion



        
    }
}
