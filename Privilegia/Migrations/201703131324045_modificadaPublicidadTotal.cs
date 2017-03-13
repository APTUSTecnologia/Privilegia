namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificadaPublicidadTotal : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PublicidadModels", "Total", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PublicidadModels", "Total", c => c.String());
        }
    }
}
