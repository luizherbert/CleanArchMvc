﻿using CleanArchMvc.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchMvc.Infra.Data.EntitiesConfiguration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Name)
                                .IsRequired()
                                .HasMaxLength(100);

            builder.HasData(new Category(1, "Material Escolar"), 
                            new Category(2, "Eletrônicos"),	
                            new Category(3, "Acessórios"));
                                                         
        }
    }
}