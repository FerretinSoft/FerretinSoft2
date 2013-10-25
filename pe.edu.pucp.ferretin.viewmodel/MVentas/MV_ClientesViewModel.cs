using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_ClientesViewModel : ViewModelBase
    {
        #region Valores para el cuadro de Búsqueda
        public String searchNroDoc { get; set; }
        public String searchNombre { get; set; }
        public String searchApMaterno { get; set; }
        public String searchApPaterno { get; set; }
        public int searchTipoDocumento { get; set; }
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
                    case (int)tabs.BUSQUEDA: detallesTabHeader = "Agregar"; cliente = new Cliente(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case (int)tabs.AGREGAR: detallesTabHeader = "Agregar"; cliente = new Cliente(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case (int)tabs.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case (int)tabs.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; cliente = new Cliente(); break;//Si es agregar, creo un nuevo objeto Cliente
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

        private Cliente _cliente = new Cliente();
        public Cliente cliente
        {
            get
            {
                return _cliente;
            }
            set
            {
                _cliente = value;
                NotifyPropertyChanged("cliente");
            }
        }
    }
}
