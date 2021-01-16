using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace TP_PWEB.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        [Display(Name = "Nome Utilizador")]
        public string NomeCompleto { get; set; }
        

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("eShopping_Connection", throwIfV1Schema: false)
        {
        }
        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<EmpresaFuncionario> Funcionarios { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<LinhaCompra> LinhaCompras { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			base.OnModelCreating(modelBuilder);

			modelBuilder.Entity<Produto>().ToTable("Produtos");
			modelBuilder.Entity<Promocao>().ToTable("Promocoes");
			modelBuilder.Entity<EmpresaFuncionario>().
				HasKey(i => new
				{
					i.IdEmpresa,
					i.ApplicationUserId
				});
		}

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

    }
}