using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using pe.edu.pucp.ferretin.controller.MCompras;
using pe.edu.pucp.ferretin.model;

namespace pe.edu.pucp.ferretin.tdd.MCompras
{
    [TestFixture]
    public class MC_AdministrarGuiaRemisionTest
    {

        [TestCase]
        public void cantidad_total_de_OC_igual_cantidad_recibida_mas_cantidad_restante()
        {
            IEnumerable<DocumentoCompra> documentos = MC_DocumentoCompraService.db.GuiaRemision.Where(dc => dc.DocumentoCompra != null).Select(dc => dc.DocumentoCompra).Distinct();
            //documentos = documentos.Where(dc => dc.id == 167);
            foreach (DocumentoCompra dc in documentos)
            {
                decimal? cantTotal = 0, cantRestante = 0;
                foreach (DocumentoCompraProducto dcp in dc.DocumentoCompraProducto)
                {
                    cantTotal = cantTotal + dcp.cantidad;
                    cantRestante = cantRestante + dcp.cantidadRestante;
                }

                decimal? cantRecibida = 0;
                foreach (GuiaRemision gr in dc.GuiaRemision)
                {
                    foreach (GuiaRemisionProducto grp in gr.GuiaRemisionProducto)
                    {
                        cantRecibida = cantRecibida + grp.cantidadRecibida;
                    }
                }
                
                Assert.AreEqual(cantTotal,cantRestante + cantRecibida);
            }
        }      
    }
}
