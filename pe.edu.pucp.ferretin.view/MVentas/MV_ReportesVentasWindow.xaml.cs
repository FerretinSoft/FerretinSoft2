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
                        repIntroGrid.Visibility = System.Windows.Visibility.Collapsed;
                        repConfGrid.Visibility = System.Windows.Visibility.Visible;
                    
                        padre_DataContext.nombreBoton = "GENERAR";
                        if (this.listaRepDisp.SelectedIndex == 2)
                        {
                            padre_DataContext.aliasRep = "Reporte de ventas por tienda";
                            padre_DataContext.comentRep = "El presente reporte tiene como finalidad mostrar un resumen de las ventas realizadas por tienda en un período de tiempo";
                        }
                        estado = 1;
                        repAntBtn.IsEnabled = true;
                        break;
                    case 1:
                        repConfGrid.Visibility = System.Windows.Visibility.Collapsed;
                        repFinalGrid.Visibility = System.Windows.Visibility.Visible;
                        estado = 2;
                        repSigBtn.IsEnabled = false;
                        break;
                }
            }else{
                padre_DataContext = this.main.DataContext as MV_ReportesViewModel;
                MV_VisorReporte repW = new MV_VisorReporte(padre_DataContext.searchFechaInicio, padre_DataContext.searchFechaFin);
                
                repW.Show();

            }


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
                    padre_DataContext.nombreBoton = "GUARDAR";
                    break;
                case 2:
                    repConfGrid.Visibility = System.Windows.Visibility.Visible;
                    repFinalGrid.Visibility = System.Windows.Visibility.Collapsed;
                    estado = 1;
                    repSigBtn.IsEnabled = true;
                    break;


            }
        }
    }
}
