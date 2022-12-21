using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace TesteASPNet.Application.Model
{
    public class ProductModel
    {
        public int Id { get; set; }     
        public enum StatusSituation {
            [Description("Active")]
            Active,
            [Description("Inactive")]
            Inactive };
        
        public string Description { get; set; }

        public StatusSituation Status { get; set; }

        public DateTime ManufacturingDate { get; set; }

        public DateTime ExpirationDate { get; set; }

        public string VendorCode { get; set; }

        public string VendorName { get; set; }

        public string VendorCNPJ { get; set; }

    }
}
