using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.viewmodel
{
    public sealed class CamelCaseString
    {
        #region " Constructors "
        private CamelCaseString()
        {
        }

        #endregion

        #region " Methods "

        /// <summary> 
        /// Designed to parse property or database column names and return a friendly name without punctuation characters. Example: "ap_c_FirstName" will result in "First Name" 
        /// </summary> 
        /// <returns>String with words parsed from camel case string and space added between words.</returns> 
        public static string GetWords(string strCamel)
        {

            System.Text.StringBuilder sb = new System.Text.StringBuilder(256);
            bool foundUpper = false;

            foreach (char c in strCamel)
            {

                if (foundUpper)
                {

                    if (char.IsUpper(c))
                    {
                        sb.Append(" ");
                        sb.Append(c);
                    }

                    else if (char.IsLetterOrDigit(c))
                    {
                        sb.Append(c);
                    }
                }

                else if (char.IsUpper(c))
                {
                    foundUpper = true;
                    sb.Append(c);
                }
            }


            return sb.ToString();
        }

        #endregion
    }
}
