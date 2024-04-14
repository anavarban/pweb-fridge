using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MobyLabWebProgramming.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MobyLabWebProgramming.Infrastructure.EntityConfigurations
{
    public class IngredientConfiguration : IEntityTypeConfiguration<Ingredient>
    {
        public void Configure(EntityTypeBuilder<Ingredient> builder)
        {
            builder.Property(e => e.Id)
                .IsRequired();
            builder.HasKey(x => x.Id);
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(e => e.Description)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(e => e.ImagePath)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(e => e.Quantity)
                .IsRequired();
            builder.Property(e => e.Unit)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(e => e.ExpiryDate)
                .IsRequired();

            builder.HasMany(e => e.Recipes)
                .WithMany(e => e.Ingredients);
        }
    }
}
