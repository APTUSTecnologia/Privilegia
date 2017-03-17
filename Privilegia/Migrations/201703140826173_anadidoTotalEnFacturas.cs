namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadidoTotalEnFacturas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FacturacionPublicidadModels", "Total", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FacturacionPublicidadModels", "Total");
        }
    }
}
