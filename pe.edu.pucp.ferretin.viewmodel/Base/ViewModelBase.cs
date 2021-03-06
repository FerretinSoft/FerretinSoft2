﻿using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.viewmodel
{
    public class ViewModelBase : INotifyPropertyChanged
    {

        public String fechaHoyString
        {
            get
            {
                return DateTime.Today.ToString("d MMM yyyy").ToUpper();
            }
        }

        #region INotifyPropertyChanged Members

        //Instance
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged(string name)
        {
            //this.VerifyPropertyName(propertyName);
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        //Instance .Net 4.5
        public void NotifyPropertyChanged([CallerMemberName] String propertyName = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion INotifyPropertyChanged Members

        #region " Declaraciones "

        private Dictionary<string, UIValidationError> _uIValidationErrorDictionary = new Dictionary<string, UIValidationError>();

        #endregion 

        #region " Propiedades "

        public int UIValidationErrorCount
        {
            get { return _uIValidationErrorDictionary.Count; }
        }

        public string UIValidationErrorMessages
        {
            get
            {

                if (UIValidationErrorCount > 0)
                {

                    System.Text.StringBuilder sb = new System.Text.StringBuilder(1024);

                    foreach (KeyValuePair<string, UIValidationError> kvp in _uIValidationErrorDictionary)
                    {
                        sb.AppendLine(kvp.Value.ToFriendlyErrorMessage());
                    }

                    return sb.ToString();
                }

                else
                {
                    return string.Empty;
                }
            }

        }

        #endregion 
    
        private IEnumerable<Tienda> _almacenes;
        public IEnumerable<Tienda> almacenes
        {
            get
            {
                if (_almacenes == null)
                {
                    _almacenes = ComunService.almacenes;
                }
                return _almacenes;
            }
            set
            {
                _almacenes = value;
            }
        }

        private IEnumerable<Tienda> _tiendas;
        public IEnumerable<Tienda> tiendas
        {
            get
            {
                if (_tiendas == null)
                {
                    _tiendas = ComunService.tiendas;
                }
                return _tiendas;
            }
            set
            {
                _tiendas = value;
            }
        }

        private IEnumerable<Tienda> _tiendasFiltro;
        public IEnumerable<Tienda> tiendasFiltro
        {
            get
            {
                if (_tiendasFiltro == null)
                {
                    var sequence = Enumerable.Empty<Tienda>();
                    IEnumerable<Tienda> items = new Tienda[] { new Tienda{ id = 0, nombre = "Todas" } };
                    return items.Concat(ComunService.tiendas);
                }
                return _tiendasFiltro;
            }
            set
            {
                _tiendasFiltro = value;
                NotifyPropertyChanged("tiendasFiltro");
            }
        }

        public UbigeoDistrito selectedDistrito { get; set; }
        private UbigeoDepartamento _selectedDepartamento;
        public UbigeoDepartamento selectedDepartamento
        {
            get
            {
                return _selectedDepartamento;
            }
            set
            {
                _selectedDepartamento = value;
                NotifyPropertyChanged("selectedDepartamento");
                if(_selectedDepartamento!=null)
                    provincias = from d in ComunService.provincias where d.id_ubig_departamento == value.id select d;
            }
        }


        private UbigeoProvincia _selectedProvincia;
        public UbigeoProvincia selectedProvincia
        {
            get
            {
                return _selectedProvincia;
            }
            set
            {
                _selectedProvincia = value;
                NotifyPropertyChanged("selectedProvincia");
                if (_selectedProvincia != null)
                    distritos = from d in ComunService.distritos where d.id_ubig_provincia == value.id select d;
            }
        }

        private IEnumerable<UbigeoDepartamento> _departamentos;
        public IEnumerable<UbigeoDepartamento> departamentos
        {
            get
            {
                if (_departamentos == null)
                {
                    _departamentos = ComunService.departamentos;
                }
                return _departamentos;
            }
            set
            {
                _departamentos = value;
                NotifyPropertyChanged("departamentos");
            }
        }
        private IEnumerable<UbigeoProvincia> _provincias;
        public IEnumerable<UbigeoProvincia> provincias
        {
            get
            {
                return _provincias;
            }
            set
            {
                _provincias = value;
                NotifyPropertyChanged("provincias");
            }
        }

        private IEnumerable<UbigeoDistrito> _distritos;
        public IEnumerable<UbigeoDistrito> distritos
        {
            get
            {
                return _distritos;
            }
            set
            {
                _distritos = value;
                NotifyPropertyChanged("distritos");
            }
        }

        /// <summary>
        /// Estado de las solicitudes de abastecimiento
        /// </summary>
        //private IEnumerable<SolicitudAbastecimientoEstado> _estadoSolicitud;
        public IEnumerable<SolicitudAbastecimientoEstado> estadoSolicitud
        {
            get
            {
                var sequence = Enumerable.Empty<SolicitudAbastecimientoEstado>();
                IEnumerable<SolicitudAbastecimientoEstado> items = new SolicitudAbastecimientoEstado[] { new SolicitudAbastecimientoEstado { id = 0, nombre = "Todos" } };
                return items.Concat(MA_SharedService.estadosSolicitud);
                
            }            
        }

        /// <summary>
        /// Usuario Logueado
        /// </summary>

        private Usuario _usuarioLogueado;
        public Usuario usuarioLogueado
        {
            get
            {
                return ComunService.usuarioL;
            }
        }
        /*********************************************/
        private Tienda _tiendaTest;
        public Tienda tiendaTest
        {
            get
            {
                if (_tiendaTest == null)
                {
                    _tiendaTest = ComunService.tienda;
                }
                return _tiendaTest;
            }
            set
            {
                _tiendaTest = value;
            }
        }
        /*********************************************/



    }
}
