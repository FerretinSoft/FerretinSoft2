using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace pe.edu.pucp.ferretin.viewmodel.MAlmacen
{
    public class MA_MantenimientoCategoriasViewModel : ViewModelBase
    {
        
        public MA_MantenimientoCategoriasViewModel()
        {
            
        }



        private IEnumerable<Categoria> _categoriasPadre;
        public IEnumerable<Categoria> categoriasPadre { get { return _categoriasPadre; } set { _categoriasPadre = value; OnPropertyChanged("categoriasPadre"); } }


        private IEnumerable<Categoria> _categoriaPrincipal;
        public IEnumerable<Categoria> categoriaPrincipal
        {
            get
            {
                //Devolver la categoría padre
               _categoriaPrincipal = MA_CategoriaService.categorias.Where(c => c.id_padre == null);
               
                return _categoriaPrincipal;
            }
            set
            {
                _categoriaPrincipal = value;
            }
        }
        private Categoria _CategoriaSeleccionada;
        public Categoria CategoriaSeleccionada
        {

            get
            {
                return _CategoriaSeleccionada;
            }
            set
            {
                _CategoriaSeleccionada = value;
                //Actualizo el combobox de categorias padre
                categoriasPadre = MA_CategoriaService.categorias.Where(c => c.nivel == _CategoriaSeleccionada.Categoria1.nivel);
                OnPropertyChanged("CategoriaSeleccionada");
            }
        }


        #region RelayCommand
        RelayCommand _saveCategoriaCommand;
        public ICommand saveCategoriaCommand
        {
            get
            {
                if (_saveCategoriaCommand == null)
                {
                    _saveCategoriaCommand = new RelayCommand(saveCategoria);
                }
                return _saveCategoriaCommand;
            }
        }
        RelayCommand _nuevaCategoriaCommand;
        public ICommand nuevaCategoriaCommand
        {
            get
            {
                if (_nuevaCategoriaCommand == null)
                {
                    _nuevaCategoriaCommand = new RelayCommand(nuevaCategoria);
                }
                return _nuevaCategoriaCommand;
            }
        }

        RelayCommand _actualizarArbolCategoriaCommand;
        public ICommand actualizarArbolCategoriaCommand
        {
            get
            {
                if (_actualizarArbolCategoriaCommand == null)
                {
                    _actualizarArbolCategoriaCommand = new RelayCommand(param => NotifyPropertyChanged("categoriaPrincipal"));
                }
                return _actualizarArbolCategoriaCommand;
            } 
        }


        //deleteCategoriaCommand
        RelayCommand _deleteCategoriaCommand;
        public ICommand deleteCategoriaCommand
        {
            get
            {
                if (_deleteCategoriaCommand == null)
                {
                    _deleteCategoriaCommand = new RelayCommand(deleteCategoria);
                }
                return _deleteCategoriaCommand;
            }
        }

        #endregion

        #region Comandos

       

        public void nuevaCategoria(Object obj)
        {
            Categoria categoria = new Categoria();
            categoria.nivel = (byte)(CategoriaSeleccionada.nivel + 1);
            categoria.Categoria1 = CategoriaSeleccionada;
            CategoriaSeleccionada = categoria;
        }

        public void deleteCategoria(Object obj)
        {
            bool valor=MA_CategoriaService.eliminarCategoria(CategoriaSeleccionada);
            if (valor==false)
            {
                MessageBox.Show("La Categoría esta asignada, no se pudo eliminar");
            }
            else 
            {
                MessageBox.Show("La Categoría se elimino con éxito");
            }

           
           
        }

        public void saveCategoria(Object obj)
        {

            if (CategoriaSeleccionada.id > 0)//Si existe
            {
                if (!MA_CategoriaService.enviarCambios())
                {
                    MessageBox.Show("No se pudo actualizar la categoría");
                }
                else
                {
                    MessageBox.Show("La categoría fue guardado con éxito");
                }
            }
            else
            {
                if (CategoriaSeleccionada.nombre == null)
                {
                    MessageBox.Show("Ingresar el nombre de la categoría");
                }
                else if(CategoriaSeleccionada.descripcion==null){
                    MessageBox.Show("Ingresar la descripción de la categoría");
                }
                else if (CategoriaSeleccionada.nombre == null && CategoriaSeleccionada.descripcion == null) {
                    MessageBox.Show("Ingresar todos los campos");
                }
                else
                {
                    if (!MA_CategoriaService.insertarCategoria(CategoriaSeleccionada))
                    {
                        MessageBox.Show("No se pudo agregar la nuevo categoría");
                    }
                    else
                    {
                        MessageBox.Show("La categoría fue agregado con éxito");

                    }
                }
            }
            NotifyPropertyChanged("categoriaPrincipal");
        }
        
        #endregion
    }
}
