using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.ComponentModel;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.controller;
using System.Threading;
using System.Windows.Threading;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.viewmodel;
using pe.edu.pucp.ferretin.viewmodel.MAlmacen;

namespace pe.edu.pucp.ferretin.view.MAlmacen
{
    /// <summary>
    /// Lógica de interacción para MA_MovimientosWindow.xaml
    /// </summary>
    public partial class MA_MovimientosWindow : Window
    {
        MA_MovimientosViewModel movViewModel = new MA_MovimientosViewModel();
        public MA_MovimientosWindow()
        {
            InitializeComponent();
            Thread thread = new Thread(
              new ThreadStart(
                delegate()
                {
                      busquedaMovGrid.Dispatcher.Invoke(
                      DispatcherPriority.Normal,
                      new Action(
                        delegate()
                        {
                            busquedaMovGrid.ItemsSource = MA_MovimientosService.ListaMovimientos;
                        }
                    ));
                }
            ));
            thread.Start();
            movimientosTabControl.DataContext = movViewModel;
        }

        

        private IEnumerable<Movimiento> ListaMovimientos()
        {
            return MA_MovimientosService.ListaMovimientos;
        }

        private void nuevoMvimientoBtn_Click(object sender, RoutedEventArgs e)
        {
            movimientosTabControl.SelectedIndex = 1;
            detallesTab.Header = "Nuevo Movimiento";

            //CleanForm();
            //movViewModel.statusTab = (int)MA_MovimientosViewModel.tabs.AGREGAR;
        }

        private void CleanForm()
        {
            codMovimientoTxt.Text = "";
            tipoMovimientoCombo.SelectedIndex = -1; //listado de tipos de movimiento
            tipoMovimientoCombo.IsEnabled = true;
            fechaMovDate.SelectedDate = System.DateTime.Today;
            fechaMovDate.IsEnabled = true;
            tiendaDesdeCombo.SelectedIndex = -1; //lista de tiendas
            tiendaDesdeCombo.IsEnabled = true;
            tiendaHastaCombo.SelectedIndex = -1; //lista de tiendas
            tiendaDesdeCombo.IsEnabled = true;
            noDocumentoTxt.Text = "";
            noDocumentoTxt.IsEnabled = true;
            estadoCombo.SelectedIndex = -1; //lista de estados de movimientos
            estadoCombo.IsEnabled = true;
            
        }


        private void editarMovimientoBtn_Click(object sender, RoutedEventArgs e)
        {
            if (busquedaMovGrid.SelectedItem != null)
            {
                movViewModel.movimiento = (Movimiento)busquedaMovGrid.SelectedItem;
                movimientosTabControl.SelectedIndex = 1;
                detallesTab.Header = "Detalle";

                
                //movViewModel.selectedTipoMovimiento = movViewModel.tiposMovimiento.FirstOrDefault(tipo => tipo.Equals(movViewModel.movimiento.MovimientoTipo));

                //FillForm();
                //movViewModel.statusTab = (int)MA_MovimientosViewModel.tabs.MODIFICAR;//Modificar
            }
        }

        private void FillForm()
        {
            /*
            codMovimientoTxt.Text = movViewModel.movimiento.codigo;
            codMovimientoTxt.IsEnabled = false;
            
            MovimientoTipo tipo = movViewModel.movimiento.MovimientoTipo;
            if (tipo != null)
            {
                for (int i = 0; i < movViewModel.tiposMovimiento.Count(); i++)
                {
                    if (tipo.id == movViewModel.tiposMovimiento.ElementAt(i).id)
                    {
                        tipoMovimientoCombo.SelectedIndex = i;
                        break;
                    }
                }
            }
            tipoMovimientoCombo.IsEnabled = false;
            
            fechaMovDate.SelectedDate = movViewModel.movimiento.fecha;
            fechaMovDate.IsEnabled = false;
            
            Almacen tiendaDesde = movViewModel.movimiento.Almacen;
            if (tiendaDesde != null)
            {
                for (int i = 0; i < movViewModel.tiendas.Count(); i++)
                {
                    if (tiendaDesde.id == movViewModel.tiendas.ElementAt(i).id)
                    {
                        tiendaDesdeCombo.SelectedIndex = i;
                        break;
                    }
                }
            }
            tiendaDesdeCombo.IsEnabled = false;
            
            Almacen tiendaHasta = movViewModel.movimiento.Almacen;
            if (tiendaHasta != null)
            {
                for (int i = 0; i < movViewModel.tiendas.Count(); i++)
                {
                    if (tiendaHasta.id == movViewModel.tiposMovimiento.ElementAt(i).id)
                    {
                        tiendaHastaCombo.SelectedIndex = i;
                        break;
                    }
                }
            }
            tiendaHastaCombo.IsEnabled = false;

            noDocumentoTxt.Text = "";

            Movimiento_Estado estado = movViewModel.movimiento.Movimiento_Estado; //lista de estados de movimientos
            if (estado != null)
            {
                for (int i = 0; i < movViewModel.estadosMovimiento.Count(); i++)
                {
                    if (estado.id == movViewModel.estadosMovimiento.ElementAt(i).id)
                    {
                        estadoCombo.SelectedIndex = i;
                        break;
                    }
                }
            }
            estadoCombo.IsEnabled = true;

            comentarioTxt.Text = movViewModel.movimiento.comentario;
             * */
        }

        private void cancelarMovimientoBtn_Click(object sender, RoutedEventArgs e)
        {
            movViewModel.statusTab = (int)MA_MovimientosViewModel.Tab.BUSQUEDA;
        }

        private void buscarMovimientoBtn_Click(object sender, RoutedEventArgs e)
        {
            /*
            Dictionary<String, Object> parametros = new Dictionary<string, object>();
            int tiendaId = movViewModel.tiendas.ElementAt(movViewModel.selectedTienda).id;
            if (movViewModel.selectedTienda>= 0) parametros.Add("tienda", tiendaId);
            if (movViewModel.searchNombreProducto != null) parametros.Add("nombreProducto", movViewModel.searchNombreProducto);
            if (movViewModel.searchFechaDesde != null) parametros.Add("fechaDesde", movViewModel.searchFechaDesde);
            if (movViewModel.searchFechaHasta != null) parametros.Add("fechaHasta", movViewModel.searchFechaHasta);

            busquedaMovGrid.ItemsSource = MA_MovimientosService.ObtenerListaMovimientos(parametros);
             */
        }  

        private void guardarMovimientoBtn_Click(object sender, RoutedEventArgs e)
        {//Puede ser nuevo o modificar
            //Movimiento movimiento;
            //if(movViewModel.movimiento.id > 0) 
            if (movViewModel.movimiento.id > 0)
            {
                MA_MovimientosService.ActualizarMovimiento(movViewModel.movimiento);
                
            }
            else
            {
                MA_MovimientosService.InsertarMovimiento(movViewModel.movimiento);
            }
            movViewModel.statusTab = (int)MA_MovimientosViewModel.Tab.BUSQUEDA;
        }

        
        

        

        

        

        

        
    }
}
