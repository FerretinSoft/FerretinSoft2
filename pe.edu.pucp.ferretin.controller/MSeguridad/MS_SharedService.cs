using pe.edu.pucp.ferretin.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.controller.MSeguridad
{
    public class MS_SharedService : ComunService
    {

        /*
         * Metodo: obtenerParametro
         Metodo que recibe un nombre de un parametro y devuelve un STRING con su valor.
         Nombres que recibe: (no importan mayusculas o minusculas).
         -numero intentos
         -tiempo de sesion
         -duracion de clave
         -tipo de cambio
         -IGV
         -vigencia de proforma
         -vigencia de notas de credito
         -cantidad soles por punto         
        */

        public static String obtenerParametro(String nombre)
        {
            List<Parametro> listaParametro = MS_ParametroService.obtenerListaParametros().ToList();

            if (String.Compare(nombre,"numero intentos",true) == 0)
            {
                return listaParametro.ElementAt(0).valor;
            }

            if (String.Compare(nombre, "tiempo de sesion", true) == 0)
            {
                return listaParametro.ElementAt(1).valor;
            }

            if (String.Compare(nombre, "duracion de clave", true) == 0)
            {
                return listaParametro.ElementAt(2).valor;
            }

            if (String.Compare(nombre, "tipo de cambio", true) == 0)
            {
                return listaParametro.ElementAt(3).valor;
            }

            if (String.Compare(nombre, "IGV", true) == 0)
            {
                return listaParametro.ElementAt(4).valor;
            }

            if (String.Compare(nombre, "vigencia de proforma", true) == 0)
            {
                return listaParametro.ElementAt(5).valor;
            }

            if (String.Compare(nombre, "vigencia de notas de credito", true) == 0)
            {
                return listaParametro.ElementAt(6).valor;
            }

            if (String.Compare(nombre, "cantidad soles por punto", true) == 0)
            {
                return listaParametro.ElementAt(7).valor;
            }

            return null;
        }

        public static Double obtenerIGV()
        {
            return Convert.ToDouble(obtenerParametro("IGV"));
        }

        public static Double obtenerTipodeCambio()
        {
            return Convert.ToDouble(obtenerParametro("tipo de cambio"));
        }

    }
}
