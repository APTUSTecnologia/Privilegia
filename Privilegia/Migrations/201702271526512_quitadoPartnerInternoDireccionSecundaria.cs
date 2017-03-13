namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class quitadoPartnerInternoDireccionSecundaria : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DireccionModels", "PartnerInterno_Id", "dbo.PartnerModels");
            AddForeignKey("dbo.DireccionModels", "PartnerInterno_Id", "dbo.PartnerModels", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.DireccionModels", "PartnerInterno_Id", "dbo.PartnerModels");
            AddForeignKey("dbo.DireccionModels", "PartnerInterno_Id", "dbo.PartnerModels", "Id", cascadeDelete: true);
        }
    }
}
