using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Huertos_Autosustentables.Models;

namespace Huertos_Autosustentables.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Huertos_Autosustentables.Models.Clima> Clima { get; set; }
        public DbSet<Huertos_Autosustentables.Models.Cultivo> Cultivo { get; set; }
        public DbSet<Huertos_Autosustentables.Models.DetalleUsersCultivo> DetalleUsersCultivo { get; set; }
        public DbSet<Huertos_Autosustentables.Models.Region> Region { get; set; }
        public DbSet<Huertos_Autosustentables.Models.TipoCultivo> TipoCultivo { get; set; }
    }
}
