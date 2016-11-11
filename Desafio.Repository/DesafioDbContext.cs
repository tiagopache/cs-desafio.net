using Desafio.Model;
using Desafio.Model.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Desafio.Repository
{
    public class DesafioDbContext : DbContext
    {
        public DesafioDbContext() : base("DesafioDbContext")
        {
            this.BasicContextConfiguration();
        }

        public DbSet<Usuario> Usuarios { get; set; }

        public DbSet<Telefone> Telefones { get; set; }

        public void BasicContextConfiguration()
        {
            this.Configuration.LazyLoadingEnabled = true;
            this.Configuration.ProxyCreationEnabled = true;
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
