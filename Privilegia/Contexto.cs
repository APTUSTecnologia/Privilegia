using System.Data.Entity;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.ModelConfiguration;
using System.Security.Claims;
using System.Threading.Tasks;
using Privilegia.Models;
using Privilegia.Models.FacturacionPartners;
using Privilegia.Models.FacturasPremios;
using Privilegia.Models.Incidencias;
using Privilegia.Models.Productos;
using Privilegia.Models.Tipos;


namespace Privilegia
{// Puede agregar datos del perfil del usuario agregando más propiedades a la clase ApplicationUser. Para más información, visite http://go.microsoft.com/fwlink/?LinkID=317594.

    public class Contexto : DbContext
    {

        public Contexto()
            : base("name=Contexto")
        {
            Configuration.ProxyCreationEnabled = false;
        }

        public DbSet<Logo> Logos { get; set; }
        public DbSet<PartnerModel> Partners { get; set; }
        public DbSet<DireccionModel> Direcciones { get; set; }
        public DbSet<PersonaContactoModel> PersonasDeContacto { get; set; }
        public DbSet<ProductoModel> Productos { get; set; }
        public DbSet<TipoModel> Tipos { get; set; }
        public DbSet<PublicidadModel> Publicidad { get; set; }
        public DbSet<EspacioPublicidadModel> EspaciosPublicidad { get; set; }
        public DbSet<ParteEspacioPublicidadModel> PartesEspaciosPublicidad { get; set; }
        public DbSet<FacturacionPublicidadModel> FacturacionPublicidad { get; set; }
        public DbSet<FacturacionPremiosModel> FacturacionPremios { get; set; }
        public DbSet<FacturasPremiosModel> FacturasPremios { get; set; }
        public DbSet<IncidenciaModel> Incidencias { get; set; }


       


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new PartnerMap());
            modelBuilder.Configurations.Add(new DireccionesMap());

           
            //modelBuilder.Entity<DireccionSecundaria>()
            //        .HasRequired<PartnerInterno>(s => s.PartnerInterno) // DireccionSecundaria entity requires PartnerInterno 
            //        .WithMany(s => s.DireccionesSecundarias); // PartnerInterno entity includes many DireccionesSecundarias entities

            base.OnModelCreating(modelBuilder);
        }
    }

    public class PartnerMap : EntityTypeConfiguration<PartnerModel>
    {
        public PartnerMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Nombre).HasMaxLength(20).IsRequired();
            Property(x => x.Cif).HasMaxLength(9).IsRequired();

            Map<PartnerInterno>(x => x.Requires("Type")
                                            .HasValue("I")
                                            .HasColumnType("char")
                                            .HasMaxLength(1));

            Map<PartnerExterno>(x => x.Requires("Type")
                                            .HasValue("E"));

        }
    }
    public class DireccionesMap : EntityTypeConfiguration<DireccionModel>
    {
        public DireccionesMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Calle).HasMaxLength(100).IsRequired();
            Property(x => x.Numero).HasMaxLength(4).IsRequired();
            Property(x => x.CodigoPostal).HasMaxLength(5).IsRequired();
            Property(x => x.Municipio).HasMaxLength(50).IsRequired();
            Property(x => x.Provincia).HasMaxLength(50).IsRequired();

            Map<DireccionPrincipal>(x => x.Requires("Type")
                                            .HasValue("P")
                                            .HasColumnType("char")
                                            .HasMaxLength(1));

            Map<DireccionSecundaria>(x => x.Requires("Type")
                                            .HasValue("S"));

        }

    }
}