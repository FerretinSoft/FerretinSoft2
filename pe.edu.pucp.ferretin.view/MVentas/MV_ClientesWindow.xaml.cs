using pe.edu.pucp.ferretin.controller;
using pe.edu.pucp.ferretin.controller.MVentas;
using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.MVentas;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
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
using System.Windows.Threading;

namespace pe.edu.pucp.ferretin.view.MVentas
{
    /// <summary>
    /// Lógica de interacción para MV_ClientesWindow.xaml
    /// </summary>
    public partial class MV_ClientesWindow : Window
    {

        public MV_ClientesWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Owner != null)//O sea que proviene de un padre
            {
                try
                {

                    MV_ClientesViewModel my_DataContext = this.DataContext as MV_ClientesViewModel;
                    
                    if (Owner is MV_RegistrarVentaWindow)
                    {
                        MV_RegistrarVentaWindow padre = Owner as MV_RegistrarVentaWindow;
                        if (my_DataContext.soloSeleccionarCliente)
                        {
                            MV_RegistrarVentaViewModel padre_DataContext = padre.DataContext as MV_RegistrarVentaViewModel;
                            padre_DataContext.nroDocSeleccionado = my_DataContext.cliente.nroDoc;
                            this.Close();
                        }
                    }
                    else if (Owner is MV_DevolucionesWindow)
                    {
                        MV_DevolucionesWindow padre = this.Owner as MV_DevolucionesWindow;
                        MV_DevolucionesViewModel padre_DataContext = padre.main.DataContext as MV_DevolucionesViewModel;
                        padre_DataContext.searchNroDocCliente = my_DataContext.cliente.nroDoc;
                        padre_DataContext.searchnombreCliente = my_DataContext.cliente.nombreCompleto;
                        this.Close();
                    }
                    else if (Owner is MV_AdministrarNotaCreditoWindow)
                    {
                        MV_AdministrarNotaCreditoWindow padre = this.Owner as MV_AdministrarNotaCreditoWindow;
                        MV_NotaCreditoViewModel padre_DataContext = padre.main.DataContext as MV_NotaCreditoViewModel;
                        padre_DataContext.searchNroDocCliente = my_DataContext.cliente.nroDoc;
                        padre_DataContext.nombreCliente = my_DataContext.cliente.nombreCompleto;
                        this.Close();
                    }
                    else if (Owner is MV_AdministrarVentasWindow)
                    {
                        MV_AdministrarVentasWindow padre = this.Owner as MV_AdministrarVentasWindow;
                        MV_VentasViewModel padre_DataContext = padre.main.DataContext as MV_VentasViewModel;
                        padre_DataContext.searchNroDocCliente = my_DataContext.cliente.nroDoc;
                        padre_DataContext.nombreCliente = my_DataContext.cliente.nombreCompleto;
                        this.Close();
                    }
                    else if (Owner is MV_AdministrarValesWindow)
                    {
                        MV_AdministrarValesWindow padre = this.Owner as MV_AdministrarValesWindow;
                        MV_ValesViewModel padre_DataContext = padre.main.DataContext as MV_ValesViewModel;
                        if (padre_DataContext.selectedTab == 0)
                        {
                            padre_DataContext.searchNroDocCliente = my_DataContext.cliente.nroDoc;
                            padre_DataContext.nombreCliente = my_DataContext.cliente.nombreCompleto;
                        }
                        if (padre_DataContext.selectedTab == 1)
                            padre_DataContext.loteVale.Cliente = my_DataContext.cliente;
                        this.Close();
                    }
                    else if (Owner is MV_AdministrarProformasWindow)
                    {
                        MV_AdministrarProformasWindow padre = this.Owner as MV_AdministrarProformasWindow;
                        MV_AdministrarProformasViewModel padre_DataContext = padre.DataContext as MV_AdministrarProformasViewModel;
                        if (clienteSearch!=null && clienteSearch.Value==true)
                        {
                            padre_DataContext.clienteSearch = my_DataContext.cliente;
                        }
                        else
                        {
                            padre_DataContext.nroDocSeleccionado = my_DataContext.cliente.nroDoc;
                        }
                        this.Close();
                    }

                    
                }
                catch
                {
                }
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }

        public bool? clienteSearch { get; set; }
    }

}
