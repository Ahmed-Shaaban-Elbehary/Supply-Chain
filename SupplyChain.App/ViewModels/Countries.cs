using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace SupplyChain.Core.Models
{
    public class Countries
    {
        public string Name { get; set; } = string.Empty;
        public int Code { get; set; }

        public static List<Countries> GetCountries()
        {
            string jsonString = File.ReadAllText("/Countries.json");
            List<Countries> countries = JsonConvert.DeserializeObject<List<Countries>>(jsonString);
            if (countries == null)
            {
                return new List<Countries>();
            }
            return countries;
        }
    }
}
