using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
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
using CrystalDecisions.Shared;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.view.MCompras
{
    /// <summary>
    /// Lógica de interacción para MA_VisorReporteStock.xaml
    /// </summary>
    public partial class MC_VisorReporteOC : Window
    {
        DateTime fini;
        DateTime ffin;
        int idtienda;
        ReporteOrdenCompra rep;

        public MC_VisorReporteOC(DateTime f_ini, DateTime f_fin, int selectedItem)
        {
            InitializeComponent();
            this.fini = f_ini;
            this.ffin = f_fin;
            this.idtienda = selectedItem;
        }

        public void GenerarReporte()
        {
            rep = new ReporteOrdenCompra();

            rep.SetParameterValue("f_ini", fini);
            rep.SetParameterValue("f_fin", ffin);
            rep.SetParameterValue("idTienda", idtienda);
            rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
            rep.Refresh();

            rep.SetParameterValue("f_ini", fini);
            rep.SetParameterValue("f_fin", ffin);
            rep.SetParameterValue("idTienda", idtienda);
            VisorReporteOC.ViewerCore.ReportSource = rep;
        }

        private void VisorReporteOC_Refresh(object source, SAPBusinessObjects.WPF.Viewer.ViewerEventArgs e)
        {
            rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
        }

        public void enviarEmail(string email, string mensajeEmail)
        {
            rep = new ReporteOrdenCompra();

            rep.SetParameterValue("f_ini", fini);
            rep.SetParameterValue("f_fin", ffin);
            rep.SetParameterValue("idTienda", idtienda);
            rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
            rep.Refresh();

            rep.SetParameterValue("f_ini", fini);
            rep.SetParameterValue("f_fin", ffin);
            rep.SetParameterValue("idTienda", idtienda);
            try
            {//Mandamos el email
                MailMessage message = new MailMessage(
                   "ferretinsoft@pucp.edu.pe",
                   email,
                   "FerretinSoft: Reporte estados de Ordenes de Compra", null);
                message.Body = mensajeEmail;

                message.Attachments.Add(new Attachment(rep.ExportToStream(ExportFormatType.PortableDocFormat), "Reporte_EstadosOC.pdf"));
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("ferretinsoft@gmail.com", "hUasnASiraQ");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                client.Send(message);
                MessageBox.Show("Email enviado correctamente", "Mensaje de confirmación", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception e)
            {
                MessageBox.Show("Ocurrió un error al enviar el email, inténtelo más tarde.\nDetalles:\n" + e.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
            }


        }
    }
}
