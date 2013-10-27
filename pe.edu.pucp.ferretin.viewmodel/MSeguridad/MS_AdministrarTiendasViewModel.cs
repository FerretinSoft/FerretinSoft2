using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.viewmodel.MSeguridad
{
    public class MS_AdministrarTiendasViewModel : ViewModelBase
    {
        #region Valores para el cuadro de Búsqueda
        public String searchCodTienda { get; set; }
        public String searchNombre { get; set; }
        private int _searchEstado;
        public int searchEstado { get { return _searchEstado; } set { _searchEstado = value; NotifyPropertyChanged("searchEstado"); } }
        private int  _searchProvincia { get; set; }
        public int searchProvincia
        {
            get
            {
                return _searchProvincia > 0 ? _searchProvincia : 0;
            }
            set
            {
                _searchProvincia = value;
                NotifyPropertyChanged("searchProvincia");
                NotifyPropertyChanged("provincias");
                NotifyPropertyChanged("distritos");
            }
        }
        private int _searchTipo;
        public int searchTipo { get { return _searchTipo; } set { _searchTipo = value; NotifyPropertyChanged("searchTipo"); } }
        public int _searchDistrito;
        public int searchDistrito
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
                    case (int)tabs.BUSQUEDA: detallesTabHeader = "Agregar"; tienda = new Almacen(); break;//Si es agregar, creo un nuevo objeto Tienda
                    case (int)tabs.AGREGAR: detallesTabHeader = "Agregar"; tienda = new Almacen(); break;//Si es agregar, creo un nuevo objeto Tienda
                    case (int)tabs.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case (int)tabs.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; tienda = new Almacen(); break;//Si es agregar, creo un nuevo objeto Tienda
                }
                //Cuando se cambia el status, tambien se tiene que cambiar el currentIndex del tab
                //currentIndexTab = _statusTab == 0 ? 0 : 1;
                NotifyPropertyChanged("statusTab");
            }
        }

        //Usado para mover los tabs de acuerdo a las acciones realizadas
        public int currentIndexTab
        {
            get { return _statusTab == 0 ? 0 : 1; }
            set { statusTab = (int)tabs.AGREGAR; NotifyPropertyChanged("currentIndexTab"); }
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

        private Almacen _tienda = new Almacen();
        
        public Almacen tienda
        {
            get
            {
                return _tienda;
            }
            set
            {
                _tienda = value;
                NotifyPropertyChanged("tienda");
            }
        }

        int selectedDepartamento = 14; //index Lima default
       


    }
}
