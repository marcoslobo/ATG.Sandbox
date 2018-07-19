using ATG.Sandbox.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ATG.Sandbox.Repository
{
    public class PersistentContext : DbContext
    {
        public PersistentContext(DbContextOptions<PersistentContext> options)
            : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
    }
}
