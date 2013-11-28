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
    public class MC_AdministrarDocumentoCompraTest
    {
        /************ Test Documentos asociados a una Tienda **************/
        [TestCase]
        public void todos_documentos_asociados_a_tienda()
        {
            IEnumerable<DocumentoCompra> documentos = MC_DocumentoCompraService.db.DocumentoCompra.Where(dc => dc.id_tienda == null);
            
            //Act - No hay nada que hacer, porque la accion fue la creacion misma

            int cantRegistros;
            if (documentos == null) cantRegistros = 0;
            else cantRegistros = documentos.Count();

            //Assert - verificar condicion o criterio de aceptacion
            Assert.AreEqual(0, cantRegistros);
        }

        [TestCase]
        public void cantidad_documentoCompra_subtotal_mas_igv_igual_total()
        {
            // Creo el entorno
            foreach (DocumentoCompra dc in MC_DocumentoCompraService.db.DocumentoCompra)
            {
                decimal? montoSub = dc.subTotal;
                decimal? montoIgv = dc.igv;
                decimal? montoTotal = dc.total;

                decimal? valor = montoTotal - (montoSub + montoIgv);

                //La perdida del monto total sera de entre 0.00 & 0.01 centavos
                //Assert - Verificar la condicion o criterio de aceptacion
                Assert.GreaterOrEqual(valor, 0.00);
                Assert.LessOrEqual(valor, 0.01);
            }
        }

        [TestCase]
        public void codigo_documentoCompra_unico()
        {
            foreach (DocumentoCompra documento in MC_DocumentoCompraService.db.DocumentoCompra)
            {
                //var creo el entorno
                IEnumerable<DocumentoCompra> doc = MC_DocumentoCompraService.db.DocumentoCompra.Where(dcp => dcp.codigo == documento.codigo);

                //Act - No hay nada que hacer, porque la accion fue la creacion misma
                int cantRegistros = doc.Count();

                //Assert - verificar condicion o criterio de aceptacion
                Assert.AreEqual(cantRegistros, 1);
            }
        }
    }
}
