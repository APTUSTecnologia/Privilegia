namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class anadidoconceptotituloytotalfacturapremios : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.FacturasPremiosModels", "Concepto", c => c.String());
            AddColumn("dbo.FacturasPremiosModels", "Total", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FacturasPremiosModels", "Total");
            DropColumn("dbo.FacturasPremiosModels", "Concepto");
        }
    }
}
