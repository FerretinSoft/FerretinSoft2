using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;

namespace pe.edu.pucp.ferretin.viewmodel.MCompras
{
    public class MC_BuscadorProveedoresViewModel : ViewModelBase
    {
        public string _searchRazonSoc = "";
        public string searchRazonSoc
        {
            get
            {
                return _searchRazonSoc;
            }
            set
            {
                _searchRazonSoc = value;
                NotifyPropertyChanged("searchRazonSoc");
            }
        }

        public string _searchRuc = "";
        public string searchRuc
        {
            get
            {
                return _searchRuc;
            }
            set
            {
                _searchRuc = value;
                NotifyPropertyChanged("searchRuc");
            }
        }

        private IEnumerable<Proveedor> _listaProveedores;
        public IEnumerable<Proveedor> listaProveedores
        {
            get
            {
                _listaProveedores = ComunService.db.Proveedor.Where(p => p.razonSoc.ToLower().Trim().Contains(searchRazonSoc.ToLower().Trim()) && p.ruc.ToLower().Trim().Contains(searchRuc.ToLower().Trim()));

                return _listaProveedores;
            }
        }

        RelayCommand _actualizarListaProveedoresCommand;
        public ICommand actualizarListaProveedoresCommand
        {
            get
            {
                if (_actualizarListaProveedoresCommand == null)
                {
                    _actualizarListaProveedoresCommand = new RelayCommand(buscarProveedores);
                }
                return _actualizarListaProveedoresCommand;
            }
        }

        public void buscarProveedores(object obj)
        {
            NotifyPropertyChanged("listaProveedores");
        }
    }
}
