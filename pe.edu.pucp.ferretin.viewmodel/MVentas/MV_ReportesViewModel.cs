using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.viewmodel.MVentas
{
    public class MV_ReportesViewModel : ViewModelBase
    {
        #region Constructor
        public MV_ReportesViewModel()
        {
            
        }
        #endregion

        #region Valores para el cuadro de Búsqueda
        public String _aliasRep = "";
        public String aliasRep { get { return _aliasRep; } set { _aliasRep = value; NotifyPropertyChanged("aliasRep"); } }
        
        public DateTime _searchFechaInicio = DateTime.Parse("10/09/2013");
        public DateTime searchFechaInicio { get { return _searchFechaInicio; } set { _searchFechaInicio = value; NotifyPropertyChanged("searchFechaInicio"); } }

        public DateTime _searchFechaFin = DateTime.Today;
        public DateTime searchFechaFin { get { return _searchFechaFin; } set { _searchFechaFin = value; NotifyPropertyChanged("searchFechaFin"); } }

        #endregion



    }
}
