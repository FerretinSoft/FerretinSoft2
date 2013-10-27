using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace pe.edu.pucp.ferretin.viewmodel.MAlmacen
{
    public class MA_MantenimientoCategoriasViewModel : ViewModelBase
    {
        public String _nombre = "Hola";
        public String nombre { get { return _nombre; } set { _nombre = value; } }

        public MA_MantenimientoCategoriasViewModel()
        {
            MessageBox.Show("Inicio");
        }

        private IEnumerable<Categoria> _categoriasProductos;
        public IEnumerable<Categoria> categoriasProductos
        {
            get
            {
                _categoriasProductos = MA_CategoriaService.obtenerTodasCategorias();
                return _categoriasProductos;
            }
            set
            {
                _categoriasProductos = value;
            }
        }
    }
}
