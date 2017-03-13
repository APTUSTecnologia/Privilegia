namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadidoPublicidad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EspacioPublicidadModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
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
                "dbo.PublicidadModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IdPartner = c.String(nullable: false),
                        Importe = c.String(nullable: false),
                        FechaInicio = c.String(nullable: false),
                        FechaFin = c.String(nullable: false),
                        NombreEspacioPublicidad = c.String(),
                        NombreParteEspacioPublicidad = c.String(),
                        FechaCreacion = c.String(),
                        Partner_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartnerModels", t => t.Partner_Id)
                .Index(t => t.Partner_Id);
            
            AlterColumn("dbo.ProductoModels", "IdPartner", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PublicidadModels", "Partner_Id", "dbo.PartnerModels");
            DropIndex("dbo.PublicidadModels", new[] { "Partner_Id" });
            AlterColumn("dbo.ProductoModels", "IdPartner", c => c.String());
            DropTable("dbo.PublicidadModels");
            DropTable("dbo.ParteEspacioPublicidadModels");
            DropTable("dbo.EspacioPublicidadModels");
        }
    }
}
