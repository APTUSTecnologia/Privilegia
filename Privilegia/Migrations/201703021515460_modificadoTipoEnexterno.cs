namespace Privilegia.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modificadoTipoEnexterno : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PartnerModels", "Tipo_Id", "dbo.TipoModels");
            DropIndex("dbo.PartnerModels", new[] { "Tipo_Id" });
            AddColumn("dbo.PartnerModels", "Tipo", c => c.String());
            DropColumn("dbo.PartnerModels", "Tipo_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PartnerModels", "Tipo_Id", c => c.Guid());
            DropColumn("dbo.PartnerModels", "Tipo");
            CreateIndex("dbo.PartnerModels", "Tipo_Id");
            AddForeignKey("dbo.PartnerModels", "Tipo_Id", "dbo.TipoModels", "Id");
        }
    }
}
