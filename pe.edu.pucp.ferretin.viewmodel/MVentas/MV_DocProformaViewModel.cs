using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_DocProformaViewModel : ViewModelBase
    {
        private Proforma _proforma;
        public Proforma proforma
        {
            get
            {
                return _proforma;
            }
            set
            {
                _proforma = value;
                NotifyPropertyChanged("proforma");
            }
        }
    }
}
