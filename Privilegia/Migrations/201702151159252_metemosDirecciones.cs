namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class metemosDirecciones : DbMigration
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
                        PartnerInterno_Id = c.Guid(),
                        Type = c.String(maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartnerModels", t => t.PartnerInterno_Id, cascadeDelete: true)
                .Index(t => t.PartnerInterno_Id);
            
            CreateTable(
                "dbo.PartnerModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false, maxLength: 20),
                        Cif = c.String(nullable: false, maxLength: 9),
                        ActividadProfesional = c.String(),
                        Telefono = c.String(),
                        Email = c.String(),
                        Comisiones = c.Boolean(),
                        Premios = c.Boolean(),
                        Observaciones = c.String(),
                        Logo = c.String(),
                        Estado = c.String(),
                        FechaCreacion = c.String(),
                        DireccionPrincipal_Id = c.Guid(),
                        Type = c.String(maxLength: 1, fixedLength: true, unicode: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DireccionModels", t => t.DireccionPrincipal_Id)
                .Index(t => t.DireccionPrincipal_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DireccionModels", "PartnerInterno_Id", "dbo.PartnerModels");
            DropForeignKey("dbo.PartnerModels", "DireccionPrincipal_Id", "dbo.DireccionModels");
            DropIndex("dbo.PartnerModels", new[] { "DireccionPrincipal_Id" });
            DropIndex("dbo.DireccionModels", new[] { "PartnerInterno_Id" });
            DropTable("dbo.PartnerModels");
            DropTable("dbo.DireccionModels");
        }
    }
}
