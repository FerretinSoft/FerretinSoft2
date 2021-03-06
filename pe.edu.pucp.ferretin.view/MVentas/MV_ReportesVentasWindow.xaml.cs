﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.view.MRecursosHumanos;
using pe.edu.pucp.ferretin.viewmodel.MRecursosHumanos;
using pe.edu.pucp.ferretin.viewmodel.MVentas;

namespace pe.edu.pucp.ferretin.view.MVentas
{
    /// <summary>
    /// Lógica de interacción para MV_ReportesVentasWindow.xaml
    /// </summary>
    public partial class MV_ReportesVentasWindow : Window
    {
        int estado = 0; //variable que representa el estado en que se encuentra la elaboración del reporte:
        // estado=0 > intro; estado=1 > configuración; estado=2 > fin

        public MV_ReportesVentasWindow()
        {
            InitializeComponent();
        }
        private void repSigBtn_Click(object sender, RoutedEventArgs e)
        {
           
            MV_ReportesViewModel padre_DataContext = this.main.DataContext as MV_ReportesViewModel;
            if (padre_DataContext.nombreBoton.Equals("SIGUIENTE")){
               
                switch (estado)
                {
                    case 0:
                        if (this.listaRepDisp.SelectedItem != null)
                        {
                            repIntroGrid.Visibility = System.Windows.Visibility.Collapsed;
                            repConfGrid.Visibility = System.Windows.Visibility.Visible;

                            padre_DataContext.nombreBoton = "SIGUIENTE";
                            padre_DataContext.searchFechaFin = DateTime.Today.AddDays(1);
                            padre_DataContext.searchFechaInicio = DateTime.Parse("10/09/2013");

                            if (this.listaRepDisp.SelectedIndex == 2)
                            {
                                padre_DataContext.visibleProducto = System.Windows.Visibility.Collapsed;
                                padre_DataContext.visibleVendedor = System.Windows.Visibility.Collapsed;
                                padre_DataContext.visibleCliente = System.Windows.Visibility.Collapsed;
                                padre_DataContext.visibleTienda = System.Windows.Visibility.Visible;
                                padre_DataContext.nombreVentana = "Reporte por tienda";
                                padre_DataContext.aliasRep = "Reporte de ventas por tienda";
                                padre_DataContext.comentRep = "El presente reporte tiene como finalidad mostrar un resumen de las ventas realizadas por tienda en un período de tiempo";
                            }
                            if (this.listaRepDisp.SelectedIndex == 1)
                            {
                                padre_DataContext.visibleProducto = System.Windows.Visibility.Visible;
                                padre_DataContext.visibleVendedor = System.Windows.Visibility.Collapsed;
                                padre_DataContext.visibleCliente = System.Windows.Visibility.Collapsed;
                                padre_DataContext.visibleTienda = System.Windows.Visibility.Visible;
                                padre_DataContext.nombreVentana = "Reporte por productos";
                                padre_DataContext.aliasRep = "Reporte de ventas por producto";
                                padre_DataContext.comentRep = "El presente reporte tiene como finalidad mostrar un resumen de las ventas realizadas por producto en un período de tiempo";
                            }

                            if (this.listaRepDisp.SelectedIndex == 0)
                            {
                                padre_DataContext.visibleProducto = System.Windows.Visibility.Collapsed;
                                padre_DataContext.visibleVendedor = System.Windows.Visibility.Collapsed;
                                padre_DataContext.visibleCliente = System.Windows.Visibility.Visible;
                                padre_DataContext.visibleTienda = System.Windows.Visibility.Collapsed;
                                padre_DataContext.nombreVentana = "Reporte por cliente";
                                padre_DataContext.aliasRep = "Reporte de ventas por cliente";
                                padre_DataContext.comentRep = "El presente reporte tiene como finalidad mostrar un resumen de las ventas realizadas por cliente en un período de tiempo";
                            }

                            if (this.listaRepDisp.SelectedIndex == 3)
                            {
                                padre_DataContext.visibleProducto = System.Windows.Visibility.Collapsed;
                                padre_DataContext.visibleCliente = System.Windows.Visibility.Collapsed;
                                padre_DataContext.visibleVendedor = System.Windows.Visibility.Visible;                      
                                padre_DataContext.visibleTienda = System.Windows.Visibility.Collapsed;
                                padre_DataContext.nombreVentana = "Reporte por vendedor";
                                padre_DataContext.aliasRep = "Reporte de ventas por vendedor";
                                padre_DataContext.comentRep = "El presente reporte tiene como finalidad mostrar un resumen de las ventas realizadas por vendedor en un período de tiempo";
                            }

                            if (this.listaRepDisp.SelectedIndex == 4)
                            {
                                padre_DataContext.visibleProducto = System.Windows.Visibility.Collapsed;
                                padre_DataContext.visibleCliente = System.Windows.Visibility.Collapsed;
                                padre_DataContext.visibleVendedor = System.Windows.Visibility.Collapsed;
                                padre_DataContext.visibleTienda = System.Windows.Visibility.Visible;
                                padre_DataContext.nombreVentana = "Reporte de devoluciones";
                                padre_DataContext.aliasRep = "Reporte de devoluciones";
                                padre_DataContext.comentRep = "El presente reporte tiene como finalidad mostrar un resumen de las devoluciones realizadas en un período de tiempo";
                            }

                            if (this.listaRepDisp.SelectedIndex == 5)
                            {
                                padre_DataContext.visibleProducto = System.Windows.Visibility.Collapsed;
                                padre_DataContext.visibleCliente = System.Windows.Visibility.Visible;
                                padre_DataContext.visibleVendedor = System.Windows.Visibility.Collapsed;
                                padre_DataContext.visibleTienda = System.Windows.Visibility.Visible;
                                padre_DataContext.nombreVentana = "Reporte de servicios";
                                padre_DataContext.aliasRep = "Reporte de servicios";
                                padre_DataContext.comentRep = "El presente reporte tiene como finalidad mostrar un resumen de los servicios brindados por la empresa en un período de tiempo";
                            }

                            estado = 1;
                            repAntBtn.IsEnabled = true;
                        }
                        else
                            MessageBox.Show("Debe seleccionar un reporte", "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                        break;
                    case 1:
                        repConfGrid.Visibility = System.Windows.Visibility.Collapsed;
                        repFinalGrid.Visibility = System.Windows.Visibility.Visible;
                        estado = 2;
                        repSigBtn.IsEnabled = false;
                        break;
                }
            }else{
                
                

            }


            }
        private void repShow_Click(object sender, RoutedEventArgs e)
        {
            MV_ReportesViewModel padre_DataContext = this.main.DataContext as MV_ReportesViewModel;
            MV_VisorReporte rep;
            if (this.listaRepDisp.SelectedIndex == 2)
                rep = new MV_VisorReporte(padre_DataContext.searchFechaInicio, padre_DataContext.searchFechaFin, padre_DataContext.selectedTienda.id, "RTienda", "", "", "");
            else if (this.listaRepDisp.SelectedIndex == 1)
                rep = new MV_VisorReporte(padre_DataContext.searchFechaInicio, padre_DataContext.searchFechaFin, padre_DataContext.selectedTienda.id, "RProducto", "", "", padre_DataContext.searchProducto);
            else if (this.listaRepDisp.SelectedIndex == 0)
                rep = new MV_VisorReporte(padre_DataContext.searchFechaInicio, padre_DataContext.searchFechaFin, 0, "RCliente", "", padre_DataContext.searchCliente, "");
            else if (this.listaRepDisp.SelectedIndex == 4)
                rep = new MV_VisorReporte(padre_DataContext.searchFechaInicio, padre_DataContext.searchFechaFin, padre_DataContext.selectedTienda.id, "RDevolucion", "", "", "");
            else if (this.listaRepDisp.SelectedIndex == 5)
                rep = new MV_VisorReporte(padre_DataContext.searchFechaInicio, padre_DataContext.searchFechaFin, padre_DataContext.selectedTienda.id, "RServicios", "", padre_DataContext.searchCliente, "");          
            else
                rep = new MV_VisorReporte(padre_DataContext.searchFechaInicio, padre_DataContext.searchFechaFin, 0, "RVendedor", padre_DataContext.searchVendedor, "", "");

            rep.Show();
        }


        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
           
                ComunService.Clean();
                    
        }


