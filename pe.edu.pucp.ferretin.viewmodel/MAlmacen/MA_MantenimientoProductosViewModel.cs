using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.model;
using System.Windows.Input;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Controls;
using System.IO;
using System.Windows;
using Microsoft.Win32;



namespace pe.edu.pucp.ferretin.viewmodel.MAlmacen
{
    public class MA_MantenimientoProductosViewModel : ViewModelBase
    {
        
        public IEnumerable<Categoria> listaCategoriasAsignadas { get; set; }

        public String searchNombre { get; set; }
        public Categoria searchIdCategoria { get; set; }

        public Image _imgProd { get; set; }
        public Image imgProd
        {
            set
            {
                _imgProd = value;
                NotifyPropertyChanged("imgProd");
            }

            get
            {
                return _imgProd;
            }
        }


        private IEnumerable<Categoria> _categoriaPrincipalInit { get; set; }


        private IEnumerable<Categoria> _categoriaPrincipal;
        public IEnumerable<Categoria> categoriaPrincipal
        {
            get
            {
                //Devolver la categoría padre
                //_categoriaPrincipal = MA_CategoriaService.categorias.Where(c => c.id_padre == null);
                _categoriaPrincipal = MA_CategoriaService.obtenerCategoriasPadres();
                
                return _categoriaPrincipal;
            }
            set
            {
                _categoriaPrincipal = value;
                NotifyPropertyChanged("categoriaPrincipal");
            }
        }
        //private Categoria _CategoriaSeleccionada;
        //public Categoria CategoriaSeleccionada
        //{

        //    get
        //    {
        //        return _CategoriaSeleccionada;
        //    }
        //    set
        //    {
        //        _CategoriaSeleccionada = value;
        //        //Actualizo el combobox de categorias padre
        //        categoriasPadre = MA_CategoriaService.categorias.Where(c => c.nivel == _CategoriaSeleccionada.Categoria1.nivel);
        //        NotifyPropertyChanged("CategoriaSeleccionada");
        //    }
        //}




        public Boolean _radioYes;
        public Boolean radioYes
        {
            get
            {
                return this._radioYes;

            }

            set
            {
                this._radioYes=value;
                this.OnPropertyChanged("radioYes");

            }
        }

        public Boolean _radioNo;
        public Boolean radioNo
        {
            get
            {
                return this._radioNo;

            }

            set
            {
                this._radioNo=value;
                this.OnPropertyChanged("radioNo");

            }
        }



        public bool chkActivo { get; set; }
        public bool chkInactivo { get; set; }


        public Tienda _selectedTienda;
        public Tienda selectedTienda
        {
            set
            {   
                _selectedTienda = value;
                if (_selectedTienda != null)
                {
                    prodAlm = MA_ProductoService.obtenerProdxTienda(producto.id, _selectedTienda.id);
                    if (prodAlm != null)
                    {
                        radioYes = false;
                        radioNo = false;
                        if (prodAlm.estado == 1)
                            radioYes = true;
                        else
                            radioNo = true;
                    }
                }
            }

            get
            {
                return _selectedTienda;
            }



        }


        public IEnumerable<Tienda> _listaTiendas;
        public IEnumerable<Tienda> listaTiendas
        {
            get
            {
                _listaTiendas = MS_TiendaService.listaTiendas;
                return _listaTiendas;
            }
            set
            {
                _listaTiendas = value;
                NotifyPropertyChanged("listaTiendas");
            }
        }

        public IEnumerable<Material> _listaMatSec;
        public IEnumerable<Material> listaMatSec
        {
            get
            {
                _listaMatSec = MA_UnidadMedidaServiceMateriales.obtenerMaterialesPrimario();
                return _listaMatSec;
            }
            set
            {
                _listaMatSec = value;
                NotifyPropertyChanged("listaMatSec");
            }
        }


        public IEnumerable<Material> _listaMatBase;
        public IEnumerable<Material> listaMatBase
        {
            get
            {
                _listaMatBase = MA_UnidadMedidaServiceMateriales.obtenerMaterialesPrimario();
                return _listaMatBase;
            }
            set
            {
                _listaMatBase = value;
                NotifyPropertyChanged("listaMatBase");
            }
        }

        public IEnumerable<UnidadMedida> _listaUMed;
        public IEnumerable<UnidadMedida> listaUMed
        {
            get
            {
                _listaUMed = MA_UnidadMedidaServiceMateriales.obtenerUnidadesMedida();
                return _listaUMed;            }
            set
            {
                _listaUMed = value;
                NotifyPropertyChanged("listaUMed");
            }
        }



