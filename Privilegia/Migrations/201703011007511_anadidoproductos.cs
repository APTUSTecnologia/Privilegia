namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadidoproductos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ProductoModels",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        FechaAlta = c.String(),
                        FechaBaja = c.String(),
                        Descripcion = c.String(),
                        Estado = c.String(nullable: false),
                        Codigo = c.String(nullable: false),
                        PremioValor = c.Single(nullable: false),
                        ComisionValor = c.Single(nullable: false),
                        IdPartner = c.String(),
                        FechaCreacion = c.String(),
                        Proveedor_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.PartnerModels", t => t.Proveedor_Id)
                .Index(t => t.Proveedor_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ProductoModels", "Proveedor_Id", "dbo.PartnerModels");
            DropIndex("dbo.ProductoModels", new[] { "Proveedor_Id" });
            DropTable("dbo.ProductoModels");
        }
    }
}
