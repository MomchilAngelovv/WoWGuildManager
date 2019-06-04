using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using WowGuildManager.Domain.Characters;
using WowGuildManager.Domain.Identity;

namespace WowGuildManager.Data
{
    public class WowGuildManagerDbContext : IdentityDbContext<WowGuildManagerUser, WowGuildManagerRole, string>
    {
        public DbSet<Character> Characters { get; set; }

        public WowGuildManagerDbContext(DbContextOptions<WowGuildManagerDbContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
