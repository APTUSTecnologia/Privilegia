namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Inicial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DireccionModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Calle = c.String(nullable: false, maxLength: 100),
                        Numero = c.String(nullable: false, maxLength: 4),
                        CodigoPostal = c.String(nullable: false, maxLength: 5),
                        Municipio = c.String(nullable: false, maxLength: 50),
                        Provincia = c.String(nullable: false, maxLength: 50),
                        PartnerId = c.String(),
                        Type = c.String(maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EspacioPublicidadModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FacturacionPremiosModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        NifProveed = c.String(nullable: false, maxLength: 10),
                        CodigoPcto = c.String(nullable: false, maxLength: 10),
                        CodigoCliente = c.String(nullable: false, maxLength: 20),
                        FechaPago = c.String(nullable: false, maxLength: 10),
                        NumeroPedido = c.String(nullable: false, maxLength: 20),
                        Descripcion = c.String(nullable: false, maxLength: 80),
                        Valor = c.String(nullable: false, maxLength: 8),
                        FechaContratacion = c.String(nullable: false, maxLength: 10),
                        FechaBaja = c.String(maxLength: 8),
                        FechaDeAnulacion = c.String(maxLength: 8),
                        Situacion = c.String(nullable: false, maxLength: 1),
                        Observaciones = c.String(maxLength: 150),
                        ImportePago = c.String(nullable: false, maxLength: 8),
                        SituacionPago = c.String(nullable: false, maxLength: 1),
                        ComisionPago = c.String(nullable: false, maxLength: 8),
                        NumeroMutualista = c.String(nullable: false, maxLength: 8),
                        IdFactura = c.String(),
                        FechaDeCreacion = c.String(),
                    })
                .PrimaryKey(t => new { t.Id, t.NifProveed, t.CodigoPcto, t.CodigoCliente, t.FechaPago })
                .ForeignKey("dbo.IncidenciaModels", t => t.Id, cascadeDelete: true)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.IncidenciaModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FechaDeCreacion = c.String(),
                        CifPartner = c.String(),
                        NombreFichero = c.String(),
                        Linea = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.FacturacionPublicidadModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IdFactura = c.String(),
                        IdPartner = c.String(nullable: false),
                        IdPublicidad = c.String(nullable: false),
                        PlanDeMedios = c.Boolean(nullable: false),
                        Total = c.String(),
                        FechaCreacion = c.String(),
                        Partner_Id = c.Guid(),
                        Publicidad_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartnerModels", t => t.Partner_Id)
                .ForeignKey("dbo.PublicidadModels", t => t.Publicidad_Id)
                .Index(t => t.Partner_Id)
                .Index(t => t.Publicidad_Id);
            
            CreateTable(
                "dbo.PartnerModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        Cif = c.String(nullable: false, maxLength: 9),
                        Estado = c.String(nullable: false),
                        FechaAlta = c.String(nullable: false),
                        FechaBaja = c.String(),
                        FechaCreacion = c.String(),
                        Tipo = c.String(),
                        ActividadProfesional = c.String(),
                        Comisiones = c.Boolean(),
                        Premios = c.Boolean(),
                        Observaciones = c.String(maxLength: 100),
                        DireccionPrincipal_Id = c.Guid(),
                        Logo_Id = c.Guid(),
                        Type = c.String(maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DireccionModels", t => t.DireccionPrincipal_Id)
                .ForeignKey("dbo.Logoes", t => t.Logo_Id)
                .Index(t => t.DireccionPrincipal_Id)
                .Index(t => t.Logo_Id);
            
            CreateTable(
                "dbo.Logoes",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FileName = c.String(maxLength: 255),
                        ContentType = c.String(maxLength: 100),
                        Content = c.Binary(),
                        FileType = c.Int(nullable: false),
                        IdPartner = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.PersonaContactoModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false),
                        Apellidos = c.String(nullable: false),
                        Telefono = c.String(nullable: false),
                        Cargo = c.String(nullable: false),
                        Principal = c.Boolean(nullable: false),
                        PartnerId = c.String(),
                        PartnerInterno_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartnerModels", t => t.PartnerInterno_Id)
                .Index(t => t.PartnerInterno_Id);
            
            CreateTable(
                "dbo.PublicidadModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IdPartner = c.String(nullable: false),
                        Importe = c.String(nullable: false),
                        Descuento = c.String(),
                        PlanDeMedios = c.Boolean(nullable: false),
                        Total = c.String(nullable: false),
                        FechaInicio = c.String(nullable: false),
                        FechaFin = c.String(nullable: false),
                        NombreEspacioPublicidad = c.String(nullable: false),
                        NombreParteEspacioPublicidad = c.String(nullable: false),
                        IdFactura = c.String(),
                        FechaCreacion = c.String(),
                        Partner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartnerModels", t => t.Partner_Id)
                .Index(t => t.Partner_Id);
            
            CreateTable(
                "dbo.FacturasPremiosModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FechaDeCreacion = c.String(),
                        Estado = c.String(),
                        IdPartner = c.String(),
                        Partner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartnerModels", t => t.Partner_Id)
                .Index(t => t.Partner_Id);
            
            CreateTable(
                "dbo.ParteEspacioPublicidadModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(),
                        IdEspacioPublicidad = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ProductoModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false),
                        FechaAlta = c.String(nullable: false),
                        FechaBaja = c.String(),
                        Descripcion = c.String(),
                        Estado = c.String(nullable: false),
                        Codigo = c.String(nullable: false),
                        PremioValor = c.Single(nullable: false),
                        TipoPremio = c.String(nullable: false, maxLength: 1),
                        TipoComisionIndirecta = c.String(maxLength: 1),
                        ComisionValor = c.Single(nullable: false),
                        ImporteMutualidad = c.Single(nullable: false),
                        ImporteMutualista = c.Single(nullable: false),
                        TipoComision = c.String(nullable: false, maxLength: 1),
                        IdPartner = c.String(nullable: false),
                        FechaCreacion = c.String(),
                        Partner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartnerModels", t => t.Partner_Id)
                .Index(t => t.Partner_Id);
            
            CreateTable(
                "dbo.TipoModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductoModels", "Partner_Id", "dbo.PartnerModels");
            DropForeignKey("dbo.FacturasPremiosModels", "Partner_Id", "dbo.PartnerModels");
            DropForeignKey("dbo.FacturacionPublicidadModels", "Publicidad_Id", "dbo.PublicidadModels");
            DropForeignKey("dbo.PublicidadModels", "Partner_Id", "dbo.PartnerModels");
            DropForeignKey("dbo.FacturacionPublicidadModels", "Partner_Id", "dbo.PartnerModels");
            DropForeignKey("dbo.PersonaContactoModels", "PartnerInterno_Id", "dbo.PartnerModels");
            DropForeignKey("dbo.PartnerModels", "Logo_Id", "dbo.Logoes");
            DropForeignKey("dbo.PartnerModels", "DireccionPrincipal_Id", "dbo.DireccionModels");
            DropForeignKey("dbo.FacturacionPremiosModels", "Id", "dbo.IncidenciaModels");
            DropIndex("dbo.ProductoModels", new[] { "Partner_Id" });
            DropIndex("dbo.FacturasPremiosModels", new[] { "Partner_Id" });
            DropIndex("dbo.PublicidadModels", new[] { "Partner_Id" });
            DropIndex("dbo.PersonaContactoModels", new[] { "PartnerInterno_Id" });
            DropIndex("dbo.PartnerModels", new[] { "Logo_Id" });
            DropIndex("dbo.PartnerModels", new[] { "DireccionPrincipal_Id" });
            DropIndex("dbo.FacturacionPublicidadModels", new[] { "Publicidad_Id" });
            DropIndex("dbo.FacturacionPublicidadModels", new[] { "Partner_Id" });
            DropIndex("dbo.FacturacionPremiosModels", new[] { "Id" });
            DropTable("dbo.TipoModels");
            DropTable("dbo.ProductoModels");
            DropTable("dbo.ParteEspacioPublicidadModels");
            DropTable("dbo.FacturasPremiosModels");
            DropTable("dbo.PublicidadModels");
            DropTable("dbo.PersonaContactoModels");
            DropTable("dbo.Logoes");
            DropTable("dbo.PartnerModels");
            DropTable("dbo.FacturacionPublicidadModels");
            DropTable("dbo.IncidenciaModels");
            DropTable("dbo.FacturacionPremiosModels");
            DropTable("dbo.EspacioPublicidadModels");
            DropTable("dbo.DireccionModels");
        }
    }
}
