namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificadoFacturacionPremiosQuitadoIncidencia : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FacturacionPremiosModels", "Id", "dbo.IncidenciaModels");
            DropIndex("dbo.FacturacionPremiosModels", new[] { "Id" });
            AddColumn("dbo.FacturacionPremiosModels", "IdIncidencia", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.FacturacionPremiosModels", "IdIncidencia");
            CreateIndex("dbo.FacturacionPremiosModels", "Id");
            AddForeignKey("dbo.FacturacionPremiosModels", "Id", "dbo.IncidenciaModels", "Id", cascadeDelete: true);
        }
    }
}
