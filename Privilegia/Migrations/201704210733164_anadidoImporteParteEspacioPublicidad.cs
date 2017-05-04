namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadidoImporteParteEspacioPublicidad : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParteEspacioPublicidadModels", "Importe", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ParteEspacioPublicidadModels", "Importe");
        }
    }
}
