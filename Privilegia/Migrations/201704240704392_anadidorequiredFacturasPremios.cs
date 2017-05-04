namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadidorequiredFacturasPremios : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.FacturasPremiosModels", "IdPartner", c => c.String(nullable: false));
            AlterColumn("dbo.FacturasPremiosModels", "Titulo", c => c.String(nullable: false));
            AlterColumn("dbo.FacturasPremiosModels", "Concepto", c => c.String(nullable: false));
            AlterColumn("dbo.FacturasPremiosModels", "Total", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.FacturasPremiosModels", "Total", c => c.String());
            AlterColumn("dbo.FacturasPremiosModels", "Concepto", c => c.String());
            AlterColumn("dbo.FacturasPremiosModels", "Titulo", c => c.String());
            AlterColumn("dbo.FacturasPremiosModels", "IdPartner", c => c.String());
        }
    }
}
