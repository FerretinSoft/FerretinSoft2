using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class ProductoSol : INotifyPropertyChanged
    {
        public ProductoAlmacen producto { get; set; }

        private decimal _cantidad;
        public decimal cantidad {
            get
            {
                return _cantidad;
            }
            set
            {
                _cantidad = value;
                NotifyPropertyChanged("cantidad");
            }
        }
        private IEnumerable<Proveedor> _posiProveedor;
        public IEnumerable<Proveedor> posiProveedor
        {
            get
            {
                return _posiProveedor;
            }
            set
            {
                _posiProveedor = value;
                NotifyPropertyChanged("posiProveedor");
            }
        }
        private Proveedor _selectedProveedor;
        public Proveedor selectedProveedor
        {
            get
            {
                return _selectedProveedor;
            }
            set
            {
                _selectedProveedor = value;
                NotifyPropertyChanged("selectedProveedor");
            }
        }

       

        #region INotifyPropertyChanged Members

        //Instance
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            //this.VerifyPropertyName(propertyName);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        //Instance .Net 4.5
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged Members

    }
}
