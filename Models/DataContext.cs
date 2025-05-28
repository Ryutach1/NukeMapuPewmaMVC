using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ProyectoNukeMapuPewmaMVC.Models
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }
        public DbSet<Artesania> Artesania { get; set; }
        public DbSet<Libro> Libro { get; set; }  
        public DbSet<Ropa> Ropa { get; set; }  
    }
}