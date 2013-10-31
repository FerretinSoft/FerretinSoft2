using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class Tienda
    {        

        private int _lunesHoraInicio=0;
        public int lunesHoraInicio { 
            get {
                _lunesHoraInicio = this.TiendaHorario.Single(t => t.dia == "lunes").horaInicio.Value;
                return _lunesHoraInicio;
            } 
            set {
                this.verificaTiendaHorario();
                _lunesHoraInicio = value;
                this.TiendaHorario.Single(t => t.dia == "lunes").horaInicio = _lunesHoraInicio;                
            } 
        }

        private int _lunesHoraFin = 0;
        public int lunesHoraFin
        {
            get
            {
                _lunesHoraFin = this.TiendaHorario.Single(t => t.dia == "lunes").horaFin.Value;
                return _lunesHoraFin;
            }
            set
            {
                this.verificaTiendaHorario();
                _lunesHoraFin = value;
                this.TiendaHorario.Single(t => t.dia == "lunes").horaFin = _lunesHoraFin;                
            }
        }

        private int _martesHoraInicio = 0;
        public int martesHoraInicio
        {
            get
            {
                _martesHoraInicio = this.TiendaHorario.Single(t => t.dia == "martes").horaInicio.Value;
                return _martesHoraInicio;
            }
            set
            {
                this.verificaTiendaHorario();
                _martesHoraInicio = value;
                this.TiendaHorario.Single(t => t.dia == "martes").horaInicio = _martesHoraInicio;                
            }
        }

        private int _martesHoraFin = 0;
        public int martesHoraFin
        {
            get
            {
                _martesHoraFin = this.TiendaHorario.Single(t => t.dia == "martes").horaFin.Value;
                return _martesHoraFin;
            }
            set
            {
                this.verificaTiendaHorario();
                _martesHoraFin = value;
                this.TiendaHorario.Single(t => t.dia == "martes").horaFin = _martesHoraFin;                
            }
        }

        private int _miercolesHoraInicio = 0;
        public int miercolesHoraInicio
        {
            get
            {
                _miercolesHoraInicio = this.TiendaHorario.Single(t => t.dia == "miercoles").horaInicio.Value;
                return _miercolesHoraInicio;
            }
            set
            {
                this.verificaTiendaHorario();
                _miercolesHoraInicio = value;
                this.TiendaHorario.Single(t => t.dia == "miercoles").horaInicio = _miercolesHoraInicio;             
            }
        }

        private int _miercolesHoraFin = 0;
        public int miercolesHoraFin
        {
            get
            {
                _miercolesHoraFin = this.TiendaHorario.Single(t => t.dia == "miercoles").horaFin.Value;
                return _miercolesHoraFin;
            }
            set
            {
                this.verificaTiendaHorario();
                _miercolesHoraFin = value;
                this.TiendaHorario.Single(t => t.dia == "miercoles").horaFin = _miercolesHoraFin;                
            }
        }

        private int _juevesHoraInicio = 0;
        public int juevesHoraInicio
        {
            get
            {
                _juevesHoraInicio = this.TiendaHorario.Single(t => t.dia == "jueves").horaInicio.Value;
                return _juevesHoraInicio;
            }
            set
            {
                this.verificaTiendaHorario();
                _juevesHoraInicio = value;
                this.TiendaHorario.Single(t => t.dia == "jueves").horaInicio = _juevesHoraInicio;                
            }
        }

        private int _juevesHoraFin = 0;
        public int juevesHoraFin
        {
            get
            {
                _juevesHoraFin = this.TiendaHorario.Single(t => t.dia == "jueves").horaFin.Value;
                return _juevesHoraFin;
            }
            set
            {
                this.verificaTiendaHorario();
                _juevesHoraFin = value;
                this.TiendaHorario.Single(t => t.dia == "jueves").horaFin = _juevesHoraFin;                
            }
        }

        private int _viernesHoraInicio = 0;
        public int viernesHoraInicio
        {
            get
            {
                _viernesHoraInicio = this.TiendaHorario.Single(t => t.dia == "viernes").horaInicio.Value;
                return _viernesHoraInicio;
            }
            set
            {
                this.verificaTiendaHorario();
                _viernesHoraInicio = value;
                this.TiendaHorario.Single(t => t.dia == "viernes").horaInicio = _viernesHoraInicio;               
            }
        }

        private int _viernesHoraFin = 0;
        public int viernesHoraFin
        {
            get
            {
                _viernesHoraFin = this.TiendaHorario.Single(t => t.dia == "viernes").horaFin.Value;
                return _viernesHoraFin;
            }
            set
            {
                this.verificaTiendaHorario();
                _viernesHoraFin = value;
                this.TiendaHorario.Single(t => t.dia == "viernes").horaFin = _viernesHoraFin;                
            }
        }

        private int _sabadoHoraInicio = 0;
        public int sabadoHoraInicio
        {
            get
            {
                _sabadoHoraInicio = this.TiendaHorario.Single(t => t.dia == "sabado").horaInicio.Value;
                return _sabadoHoraInicio;
            }
            set
            {
                this.verificaTiendaHorario();
                _sabadoHoraInicio = value;
                this.TiendaHorario.Single(t => t.dia == "sabado").horaInicio = _sabadoHoraInicio;                
            }
        }

        private int _sabadoHoraFin = 0;
        public int sabadoHoraFin
        {
            get
            {
                _sabadoHoraFin = this.TiendaHorario.Single(t => t.dia == "sabado").horaFin.Value;
                return _sabadoHoraFin;
            }
            set
            {
                this.verificaTiendaHorario();
                _sabadoHoraFin = value;
                this.TiendaHorario.Single(t => t.dia == "sabado").horaFin = _sabadoHoraFin;                
            }
        }

        private int _domingoHoraInicio = 0;
        public int domingoHoraInicio
        {
            get
            {
                _domingoHoraInicio = this.TiendaHorario.Single(t => t.dia == "domingo").horaInicio.Value;
                return _domingoHoraInicio;
            }
            set
            {
                this.verificaTiendaHorario();
                _domingoHoraInicio = value;
                this.TiendaHorario.Single(t => t.dia == "domingo").horaInicio = _domingoHoraInicio;                
            }
        }

        private int _domingoHoraFin = 0;
        public int domingoHoraFin
        {
            get
            {
                _domingoHoraFin = this.TiendaHorario.Single(t => t.dia == "domingo").horaFin.Value;
                return _domingoHoraFin;
            }
            set
            {
                this.verificaTiendaHorario();
                _domingoHoraFin = value;
                this.TiendaHorario.Single(t => t.dia == "domingo").horaFin = _domingoHoraFin;                
            }
        }

        public IEnumerable<TiendaHorario> tiendasH = null;

        public void verificaTiendaHorario(){            

            if ((this.TiendaHorario.Where(t => t.id_almacen == this.id)).Count() <= 0)
            {
                //Creo una nueva secuencia
                var sequence = Enumerable.Empty<TiendaHorario>();
                //Primero agrego un item de Todos para que salga al inicio
                //Pongo el ID en 0 para que al buscar, no filtre nada cuando se selecciona todos
                IEnumerable<TiendaHorario> tiendasHorariosL = new TiendaHorario[] { new TiendaHorario { id_almacen = this.id, Tienda = this, dia = "lunes" } };
                IEnumerable<TiendaHorario> tiendasHorariosM = new TiendaHorario[] { new TiendaHorario { id_almacen = this.id, Tienda = this, dia = "martes" } };
                IEnumerable<TiendaHorario> tiendasHorariosMi = new TiendaHorario[] { new TiendaHorario { id_almacen = this.id, Tienda = this, dia = "miercoles" } };
                IEnumerable<TiendaHorario> tiendasHorariosJ = new TiendaHorario[] { new TiendaHorario { id_almacen = this.id, Tienda = this, dia = "jueves" } };
                IEnumerable<TiendaHorario> tiendasHorariosV = new TiendaHorario[] { new TiendaHorario { id_almacen = this.id, Tienda = this, dia = "viernes" } };
                IEnumerable<TiendaHorario> tiendasHorariosS = new TiendaHorario[] { new TiendaHorario { id_almacen = this.id, Tienda = this, dia = "sabado" } };
                IEnumerable<TiendaHorario> tiendasHorariosD = new TiendaHorario[] { new TiendaHorario { id_almacen = this.id, Tienda = this, dia = "domingo" } };
                sequence.Concat(tiendasHorariosL).Concat(tiendasHorariosM).Concat(tiendasHorariosMi).Concat(tiendasHorariosJ).Concat(tiendasHorariosV).Concat(tiendasHorariosS).Concat(tiendasHorariosD);
                tiendasH = sequence;
            }           
        }
        

    }
}
