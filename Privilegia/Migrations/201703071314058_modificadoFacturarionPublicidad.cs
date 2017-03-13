namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificadoFacturarionPublicidad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FacturacionPublicidadModels", "IdFactura", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FacturacionPublicidadModels", "IdFactura");
        }
    }
}
