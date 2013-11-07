using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class EmpleadoTurno
    {

        //private bool _montoReadOnly = false;
        //public bool montoReadOnly
        //{
        //    get
        //    {
        //        return _montoReadOnly;
        //    }
        //    set
        //    {
        //        _montoReadOnly = value;
        //    }
        //}

        private bool _turnoReadOnly = false;
        public bool turnoReadOnly
        {
            get
            {
                return _turnoReadOnly;
            }
            set
            {
                _turnoReadOnly = value;
            }
        }

        public bool turnoIsEnable
        {
            get
            {
                return !turnoReadOnly;
            }
        }
        
      
    }
}
