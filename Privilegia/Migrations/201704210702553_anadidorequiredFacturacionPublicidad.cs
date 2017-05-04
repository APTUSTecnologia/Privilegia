namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadidorequiredFacturacionPublicidad : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FacturacionPublicidadModels", "Total", c => c.String(nullable: false));
            AlterColumn("dbo.FacturacionPublicidadModels", "Titulo", c => c.String(nullable: false));
            AlterColumn("dbo.FacturacionPublicidadModels", "Concepto", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FacturacionPublicidadModels", "Concepto", c => c.String());
            AlterColumn("dbo.FacturacionPublicidadModels", "Titulo", c => c.String());
            AlterColumn("dbo.FacturacionPublicidadModels", "Total", c => c.String());
        }
    }
}
