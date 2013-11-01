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
                try
                {
                    _lunesHoraInicio = this.TiendaHorario.Single(t => t.dia == "lunes").horaInicio.Value;
                    return _lunesHoraInicio;
                }
                catch (Exception e)
                {
                    return _lunesHoraInicio;
                }
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
                try{
                    _lunesHoraFin = this.TiendaHorario.Single(t => t.dia == "lunes").horaFin.Value;
                    return _lunesHoraFin;
                }catch(Exception e){
                    return _lunesHoraFin;
                }
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
                try
                {
                    _martesHoraInicio = this.TiendaHorario.Single(t => t.dia == "martes").horaInicio.Value;
                    return _martesHoraInicio;
                }
                catch (Exception e)
                {
                    return _martesHoraInicio;
                }
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
                try
                {
                    _martesHoraFin = this.TiendaHorario.Single(t => t.dia == "martes").horaFin.Value;
                    return _martesHoraFin;
                }
                catch (Exception e)
                {
                    return _martesHoraFin;
                }

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
                try
                {
                    _miercolesHoraInicio = this.TiendaHorario.Single(t => t.dia == "miercoles").horaInicio.Value;
                    return _miercolesHoraInicio;
                }
                catch (Exception e)
                {
                    return _miercolesHoraInicio;
                }
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
                try
                {
                    _miercolesHoraFin = this.TiendaHorario.Single(t => t.dia == "miercoles").horaFin.Value;
                    return _miercolesHoraFin;
                }catch(Exception e){
                    return _miercolesHoraFin;
                }
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
                try
                {
                    _juevesHoraInicio = this.TiendaHorario.Single(t => t.dia == "jueves").horaInicio.Value;
                    return _juevesHoraInicio;
                }
                catch (Exception e)
                {
                    return _juevesHoraInicio;
                }
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
                try
                {
                    _juevesHoraFin = this.TiendaHorario.Single(t => t.dia == "jueves").horaFin.Value;
                    return _juevesHoraFin;
                }catch(Exception e){
                    return _juevesHoraFin;
                }
                
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
                try
                {
                    _viernesHoraInicio = this.TiendaHorario.Single(t => t.dia == "viernes").horaInicio.Value;
                    return _viernesHoraInicio;
                }
                catch (Exception e)
                {
                    return _viernesHoraInicio;
                }
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
                try{
                    _viernesHoraFin = this.TiendaHorario.Single(t => t.dia == "viernes").horaFin.Value;                
                    return _viernesHoraFin;
                }catch(Exception e){
                    return _viernesHoraFin;
                }
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
                try
                {
                    _sabadoHoraInicio = this.TiendaHorario.Single(t => t.dia == "sabado").horaInicio.Value;
                    return _sabadoHoraInicio;
                }
                catch (Exception e)
                {
                    return _sabadoHoraInicio;
                }       
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
                try
                {
                _sabadoHoraFin = this.TiendaHorario.Single(t => t.dia == "sabado").horaFin.Value;                
                    return _sabadoHoraFin;
                }
                catch(Exception e){
                    return _sabadoHoraFin;
                }               
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
                try
                {
                    _domingoHoraInicio = this.TiendaHorario.Single(t => t.dia == "domingo").horaInicio.Value;
                    return _domingoHoraInicio;
                }
                catch (Exception e)
                {
                    return _domingoHoraInicio;
                }
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
                try
                {
                    _domingoHoraFin = this.TiendaHorario.Single(t => t.dia == "domingo").horaFin.Value;
                    return _domingoHoraFin;
                }
                catch (Exception e)
                {
                    return _domingoHoraFin;
                }
                    
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
