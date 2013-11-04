using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.viewmodel.MSeguridad
{
    public class MS_AdministrarArbolPerfilesViewModel : ViewModelBase
    {

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
                    case tabs.BUSQUEDA: detallesTabHeader = "Agregar"; /*perfil = new Perfil();*/ break;//Si es agregar, creo un nuevo objeto Cliente
                    case tabs.AGREGAR: detallesTabHeader = "Agregar"; /*perfil = new Perfil();*/ break;//Si es agregar, creo un nuevo objeto Cliente
                    case tabs.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case tabs.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; /*perfil = new Perfil();*/ break;//Si es agregar, creo un nuevo objeto Cliente
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

    }
}
