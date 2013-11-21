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
        /***********************************/
        #region Valores para el cuadro de Búsqueda
        private int _searchModulo = 0;
        public int searchModulo { get { return _searchModulo; } set { _searchModulo = value; NotifyPropertyChanged("searchModulo"); } }
        
        private string _searchDescripcion = null;
        public string searchDescripcion { get { return _searchDescripcion; } set { _searchDescripcion = value; NotifyPropertyChanged("searchPerfil"); } }
        #endregion
        /***********************************/
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
                switch (value)
                {
                    case tabs.BUSQUEDA: detallesTabHeader = "Agregar";
                        {
                            break;
                        }
                    case tabs.AGREGAR:
                        {
                            
                            detallesTabHeader = "Agregar";
                            if (perfil == null || perfil.id>0)
                            {
                                perfil = new Perfil();
                                perfil.nombre = "";
                                PerfilMenu perfilMenu = new PerfilMenu();
                                copiarPerfil(perfilMenu, menuPadre, perfil);
                                perfil.estado = 2;
                                perfil.PerfilMenu.Add(perfilMenu);
                            }
                            NotifyPropertyChanged("perfil");
                            break;
                        };
                    case tabs.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case tabs.DETALLES: detallesTabHeader = "Detalles"; break;
                }
                NotifyPropertyChanged("statusTab");
                NotifyPropertyChanged("currentIndexTab");
            }
        }

        private void copiarPerfil(PerfilMenu perfilMenu, Menu menu,Perfil perfil)
        {
            //perfilMenu.Perfil = perfil;
            perfilMenu.Menu = menu;
            perfilMenu.estado = false;
            perfilMenu.Perfil = perfil;

            for (int i = 0; i < menu.Menu2.Count;i++ )
            {
                PerfilMenu hijoperfilMenu = new PerfilMenu();
                perfilMenu.PerfilMenu2.Add(hijoperfilMenu);
                copiarPerfil(hijoperfilMenu, menu.Menu2.ElementAt(i),perfil);
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

        #endregion
        /***********************************/
        #region Lista de Perfiles y Edición de Perfiles

        private IEnumerable<Menu> _menus;
        public IEnumerable<Menu> menus
        {
            get
            {
                if (_menus == null)
                {
                    _menus = MS_PerfilService.db.Menu;
                }
                return _menus;
            }
            set
            {
                _menus = value;
                NotifyPropertyChanged("menus");
            }
        }

        public Perfil _perfil = null;
        public Perfil perfil
        {
            get
            {
                return _perfil;
            }
            set
            {
                _perfil = value;                
                NotifyPropertyChanged("perfil");
            }
        }

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
        /***********************************/
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
        /***********************************/
        #region Comandos
        /**************************************************/
        public void viewEditPerfil(Object id)
        {
            try
            {
                perfil = listaPerfiles.Single(p => p.id == (short)id);
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
            listaPerfiles = null;
            if (perfil.nombre.Length <= 0)
            {
                MessageBox.Show("Ingrese datos en los campos necesarios, por favor");
                return;
            }

            /*Para actualizar un perfil existente*/
            if (perfil.id > 0)//Si existe
            {
                ComunService.idVentana(11);
                if (!MS_PerfilService.enviarCambios())
                {
                    MessageBox.Show("No se pudo actualizar el perfil");
                }
                else
                {
                    MessageBox.Show("El perfil fue actualizado con éxito");
                }
            }
            /*Para agregar un perfil nuevo*/
            else
            {
                ComunService.idVentana(10);
                                   
                if (!MS_PerfilService.insertarPerfil(perfil))
                {
                    MessageBox.Show("No se pudo agregar el nuevo perfil");
                }
                else
                {
                    MessageBox.Show("El perfil fue agregado con éxito");
                    this.statusTab = tabs.BUSQUEDA;
                    listaPerfiles = MS_PerfilService.listaPerfiles;                        
                }               
            }            
        }
        /**************************************************/
        public void cancelPerfil(Object obj)
        {
             MessageBoxResult result =MessageBox.Show("Al salir, perderá todos los datos ingresados. ¿Desea continuar?",
            "ATENCIÓN", MessageBoxButton.OKCancel, MessageBoxImage.Warning, MessageBoxResult.Cancel);
             if (result == MessageBoxResult.OK)
             {
                 this.statusTab = tabs.BUSQUEDA;
                 listaPerfiles = MS_PerfilService.listaPerfiles;
             }
        }
        /**************************************************/
        #endregion
        /***********************************/
        public Menu CategoriaSeleccionada { get; set; }
        /***********************************/
        public Menu menuPadre
        {
            get
            {
                //Devolver la categoría padre
                return MS_PerfilService.menuPadre;
            }
        }        
    }
}