        public IEnumerable<Categoria> _listaCategorias;
        public IEnumerable<Categoria> listaCategorias
        {
            get
            {
                _listaCategorias = MA_CategoriaService.categorias;
                return _listaCategorias;
            }
            set
            {
                _listaCategorias=value;
                NotifyPropertyChanged("listaCategorias");
            }
        }



        public IEnumerable<Producto> _listaProductos;
        public IEnumerable<Producto> listaProductos {
            
            get
            {
                searchNombre = searchNombre == null ? "" : searchNombre;
                _listaProductos=MA_ProductoService.obtenerProductosPorNombre(searchNombre,chkActivo,chkInactivo,searchIdCategoria);
                
                return _listaProductos;
            }
            
            set
            {
                _listaProductos = value;
                NotifyPropertyChanged("listaProductos");
            }
        }


        private Producto _prod;
        public Producto producto
        {
            get
            {
                return _prod;
            }
            set
            {
                _prod = value;
                NotifyPropertyChanged("producto");
                NotifyPropertyChanged("productoImagen");
            }
        }

        private ProductoAlmacen _prodAlm;
        public ProductoAlmacen prodAlm
        {
            get
            {
                return _prodAlm;
            }
            set
            {
                _prodAlm = value;
                NotifyPropertyChanged("prodAlm");
            }
        }

