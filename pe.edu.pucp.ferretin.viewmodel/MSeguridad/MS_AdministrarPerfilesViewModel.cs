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
        private int _searchPerfil = 0;
        public int searchPerfil { get { return _searchPerfil; } set { _searchPerfil = value; NotifyPropertyChanged("searchPerfil"); } }

        private int _searchModulo = 0;
        public int searchModulo { get { return _searchModulo; } set { _searchModulo = value; NotifyPropertyChanged("searchModulo"); } }
        #endregion


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
        private Perfil _perfil = new Perfil();
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
        /************************************************/
        /************************************************
        public IEnumerable<Perfil> perfiles
        {
            get
            {
                return MS_UsuarioService.obtenerPerfiles();
            }
         }
         * lo mismo pero para modulos porque debemos traer la lista de modulos
         * 
         * /

        /************************************************/
        private IEnumerable<Perfil> _listaPerfiles;
        public IEnumerable<Perfil> listaPerfiles
        {
            get
            {
                _listaPerfiles = MS_PerfilService.buscar(searchPerfil, searchModulo);
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
