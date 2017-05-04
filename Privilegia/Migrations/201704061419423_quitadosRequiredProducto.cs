namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quitadosRequiredProducto : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductoModels", "Codigo", c => c.String());
            AlterColumn("dbo.ProductoModels", "TipoPremio", c => c.String(maxLength: 1));
            AlterColumn("dbo.ProductoModels", "TipoComision", c => c.String(maxLength: 1));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductoModels", "TipoComision", c => c.String(nullable: false, maxLength: 1));
            AlterColumn("dbo.ProductoModels", "TipoPremio", c => c.String(nullable: false, maxLength: 1));
            AlterColumn("dbo.ProductoModels", "Codigo", c => c.String(nullable: false));
        }
    }
}
