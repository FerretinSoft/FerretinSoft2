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

namespace pe.edu.pucp.ferretin.view.MVentas
{
    /// <summary>
    /// Lógica de interacción para MV_DocProforma.xaml
    /// </summary>
    public partial class MV_DocProforma : Window
    {
        public MV_DocProforma()
        {
            InitializeComponent();
        }
        public void imprimir(){
            this.Show();
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {
                printDialog.PrintVisual(this, "Proforma");
                this.Close();
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

            

            
        }

        internal void enviarEmail()
        {
            try
            {
                var vm = DataContext as MV_DocProformaViewModel;

                MemoryStream lMemoryStream = new MemoryStream();
                Package package = Package.Open(lMemoryStream, FileMode.Create);
                XpsDocument doc = new XpsDocument(package);
                XpsDocumentWriter writer = XpsDocument.CreateXpsDocumentWriter(doc);
                writer.Write(this);
                doc.Close();
                package.Close();

               var pdfXpsDoc = PdfSharp.Xps.XpsModel.XpsDocument.Open(lMemoryStream);
               var file = "proforma-" + vm.proforma.codigo + new Random(99).Next(1, 99).ToString() + ".pdf";
                PdfSharp.Xps.XpsConverter.Convert(pdfXpsDoc, file, 0);

                MailMessage message = new MailMessage(
                   "ferretinsoft@pucp.edu.pe",
                   vm.proforma.destinatario,
                   "FerretinSoft: Solicitud de Proforma",
                   vm.proforma.mensaje == null ? "" : vm.proforma.mensaje);
                Attachment data = new Attachment(file, MediaTypeNames.Application.Octet);
                ContentDisposition disposition = data.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(file);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(file);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(file);
                message.Attachments.Add(data);
                //SmtpClient client = new SmtpClient("palas.pucp.edu.pe");
                //Aquí es donde se hace lo especial 
                SmtpClient client = new SmtpClient();
                client.Credentials = new System.Net.NetworkCredential("ferretinsoft@gmail.com", "hUasnASiraQ");
                client.Port = 587;
                client.Host = "smtp.gmail.com";
                client.EnableSsl = true;
                //client.Credentials = CredentialCache.DefaultNetworkCredentials;
                client.Send(message);

                data.Dispose();

                MessageBox.Show("Email Enviado correctamente");
            }
            catch(Exception e)
            {
                MessageBox.Show("Ocurrió un error al enviar el email, inténtelo más tarde.\nDetalles:\n" + e.Message);
            }
        }
    }
}
