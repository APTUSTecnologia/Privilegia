namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificadoIncidencia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.IncidenciaModels", "CampoErroneo", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.IncidenciaModels", "CampoErroneo");
        }
    }
}
