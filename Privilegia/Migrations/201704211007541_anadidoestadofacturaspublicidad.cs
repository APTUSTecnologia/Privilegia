namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadidoestadofacturaspublicidad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FacturacionPublicidadModels", "Estado", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FacturacionPublicidadModels", "Estado");
        }
    }
}
