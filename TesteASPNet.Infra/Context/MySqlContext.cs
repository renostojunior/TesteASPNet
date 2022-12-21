using TesteASPNet.Domain.Entity;
using TesteASPNet.Infra.Mapping;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace TesteASPNet.Infra.Context
{
    public class MySqlContext:DbContext

    {
        public MySqlContext(DbContextOptions<MySqlContext> options) : base(options) 
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

          //  modelBuilder.Entity<Product>(new ProductMap().Configure);

            modelBuilder.Entity<Product>().HasData(
             new Product()
             {
                 Id = 1,
                 Description = "Produto teste",
                 ManufacturingDate = DateTime.Now,
                 ExpirationDate = DateTime.Now.AddDays(1),
                 Status = Product.StatusSituation.Active,
                 VendorCNPJ = "11111111111111",
                 VendorCode = "FORNECE52",
                 VendorName = "Fornecedor de Produtos"
             }
         ) ; 
        }
    }
}

