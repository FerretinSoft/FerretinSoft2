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
    class MC_AdministrarSolAbastecimientoTest
    {
        [TestCase]
        public void todos_documentos_asociados_a_tienda()
        {
            IEnumerable<SolicitudCompra> documentos = MC_SolicitudComprasService.db.SolicitudCompra.Where(dc => dc.almacen_id == null);

            //Act - No hay nada que hacer, porque la accion fue la creacion misma

            int cantRegistros;
            if (documentos == null) cantRegistros = 0;
            else cantRegistros = documentos.Count();

            //Assert - verificar condicion o criterio de aceptacion
            Assert.AreEqual(0, cantRegistros);
        }
    }
}
