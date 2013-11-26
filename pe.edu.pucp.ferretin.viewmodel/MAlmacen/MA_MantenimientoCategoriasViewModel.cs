using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller;
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
        public Categoria categoriaPadre
        {
            get
            {
                //Devolver la categoría padre
                return MA_CategoriaService.categoriaPadre;
            }

        }
    
  
        /************************************************/
        private void copiarCategoria(Categoria categoria)
        {
            //perfilMenu.Perfil = perfil;
            for (int i = 0; i < categoria.Categoria2.Count; i++)
            {
                Categoria hijoCategoria = new Categoria();
                categoriaPrincipal.ElementAt(i).Categoria2.Add(hijoCategoria);
                copiarCategoria(categoria.Categoria2.ElementAt(i));
            }
        }
        /************************************************/
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
                NotifyPropertyChanged("categoriaPrincipal");
            }
        }

        
        private Categoria _categoria = null;
        public Categoria categoria
        {

            get
            {

                return _categoria;
            }
            set
            {


                _categoria = value;
                NotifyPropertyChanged("categoria");
            }
        }
        
        private Categoria _CategoriaSeleccionada=null;
        
        public Categoria CategoriaSeleccionada
        {

            get
            {

                return _CategoriaSeleccionada;
            }
            set
            {
                if (value == null) { _CategoriaSeleccionada.id = 0; _CategoriaSeleccionada.nombre = ""; _CategoriaSeleccionada.descripcion = ""; }
                else
                {
                    _CategoriaSeleccionada = value;
                
                //Actualizo el combobox de categorias padre
                    try
                    {
                        categoriasPadre = MA_CategoriaService.categorias.Where(c => c.nivel == _CategoriaSeleccionada.Categoria1.nivel);
                    }
                    catch (Exception ex) { 
                             
                    }
               }
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

        /*RelayCommand _saveCategoriaPrincipalCommand;
        public ICommand saveCategoriaPrincipalCommand
        {
            get
            {
                if (_saveCategoriaPrincipalCommand == null)
                {
                    _saveCategoriaPrincipalCommand = new RelayCommand(saveCategoriaPrincipal);
                }
                return _saveCategoriaPrincipalCommand;
            }
        }*/



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
                    _actualizarArbolCategoriaCommand = new RelayCommand(param => NotifyPropertyChanged("listaArbolCategoria"));
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


        private IEnumerable<Categoria> _listaArbolCategoria;
        public IEnumerable<Categoria> listaArbolCategoria
        {
            get
            {
                _listaArbolCategoria =MA_CategoriaService.categorias;
                return _listaArbolCategoria;
            }
            set
            {
                _listaArbolCategoria = value;
                NotifyPropertyChanged("listaArbolCategoria");
            
            }
        }


        #region Comandos

       

        public void nuevaCategoria(Object obj)
        {
            if (CategoriaSeleccionada != null)
            {
                Categoria categoria = new Categoria();
                categoria.nivel = (byte)(CategoriaSeleccionada.nivel + 1);
                categoria.Categoria1 = CategoriaSeleccionada;
                CategoriaSeleccionada = categoria;
            }
            else 
            {
                MessageBox.Show("Seleccionar una categoría");
            }
        }

        public void deleteCategoria(Object obj)
        {

            if (CategoriaSeleccionada != null)
            {
                int valor = MA_CategoriaService.eliminarCategoria(CategoriaSeleccionada);
                if (valor.Equals(0))
                {
                    MessageBox.Show("La Categoría esta asignada, no se pudo eliminar");
                }
                else
                {
                    MessageBox.Show("La Categoría se elimino con éxito");
                }
            }
            else {
                MessageBox.Show("Seleccinar un categoría");
            }

           
           
        }

        
        public void saveCategoriaPrincipal(Object obj) 
        {

          
                if (!MA_CategoriaService.insertarCategoria(categoria))
                {
                    MessageBox.Show("No se pudo agregar la nueva categoría");
                }
                else
                {
                    MessageBox.Show("La categoría fue agregado con éxito");

                }
            

        }

        public void saveCategoria(Object obj)
        {
            if (CategoriaSeleccionada != null)
            {
                ComunService.idVentana(20);
                if (CategoriaSeleccionada.id > 0)//Actualizar categoria
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
                    ComunService.idVentana(19);
                    //insertar categoria nueva
                    if (CategoriaSeleccionada.nombre == null && CategoriaSeleccionada.descripcion != null)
                    {
                        MessageBox.Show("Ingresar el nombre de la categoría");
                    }
                    else if (CategoriaSeleccionada.nombre == null && CategoriaSeleccionada.descripcion == null)
                    {
                        MessageBox.Show("Ingresar todos los campos");
                    }
                    else
                    {
                        int esSub = 0;
                        /************Cambios**************/
                        /*esSub= (from cs in categoriaPrincipal
                                   where
                                   CategoriaSeleccionada.id_padre==cs.id
                                   select cs).Count();*/
                        if (CategoriaSeleccionada.nivel==2)
                        {
                            //Console.WriteLine("assaas", CategoriaSeleccionada.nivel);

                            if (!MA_CategoriaService.insertarCategoria(CategoriaSeleccionada))

                                MessageBox.Show("No se pudo agregar la nuevo categoría");

                            else

                                MessageBox.Show("La categoría fue agregado con éxito");



                        }
                        else { MessageBox.Show("No se puede asignar una categoria hijo a una subCategoría"); }
                    }

                }
            }
            else MessageBox.Show("Seleccionar una Categoria principal");
             NotifyPropertyChanged("categoriaPrincipal");

            
            
        }
        
        #endregion
    }
}
