namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificadoPartnerConFechaAltayBaja : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PartnerModels", "FechaAlta", c => c.String());
            AddColumn("dbo.PartnerModels", "FechaBaja", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PartnerModels", "FechaBaja");
            DropColumn("dbo.PartnerModels", "FechaAlta");
        }
    }
}
