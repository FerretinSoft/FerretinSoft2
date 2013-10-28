using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MRecursosHumanos;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace pe.edu.pucp.ferretin.view.MRecursosHumanos
{


    public class MR_AdministrarPersonalViewModel : INotifyPropertyChanged
    {
        public String searchDni { get; set; }
        public String searchNombre { get; set; }
        public int searchCodigo { get; set; }
        public String searchCargo { get; set; }
        public String searchTienda { get; set; }

        public enum tabs
        {
            //Pestañas virtuales:
            //0       1        2          3
            BUSQUEDA, EDICION , MODIFICAR, DETALLES
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
                    case (int)tabs.BUSQUEDA: detallesTabHeader = "Agregar"; empleado = new Empleado(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case (int)tabs.EDICION: detallesTabHeader = "Edicion"; empleado = new Empleado(); break;//Si es agregar, creo un nuevo objeto Cliente
                    case (int)tabs.MODIFICAR: detallesTabHeader = "Modificar"; break;
                    case (int)tabs.DETALLES: detallesTabHeader = "Detalles"; break;
                    default: detallesTabHeader = "Agregar"; empleado = new Empleado(); break;//Si es agregar, creo un nuevo objeto Cliente
                }
                //Cuando se cambia el status, tambien se tiene que cambiar el currentIndex del tab
                //currentIndexTab = _statusTab == 0 ? 0 : 1;
                NotifyPropertyChanged("statusTab");
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


        public IEnumerable<Cargo> cargo
        {
            get 
            {
                return MR_CargTieService.cargos;        
            }           
        }

        public IEnumerable<Tienda> tienda
        {
            get
            {
                return MR_CargTieService.tiendas;
            }
        }

        public IEnumerable<UbigeoDepartamento> departamento
        {
            get
            {
                return MR_ComunService.departamentos;
            }
        }

        public IEnumerable<UbigeoProvincia> provincia
        {
            get
            {
                return MR_ComunService.departamentos.ElementAt(selectedDepartamento).UbigeoProvincia;
            }
        }

        public IEnumerable<UbigeoDistrito> distrito
        {
            get
            {
                return MR_ComunService.departamentos.ElementAt(selectedDepartamento).UbigeoProvincia.ElementAt(selectedProvincia).UbigeoDistrito;
            }
        }
        private int selectedDistrito { get; set; }
        private int _selectedDepartamento;
        public int selectedDepartamento
        {
            get
            {
                return _selectedDepartamento > 0 ? _selectedDepartamento : 0;
            }
            set
            {
                _selectedDepartamento = value;
                NotifyPropertyChanged("selectedDepartamento");
                NotifyPropertyChanged("provincia");
                NotifyPropertyChanged("distrito");
            }
        }

        
        private int _selectedProvincia;
        public int selectedProvincia
        {
            get
            {
                return _selectedProvincia > 0 ? _selectedProvincia : 0;
            }
            set
            {
                _selectedProvincia = value;
                NotifyPropertyChanged("distrito");
            }
        }

        private int _selectedCargo;
        public int selectedCargo
        {
            get
            {
                return _selectedCargo > 0 ? _selectedCargo : 0;
            }
            set
            {
                _selectedCargo = value;
                NotifyPropertyChanged("selectedCargo");
                NotifyPropertyChanged("cargo");
                NotifyPropertyChanged("cargoMenu");
                
            }
        }

        private int _selectedTienda;
        public int selectedTienda
        {
            get
            {
                return _selectedTienda > 0 ? _selectedCargo : 0;
            }
            set
            {
                _selectedTienda = value;
                NotifyPropertyChanged("selectedTienda");
                NotifyPropertyChanged("tienda");
                NotifyPropertyChanged("tiendaMenu");

            }
        }


      


        #region INotifyPropertyChanged Members
        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }
        #endregion

        private Empleado _empleado = new Empleado();
        public Empleado empleado
        {
            get
            {
                return _empleado;
            }
            set
            {
                _empleado = value;
                NotifyPropertyChanged("empleado");
            }
        }
    }


    /// <summary>
    /// Lógica de interacción para MR_AdministrarPersonalWindow.xaml
    /// </summary>
    public partial class MR_AdministrarPersonalWindow : Window
    {
        MR_AdministrarPersonalViewModel MR_AdministrarPersonalViewModel = new MR_AdministrarPersonalViewModel();

        public MR_AdministrarPersonalWindow()
        {
            InitializeComponent();
          

            empleadosGrid.ItemsSource = MR_EmpleadoService.obtenerListaEmpleados();
            personalTabControl.DataContext = MR_AdministrarPersonalViewModel;          
        }

        private void nuevoEmpleadoBtn_Click(object sender, RoutedEventArgs e)
        {
         
        }

        private void edEmpleadoBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }



        public void codigoPersonal_Click(object sender, RoutedEventArgs e)
        {


        }




    }
}