        private String _detallesTabHeader = "Agregar Producto"; //Default
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
                NotifyPropertyChanged("productoImagen");
            }
        }


        #region manejo de tabs
        public enum tabs
        {
            //Pestañas virtuales:
            //0       1        2          3
            BUSQUEDA, AGREGAR, MODIFICAR, DETALLES
        }
        private int _statusTab = (int)tabs.BUSQUEDA; //pestaña default 
        public int statusTab
        {
            get
            {
                return _statusTab;
            }
            set
            {
                _statusTab = value == 0 ? 0 : 1;
                //Si cambió el estado de las pestañas también cambio los Header
                //Si la pestaña es para agregar nuevo, limpio los input
                switch (value)
                {
                    case (int)tabs.BUSQUEDA: detallesTabHeader = "Agregar Producto"; producto = new Producto(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case (int)tabs.AGREGAR: detallesTabHeader = "Agregar Producto"; productoImagen = null; producto = new Producto(); prodAlm = new ProductoAlmacen(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case (int)tabs.MODIFICAR: detallesTabHeader = "Edición de Producto"; break;

                    //case (int)tabs.DETALLES: detallesTabHeader = "Detalles"; break;
                    //default: detallesTabHeader = "Agregar"; cliente = new Cliente(); break;//Si es agregar, creo un nuevo objeto Cliente
                }
                //Cuando se cambia el status, tambien se tiene que cambiar el currentIndex del tab
                //currentIndexTab = _statusTab == 0 ? 0 : 1;
                NotifyPropertyChanged("statusTab");
                NotifyPropertyChanged("productoImagen");
            }
        }
        #endregion

        #region RelayCommand


        RelayCommand _checkTreeCommand;
        public ICommand checkTreeCommand
        {
            get
            {
                if (_checkTreeCommand == null)
                {
                    _checkTreeCommand = new RelayCommand(checkTree);
                }
                return _checkTreeCommand;
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

        RelayCommand _nuevoProductoCommand;
        public ICommand nuevoProductoCommand
        {
            get
            {
                if (_nuevoProductoCommand == null)
                {
                    _nuevoProductoCommand = new RelayCommand(nuevoProductoBtn);
                }
                return _nuevoProductoCommand;
            }
        }

        RelayCommand _buscarClienteCommand;
        public ICommand buscarClienteCommand
        {
            get
            {
                if (_buscarClienteCommand == null)
                {
                    _buscarClienteCommand = new RelayCommand(param => NotifyPropertyChanged("listaProductos"));
                }
                return _buscarClienteCommand;
            }
        }


        RelayCommand _editarCommand;
        public ICommand editarCommand
        {
            get
            {
                if (_editarCommand == null)
                {
                    _editarCommand = new RelayCommand(editarProductoBtn);
                }
                return _editarCommand;
            }
        }


        #endregion

        //Utilizado en arbol

        private void cleanCategoriasTree()
        {
            foreach (Categoria c in categoriaPrincipal)
            {
                c.isChecked = false;
                foreach (Categoria c2 in c.Categoria2)
                    c2.isChecked = false;
            }

            NotifyPropertyChanged("categoriaPrincipal");
        }

        private void obtenerCategoriasDeProducto()
        {   
            IEnumerable<Categoria> catxProd = MA_CategoriaService.obtenerCategoriasxProducto(producto.id);
            //IEnumerable<Categoria> res=_categoriaPrincipal.Intersect(catxProd);

            foreach (Categoria c in _categoriaPrincipal)
            {
                foreach (Categoria c2 in c.Categoria2)
                    c2.isChecked = false;

                c.isChecked = false;

            }

            foreach (Categoria c in catxProd)
            {
                if (c.id_padre == null)
                    _categoriaPrincipal.Single(categoria => categoria.id == c.id).isChecked = true;

                else
            {
                    Categoria cPadre = _categoriaPrincipal.Single(categoria => categoria.id == c.id_padre);
                    cPadre.Categoria2.Single(categoria => categoria.id == c.id).isChecked=true;
                }
            }

            _categoriaPrincipalInit = _categoriaPrincipal;
            NotifyPropertyChanged("categoriaPrincipal");
            
        }


        private void nuevoProductoBtn(Object obj)
        {
            //productoTabControl.SelectedIndex = 1;
            //txtCodigo.IsEnabled = true;

        }

        private void editarProductoBtn(Object codigo)
        {
            try
            {
                this.producto = listaProductos.Single(producto => producto.codigo == (String)codigo);
                this.prodAlm = new ProductoAlmacen();
                this.statusTab = (int)tabs.MODIFICAR;
                obtenerCategoriasDeProducto();
            }
            catch (Exception e)
            {
                //MessageBox.Show(e.Message);
            }
        }


        private void checkTree(Object id)
        {
            Console.WriteLine(id);
        }


        public void guardarCategoriasProducto()
        {
            List<ProductoCategoria> prodCat=new List<ProductoCategoria>();

            foreach (Categoria c in categoriaPrincipal)
        {
                if (c.isChecked == true)
            {
                    ProductoCategoria p = new ProductoCategoria();
                    p.id_producto = producto.id;
                    p.id_categoria = c.id;
                    prodCat.Add(p);
                }

                foreach (Categoria c2 in c.Categoria2)
                {
                    if (c2.isChecked == true)
                {
                        ProductoCategoria p=new ProductoCategoria();
                        p.id_producto = producto.id;
                        p.id_categoria = c2.id;
                        prodCat.Add(p);
                    }
                }
                }

            MA_CategoriaService.agregarCategoriaProductos(prodCat);
            IEnumerable<Categoria> p2= MA_CategoriaService.obtenerCategoriasxProducto(producto.id);
            String cad = "";
            foreach (Categoria pc in p2)
                {
                if (cad != "") cad += ", ";
                cad += pc.nombre;
                }

            listaProductos.Single(prod => prod.id == producto.id).cadenaCategoria = cad;
            NotifyPropertyChanged("listaProductos");
                
            
        }


        private void tabBúsqueda_Click(object sender, MouseButtonEventArgs e)
        {
            //enable_disable_campos(true);
        }




        #region comandos para obtener foto y cargar foto

         //obtener  foto
        private ImageSource _productoImagen;
        public ImageSource productoImagen
        {
            get
            {
                if (producto != null)
                {
                    if (this.producto.imagen != null)
                    {
                        MemoryStream strm = new MemoryStream();
                        strm.Write(producto.imagen.ToArray(), 0, producto.imagen.Length);
                        strm.Position = 0;
                        System.Drawing.Image img = System.Drawing.Image.FromStream(strm);

                        BitmapImage bitmapImage = new BitmapImage();
                        bitmapImage.BeginInit();
                        MemoryStream memoryStream = new MemoryStream();
                        img.Save(memoryStream, System.Drawing.Imaging.ImageFormat.Bmp);
                        memoryStream.Seek(0, SeekOrigin.Begin);
                        bitmapImage.StreamSource = memoryStream;
                        bitmapImage.EndInit();

                        _productoImagen = bitmapImage;
                    }
                }
                return _productoImagen;
            }
            set
            {
                _productoImagen = value;
                NotifyPropertyChanged("productoImagen");
            }
        }


        //cargar  foto
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
                producto.imagen = file_binary;
                NotifyPropertyChanged("productoImagen");
            }
        }


        #endregion
    }
}
