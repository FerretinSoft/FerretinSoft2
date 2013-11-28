using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.model
{
    partial class ProductoAlmacen
    {
        private bool _isService = false;
        public bool isService
        {
            get
            {
                return _isService;
            }
            set
            {
                _isService = value;
            }
        }

        public Servicio servicioSeleccionado
        {
            get;
            set;
        }


        private string _nombreProducto = "";
        public string nombreProducto
        {
            get
            {
                if (Producto != null)
                {
                    return Producto.nombre;
                }
                else
                {
                    return _nombreProducto;
                }
            }
            set
            {
                _nombreProducto = value;
            }
        }

        private decimal _montoServicio = 0;
        public decimal montoServicio
        {
            get
            {
                return _montoServicio;
            }
            set
            {
                _montoServicio = value;
            }
        }
    }
}
