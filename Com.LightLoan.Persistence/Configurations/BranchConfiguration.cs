using System;
using Com.LightLoan.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Com.LightLoan.Persistence.Configurations
{
    public class BranchConfiguration : IEntityTypeConfiguration<Branch>
    {
        public void Configure(EntityTypeBuilder<Branch> builder)
        {
            builder
            .HasOne<Address>(s=>s.Address)
            .WithMany(o=>o.Branches)
            .HasForeignKey(f=>f.AddressId);
        }
    }
}
