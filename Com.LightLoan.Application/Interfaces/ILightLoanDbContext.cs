using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Com.LightLoan.Domain.Entities;

namespace Com.LightLoan.Application.Interface
{
    public interface ILightLoanDbContext
    {
        DbSet<Branch> Branches { get; set; }
        DbSet<Audit> Audit {get;set;}
        DbSet<Address> Addresses {get;set;}

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}