namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificadoProductoComisiones : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductoModels", "ImporteMutualidad", c => c.Single(nullable: false));
            AddColumn("dbo.ProductoModels", "ImporteMutualista", c => c.Single(nullable: false));
            AlterColumn("dbo.PartnerModels", "FechaAlta", c => c.String(nullable: false));
            AlterColumn("dbo.ProductoModels", "FechaAlta", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductoModels", "FechaAlta", c => c.String());
            AlterColumn("dbo.PartnerModels", "FechaAlta", c => c.String());
            DropColumn("dbo.ProductoModels", "ImporteMutualista");
            DropColumn("dbo.ProductoModels", "ImporteMutualidad");
        }
    }
}
