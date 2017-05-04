namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadidotituloenfacturas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FacturacionPublicidadModels", "Titulo", c => c.String());
            AddColumn("dbo.FacturasPremiosModels", "Titulo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FacturasPremiosModels", "Titulo");
            DropColumn("dbo.FacturacionPublicidadModels", "Titulo");
        }
    }
}
