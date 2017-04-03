namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadidoTemp : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FacturacionPremiosModels", "Temp", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.FacturacionPremiosModels", "Temp");
        }
    }
}
