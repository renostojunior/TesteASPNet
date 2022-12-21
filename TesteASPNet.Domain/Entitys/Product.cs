using TesteASPNet.Domain.Interfaces;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TesteASPNet.Domain.Entity
{
    [Table("product")]
    public class Product : BaseEntity
    {
        public enum StatusSituation
        {
            [Description("Inativo")]
            Inactive,
            [Description("Ativo")]
            Active
        };

        [Key]
        [Required]
        [Column("product_id")]
        public override int Id { get => base.Id; set => base.Id = value; }

        [Required]
        [Column("description")]
        [StringLength(60)]
        public string Description { get; set; }

        [Required]
        [Column("situation")]
        public StatusSituation Status { get; set; }

        [Required]
        [Column("manufacturing_date")]
        public DateTime ManufacturingDate { get; set; }

        [Required]
        [Column("expiration_date")]
        public DateTime ExpirationDate { get; set; }

        [Column("vendor_code")]
        [StringLength(50)]
        public string VendorCode { get; set; }

        [Column("vendor_name")]
        [StringLength(60)]
        public string VendorName { get; set; }

        [Column("vendor_cnpj")]
        [StringLength(14)]
        public string VendorCNPJ { get; set; }
    }
}
