namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadidoTituloFacturacionPublicidad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FacturacionPublicidadModels", "Concepto", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FacturacionPublicidadModels", "Concepto");
        }
    }
}
