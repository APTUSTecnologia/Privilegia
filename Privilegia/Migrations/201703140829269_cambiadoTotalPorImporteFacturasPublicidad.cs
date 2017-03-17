namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class cambiadoTotalPorImporteFacturasPublicidad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FacturacionPublicidadModels", "Importe", c => c.String());
            DropColumn("dbo.FacturacionPublicidadModels", "Total");
        }
        
        public override void Down()
        {
            AddColumn("dbo.FacturacionPublicidadModels", "Total", c => c.String());
            DropColumn("dbo.FacturacionPublicidadModels", "Importe");
        }
    }
}
