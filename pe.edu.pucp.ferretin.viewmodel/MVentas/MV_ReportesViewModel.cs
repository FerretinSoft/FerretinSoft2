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

        public String _nombreBoton = "SIGUIENTE";
        public String nombreBoton { get { return _nombreBoton; } set { _nombreBoton = value; NotifyPropertyChanged("nombreBoton"); } }

        public String _comentRep = "";
        public String comentRep { get { return _comentRep; } set { _comentRep = value; NotifyPropertyChanged("comentRep"); } }
        
        public DateTime _searchFechaInicio = DateTime.Parse("10/09/2013");
        public DateTime searchFechaInicio { get { return _searchFechaInicio; } set { _searchFechaInicio = value; NotifyPropertyChanged("searchFechaInicio"); } }

        public DateTime _searchFechaFin = DateTime.Today.AddDays(1);
        public DateTime searchFechaFin { get { return _searchFechaFin; } set { _searchFechaFin = value; NotifyPropertyChanged("searchFechaFin"); } }

        #endregion



    }
}
