using System.ComponentModel.DataAnnotations;

namespace SupplyChain.App.Utils.Validations
{
    public class CustomPhoneAttribute : RegularExpressionAttribute
    {
        /// <summary>
        /// Custom Phone Number to be in Egyptian Format.
        /// </summary>
        public CustomPhoneAttribute() : base("^\\+d{1}-\\d{3}-\\d{4}-\\d{4}$")
        {
            ErrorMessage = "Invalid phone number format. The correct format is +X-XXX-XXXX-XXXX.";
        }
    }
}
