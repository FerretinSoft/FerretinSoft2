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

                    //IEnumerable<ProveedorProducto>  mio = miViewModel.listaProductosProveedorFinal;
                    
                    var sequence = new List<DocumentoCompraProducto>();
                    if (miViewModel.listaProductosProveedorFinal != null)
                    {
                        List<ProveedorProducto> listAux = miViewModel.listaProductosProveedorFinal.ToList();
                        for (int i = 0; i < listAux.Count(); i++)
                        {
                            if (listAux[i].isSelected)
                            {
                                var linea = new DocumentoCompraProducto() { 
                                    Producto = listAux[i].Producto,
                                    UnidadMedida = listAux[i].UnidadMedida,
                                    precioUnit = listAux[i].precio,
                                    id_unidad_medida = listAux[i].id_unidad
                                };
                                sequence.Add(linea);
                            }
                        }
                        padreViewModel.listaProductosDC = sequence;
                    }
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
