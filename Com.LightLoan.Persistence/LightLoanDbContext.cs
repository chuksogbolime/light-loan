using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Com.LightLoan.Application.Interface;
using Com.LightLoan.Domain.Entities;
using Com.LightLoan.Domain.Enums;
using System.Collections.Generic;

namespace Com.LightLoan.Persistence
{
    public class LightLoanDbContext : DbContext, ILightLoanDbContext 
    {
        private IUserService _userService;
        private IDatetimeService _datetimeService;
        public LightLoanDbContext(IUserService userService,
            IDatetimeService datetimeService, DbContextOptions<LightLoanDbContext> options):base(options)
        {
            _userService=userService;
            _datetimeService=datetimeService;
        }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Audit> Audit {get;set;}
        public DbSet<Address> Addresses {get;set;}

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            List<Audit> audits=new List<Audit>();
            foreach(var entry in ChangeTracker.Entries<AuditableEntity>())
            {
                switch(entry.State)
                {
                    case EntityState.Added:
                    entry.Entity.CreatedBy=_userService.UserId;
                    entry.Entity.CreatedDate=_datetimeService.Now.Date;
                    entry.Entity.CreatedDateTime=_datetimeService.Now;
                    entry.Entity.Status = (char)Status.New;
                    
                    break;
                    case EntityState.Modified:
                    //case EntityState.Deleted:
                    entry.Entity.LastModifiedBy=_userService.UserId;
                    entry.Entity.LastModifiedDate=_datetimeService.Now.Date;
                    entry.Entity.LastModifiedDateTime=_datetimeService.Now;
                    //entry.Entity.Status = entry.State==EntityState.Deleted ?(char)Status.Deleted:(char)Status.New;
                    break;
                }
                audits.Add( new Audit
                {
                    Id = Guid.NewGuid(),
                    ActionBy = _userService.UserId,
                    ActionDate = _datetimeService.Now.Date,
                    ActionDateTime = _datetimeService.Now,
                    State = entry.State.ToString(),
                    Name = entry.Entity.GetType().Name,

                });
                //base.Set<Audit>().Add(audit);
                //this.Audit.Add(audit);  
                //this.Attach(audit);             
            }

            return base.SaveChangesAsync(cancellationToken);
        
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(builder);
        }
    }
}