using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;

namespace pe.edu.pucp.ferretin.viewmodel.MSeguridad
{
    public class MS_AdministrarPerfilesViewModel : ViewModelBase
    {
        #region Valores para el cuadro de Búsqueda
        private int _searchModulo = 0;
        public int searchModulo { get { return _searchModulo; } set { _searchModulo = value; NotifyPropertyChanged("searchModulo"); } }
        
        private string _searchDescripcion = "";
        public string searchDescripcion { get { return _searchDescripcion; } set { _searchDescripcion = value; NotifyPropertyChanged("searchPerfil"); } }
        #endregion

        public Menu CategoriaSeleccionada { get; set; }

        private Menu _menuPadre;
        public Menu menuPadre
        {
            get
            {
                //Devolver la categoría padre
                _menuPadre = MS_PerfilService.menuPadre();
                return _menuPadre;
            }
            set
            {
                _menuPadre = value;                
            }            
            
        }

        #region Manejo de los Tabs
        /************************************************/
        public enum tabs
        {
            //Pestañas virtuales:
            //0       1        2          3
            BUSQUEDA, AGREGAR, MODIFICAR, DETALLES
        }
        /************************************************/
        private tabs _statusTab = tabs.BUSQUEDA; //pestaña default 
        public tabs statusTab
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
                switch (value)
                {
                    case tabs.BUSQUEDA: detallesTabHeader = "Agregar"; perfil = new Perfil(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case tabs.AGREGAR: detallesTabHeader = "Agregar"; perfil = new Perfil(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case tabs.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case tabs.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; perfil = new Perfil(); break;//Si es agregar, creo un nuevo objeto Cliente
                }
                //Cuando se cambia el status, tambien se tiene que cambiar el currentIndex del tab
                //currentIndexTab = _statusTab == 0 ? 0 : 1;
                NotifyPropertyChanged("statusTab");
                //Cuando se cambia el status, tambien se tiene que actualizar el currentIndex del tab
                NotifyPropertyChanged("currentIndexTab"); //Hace que cambie el tab automaticamente
            }
        }
        /************************************************/
        //Usado para mover los tabs de acuerdo a las acciones realizadas
        public int currentIndexTab
        {
            get { return _statusTab == tabs.BUSQUEDA ? 0 : 1; }
            set { statusTab = value == 0 ? tabs.BUSQUEDA : tabs.AGREGAR; }
        }
        /************************************************/
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
        /************************************************/
        #endregion

        #region Lista de Perfiles y Edición de Perfiles
        /************************************************/

        private IEnumerable<Menu> _menus;
        public IEnumerable<Menu> menus
        {
            get
            {
                if (_menus == null)
                {
                    _menus = MS_PerfilService.db.Menu.ToList();
                }
                return _menus;
            }
            set
            {
                _menus = value;                
            }
        }

        private Perfil _perfil = new Perfil();
        public Perfil perfil
        {
            get
            {
                foreach (var perfilMenu in _perfil.PerfilMenu)
                {
                    for (int i=0;i<menus.Count();i++)
                    {

                        if (perfilMenu.Menu == menus.ElementAt(i) )
                        {
                            if (perfilMenu.estado.Value)
                            {
                                menus.ElementAt(i).isChecked = true;
                            }else
                            {
                                menus.ElementAt(i).isChecked = false;
                            }
                        }
                        
                    }
                }
                return _perfil;
            }
            set
            {
                _perfil = value;
                NotifyPropertyChanged("menuPadre");
                NotifyPropertyChanged("perfil");
            }
        }
        /************************************************/
        public IEnumerable<Perfil> perfiles
        {
            get
            {
                //Creo una nueva secuencia
                var sequence = Enumerable.Empty<Perfil>();
                //Primero agrego un item de Todos para que salga al inicio
                //Pongo el ID en 0 para que al buscar, no filtre nada cuando se selecciona todos
                IEnumerable<Perfil> items = new Perfil[] { new Perfil { id = 0, nombre = "Todos" } };
                //Luego concateno el itemcon los elementos del combobox
                return items.Concat(MS_PerfilService.obtenerPerfiles());
            }
        }         
        /************************************************/
        private IEnumerable<Perfil> _listaPerfiles;
        public IEnumerable<Perfil> listaPerfiles
        {
            get
            {
                _listaPerfiles = MS_PerfilService.buscar(searchModulo,searchDescripcion);
                string admin = "Administrador";
                string recHumanos = "Recursos Humanos";
                string alm = "Almacen";
                string comp = "Compras";
                string vent = "Ventas";
                string tien = "Tienda";
                string vend = "Vendedor";

                foreach (Perfil valor in _listaPerfiles) {

                    if (valor.nombre.ToLower().Contains(admin.ToLower().Trim())) valor.modulo = "Modulo Administrador";
                    if (valor.nombre.ToLower().Contains(recHumanos.ToLower().Trim())) valor.modulo = "Modulo Recursos Humanos";
                    if (valor.nombre.ToLower().Contains(comp.ToLower().Trim())) valor.modulo = "Modulo Compras";
                    if (valor.nombre.ToLower().Contains(alm.ToLower().Trim())) valor.modulo = "Modulo Almacen";
                    if (valor.nombre.ToLower().Contains(vent.ToLower().Trim())
                        || valor.nombre.ToLower().Contains(tien.ToLower().Trim()) 
                        || valor.nombre.ToLower().Contains(vend.ToLower().Trim())) valor.modulo = "Modulo Ventas";         
                }
                return _listaPerfiles;
            }            
            set
            {
                _listaPerfiles = value;
                NotifyPropertyChanged("listaPerfiles");
            }
        }    
        /**************************************************/
        #endregion

