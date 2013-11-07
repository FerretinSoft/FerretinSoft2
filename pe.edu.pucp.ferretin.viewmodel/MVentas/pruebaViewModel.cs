using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class pruebaViewModel : ViewModelBase
    {
        private Venta _venta;
        public Venta venta
        {
            get
            {
                return _venta;
            }
            set
            {
                _venta = value;
                NotifyPropertyChanged("venta");
            }
        }
    }
}
