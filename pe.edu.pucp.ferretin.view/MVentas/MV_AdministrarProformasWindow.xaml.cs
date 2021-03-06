﻿using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.controller.MSeguridad;
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.view.MRecursosHumanos;
using pe.edu.pucp.ferretin.viewmodel.MRecursosHumanos;
using pe.edu.pucp.ferretin.viewmodel.MVentas;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Xps;
using System.Windows.Xps.Packaging;

namespace pe.edu.pucp.ferretin.view.MVentas
{
    /// <summary>
    /// Lógica de interacción para MV_AdministrarProformasWindow.xaml
    /// </summary>
    public partial class MV_AdministrarProformasWindow : Window
    {
        public MV_AdministrarProformasWindow()
        {
            InitializeComponent();
        }

        private void detalleProformaBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void buscarClienteBtn_Click(object sender, RoutedEventArgs e)
        {
            //ClientesWindow pw = new ClientesWindow();
            //pw.ShowDialog();
        }

        private void buscarVendedorBtn_Click(object sender, RoutedEventArgs e)
        {
            //PersonalAdminWindow pw = new PersonalAdminWindow();
            //pw.ShowDialog();
        }

        private void nuevaProformaBtn_Click(object sender, RoutedEventArgs e)
        {
            
        }

        private void registrarProformaBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void cancelarBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void buscarProformaBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            //Buscar cliente

            MV_ClientesWindow v = new MV_ClientesWindow();
            v.Owner = this;
            var viewModel = v.DataContext as MV_ClientesViewModel;
            viewModel.soloSeleccionarCliente = true;
            v.ShowDialog();
        }

        private void registrarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (this.Owner!=null)//Si tiene un padre
            {

                MV_AdministrarProformasViewModel miVM = this.DataContext as MV_AdministrarProformasViewModel;

                if(Owner is MV_RegistrarVentaWindow)
                {
                    var padre = this.Owner as MV_RegistrarVentaWindow;
                    var padreViewModel = padre.DataContext as MV_RegistrarVentaViewModel;

                    if (miVM.proforma != null)
                    {
                        padreViewModel.venta.VentaProducto.Clear();
                        foreach (var vp in miVM.proforma.ProformaProducto)
                        {
                            var productoAlmacen = MA_ProductoAlmacenService.ObtenerProductoAlmacenPorTiendaProducto(padreViewModel.venta.Tienda, vp.Producto);
                            var _stockDisponible = (int)productoAlmacen.stock;

                            padreViewModel.venta.Cliente = vp.Proforma.Cliente;
                            VentaProducto ventaProducto = new VentaProducto()
                            {
                                vieneDeProforma = true,
                                canjeado = false,
                                cantidad = vp.cantidad,
                                descuento = vp.descuento,
                                descuentoPorcentaje = vp.descuentoPorcentaje,
                                moneda = vp.moneda,
                                montoParcial = vp.montoParcial,
                                montoReal = vp.montoReal,
                                precioPuntos = vp.precioPuntos,
                                precioPuntosParcial = vp.precioPuntosParcial,
                                precioUnitario = vp.preciounitario,
                                prodConDesc = vp.prodConDesc,
                                Producto = vp.Producto,
                                PromocionActual = null,
                                puntosCanejado = 0,
                                puntosGanado = vp.puntosGanado,
                                stockDisponible = _stockDisponible,
                                stockRestante = _stockDisponible - vp.cantidad,
                                tipoCambio = vp.tipoCambio,
                                Venta = padreViewModel.venta,
                            };
                            padreViewModel.venta.VentaProducto.Add(ventaProducto);
                            
                            padreViewModel.actualizarMontosVenta(null, null);
                            padreViewModel.NotifyPropertyChanged("venta");
                        }
                        padreViewModel.venta.Proforma = miVM.proforma;
                       
                    }
                    this.Close();
                }
                else if (Owner is MV_AdministrarProformasWindow)
                {

                    var padre = this.Owner as MV_AdministrarProformasWindow;
                    var padreViewModel = padre.DataContext as MV_AdministrarProformasViewModel;
                    if (miVM.proforma != null)
                    {

                        padreViewModel.proforma.ProformaProducto.Clear();
                        foreach (var vp in miVM.proforma.ProformaProducto)
                        {
                            padreViewModel.codProdAgregar = vp.Producto.codigo;
                            padreViewModel.agregarProducto(null);
                        }
                    }
                    this.Close();
                }
            }
        }

        private void buscarProformaBtn_Click_1(object sender, RoutedEventArgs e)
        {
            MV_AdministrarProformasWindow profWindow = new MV_AdministrarProformasWindow();
            profWindow.Owner = this;
            MV_AdministrarProformasViewModel prodViewModel = profWindow.DataContext as MV_AdministrarProformasViewModel;
            prodViewModel.soloSeleccionarProforma = true;
            profWindow.ShowDialog();
        }

        private void buscarClienteBtn_Click_1(object sender, RoutedEventArgs e)
        {
            //Buscar Cliente
            MV_ClientesWindow v = new MV_ClientesWindow();
            v.Owner = this;
            v.clienteSearch = true;
            var viewModel = v.DataContext as MV_ClientesViewModel;
            viewModel.soloSeleccionarCliente = true;
            v.ShowDialog();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            var vm = DataContext as MV_AdministrarProformasViewModel;
            if (!vm.soloSeleccionarProforma)
            {
                ComunService.Clean();
            }
        }

        private void buscarVendedorBtn_Click_1(object sender, RoutedEventArgs e)
        {
            MR_AdministrarPersonalWindow v = new MR_AdministrarPersonalWindow();
            v.Owner = this;
            var viewModel = v.main.DataContext as MR_AdministrarPersonalViewModel;
            viewModel.soloSeleccionarVendedor = true;
            v.ShowDialog();
        }


        //Buscador de productos
        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MV_AdministrarProformasViewModel;
            var buscador = new MV_BuscadorProductos(this, vm.usuarioLogueado.Empleado.tiendaActual);

        }

        private void imprimirBtn_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MV_AdministrarProformasViewModel;
            if (vm.proforma.id <= 0)
            {
                vm.registrar(null);
            }
            if (vm.proforma.id > 0)
            {
                var print = new MV_DocProforma();
                var printVM = print.DataContext as MV_DocProformaViewModel;
                printVM.proforma = vm.proforma;
                print.imprimir();
            }
        }

        private void enviarEmailBtn_Click(object sender, RoutedEventArgs e)
        {
            var vm = DataContext as MV_AdministrarProformasViewModel;
            if (vm.proforma.id <= 0)
            {
                vm.registrar(null);
            }
            if (vm.proforma.id > 0)
            {
                if (String.IsNullOrEmpty(vm.proforma.destinatario))
                {
                    MessageBox.Show("Debe ingresar el email de un destinatario");
                    return;
                }
                MailAddress m;
                try
                {
                    m = new MailAddress(vm.proforma.destinatario);
                }
                catch
                {
                    MessageBox.Show("El email ingresado no es válido");
                    return;
                }

                var print = new MV_DocProforma();
                print.Owner = this;
                var printVM = print.DataContext as MV_DocProformaViewModel;
                printVM.proforma = vm.proforma;
                print.Show();
                print.enviarEmail();
                
            }
        }

    }
}
