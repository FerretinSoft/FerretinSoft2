using pe.edu.pucp.ferretin.viewmodel.MVentas;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
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
using System.Windows.Xps;
using System.Windows.Xps.Packaging;
using System.Web;
using CrystalDecisions.Shared;

namespace pe.edu.pucp.ferretin.view.MVentas
{
    /// <summary>
    /// Lógica de interacción para MV_VisorReporte.xaml
    /// </summary>
    public partial class MV_VisorReporte : Window
    {
        public MV_VisorReporte()
        {

        }

        public MV_VisorReporte(DateTime fechaInicio, DateTime fechaFin, int selectedItem, string nombreReporte, string codEmpleado, string codCliente, string codProducto)
        {
            InitializeComponent();
            if (nombreReporte.Equals("RTienda"))
            {
                ReporteVentaTienda rep;
                rep = new ReporteVentaTienda();

                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                rep.SetParameterValue("idTienda", selectedItem);
                rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
                rep.Refresh();

                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                rep.SetParameterValue("idTienda", selectedItem);
                VisorReporte.ViewerCore.ReportSource = rep;
            }
            else if (nombreReporte.Equals("RCliente"))
            {
                ReporteVentaCliente rep;
                rep = new ReporteVentaCliente();
                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                if (codCliente != "")
                    rep.SetParameterValue("codCliente", Convert.ToInt32(codCliente));
                else
                    rep.SetParameterValue("codCliente", 0);
                rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
                rep.Refresh();
                if (codCliente != "")
                    rep.SetParameterValue("codCliente", Convert.ToInt32(codCliente));
                else
                    rep.SetParameterValue("codCliente", 0);
                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                VisorReporte.ViewerCore.ReportSource = rep;
            }
            else if (nombreReporte.Equals("RProducto"))
            {
                ReporteVentaProducto rep;
                rep = new ReporteVentaProducto();
                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                rep.SetParameterValue("idTienda", selectedItem);
                rep.SetParameterValue("codProducto", codProducto);
                rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
                rep.Refresh();
                rep.SetParameterValue("codProducto", codProducto);
                rep.SetParameterValue("idTienda", selectedItem);
                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                VisorReporte.ViewerCore.ReportSource = rep;
            }
            else
            {
                ReporteVentaVendedor rep;
                rep = new ReporteVentaVendedor();
                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                rep.SetParameterValue("codEmpleado", codEmpleado);
                rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
                rep.Refresh();
                rep.SetParameterValue("codEmpleado", codEmpleado);
                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                //Exportar a PDF//
                /*rep.ExportOptions.ExportFormatType = ExportFormatType.PortableDocFormat;
                rep.ExportOptions.ExportDestinationType = ExportDestinationType.DiskFile;
                DiskFileDestinationOptions objDiskOpt = new DiskFileDestinationOptions();
                objDiskOpt.DiskFileName = @"D:\Cursos\TFA.pdf";
                rep.ExportOptions.DestinationOptions = objDiskOpt;
                rep.Export();*/
                //

            }


        }

        private void VisorReporte_Refresh(object source, SAPBusinessObjects.WPF.Viewer.ViewerEventArgs e)
        {
            //rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
        }

