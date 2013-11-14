using System.ComponentModel;

namespace pe.edu.pucp.ferretin.model
{

    public partial class ProveedorProducto : INotifyPropertyChanged
    {
        private bool _isSelected;
        public bool isSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                OnPropertyChanged("isSelected");
            }
        }

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }       
    }
}
