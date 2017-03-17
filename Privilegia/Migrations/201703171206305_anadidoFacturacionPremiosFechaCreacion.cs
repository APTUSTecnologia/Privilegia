namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadidoFacturacionPremiosFechaCreacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FacturacionPremiosModels", "FechaDeCreacion", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FacturacionPremiosModels", "FechaDeCreacion");
        }
    }
}
