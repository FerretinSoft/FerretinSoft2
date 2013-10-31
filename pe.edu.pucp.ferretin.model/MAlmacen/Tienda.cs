using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    public partial class Tienda
    {        

        private int _lunesHoraInicio=-1;
        public int lunesHoraInicio { 
            get {               
                _lunesHoraInicio = this.TiendaHorario.Single(t => t.dia == "lunes").horaInicio.Value;
                if (_lunesHoraInicio >= 0)
                {
                    return _lunesHoraInicio;
                }
                    return -1;                
            } 
            set {
                this.verificaTiendaHorario();
                _lunesHoraInicio = value;
                if(_lunesHoraInicio!=-1)
                    this.TiendaHorario.Single(t => t.dia == "lunes").horaInicio = _lunesHoraInicio;                
            } 
        }

        private int _lunesHoraFin = -1;
        public int lunesHoraFin
        {
            get
            {                
                _lunesHoraFin = this.TiendaHorario.Single(t => t.dia == "lunes").horaFin.Value;
                if (_lunesHoraFin >= 0)
                {
                    return _lunesHoraFin;
                }
                    return -1;
            }
            set
            {
                this.verificaTiendaHorario();
                _lunesHoraFin = value;
                if (_lunesHoraFin != -1)
                    this.TiendaHorario.Single(t => t.dia == "lunes").horaFin = _lunesHoraFin;                
            }
        }

        private int _martesHoraInicio = -1;
        public int martesHoraInicio
        {
            get
            {
                _martesHoraInicio = this.TiendaHorario.Single(t => t.dia == "martes").horaInicio.Value;
                if (_martesHoraInicio >= 0)
                {
                    return _martesHoraInicio;
                }
                return -1;
            }
            set
            {
                this.verificaTiendaHorario();
                _martesHoraInicio = value;
                if (_martesHoraInicio != -1)
                    this.TiendaHorario.Single(t => t.dia == "martes").horaInicio = _martesHoraInicio;                
            }
        }

        private int _martesHoraFin = -1;
        public int martesHoraFin
        {
            get
            {
                _martesHoraFin = this.TiendaHorario.Single(t => t.dia == "martes").horaFin.Value;
                if (_martesHoraFin >= 0)
                {
                    return _martesHoraFin;
                }
                return -1;
            }
            set
            {
                this.verificaTiendaHorario();
                _martesHoraFin = value;
                if (_martesHoraFin != -1)
                    this.TiendaHorario.Single(t => t.dia == "martes").horaFin = _martesHoraFin;                
            }
        }

        private int _miercolesHoraInicio = -1;
        public int miercolesHoraInicio
        {
            get
            {
                _miercolesHoraInicio = this.TiendaHorario.Single(t => t.dia == "miercoles").horaInicio.Value;
                if (_miercolesHoraInicio >= 0)
                {
                    return _miercolesHoraInicio;
                }
                return -1;
            }
            set
            {
                this.verificaTiendaHorario();
                _miercolesHoraInicio = value;
                if (_miercolesHoraInicio != -1)
                    this.TiendaHorario.Single(t => t.dia == "miercoles").horaInicio = _miercolesHoraInicio;             
            }
        }

        private int _miercolesHoraFin = -1;
        public int miercolesHoraFin
        {
            get
            {
                _miercolesHoraFin = this.TiendaHorario.Single(t => t.dia == "miercoles").horaFin.Value;
                if (_miercolesHoraFin >= 0)
                {
                    return _miercolesHoraFin;
                }
                return -1;
            }
            set
            {
                this.verificaTiendaHorario();
                _miercolesHoraFin = value;
                if (_miercolesHoraFin != -1)
                    this.TiendaHorario.Single(t => t.dia == "miercoles").horaFin = _miercolesHoraFin;                
            }
        }

        private int _juevesHoraInicio = -1;
        public int juevesHoraInicio
        {
            get
            {
                _juevesHoraInicio = this.TiendaHorario.Single(t => t.dia == "jueves").horaInicio.Value;
                if (_juevesHoraInicio >= 0)
                {
                    return _juevesHoraInicio;
                }
                return -1;
            }
            set
            {
                this.verificaTiendaHorario();
                _juevesHoraInicio = value;
                if (_juevesHoraInicio != -1)
                    this.TiendaHorario.Single(t => t.dia == "jueves").horaInicio = _juevesHoraInicio;                
            }
        }

        private int _juevesHoraFin = -1;
        public int juevesHoraFin
        {
            get
            {
                _juevesHoraFin = this.TiendaHorario.Single(t => t.dia == "jueves").horaFin.Value;
                if (_juevesHoraFin >= 0)
                {
                    return _juevesHoraFin;
                }
                return -1;
            }
            set
            {
                this.verificaTiendaHorario();
                _juevesHoraFin = value;
                if (_juevesHoraFin != -1)
                    this.TiendaHorario.Single(t => t.dia == "jueves").horaFin = _juevesHoraFin;                
            }
        }

        private int _viernesHoraInicio = -1;
        public int viernesHoraInicio
        {
            get
            {
                _viernesHoraInicio = this.TiendaHorario.Single(t => t.dia == "viernes").horaInicio.Value;
                if (_viernesHoraInicio >= 0)
                {
                    return _viernesHoraInicio;
                }
                return -1;
            }
            set
            {
                this.verificaTiendaHorario();
                _viernesHoraInicio = value;
                if (_viernesHoraInicio != -1)
                    this.TiendaHorario.Single(t => t.dia == "viernes").horaInicio = _viernesHoraInicio;               
            }
        }

        private int _viernesHoraFin = -1;
        public int viernesHoraFin
        {
            get
            {
                _viernesHoraFin = this.TiendaHorario.Single(t => t.dia == "viernes").horaFin.Value;
                if (_viernesHoraFin >= 0)
                {
                    return _viernesHoraFin;
                }
                return -1;
            }
            set
            {
                this.verificaTiendaHorario();
                _viernesHoraFin = value;
                if (_viernesHoraFin != -1)
                    this.TiendaHorario.Single(t => t.dia == "viernes").horaFin = _viernesHoraFin;                
            }
        }

        private int _sabadoHoraInicio = -1;
        public int sabadoHoraInicio
        {
            get
            {
                _sabadoHoraInicio = this.TiendaHorario.Single(t => t.dia == "sabado").horaInicio.Value;
                if (_sabadoHoraInicio >= 0)
                {
                    return _sabadoHoraInicio;
                }
                return -1;
            }
            set
            {
                this.verificaTiendaHorario();
                _sabadoHoraInicio = value;
                if (_sabadoHoraInicio != -1)
                    this.TiendaHorario.Single(t => t.dia == "sabado").horaInicio = _sabadoHoraInicio;                
            }
        }

        private int _sabadoHoraFin = -1;
        public int sabadoHoraFin
        {
            get
            {
                _sabadoHoraFin = this.TiendaHorario.Single(t => t.dia == "sabado").horaFin.Value;
                if (_sabadoHoraFin >= 0)
                {
                    return _sabadoHoraFin;
                }
                return -1;
            }
            set
            {
                this.verificaTiendaHorario();
                _sabadoHoraFin = value;
                if (_sabadoHoraFin != -1)
                    this.TiendaHorario.Single(t => t.dia == "sabado").horaFin = _sabadoHoraFin;                
            }
        }

        private int _domingoHoraInicio = -1;
        public int domingoHoraInicio
        {
            get
            {
                _domingoHoraInicio = this.TiendaHorario.Single(t => t.dia == "domingo").horaInicio.Value;
                if (_domingoHoraInicio >= 0)
                {
                    return _domingoHoraInicio;
                }
                return -1;
            }
            set
            {
                this.verificaTiendaHorario();
                _domingoHoraInicio = value;
                if (_domingoHoraInicio != -1)
                    this.TiendaHorario.Single(t => t.dia == "domingo").horaInicio = _domingoHoraInicio;                
            }
        }

        private int _domingoHoraFin = -1;
        public int domingoHoraFin
        {
            get
            {
                _domingoHoraFin = this.TiendaHorario.Single(t => t.dia == "domingo").horaFin.Value;
                if (_domingoHoraFin >= 0)
                {
                    return _domingoHoraFin;
                }
                return -1;
            }
            set
            {
                this.verificaTiendaHorario();
                _domingoHoraFin = value;
                if (_domingoHoraFin != -1)
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
