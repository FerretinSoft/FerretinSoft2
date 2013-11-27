using pe.edu.pucp.ferretin.model;
using pe.edu.pucp.ferretin.view.MVentas;
using pe.edu.pucp.ferretin.viewmodel.MAlmacen;
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

namespace pe.edu.pucp.ferretin.view.MAlmacen
{
    /// <summary>
    /// Lógica de interacción para MA_ReportesWindow.xaml
    /// </summary>
    public partial class MA_ReportesWindow : Window
    {
        int estado = 0; //variable que representa el estado en que se encuentra la elaboración del reporte:
        // estado=0 > intro; estado=1 > configuración; estado=2 > fin

        int reporte = 0; 
        /*
         * 1 - Reporte de Stock
         * 2 - Reporte Kardex
         * 3 - Reporte Kardex diario
         * 4 - Reporte Kardex mensual
         * */

        public MA_ReportesWindow()
        {
            InitializeComponent();
        }

        private void repSigBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (estado)
            {
                case 0:
                    if (reporte > 0)
                    {
                        repIntroGrid.Visibility = System.Windows.Visibility.Collapsed;
                        setConfValues();
                        repConfGrid.Visibility = System.Windows.Visibility.Visible;
                        estado = 1;
                        repAntBtn.IsEnabled = true;
                    }
                    else
                        MessageBox.Show("Debe seleccionar algún reporte");
                    break;
                case 1:
                    if (cmbTienda.SelectedItem == null)
                        MessageBox.Show("Debe seleccionar una tienda / almacén para generar su reporte.");
                    else if (fechaDesdePicker.SelectedDate == null || fechaHastaPicker.SelectedDate == null)
                        MessageBox.Show("Debe seleccionar un período para generar su reporte.");
                    else
                    {
                        repConfGrid.Visibility = System.Windows.Visibility.Collapsed;
                        repFinalGrid.Visibility = System.Windows.Visibility.Visible;
                        estado = 2;
                        repSigBtn.IsEnabled = false;
                    }
                    break;
            }
        }

        private void setConfValues()
        {
            switch (reporte)
            {
                case 1: // reporte de stock
                    fechaDesdePicker.IsEnabled = false;
                    fechaHastaPicker.IsEnabled = false;
                    aliasText.Text = "";
                    comentarioText.Text = "Reporte de stock de productos por tienda a la fecha.";
                    break;
                case 2: // reporte kardex
                    fechaDesdePicker.IsEnabled = true;
                    fechaDesdePicker.SelectedDate = DateTime.Today;
                    fechaHastaPicker.IsEnabled = true;
                    fechaHastaPicker.SelectedDate = DateTime.Today;
                    aliasText.Text = "";
                    comentarioText.Text = "Reporte kardex de movimientos de almacén en el período a seleccionar.";
                    break;
                case 3: // reporte kardex diario
                    fechaDesdePicker.IsEnabled = true;
                    fechaDesdePicker.SelectedDate = DateTime.Today;
                    fechaHastaPicker.IsEnabled = true;
                    fechaHastaPicker.SelectedDate = DateTime.Today;
                    aliasText.Text = "Reporte kardex diario";
                    comentarioText.Text = "Reporte kardex de movimientos diarios de almacén.";
                    break;
                case 4: // reporte kardex mensual (mes en curso)
                    fechaDesdePicker.IsEnabled = true;
                    fechaDesdePicker.SelectedDate = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
                    fechaHastaPicker.IsEnabled = true;
                    fechaHastaPicker.SelectedDate = DateTime.Today;
                    aliasText.Text = "Reporte kardex mensual";
                    comentarioText.Text = "Reporte kardex de movimientos de almacén en el mes en curso.";
                    break;
            }
        }

        private void repAntBtn_Click(object sender, RoutedEventArgs e)
        {
            switch (estado)
            {
                case 1:
                    repConfGrid.Visibility = System.Windows.Visibility.Collapsed;
                    repIntroGrid.Visibility = System.Windows.Visibility.Visible;
                    estado = 0;
                    repAntBtn.IsEnabled = false;
                    break;
                case 2:
                    repFinalGrid.Visibility = System.Windows.Visibility.Collapsed;
                    repConfGrid.Visibility = System.Windows.Visibility.Visible;
                    estado = 1;
                    repSigBtn.IsEnabled = true;
                    break;


            }
        }

        private void btnViewReport_Click(object sender, RoutedEventArgs e)
        {
            if (reporte == 1) //Reporte de Stock
            {
                MA_VisorReporteStock vrs = new MA_VisorReporteStock(((Tienda)this.cmbTienda.SelectedItem).id);
                vrs.ShowDialog();

            }
            else if (reporte >= 2 && reporte <= 4)
            {
                MA_KardexVisor visor = new MA_KardexVisor((DateTime)fechaDesdePicker.SelectedDate,
                                                          (DateTime)fechaHastaPicker.SelectedDate,
                                                          (Tienda)this.cmbTienda.SelectedItem);
                visor.GenerarReporte();
                visor.ShowDialog();
            }
            
            
        }

        private void reportesBaseListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reportesFavoritosListBox.SelectedIndex = -1;
            switch (reportesBaseListBox.SelectedIndex)
            {
                case 0://reporte de stock de productos
                    reporte = 1;
                    //MessageBox.Show("Stock de productos");
                    break;
                case 1://reporte kardex
                    reporte = 2;    
                    //MessageBox.Show("Kardex");
                    break;
                default:
                    reporte = 0;
                    break;
            }
        }

        private void reportesFavoritosListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            reportesBaseListBox.SelectedIndex = -1;
            switch (reportesFavoritosListBox.SelectedIndex)
            {
                case 0: // kardex diario
                    reporte = 3;
                    //MessageBox.Show("kardex diario");
                    break;
                case 1: // kardex mensual
                    reporte = 4;
                    //MessageBox.Show("Kardex mensual");
                    break;
                default:
                    reporte = 0;
                    break;
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            MA_ReportesViewModel padre_DataContext = this.main.DataContext as MA_ReportesViewModel;

            if (reporte == 1) //Reporte de Stock
            {
                MessageBox.Show("La opción de enviar por mail solo está habilitada para el reporte KARDEX.");
                return;
            }
            else if (reporte >= 2 && reporte <= 4)
            {
                MA_KardexVisor visor = new MA_KardexVisor((DateTime)fechaDesdePicker.SelectedDate,
                                                          (DateTime)fechaHastaPicker.SelectedDate,
                                                          (Tienda)this.cmbTienda.SelectedItem);                
            

                MA_DatosMailWindow v = new MA_DatosMailWindow(visor);
                v.Owner = this;
                v.ShowDialog();
            }
        }

       

        

        
    }
}
