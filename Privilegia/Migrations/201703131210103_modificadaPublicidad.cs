namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificadaPublicidad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PublicidadModels", "Descuento", c => c.String());
            AddColumn("dbo.PublicidadModels", "PlanDeMedios", c => c.Boolean(nullable: false));
            AddColumn("dbo.PublicidadModels", "Total", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PublicidadModels", "Total");
            DropColumn("dbo.PublicidadModels", "PlanDeMedios");
            DropColumn("dbo.PublicidadModels", "Descuento");
        }
    }
}
