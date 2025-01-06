using System.ComponentModel.DataAnnotations;

namespace TurfBookingSystem.Models
{
    public class Currency
    {

        [Key]
        public int currencyId { get; set; }
        //public string MyProperty { get; set; }
        public string Mypropertyname { get; set; }
    }
}
