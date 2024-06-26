﻿using System.ComponentModel.DataAnnotations;

namespace SupplyChain.App.Utils.Validations
{
    /// <summary>
    ///Specifies that the class or method that this attribute, to check if the phone number in right format.
    ///the format is | +X-XXX-XXXX-XXXX
    /// </summary>
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
