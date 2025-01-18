﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TaskSimulation6.Models;

namespace TaskSimulation6.DAL.Configuration
{
    public class AgentConfiguration : IEntityTypeConfiguration<Agent>
    {
        public void Configure(EntityTypeBuilder<Agent> builder)
        {
            builder.Property(x => x.Name)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasOne(x => x.Position)
                .WithMany(x => x.Agents)
                .HasForeignKey(x => x.PositionId);
        }
    }
}
