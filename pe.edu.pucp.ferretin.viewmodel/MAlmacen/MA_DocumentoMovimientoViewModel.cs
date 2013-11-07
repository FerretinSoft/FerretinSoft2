using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.viewmodel.MAlmacen
{
    public class MA_DocumentoMovimientoViewModel : ViewModelBase
    {
        public MA_DocumentoMovimientoViewModel()
        {
            _movimiento = new Movimiento();
        }
         private Movimiento _movimiento;
         public Movimiento movimiento
         {
             get
             {
                 return _movimiento;
             }
             set
             {
                 _movimiento = value;
                 NotifyPropertyChanged("movimiento");
             }
         }

        
    }
}
