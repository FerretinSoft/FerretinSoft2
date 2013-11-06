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
                    MV_RegistrarVentaWindow padre = this.Owner as MV_RegistrarVentaWindow;
                    MV_ClientesViewModel my_DataContext = this.main.DataContext as MV_ClientesViewModel;
                    MV_RegistrarVentaViewModel padre_DataContext = padre.main.DataContext as MV_RegistrarVentaViewModel;

                    padre_DataContext.nroDocSeleccionado = my_DataContext.cliente.nroDoc;
                    this.Close();
                }
                catch
                {
                    try
                    {
                        MV_DevolucionesWindow padre = this.Owner as MV_DevolucionesWindow;
                        MV_ClientesViewModel my_DataContext = this.main.DataContext as MV_ClientesViewModel;
                        MV_DevolucionesViewModel padre_DataContext = padre.main.DataContext as MV_DevolucionesViewModel;

                        padre_DataContext.searchNroDocCliente = my_DataContext.cliente.nroDoc;
                        padre_DataContext.searchnombreCliente = my_DataContext.cliente.nombreCompleto;
                        this.Close();
                    }

                    catch
                    {
                        try
                        {
                            MV_AdministrarNotaCreditoWindow padre = this.Owner as MV_AdministrarNotaCreditoWindow;
                            MV_ClientesViewModel my_DataContext = this.main.DataContext as MV_ClientesViewModel;
                            MV_NotaCreditoViewModel padre_DataContext = padre.main.DataContext as MV_NotaCreditoViewModel;

                            padre_DataContext.searchNroDocCliente = my_DataContext.cliente.nroDoc;
                            padre_DataContext.nombreCliente = my_DataContext.cliente.nombreCompleto;
                            this.Close();
                        }

                        catch
                        {
                            try
                            {
                                MV_AdministrarVentasWindow padre = this.Owner as MV_AdministrarVentasWindow;
                                MV_ClientesViewModel my_DataContext = this.main.DataContext as MV_ClientesViewModel;
                                MV_VentasViewModel padre_DataContext = padre.main.DataContext as MV_VentasViewModel;
                                padre_DataContext.searchNroDocCliente = my_DataContext.cliente.nroDoc;
                                padre_DataContext.nombreCliente = my_DataContext.cliente.nombreCompleto;
                                this.Close();
                            }

                            catch
                            {
                                try
                                {
                                    MV_AdministrarValesWindow padre = this.Owner as MV_AdministrarValesWindow;
                                    MV_ClientesViewModel my_DataContext = this.main.DataContext as MV_ClientesViewModel;
                                    MV_ValesViewModel padre_DataContext = padre.main.DataContext as MV_ValesViewModel;
                                    padre_DataContext.searchNroDocCliente = my_DataContext.cliente.nroDoc;
                                    padre_DataContext.nombreCliente = my_DataContext.cliente.nombreCompleto;
                                    this.Close();
                                }
                                catch { }
                            }
                        }

                    }
                }
            }
        }
    }

}
