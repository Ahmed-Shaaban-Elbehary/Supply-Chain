using SupplyChain.App.Utils.Contracts;

namespace SupplyChain.App.Utils
{
    public class UnitOfMeasurments : IUnits
    {
        public Dictionary<int, string> GetMeasurmentsList()
        {
            var units = new Dictionary<int, string>();
            // Adding units to the dictionary
            units.Add(1, "Piece");
            units.Add(2, "Kilogram");
            units.Add(3, "Gram");
            units.Add(4, "Milligram");
            units.Add(5, "Pound");
            units.Add(6, "Ounce");
            units.Add(7, "Ton");
            units.Add(8, "Liter");
            units.Add(9, "Milliliter");
            units.Add(10, "Gallon");
            units.Add(11, "Fluid Ounce");
            units.Add(12, "Meter");
            units.Add(13, "Centimeter");
            units.Add(14, "Millimeter");
            units.Add(15, "Inch");
            units.Add(16, "Foot");
            units.Add(17, "Yard");
            units.Add(18, "Mile");
            units.Add(19, "Square Meter");
            units.Add(20, "Square Foot");
            units.Add(21, "Square Inch");

            return units;
        }
    }
}
