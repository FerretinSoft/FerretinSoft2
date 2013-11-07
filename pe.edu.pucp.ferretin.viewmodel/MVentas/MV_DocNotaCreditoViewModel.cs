using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Documents;
using System.Windows.Media;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_DocNotaCreditoViewModel : ViewModelBase
    {
         #region Constructor
        public MV_DocNotaCreditoViewModel()
        {
            _notaCredito = new NotaCredito();
        }
        #endregion

         #region
         private NotaCredito _notaCredito;
         public NotaCredito notaCredito
         {
             get
             {
                 return _notaCredito;
             }
             set
             {
                 _notaCredito = value;
                 NotifyPropertyChanged("notaCredito");
             }
         }

         #endregion
    }
}
