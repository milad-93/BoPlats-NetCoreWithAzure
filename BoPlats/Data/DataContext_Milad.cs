using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using BoPlats.Models;
using System.Runtime.CompilerServices;

public class DataContext_Milad : DbContext
    {
        public DataContext_Milad (DbContextOptions<DataContext_Milad> options)
            : base(options)
        {
        }

        public DbSet<BoPlats.Models.Apartment> Apartment { get; set; }

        public DbSet<BoPlats.Models.Apply> Apply { get; set; }


    //foreign key
 //   https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key#other-relationship-patterns
      protected override void OnModelCreating(ModelBuilder modelBuilder)
     {

        modelBuilder.Entity<Apply>()
            .HasOne(k => k.Apartment)
            .WithMany(x => x.Apply)
            .HasForeignKey(y => y.ApartmentForeginKey)
           // .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
        
       
    }
}

//publish azure

//https://docs.microsoft.com/en-us/aspnet/core/tutorials/publish-to-azure-webapp-using-vs?view=aspnetcore-3.1