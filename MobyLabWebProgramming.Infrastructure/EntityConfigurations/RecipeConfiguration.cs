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
    public class RecipeConfiguration: IEntityTypeConfiguration<Recipe>
    {
        public void Configure(EntityTypeBuilder<Recipe> builder)
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
            builder.Property(e => e.VideoPath)
                .HasMaxLength(255)
                .IsRequired();
            builder.Property(e => e.Servings)
                .IsRequired();
            builder.Property(e => e.PrepTime)
                .IsRequired();
            builder.Property(e => e.CookTime)
                .IsRequired();
            builder.Property(e => e.TotalTime)
                .IsRequired();
            builder.Property(e => e.CreatedAt)
                .IsRequired();
            builder.Property(e => e.UpdatedAt)
                .IsRequired();
            builder.Property(e => e.Instructions)
                .IsRequired();
            builder.HasMany(e => e.Ingredients);
        }
    }
}
