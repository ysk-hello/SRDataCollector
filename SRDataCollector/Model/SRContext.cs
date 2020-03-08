using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace SRDataCollector.Model
{
    public class SRContext : DbContext
    {
        public DbSet<Member> Member { get; set; }

        public DbSet<Team> Team { get; set; }

        public DbSet<Data> Data { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseNpgsql("Host=localhost;Database=showroom;Username=postgres;Password=postgres",
                options => options.SetPostgresVersion(new Version(9, 6)));
            //options => options.SetPostgresVersion(new Version(10, 12)));

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Data>()
                .HasKey(d => new { d.MemberId, d.DateTime });

            // http://kuttsun.blogspot.com/2018/01/entity-framework-core_11.html
            // https://docs.microsoft.com/en-us/ef/core/modeling/relationships?tabs=fluent-api%2Cfluent-api-simple-key%2Csimple-key
            modelBuilder.Entity<Data>()
                .HasOne(d => d.Member)
                .WithMany()
                .HasForeignKey(d => d.MemberId);
        }
    }
}
