using pe.edu.pucp.ferretin.controller;
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

        private IEnumerable<Almacen> _almacenes;
        public IEnumerable<Almacen> almacenes
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
    }
}