        internal void enviarEmail(DateTime fechaInicio, DateTime fechaFin, int selectedItem, string nombreReporte, string codEmpleado, string codCliente, string codProducto,
            string email, string mensajeEmail)
        {

            if (nombreReporte.Equals("RTienda"))
            {
                ReporteVentaTienda rep;
                rep = new ReporteVentaTienda();

                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                rep.SetParameterValue("idTienda", selectedItem);
                rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
                rep.Refresh();

                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                rep.SetParameterValue("idTienda", selectedItem);
                try
                {//Mandamos el email
                    MailMessage message = new MailMessage(
                       "ferretinsoft@pucp.edu.pe",
                       email,
                       "FerretinSoft: Reporte de ventas por tienda", null);
                    message.Body = mensajeEmail;
                    message.Attachments.Add(new Attachment(rep.ExportToStream(ExportFormatType.PortableDocFormat), "ReporteVenta-Tienda.pdf"));
                    SmtpClient client = new SmtpClient();
                    client.Credentials = new System.Net.NetworkCredential("ferretinsoft@gmail.com", "hUasnASiraQ");
                    client.Port = 587;
                    client.Host = "smtp.gmail.com";
                    client.EnableSsl = true;
                    client.Send(message);
                    MessageBox.Show("Email enviado correctamente", "Mensaje de confirmación",MessageBoxButton.OK,MessageBoxImage.Information);
                }
                catch (Exception e)
                {
                    MessageBox.Show("Ocurrió un error al enviar el email, inténtelo más tarde.\nDetalles:\n" + e.Message, "ERROR", MessageBoxButton.OK, MessageBoxImage.Error);
                }
               
            }
            else if (nombreReporte.Equals("RCliente"))
            {//Mandamos el email
                ReporteVentaCliente rep;
                rep = new ReporteVentaCliente();
                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                if (codCliente != "")
                    rep.SetParameterValue("codCliente", Convert.ToInt32(codCliente));
                else
                    rep.SetParameterValue("codCliente", 0);
                rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
                rep.Refresh();
                if (codCliente != "")
                    rep.SetParameterValue("codCliente", Convert.ToInt32(codCliente));
                else
                    rep.SetParameterValue("codCliente", 0);
                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                try
                {
                    MailMessage message = new MailMessage(
                       "ferretinsoft@pucp.edu.pe",
                       email,
                       "FerretinSoft: Reporte de ventas por cliente", null);
                    message.Body = mensajeEmail;
                    
                    message.Attachments.Add(new Attachment(rep.ExportToStream(ExportFormatType.PortableDocFormat), "ReporteVenta-Cliente.pdf"));
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
            else if (nombreReporte.Equals("RProducto"))
            {//Mandamos el email
                ReporteVentaProducto rep;
                rep = new ReporteVentaProducto();
                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                rep.SetParameterValue("idTienda", selectedItem);
                rep.SetParameterValue("codProducto", codProducto);
                rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
                rep.Refresh();
                rep.SetParameterValue("codProducto", codProducto);
                rep.SetParameterValue("idTienda", selectedItem);
                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                try
                {
                    
                    MailMessage message = new MailMessage(
                       "ferretinsoft@pucp.edu.pe",
                       email,
                       "FerretinSoft: Reporte de ventas por producto", null);
                    message.Body = mensajeEmail;
                    
                    message.Attachments.Add(new Attachment(rep.ExportToStream(ExportFormatType.PortableDocFormat), "ReporteVenta-Producto.pdf"));
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
            else
            {
                ReporteVentaVendedor rep;
                rep = new ReporteVentaVendedor();
                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                rep.SetParameterValue("codEmpleado", codEmpleado);
                rep.SetDatabaseLogon("inf245g2usr", "server", "inti.lab.inf.pucp.edu.pe", "inf245g2");
                rep.Refresh();
                rep.SetParameterValue("codEmpleado", codEmpleado);
                rep.SetParameterValue("fechaInicio", fechaInicio);
                rep.SetParameterValue("fechaFin", fechaFin);
                try
                {//Mandamos el email
                    MailMessage message = new MailMessage(
                       "ferretinsoft@pucp.edu.pe",
                       email,
                       "FerretinSoft: Reporte de ventas por vendedor", null);
                    message.Body = mensajeEmail;
                    
                    message.Attachments.Add(new Attachment(rep.ExportToStream(ExportFormatType.PortableDocFormat), "ReporteVenta-Vendedor.pdf"));
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
                    MessageBox.Show("Ocurrió un error al enviar el email, inténtelo más tarde.\nDetalles:\n" + e.Message, "ERROR", MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
                
            }
        }
    }

