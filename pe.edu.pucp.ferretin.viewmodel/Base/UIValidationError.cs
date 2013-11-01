using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pe.edu.pucp.ferretin.viewmodel
{
    public class UIValidationError
    {
        #region " Declarations "

        private string _dataItemName = string.Empty;
        private string _errorMessage = string.Empty;
        private string _propertyName = string.Empty;

        #endregion

        #region " Properties "

        public string DataItemName
        {
            get { return _dataItemName; }
        }

        public string ErrorMessage
        {
            get { return _errorMessage; }
        }

        public string Key
        {
            get { return string.Format("{0}:{1}", _dataItemName, _propertyName); }
        }

        public string PropertyName
        {
            get { return _propertyName; }
        }

        #endregion

        #region " Constructors "

        public UIValidationError(string strDataItemName, string propertyName, string errorMessage)
        {
            _dataItemName = strDataItemName;
            _propertyName = propertyName;
            _errorMessage = errorMessage;
        }

        #endregion

        #region " Methods "

        public string ToErrorMessage()
        {
            return string.Concat(CamelCaseString.GetWords(this.PropertyName), " ", this.ErrorMessage);
        }

        public string ToFriendlyErrorMessage()
        {

            string errorMessage = null;

            if (this.ErrorMessage.Contains("not recognized as a valid DateTime"))
            {
                errorMessage = "date is not a valid format.";
            }

            else if (this.ErrorMessage.Contains("not in a correct format."))
            {
                errorMessage = "entered value is not the correct data type.";
            }

            else
            {
                //TODO add more tests here 
                errorMessage = this.ErrorMessage;
            }

            string propertyName = null;

            if (this.PropertyName.Contains("."))
            {
                propertyName = CamelCaseString.GetWords(this.PropertyName.Substring(this.PropertyName.LastIndexOf(".") + 1));
            }

            else
            {
                propertyName = CamelCaseString.GetWords(this.PropertyName);
            }

            return string.Concat(propertyName, " ", errorMessage);
        }

        public override string ToString()
        {
            return string.Format("DataItem: {0}, PropertyName: {1}, Error: {2}", _dataItemName, _propertyName, _errorMessage);
        }

        #endregion
    }
}
