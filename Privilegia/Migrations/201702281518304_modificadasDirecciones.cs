namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificadasDirecciones : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.DireccionModels", "PartnerInterno_Id", "dbo.PartnerModels");
            DropIndex("dbo.DireccionModels", new[] { "PartnerInterno_Id" });
            DropColumn("dbo.DireccionModels", "PartnerInterno_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.DireccionModels", "PartnerInterno_Id", c => c.Guid());
            CreateIndex("dbo.DireccionModels", "PartnerInterno_Id");
            AddForeignKey("dbo.DireccionModels", "PartnerInterno_Id", "dbo.PartnerModels", "Id");
        }
    }
}
