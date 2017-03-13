namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadidoIdPartnerEnDireccionSecundaria : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.DireccionModels", "PartnerId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.DireccionModels", "PartnerId");
        }
    }
}