        #region RalayCommand
        /**************************************************/
        RelayCommand _actualizarListaPerfilesCommand;
        public ICommand actualizarListaPerfilesCommand
        {
            get
            {
                if (_actualizarListaPerfilesCommand == null)
                {
                    _actualizarListaPerfilesCommand = new RelayCommand(param => NotifyPropertyChanged("listaPerfiles"));
                }
                return _actualizarListaPerfilesCommand;
            }            
        }
        /**************************************************/
        RelayCommand _agregarPerfilCommand;
        public ICommand agregarPerfilCommand
        {
            get
            {
                if (_agregarPerfilCommand == null)
                {
                    _agregarPerfilCommand = new RelayCommand(p => statusTab = tabs.AGREGAR);
                }
                return _agregarPerfilCommand;
            }
        }
        /**************************************************/
        RelayCommand _viewEditPerfilCommand;
        public ICommand viewEditPerfilCommand
        {
            get
            {
                if (_viewEditPerfilCommand == null)
                {
                    _viewEditPerfilCommand = new RelayCommand(viewEditPerfil);
                }
                return _viewEditPerfilCommand;
            }
        }
        /**************************************************/
        RelayCommand _savePerfilCommand;
        public ICommand savePerfilCommand
        {
            get
            {
                if (_savePerfilCommand == null)
                {
                    _savePerfilCommand = new RelayCommand(savePerfil);
                }
                return _savePerfilCommand;
            }
        }
        /**************************************************/
        RelayCommand _cancelPerfilCommand;
        public ICommand cancelPerfilCommand
        {
            get
            {
                if (_cancelPerfilCommand == null)
                {
                    _cancelPerfilCommand = new RelayCommand(cancelPerfil);
                }
                return _cancelPerfilCommand;
            }
        }
        /**************************************************/
        #endregion

        #region Comandos
        /**************************************************/
        public void viewEditPerfil(Object id)
        {
            try
            {
                //se instancia un perfil paa cargar los textbox de la ventana edit
                this.perfil = listaPerfiles.Single(perfil => perfil.id == (short)id);
                //if (this.almacen.id_ubigeo != null)
                //{
                //    selectedProvincia = this.almacen.UbigeoDistrito.UbigeoProvincia;
                //    selectedDepartamento = selectedProvincia.UbigeoDepartamento;
                //}
                this.statusTab = tabs.MODIFICAR;                
 
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }
        /**************************************************/
        public void savePerfil(Object obj)
        {
            //Actulizo los checkbox seleccionados

            foreach (var menu in menus)
            {
                
                int cantidad = perfil.PerfilMenu.Count(pm => (pm.Menu == menu));
                if (cantidad <= 0)
                {
                    perfil.PerfilMenu.Add(new PerfilMenu { Menu = menu, estado = menu.isChecked, Perfil = perfil });
                }
            }

            for (int i = 0; i < perfil.PerfilMenu.Count(); i++)
            {
                foreach (var menu in menus)
                {
                    if (perfil.PerfilMenu[i].Menu == menu)
                    {
                        if (!menu.isChecked)
                        {
                            perfil.PerfilMenu[i].estado = false;
                        }
                        else
                        {
                            perfil.PerfilMenu[i].estado = true;
                        }
                    }
                }
            }

            /*Para actualizar un usuario existente*/
            if (perfil.id > 0)//Si existe
            {
                if (!MS_PerfilService.enviarCambios())
                {
                    MessageBox.Show("No se pudo actualizar el perfil");
                }
                else
                {
                    MessageBox.Show("El perfil fue actualizado con éxito");
                }
            }
            /*Para agregar un usuario nuevo*/
            else
            {
                if (!MS_PerfilService.insertarPerfil(perfil))
                {
                    MessageBox.Show("No se pudo agregar el nuevo perfil");
                }
                else
                {
                    MessageBox.Show("El perfil fue agregado con éxito");
                }
            }
            NotifyPropertyChanged("listaPerfiles");
        }
        /**************************************************/
        public void cancelPerfil(Object obj)
        {
            this.statusTab = tabs.BUSQUEDA;
            listaPerfiles = MS_PerfilService.listaPerfiles;
        }
        /**************************************************/
        #endregion

        
    }
}
