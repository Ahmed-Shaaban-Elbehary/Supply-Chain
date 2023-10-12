using System.ComponentModel.DataAnnotations;

namespace SupplyChain.App.Utils.Validations
{
    /// <summary>
    ///  Specifies that the class or method that this attribute, to validate if the date in future.
    /// </summary>
    public class FutureDateAttribute : ValidationAttribute
    {
        /// <summary>
        ///  Override of <see cref="ValidationAttribute.IsValid(object)" />
        /// </summary>
        /// <param name="value">The value to test</param>
        /// <returns>flase if the datetime input after the Datetime.Now</returns>
        public override bool IsValid(object value)
        {
            DateTime date;

            if (DateTime.TryParse(value.ToString(), out date))
            {
                return date > DateTime.Now;
            }

            return false;
        }
    }
}
