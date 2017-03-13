namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificadoPublicidad : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PublicidadModels", "NombreEspacioPublicidad", c => c.String(nullable: false));
            AlterColumn("dbo.PublicidadModels", "NombreParteEspacioPublicidad", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PublicidadModels", "NombreParteEspacioPublicidad", c => c.String());
            AlterColumn("dbo.PublicidadModels", "NombreEspacioPublicidad", c => c.String());
        }
    }
}
