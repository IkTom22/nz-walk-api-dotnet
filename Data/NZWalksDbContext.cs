using Microsoft.EntityFrameworkCore;
using NZWalksAPI.Models.Domain;
namespace NZWalksAPI.Data
{
    public class NZWalksDbContext : DbContext
    {
        //ctor double tap tab
        public NZWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions)
        {

        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}