namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadidoFacturacionPublicidad : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FacturacionPublicidadModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        IdPartner = c.String(nullable: false),
                        IdPublicidad = c.String(nullable: false),
                        FechaCreacion = c.String(),
                        Partner_Id = c.Guid(),
                        Publicidad_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartnerModels", t => t.Partner_Id)
                .ForeignKey("dbo.PublicidadModels", t => t.Publicidad_Id)
                .Index(t => t.Partner_Id)
                .Index(t => t.Publicidad_Id);
            
            AddColumn("dbo.PublicidadModels", "IdFactura", c => c.String());
            AlterColumn("dbo.PartnerModels", "Estado", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FacturacionPublicidadModels", "Publicidad_Id", "dbo.PublicidadModels");
            DropForeignKey("dbo.FacturacionPublicidadModels", "Partner_Id", "dbo.PartnerModels");
            DropIndex("dbo.FacturacionPublicidadModels", new[] { "Publicidad_Id" });
            DropIndex("dbo.FacturacionPublicidadModels", new[] { "Partner_Id" });
            AlterColumn("dbo.PartnerModels", "Estado", c => c.String());
            DropColumn("dbo.PublicidadModels", "IdFactura");
            DropTable("dbo.FacturacionPublicidadModels");
        }
    }
}
