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
using pe.edu.pucp.ferretin.controller.MAlmacen;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.view.MAlmacen
{
    /// <summary>
    /// Lógica de interacción para MA_ReporteKardexWindow.xaml
    /// </summary>
    public partial class MA_ReporteKardexWindow : Window
    {
        List<Movimiento> entradas;
        List<Movimiento> salidas;

        public MA_ReporteKardexWindow()
        {
            InitializeComponent();
        }

        public MA_ReporteKardexWindow(Tienda almacen, DateTime fechaDesde, DateTime fechaHasta)
        {
            fechaReporteLabel.Content = DateTime.Today.ToShortDateString();
            almacenLabel.Content = almacen.nombre;
            direccionLabel.Content = almacen.direccion;
            telefonoLabel.Content = almacen.telefono1 + " / " + almacen.telefono2;
            fechaDesdeLabel.Content = fechaDesde.ToShortDateString();
            fechaHastaLabel.Content = fechaHasta.ToShortDateString();

            entradas = MA_MovimientosService.buscarEntradasPorTiendaPeriodo(almacen, fechaDesde, fechaHasta).ToList();
            salidas = MA_MovimientosService.buscarSalidasPorTiendaPeriodo(almacen, fechaDesde, fechaHasta).ToList();
            InitializeComponent();
        }
    }
}
