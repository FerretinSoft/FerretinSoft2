using CrystalDecisions.Shared;
using pe.edu.pucp.ferretin.model;
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

namespace pe.edu.pucp.ferretin.view.MAlmacen
{
    /// <summary>
    /// Lógica de interacción para KardexVisor.xaml
    /// </summary>
    public partial class MA_KardexVisor : Window
    {
        DateTime fechaDesde;
        DateTime fechaHasta;
        Tienda almacen;
        ReporteKardex kardex;
        public MA_KardexVisor(DateTime fechaDesde, DateTime fechaHasta, Tienda almacen)
        {
            InitializeComponent();
            this.fechaDesde = fechaDesde;
            this.fechaHasta = fechaHasta;
            this.almacen = almacen;

        }

        public void GenerarReporte()
        {
            kardex = new ReporteKardex();

            kardex.SetParameterValue("FechaDesde", fechaDesde);
            kardex.SetParameterValue("FechaHasta", fechaHasta);
            kardex.SetParameterValue("IdTienda", almacen.id);
            kardex.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
            kardex.Refresh();

            kardex.SetParameterValue("FechaDesde", fechaDesde);
            kardex.SetParameterValue("FechaHasta", fechaHasta);
            kardex.SetParameterValue("IdTienda", almacen.id);

            KardexVisor.ViewerCore.ReportSource = kardex;
        }

        private void KardexVisor_Refresh(object source, SAPBusinessObjects.WPF.Viewer.ViewerEventArgs e)
        {
            kardex.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");

        }

        public void enviarEmail(string email, string mensajeEmail)
        {
            kardex = new ReporteKardex();

            kardex.SetParameterValue("FechaDesde", fechaDesde);
            kardex.SetParameterValue("FechaHasta", fechaHasta);
            kardex.SetParameterValue("IdTienda", almacen.id);
            kardex.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
            kardex.Refresh();

            kardex.SetParameterValue("FechaDesde", fechaDesde);
            kardex.SetParameterValue("FechaHasta", fechaHasta);
            kardex.SetParameterValue("IdTienda", almacen.id);
            try
            {//Mandamos el email
                MailMessage message = new MailMessage(
                   "ferretinsoft@pucp.edu.pe",
                   email,
                   "FerretinSoft: Reporte KARDEX", null);
                message.Body = mensajeEmail;

                message.Attachments.Add(new Attachment(kardex.ExportToStream(ExportFormatType.PortableDocFormat), "Reporte_KARDEX.pdf"));
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