        private void repAntBtn_Click(object sender, RoutedEventArgs e)
        {
            MV_ReportesViewModel padre_DataContext = this.main.DataContext as MV_ReportesViewModel;
            
            switch (estado)
            {
                case 1:
                    repConfGrid.Visibility = System.Windows.Visibility.Collapsed;
                    repIntroGrid.Visibility = System.Windows.Visibility.Visible;
                    estado = 0;
                    repAntBtn.IsEnabled = false;
                    padre_DataContext.nombreBoton = "SIGUIENTE";
                    break;
                case 2:
                    repConfGrid.Visibility = System.Windows.Visibility.Visible;
                    repFinalGrid.Visibility = System.Windows.Visibility.Collapsed;
                    estado = 1;
                    repSigBtn.IsEnabled = true;
                    break;


            }
        }


        private void Button_Click_Mail(object sender, RoutedEventArgs e)
        {
            MV_ReportesViewModel padre_DataContext = this.main.DataContext as MV_ReportesViewModel;
            MV_DatosMailWindow v = new MV_DatosMailWindow();
            v.Owner = this;
            var viewModel = v.main.DataContext as MV_ReportesViewModel;
            viewModel.selectedReport = this.listaRepDisp.SelectedIndex;
            viewModel.searchFechaInicio = padre_DataContext.searchFechaInicio;
            viewModel.searchFechaFin = padre_DataContext.searchFechaFin;
            viewModel.searchProducto = padre_DataContext.searchProducto;
            viewModel.searchVendedor = padre_DataContext.searchVendedor;
            viewModel.selectedTienda = padre_DataContext.selectedTienda;
            viewModel.searchCliente = padre_DataContext.searchCliente;
            v.ShowDialog(); 

            }
 
        private void Button_Click_Vendedor(object sender, RoutedEventArgs e)
        {
            MR_AdministrarPersonalWindow v = new MR_AdministrarPersonalWindow();
            v.Owner = this;
            var viewModel = v.main.DataContext as MR_AdministrarPersonalViewModel;
            viewModel.soloSeleccionarVendedor = true;
            v.ShowDialog();
        }

        private void Button_Click_Cliente(object sender, RoutedEventArgs e)
        {
            MV_ClientesWindow v = new MV_ClientesWindow();
            v.Owner = this;
            var viewModel = v.DataContext as MV_ClientesViewModel;
            viewModel.soloSeleccionarCliente = true;
            v.ShowDialog();  
        }

        private void Button_Click_Producto(object sender, RoutedEventArgs e)
        {
             
            var vm = main.DataContext as MV_ReportesViewModel;
            var buscador = new MV_BuscarProdReporte(this);
        
        }

        private void validarCodCliente(object sender, TextCompositionEventArgs e)
        {
            if (Regex.IsMatch(e.Text, "[0-9]"))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        private void validarCodCliente_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Space)
                e.Handled = false;
            else
                e.Handled = true;
        }



    }
}
