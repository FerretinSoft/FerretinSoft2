using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_RegistrarVentaViewModel : ViewModelBase
    {
        private string _nroDocSeleccionado = "";
        public string nroDocSeleccionado
        {
            get
            {
                return _nroDocSeleccionado;
            }
            set
            {
                _nroDocSeleccionado = value;
                if (value.Length == 8 || value.Length == 11)
                {
                    cargarCliente(null);
                }
                NotifyPropertyChanged("nroDocSeleccionado");
            }
        }

        Cliente _cliente;
        public Cliente cliente
        {
            get
            { return _cliente; }
            set
            {
                _cliente = value;
                NotifyPropertyChanged("cliente");
                NotifyPropertyChanged("widthClienteBar");
            }
        }


        public GridLength widthClienteBar
        {
            get
            {
                return cliente == null ? new GridLength(0) : GridLength.Auto;
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
        RelayCommand _buscarClienteCommand;
        public ICommand buscarClienteCommand
        {
            get
            {
                if (_buscarClienteCommand == null)
                {
                    _buscarClienteCommand = new RelayCommand(buscarCliente);
                }
                return _buscarClienteCommand;
            }
        }
        #endregion

        #region Comandos

        public void cargarCliente(Object id)
        {
            Cliente buscado = null;
            try
            {
                buscado = MV_ClienteService.obtenerClienteByNroDoc(nroDocSeleccionado);
            }catch{}

            if(buscado==null){
                MessageBox.Show("No se encontro ningún Cliente con el número de documento proporcionado","No se encontro",MessageBoxButton.OK,MessageBoxImage.Question);   
            }
            cliente = buscado;
            
        }

        public void buscarCliente(Object id)
        {
            
        }
        #endregion
    }
}
