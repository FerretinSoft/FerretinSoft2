using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.viewmodel.MCompras;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;

namespace pe.edu.pucp.ferretin.view.MCompras
{
    /// <summary>
    /// Interaction logic for MC_BuscarProductosProveedorWindow.xaml
    /// </summary>
    public partial class MC_BuscarProductosProveedorWindow : Window
    {
        public MC_BuscarProductosProveedorWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (this.Owner != null)
            {
                try
                {
                    MC_BuscarProductosProveedorViewModel miViewModel = this.main.DataContext as MC_BuscarProductosProveedorViewModel;
                    MC_AdministrarOCCotizacionWindow padre = this.Owner as MC_AdministrarOCCotizacionWindow;
                    MC_CotizacionesOCViewModel padreViewModel = padre.main.DataContext as MC_CotizacionesOCViewModel;
                    IEnumerable<ProveedorProducto> listaPPFinal = miViewModel.listaProductosProveedorFinal;

                    if (listaPPFinal != null)
                    {
                        List<ProveedorProducto> listAux = listaPPFinal.ToList();
                        int cont = listAux.Count();
                        for (int i = 0; i < cont; i++)
                        {
                            var linea = new DocumentoCompraProducto() { 
                                Producto = listAux[i].Producto,
                                UnidadMedida = listAux[i].UnidadMedida,
                                precioUnit = listAux[i].precio,
                            };
                            listAux[i].isSelected = false;
                            padreViewModel.documentoCompra.DocumentoCompraProducto.Add(linea);
                        }
                    }
                    padreViewModel.actualizar();
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
    }
}
