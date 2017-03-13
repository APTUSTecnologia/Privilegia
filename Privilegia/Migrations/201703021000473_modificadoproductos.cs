namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificadoproductos : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ProductoModels", name: "Proveedor_Id", newName: "Partner_Id");
            RenameIndex(table: "dbo.ProductoModels", name: "IX_Proveedor_Id", newName: "IX_Partner_Id");
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
            DropTable("dbo.TipoModels");
            RenameIndex(table: "dbo.ProductoModels", name: "IX_Partner_Id", newName: "IX_Proveedor_Id");
            RenameColumn(table: "dbo.ProductoModels", name: "Partner_Id", newName: "Proveedor_Id");
        }
    }
}
