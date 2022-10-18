using Core.Domain.AggregatesModel.RRHH;
using Core.Domain.AggregatesModel.User;
using Core.Domain.SeedWork;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Core.Infraestructure
{
    public class ApplicationDBContext : DbContext   
    {
        public IHttpContextAccessor _httpContextAccessor;

        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options , IHttpContextAccessor httpContextAccessor) 
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }
        public DbSet<UserAPI> Users { get; set; }       
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<Empleo> Empleos { get; set; }

        //public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        //{
        //    this.ChangeTracker.DetectChanges();
        //    var currentUsername = _httpContextAccessor.GetUsername();

        //    var added = this.ChangeTracker.Entries()
        //                .Where(t => t.Entity is EntityBase && t.State == EntityState.Added)
        //                .Select(t => t.Entity)
        //                .ToArray();

        //    foreach (var entity in added)
        //    {
        //        var baseEntity = entity as EntityBase;
        //        baseEntity.CreationDate = DateTime.Now;
        //        baseEntity.CreatedBy = currentUsername;
        //    }

        //    var modified = this.ChangeTracker.Entries()
        //                .Where(t => t.Entity is EntityBase && t.State == EntityState.Modified)
        //                .Select(t => t.Entity)
        //                .ToArray();

        //    foreach (var entity in modified)
        //    {
        //        var track = entity as EntityBase;
        //        track.ModificationDate = DateTime.Now;
        //        track.ModifiedBy = currentUsername;
        //    }

        //    return base.SaveChangesAsync(cancellationToken);
        //}
    }
}
