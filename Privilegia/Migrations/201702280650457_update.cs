namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class update : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.PartnerModels", "Observaciones", c => c.String(maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.PartnerModels", "Observaciones", c => c.String());
        }
    }
}
