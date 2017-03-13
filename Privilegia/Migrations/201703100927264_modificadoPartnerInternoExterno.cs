namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificadoPartnerInternoExterno : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ProductoModels", "Nombre", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ProductoModels", "Nombre");
        }
    }
}
